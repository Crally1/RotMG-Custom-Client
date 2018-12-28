using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketAnalyzer.Packets
{
    class MovePacket : Packet
    {
        int tickID;
        int time;
        Location pos;
        LocationRecord[] records;

        public MovePacket(byte[] packet) : base(packet)
        {
            tickID = ReadInt();
            time = ReadInt();
            pos = new Location(ReadFloat(), ReadFloat());

            records = new LocationRecord[ReadShort()];
            for (int i = 0; i < records.Length; ++i)
                records[i] = new LocationRecord(ReadInt(), ReadFloat(), ReadFloat());
        }

        public void Print()
        {
            Header("Move");

            Console.WriteLine("Tick ID: " + tickID);
            Console.WriteLine("Time: " + time);
            Console.WriteLine("Minutes: " + TimeSpan.FromMilliseconds(time).TotalMinutes);
            Console.WriteLine("Location X: " + pos.x);
            Console.WriteLine("Location Y: " + pos.y);

            if (records.Length > 0) Console.WriteLine("Records:");
            for (int i = 0; i < records.Length; ++i)
            {
                Console.WriteLine("    Time: " + records[i].Time);
                Console.WriteLine("    PosX: " + records[i].X);
                Console.WriteLine("    PosY: " + records[i].Y);
            }
        }
    }
}
