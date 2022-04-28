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

            camera = new Camera(-480, -480, 960, 960);

            game.AddChild(camera);
            //AddChild(camera);


            this.SetOrigin(this.width / 2, this.height / 2);
        }

        void Update()
        {
            if (Input.GetKeyDown(Key.RIGHT))
            {
                rotationPlayer -= 90f;
                camera.rotation = -rotationPlayer;
                //camera.rotation = rotationPlayer;
            }
            if (Input.GetKeyDown(Key.LEFT))
            {
                rotationPlayer += 90f;
                camera.rotation = -rotationPlayer;
                //camera.rotation = rotationPlayer;
            }
        }
    }
}
