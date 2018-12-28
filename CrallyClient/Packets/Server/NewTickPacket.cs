using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient.Packets.Server
{
    class NewTickPacket : ServerPacket
    {
        public int      TickID   { get; }
        public int      TickTime { get; }
        public Status[] Statuses { get; }

        static bool IsUTF(byte id)
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
            TickID = ReadInt();
            TickTime = ReadInt();

            short statusLength = ReadShort();
            Statuses = new Status[statusLength];
            for (int i = 0; i < statusLength; ++i)
            {
                int objectID = ReadInt();
                Location pos = new Location(ReadFloat(), ReadFloat());

                short statLength = ReadShort();
                StatData[] data = new StatData[statLength];
                for (int j = 0; j < statLength; ++j)
                {
                    byte id = ReadByte();
                    data[j] = IsUTF(id) ? new StatData(id, ReadString()) : new StatData(id, ReadInt());
                }

                Statuses[i] = new Status(objectID, pos, data);
            }
        }
    }
}
