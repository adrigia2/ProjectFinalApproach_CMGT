using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiledMapParser;


namespace GXPEngine
{
    public enum PlayerState {Moving, Idle, NULL};
    public class Player : Sprite
    {
        public bool canJump = false;
        float speed = 1f;

        Sound walkNoise = new Sound("Sounds/walking.wav", true, false);
        bool isWalking = false;
        bool soundPlaying = false;

        public float debug = 0;
        Vec2 velocityRotated = new Vec2(0, 0);
        Vec2 gravity;

        //private String characterName;

        //For reseting the level
        private LevelCreation currentLevel;

        //animations
        AnimationSprite animations = new AnimationSprite("PlayerSprites/PlayerSpriteSheet.png", 8, 3, addCollider: false);
        PlayerState playerState;

        public Player(TiledObject obj) : base(new Texture2D(32, 64))
        {
            /*Console.WriteLine("Player: ");
            Console.WriteLine("width: " + width);
            Console.WriteLine("height: " + height);*/

            playerState = PlayerState.Idle;
            this.collider.isTrigger = true;
            this.SetOrigin(width / 2, height / 2);

            animations.SetOrigin(animations.width / 2, animations.height / 2);
            animations.SetXY(0, 0);

            AddChild(animations);
            gravity = new Vec2(0, 1f);
        }

        void AnimationsControl()
        {
            switch (playerState)
            {
                case PlayerState.Moving:
                    animations.SetCycle(16, 8);
                    animations.Animate(0.1f);
                    break;

                case PlayerState.Idle:
                    animations.SetCycle(8, 4);
                    animations.Animate(0.05f);
                    break;
                case PlayerState.NULL: Console.WriteLine("facing is NULL... you are doing something horribly wrong here"); break;
            }
        }
        public void Update()
        {
            rotation = currentLevel.levelControl.getCameraRotation();

            if (currentLevel == null)
                return;


            Audio();
            if(!currentLevel.levelControl.toRotate)
            {
            Movement();
            AnimationsControl();
            }
            CheckCollisionObject();
        }

        private void CheckCollisionObject()
        {
            GameObject[] objects = this.GetCollisions(true, false);
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] is Laser laser && !laser.invisible)
                {
                    Console.WriteLine("hit a (non invisible) laser");
                    currentLevel.levelControl.LoadLevel(currentLevel.levelControl.levelName);
                }

                if (objects[i] is Boundaries)
                {
                    Console.WriteLine("hit the boundary");
                    currentLevel.levelControl.LoadLevel(currentLevel.levelControl.levelName);
                }

                if (objects[i] is RadioactiveBox)
                {
                    Console.WriteLine("hit a RadioactiveBox");
                    currentLevel.levelControl.LoadLevel(currentLevel.levelControl.levelName);
                }

                if (objects[i] is DoorButton button)
                {
                    button.isPressed = true;
                }
                if (objects[i] is NextLevelPortal portal)
                {
                    Console.WriteLine(portal.nextLevelName);
                    currentLevel.levelControl.LoadLevel(portal.nextLevelName);
                }
            }
        }

        void Audio()
        {
            isWalking = false;
            if (Input.GetKey(Key.D))
            {
                isWalking = true;
            }

            if (Input.GetKey(Key.A))
            {
                isWalking = true;
            }

            if (isWalking && !soundPlaying)
            {
                //walkNoise.Play(volume: 0.25f);
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
            if (Input.GetKey(Key.D))
            {
                nonRotatedVelocity.x += speed;
                playerState = PlayerState.Moving;
                animations.Mirror(false, false);
            }

            if (Input.GetKey(Key.A))
            {
                nonRotatedVelocity.x += -speed;
                playerState = PlayerState.Moving;
                animations.Mirror(true, false);
            }

            if (canJump && Input.GetKeyDown(Key.SPACE))
            {
                nonRotatedVelocity.y = -25;
                canJump = false;

            }
            if (canJump && velocityRotated.y > 0)
            {
                canJump = false;

                Console.WriteLine("I am falling without jumping");
            }
            if ((!Input.GetKey(Key.A) && !Input.GetKey(Key.D)) || (Input.GetKey(Key.A) && Input.GetKey(Key.D)))
            {
                playerState = PlayerState.Idle;
            }

            //nonRotatedVelocity.x += (Input.GetKey(Key.D) ? 3 : 0) - (Input.GetKey(Key.A) ? 3 : 0);
            nonRotatedVelocity += gravity;
            nonRotatedVelocity.RotateDegrees(-currentLevel.levelControl.rotationPlayer);
            velocityRotated += nonRotatedVelocity;
            velocityRotated *= 0.80f;
            velocityRotated.x = Mathf.Clamp(velocityRotated.x, -30, 15);
            velocityRotated.y = Mathf.Clamp(velocityRotated.y, -30, 15);

            //basically you get stuck in the floor sometimes, which kinda fucks you up (unless you jump (again)). prob occurs because you don't reflect the player or something. BRB
            if (MoveUntilCollision(0, velocityRotated.y, currentLevel.GetTiles(this)) != null)
            {
                velocityRotated.y = 0;
                canJump = true;

                if (currentLevel.levelControl.rotationPlayer % 180 == 0)
                {
                    canJump = true;
                }
            }

            if (MoveUntilCollision(velocityRotated.x, 0, currentLevel.GetTiles(this)) != null)
            {
                //[AnimationSprite::Tiled\TileSets/TileSheet16.png]
                velocityRotated.x = 0;
                    canJump = true;
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
