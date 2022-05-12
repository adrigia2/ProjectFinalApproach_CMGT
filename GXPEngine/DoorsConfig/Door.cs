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
        Sound doorOpened = new Sound("Sounds/DoorOpen.wav", false, false);
        Sound buttonPressed = new Sound("Sounds/ButtonOn.wav", false, false);

        public DoorButton button;

        public int doorNumber;
        public Door(String name, int rows, int cols, TiledObject obj) : base(name, rows, cols, -1, true)
        {
            doorNumber = obj.GetIntProperty("doorNumber");
        }

        void Update()
        {
            if (button != null)
                if (button.isPressed)
                {
                    buttonPressed.Play();
                    doorOpened.Play();
                    visible = false;
                    Destroy();
                }
        }


    }
}
