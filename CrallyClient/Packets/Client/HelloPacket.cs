using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CrallyClient.Packets.Client
{
    class HelloPacket : ClientPacket
    {
        const byte ID = 100;

        public HelloPacket(string guid, string passwd) : base()
        {
            Random random = new Random();

            // Generate two random numbers between 0 and a billion
            int random1 = (int)Math.Floor(random.NextDouble() * 1000000000);
            int random2 = (int)Math.Floor(random.NextDouble() * 1000000000);

            // Encrypt and Base64 encode GUID and PASSWD
            string GUID = System.Convert.ToBase64String(Crypto.RSA.RSAEncrypt(Encoding.UTF8.GetBytes(guid), Info.rsaPubKey));
            string PASSWD = System.Convert.ToBase64String(Crypto.RSA.RSAEncrypt(Encoding.UTF8.GetBytes(passwd), Info.rsaPubKey));

            writeString(Info.buildVersion); // Build Version
            writeInt(-2);                   // Game ID
            writeString(GUID);              // GUID
            writeInt(random1);              // Random1
            writeString(PASSWD);            // Password
            writeInt(random2);              // Random2
            writeString(Info.secret);       // Secret
            writeInt(-1);                   // Key Time
            writeBytes(new byte[0]);        // Key and Key Length
            writeBytes(new byte[0], true);  // Map JSON and Map JSON Length
            writeString("");                // Entry Tag
            writeString("rotmg");           // Game Net
            writeString("");                // Game Net User ID
            writeString("rotmg");           // Play Platform
            writeString("");                // Platform Token
            writeString("");                // User Token
        }

        public override byte[] build()
        {
            byte[] packet = base.build();
            packet[4] = ID;
            return packet;
        }
    }
}
