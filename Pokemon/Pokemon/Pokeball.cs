using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Pokemon
{
    public class Pokeball
    {
        public int type;
        public float chance;
        public int number;
        public int price;

        public Image image;

        public string name, description;

        public Pokeball(int t)
        {
            type = t;
            chance = 0.5f * (type + 1);
            price = 200 * type;

            if(t == 1)
            {
                name = "Poké Ball";
                description = "The PokéBall is the standard PokéBall you can obtain. It has a 1x Capture rate that doesn't increase the chances of capturing a Pokémon.";
                image = Resources.pokeball;
            }
            if(t == 2)
            {
                name = "Great Ball";
                description = "The Great Ball is a standard PokéBall you can obtain. It has a 1.5x Capture rate that increases the likelihood of capturing a Pokémon.";
                image = Resources.greatball;
            }
            if(t == 3)
            {
                name = "Ultra Ball";
                description = "The Ultra Ball is a standard PokéBall you can obtain. It has a 2x Capture rate that increases the likelihood of capturing a Pokémon.";
                image = Resources.ultraball;
            }
            if (t == 4)
            {
                chance = 100000;
                name = "Master Ball";
                description = "The Master Ball allows for capture of any Wild Pokémon regardless of Level, remaining HP or Capture Rate. It does not work on Pokémon owned by trainers.";
                image = Resources.masterball;
            }
        }
    }
}
