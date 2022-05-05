using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiledMapParser;

namespace GXPEngine
{
    class Items : AnimationSprite
    {
        Player player;

        public int addHp;
        public int addDamage;
        private int increaseHP = 2;

        private int itemNumber;
        public Items(int itemNr) : base("Items/items.png", 13, 15, -1, false, true)
        {
            //Console.WriteLine("item");
            this.SetCycle(itemNr, 1);
            this.collider.isTrigger = true;
            AddStats(itemNr);
            itemNumber = itemNr;
            //Console.WriteLine(player);
        }

        public Items(TiledObject obj) : base("Items/items.png", 13, 15, -1, false, true)
        {
            this.SetCycle(obj.GetIntProperty("ItemNumber"), 1);
            this.collider.isTrigger = true;
            AddStats(obj.GetIntProperty("ItemNumber"));
            itemNumber = obj.GetIntProperty("ItemNumber");
            //Console.WriteLine(player);
        }

        private void AddStats(int itemNr)
        {
            if (itemNr > 85 && itemNr < 117)
                addDamage = 1;

            if (itemNr > 6 && itemNr < 13)
            {
                addHp = 1;
            }
        }

        public void PickUp()
        {
            if(itemNumber == 28)
            {
                player.hasKey = true;
            }

            if(itemNumber > 85 && itemNumber < 117)
            {
                player.damage += addDamage;
            }

            if(itemNumber > 6 && itemNumber < 13)
            {
                player.HP += addHp;
                if(player.HP > player.maxHP)
                {
                    player.HP = player.maxHP;
                }
                player.healthUI.UpdateHealth();
            }

            if (itemNumber > 19 && itemNumber < 26)
            {
                player.maxHP += increaseHP;
                player.HP += increaseHP;
                player.healthUI.UpdateHealth();

                Console.WriteLine(player.HP);
            }
            this.LateDestroy();
        }

        public void AddPlayer(Player p)
        {
            player = p;
        }
    }
}
