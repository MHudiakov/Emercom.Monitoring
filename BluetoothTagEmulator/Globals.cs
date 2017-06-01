using System;

namespace BluetoothTagEmulator
{
    public static class Globals
    {
        public const int ComRequRespHeader = 255;

        public enum ComReciveActions
        {
            InitReadersCount = 0,
            TagsCountOnReader = 1,
            CurrentTagOnReader = 2,
            ReadersCountWithCurrentTag = 3,
            CurrentSignal = 4,
            CurrentReadersCount = 5
        }

        public enum MessageType { Incoming, Outgoing, Normal, Warning, Error };

        private readonly static ConsoleColor[] MessageColor = { ConsoleColor.Blue, ConsoleColor.Green, ConsoleColor.White, ConsoleColor.Yellow, ConsoleColor.Red };

        [STAThread]
        public static void DisplayData(MessageType type, string msg)
        {
            Console.ForegroundColor = MessageColor[(int)type];
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static string ByteToHex(this byte b)
        {
            return b.ToString("X").PadLeft(2, '0');
        }

        public static string IntToHex(this int b)
        {
            return b.ToString("X").PadLeft(2, '0');
        }
    }
}
