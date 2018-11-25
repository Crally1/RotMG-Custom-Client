using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient.Packets.Client
{
    class LoadPacket : ClientPacket
    {
        const byte ID = 62;

        public LoadPacket(int charID, bool fromArena) : base()
        {
            writeInt(charID);
            writeBool(fromArena);
        }

        public new byte[] build()
        {
            byte[] packet = base.build();
            packet[4] = ID;
            return packet;
        }
    }
}
