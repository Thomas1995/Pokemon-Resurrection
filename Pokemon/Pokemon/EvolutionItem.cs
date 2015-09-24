using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public class EvolutionItem
    {
        public int type;
        public string name, description;
        public List<int> evolvedPokemons = new List<int>();

        public EvolutionItem(int t)
        {
            type = t;

            if(type == 1)
            {
                name = "Thunderstone";
                description = "A peculiar stone that can make certain species of Pokémon evolve (Pikachu, Eevee). It has a distinct thunderbolt pattern.";
                evolvedPokemons.AddRange(new int[] { 25, 133 });
            }
            if(type == 2)
            {
                name = "Fire Stone";
                description = "A peculiar stone that can make certain species of Pokémon evolve (Vulpix, Growlithe, Eevee). The stone has a fiery orange heart.";
                evolvedPokemons.AddRange(new int[] { 37, 58, 133 });
            }
            if (type == 3)
            {
                name = "Water Stone";
                description = "A peculiar stone that can make certain species of Pokémon evolve (Poliwhirl, Shellder, Staryu, Eevee, Lombre). It is the blue of a pool of clear water.";
                evolvedPokemons.AddRange(new int[] { 61, 90, 120, 133, 171 });
            }
            if (type == 4)
            {
                name = "Dawn Stone";
                description = "A peculiar stone that can make certain species of Pokémon evolve (Kirlia (male only), Snorunt). It sparkles like a glittering eye.";
                evolvedPokemons.AddRange(new int[] { 281, 361 });
            }
            if (type == 5)
            {
                name = "Dusk Stone";
                description = "A peculiar stone that can make certain species of Pokémon evolve (Murkrow, Misdreavus). It holds shadows as dark as can be.";
                evolvedPokemons.AddRange(new int[] { 198, 200 });
            }
            if (type == 6)
            {
                name = "Moon Stone";
                description = "A peculiar stone that can make certain species of Pokémon evolve (Nidorina, Nidorino, Clefairy, Jigglypuff, Skitty). It is as black as the night sky.";
                evolvedPokemons.AddRange(new int[] { 30, 33, 35, 39, 300 });
            }
            if (type == 7)
            {
                name = "Shiny Stone";
                description = "A peculiar stone that can make certain species of Pokémon evolve (Togetic, Roselia). It shines with a dazzling light.";
                evolvedPokemons.AddRange(new int[] { 176, 315 });
            }
            if (type == 8)
            {
                name = "Sun Stone";
                description = "A peculiar stone that can make certain species of Pokémon evolve (Gloom, Sunkern). It burns as red as the evening sun.";
                evolvedPokemons.AddRange(new int[] { 44, 191 });
            }
            if (type == 9)
            {
                name = "Leaf Stone";
                description = "A peculiar stone that can make certain species of Pokémon evolve (Gloom, Weepinbell, Exeggcute, Nuzleaf). It has an unmistakable leaf pattern.";
                evolvedPokemons.AddRange(new int[] { 44, 70, 102, 274 });
            }
            if (type == 10)
            {
                name = "King's Rock";
                description = "A peculiar item that can make certain species of Pokémon evolve (Poliwhirl, Slowpoke). It is a crown-shaped rock.";
                evolvedPokemons.AddRange(new int[] { 61, 79 });
            }
            if (type == 11)
            {
                name = "Metal Coat";
                description = "A peculiar item that can make certain species of Pokémon evolve (Onix, Scyther). It is a special metallic film.";
                evolvedPokemons.AddRange(new int[] { 95, 123 });
            }
            if (type == 12)
            {
                name = "Reaper Cloth";
                description = "A peculiar item that can make certain species of Pokémon evolve (Dusclops). It is a cloth imbued with horrifyingly strong spiritual energy.";
                evolvedPokemons.AddRange(new int[] { 356 });
            }
            if (type == 13)
            {
                name = "Electirizer";
                description = "A peculiar item that can make certain species of Pokémon evolve (Electabuzz). It is a box packed with a tremendous amount of electric energy.";
                evolvedPokemons.AddRange(new int[] { 125 });
            }
            if (type == 14)
            {
                name = "Magmarizer";
                description = "A peculiar item that can make certain species of Pokémon evolve (Magmar). It is a box packed with a tremendous amount of magma energy.";
                evolvedPokemons.AddRange(new int[] { 126 });
            }
            if (type == 15)
            {
                name = "Protector";
                description = "A peculiar item that can make certain species of Pokémon evolve (Rhydon). It is extremely stiff and heavy.";
                evolvedPokemons.AddRange(new int[] { 112 });
            }
            if (type == 16)
            {
                name = "Prism Scale";
                description = "A peculiar item that can make certain species of Pokémon evolve (Feebas). It is a mysterious scale.";
                evolvedPokemons.AddRange(new int[] { 349 });
            }
            if (type == 17)
            {
                name = "Razor Claw";
                description = "A peculiar item that can make certain species of Pokémon evolve (Sneasel). It is a sharply hooked claw.";
                evolvedPokemons.AddRange(new int[] { 215 });
            }
            if (type == 18)
            {
                name = "Razor Fang";
                description = "A peculiar item that can make certain species of Pokémon evolve (Gligar). It is a fierce fang.";
                evolvedPokemons.AddRange(new int[] { 207 });
            }
            if (type == 19)
            {
                name = "Deep Sea Scale";
                description = "A peculiar item that can make certain species of Pokémon evolve (Clamperl). This scale shines with a faint pink.";
                evolvedPokemons.AddRange(new int[] { 366 });
            }
            if (type == 20)
            {
                name = "Deep Sea Tooth";
                description = "A peculiar item that can make certain species of Pokémon evolve (Clamperl). This fang gleams a sharp silver.";
                evolvedPokemons.AddRange(new int[] { 366 });
            }
            if (type == 21)
            {
                name = "Dragon Scale";
                description = "A peculiar item that can make certain species of Pokémon evolve (Seadra). It is a very tough and inflexible scale.";
                evolvedPokemons.AddRange(new int[] { 117 });
            }
            if (type == 22)
            {
                name = "Oval Stone";
                description = "A peculiar item that can make certain species of Pokémon evolve (Happiny). It's as round as a Pokémon Egg.";
                evolvedPokemons.AddRange(new int[] { 440 });
            }
            if (type == 23)
            {
                name = "Up-Grade";
                description = "A peculiar item that can make certain species of Pokémon evolve (Porygon). It is a transparent device somehow filled with all sorts of data. It was produced by Silph Co.";
                evolvedPokemons.AddRange(new int[] { 137 });
            }
            if (type == 24)
            {
                name = "Dubious Disc";
                description = "A peculiar item that can make certain species of Pokémon evolve (Porygon2). It is a transparent device overflowing with dubious data. Its producer is unknown.";
                evolvedPokemons.AddRange(new int[] { 233 });
            }
        }
    }
}
