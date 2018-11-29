using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient
{
    static class Log
    {
        public static void Info(string info)
        {
            Console.WriteLine(DateTime.Now.ToString("[HH:mm:ss] ") + info);
        }

        public static void Error(string err)
        {
            ConsoleColor old = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;

            Info(err);

            Console.ForegroundColor = old;
        }
    }
}
