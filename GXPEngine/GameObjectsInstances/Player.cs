using GXPEngine.Core;
using GXPEngine.GameObjectsInstances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiledMapParser;


namespace GXPEngine
{
    public enum Facing {LEFT, RIGHT, JUMPING, IDLE};
    class Player : Sprite
    {
        private bool canJump = false;
        float speed = 2f;
        Facing facing;


        public float debug = 0;
        Vec2 velocityRotated = new Vec2(0, 0);
        Vec2 gravity;

        //private String characterName;

        public int HP = 6;
        public int damage = 2;
        public int maxHP = 6;

        private AnimationSprite animations;

        private LevelCreation currentLevel;


        public Player(TiledObject obj) : base(new Texture2D(295, 576))
        {
            this.collider.isTrigger = true;

             animations = new AnimationSprite("2 GraveRobber/sam.png", 1, 1, -1, false, false);
            //playerSkin = new Sprite("2 GraveRobber/sam_256px.png");

            //Console.WriteLine(animations.width);
            AddChild(animations);
            AddChild(animations);
            SetOrigin(width / 2, height / 2);
            animations.SetOrigin(animations.width / 2, animations.height / 2 + 8);
            gravity = new Vec2(0, 1f);
        }

        public void Update()
        {
            rotation = -currentLevel.levelControl.rotationPlayer;

            if (currentLevel == null)
                return;

                Movement();
                CheckCollisionObject();
        }

        private void CheckCollisionObject()
        {
            GameObject[] objects = this.GetCollisions(true, false);
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] is Laser laser)
                {
                    if (!laser.invisible)
                    {
                        currentLevel.levelControl.LoadLevel(currentLevel.levelControl.levelName);
                    }
                }

                if (objects[i] is Boundaries)
                {
                        currentLevel.levelControl.LoadLevel(currentLevel.levelControl.levelName);
                }

                if (objects[i] is RadioactiveBox)
                {             
                        currentLevel.levelControl.LoadLevel(currentLevel.levelControl.levelName);
                }

                if (objects[i] is DoorButton button)
                {
                    ((DoorButton)button).isPressed = true;
                }

                if(objects[i] is NextLevelPortal portal)
                {
                    currentLevel.levelControl.LoadLevel(portal.nextLevelName);
                }
            }
        }
        void Movement()
        {

            Vec2 nonRotatedVelocity = new Vec2(0, 0);

            if (Input.GetKey(Key.D))
            {
                nonRotatedVelocity.x += speed;
                facing = Facing.LEFT;
            }

            if (Input.GetKey(Key.A))
            {
                nonRotatedVelocity.x += -speed;
                facing = Facing.RIGHT;
            }
            
            if (canJump && Input.GetKeyDown(Key.SPACE))
            {
                nonRotatedVelocity.y = -20;
                canJump = false;
                facing = Facing.JUMPING;
            }
            if (canJump && velocityRotated.y > 0)
            {
                canJump = false;
                facing = Facing.JUMPING;
                Console.WriteLine("I am falling without jumping");
            }
            if (canJump && Input.AnyKey())
            {
                facing = Facing.IDLE;
            }
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
                if (currentLevel.levelControl.rotationPlayer % 180 == 0)
                {
                    canJump = true;
                }
            }

            if (MoveUntilCollision(velocityRotated.x, 0, currentLevel.GetTiles(this)) != null)
            {
                velocityRotated.x = 0;
                if (currentLevel.levelControl.rotationPlayer % 180 == 90)
                {
                    canJump = true;
                }
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
