using GXPEngine.Core;
using GXPEngine.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiledMapParser;

namespace GXPEngine
{
    class Enemy : Sprite
    {
        private Player p;

        String enemyFileName;
        private float fallingSpeed = 0;

        public Boolean gotDamaged = false;
        public float damagedTimer = 0f;

        private int HP;
        private int xSpeed = 1;

        public int damage;

        Items itemDropped;

        AnimationSprite enemyAnimation;
        private Level currentLevel;

        public Enemy(TiledObject obj) : base("Enemies/Debug.png")
        {
            SetOrigin(width / 2, height / 2);
            this.alpha = 0;

            this.collider.isTrigger = true;
            enemyFileName = obj.GetStringProperty("EnemyFileName");
            //Console.WriteLine(enemyFileName);

            damage = obj.GetIntProperty("Damage");
            HP = obj.GetIntProperty("HP");

            enemyAnimation = new AnimationSprite("Enemies/" + enemyFileName, 5, 1, -1, false, false);
            enemyAnimation.SetOrigin(this.x + this.width / 2, this.y + this.width / 2 + 3);
            AddChild(enemyAnimation);
            //Console.WriteLine(parent.FindObjectsOfType<Waypoint>());

            itemDropped = new Items(obj.GetIntProperty("ItemDropped"));
            
            
        }

        void Update()
        {
            enemyAnimation.SetCycle(1, 4);
            enemyAnimation.Animate(0.1f);
            VerticalMovement();
            if (!gotDamaged)
            {
                HorizontalMovement();
            }
            else
            {
                ResumeMovement();
            }

            if( HP <= 0 )
            DestroyEnemy();
        }

        void VerticalMovement()
        {

            if (MoveUntilCollision(0, fallingSpeed, currentLevel.GetTiles(this)) != null)
            {
                fallingSpeed = 0;
            }
            else
            {
                fallingSpeed += 1;
            }
        }

        void HorizontalMovement()
        {
            
            if(this.y - p.y < 50 && (this.x - p.x <= 150 && this.x - p.x >= -150))
            {
                if (this.x - p.x > 0)
                {
                    xSpeed = -1;
                }
                else if (this.x - p.x < 0)
                {
                    xSpeed = 1;
                }

                MoveUntilCollision(xSpeed, 0, currentLevel.waypoints);

            }else
            {
                if(MoveUntilCollision(xSpeed, 0, currentLevel.waypoints) != null)
                {
                    xSpeed *= -1;
                }
            }
                if (xSpeed < 0)
                {
                    enemyAnimation.Mirror(true, false);
                }
                else
                {
                    enemyAnimation.Mirror(false, false);
                }
        }

        void DestroyEnemy()
        {   
                itemDropped.AddPlayer(this.p);
                parent.AddChild(itemDropped);
                itemDropped.SetXY(this.x, this.y);
                this.LateDestroy();
        }

        public void LowerHP(int damage)
        {
            HP -= damage;
        }

        void ResumeMovement()
        {
            enemyAnimation.SetColor(Mathf.Sin(Time.time / 100.0f), Mathf.Sin(Time.time / 100.0f), Mathf.Sin(Time.time / 100.0f));
            if (Time.time - damagedTimer > 1000)
            {
                gotDamaged = false;
                enemyAnimation.SetColor(1, 1, 1);
                //Console.WriteLine(Time.time - damagedTimer);
            }
        }

        public void SetLevel(Level _level)
        {
            currentLevel = _level;
            p = _level.FindObjectOfType<Player>();
        }
    }


}

