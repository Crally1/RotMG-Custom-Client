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
        static int playerObjectID;

        static void Main(string[] args)
        {
            Timer.init();

            const string ip = "13.57.254.131";
            const int port = 2050;

            Connection connection = new Connection(ip, port);

            Log.Info("Sending HELLO packet");

            connection.Send(new HelloPacket("crally68@gmail.com", "Clocker12345"));

            MapInfoPacket mapInfo = new MapInfoPacket(connection.ReadBytes());

            Log.Info("Received map info");
            Log.Info("Sending LOAD packet");

            connection.Send(new LoadPacket(1, false));

            CreateSuccessPacket success = new CreateSuccessPacket(connection.ReadBytes());

            Log.Info("Received CREATESUCCESS packet");

            playerObjectID = success.objectID;

            Location lastPos = new Location(0, 0);
            while (true)
            {
                byte[] packet = connection.ReadBytes(); 

                switch (packet[4])
                {
                    case 0x1F:
                        {
                            new UpdatePacket(packet);
                            connection.Send(new UpdateAckPacket());

                            Log.Info("Update -> UpdateAck");
                        }
                        break;

                    case 0x24:
                        {
                            NewTickPacket tick = new NewTickPacket(packet);
                            for (int i = 0; i < tick.statuses.Length; ++i)
                            {
                                if (tick.statuses[i].objectID == playerObjectID)
                                {
                                    Log.Info("Updating position");
                                    lastPos = tick.statuses[i].pos;
                                    break;
                                }
                            }

                            int time = Timer.getTime();
                            connection.Send(new MovePacket(tick.tickID, time, lastPos));
                            Log.Info($"Tick [{tick.tickID}:{tick.tickTime}] -> " +
                                     $"Move [ID:{tick.tickID}:X:{lastPos.x.ToString("F2")}:Y:{lastPos.y.ToString("F2")}:Time:{time}]");
                        }
                        break;

                    //case 0x1c:
                    //    {
                    //        ShowEffectPacket effect = new ShowEffectPacket(packet);
                    //    }
                    //    break;

                    //case 0x14:
                    //    {
                    //        new TextPacket(packet).print();
                    //    }
                    //    break;

                    case 0x04:
                        {
                            PingPacket ping = new PingPacket(packet);
                            PongPacket pong = new PongPacket(ping.serial, Timer.getTime());

                            connection.Send(pong);

                            Log.Info($"Ping [{ping.serial}] -> Pong [{pong.serial}:{pong.time}]");
                        }
                        break;

                    case 0x00:
                        {
                            FailurePacket error = new FailurePacket(packet);
                            Log.Error(error.ToString());
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
