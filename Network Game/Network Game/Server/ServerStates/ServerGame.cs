using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Network_Game.Server.GameObjects;

namespace Network_Game.Server.ServerStates
{
    public class ServerGame : ServerState
    {

        Dictionary<long, Player> Players;

        public ServerGame(ServerObject server)
            : base(server)
        {
            Players = new Dictionary<long, Player>();
        }

        public override void Update(GameTime gameTime)
        {


            foreach (Player obj in Players.Values)
            {
                obj.Update(gameTime);
            }
        }
    }
}
