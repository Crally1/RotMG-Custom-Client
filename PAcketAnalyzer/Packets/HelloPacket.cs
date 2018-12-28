using PacketAnalyzer.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketAnalyzer.Packets
{
    class HelloPacket : Packet
    {
        string buildVersion;
        int gameID;
        string guid;
        int random1;
        string passwd;
        int random2;
        string secret;
        int keyTime;
        byte[] key;
        byte[] mapJson;
        string entryTag;
        string gameNet;
        string gameNetUserId;
        string playPlatform;
        string platformToken;
        string userToken;

        public HelloPacket(byte[] packet) : base(packet)
        {
            buildVersion = ReadString();
            gameID = ReadInt();
            guid = ReadString();
            random1 = ReadInt();
            passwd = ReadString();
            random2 = ReadInt();
            secret = ReadString();
            keyTime = ReadInt();
            key = ReadBytes();
            mapJson = ReadBytes(true);
            entryTag = ReadString();
            gameNet = ReadString();
            gameNetUserId = ReadString();
            playPlatform = ReadString();
            platformToken = ReadString();
            userToken = ReadString();
        }

        public void Print()
        {
            Header("HELLO");

            Console.WriteLine("Build Version: " + buildVersion);
            Console.WriteLine("Game ID: " + gameID);
            Console.WriteLine("GUID: " + guid);
            Console.WriteLine("Random1: " + random1);
            Console.WriteLine("Password: " + passwd);
            Console.WriteLine("Radnom2: " + random2);
            Console.WriteLine("Secret: " + secret);
            Console.WriteLine("Key Time: " + keyTime);
            Console.WriteLine("Key: " + key);
            Console.WriteLine("Map JSON: " + mapJson);
            Console.WriteLine("Entry Tag: " + entryTag);
            Console.WriteLine("Game Net: " + gameNet);
            Console.WriteLine("Game Net User ID: " + gameNetUserId);
            Console.WriteLine("Play Platform: " + playPlatform);
            Console.WriteLine("Platform Token: " + platformToken);
            Console.WriteLine("User Token: " + userToken);
        }
    }
}
