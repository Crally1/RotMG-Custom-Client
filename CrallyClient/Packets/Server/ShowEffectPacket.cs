using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient.Packets.Server
{
    class ShowEffectPacket : ServerPacket
    {
        int EffectType { get; }
        int TargetObjectID { get; }
        Location pos1 { get; }
        Location pos2 { get; }
        int color { get; }
        float duration { get; }

        public ShowEffectPacket(byte[] packet) : base(packet)
        {
            EffectType = ReadInt();
            TargetObjectID = ReadByte();

            //Console.WriteLine("Type: " + effectType);
            //Console.WriteLine("Target Object ID: " + targetObjectID);
        }
    }
}
