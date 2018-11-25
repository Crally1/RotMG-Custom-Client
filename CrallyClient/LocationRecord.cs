using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient
{
    class LocationRecord
    {
        int time;
        float x;
        float y;

        public LocationRecord(int time, float x, float y)
        {
            this.time = time;
            this.x = x;
            this.y = y;
        }
    }
}
