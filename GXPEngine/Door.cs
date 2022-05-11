using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

namespace GXPEngine
{
    public class Door : AnimationSprite
    {
        public ButtonDoor buttonDoor;

        public Door(String name, int rows, int cols, TiledObject obj) : base(name, rows, cols, -1, true)
        {
        }

        void Update()
        {
            if (buttonDoor != null)
                if (buttonDoor.isPressed)
                {
                    visible = false;
                    Destroy();
                }
        }


    }
}
