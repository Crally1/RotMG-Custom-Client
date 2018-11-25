using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using CrallyClient.Packets.Client;
using CrallyClient.Packets.Server;

namespace CrallyClient
{
    class Program
    {
        static byte[] packet = new byte[20000];

        static int playerObjectID;

        static void print(string info)
        {
            Console.WriteLine(DateTime.Now.ToString("[HH:mm:ss] ") + info);
        }

        static void error(string err)
        {
            ConsoleColor old = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            print(err);
            Console.ForegroundColor = old;
        }

        static byte[] readPacket(NetworkStream stream)
        {
            stream.Read(packet, 0, 5);
            int size = ((packet[0] << 24) | (packet[1] << 16) | (packet[2] << 8) | (packet[3]) - 5);

            if (size == -5)
                size = 0;

            print($"Read packet [ID:0x{packet[4].ToString("X0")}]: size={size}");

            int read = 0;
            while (read != size)
            {
                read += stream.Read(packet, 5 + read, size - read);
            }

            return packet;
        }

        static string policyRequest(TcpClient client, byte[] data)
        {
            print("Requesting policy file");

            client.Connect("13.57.254.131", 843);
            NetworkStream stream = client.GetStream();
            stream.Write(Encoding.UTF8.GetBytes("<policy-file-request/>\0"), 0, 23);
            stream.Read(data, 0, data.Length);
            client.Close();

            print("Received policy file");

            return Encoding.UTF8.GetString(data).TrimEnd((char)0);
        }

        static void Main(string[] args)
        {
            Timer.init();
            byte[] data = new byte[50000];
            TcpClient client = new TcpClient();
            NetworkStream stream;

            const string ip = "13.57.254.131";
            const int port = 2050;

            policyRequest(client, data);

            print("Opening socket to " + ip + " on " + port);
            client = new TcpClient();
            client.Connect(ip, port);
            stream = client.GetStream();

            print("Sending HELLO packet");
            byte[] hello = new HelloPacket("crally68@gmail.com", "Clocker12345").build();
            stream.Write(hello, 0, hello.Length);

            print("Receiving map info");
            readPacket(stream);
            MapInfoPacket mapInfo = new MapInfoPacket(packet);

            print("Sending LOAD packet");
            byte[] load = new LoadPacket(1, false).build();
            stream.Write(load, 0, load.Length);

            readPacket(stream);
            CreateSuccessPacket success = new CreateSuccessPacket(packet);
            print("Received CREATESUCCESS packet");

            playerObjectID = success.objectID;

            print("CONNECTED");

            Location lastPos = new Location(0, 0);
            int updateCount = 0;
            int prevTime = Timer.getTime();
            while (true)
            {
                readPacket(stream);   

                switch (packet[4])
                {
                    case 0x1F:
                        {
                            new UpdatePacket(packet);
                            byte[] update = new UpdateAckPacket().build();
                            stream.Write(update, 0, update.Length);
                            print($"UpdateAck Sent [{++updateCount}]");
                        }
                        break;

                    case 0x24:
                        {
                            NewTickPacket tick = new NewTickPacket(packet);
                            for (int i = 0; i < tick.statuses.Length; ++i)
                            {
                                if (tick.statuses[i].objectID == playerObjectID)
                                {
                                    print("Updating position");
                                    lastPos = tick.statuses[i].pos;
                                    break;
                                }
                            }

                            int time = Timer.getTime();
                            byte[] move = new MovePacket(tick.tickID, time, lastPos).build();
                            stream.Write(move, 0, move.Length);
                            print($"Tick [{tick.tickID}:{tick.tickTime}] -> " +
                                $"Move [ID:{tick.tickID}:X:{lastPos.x.ToString("F2")}:Y:{lastPos.y.ToString("F2")}:Time:{time}]");
                            prevTime = time;
                        }
                        break;

                    //case 0x1c:
                    //    {
                    //        ShowEffectPacket effect = new ShowEffectPacket(packet);
                    //    }
                    //    break;

                    case 0x14:
                        {
                            new TextPacket(packet).print();
                        }
                        break;

                    case 0x04:
                        {
                            PingPacket ping = new PingPacket(packet);
                            print($"Ping [{ping.serial}]");

                            int time = Timer.getTime();
                            byte[] pong = new PongPacket(ping.serial, time).build();
                            stream.Write(pong, 0, pong.Length);
                            print($"Pong [{ping.serial}:{time}]");
                        }
                        break;

                    case 0x00:
                        {
                            error(new ErrorPacket(packet).get());
                            return;
                        }
                        break;

                    default:
                        {
                            new UnknownPacket(packet);
                            //print($"Unimplemented packet [ID:{packet[4]}]");
                        }
                        break;
                }
            }

        }
    }
}
