using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient.Packets.Server
{
    class MapInfoPacket : ServerPacket
    {
        public int Width  { get; }
        public int Height { get; }

        public string Name        { get; }
        public string DisplayName { get; }

        public int Fp         { get; }
        public int Background { get; }
        public int Difficulty { get; }

        public bool AllowPlayerTeleport { get; }
        public bool ShowDisplays        { get; }

        public string ClientXML { get; }
        public string ExtraXML  { get; }

        public MapInfoPacket(byte[] data) : base(data)
        {
            Width = ReadInt();
            Height = ReadInt();

            Name = ReadString();
            DisplayName = ReadString();

            Fp = ReadInt();
            Background = ReadInt();
            Difficulty = ReadInt();

            AllowPlayerTeleport = ReadBool();
            ShowDisplays = ReadBool();

            ClientXML = ReadString();
            ExtraXML = ReadString();
        }

        public override string ToString()
        {
            return $"[height:{Height}:width:{Width}:name:{Name}:displayName:{DisplayName}]";
        }
    }
}
