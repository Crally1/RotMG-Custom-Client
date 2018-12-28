using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketAnalyzer.Packets
{
    class UpdateAckPacket : Packet
    {
        public UpdateAckPacket(byte[] packet) : base(packet)
        {

        }

        public void Print()
        {
            Header("UPDATEACK");
            Console.WriteLine("No fields");
        }
    }
}
