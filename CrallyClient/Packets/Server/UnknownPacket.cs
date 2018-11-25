using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient.Packets.Server
{
    class UnknownPacket : ServerPacket
    {
        public UnknownPacket(byte[] packet) : base(packet)
        {

        }
    }
}
