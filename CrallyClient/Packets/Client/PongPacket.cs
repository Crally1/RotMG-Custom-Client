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

        public int serial { get; }
        public int time   { get; }

        public PongPacket(int serial, int time) : base()
        {
            this.serial = serial;
            this.time = time;

            writeInt(serial);
            writeInt(time);
        }

        public override byte[] build()
        {
            byte[] packet = base.build();
            packet[4] = ID;
            return packet;
        }
    }
}
