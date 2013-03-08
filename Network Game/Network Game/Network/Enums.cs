using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Network_Game.Network
{
    public enum PacketTypes
    {
        ConnectionRequest,
        ConnectionResponse,
        StateChange,
        StartGame,
        EndGame,
        ClientInput,
        ClientSprite
    }

    public enum SpriteIDs
    {
        platform1 = 1,
        Player1_anim1_frame1 = 0,
        Player1_anim1_frame2 = 2,
        Player1_anim1_frame3 = 3
    }
}
