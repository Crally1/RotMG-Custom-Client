using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketAnalyzer
{
    class LocationRecord
    {
        public int Time { get; }
        public float X { get; }
        public float Y { get; }

        public LocationRecord(int time, float x, float y)
        {
            this.Time = time;
            this.X = x;
            this.Y = y;
        }
    }
}
