using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CrallyClient.Packets.Server
{
    class ServerPacket
    {
        protected int size;
        protected byte[] data;
        
        private int it;

        static private Crypto.RC4 rcin = new Crypto.RC4(Info.inKey);

        protected ServerPacket(byte[] packet)
        {
            it = 0;

            size = (packet[0] << 24) | (packet[1] << 16) | (packet[2] << 8) | (packet[3]) - 5;
            data = new byte[size];
            Array.Copy(packet, 5, data, 0, size);

            rcin.Text = data;
            data = rcin.EnDeCrypt();
        }

        protected int readInt()
        {
            return (data[it++] << 24) | (data[it++] << 16) | (data[it++] << 8) | (data[it++]);
        }

        protected short readShort()
        {
            return (short)(data[it++] << 8 | data[it++]);
        }

        protected byte readByte()
        {
            return data[it++];
        }

        protected bool readBool()
        {
            return data[it++] != 0;
        }

        protected float readFloat()
        {
            byte[] bytes = new byte[4];
            float[] fl = new float[1];
            bytes[0] = (byte)(data[it + 3]);
            bytes[1] = (byte)(data[it + 2]);
            bytes[2] = (byte)(data[it + 1]);
            bytes[3] = (byte)(data[it]);
            Buffer.BlockCopy(bytes, 0, fl, 0, 4);
            it += 4;
            return fl[0];
        }

        protected string readString()
        {
            short length = readShort();
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < length; ++i)
                builder.Append((char)data[it++]);

            return builder.ToString();
        }
    }
}
