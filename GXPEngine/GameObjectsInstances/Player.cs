using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiledMapParser;


namespace GXPEngine
{
    public enum Facing {LEFT, RIGHT, JUMPING, IDLE};
    public class Player : Sprite
    {
        public bool canJump = false;
        float speed = 1f;
        Facing facing;

        Sound walkNoise = new Sound("Sounds/walking.wav", true, false);
        bool isWalking = false;
        bool soundPlaying = false;

        public float debug = 0;
        Vec2 velocityRotated = new Vec2(0, 0);
        Vec2 gravity;

        //private String characterName;

        public int HP = 6;
        public int damage = 2;
        public int maxHP = 6;

        private AnimationSprite animationsRun;
        private AnimationSprite animationsHide;
        private AnimationSprite animationsBoh;

        private LevelCreation currentLevel;


        public Player(TiledObject obj) : base(new Texture2D(300, 600))
        {
            Console.WriteLine("Player: ");
            Console.WriteLine("width: "+width);
            Console.WriteLine("height: " + height);
            this.collider.isTrigger = true;
            animationsHide = new AnimationSprite("2 GraveRobber/spritesheet2.png", 4, 1, 1, false, false);
            animationsRun = new AnimationSprite("2 GraveRobber/spritesheet3.png",8,1, 1, false, false);
            animationsBoh = new AnimationSprite("2 GraveRobber/spritesheet1.png", 3, 1, 1, false, false);

            animationsHide.SetOrigin(animationsHide.width / 2, animationsHide.height / 2);
            animationsHide.SetCycle(1, 4, 1);
            animationsRun.SetOrigin(animationsHide.width / 2, animationsHide.height / 2);
            animationsRun.SetCycle(1, 8, 1);
            animationsBoh.SetOrigin(animationsHide.width / 2, animationsHide.height / 2);
            animationsBoh.SetCycle(1, 3, 1);
            //playerSkin = new Sprite("2 GraveRobber/sam_256px.png");

            animations.SetCycle(0, 4, 255);

            //Console.WriteLine(animations.width);
            AddChild(animationsHide);
            AddChild(animationsRun);
            AddChild(animationsBoh);
            animationsHide.visible = false;
            animationsRun.visible = true;
            animationsBoh.visible = false;

            
            //AddChild(animations);
            SetOrigin(width / 2, height / 2);

            gravity = new Vec2(0, 0.45f);
        }


        public void Update()
        {

                animationsRun.AnimateFixed();

            rotation = currentLevel.levelControl.getCameraRotation();

                if (currentLevel == null)
                return;

                animations.Animate();

                Audio();
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
                    Console.WriteLine("die");
                        currentLevel.levelControl.LoadLevel(currentLevel.levelControl.levelName);
                }

                if (objects[i] is RadioactiveBox)
                {             
                        currentLevel.levelControl.LoadLevel(currentLevel.levelControl.levelName);
                }

                if (objects[i] is DoorButton button)
                {
                    button.isPressed = true;
                }

                if(objects[i] is NextLevelPortal portal)
                {
                    Console.WriteLine(portal.nextLevelName);
                    currentLevel.levelControl.LoadLevel(portal.nextLevelName);
                }
            }
        }

        void Audio()
        {
            if (Input.GetKeyDown(Key.D))
            {
                isWalking = true;
            }

            if (Input.GetKeyDown(Key.A))
            {
                isWalking = true;
            }

            if (Input.GetKeyUp(Key.D))
            {
                isWalking = false;
            }

            if (Input.GetKeyUp(Key.A))
            {
                isWalking = false;
            }

            if (isWalking == true && soundPlaying == false)
            {
                //walkNoise.Play();
                soundPlaying = true;
            }
            if (!isWalking)
            {
                //walkNoise = null;
            }

        }


        void Movement()
        {

            Vec2 nonRotatedVelocity = new Vec2(0, 0);

            if (canJump)
            {
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

            if(canJump)
            if (Input.GetKey(Key.D) || Input.GetKey(Key.A))
            {
                if(Input.GetKey(Key.A))
                    animationsRun.scaleX = -1;
                else
                    animationsRun.scaleX = 1;


                animationsBoh.visible = false;
                animationsHide.visible = false;
                animationsRun.visible = true;
            }
            else
            {
                animationsBoh.visible = false;
                animationsHide.visible = true;
                animationsRun.visible = false;
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
