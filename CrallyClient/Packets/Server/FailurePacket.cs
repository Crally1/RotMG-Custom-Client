using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient.Packets.Server
{
    class FailurePacket : ServerPacket
    {
        public int Code { get; }
        public string Text { get; }

        public FailurePacket(byte[] packet) : base(packet)
        {
            Code = ReadInt();
            Text = ReadString();
        }

        public override string ToString()
        {
            return $"Error [ID:{Code}]: {Text}";
        }
    }
}
