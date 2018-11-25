using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient
{
    class StatData
    {
        int id;
        int value;
        public string str;

        public StatData(int id, int value)
        {
            this.id = id;
            this.value = value;
        }

        public StatData(int id, string value)
        {
            this.id = id;
            this.str = value;
        }
    }
}
