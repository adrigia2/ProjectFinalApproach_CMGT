using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiledMapParser;

namespace GXPEngine
{
    class Waypoint : AnimationSprite
    {
        public Waypoint(String name, int rows, int cols, TiledObject obj) : base(name, rows, cols, -1, false)
        {
            visible = false;
            //Console.WriteLine("waypoint");
        }
    }
}
