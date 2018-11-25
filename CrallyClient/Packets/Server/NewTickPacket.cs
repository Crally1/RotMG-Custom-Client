using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient.Packets.Server
{
    class NewTickPacket : ServerPacket
    {
        public int tickID;
        public int tickTime;
        public Status[] statuses;
        
        static bool isUTF(byte id)
        {
            switch (id)
            {
                case 31:
                case 62:
                case 82:
                case 38:
                case 54:
                    return true;

                default:
                    return false;
            }
        }

        public NewTickPacket(byte[] packet) : base(packet)
        {
            tickID = readInt();
            tickTime = readInt();

            short statusLength = readShort();
            statuses = new Status[statusLength];
            for (int i = 0; i < statusLength; ++i)
            {
                int objectID = readInt();
                Location pos = new Location(readFloat(), readFloat());

                short statLength = readShort();
                StatData[] data = new StatData[statLength];
                for (int j = 0; j < statLength; ++j)
                {
                    byte id = readByte();
                    data[j] = isUTF(id) ? new StatData(id, readString()) : new StatData(id, readInt());
                }

                statuses[i] = new Status(objectID, pos, data);
            }
        }
    }
}
