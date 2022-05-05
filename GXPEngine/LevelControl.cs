using GXPEngine.Core;
using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class LevelControl : Sprite
    {
        int state = -1;
        LevelCreation level = new LevelCreation();
        Camera camera;

        public float rotationPlayer = 0;
        public LevelControl(float _width, float _height) : base("Untitled.png")
        {
            this.collider.isTrigger = false;

            level.CreateLevel("TestMap2");
            level.SetXY(0 - this.width / 2, 0 - this.height / 2);
            level.SetLevelControl(this);

            AddChild(level);

            //setting up the camera was easy, i just moved the level control to 0,0 in MyGame
            camera = new Camera(0, 0, 960, 960);
            game.AddChild(camera);

            this.SetOrigin(this.width / 2, this.height / 2);
        }

        void Update()
        {
            bool something = false;
            if (Input.GetKeyDown(Key.RIGHT))
            {
                something = true;
                state++;
                rotationPlayer -= 90f;
                if (rotationPlayer < 0)
                    rotationPlayer += 360;
                //camera.rotation = -rotationPlayer;
                camera.rotation = -rotationPlayer;
            }
            if (Input.GetKeyDown(Key.LEFT))
            {
                state--;
                something = true;
                rotationPlayer += 90f;
                if (rotationPlayer == 360)
                    rotationPlayer = 0;
                camera.rotation = -rotationPlayer;
            }

            //Console.WriteLine(rotationPlayer);
        }
    }
}
