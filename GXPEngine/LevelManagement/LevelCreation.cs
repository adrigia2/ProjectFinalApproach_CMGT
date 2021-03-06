using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiledMapParser;

namespace GXPEngine
{
    public class LevelCreation : GameObject
    {
        public Player player = null;

        //Map level;
        Sound backgroundAmbient = new Sound("Sounds/ambientBackground.mp3", true ,false);

        public LevelControl levelControl;
        
        //list of boxes for the collider to be easier i guess
        List<RadioactiveBox> radioactiveBoxes = new List<RadioactiveBox>();

        public ConnectionDoorButton connect = new ConnectionDoorButton(); 

        TiledLoader loader;
        //public String levelName = "TestMap4";

        GameObject[,] gameObjects;
        private List<GameObject> surroundingTiles = new List<GameObject>();
        private Map map;

        private SpriteBatch fillingTiles = new SpriteBatch();
        public LevelCreation()
        {

        }

        public void SetLevelControl(LevelControl levelControl)
        {
            this.levelControl = levelControl;
        }


        public void CreateLevel(String levelName)
        {
            loader = new TiledLoader("Tiled/" + levelName + ".tmx");
            map = loader.map;
            loader.rootObject = this;

            backgroundAmbient.Play(volume: 0.25f);

            gameObjects = new GameObject[loader.map.Width, loader.map.Height];



            loader.OnObjectCreated += Tileloader_OnObjectCreated;

            loader.LoadImageLayers();
            loader.autoInstance = true;
            //loader.addColliders = false;
            loader.LoadObjectGroups();
            //loader.LoadObjectGroups(0);

            //Sprite batch

            Console.WriteLine(loader.NumTileLayers);

            loader.rootObject = fillingTiles;
            loader.addColliders = false;



            loader.rootObject = this;

            loader.addColliders = false;


            int childCount = game.GetChildCount();

            loader.addColliders = true;

            loader.OnTileCreated += Tileloader_OnTileCreated;

            loader.addColliders = true;

            loader.LoadTileLayers(1);
            loader.addColliders = false;

            loader.LoadTileLayers(0);
            loader.addColliders = true;

            loader.LoadTileLayers(2);

            loader.OnTileCreated -= Tileloader_OnTileCreated;

            //Console.WriteLine(gameObjects);

            fillingTiles.Freeze();
            this.AddChild(fillingTiles);

            connect.Connect();
        }

        private void Tileloader_OnTileCreated(Sprite sprite, int row, int column)
        {
            gameObjects[column, row] = sprite;


        }


        private void Tileloader_OnObjectCreated(Sprite sprite, TiledObject obj)
        {
            if (sprite is Player p)
            {
                player = p;
                p.SetLevel(this);
            }
            if (sprite is RadioactiveBox box)
            {
                box.SetLevel(this);
                radioactiveBoxes.Add(box);
            }
            if (sprite is DoorButton button)
            {
                connect.buttons.Add(button);
            }
            if (sprite is Door door)
            {
                connect.doors.Add(door);
            }

            if (sprite is Button buttonMenu)
            {
                buttonMenu.levelControl = this.levelControl;
            }
        }

        public List<GameObject> GetTiles(Sprite sprite)
        {
            surroundingTiles.Clear();

            //get sprite extents and center
            Vector2[] extents = sprite.GetExtents();


            extents[0] = InverseTransformPoint(extents[0].x, extents[0].y);
            extents[2] = InverseTransformPoint(extents[2].x, extents[2].y);


            //Console.WriteLine(extents[0]);
            //Console.WriteLine(extents[2]);
            
            int tileSize = map.TileWidth;


            Vector2 centerPointIndex = new Vector2((int)((extents[0].x + extents[2].x) / (2 * tileSize)), (int)((extents[0].y + extents[2].y) / (2 * tileSize)));
            Vector2 topLeft = new Vector2(centerPointIndex.x - 1, centerPointIndex.y - 1);

            topLeft.x = Mathf.Clamp(topLeft.x, 0, map.Width - 1);
            topLeft.y = Mathf.Clamp(topLeft.y, 0, map.Height - 1);

            Vector2 bottomRight = new Vector2(centerPointIndex.x + 1, centerPointIndex.y + 1);
            bottomRight.x = Mathf.Clamp(bottomRight.x, 0, map.Width - 1);
            bottomRight.y = Mathf.Clamp(bottomRight.y, 0, map.Height - 1);

            try
            {
                for (int i = (int)topLeft.x - 1; i <= bottomRight.x + 1; i++)
                {
                    for (int j = (int)topLeft.y - 1; j <= bottomRight.y + 1; j++)
                    {
                        if (gameObjects[i, j] != null) surroundingTiles.Add(gameObjects[i, j]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            foreach (GameObject door in connect.doors)
            {
                surroundingTiles.Add(door);
            }

            if(sprite is RadioactiveBox box)
            {
                foreach(RadioactiveBox radioactiveBox in radioactiveBoxes)
                {
                    surroundingTiles.Add(radioactiveBox);
                }
            }
            /*foreach (GameObject door in connect.doors)
            {
                surroundingTiles.Add(door);
            }*/

            /*surroundingTiles.AddRange(radioactiveBoxes);*/

            //Console.WriteLine(player); //wtf is this?
            /*surroundingTiles.Add(player);*/

            //Gizmos.SetColor(0, 1, 0, 1);
            //Gizmos.DrawRectangle(centerPointIndex.x, centerPointIndex.y, Mathf.Abs(topLeft.x - bottomRight.x), Mathf.Abs(topLeft.y - bottomRight.y), this);

            /* Gizmos.SetColor(0, 1, 0, 1);
             Gizmos.DrawRectangle(centerPointIndex.x * tileSize + tileSize / 2, centerPointIndex.y * tileSize + tileSize / 2, tileSize, tileSize, this);*/

            //Gizmos.SetColor(1, 0, 0, 1);
            //Gizmos.DrawRectangle(centerPointIndex.x * tileSize + tileSize / 2, centerPointIndex.y * tileSize + tileSize / 2, tileSize * 3, tileSize * 3, this);
            //System.Console.WriteLine(topLeft + " / " + centerPointIndex + " / " + bottomRight + "/" + surroundingTiles.Count);
            return surroundingTiles;
        }

    }
}

