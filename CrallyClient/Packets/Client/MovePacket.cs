using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient.Packets.Client
{
    class MovePacket : ClientPacket
    {
        public MovePacket(int id, int time, Location pos, LocationRecord[] records) : base()
        {
            writeInt(id);
            writeInt(time);
            writeFloat(pos.x);
            writeFloat(pos.y);
            writeShort((short)records.Length);
            
            for (int i = 0; i < records.Length; ++i)
            {
                writeInt(records[i].Time);
                writeFloat(records[i].X);
                writeFloat(records[i].Y);
            }
        }

        public override byte[] build()
        {
            byte[] packet = base.build();
            packet[4] = (byte)ID.MovePacket;
            return packet;
        }
    }
}
