using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Network_Game.Nettwork
{
    enum PacketTypes
    {
        ConnectionRequest,
        ConnectionResponse,
        StateChange,
        StartGame,
        EndGame,
        ClientInput,
        ClientSprite
    }
}
