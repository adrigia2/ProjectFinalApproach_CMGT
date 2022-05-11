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


        public float debug = 0;
        private float timer = 0f;
        Vec2 velocityRotated = new Vec2(0, 0);


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
        }

        public void Update()
        {

           // rotation = -currentLevel.levelControl.rotationPlayer;

            if (currentLevel == null)
                return;
            if (HP > 0)
            {
                Movement();
                CheckCollisionObject();
            }
            else
            {
                //PlayerDeath();
            }
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
                        currentLevel.levelControl.LoadLevel("TestMap2");
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

            List<GameObject> elementToCheck=new List<GameObject>();
            elementToCheck.AddRange(currentLevel.GetTiles(this));
            elementToCheck.Add(currentLevel.connect.door);

            Console.WriteLine(CheckCollisionDoor().Count);

            if (MoveUntilCollision(0, velocityRotated.y, elementToCheck) != null /*|| MoveUntilCollision(0, velocityRotated.y, new List<GameObject>() { currentLevel.connect.door })!=null*/)
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

            if(MoveUntilCollision(velocityRotated.x, 0, elementToCheck)!=null)

            //MoveUntilCollision(velocityRotated.x, 0, CheckCollisionDoor());

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
