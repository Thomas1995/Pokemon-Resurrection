using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Pokemon
{
    public partial class FormLoadingScreen : Form
    {
        bool newGame;

        public FormLoadingScreen(bool _newGame)
        {
            newGame = _newGame;

            InitializeComponent();

            timerLoading.Start();
        }

        bool applicationIsRunning;
        private void FormLoadingScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!applicationIsRunning)
                Application.Exit();
        }

        private int points;

        private void timerLoading_Tick(object sender, EventArgs e)
        {
            points = (points + 1) % 4;
            labelLoading.Text = "Loading";
            for (int i = 1; i <= points; ++i) labelLoading.Text += ".";

            if (Resources.resourcesDone)
            {
                timerLoading.Stop();

                applicationIsRunning = true;

                if(newGame) new FormCharacterCreation().Show();
                else
                {
                    StreamReader sr = new StreamReader(Application.StartupPath + "\\game.sav");

                    string line;
                    string[] words;

                    line = sr.ReadLine();
                    words = line.Split();

                    Character player = new Character(words[0]);
                    player.money = Convert.ToInt32(words[1]);
                    player.badges = Convert.ToInt32(words[2]);
                    player.healingcenter = Convert.ToInt32(words[3]);
                    player.food = Convert.ToInt32(words[4]);

                    line = sr.ReadLine();
                    words = line.Split();
                    int pokeCount = Convert.ToInt32(words[0]);
                    for(int i=0;i<pokeCount;++i)
                    {
                        line = sr.ReadLine();
                        words = line.Split();
                        Pokemons pokemon = new Pokemons(Convert.ToInt32(words[0]), 1);
                        pokemon.level = Convert.ToInt32(words[1]);

                        pokemon.gender = Convert.ToInt32(words[2]);
                        pokemon.name = words[3] + "";
                        pokemon.lovebase = (float)Convert.ToDouble(words[4]);
                        pokemon.EarnLove((float)Convert.ToDouble(words[5]), (float)Convert.ToDouble(words[6]), (float)Convert.ToDouble(words[7]));

                        pokemon.hp = (float)Convert.ToDouble(words[8]);
                        pokemon.hpmax = (float)Convert.ToDouble(words[9]);
                        pokemon.atk = (float)Convert.ToDouble(words[10]);
                        pokemon.def = (float)Convert.ToDouble(words[11]);
                        pokemon.spatk = (float)Convert.ToDouble(words[12]);
                        pokemon.spdef = (float)Convert.ToDouble(words[13]);
                        pokemon.speed = (float)Convert.ToDouble(words[14]);

                        pokemon.xp = Convert.ToInt32(words[15]);

                        line = sr.ReadLine();
                        words = line.Split();
                        pokemon.moves = new List<Attack>();
                        int moveCount = Convert.ToInt32(words[0]);
                        for (int j = 0; j < moveCount; ++j)
                        {
                            Attack move = new Attack(Convert.ToInt32(words[1 + j * 2]));
                            move.PP = Convert.ToInt32(words[2 + j * 2]);
                            pokemon.LearnMove(move);
                        }

                        player.AddPokemon(pokemon);
                    }

                    line = sr.ReadLine();
                    words = line.Split();
                    int pokeLabCount = Convert.ToInt32(words[0]);
                    for (int i = 0; i < pokeLabCount; ++i)
                    {
                        line = sr.ReadLine();
                        words = line.Split();
                        Pokemons pokemon = new Pokemons(Convert.ToInt32(words[0]), 1);
                        pokemon.level = Convert.ToInt32(words[1]);

                        pokemon.gender = Convert.ToInt32(words[2]);
                        pokemon.name = words[3] + "";
                        pokemon.lovebase = (float)Convert.ToDouble(words[4]);
                        pokemon.EarnLove((float)Convert.ToDouble(words[5]), (float)Convert.ToDouble(words[6]), (float)Convert.ToDouble(words[7]));

                        pokemon.hp = (float)Convert.ToDouble(words[8]);
                        pokemon.hpmax = (float)Convert.ToDouble(words[9]);
                        pokemon.atk = (float)Convert.ToDouble(words[10]);
                        pokemon.def = (float)Convert.ToDouble(words[11]);
                        pokemon.spatk = (float)Convert.ToDouble(words[12]);
                        pokemon.spdef = (float)Convert.ToDouble(words[13]);
                        pokemon.speed = (float)Convert.ToDouble(words[14]);

                        pokemon.xp = Convert.ToInt32(words[15]);

                        line = sr.ReadLine();
                        words = line.Split();
                        pokemon.moves = new List<Attack>();
                        int moveCount = Convert.ToInt32(words[0]);
                        for (int j = 0; j < moveCount; ++j)
                        {
                            Attack move = new Attack(Convert.ToInt32(words[1 + j * 2]));
                            move.PP = Convert.ToInt32(words[2 + j * 2]);
                            pokemon.LearnMove(move);
                        }

                        player.pokemonsLab.Add(pokemon);
                    }

                    line = sr.ReadLine();
                    words = line.Split();
                    int pokeballCount = Convert.ToInt32(words[0]);
                    for (int i = 0; i < pokeballCount;++i )
                    {
                        Pokeball ball = new Pokeball(Convert.ToInt32(words[1 + i * 2]));
                        ball.number = Convert.ToInt32(words[2 + i * 2]);
                        player.pokeballs.Add(ball);
                    }

                    line = sr.ReadLine();
                    words = line.Split();
                    int potionCount = Convert.ToInt32(words[0]);
                    for (int i = 0; i < potionCount; ++i)
                    {
                        Potion pot = new Potion(Convert.ToInt32(words[1 + i * 2]));
                        pot.number = Convert.ToInt32(words[2 + i * 2]);
                        player.potions.Add(pot);
                    }

                    line = sr.ReadLine();
                    words = line.Split();
                    int TMCount = Convert.ToInt32(words[0]);
                    for (int i = 0; i < TMCount; ++i)
                    {
                        player.TMs.Add(new Attack(Convert.ToInt32(words[1 + i])));
                    }

                    line = sr.ReadLine();
                    words = line.Split();
                    int EvolutionItemsCount = Convert.ToInt32(words[0]);
                    for (int i = 0; i < EvolutionItemsCount; ++i)
                    {
                        player.evolutionItems.Add(new EvolutionItem(Convert.ToInt32(words[1 + i])));
                    }

                    FormGame formGame = new FormGame(player);

                    line = sr.ReadLine();
                    words = line.Split();
                    FormGame.map = new Map(Convert.ToInt32(words[0]), Convert.ToInt32(words[1]), Convert.ToInt32(words[2]));
                    FormGame.gameTime = Convert.ToInt32(words[3]);
                    Map.lastTimeGym = Convert.ToInt32(words[4]);

                    line = sr.ReadLine();
                    words = line.Split();
                    for (int i = 0; i < 200;++i )
                    {
                        if (Convert.ToInt32(words[i]) == 1) Map.trainerDefeated[i] = true;
                    }

                    line = sr.ReadLine();
                    words = line.Split();
                    for (int i = 1; i < 494; ++i)
                    {
                        if (Convert.ToInt32(words[i - 1]) == 1) Map.pokemonDefeated[i] = true;
                    }

                    line = sr.ReadLine();
                    words = line.Split();
                    FormPokedex.seen = Convert.ToInt32(words[0]);
                    FormPokedex.owned = Convert.ToInt32(words[1]);

                    line = sr.ReadLine();
                    words = line.Split();
                    for (int i = 1; i < 494; ++i)
                        if (Convert.ToInt32(words[i - 1]) == 1)
                            FormPokedex.inPokedex[i] = true;

                    line = sr.ReadLine();
                    words = line.Split();
                    for (int i = 1; i < 494; ++i)
                        if (Convert.ToInt32(words[i - 1]) == 1)
                            FormPokedex.ownedPokemon[i] = true;

                    line = sr.ReadLine();
                    words = line.Split();
                    for (int i = 1; i < 494; ++i)
                    { 
                        string nameString = words[i - 1] + "";
                        if (nameString != "-")
                            FormPokedex.names[i] = nameString;
                    }

                    formGame.Show();

                    sr.Close();
                }
                
                this.Close();
            }
        }
    }
}
