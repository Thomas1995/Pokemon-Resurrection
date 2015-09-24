using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Pokemon
{
    public class Attack
    {
        public int index;

        public string name;
        public string description;

        public int PP, PPmax;
        public int damage, accuracy;
        public int type;
        public int category; // 1 physical 2 special 3 status

        public bool isHM = false;

        public Color color = Color.BurlyWood;

        public Attack(int id)
        {
            if (id == 15 || id == 57) isHM = true;

            index = id;
            
            List<string> ret = DataBase.GetAttack(id);
            name = ret[0];
            description = ret[1];

            PPmax = Convert.ToInt32(ret[2]);
            PP = PPmax;
            
            damage = Convert.ToInt32(ret[3]);
            accuracy = Convert.ToInt32(ret[4]);

            type = Convert.ToInt32(ret[5]);
            category = Convert.ToInt32(ret[6]);

            switch(type)
            {
                case 1: color = Color.Tan; break; // normal
                case 2: color = Color.OrangeRed; break; // fire
                case 3: color = Color.Blue; break; // water
                case 4: color = Color.LightGoldenrodYellow; break; // electricity
                case 5: color = Color.ForestGreen; break; // grass
                case 6: color = Color.Turquoise; break; // ice
                case 7: color = Color.DarkRed; break; // fight
                case 8: color = Color.Purple; break; // poison
                case 9: color = Color.SandyBrown; break; // ground
                case 10: color = Color.MediumPurple; break; // flying
                case 11: color = Color.LightPink; break; // psychic
                case 12: color = Color.GreenYellow; break; // bug
                case 13: color = Color.SaddleBrown; break; // rock
                case 14: color = Color.Indigo; break; // ghost
                case 15: color = Color.BlueViolet; break; // dragon
                case 16: color = Color.DarkSlateGray; break; // dark
                case 17: color = Color.LightGray; break; // steel
            }
        }
    }
}
