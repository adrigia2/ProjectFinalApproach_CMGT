using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine.Scenes
{
    class SceneManager : GameObject
    {
        private Scenes.Level level1 = new Level();
        private MainMenu mainMenu = new MainMenu();
        //public String SceneName;

        enum PossibleScenes
        {
            Menu,
            Level
        }

        PossibleScenes currentPossibleScene;

        public SFX sfx = new SFX();
        private EasyDraw easyDraw;
        
        

        HealthUI healthUi;

        Player player;

        public SceneManager()
        {
            easyDraw = new EasyDraw(game.width, game.height, false);
            easyDraw.SetXY(0, 0);
            easyDraw.TextAlign(CenterMode.Center, CenterMode.Center);
            easyDraw.Text(sfx.musicVolume.ToString(), sfx.musicVolumePoz.x, sfx.musicVolumePoz.y, true);
            easyDraw.Text(sfx.effectVolume.ToString(), sfx.effectVolumePoz.x, sfx.effectVolumePoz.y, true); //since some time ago, Hans gave me a pice of code for Text for Tiled since it wasnt working properly
        }                                                                                                   //I deleted the old one and put the new one it back then
                                                                                                            //now I've got the old one in as well and added a null parameter to make a difference between them
        void Update()
        {
            if(player != null)
            {
                this.x = -this.player.x + game.width / 2;
                this.y = -this.player.y + 2 * game.height / 3;
            }

            if (Input.GetKeyDown(Key.Q))
            {
                this.LoadLevel("SelectingMenu");
            }

            /*if(Input.GetMouseButtonDown(0))
            {
                Console.WriteLine(Input.mouseX + " " + Input.mouseY);
            }*/
        }


        public void LoadLevel(string currentSceneName)
        {

            if (!currentSceneName.Contains("Level"))
            {
                currentPossibleScene = PossibleScenes.Menu;
            }
            else
            {
                currentPossibleScene = PossibleScenes.Level;
            }

            //this.SceneName = currentSceneName;

            if (currentPossibleScene == PossibleScenes.Menu)
            {
                RemoveAllChildren();
                mainMenu = new MainMenu();
                mainMenu.CreateLevel(currentSceneName);
                AddChild(mainMenu);
                sfx.PlayMusic(false);
                //Console.WriteLine(mainMenu == null);

            }
            else
            {
                RemoveAllChildren();
                level1 = new Level();
                level1.CreateLevel(currentSceneName);
                AddChild(level1);
                level1.levelName = currentSceneName;

                sfx.PlayMusic(true);
            }

            player = this.FindObjectOfType<Player>();
            if (player != null)
            {
                healthUi = new HealthUI(player);
                parent.AddChild(healthUi);
                player.healthUI = this.healthUi;
                player.sceneManager = this;
                player.sfx = this.sfx;
            }

            if(currentSceneName == "Settings")
            {
                AddChild(easyDraw);
                Console.WriteLine("added");
            }

            this.x = 0;
            this.y = 0;
            
        }

        private void RemoveAllChildren()
        {
            List<GameObject> children = this.GetChildren();
            //Console.WriteLine(children.Count);
            foreach (GameObject child in children)
            {
                //Console.WriteLine("destroied");
                //Console.WriteLine(mainMenu == null);
                //Console.WriteLine(level1 == null);
                if(child != sfx)
                child.Remove();
            }
            
            HealthUI[] UI = game.FindObjectsOfType<HealthUI>();
            
            if(UI != null)
            foreach (HealthUI child in UI)
            { 
                child.Remove();
            }
        }

        public void ModifySoundText()
        {
            easyDraw.ClearTransparent();
            easyDraw.Text(sfx.musicVolume.ToString(), sfx.musicVolumePoz.x, sfx.musicVolumePoz.y, true);
            easyDraw.Text(sfx.effectVolume.ToString(), sfx.effectVolumePoz.x, sfx.effectVolumePoz.y, true);
        }
    }
}
