using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CrallyClient.Packets.Client
{
    class ClientPacket
    {
        protected MemoryStream stream;

        static private Crypto.RC4 rcout = new Crypto.RC4(Info.outKey);

        private byte[] byteBuffer;
        private byte[] shortBuffer;
        private byte[] intBuffer;

        protected ClientPacket()
        {
            stream = new MemoryStream();

            byteBuffer = new byte[1];
            shortBuffer = new byte[2];
            intBuffer = new byte[4];
        }

        protected void writeShort(short num)
        {
            shortBuffer[0] = (byte)(num >> 8);
            shortBuffer[1] = (byte)(num & 0x00FF);
            stream.Write(shortBuffer, 0, 2);
        }

        protected void writeInt(int num)
        {
            intBuffer[0] = (byte)(num >> 24);
            intBuffer[1] = (byte)((num >> 16) & 0xFF);
            intBuffer[2] = (byte)((num >> 8) & 0xFF);
            intBuffer[3] = (byte)(num & 0xFF);
            stream.Write(intBuffer, 0, 4);
        }

        protected void writeFloat(float num)
        {
            byte[] bytes = BitConverter.GetBytes(num);
            //Array.Reverse(bytes);
            stream.Write(bytes, 0, 4);
        }

        protected void writeBool(bool flag)
        {
            byteBuffer[0] = (byte)(flag ? 1 : 0);
            stream.Write(byteBuffer, 0, 1);
        }

        protected void writeString(string str)
        {
            writeShort((short)str.Length);
            stream.Write(Encoding.UTF8.GetBytes(str), 0, str.Length);
        }

        protected void writeBytes(byte[] bytes, bool large = false)
        {
            if (large)
                writeInt(bytes.Length);
            else
                writeShort((short)bytes.Length);

            stream.Write(bytes, 0, bytes.Length);
        }

        public virtual byte[] build()
        {
            rcout.Text = stream.ToArray();

            byte[] data = rcout.EnDeCrypt();
            byte[] packet = new byte[data.Length + 5];
            int size = data.Length + 5;

            packet[0] = (byte)(size >> 24);
            packet[1] = (byte)((size >> 16) & 0xFF);
            packet[2] = (byte)((size >> 8) & 0xFF);
            packet[3] = (byte)(size & 0xFF);

            packet[4] = 100;

            for (int i = 0; i < data.Length; ++i)
                packet[5 + i] = data[i];

            return packet;
        }
    }
}
