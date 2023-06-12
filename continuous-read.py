from autolaptime.yanzeo_rfid.base import (
    CommandSender,
    ReaderCommand,
    ReaderInfoFrame,
    ReaderResponseFrame,
    Tag,
)
from autolaptime.yanzeo_rfid.transport import BaseTransport, SerialTransport
from autolaptime.yanzeo_rfid.base import G2TagResponseFrame


responses = []


def run_command(transport: BaseTransport, command: ReaderCommand) -> int:
    transport.write(command.serialize())
    status = ReaderResponseFrame(transport.read_frame()).result_status
    return status


def read_tags(reader_addr: str) -> None:
    transport = SerialTransport(device=reader_addr, timeout=None)
    runner = CommandSender(transport)

    verbose = False
    running = True
    with transport as ser:
        while running:
            try:
                response = G2TagResponseFrame(transport.read_frame())
                responses.append(response)
            except KeyboardInterrupt:
                running = False
            except Exception as err:
                running = False
                raise err


if __name__ == "__main__":
    from threading import Thread

    serial_comms_thread = Thread(target=read_tags, args=("COM5",))
    serial_comms_thread.start()

    running = True
    print("Start")
    while running:
        for response in responses[:]:
            tag = response.tag
            print(tag.uid)
            responses.remove(response)

    serial_comms_thread.join()
