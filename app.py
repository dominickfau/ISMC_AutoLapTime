import csv
from datetime import datetime, timedelta
from dataclasses import dataclass
from typing import Dict, List
from PyQt5 import QtCore, QtGui, QtWidgets
from autolaptime.yanzeo_rfid.base import (
    CommandSender,
    ReaderCommand,
    ReaderInfoFrame,
    ReaderResponseFrame,
    Tag,
)
from autolaptime.yanzeo_rfid.transport import BaseTransport, SerialTransport
from autolaptime.yanzeo_rfid.base import G2TagResponseFrame
from autolaptime.customwidgets import CustomQTableWidget
from autolaptime.database import DBContext, create_db_and_tables
from autolaptime.models import Vehicle, CrossFinishLine
from autolaptime.dialogs import ReaderSettingsDialog


responses = []  # type: List[G2TagResponseFrame]
serial_comms_running = True


@dataclass
class ScannedTag:
    tag: Tag
    last_seen: datetime


class SerialCommunicationWorker(QtCore.QObject):
    finished = QtCore.pyqtSignal()
    scanned_tag = QtCore.pyqtSignal(Tag)

    def read_tags(self) -> None:
        global serial_comms_running
        reader_addr = "COM5"
        transport = SerialTransport(device=reader_addr, timeout=None)
        runner = CommandSender(transport)

        with transport as ser:
            while serial_comms_running:
                try:
                    response = G2TagResponseFrame(transport.read_frame())
                    self.scanned_tag.emit(response.tag)
                except KeyboardInterrupt:
                    serial_comms_running = False
                except Exception as err:
                    serial_comms_running = False
                    raise err


class MainWindow(QtWidgets.QMainWindow):
    def __init__(self, parent=None):
        super().__init__()
        self.setWindowTitle("Auto Lap Timer")
        self.resize(1000, 800)
        self.initUi()
        self.connectSignals()

        self.settings = QtCore.QSettings("DF-Software", "SMC Auto Lap Time")
        self.loadSettings()

        self.loadSave()

        self.scanned_tags = {}  # type: Dict[str, ScannedTag]
        # self.startThreads()

    def loadSettings(self) -> None:
        # Load Window Settings
        self.settings.beginGroup("GUI Properties")
        self.restoreState(self.settings.value("mainWindowState", self.saveState()))
        self.resize(self.settings.value("mainWindowSize", self.size()))
        self.move(self.settings.value("mainWindowPosition", self.pos()))
        self.tableSplitter.restoreState(
            self.settings.value("tableSplitter", self.tableSplitter.saveState())
        )
        maximzed = self.settings.value("mainWindowMaximzed", "false")
        if maximzed == "true":
            self.showMaximized()
        self.tagTable.resize_all_columns()
        self.lapTable.resize_all_columns()
        self.settings.endGroup()

    def saveSettings(self) -> None:
        # Save Serial Communication Settings
        self.settings.beginGroup("Serial Communication")
        self.settings.setValue("Port", "COM5")
        self.settings.endGroup()

        # Save Window Settings
        self.settings.beginGroup("GUI Properties")
        self.settings.setValue("tableSplitter", self.tableSplitter.saveState())
        self.settings.setValue("mainWindowState", self.saveState())
        self.settings.setValue("mainWindowSize", self.size())
        self.settings.setValue("mainWindowMaximzed", self.isMaximized())
        self.settings.setValue("mainWindowPosition", self.pos())
        self.tableSplitter.saveState()
        self.settings.endGroup()

    def startThreads(self) -> None:
        global serial_comms_running
        serial_comms_running = True

        self.serial_comm_thread = QtCore.QThread()

        self.serial_comm_worker = SerialCommunicationWorker()
        self.serial_comm_worker.moveToThread(self.serial_comm_thread)

        self.serial_comm_thread.started.connect(self.serial_comm_worker.read_tags)
        self.serial_comm_worker.finished.connect(self.serial_comm_thread.quit)
        self.serial_comm_worker.finished.connect(self.serial_comm_worker.deleteLater)
        self.serial_comm_thread.finished.connect(self.serial_comm_worker.deleteLater)

        self.serial_comm_worker.scanned_tag.connect(self.onNewTagScan)

        self.serial_comm_thread.start()

    def stopThreads(self) -> None:
        global serial_comms_running
        serial_comms_running = False

    def initUi(self) -> None:
        self.menubar = QtWidgets.QMenuBar()
        self.setMenuBar(self.menubar)

        self.menuFIle = QtWidgets.QMenu(self.menubar)
        self.menuFIle.setTitle("File")

        self.actionNew = QtWidgets.QAction()
        self.actionNew.setText("New")
        self.actionOpen = QtWidgets.QAction()
        self.actionOpen.setText("Open")
        self.actionSave = QtWidgets.QAction()
        self.actionSave.setText("Save")
        self.actionImportTags = QtWidgets.QAction()
        self.actionImportTags.setText("Import Tags")
        self.actionExport = QtWidgets.QAction()
        self.actionExport.setText("Export")
        self.actionExit = QtWidgets.QAction()
        self.actionExit.setText("Exit")

        self.menuFIle.addAction(self.actionNew)
        self.menuFIle.addAction(self.actionOpen)
        self.menuFIle.addAction(self.actionSave)
        self.menuFIle.addSeparator()
        self.menuFIle.addAction(self.actionImportTags)
        self.menuFIle.addAction(self.actionExport)
        self.menuFIle.addSeparator()
        self.menuFIle.addAction(self.actionExit)
        self.menubar.addAction(self.menuFIle.menuAction())

        self.menuSetup = QtWidgets.QMenu(self.menubar)
        self.menuSetup.setTitle("Setup")

        self.actionReaderConfig = QtWidgets.QAction()
        self.actionReaderConfig.setText("Reader Configuration")
        self.actionWebsiteAuth = QtWidgets.QAction()
        self.actionWebsiteAuth.setText("Website Authentication")

        self.menuSetup.addAction(self.actionReaderConfig)
        self.menuSetup.addAction(self.actionWebsiteAuth)
        self.menubar.addAction(self.menuSetup.menuAction())

        self.centralwidget = QtWidgets.QWidget(self)
        self.setCentralWidget(self.centralwidget)
        self.mainLayout = QtWidgets.QVBoxLayout(self.centralwidget)
        self.tableSplitter = QtWidgets.QSplitter(QtCore.Qt.Orientation.Horizontal)
        self.tableSplitter.setChildrenCollapsible(False)
        self.mainLayout.addWidget(self.tableSplitter)

        self.tagTable = CustomQTableWidget(self)
        headers = ["School Name", "Vehicle Number", "Team Name", "Tag UID"]
        self.tagTable.setTableHeaders(headers)
        self.tableSplitter.addWidget(self.tagTable)

        self.lapTable = CustomQTableWidget(self)
        headers = ["Index", "Time (HH:MM:SS)"]
        self.lapTable.setTableHeaders(headers)
        self.tableSplitter.addWidget(self.lapTable)

    def closeEvent(self, event=None) -> None:
        """Closes the application."""
        self.saveSettings()
        self.close()

    def loadSave(self) -> None:
        with DBContext() as session:
            vehicles = session.query(Vehicle).all()  # type: List[Vehicle]
            if len(vehicles) == 0:
                return

            data = []
            for vehicle in vehicles:
                data.append(
                    {
                        "SchoolName": vehicle.school_name,
                        "VehicleNumber": vehicle.number,
                        "TeamName": vehicle.team_name,
                        "TagUID": vehicle.tag_uid,
                    }
                )
            self.populateTagTable(data)

    def onActionReaderConfig(self) -> None:
        dialog = ReaderSettingsDialog()
        dialog.exec()

    def onNewTagScan(self, tag: Tag) -> None:
        print(f"New tag scanned. Tag: {tag.uid}")
        previous_scanned_tag = self.scanned_tags.pop(tag.uid, None)
        current_time = datetime.now()
        current_scanned_tag = ScannedTag(tag, current_time)

        if not previous_scanned_tag:
            print("No prev tag.")
            self.scanned_tags[tag.uid] = current_scanned_tag

            with DBContext() as session:
                vehicle = (
                    session.query(Vehicle).filter(Vehicle.tag_uid == tag.uid).first()
                )  # type: Vehicle
                if not vehicle:
                    return
                cross_finish_line = CrossFinishLine(
                    date_created=current_time, vehicle_id=vehicle.id
                )
                session.add(cross_finish_line)
                session.commit()
            return

        delta = current_time - previous_scanned_tag.last_seen
        self.scanned_tags[tag.uid] = current_scanned_tag
        if delta.total_seconds() <= 10:
            return

        with DBContext() as session:
            vehicle = (
                session.query(Vehicle).filter(Vehicle.tag_uid == tag.uid).first()
            )  # type: Vehicle
            if not vehicle:
                return
            cross_finish_line = CrossFinishLine(
                date_created=current_time, vehicle_id=vehicle.id
            )
            session.add(cross_finish_line)
            session.commit()

    def onActionImportTagsClicked(self) -> None:
        REQUIRED_COLUMNS = ["SchoolName", "TeamName", "VehicleNumber", "TagUID"]
        fileName, _ = QtWidgets.QFileDialog.getOpenFileName(
            self, "Import Tags", "", "CSV Files (*.csv)"
        )
        if not fileName:
            return

        with open(fileName, "r") as csv_file:
            reader = csv.DictReader(csv_file, delimiter=",", quoting=csv.QUOTE_NONE)

            # Validate Columns
            headers = [x.strip() for x in reader.fieldnames]
            missing_columns = []
            for required in REQUIRED_COLUMNS:
                if required not in headers:
                    missing_columns.append(required)
            if len(missing_columns) != 0:
                msg = QtWidgets.QMessageBox()
                msg.setWindowTitle("Error")
                msg.setIcon(QtWidgets.QMessageBox.Icon.Warning)
                msg.setText("File missing required column names.")
                msg.setDetailedText(f"Missing {missing_columns}.")
                msg.exec()
                return

            self.populateTagTable([row for row in reader])
            return

    def populateTagTable(self, rows: List[Dict[str, str]]) -> None:
        self.tagTable.setRowCount(0)

        with DBContext() as session:
            for row in rows:
                data = [
                    row["SchoolName"].strip(),
                    row["VehicleNumber"].strip(),
                    row["TeamName"].strip(),
                    row["TagUID"].strip(),
                ]
                self.tagTable.addRow(data)
                vehicle = (
                    session.query(Vehicle)
                    .filter(Vehicle.tag_uid == row["TagUID"].strip())
                    .first()
                )
                if not vehicle:
                    vehicle = Vehicle(
                        school_name=row["SchoolName"].strip(),
                        number=row["VehicleNumber"].strip(),
                        team_name=row["TeamName"].strip(),
                        tag_uid=row["TagUID"].strip(),
                    )
                    session.add(vehicle)
            session.commit()

    def connectSignals(self):
        # File menu
        # self.actionNew.triggered.connect()
        # self.actionOpen.triggered.connect()
        # self.actionSave.triggered.connect()
        self.actionImportTags.triggered.connect(self.onActionImportTagsClicked)
        # self.actionExport.triggered.connect()
        self.actionExit.triggered.connect(self.closeEvent)
        self.actionExit.setShortcut("Ctrl+Q")

        # Setup Menu
        self.actionReaderConfig.triggered.connect(self.onActionReaderConfig)

        # Tag Table
        self.tagTable.itemSelectionChanged.connect(self.onTagTableItemClicked)

    def onTagTableItemClicked(self) -> None:
        item = self.tagTable.selectedItems()
        if not item:
            return
        tag_uid = item[-1].text()

        with DBContext() as session:
            vehicle = (
                session.query(Vehicle).filter(Vehicle.tag_uid == tag_uid).first()
            )  # type: Vehicle
            if not vehicle:
                return

            self.lapTable.setRowCount(0)
            for crossing in vehicle.crossings:
                index = vehicle.crossings.index(crossing)
                if index == 0:
                    self.lapTable.addRow([index, str(timedelta(seconds=0))])
                    continue
                lap_time = vehicle.lap_time_seconds(crossing)
                self.lapTable.addRow([index, str(timedelta(seconds=lap_time))])


if __name__ == "__main__":
    create_db_and_tables()
    app = QtWidgets.QApplication([])
    window = MainWindow()
    window.show()
    app.exec()
