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

            //level.SetScaleXY(0.5f);
            
            //level.SetXY(this.width/2, this.height/2);
            AddChild(level);

            //camera = new Camera(-game.width/2, -game.height / 2, game.width, game.height);
            //camera.SetScaleXY(2);
            //game.AddChild(camera);
            //AddChild(camera);


            this.SetOrigin(this.width / 2, this.height / 2);
        }

        void Update()
        {
            bool something=false;
            if (Input.GetKeyDown(Key.RIGHT))
            {
                something = true;
                state++;
                rotationPlayer -= 90f;
                if (rotationPlayer < 0)
                    rotation += 360;
                //camera.rotation = -rotationPlayer;
                //camera.rotation = rotationPlayer;
            }
            if (Input.GetKeyDown(Key.LEFT))
            {
                state--;
                something = true;
                rotationPlayer += 90f;
                if (rotationPlayer == 360)
                    rotationPlayer = 0;
                //camera.rotation = -rotationPlayer;
                //camera.rotation = rotationPlayer;
            }
            //if (something)
            //{
            //    if (state == 4)
            //        state = 0;
            //    else
            //    if (state == -1)
            //        state = 3;

            //    if (state == 0)
            //    {
            //        camera.x += game.width;
            //    }
            //    else
            //    if (state == 1)
            //    {
            //        camera.y += game.height;
            //    }
            //    else
            //            if (state == 2)
            //    {
            //        camera.x -= game.width;
            //    }
            //    if (state == 3)
            //    {
            //        camera.y -= game.height;
            //        state = 0;
            //    }
            //}

        }
    }
}
