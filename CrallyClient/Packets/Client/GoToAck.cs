using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient.Packets.Client
{
    class GoToAckPacket : ClientPacket
    {
        public int Time { get; }

        public GoToAckPacket(int time) : base()
        {
            Time = time;

            writeInt(Time);
        }

        public override byte[] build()
        {
            byte[] packet = base.build();
            packet[4] = (byte)ID.GoToAckPacket;
            return packet;
        }
    }
}
