using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketAnalyzer
{
    static class Info
    {
        public static byte[] outKey = Util.HexStringToByteArray("6a39570cc9de4ec71d64821894");
        public static byte[] inKey = Util.HexStringToByteArray("c79332b197f92ba85ed281a023");
    }
}
