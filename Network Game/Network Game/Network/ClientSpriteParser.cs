using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace Network_Game.Network
{
    class ClientSpriteParser
    {

        public static void readBytes(NetIncomingMessage data, ClientSprite cs)
        {
            data.ReadByte();
            cs.ID = data.ReadUInt32();
            cs.SpriteID = (SpriteIDs)data.ReadUInt16();
            cs.Position = new Microsoft.Xna.Framework.Vector2(data.ReadFloat(),data.ReadFloat());
            cs.Rotation = data.ReadFloat();
        }

        public static void writeBytes(NetOutgoingMessage om, ClientSprite cs)
        {
                om.Write((byte)PacketTypes.ClientSprite);
                om.Write((byte)18);
                om.Write(cs.ID);
                om.Write((ushort)cs.SpriteID);
                om.Write(cs.Position.X);
                om.Write(cs.Position.Y);
                om.Write(cs.Rotation);
        }
    }
}
