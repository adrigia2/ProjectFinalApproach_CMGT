using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiledMapParser;

namespace GXPEngine.Scenes
{
    class MainMenu : GameObject
    {
        //GXPEngine.Button button;
        TiledLoader loader;
        public MainMenu() : base()
        {
            //Console.WriteLine("created");
        }

        public void CreateLevel(String menuName)
        {
            //Console.WriteLine("created");
            loader = new TiledLoader("Tiled/" + menuName + ".tmx");
            loader.rootObject = this;
            loader.autoInstance = true;
            loader.LoadImageLayers(0);
            loader.addColliders = true;
            //loader.LoadTileLayers(0);
            loader.LoadObjectGroups();

            //button = FindObjectOfType<Button>();
            //player = FindObjectOfType<Player>();
            //Console.WriteLine(player);
            // Console.WriteLine(player.x + "/" +player.y);
        }
    }
}
