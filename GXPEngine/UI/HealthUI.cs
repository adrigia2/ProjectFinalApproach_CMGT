using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class HealthUI : GameObject
    {
        private Sprite fullHeart;

        private Sprite[] hearts;
        private Pivot health = new Pivot();

        Player player;

        private int HP;

        private int maxHP;
        public HealthUI(Player p)
        {
            
            fullHeart = new Sprite("UI/HP/Player/heart_full_16x16.png");
            

            player = p;
            HP = player.HP;
            maxHP = player.maxHP;

            hearts = new Sprite[maxHP];

            for (int i = 0; i < HP / 2; i++)
            {
                hearts[i] = new Sprite("UI/HP/Player/heart_full_16x16.png");
                hearts[i].SetXY(i * fullHeart.width, 0);
                health.AddChild(hearts[i]);
                //health.AddChild(new Sprite("UI/HP/Player/heart_full_16x16.png"));
                //Console.WriteLine("added child");
            }

            

            /*List<GameObject> children = health.GetChildren();


            int j = 0;
            foreach (GameObject child in children)
            {
                child.SetXY(j * fullHeart.width, 0);
                j++;
            }*/

            this.AddChild(health);

            //Console.WriteLine(this.GetChildCount());
        }

        public void UpdateHealth()
        {
            HP = player.HP;
            maxHP = player.maxHP;
            hearts = new Sprite[maxHP];

            List<GameObject> children = health.GetChildren();


            foreach (GameObject child in children)
            {

                child.LateDestroy();

            }

            for (int i = 0; i < HP / 2; i++)
            {
                hearts[i] = new Sprite("UI/HP/Player/heart_full_16x16.png");
                health.AddChild(hearts[i]);
                hearts[i].SetXY(i * fullHeart.width, 0);
            }
            if (HP < maxHP)
            {
                if (HP % 2 == 1)
                {
                    //this.AddChildAt(new Sprite("UI/HP/Player/heart_half_16x16.png"), HP / 2);
                    hearts[HP / 2] = new Sprite("UI/HP/Player/heart_half_16x16.png");
                    hearts[HP / 2].SetXY(HP / 2 * fullHeart.width, 0);
                    health.AddChild(hearts[HP / 2]);
                }

                if ((maxHP - HP) > 1)
                {
                    if (HP % 2 == 1)
                        HP++;
                    for (int i = maxHP / 2; i > HP / 2; i--)
                    {
                        hearts[i] = new Sprite("UI/HP/Player/heart_empty_16x16.png");
                        hearts[i].SetXY((i - 1) * fullHeart.width, 0);
                        health.AddChild(hearts[i]);
                    }
                }
            }


            //Console.WriteLine(this.GetChildCount());
        }
    }
}
