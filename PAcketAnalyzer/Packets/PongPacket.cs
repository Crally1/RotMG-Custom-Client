using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketAnalyzer.Packets
{
    class PongPacket : Packet
    {
        int serial;
        int time;

        public PongPacket(byte[] packet) : base(packet)
        {
            serial = ReadInt();
            time = ReadInt();
        }

        public void Print()
        {
            Header("PONG");

            Console.WriteLine("Serial: " + serial);
            Console.WriteLine("Time: " + time);
        }
    }
}
