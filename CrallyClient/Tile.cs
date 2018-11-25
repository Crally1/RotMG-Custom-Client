using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient
{
    class Tile
    {
        short x;
        short y;
        int type;

        Tile(short x, short y, int type)
        {
            this.x = x;
            this.y = y;
            this.type = type;
        }
    }
}
