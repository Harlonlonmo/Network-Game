using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nettwork_Game.Services;
using Lidgren.Network;

namespace Network_Game.Nettwork
{
    public class InputParser
    {

        public static void readBytes(NetIncomingMessage data, ref InputObject io)
        {
            data.ReadByte();
            Console.WriteLine(io.HorizontalAxis = data.ReadFloat());
            Console.WriteLine(io.VerticalAxis = data.ReadFloat());
            Console.WriteLine(io.AmingAngle = data.ReadFloat());
            Console.WriteLine(io.Buttons = data.ReadByte());

        }

        public static void writeBytes(NetOutgoingMessage om, InputObject input)
        {
            om.Write((byte)PacketTypes.ClientInput);
            om.Write((byte)13);
            // 3 floats:
            //4   move horisontal
            //4   move vertical
            //4   aming angle
            // 8 bits:
            //1  buttons
            om.Write(input.HorizontalAxis);
            om.Write(input.VerticalAxis);
            om.Write(input.AmingAngle);
            om.Write(input.Buttons);
        }
    }
}
