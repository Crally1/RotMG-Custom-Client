using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient.Packets.Client
{
    class UpdateAckPacket : ClientPacket
    {
        public UpdateAckPacket() : base()
        {
            // No fields
        }

        public override byte[] build()
        {
            byte[] packet = base.build();
            packet[4] = (byte)ID.UpdateAckPacket;
            return packet;
        }
    }
}
