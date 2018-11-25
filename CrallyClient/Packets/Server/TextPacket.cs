using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient.Packets.Server
{
    class TextPacket : ServerPacket
    {
        string name;
        int stars;
        string recipient;
        string text;
        string cleanText;

        public TextPacket(byte[] packet) : base(packet)
        {
            name = readString();
            readInt();  // ObjectID
            stars = readInt();
            readByte();  // Bubble Time
            recipient = readString();
            text = readString();
            cleanText = readString();
            readBool(); // Is Supported
        }

        public void print()
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
