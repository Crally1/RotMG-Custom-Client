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
            //FailurePacket err = new FailurePacket(connection.ReadBytes());
            //return;

            Log.Info("Received CREATESUCCESS packet");

            playerObjectID = success.ObjectID;

            Location lastPos = new Location(0, 0);
            LocationRecord[] records = new LocationRecord[1];
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
                    } break;

                    case 0x24:
                    {
                        NewTickPacket tick = new NewTickPacket(packet);
                        for (int i = 0; i < tick.Statuses.Length; ++i)
                        {
                            if (tick.Statuses[i].objectID == playerObjectID)
                            {
                                Log.Info("Updating position");
                                lastPos = tick.Statuses[i].pos;
                                records[0] = new LocationRecord(Timer.getTime(), lastPos.x, lastPos.y);
                                break;
                            }
                        }

                        int time = Timer.getTime();
                        connection.Send(new MovePacket(tick.TickID, time, lastPos, tick.TickID > 0 ? records : new LocationRecord[0]));
                        Log.Info($"Tick [{tick.TickID}:{tick.TickTime}] -> " +
                                    $"Move [ID:{tick.TickID}:X:{lastPos.x.ToString("F2")}:Y:{lastPos.y.ToString("F2")}:Time:{time}]");
                    } break;

                    //case 0x1c:
                    //    {
                    //        ShowEffectPacket effect = new ShowEffectPacket(packet);
                    //    }
                    //    break;

                    case 0x14:
                    {
                        new TextPacket(packet).Print();
                    }
                    break;

                    case 0x04:
                    {
                        PingPacket ping = new PingPacket(packet);
                        PongPacket pong = new PongPacket(ping.Serial, Timer.getTime());

                        connection.Send(pong);

                        Log.Info($"Ping [{ping.Serial}] -> Pong [{pong.Serial}:{pong.Time}]");
                    }
                    break;

                    case 0x00:
                    {
                        FailurePacket error = new FailurePacket(packet);
                        Log.Error(error.ToString());
                        return;
                    } break;

                    case 0x4c:
                    {
                        GoToPacket go = new GoToPacket(packet);
                        GoToAckPacket ack = new GoToAckPacket(Timer.getTime());

                        connection.Send(ack);

                        Log.Info($"GoTo [ObjID:{go.ObjectID}:X:{go.Pos.x}:Y:{go.Pos.y}] -> " +
                            $"GoToAck [Time:{ack.Time}]");
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
