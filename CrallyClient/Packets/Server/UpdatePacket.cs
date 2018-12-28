using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient.Packets.Server
{
    class UpdatePacket : ServerPacket
    {
        //Tile[] tiles;

        public UpdatePacket(byte[] packet) : base(packet)
        {
            short tilesLength = ReadShort();
            //tiles = new Tile[tilesLength];

            //Console.WriteLine("Tile Count: " + tilesLength);
        }
    }
}
