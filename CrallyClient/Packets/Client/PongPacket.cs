using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient.Packets.Client
{
    class PongPacket : ClientPacket
    {
        const int ID = 86;

        public PongPacket(int serial, int time) : base()
        {
            writeInt(serial);
            writeInt(time);
        }

        public new byte[] build()
        {
            byte[] packet = base.build();
            packet[4] = ID;
            return packet;
        }
    }
}
