using GXPEngine.Core;
using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    class LevelControl : Sprite
    {
        LevelCreation level = new LevelCreation();

        public LevelControl(float _width, float _height) : base("Untitled.png")
        {
            this.collider.isTrigger = false;
            
            level.CreateLevel("TestMap");
            level.SetXY(0 - this.width / 2, 0 - this.height / 2);
            level.SetLevelControl(this);
            //level.SetXY(this.width/2, this.height/2);
            AddChild(level);
            this.SetOrigin(this.width / 2, this.height / 2);
        }

        void Update()
        {
            if (Input.GetKey(Key.RIGHT))
            {
                this.rotation -= 0.5f;
            }

            if (Input.GetKey(Key.LEFT))
            {
                this.rotation += 0.5f;
            }
        }
    }
}
