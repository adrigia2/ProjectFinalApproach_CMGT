using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

namespace GXPEngine
{
    public class ButtonDoor : AnimationSprite
    {
        public bool isPressed=false;

        public ButtonDoor(String name, int rows, int cols, TiledObject obj) : base(name, rows, cols, -1, true)
        {
            this.collider.isTrigger = true;
        }

        public void ActivateButton()
        { 
            isPressed = true;
            visible = false;
        }
        



    }
}
