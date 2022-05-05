using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiledMapParser;

namespace GXPEngine
{
    class Spike : AnimationSprite
    {
        public int damage = 1;
        public Spike(String name, int rows, int cols, TiledObject obj) : base(name, rows, cols, -1, true)
        {
            this.collider.isTrigger = true;
            //comment
        }
    }
}
