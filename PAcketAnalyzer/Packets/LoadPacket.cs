using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketAnalyzer.Packets
{
    class LoadPacket : Packet
    {
        int charID;
        bool isFromArena;

        public LoadPacket(byte[] packet) : base(packet)
        {
            charID = ReadInt();
            isFromArena = ReadBool();
        }

        public void Print()
        {
            Header("LOAD");

            Console.WriteLine("Char ID: " + charID);
            Console.WriteLine("Is From Arena: " + isFromArena);
        }
    }
}
