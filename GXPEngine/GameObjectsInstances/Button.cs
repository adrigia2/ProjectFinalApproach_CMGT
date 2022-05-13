using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiledMapParser;

namespace GXPEngine
{
    public class Button : AnimationSprite
    {
        //GameObject myGame;
        String levelName;

        public LevelControl levelControl;
        public Button(string name, int rows, int cols, TiledObject obj) : base(name, rows, cols, -1, true)
        {
            levelName = obj.GetStringProperty("Load"); // for loading the scenes, if null no problem
            Console.WriteLine(x);
            /*this.SetOrigin(this.width / 2, this.height / 2);
            */
            Console.WriteLine(base.x);
        }

        void Update()
        {
            if (this != null)
            {
                //Console.WriteLine("here");
                if (this.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    //Console.WriteLine("here");
                    this.SetColor(1, 1, 1);
                    if (Input.GetMouseButtonDown(0))
                    {
                        Console.WriteLine("pressed");
                        if (levelName == "Exit")
                        {
                            game.Destroy();
                        }

                        if (levelName == "Play")
                        {
                            levelControl.LoadLevel("Level_1");
                        }
                    }
                }
                else
                {
                    this.SetColor(0.8f, 0.8f, 0.8f);
                }
            }
        }
    }
}
