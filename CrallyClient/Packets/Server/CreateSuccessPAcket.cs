﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient.Packets.Server
{
    class CreateSuccessPacket : ServerPacket
    {
        public int ObjectID { get; }
        public int CharID { get; }

        public CreateSuccessPacket(byte[] packet) : base(packet)
        {
            ObjectID = ReadInt();
            CharID = ReadInt();
        }
    }
}
