using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient.Packets.Server
{
    class MapInfoPacket : ServerPacket
    {
        int width;
        int height;

        string name;
        string displayName;

        int fp;
        int background;
        int difficulty;

        bool allowPlayerTeleport;
        bool showDisplays;

        string clientXML;
        string extraXML;

        public MapInfoPacket(byte[] data) : base(data)
        {
            width = readInt();
            height = readInt();

            name = readString();
            displayName = readString();

            fp = readInt();
            background = readInt();
            difficulty = readInt();

            allowPlayerTeleport = readBool();
            showDisplays = readBool();

            clientXML = readString();
            extraXML = readString();
        }

        public override string ToString()
        {
            return $"[height:{height}:width:{width}:name:{name}:displayName:{displayName}]";
        }
    }
}
