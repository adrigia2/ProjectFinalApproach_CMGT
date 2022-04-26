using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiledMapParser;

namespace GXPEngine
{
    internal class Box2 : Sprite
    {
        Vec2 position;
        static Vec2 velocity = new Vec2(0, 0);
        static Vec2 gravity = new Vec2(0, 1);
        public Box2(TiledObject obj) : base(new Texture2D(16, 16))
        {
            position.x = obj.X;
            position.y = obj.Y;
            this.collider.isTrigger = true;
        }

        private void UpdatePosition()
        {
            position += velocity;
            this.x = position.x;
            this.y = position.y;
        }

        private void UpdateVelocity()
        {
            velocity += gravity;
        }
    }
}
