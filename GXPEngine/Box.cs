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

        float fallingSpeed = 1;

        Vec2 position;
        static Vec2 velocity = new Vec2(0, 0);
        Vec2 gravity = new Vec2(0, 1);

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
            /* if (fallingSpeed <= 15)
                 fallingSpeed += 1;*/
            velocity += gravity;
            //position += velocity;
            //this.x = position.x;
            //this.GetCollisions();
            MoveUntilCollision(velocity.x, 0, GetCollisions(true, false));
            if(MoveUntilCollision(0, fallingSpeed, level.GetTiles(this)) != null)
            {
                fallingSpeed = 0;
                velocity.y = 0;
            }
            Console.WriteLine(this.y);
        }

        private void Update()
        {
            UpdatePosition();
        }
    }
}
