using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiledMapParser;

namespace GXPEngine
{
    class Button : Sprite
    {
        //GameObject myGame;
        String levelName;
        Scenes.SceneManager sceneManager;

        private bool isSettings;
        private string settingName;
        private int increasingValue;

        public Button(TiledObject obj) : base("Buttons/" + obj.GetStringProperty("SpriteName"))
        {
            levelName = obj.GetStringProperty("Load"); // for loading the scenes, if null no problem

            isSettings = obj.GetBoolProperty("isSettings"); //for the settings buttons
            settingName = obj.GetStringProperty("settingName"); //it will determine the setting it modifies   ( or quit the game in case of exit )
            increasingValue = obj.GetIntProperty("increasingValue"); // as I will have only sfx settings, is used to increase/decrease volume
            
            //myGame = game.FindObjectOfType<MyGame>();
            sceneManager = game.FindObjectOfType<Scenes.SceneManager>();
            //Console.WriteLine(myGame);

            /*if(isSettings)
            {
                sceneManager.AddTextInSettings();
            }*/
        }

        public Button(String _levelName, String buttonSpriteName) : base("Buttons/" + buttonSpriteName)
        {
            levelName = _levelName;
            //myGame = game.FindObjectOfType<MyGame>();
            sceneManager = game.FindObjectOfType<Scenes.SceneManager>();
            //Console.WriteLine(myGame);
        }

        void Update()
        {
            if (this != null)
            {
                if (this.HitTestPoint(Input.mouseX, Input.mouseY))
                {

                    this.SetColor(1, 1, 1);
                    if (!isSettings)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {

                            if (settingName == "Exit")
                            {
                                game.Destroy();
                            }
                            //Console.WriteLine("pressed");
                            //parent.parent.parent.RemoveChild(parent.parent); //removes the scene manager from MyGame, I can't believe it actually works
                            //Button > MainMenu > SceneManager > MyGame   //why the fuck do I need this?!
                            sceneManager.LoadLevel(levelName);
                            //myGame.AddChild(sceneManager);
                        }
                    }
                    else
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            changeVolume();
                        }
                    }
                }
                else
                {
                    this.SetColor(0.8f, 0.8f, 0.8f);
                }
            }
        }

        private void changeVolume()
        {
            if(settingName == "Music")
            {
                if (sceneManager.sfx.musicVolume <= 10 && sceneManager.sfx.musicVolume >= 0)
                {
                    sceneManager.sfx.musicVolume += this.increasingValue;
                    if(sceneManager.sfx.musicVolume < 0 || sceneManager.sfx.musicVolume > 10)
                        sceneManager.sfx.musicVolume = Mathf.Clamp(sceneManager.sfx.musicVolume, 0, 10);

                }
            }
            else
            {
                if (sceneManager.sfx.effectVolume <= 10 && sceneManager.sfx.effectVolume > 0)
                {
                    sceneManager.sfx.effectVolume += this.increasingValue;
                    if (sceneManager.sfx.effectVolume < 0 || sceneManager.sfx.effectVolume > 10)
                        sceneManager.sfx.effectVolume = Mathf.Clamp(sceneManager.sfx.effectVolume, 0, 10);
                }
                
            }
            sceneManager.ModifySoundText();
            sceneManager.sfx.SetVolume();
        }
    }
}
