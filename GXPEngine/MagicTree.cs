using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiledMapParser;

namespace GXPEngine
{
    class MagicTree : AnimationSprite
    {
        Player player;
        Random random = new Random();
        Items item;
        public MagicTree(String name, int rows, int cols, TiledObject obj) : base(name, rows, cols, -1, true)
        {
            this.collider.isTrigger = true;
            
        }

        public void MakeWish()
        {
            player.maxHP -= 2;
            if (player.HP > player.maxHP)
            {
                player.HP = player.maxHP;
            }

            player.healthUI.UpdateHealth();

            
            int copy = random.Next(1, 5);
            if (copy == 1)
            {
                item = new Items(28);
            }
            else if (copy == 2)
            {
                item = new Items(random.Next(86, 117));
            }
            else if (copy == 3)
            {
                item = new Items(random.Next(7, 13));
            }
            else
            {
                item = new Items(random.Next(20, 26));
            }
            item.AddPlayer(this.player);
            item.SetXY(this.x, this.y - this.height/2);
            parent.AddChild(item);
        }

        public void AddPlayer(Player p)
        {
            this.player = p;
        }
    }
}
