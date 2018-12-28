using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PacketAnalyzer.Packets;

namespace PacketAnalyzer
{
    class Program
    {
        private static string ReadLine()
        {
            Stream inputStream = Console.OpenStandardInput(20000);
            byte[] bytes = new byte[20000];
            int outputLength = inputStream.Read(bytes, 0, 20000);
            //Console.WriteLine(outputLength);
            char[] chars = Encoding.UTF7.GetChars(bytes, 0, outputLength);
            return new string(chars);
        }

        static void Main(string[] args)
        {
            while (true)
            {
                string hexString = "";
                Console.Write("Hex: ");
                hexString = ReadLine();

                hexString = Regex.Replace(hexString, ".{2}", "$0 ");
                byte[] data = hexString.Trim().Split(' ').Select(item => Convert.ToByte(item, 16)).ToArray();

                switch (data[4])
                {
                    case 100:
                    {
                        HelloPacket hello = new HelloPacket(data);
                        hello.Print();
                    } break;

                    case 62:
                    {
                        LoadPacket load = new LoadPacket(data);
                        load.Print();
                    } break;

                    case 80:
                    {
                        UpdateAckPacket uack = new UpdateAckPacket(data);
                        uack.Print();
                    } break;

                    case 74:
                    {
                        MovePacket move = new MovePacket(data);
                        move.Print();
                    } break;

                    case 86:
                    {
                        PongPacket pong = new PongPacket(data);
                        pong.Print();
                    } break;
                }
            }
        }
    }
}
