import abc
import socket
import serial
from .command import COMMAND, RESPONSE


class BaseTransport:

    __metaclass__ = abc.ABCMeta
    read_bytecount = 0x100

    def __init__(self):
        raise NotImplementedError

    @abc.abstractmethod
    def read_bytes(self, length: int) -> bytearray:
        raise NotImplementedError

    @abc.abstractmethod
    def write_bytes(self, byte_array: bytearray) -> None:
        raise NotImplementedError

    def read(self) -> bytearray:
        return self.read_bytes(self.read_bytecount)

    @abc.abstractmethod
    def read_frame(self) -> bytearray:
        raise NotImplementedError

    def write(self, byte_array: bytearray) -> None:
        self.write_bytes(byte_array)


class SerialTransport(BaseTransport):

    def __init__(self, device: str, baud_rate=57600, timeout=5) -> None:
        """Create a serial communication link to device.

        Args:
            device (str): Serial port of device.
            baud_rate (int, optional): Communication speed. Defaults to 57600.
            timeout (int, optional): Communication timeout in seconds. Defaults to 5.
        """
        self.device = device
        self.baud_rate = baud_rate
        self.timeout = timeout
        self.serial = serial.Serial()
    
    def __enter__(self) -> serial.Serial:
        self.open()
        return self.serial

    def __exit__(self) -> None:
        self.close()

    def read_bytes(self, length: int = 1) -> bytearray:
        return bytearray(self.serial.read(length))
    
    def read_frame(self) -> bytearray:
        first_char = self.read_bytes(1)[0]

        if first_char != RESPONSE:
            length_bytes = self.read_bytes(6)
        else:
            length_bytes = bytearray([first_char]) + self.read_bytes(5)
        
        frame_length = length_bytes[-1]
        data = length_bytes + self.read_bytes(frame_length)
        return data

    def write_bytes(self, byte_array) -> None:
        self.serial.write(byte_array)
    
    def open(self) -> None:
        self.serial.port = self.device
        self.serial.baudrate = self.baud_rate
        self.serial.timeout = self.timeout
        self.serial.open()

    def close(self) -> None:
        self.serial.close()


class TcpTransport(BaseTransport):

    buffer_size = 0xFF

    def __init__(self, reader_addr: str, reader_port: int, timeout=5, auto_connect=False):
        self.socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.socket.settimeout(timeout)
        self.reader_addr = reader_addr
        self.reader_port = reader_port
        if auto_connect:
            self.connect()

    def connect(self) -> None:
        self.socket.connect((self.reader_addr, self.reader_port))

    def read_bytes(self, length) -> bytearray:
        return bytearray(self.socket.recv(length))

    def write_bytes(self, byte_array: bytearray) -> None:
        self.socket.sendall(byte_array)

    def close(self) -> None:
        self.socket.close()


class MockTransport(BaseTransport):

    def __init__(self, data):
        self.pointer = 0
        self.data = bytes(data)

    def read_bytes(self, length: int):
        data = self.data[self.pointer:self.pointer+length]
        self.pointer += length
        return data

    def write_bytes(self, byte_array: bytearray) -> None:
        pass

    def close(self) -> None:
        pass