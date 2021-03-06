﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient.Packets.Client
{
    class LoadPacket : ClientPacket
    {
        public LoadPacket(int charID, bool fromArena) : base()
        {
            writeInt(charID);
            writeBool(fromArena);
        }

        public override byte[] build()
        {
            byte[] packet = base.build();
            packet[4] = (byte)ID.LoadPacket;
            return packet;
        }
    }
}
