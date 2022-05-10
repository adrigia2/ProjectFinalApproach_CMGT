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
            if (Input.GetKeyDown(Key.RIGHT))
            {
                rotationPlayer -= 90f;
                camera.rotation = -rotationPlayer;
            }
            if (Input.GetKeyDown(Key.LEFT))
            {
                rotationPlayer += 90f;
                camera.rotation = -rotationPlayer;
            }

            //Console.WriteLine(rotationPlayer);
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
