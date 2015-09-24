using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public class Potion
    {
        public int type;
        public int number;
        public int heal;
        public int price;

        public string name, description;

        public Potion(int t)
        {
            type = t;

            if (t <= 5) price = 300 * t;
            else price = 100;

            if(t == 1)
            {
                name = "Potion";
                description = "A spray-type medicine for treating wounds. It can be used to restore 20 HP to an injured Pokémon.";
                heal = 20;
            }
            if(t == 2)
            {
                name = "Super Potion";
                description = "A spray-type medicine for treating wounds. It can be used to restore 50 HP to an injured Pokémon.";
                heal = 50;
            }
            if (t == 3)
            {
                name = "Hyper Potion";
                description = "A spray-type medicine for treating wounds. It can be used to restore 200 HP to an injured Pokémon.";
                heal = 200;
            }
            if (t == 4)
            {
                name = "Max Potion";
                description = "A spray-type medicine for treating wounds. It will completely restore the max HP of a single Pokémon.";
                heal = 10000;
            }
            if(t == 5)
            {
                name = "Max Elixir";
                description = "This medicine can fully restore the PP of all of the moves that have been learned by a Pokémon.";
            }
            if(t == 6)
            {
                name = "Ice Heal";
                description = "A spray-type medicine for freezing. It can be used once to defrost a Pokémon that has been frozen solid.";
            }
            if(t == 7)
            {
                name = "Paralyze Heal";
                description = "A spray-type medicine for paralysis. It can be used once to free a Pokémon that has been paralyzed.";
            }
            if(t == 8)
            {
                name = "Awakening";
                description = "A spray-type medicine used against sleep. It can be used once to rouse a Pokémon from the clutches of sleep.";
            }
            if(t == 9)
            {
                name = "Antidote";
                description = "A spray-type medicine for poisoning. It can be used once to lift the effects of being poisoned from a Pokémon.";
            }
            if(t == 10)
            {
                name = "Burn Heal";
                description = "A spray-type medicine for treating burns. It can be used once to heal a Pokémon suffering from a burn.";            
            }
        }

        public void UsePotion(Pokemons poke)
        {
            // update the numbers of potions
            number -= 1;
            if (number == 0)
                FormGame.player.potions.Remove(this);

            // heal
            if (type <= 4)
            {
                poke.hp += heal;
                if (poke.hp > poke.hpmax)
                    poke.hp = poke.hpmax;
            }
            else
            {
                if (type == 5)
                {
                    for (int i = 0; i < poke.moves.Count; ++i)
                    {
                        poke.moves[i].PP = poke.moves[i].PPmax;
                    }
                }
                if (type == 6) poke.isFrozen = 0;
                if (type == 7) poke.isParalyzed = 0;
                if (type == 8) poke.isAsleep = 0;
                if (type == 9) poke.isPoisoned = 0;
                if (type == 10) poke.isBurnt = 0;
            }
        }
    }
}
