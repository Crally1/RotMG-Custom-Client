using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient.Packets.Server
{
    class TextPacket : ServerPacket
    {
        string name      { get; }
        int    stars     { get; }
        string recipient { get; }
        string text      { get; }
        string cleanText { get; }

        public TextPacket(byte[] packet) : base(packet)
        {
            name = ReadString();
            ReadInt();   // ObjectID
            stars = ReadInt();
            ReadByte();  // Bubble Time
            recipient = ReadString();
            text = ReadString();
            cleanText = ReadString();
            ReadBool();  // Is Supported
        }

        public void Print()
        {
            var old = Console.ForegroundColor;

            if (name.Length != 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"<{name}> ");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }

            Console.WriteLine($"{text}");
            Console.ForegroundColor = old;
        }
    }
}
