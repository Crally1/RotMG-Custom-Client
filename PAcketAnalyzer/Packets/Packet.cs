using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketAnalyzer.Packets
{
    class Packet
    {
        static RC4 inStream = new RC4(Info.inKey);
        static RC4 outStream = new RC4(Info.outKey);

        byte[] data;
        int it;
        int size;

        protected Packet(byte[] packet)
        {
            it = 0;

            size = (packet[0] << 24) | (packet[1] << 16) | (packet[2] << 8) | (packet[3]) - 5;
            data = new byte[size];

            Array.Copy(packet, 5, data, 0, size);

            outStream.Text = data;
            data = outStream.EnDeCrypt();
        }

        protected int ReadInt()
        {
            return (data[it++] << 24) | (data[it++] << 16) | (data[it++] << 8) | (data[it++]);
        }

        protected short ReadShort()
        {
            return (short)(data[it++] << 8 | data[it++]);
        }

        protected byte ReadByte()
        {
            return data[it++];
        }

        protected byte[] ReadBytes(bool large = false)
        {
            int size = large ? ReadInt() : ReadShort();
            byte[] bytes = new byte[size];

            for (int i = 0; i < size; ++i)
                bytes[i] = ReadByte();

            return bytes;
        }

        protected bool ReadBool()
        {
            return data[it++] != 0;
        }

        protected float ReadFloat()
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

        protected string ReadString()
        {
            short length = ReadShort();
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < length; ++i)
                builder.Append((char)data[it++]);

            return builder.ToString();
        }

        protected void Header(string name)
        {
            Console.Write(name);
            for (int i = 0; i < Console.WindowWidth - name.Length; ++i)
                Console.Write("_");
        }
    }
}
