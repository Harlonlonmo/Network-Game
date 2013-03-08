using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Network_Game
{
    public class SpriteIdGenerator
    {
        private static uint counter = 0;
 
        public static uint getId(){
            return counter++;
        }
    }
}
