using GXPEngine.Core;
using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiledMapParser;

namespace GXPEngine
{
    
    class adwdads : Sprite
    {
        
        //Arrow _gravityIndicator;
        public LevelCreation level;

        bool touchLeft=false;
        bool touchRight=false;

        private float fallingSpeed = 0;

        Vec2 position;
        static Vec2 velocity = new Vec2(0, 0);
        Vec2 gravity = new Vec2(0, 0.05f);

        //Sprite sprite = new Sprite("box.png");
        public adwdads(TiledObject obj) : base(new Texture2D(17,17))
        {
            position.x = obj.X;
            position.y = obj.Y;
            //this.collider.isTrigger = true;
            SetOrigin(width / 2, height / 2);

            //_gravityIndicator = new Arrow(position, new Vec2(0, 0), 10, 0x66ff00);
            //AddChild(_gravityIndicator);

        }

        private void UpdatePosition()
        {
            if (MoveUntilCollision(0, fallingSpeed, level.GetTiles(this)) != null)
            {
                fallingSpeed = 0;
            }
            else
            {
                fallingSpeed += 1;
            }

            /*velocity += gravity;
            Vec2 gravityRotated = gravity;
            //gravityRotated.RotateDegrees(level.levelControl.rotationPlayer);


            //_gravityIndicator.vector = gravityRotated;
            //_gravityIndicator.startPoint = position;

            Vec2 velocityRotated = velocity;
            //velocityRotated.RotateDegrees(level.levelControl.rotationPlayer);
            position += velocityRotated;

            //MoveUntilCollision(velocityRotated.x, velocityRotated.y, GetCollisions(true, false));
            Collision collision;*/



            /*if((collision=MoveUntilCollision(velocityRotated.x, velocityRotated.y, level.GetTiles(this))) != null)
            {
                position += -velocityRotated * (collision.timeOfImpact+0.01f);
                CollisionResolve(collision);
            }*/


            //Console.WriteLine(this.y);
        }

        /*void CollisionResolve(Collision collision)
        {
            Vec2 normal = new Vec2(collision.normal);
            if (level.levelControl.rotationPlayer == 0 || level.levelControl.rotationPlayer == 180)
            {
                Console.WriteLine(normal+" case 1");
                if (normal.x != 0)
                {
                    velocity.x = -velocity.x;
                }
                else
                    if (normal.y != 0)
                    velocity.y = -velocity.y;
            }
            else
            {
                Console.WriteLine(normal + " case 2");
                if (normal.x != 0)
                {
                    velocity.y = -velocity.y;
                }
                else
                    if (normal.y != 0)
                    velocity.x = -velocity.x;
            }
            //touchLeft = false;
            //touchRight=false;
        }*/

        void ControlPlayer()
        {
            if(!touchLeft)
            if (Input.GetKeyDown(Key.D))
                velocity.x =2f;
            if (Input.GetKeyUp(Key.D))
                velocity.x = 0;

            if(!touchLeft)
            if (Input.GetKeyDown(Key.A))
                velocity.x = -2f;
            if (Input.GetKeyUp(Key.A))
                velocity.x = 0;
        }
        

        void UpdateScreenPosition()
        {
            x = position.x;
            y = position.y;
        }

        private void Update()
        {

            UpdatePosition();
            ControlPlayer();
            UpdateScreenPosition();
        }

        public void SetLevel(LevelCreation _level)
        {
            level = _level;
            //p = _level.FindObjectOfType<Player>();
        }
    }
}
