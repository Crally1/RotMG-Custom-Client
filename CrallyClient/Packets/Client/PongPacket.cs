using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient.Packets.Client
{
    class PongPacket : ClientPacket
    {
        public int Serial { get; }
        public int Time   { get; }

        public PongPacket(int serial, int time) : base()
        {
            this.Serial = serial;
            this.Time = time;

            writeInt(serial);
            writeInt(time);
        }

        public override byte[] build()
        {
            byte[] packet = base.build();
            packet[4] = (byte)ID.PongPacket;
            return packet;
        }
    }
}
