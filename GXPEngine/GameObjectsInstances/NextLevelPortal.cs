using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

namespace GXPEngine
{
    public class NextLevelPortal : Sprite
    {
        public string nextLevelName;
        public NextLevelPortal(TiledObject obj) : base(new Texture2D(1, 1))
        {
            collider.isTrigger = true;
            nextLevelName = obj.GetStringProperty("NextLevelName");
        }
    }
}
