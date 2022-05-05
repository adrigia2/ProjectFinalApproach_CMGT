using GXPEngine.Core;
using GXPEngine.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiledMapParser;

namespace GXPEngine
{
    class Gate : Sprite
    {
        AnimationSprite closedPortal;
        AnimationSprite openPortal;

        Sprite SpaceShip;

        Player player;
        SceneManager sceneManager;
        public Boolean portalopened = false;

        private bool isSpaceShip;

        public Gate(TiledObject obj) : base(new Texture2D(32,32))
        {
            isSpaceShip = obj.GetBoolProperty("isSpaceShip");

            this.collider.isTrigger = true;
            if (!isSpaceShip)
            {
                closedPortal = new AnimationSprite("Terrain/portalRings1.png", 4, 5, -1, false, false);
                closedPortal.SetXY(-this.width / 2, -this.height / 2);
                closedPortal.SetCycle(0, 17);
                //closedPortal.Animate();
                //closedPortal.collider.isTrigger = true;
                this.AddChild(closedPortal);

                openPortal = new AnimationSprite("Terrain/portalRings2.png", 5, 1, -1, false, false);
                openPortal.SetXY(-this.width / 2, -this.height / 2);
            }
            else
            {
                SpaceShip = new Sprite("Terrain/player_ship.png");
                SpaceShip.SetXY(-this.width / 2, -this.height / 2);
                this.AddChild(SpaceShip);
            }

            sceneManager = game.FindObjectOfType<SceneManager>();
            //Console.WriteLine(sceneManager == null);
        }

        private void Update()
        {
            if(!isSpaceShip)
            if (!portalopened)
                closedPortal.Animate(0.2f);
            else
                openPortal.Animate(0.2f);
        }
        public void AddPlayer(Player p)
        {
            player = p;
        }

        public void OpenThePortal()
        {
            if (!isSpaceShip)
            {
                this.RemoveChild(closedPortal);
                this.AddChild(openPortal);
            }
                portalopened = true;
        }

        public void FinishLevel()
        {
            sceneManager.LoadLevel("SelectingMenu");
        }
    }
}
