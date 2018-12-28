using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient.Packets.Server
{
    class GoToPacket : ServerPacket
    {
        public int ObjectID { get; }
        public Location Pos { get; }

        public GoToPacket(byte[] packet) : base(packet)
        {
            ObjectID = ReadInt();
            Pos = new Location(ReadFloat(), ReadFloat());
        }
    }
}
