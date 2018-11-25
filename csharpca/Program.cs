using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Net.Sockets;

class Program
{
    static short readShort(byte[] data, ref int it)
    {
        return (short)(data[it++] << 8 | data[it++]);
    }

    static int readInt(byte[] data, ref int it)
    {
        return (data[it++] << 24) | (data[it++] << 16) | (data[it++] << 8) | (data[it++]);
    }

    static string readString(short length, byte[] data, ref int it)
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < length; ++i)
            builder.Append((char)data[it++]);

        return builder.ToString();
    }

    static bool readBool(byte[] data, ref int it)
    {
        return data[it++] != 0;
    }

    static byte[] readBytes(short length, byte[] data, ref int it)
    {
        byte[] bytes = new byte[length];
        for (int i = 0; i < length; ++i)
            bytes[i] = data[i++];

        return bytes;
    }

    static byte[] readBytes(int length, byte[] data, ref int it)
    {
        byte[] bytes = new byte[length];
        for (int i = 0; i < length; ++i)
            bytes[i] = data[i++];

        return bytes;
    }

    static void readHelloPacket(byte[] data)
    {
        int it = 0;

        short buildVerlength = readShort(data, ref it);
        string version = readString(buildVerlength, data, ref it);

        int gameid = readInt(data, ref it);

        short guidLength = readShort(data, ref it);
        string guid = readString(guidLength, data, ref it);

        int random1 = readInt(data, ref it);

        short pwLength = readShort(data, ref it);
        string passwd = readString(pwLength, data, ref it);

        int random2 = readInt(data, ref it);

        short secretLength = readShort(data, ref it);
        string secret = readString(secretLength, data, ref it);

        int keyTime = readInt(data, ref it);
        short keyLength = readShort(data, ref it);
        byte[] key = readBytes(keyLength, data, ref it);

        int mapJSONLength = readInt(data, ref it);
        byte[] mapJSON = readBytes(mapJSONLength, data, ref it);

        short entryTagLength = readShort(data, ref it);
        string entryTag = readString(entryTagLength, data, ref it);

        short gameNetLength = readShort(data, ref it);
        string gameNet = readString(gameNetLength, data, ref it);

        short gameNetUserIDLength = readShort(data, ref it);
        string gameNetUserID = readString(gameNetUserIDLength, data, ref it);

        short playPlatformLength = readShort(data, ref it);
        string playPlatform = readString(playPlatformLength, data, ref it);

        short platformTokenLength = readShort(data, ref it);
        string platformToken = readString(platformTokenLength, data, ref it);

        short userTokenLength = readShort(data, ref it);
        string userToken = readString(userTokenLength, data, ref it);

        Console.WriteLine("----------Hello----------");
        Console.WriteLine("Version String Length: " + buildVerlength);
        Console.WriteLine("Version String: " + version);
        Console.WriteLine("Game ID: " + gameid);
        Console.WriteLine("GUID Length: " + guidLength);
        Console.WriteLine("GUID: " + guid);
        Console.WriteLine("Random1: " + random1);
        Console.WriteLine("Password Length: " + pwLength);
        Console.WriteLine("Password: " + passwd);
        Console.WriteLine("Random2: " + random2);
        Console.WriteLine("Secret Length: " + secretLength);
        Console.WriteLine("Secret: " + secret);
        Console.WriteLine("Key Time: " + keyTime);
        Console.WriteLine("Key Length: " + keyLength);
        Console.WriteLine("Key: " + BitConverter.ToString(key).Replace("-", string.Empty));
        Console.WriteLine("Map JSON Length: " + mapJSONLength);
        Console.WriteLine("Map JSON: " + BitConverter.ToString(mapJSON).Replace("-", string.Empty));
        Console.WriteLine("Entry Tag Length: " + entryTagLength);
        Console.WriteLine("Entry Tag: " + entryTag);
        Console.WriteLine("Game Net Length: " + gameNetLength);
        Console.WriteLine("Game Net: " + gameNet);
        Console.WriteLine("Game Net User ID Length: " + gameNetUserIDLength);
        Console.WriteLine("Game Net User ID: " + gameNetUserID);
        Console.WriteLine("Play Platform Length: " + playPlatformLength);
        Console.WriteLine("Play Platform: " + playPlatform);
        Console.WriteLine("Platform Token Length: " + platformTokenLength);
        Console.WriteLine("Platform Token: " + platformToken);
        Console.WriteLine("User Token Length: " + userTokenLength);
        Console.WriteLine("User Token: " + userToken);
    }

    static void readMapInfoPacket(byte[] data)
    {
        int it = 0;

        int width = readInt(data, ref it);
        int height = readInt(data, ref it);

        short nameLength = readShort(data, ref it);
        string name = readString(nameLength, data, ref it);

        short displayNameLength = readShort(data, ref it);
        string displayName = readString(displayNameLength, data, ref it);

        int fp = readInt(data, ref it);
        int background = readInt(data, ref it);
        int difficulty = readInt(data, ref it);

        bool allowPlayerTeleport = readBool(data, ref it);
        bool showDisplays = readBool(data, ref it);

        short clientXMLLength = readShort(data, ref it);
        string clientXML = readString(clientXMLLength, data, ref it);

        short extraXMLLength = readShort(data, ref it);
        string extraXML = readString(extraXMLLength, data, ref it);

        Console.WriteLine("----------Map Info----------");
        Console.WriteLine("Width: " + width);
        Console.WriteLine("Height: " + height);
        Console.WriteLine("Name Length: " + nameLength);
        Console.WriteLine("Name: " + name);
        Console.WriteLine("Display Name Length: " + displayNameLength);
        Console.WriteLine("Display Name: " + displayName);
        Console.WriteLine("FP: " + fp);
        Console.WriteLine("Background: " + background);
        Console.WriteLine("Difficulty: " + difficulty);
        Console.WriteLine("Show Displays: " + showDisplays);
        Console.WriteLine("Client XML Length: " + clientXMLLength);
        Console.WriteLine("Client XML: " + clientXML);
        Console.WriteLine("Extra XML Length: " + extraXMLLength);
        Console.WriteLine("Extra XML: " + extraXML);
    }

    static void readLoadPacket(byte[] data)
    {
        int it = 0;

        int charID = readInt(data, ref it);

        bool fromArena = readBool(data, ref it);

        Console.WriteLine("----------Load----------");
        Console.WriteLine("Character ID: " + charID);
        Console.WriteLine("From Arena: " + fromArena);
    }

    static void readFailurePacket(byte[] data)
    {
        int it = 0;

        int ID = readInt(data, ref it);

        short errorStringLength = readShort(data, ref it);
        string errorString = readString(errorStringLength, data, ref it);

        Console.WriteLine("----------Failure----------");
        Console.WriteLine("Error ID: " + ID);
        Console.WriteLine("Error String Length: " + errorStringLength);
        Console.WriteLine("Error String: " + errorString);
    }

    static void readCreateSuccessPacket(byte[] data)
    {
        int it = 0;

        int objID = readInt(data, ref it);
        int charID = readInt(data, ref it);

        Console.WriteLine("----------Create Success----------");
        Console.WriteLine("Object ID: " + objID);
        Console.WriteLine("Character ID: " + charID);
    }

    static void readUpdatePacket(byte[] data)
    {
        int it = 0;

        short objID = readShort(data, ref it);

        Console.WriteLine("----------Update----------");
        Console.WriteLine("Tile Count: " + objID);
    }

    static void Main()
    {
        byte[] hello = RC4.Decrypt(Info.outKey, Info.hello);
        byte[] mapInfo = RC4.Decrypt(Info.inKey, Info.mapInfo);
        byte[] load = RC4.Decrypt(Info.outKey, Info.load);
        byte[] failure = RC4.Decrypt(Info.inKey, Info.failure);

        Rc4 rcout = new Rc4(Info.outKey);
        Rc4 rcin = new Rc4(Info.inKey);

        rcout.Text = Info.hello;
        readHelloPacket(rcout.EnDeCrypt());

        rcin.Text = Info.mapInfo;
        readMapInfoPacket(rcin.EnDeCrypt());

        rcout.Text = Info.load;
        readLoadPacket(rcout.EnDeCrypt());

        rcin.Text = Info.createSuccess;
        readCreateSuccessPacket(rcin.EnDeCrypt());

        rcin.Text = Info.update;
        readUpdatePacket(rcin.EnDeCrypt());

        //readMapInfoPacket(mapInfo);
        //readLoadPacket(load);
        //readFailurePacket(failure);
    }
}