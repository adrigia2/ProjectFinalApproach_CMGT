using GXPEngine.Core;
using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiledMapParser;

namespace GXPEngine
{
    
    class Box : AnimationSprite
    {
        public LevelCreation level;

        Vec2 position;
        static Vec2 velocity = new Vec2(0, 0);
        static Vec2 gravity = new Vec2(0, 1);
        public Box(String name, int rows, int cols, TiledObject obj) : base(name, rows, cols, -1, true)
        {
            position.x = obj.X;
            position.y = obj.Y;
            //this.collider.isTrigger = true;
        }

        private void UpdatePosition()
        {
            velocity += gravity;
            //position += velocity;
            //this.x = position.x;
            if (MoveUntilCollision(0, velocity.y, level.GetTiles(this)) != null)
            {
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
