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

            Vec2 rotateVelocity = velocity;
            rotateVelocity.SetAngleDegrees(-level.levelControl.rotation,90);

            position += rotateVelocity;
            //this.x = position.x;
            //this.GetCollisions();

            if (MoveUntilCollision(rotateVelocity.x, rotateVelocity.y, GetCollisions(true, false)) != null)
            {
                velocity.y = -5;
            }

            SetPositionToScreen();
            //Console.WriteLine(this.y);
        }

        public void SetPositionToScreen()
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
