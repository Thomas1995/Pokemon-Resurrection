using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Pokemon
{
    public class Character
    {
        public Image crtSprite;

        public string name;

        private int i, j;
        private Image[,] sprites = new Image[4, 4];

        public bool isInFront = true; // true if drawn last, false if drawn first

        public List<Pokemons> pokemons = new List<Pokemons>();
        public List<Pokemons> pokemonsLab = new List<Pokemons>();

        // inventory
        public List<Pokeball> pokeballs = new List<Pokeball>();
        public List<Potion> potions = new List<Potion>();
        public List<Attack> TMs = new List<Attack>();
        public List<EvolutionItem> evolutionItems = new List<EvolutionItem>();

        public int money = 0;
        public int badges = 0;
        public int food = 0;

        public int healingcenter = 1;

        public Character(string _name)
        {
            name = _name;

            // Character's graphic
            for (int p1 = 0; p1 <= 3; ++p1)
                for (int p2 = 0; p2 <= 3; ++p2)
                {
                    sprites[p1, p2] = new Bitmap(37, 48);
                    Graphics G = Graphics.FromImage(sprites[p1, p2]);

                    G.DrawImage(Resources.character_boy, new Rectangle(0, 0, 37, 48),
                        new Rectangle(37 * p1, 48 * p2, 37, 48), GraphicsUnit.Pixel);
                }
        }

        public int X, Y;
        public int index;
        public bool usedMasterBall;

        public Character(string _name, Image img, int x, int y)
        {
            name = _name;
            crtSprite = img;
            X = x;
            Y = y;
        }

        public void AddPokemon(Pokemons pokemon)
        {
            if (pokemons.Count < 6) pokemons.Add(pokemon);
            else pokemonsLab.Add(pokemon);
        }

        public void HealTeam()
        {
            for (int i = 0; i < pokemons.Count; ++i)
            {
                pokemons[i].hp = pokemons[i].hpmax;

                for (int j = 0; j < pokemons[i].moves.Count; ++j)
                    pokemons[i].moves[j].PP = pokemons[i].moves[j].PPmax;
            }
        }

        #region Animation
        public void ChangeSprite()
        {
            int dir = Keyboard.GetDir();

            if (dir == 0)
            {
                i = 0;
            }
            if (dir == 1)
            {
                if (i == 1) i = 3;
                else i = 1;
                j = 3;
            }
            if (dir == 2)
            {
                if (i == 1) i = 3;
                else i = 1;
                j = 0;
            }
            if (dir == 3)
            {
                if (i == 1) i = 3;
                else i = 1;
                j = 2;
            }
            if (dir == 4)
            {
                if (i == 1) i = 3;
                else i = 1;
                j = 1;
            }

            crtSprite = sprites[i, j];
        }

        public void ChangeSpriteWater()
        {
            int dir = Keyboard.GetDir();

            if (dir == 1) j = 3;
            if (dir == 2) j = 0;
            if (dir == 3) j = 2;
            if (dir == 4) j = 1;

            crtSprite = sprites[0, j];
        }
        #endregion
    }
}
