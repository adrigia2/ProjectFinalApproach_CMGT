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
        public bool toRotate=false;
        float start, end;
        int timeMil=500;


        LevelCreation level = new LevelCreation();
        Camera camera;

        public float rotationPlayer = 0;
        public LevelControl(float _width, float _height) : base("Untitled.png")
        {
            this.collider.isTrigger = false;

            LoadLevel("TestMap2");
            level.SetXY(0 - this.width / 2, 0 - this.height / 2);
            level.SetLevelControl(this);

            AddChild(level);

            //setting up the camera was easy, i just moved the level control to 0,0 in MyGame
            camera = new Camera(0, 0, 960, 960);
            game.AddChild(camera);
            
            this.SetOrigin(this.width / 2, this.height / 2);
        }

        void Update()
        {

            Lerp();

            if (Input.GetKeyDown(Key.RIGHT))
            {
                start =-rotationPlayer;
                rotationPlayer -= 90f;
                if (rotationPlayer < 0)
                    rotationPlayer += 360;
                end =- rotationPlayer;

                //camera.rotation = -rotationPlayer;
                toRotate = true;
                //camera.rotation = -rotationPlayer;
            }
            if (Input.GetKeyDown(Key.LEFT))
            {
                start = -rotationPlayer;
                rotationPlayer += 90f;
                if (rotationPlayer == 360)
                    rotationPlayer = 0;
                end= -rotationPlayer;
                toRotate = true;

                //camera.rotation = -rotationPlayer;
            }

            //Console.WriteLine(rotationPlayer);
        }

        void Lerp()
        {
            if (toRotate == false)
            {
                state = 0;
                return;
            }

            if (start == -270 && end == 0)
                end = -360;
            else
                if (start == 0 && end == -270)
                end = +90;


            state+=Time.deltaTime;
            float change = end - start;
            camera.rotation=start+change/timeMil*state;

            if (state > timeMil)
            {
                camera.rotation = end;
                toRotate= false;
            }
            Console.WriteLine("start: "+start +" end: "+end);
        }

        public void LoadLevel(string currentSceneName)
        {
            RemoveAllChildren();
            level = new LevelCreation();
            level.SetLevelControl(this);
            level.SetXY(0 - this.width / 2, 0 - this.height / 2);
            level.CreateLevel(currentSceneName);
            AddChild(level);
        }

        private void RemoveAllChildren()
        {
            List<GameObject> children = this.GetChildren();
            foreach (GameObject child in children)
            {
                if (child != camera)
                    child.Remove();
            }

            rotationPlayer = 0;
        }
    }
   
}
