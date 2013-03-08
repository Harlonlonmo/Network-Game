using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Network_Game.Server
{
    public abstract class ServerState
    {

        public ServerObject Server { get; protected set; }

        public ServerState(ServerObject server)
        {
            Server = server;
        }

        public abstract void Update(GameTime gameTime);

    }
}
