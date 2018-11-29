using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient.Packets.Client
{
    class MovePacket : ClientPacket
    {
        const int ID = 74;

        public MovePacket(int id, int time, Location pos) : base()
        {
            writeInt(id);
            writeInt(time);
            writeFloat(pos.x);
            writeFloat(pos.y);
            writeShort(0);
            //writeInt(time);
            //writeFloat(pos.x);
            //writeFloat(pos.y);
        }

        public override byte[] build()
        {
            byte[] packet = base.build();
            packet[4] = ID;
            return packet;
        }
    }
}
