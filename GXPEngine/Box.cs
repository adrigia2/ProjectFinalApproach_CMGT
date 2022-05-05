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

        private Boolean isWalking = false;
        private Boolean isJumping = true;
        private Boolean canJump = false;
        private Boolean isRunning = false;
        private Boolean isAttacking = false;
        private Boolean gotDamaged = false;
        public Boolean hasKey = false;
        private bool addedRestart = false;

        //private Boolean isStanding = false;

        private int walkingSpeed = 3;

        private Vec2 velocity = new Vec2(0, 0);
        private float fallingSpeed = 0;
        private int runningSpeed = 6;
        private float attackTimer = 0f;
        private float takeDamageTimer = 0f;
        private float timer = 0f;
        public float xSpeed;


        //private String characterName;

        public int HP = 6;
        public int damage = 2;
        public int maxHP = 6;

        private AnimationSprite animations;

        private Sprite attackHitBox = new Sprite("2 GraveRobber/AttackHitBox.png");

        private LevelCreation currentLevel;

        /*public HealthUI healthUI;

        public SceneManager sceneManager;

        public SFX sfx;*/
        
        public Box(TiledObject obj) : base(new Texture2D(16,16))
        {
            this.collider.isTrigger = true;

            animations = new AnimationSprite("2 GraveRobber/GraveRobber_spritesheet.png", 6, 5, -1, false, false);
            //Console.WriteLine(animations.width);
            AddChild(animations);
            SetOrigin(width / 2, height / 2);
            animations.SetOrigin(animations.width / 2, animations.height / 2 + 8);

            attackHitBox.collider.isTrigger = true;
            attackHitBox.SetOrigin(0, this.y + this.height / 2);
            attackHitBox.alpha = 0;
            animations.AddChild(attackHitBox);


            //healthUI = game.FindObjectOfType<HealthUI>();
        }

        public void Update()
        {
            if (currentLevel == null)
                return;
            if (HP > 0)
            {
                HorizontalMovement();
                VerticalMovement();
                //Attack();
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
            // Needs refactoring!!! done :D
            if (!isAttacking)
            {
                

                if (Input.GetKey(Key.D))
                {
                    if (Input.GetKey(Key.LEFT_SHIFT))
                    {
                        isRunning = true;
                        isWalking = false;
                    }

                    if (isRunning)
                    {
                        xSpeed += runningSpeed;
                    }
                    else
                    {
                        isWalking = true;
                        xSpeed += walkingSpeed;
                    } 
                    animations.Mirror(false, false);
                    if (attackHitBox.x < 0)
                        attackHitBox.x = attackHitBox.x + attackHitBox.width;
                }

                if (Input.GetKey(Key.A))
                {
                    if (Input.GetKey(Key.LEFT_SHIFT))
                    {
                        isRunning = true;
                        isWalking = false;
                    }

                    if (isRunning)
                    {
                        xSpeed -= runningSpeed;
                    }
                    else
                    {
                        isWalking = true;
                        xSpeed -= walkingSpeed;
                    }
                    animations.Mirror(true, false);
                    if (attackHitBox.x > -attackHitBox.width)
                        attackHitBox.x = attackHitBox.x - attackHitBox.width;
                }
            }

            if (Input.GetKeyUp(Key.A) || Input.GetKeyUp(Key.D)) //after releasing either of them it stops running/walking
            {
                isWalking = false;
                isRunning = false;
            }
            
            MoveUntilCollision(xSpeed, 0, currentLevel.GetTiles(this));
            //this.x += xSpeed;
        }

        void VerticalMovement()
        {
            if(velocity.y <= 15)
            velocity.y += 1;
            
            if (Input.GetKeyDown(Key.SPACE) && canJump)
            {
                velocity.y = -15;
                isJumping = true;
                canJump = false;
            }

            if (MoveUntilCollision(0, velocity.y, currentLevel.GetTiles(this)) != null)
            {
                velocity.y = 0;
                isJumping = false;
                canJump = true;
            }
            if (velocity.y > 0)
            {
                isJumping = true;
                canJump = false;
            }
            
        }

        /*void Attack()
        {
            if (Input.GetMouseButtonDown(0) && Time.time - attackTimer > 1000)
            {
                isAttacking = true;
                attackTimer = Time.time;
                //sfx.PlaySwordWoosh();
            }
            else if (Time.time - attackTimer > 1000)
            {
                isAttacking = false;
            }
        }*/

       /* private void DamageEnemy()
        {
            GameObject[] objects = attackHitBox.GetCollisions(true, false);
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] is Enemy enemy)
                {
                    //Enemy enemy = objects[i].FindObjectOfType<Enemy>();
                    if (!enemy.gotDamaged)
                    {
                        sfx.PlaySwordHit();
                        enemy.LowerHP(damage);
                        enemy.gotDamaged = true;
                        enemy.damagedTimer = Time.time;
                    }
                }
            }
        }*/

        /*void Collisions()
        {*/
            /*GameObject[] objects = this.GetCollisions(true, false);
            for (int i = 0; i < objects.Length; i++)
            {
                if(objects[i] is Enemy e && !gotDamaged) 
                {
                    HP = HP - e.damage;
                    gotDamaged = true;
                    takeDamageTimer = Time.time;
                    //Console.WriteLine(takeDamageTimer);
                    healthUI.UpdateHealth();
                }

                if(objects[i] is Spike s && !gotDamaged)
                {
                    HP = HP - s.damage;
                    gotDamaged = true;
                    takeDamageTimer = Time.time;
                    healthUI.UpdateHealth();
                }

                if(objects[i] is Items item)
                {
                    item.PickUp();
                }
                //Console.WriteLine(HP);

                if (objects[i] is Gate g)
                {
                    if (Input.GetKeyDown(Key.E) && hasKey)
                    {
                        g.OpenThePortal();
                        hasKey = false;
                        timer = Time.time;
                    }

                    if (g.portalopened && Input.GetKeyDown(Key.E) && Time.time - timer > 1000)
                    {
                        g.FinishLevel();
                    }

                }

                if(objects[i] is MagicTree mt)
                {
                    if(Input.GetKeyDown(Key.E) && this.maxHP > 2)
                    {
                        mt.MakeWish();
                    }
                }
            }

            if(gotDamaged == true && Time.time - takeDamageTimer < 1000)
            {
                animations.SetColor(Mathf.Sin(Time.time / 100.0f), 0, 0);
                
            }
            else
            {
                gotDamaged = false;
                animations.SetColor(1, 1, 1);
            }
        }*/
        
        public void SetLevel(LevelCreation _level)
        {
            currentLevel = _level;
        }
    }


}
