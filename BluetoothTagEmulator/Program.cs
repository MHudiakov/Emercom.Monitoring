using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BluetoothTagEmulator
{
    class Program
    {
        private static ComPortManager _cpm;
        private static ReaderManager _rm;

        static void Main(string[] args)
        {
            Globals.DisplayData(Globals.MessageType.Normal, "Для остановки приложения введите exit");

            _cpm = new ComPortManager(new ComPortSettings
            {
                BaudRate = 9600,
                Parity = Parity.None,
                StopBits = StopBits.One,
                PortName = "COM3",
                DataBits = 8
            });
            _cpm.NewMessageRecived += InputMessageFromComPort;
            _cpm.OpenPort();

            while (true)
            {
                Console.Write(@"> ");
                var consoleInput = Console.ReadLine();
                if (consoleInput == "exit")
                    return;

                _cpm.WriteData(consoleInput);
            }
        }

        private static void InputMessageFromComPort(List<byte> msg)
        {
            try
            {
                var msgCode = msg[1];

                if (msgCode == (int)Globals.ComReciveActions.InitReadersCount)
                {
                    _rm?.ReRandomListThread.Abort();

                    _rm = new ReaderManager();
                    Globals.DisplayData(Globals.MessageType.Normal, "Инициализация эмулятора прошла успешно.");
                }

                var readers = ReaderManager.Readers;

                if (readers == null)
                {
                    Globals.DisplayData(Globals.MessageType.Error, "Ахтунг! Эмулятор не инициализирован!");
                    return;
                }

                lock (readers)
                {
                    var isBroadcastMessage = msg[0] == 0;

                    switch (msgCode)
                    {
                        case (int)Globals.ComReciveActions.InitReadersCount:
                            _cpm.WriteData("0000" + readers.Count.IntToHex());
                            break;

                        case (int)Globals.ComReciveActions.TagsCountOnReader:
                            if (isBroadcastMessage)
                                foreach (var reader in readers)
                                {
                                    _cpm.WriteData("0001" + reader.Tags.Count.IntToHex());
                                }
                            else
                                _cpm.WriteData(msg[0].ByteToHex() + "00" + readers[msg[0] - 1].Tags.Count.IntToHex());
                            break;

                        case (int)Globals.ComReciveActions.CurrentTagOnReader:
                            if (isBroadcastMessage)
                                foreach (var reader in readers)
                                {
                                    foreach (var tag in reader.Tags)
                                    {
                                        _cpm.WriteData("0002" + reader.Id.IntToHex() + tag.Id.IntToHex() + tag.Mac);
                                    }
                                }
                            else
                                foreach (var tag in readers[msg[0] - 1].Tags)
                                {
                                    _cpm.WriteData(msg[0].ByteToHex() + "02" + tag.Id.IntToHex() + tag.Mac);
                                }
                            break;

                        case (int)Globals.ComReciveActions.ReadersCountWithCurrentTag:
                            var mac = ByteToHex(msg.Skip(2).ToArray());
                            if (mac.Length != 12)
                            {
                                throw new ArgumentException("В запросе указан некорректный MAC Адрес");
                            }
                            _cpm.WriteData("0003" + readers.Count(x => x.Tags.Any(y => y.Mac == mac)).IntToHex());

                            break;

                        case (int)Globals.ComReciveActions.CurrentSignal:
                            mac = ByteToHex(msg.Skip(2).ToArray());
                            if (mac.Length != 12)
                            {
                                throw new ArgumentException("В запросе указан некорректный MAC Адрес");
                            }
                            foreach (var reader in readers.Where(x => x.Tags.Any(y => y.Mac == mac)))
                            {
                                _cpm.WriteData("0004" + reader.Id.IntToHex() + reader.Tags.FirstOrDefault(x => x.Mac == mac).SignalLevel.IntToHex());
                            }
                            break;

                        case (int)Globals.ComReciveActions.CurrentReadersCount:
                            _cpm.WriteData("0005" + readers.Count.IntToHex());
                            break;

                        default:
                            Globals.DisplayData(Globals.MessageType.Error, "Пришел запрос с неизвестным кодом: " + msgCode);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Globals.DisplayData(Globals.MessageType.Error, "При обработке запроса произошла ошибка: " + ex.Message);
            }
        }

        private static string ByteToHex(byte[] comByte)
        {
            StringBuilder builder = new StringBuilder(comByte.Length * 3);
            foreach (byte data in comByte)
                builder.Append(Convert.ToString(data, 16).PadLeft(2, '0'));
            return builder.ToString().ToUpper();
        }
    }
}
