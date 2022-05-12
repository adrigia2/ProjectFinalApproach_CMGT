using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

namespace GXPEngine
{
    public class Boundaries : Sprite
    {
        public bool invisible = false;
        public Boundaries(TiledObject obj) : base(new Texture2D(1,1))
        {
        }
        

        void Update()
        {
            collider.isTrigger = true;

        }
    }
}
