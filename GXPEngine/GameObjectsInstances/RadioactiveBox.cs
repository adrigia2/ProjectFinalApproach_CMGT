using GXPEngine.Core;
using GXPEngine.GameObjectsInstances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiledMapParser;

namespace GXPEngine
{
    public class RadioactiveBox : AnimationSprite
    {
        private Vec2 gravity = new Vec2(0, 0.45f);
        private Vec2 velocityRotated = new Vec2(0, 0);

        private LevelCreation currentLevel;
        public RadioactiveBox(String name, int rows, int cols, TiledObject obj) : base(name, rows, cols, -1, true)
        {
        }

        void Update()
        {
            Movement();
        }
        void Movement()
        {

            Vec2 nonRotatedVelocity = new Vec2(0, 0);

            
            //nonRotatedVelocity.x += (Input.GetKey(Key.D) ? 3 : 0) - (Input.GetKey(Key.A) ? 3 : 0);
            nonRotatedVelocity += gravity;
            nonRotatedVelocity.RotateDegrees(-currentLevel.levelControl.rotationPlayer);
            velocityRotated += nonRotatedVelocity;
            velocityRotated *= 0.80f;
            velocityRotated.x = Mathf.Clamp(velocityRotated.x, -30, 15);
            velocityRotated.y = Mathf.Clamp(velocityRotated.y, -30, 15);


            if (MoveUntilCollision(0, velocityRotated.y, currentLevel.GetTiles(this)) != null /*|| MoveUntilCollision(0, velocityRotated.y, new List<GameObject>() { currentLevel.connect.door })!=null*/)
            {
                velocityRotated.y = 0;
            }

            if (MoveUntilCollision(velocityRotated.x, 0, currentLevel.GetTiles(this)) != null)
            {
                velocityRotated.x = 0;
            }

            if (Input.GetMouseButtonDown(1))
            {
                Console.WriteLine("rotated :" + velocityRotated);
                Console.WriteLine("--------------");
                Console.WriteLine("not rotated :" + nonRotatedVelocity);
                Console.WriteLine("--------------");
            }
        }

        public void SetLevel(LevelCreation _level)
        {
            currentLevel = _level;
        }
    }
}
