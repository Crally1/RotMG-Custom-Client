using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient
{
    static class Info
    {
        public static string secret = "";
        public static string buildVersion = "X31.1.2";
        public static string rsaPubKey = "<RSAKeyValue><Modulus>wihXLVa4Xxd5tinrNBQS/yRXjnJjYPXic/JP4UMtZGnQ/lxVKZbkamv/1MadrPVtqhNBKFArufboHH/6e5Rri/fVKEhpkBMp5Ab2XLfvzONC9BNK2eAVYMyziZiG1zKdCeD5z3G+lS2b8WsxnJkn42rguxd8lmKlG2XfxwgbWsM=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        public static byte[] outKey = Util.HexStringToByteArray("6a39570cc9de4ec71d64821894");
        public static byte[] inKey = Util.HexStringToByteArray("c79332b197f92ba85ed281a023");
    }
}
