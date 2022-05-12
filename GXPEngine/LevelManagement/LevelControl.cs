using GXPEngine.Core;
using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class LevelControl : Sprite
    {
        int state = -1;
        public bool toRotate = false;
        float start, end;
        int timeMil = 500;

        Sound turnNoise = new Sound("Sounds/boing.mp3", false, false);

        LevelCreation level = new LevelCreation();
        Camera camera;

        Sprite background = new Sprite("Backgrounds/BackgroundwoutShip.png");

        public string levelName = "Level4";

        public float rotationPlayer = 0;
        public LevelControl(float _width, float _height) : base(new Texture2D((int)_width, (int)_height))
        {
            this.collider.isTrigger = false;


            LoadLevel(levelName);
            level.SetXY(-480, -480);
            level.SetLevelControl(this);

            background.SetXY(0, 0);
            background.SetOrigin(background.width / 2, background.height / 2);
            AddChild(background);

            AddChild(level);

            //setting up the camera was easy, i just moved the level control to 0,0 in MyGame
            camera = new Camera(0, 0, 1920, 1080);
            game.AddChild(camera);

            this.SetOrigin(this.width / 2, this.height / 2);
        }

        void Update()
        {

            Lerp();

            //if (level.player.canJump)
            //{
                if (!toRotate && Input.GetKeyDown(Key.RIGHT))
                {
                    start = -rotationPlayer;
                    rotationPlayer -= 90f;
                    end = -rotationPlayer;
                    toRotate = true;
                }
                if (!toRotate && Input.GetKeyDown(Key.LEFT))
                {
                    start = -rotationPlayer;
                    rotationPlayer += 90f;
                    end = -rotationPlayer;
                    toRotate = true;
                }

                if (!toRotate && camera.rotation % 90 != 0)
                {
                    camera.rotation = (int)(camera.rotation / 90) * 90;
                }
            //}

            background.rotation = camera.rotation;
        }

        public float getCameraRotation()
        {
            return camera.rotation;
        }

        void Lerp()
        {
            if (!toRotate)
            {
                state = 0;
                return;
            }


            state += Time.deltaTime;
            float change = end - start;
            camera.rotation = start + change / timeMil * state;

            
            Console.WriteLine("background: " + background.rotation);
            Console.WriteLine("------------------------");
            Console.WriteLine("camera: " + camera.rotation);


            if (state > timeMil)
            {
                camera.rotation = end;
                toRotate = false;
            }
            //Console.WriteLine("start: "+start +" end: "+end);
        }

        public void LoadLevel(string currentSceneName)
        {
            RemoveAllChildren();
            level = new LevelCreation();
            level.SetLevelControl(this);
            level.SetXY(-480, -480);
            level.CreateLevel(currentSceneName);
            this.levelName = currentSceneName;
            AddChild(level);
            if (camera != null)
            {
                camera.rotation = 0;
            }
            if (background != null)
            {
                background.rotation = 0;
            }
        }

        private void RemoveAllChildren()
        {
            List<GameObject> children = this.GetChildren();
            foreach (GameObject child in children)
            {
                if (child != camera && child != background)
                    child.Remove();
            }

            rotationPlayer = 0;
        }
    }

}
