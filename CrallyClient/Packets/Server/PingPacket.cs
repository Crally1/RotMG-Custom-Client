using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient.Packets.Server
{
    class PingPacket : ServerPacket
    {
        public int serial;

        public PingPacket(byte[] packet) : base(packet)
        {
            serial = readInt();
        }
    }
}
