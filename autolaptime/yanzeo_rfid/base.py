from enum import Enum
from typing import List
from dataclasses import dataclass
from .checksum import checksum
from .transport import BaseTransport
from .command import Command, COMMAND, RESPONSE


class ReaderCommand:
    """Generates a command to send to the device."""

    def __init__(self, command: Command, address=0xFF, data: List = None) -> None:
        self.address = address
        self.command = command
        self.data = data
        if data == None:
            self.data = []

    def serialize(self) -> bytearray:
        frame_length = len(self.data)
        base_data = bytearray([ COMMAND, self.address, self.address, self.command.code, self.command.type_, frame_length ])
        base_data.extend(bytearray(self.data))
        base_data.extend(bytearray(self.checksum_bytes(base_data)))
        return base_data

    def checksum_bytes(self, data_bytes: bytearray) -> bytearray:
        crc = checksum(data_bytes)
        crc_msb = crc >> 8
        crc_lsb = crc & 0xFF
        return bytearray([ 0x00 ])


class CommandSender:
    """Sends a command to the device."""

    def __init__(self, transport: BaseTransport) -> None:
        self.transport = transport

    def run(self, command: ReaderCommand) -> bytearray:
        self.transport.write(command.serialize())
        return self.transport.read_frame()


class ReaderResponseFrame:

    def __init__(self, resp_bytes: bytearray, offset=0) -> None:
        self.resp_bytes = resp_bytes
        if len(resp_bytes) < 5:
            raise ValueError('Response must be at least 6 bytes')
        self.data_length = resp_bytes[offset+5]

        if self.data_length + offset > len(resp_bytes) - 1:
            raise ValueError('Response does not contain enough bytes for frame (expected %d bytes after offset %d, actual length %d)' % (self.data_length, offset, len(resp_bytes)))

        self.address = resp_bytes[offset+1:offset+2]
        self.resp_command = resp_bytes[offset+3]
        self.return_code = ReturnCode(resp_bytes[offset+4])
        
        self.data = resp_bytes[offset+6:len(resp_bytes)]
        # cs_status = self.verify_checksum(resp_bytes[offset:offset+self.len-1], resp_bytes[offset+self.len-1:offset+self.len+1])
        # if cs_status is not True:
        #     raise(ValueError('Checksum does not match'))

    def verify_checksum(self, data_bytes: bytearray, checksum_bytes) -> bool:
        data_crc = checksum(bytearray(data_bytes))
        crc_msb = data_crc >> 8
        crc_lsb = data_crc & 0xFF
        return checksum_bytes[0] == crc_lsb and checksum_bytes[1] == crc_msb

    def __len__(self):
        return self.data_length

    def get_data(self) -> bytearray:
        return self.data


class ReaderMode(Enum):
    Command = 0x01
    Active = 0x02
    Passive = 0x03


class ReaderComunicationMode(Enum):
    RS232 = 0x01
    RS485 = 0x02
    TCPIP = 0x03
    CANBUS = 0x04
    SYRIS = 0x05
    WG26 = 0x06
    WG34 = 0x07


class ReaderReadType(Enum):
    ISO_6B_Single = 0x01
    G2_Single = 0x10
    G2_And_ISO_6B = 0x11
    G2_Multi = 0x20
    G2_And_Mem_Bank = 0x40


class ReturnCode(Enum):
    Success = 0x00
    Fail = 0x01
    Auto_Send = 0x05


class ReaderInfoFrame(ReaderResponseFrame):

    def __init__(self, resp_bytes: bytearray) -> None:
        super(ReaderInfoFrame, self).__init__(resp_bytes)

        if len(self.data) >= 25:
            self.type = self.data[8:10] # MSB -> LSB
            self.firmware_version = int.from_bytes(self.data[11:15], byteorder="big") # MSB -> LSB
            self.reader_address = int.from_bytes(self.data[16:25], byteorder="big") # MSB -> LSB
        else:
            raise ValueError('Data must be at least 25 characters')


class ReaderParameterFrame(ReaderResponseFrame):

    def __init__(self, resp_bytes: bytearray) -> None:
        super(ReaderInfoFrame, self).__init__(resp_bytes)

        if len(self.data) >= 28:
            self.transmit_power = self.data[0]
            self.frequency_hopping_enabled = self.data[1] == 0x01
            # self.frequency_hopping_value
            self.mode = ReaderMode(self.data[9])
            self.read_interval = self.data[10] # ms
            self.trigger_enabled = self.data[11] == 0x01
            self.communication_mode = ReaderComunicationMode(self.data[12])
            self.antana_number = self.data[17]
            self.read_type = ReaderReadType(self.data[18])
            self.buzzer_enabled = self.data[20] == 0x01
        else:
            raise ValueError('Data must be at least 28 characters')


class G2TagResponseFrame(ReaderResponseFrame):

    def __init__(self, resp_bytes: bytearray) -> None:
        super().__init__(resp_bytes)

        if len(self.data) >= 16:
            self.antana_number = self.data[0]
            self.pc = int.from_bytes(self.data[1:2], byteorder="big")
            self.rssi = self.data[15]
            self.tag = Tag(pc=self.pc, uid=self.data[3:15].hex().upper(), antenna_num=self.antana_number, rssi=self.rssi)
        else:
            raise ValueError('Data must be at least 16 characters')


@dataclass
class Tag:
    uid: str
    pc: int = 0
    antenna_num: int = 0
    rssi: int = None