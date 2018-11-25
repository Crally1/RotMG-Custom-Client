using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient
{
    class Status
    {
        public int objectID;
        public Location pos;
        public StatData[] data;

        public Status(int objectID, Location pos, StatData[] data)
        {
            this.objectID = objectID;
            this.pos = pos;
            this.data = data;
        }
    }
}
