using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient.Packets.Server
{
    class ShowEffectPacket : ServerPacket
    {
        int effectType;
        int targetObjectID;
        Location pos1;
        Location pos2;
        int color;
        float duration;

        public ShowEffectPacket(byte[] packet) : base(packet)
        {
            effectType = readInt();
            targetObjectID = readByte();

            //Console.WriteLine("Type: " + effectType);
            //Console.WriteLine("Target Object ID: " + targetObjectID);
        }
    }
}
