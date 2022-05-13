using GXPEngine.Core;
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

        Sprite sprite;
        public DoorButton button;

        public int doorNumber;
        public Door(String name, int rows, int cols, TiledObject obj) : base(name, rows, cols, -1, true)
        {
            SetOrigin(0, 0);
            sprite = new Sprite(new Texture2D("Backgrounds/door.png"));
            sprite.SetXY(-width / 2, -height / 2);
            sprite.scale = 0.3f;
            sprite.scaleY = 0.05f;
            AddChild(sprite);
            doorNumber = obj.GetIntProperty("doorNumber");
        }

        void Update()
        {
            if (button != null)
                if (button.isPressed)
                {
                    buttonPressed.Play(volume: 0.25f);
                    doorOpened.Play(volume: 0.25f);
                    visible = false;
                    Destroy();
                }
        }


    }
}
