using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

namespace GXPEngine
{
    public class Laser : AnimationSprite
    {
        float timer = 0f;
        public bool invisible = false;
        public Laser(String name, int rows, int cols, TiledObject obj) : base(name, rows, cols, -1, true)
        {
            this.collider.isTrigger = true;
        }

        void Update()
        {
            if (Time.time - timer > 5000)
            {
                invisible = !invisible;
                timer = Time.time;
                if (invisible)
                {
                    this.visible = false;
                    this.collider.isTrigger = false;
                }
                else
                {
                    this.visible = true;
                    this.collider.isTrigger = true;
                }
            }

            

        }
    }
}
