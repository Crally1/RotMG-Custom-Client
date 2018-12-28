using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient
{
    class Timer
    {
        private static bool initialized = false;
        private static long start = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 11380;

        public static void init()
        {
            if (!initialized)
            {
                start = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                initialized = true;
            }
        }

        public static int getTime()
        {
            return (int)(DateTimeOffset.Now.ToUnixTimeMilliseconds() - start);
        }
    }
}
