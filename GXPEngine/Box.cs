using GXPEngine.Core;
using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiledMapParser;

namespace GXPEngine
{
    
    class Box : Sprite
    {
        public LevelCreation level;

        Vec2 position;
        static Vec2 velocity = new Vec2(0, 0);
        Vec2 gravity = new Vec2(0, 0.1f);

        //Sprite sprite = new Sprite("box.png");
        public Box(TiledObject obj) : base("box.png")
        {
            position.x = obj.X;
            position.y = obj.Y;
            //this.collider.isTrigger = true;
            SetOrigin(width / 2, height / 2);
        }

        private void UpdatePosition()
        {
            velocity += gravity;

            Vec2 velocityRotated = velocity;
            velocityRotated.RotateDegrees(level.levelControl.rotationPlayer);
            position += velocityRotated;

            //MoveUntilCollision(velocityRotated.x, velocityRotated.y, GetCollisions(true, false));
            Collision collision;

            ControlPlayer();
            

            if((collision=MoveUntilCollision(velocityRotated.x, velocityRotated.y, level.GetTiles(this))) != null)
            {
                position += -velocityRotated * collision.timeOfImpact;
                velocity.y = -velocity.y;
            }

            UpdateScreenPosition();

            Console.WriteLine(this.y);
        }

        void ControlPlayer()
        {
            if (Input.GetKey(Key.D))
            {
                velocity.x =2f;
            }
            if (Input.GetKey(Key.A))
            {
                velocity.x =-2f;
            }
        }
        

        void UpdateScreenPosition()
        {
            x = position.x;
            y = position.y;
        }

        private void Update()
        {
            UpdatePosition();
        }
    }
}
