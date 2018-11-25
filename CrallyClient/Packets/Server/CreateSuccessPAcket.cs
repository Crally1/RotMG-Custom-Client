using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient.Packets.Server
{
    class CreateSuccessPacket : ServerPacket
    {
        public int objectID;
        public int charID;

        public CreateSuccessPacket(byte[] packet) : base(packet)
        {
            objectID = readInt();
            charID = readInt();

            //Console.WriteLine("Object ID: " + objectID);
            //Console.WriteLine("Character ID: " + charID);
        }
    }
}
