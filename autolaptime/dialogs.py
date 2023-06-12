import serial
import sys
import glob
from typing import List, Optional
from PyQt5 import QtCore, QtGui, QtWidgets



class ReaderSettingsDialog(QtWidgets.QDialog):
    def __init__(self, parent: QtWidgets.QWidget = None):
        super().__init__(parent)

        self.resize(400, 200)

        self.setWindowTitle("Reader Settings")
        self.setLayout(QtWidgets.QVBoxLayout())
        # self.layout().addWidget(QtWidgets.QLabel("Select a battery to add to the drone."))
        self.form_layout = QtWidgets.QFormLayout()

        self.portComboBox = QtWidgets.QComboBox()
        self.baudRateCombobox = QtWidgets.QComboBox()
        self.timeoutSpinBox = QtWidgets.QSpinBox()
        self.timeoutSpinBox.setSuffix(" sec")
        self.timeoutSpinBox.setMinimum(0)
        self.timeoutSpinBox.setMaximum(100)
        self.timeoutSpinBox.setSingleStep(1)
        self.saveButton = QtWidgets.QPushButton("Save")

        self.portComboBoxItems = [x for x in self.getSerialPorts()]
        self.portComboBox.addItems(self.portComboBoxItems)

        self.baudRateComboboxItems = [str(x) for x in serial.Serial.BAUDRATES]
        self.baudRateCombobox.addItems(self.baudRateComboboxItems)
        self.baudRateCombobox.setCurrentText("57600")

        self.form_layout.addRow(QtWidgets.QLabel("Port:"), self.portComboBox)
        self.form_layout.addRow(QtWidgets.QLabel("Baud Rate:"), self.baudRateCombobox)
        self.form_layout.addRow(QtWidgets.QLabel("Timeout:"), self.timeoutSpinBox)
        self.layout().addLayout(self.form_layout)

        button_layout = QtWidgets.QHBoxLayout()
        self.saveButton.clicked.connect(self.onSaveButtonClicked)
        self.saveButton.clicked.connect(self.close)
        button_layout.addWidget(self.saveButton)
        self.layout().addLayout(button_layout)

    def onSaveButtonClicked(self) -> None:
        pass

    @staticmethod
    def getSerialPorts() -> List[Optional[str]]:
        """Lists all serial port names.

        Raises:
            EnvironmentError: On unsupported or unknown platforms.

        Returns:
            List[Optional[str]]: A list of the serial ports available on the system.
        """
        if sys.platform.startswith('win'):
            ports = ['COM%s' % (i + 1) for i in range(256)]
        elif sys.platform.startswith('linux') or sys.platform.startswith('cygwin'):
            # this excludes your current terminal "/dev/tty"
            ports = glob.glob('/dev/tty[A-Za-z]*')
        elif sys.platform.startswith('darwin'):
            ports = glob.glob('/dev/tty.*')
        else:
            raise EnvironmentError('Unsupported platform')

        result = []
        for port in ports:
            try:
                s = serial.Serial(port)
                s.close()
                result.append(port)
            except (OSError, serial.SerialException):
                pass
        return result