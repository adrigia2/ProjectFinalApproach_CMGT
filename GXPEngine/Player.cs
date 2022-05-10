using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiledMapParser;


namespace GXPEngine
{
    class Player : Sprite
    {

        private bool isWalking = false;
        private bool canJump = false;
        private bool addedRestart = false;

        private float timer = 0f;
        Vec2 velocityRotated = new Vec2(0, 0);


        //private String characterName;

        public int HP = 6;
        public int damage = 2;
        public int maxHP = 6;

        private AnimationSprite animations;

        private Sprite attackHitBox = new Sprite("2 GraveRobber/AttackHitBox.png");

        private LevelCreation currentLevel;


        public Player(TiledObject obj) : base(new Texture2D(16, 16))
        {
            this.collider.isTrigger = true;

            animations = new AnimationSprite("2 GraveRobber/GraveRobber_spritesheet.png", 6, 5, -1, false, false);
            //Console.WriteLine(animations.width);
            AddChild(animations);
            SetOrigin(width / 2, height / 2);
            animations.SetOrigin(animations.width / 2, animations.height / 2 + 8);
        }

        public void Update()
        {
            if (currentLevel == null)
                return;
            if (HP > 0)
            {
                Movement();
                CheckLaserCollision();
            }
            else
            {
                //PlayerDeath();
            }
        }

        private void CheckLaserCollision()
        {
            GameObject[] objects = this.GetCollisions(true, false);
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] is Laser laser)
                {
                    if(!laser.invisible)
                    {
                        currentLevel.levelControl.LoadLevel("TestMap2");
                    }
                }
            }
        }

        /*private void PlayerDeath()
        {
            if (!addedRestart)
            {
                Button restartButton = new Button(currentLevel.levelName, "Restart.png");
                restartButton.SetXY(-sceneManager.x +  game.width/4, -sceneManager.y + game.height/3);

                Console.WriteLine(restartButton.x + " " + restartButton.y);
                sceneManager.AddChild(restartButton);
                addedRestart = true;
            }
        }*/

        /*private void PlayerAnimations()
        {
            if (!isWalking && !isRunning && !isJumping && !isAttacking)
            {
                animations.SetCycle(6, 4);
                animations.Animate(0.06f);
            }
            else if (isJumping)
            {
                if (fallingSpeed < 0) // goes up and make the jump animation
                {
                    animations.SetCycle(12, 4);

                    animations.Animate(0.05f);
                }
                else // falls down animation
                {
                    animations.SetCycle(16, 1);

                    animations.Animate(0.02f);
                }


            }
            else if (isAttacking)
            {
                animations.SetCycle(0, 6);
                animations.Animate(0.1f);
                DamageEnemy();
            }
            else if (isWalking)
            {
                animations.SetCycle(24, 6);
                animations.Animate(0.15f);
            }
            else if (isRunning)
            {
                animations.SetCycle(18, 6);
                animations.Animate(0.15f);
            }
        }*/

        void Movement()
        {

            Vec2 nonRotatedVelocity = new Vec2(0, 0);

            if (Input.GetKey(Key.D))
            {
                isWalking = true;
                nonRotatedVelocity.x = 3;
            }

            if (Input.GetKey(Key.A))
            {
                isWalking = true;
                nonRotatedVelocity.x = -3;
            }

            if (Input.GetKeyUp(Key.A) || Input.GetKeyUp(Key.D)) //after releasing either of them it stops running/walking
            {
                nonRotatedVelocity.x = 0;
                isWalking = false;
            }

            if (nonRotatedVelocity.y <= 15)
                nonRotatedVelocity.y += 1;

            if (Input.GetKeyDown(Key.SPACE) && canJump)
            {
                nonRotatedVelocity.y = -80;
                //isJumping = true;
                canJump = false;
            }

            nonRotatedVelocity.RotateDegrees(-currentLevel.levelControl.rotationPlayer);
            velocityRotated = nonRotatedVelocity;


            if (MoveUntilCollision(0, velocityRotated.y, currentLevel.GetTiles(this)) != null)
            {
                velocityRotated.y = 0;
                //isJumping = false;
                canJump = true;
            }
            if (velocityRotated.y > 0)
            {
                //isJumping = true;
                canJump = false;
            }

            MoveUntilCollision(velocityRotated.x, 0, currentLevel.GetTiles(this));

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
