using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

namespace GXPEngine.GameObjectsInstances
{
    public class NextLevelPortal : AnimationSprite
    {
        public string nextLevelName;
        public NextLevelPortal(String name, int rows, int cols, TiledObject obj) : base(name, rows, cols, -1, true)
        {
            nextLevelName = obj.GetStringProperty("NextLevelName");
        }
    }
}
