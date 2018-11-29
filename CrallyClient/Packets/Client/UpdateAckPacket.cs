using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient.Packets.Client
{
    class UpdateAckPacket : ClientPacket
    {
        const int ID = 80;

        public UpdateAckPacket() : base()
        {
            // No fields
        }

        public override byte[] build()
        {
            byte[] packet = base.build();
            packet[4] = ID;
            return packet;
        }
    }
}
