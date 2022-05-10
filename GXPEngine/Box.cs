using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiledMapParser;


namespace GXPEngine
{
    class Box : Sprite
    {
        int debug = 0;

        private Boolean isWalking = false;
        private Boolean isJumping = true;
        private Boolean canJump = false;
        private Boolean isRunning = false;
        //private Boolean isAttacking = false;
        private Boolean gotDamaged = false;
        //public Boolean hasKey = false;
        private bool addedRestart = false;

        //private Boolean isStanding = false;

        private int walkingSpeed = 3;

        private Vec2 velocity = new Vec2(0, 0);
        private float fallingSpeed = 0;
        private int runningSpeed = 6;
        //private float attackTimer = 0f;
        //private float takeDamageTimer = 0f;
        private float timer = 0f;
        public float xSpeed;


        //private String characterName;

        public int HP = 6;
        public int damage = 2;
        public int maxHP = 6;

        private AnimationSprite animations;

        //private Sprite attackHitBox = new Sprite("2 GraveRobber/AttackHitBox.png");

        private LevelCreation currentLevel;

        /*public HealthUI healthUI;

        public SceneManager sceneManager;

        public SFX sfx;*/

        public Box(TiledObject obj) : base(new Texture2D(16, 16))
        {
            this.collider.isTrigger = true;

            animations = new AnimationSprite("2 GraveRobber/GraveRobber_spritesheet.png", 6, 5, -1, false, false);
            //Console.WriteLine(animations.width);
            //AddChild(attackHitBox);
            AddChild(animations);
            SetOrigin(width / 2, height / 2);
            animations.SetOrigin(animations.width / 2, animations.height / 2 + 8);

            //attackHitBox.collider.isTrigger = true;
            //attackHitBox.SetOrigin(0, this.y + this.height / 2);
            //attackHitBox.alpha = 0;
            //animations.AddChild(attackHitBox);

            //healthUI = game.FindObjectOfType<HealthUI>();
        }

        public void Update()
        {
            if (currentLevel.levelControl.toRotate)
                return;

            animations.rotation = -currentLevel.levelControl.rotationPlayer;


            if (currentLevel == null)
                return;
            if (HP > 0)
            {
                HorizontalMovement();
                VerticalMovement();
                //PlayerAnimations();
                //Collisions();
            }
            else
            {
                //PlayerDeath();
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

        void HorizontalMovement()
        {
            xSpeed = 0;

            if (Input.GetKeyDown(Key.D))
            {
                isWalking = true;
                velocity.x = 3;
            }

            if (Input.GetKeyDown(Key.A))
            {

                isWalking = true;
                velocity.x = -3;
            }

            if (Input.GetKeyUp(Key.A) || Input.GetKeyUp(Key.D)) //after releasing either of them it stops running/walking
            {
                velocity.x = 0;
                isWalking = false;
                isRunning = false;
            }

            //if (velocity.y <= 15)
            //    velocity.y += 1;


            Vec2 velocityRotated = velocity;
            velocityRotated.RotateDegrees(-currentLevel.levelControl.rotationPlayer);

            //if (Input.GetKeyDown(Key.SPACE) && canJump)
            //{
            //    velocity.y = -40;
            //    isJumping = true;
            //    canJump = false;
            //}

            if (MoveUntilCollision(0, velocityRotated.y, currentLevel.GetTiles(this)) != null)
            {
                velocityRotated.y = 0;
                isJumping = false;
                canJump = true;
            }
            if (velocity.y > 0)
            {
                isJumping = true;
                canJump = false;
            }

            MoveUntilCollision(velocityRotated.x, 0, currentLevel.GetTiles(this));
            //this.x += xSpeed;
        }

        void VerticalMovement()
        {
            Vec2 velocityRotated = velocity;
            velocityRotated.RotateDegrees(-currentLevel.levelControl.rotationPlayer);

            if (velocity.y <= 15)
                velocity.y += 0.1f;

            if (Input.GetKeyDown(Key.SPACE) && canJump)
            {
                velocityRotated.y = -40;
                isJumping = true;
                canJump = false;
            }

            if (MoveUntilCollision(0, velocityRotated.y, currentLevel.GetTiles(this)) != null)
            {
                velocityRotated.y = 0;
                isJumping = false;
                canJump = true;
            }
            if (velocity.y > 0)
            {
                isJumping = true;
                canJump = false;
            }

        }

       

        public void SetLevel(LevelCreation _level)
        {
            currentLevel = _level;
        }
    }


}
