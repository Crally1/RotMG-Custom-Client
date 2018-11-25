using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient.Packets.Server
{
    class ErrorPacket : ServerPacket
    {
        int errorID;
        string msg;

        public ErrorPacket(byte[] packet) : base(packet)
        {
            errorID = readInt();
            msg = readString();
        }

        public string get()
        {
            return $"Error [ID:{errorID}]: {msg}";
        }
    }
}
