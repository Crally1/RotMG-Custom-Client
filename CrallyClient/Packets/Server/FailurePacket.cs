using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient.Packets.Server
{
    class FailurePacket : ServerPacket
    {
        public int ID { get; }
        public string msg { get; }

        public FailurePacket(byte[] packet) : base(packet)
        {
            ID = readInt();
            msg = readString();
        }

        public override string ToString()
        {
            return $"Error [ID:{ID}]: {msg}";
        }
    }
}
