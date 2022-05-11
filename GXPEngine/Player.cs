using GXPEngine.Core;
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
        private bool isWalking = false;
        private bool canJump = false;
        private bool addedRestart = false;
        float speed = 1.5f;
        Facing facing;


        public float debug = 0;
        private float timer = 0f;
        Vec2 velocityRotated = new Vec2(0, 0);
        Vec2 gravity;

        //private String characterName;

        public int HP = 6;
        public int damage = 2;
        public int maxHP = 6;

        private AnimationSprite animations;
        private Sprite playerSkin;

        private Sprite attackHitBox = new Sprite("2 GraveRobber/AttackHitBox.png");

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
            gravity = new Vec2(0, 0.5f);
        }

        public void Update()
        {
           // rotation = -currentLevel.levelControl.rotationPlayer;

            if (currentLevel == null)
                return;

                Movement();
                CheckLaserCollision();
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
                        currentLevel.levelControl.LoadLevel("TestMap3");
                        currentLevel.levelControl.setCameraRotation(0);
                    }
                }

                if (objects[i] is ButtonDoor button)
                {
                    ((ButtonDoor)button).isPressed = true;
                }
            }
        }

        private List<GameObject> CheckCollisionDoor()
        {
            var doors = new List<GameObject>();
            GameObject[] objects = this.GetCollisions(true, false);
            for (int i = 0; i < objects.Length; i++)
            {
                if(objects[i] is Door)
                doors.Add(objects[i]);
            }
            //if(doors.Count>0)
            //    Console.WriteLine("door detect "+debug++);
            return doors;
        }


        //private void CheckButtonCollision()
        //{
        //    GameObject[] objects = this.GetCollisions(true, false);
        //    for (int i = 0; i < objects.Length; i++)
        //    {

        //    }
        //}

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
                nonRotatedVelocity.x += speed;
                facing = Facing.LEFT;
            }

            if (Input.GetKey(Key.A))
            {
                isWalking = true;
                nonRotatedVelocity.x += -speed;
                facing = Facing.RIGHT;
            }

            if (Input.GetKeyUp(Key.A) || Input.GetKeyUp(Key.D)) //after releasing either of them it stops running/walking
            {
                //do you REALLY need this?
                isWalking = false;
            }
            
            if (canJump && Input.GetKeyDown(Key.SPACE))
            {
                nonRotatedVelocity.y = -10;
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
            velocityRotated.x = Mathf.Clamp(velocityRotated.x, -15, 15);
            velocityRotated.y = Mathf.Clamp(velocityRotated.y, -15, 15);
            

            List<GameObject> elementToCheck=new List<GameObject>();
            elementToCheck.AddRange(currentLevel.GetTiles(this));
            if(currentLevel.connect.door!=null)
                elementToCheck.Add(currentLevel.connect.door);

            Console.WriteLine(CheckCollisionDoor().Count);

            if (MoveUntilCollision(0, velocityRotated.y, elementToCheck) != null /*|| MoveUntilCollision(0, velocityRotated.y, new List<GameObject>() { currentLevel.connect.door })!=null*/)
            {
                velocityRotated.y = 0;
                //isJumping = false;
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
