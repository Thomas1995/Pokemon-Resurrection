using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class FormCharacterCreation : Form
    {
        PictureBox[] pokemons = new PictureBox[12];
        RadioButton[] check = new RadioButton[12];

        public FormCharacterCreation()
        {
            InitializeComponent();

            labelError.Hide();

            Initialize();
        }

        private void Initialize()
        {
            pokemons[0] = poke1;
            pokemons[1] = poke2;
            pokemons[2] = poke3;
            pokemons[3] = poke4;
            pokemons[4] = poke5;
            pokemons[5] = poke6;
            pokemons[6] = poke7;
            pokemons[7] = poke8;
            pokemons[8] = poke9;
            pokemons[9] = poke10;
            pokemons[10] = poke11;
            pokemons[11] = poke12;

            check[0] = check1;
            check[1] = check2;
            check[2] = check3;
            check[3] = check4;
            check[4] = check5;
            check[5] = check6;
            check[6] = check7;
            check[7] = check8;
            check[8] = check9;
            check[9] = check10;
            check[10] = check11;
            check[11] = check12;

            pokemons[0].Image = Resources.pokeFront[1];
            pokemons[1].Image = Resources.pokeFront[4];
            pokemons[2].Image = Resources.pokeFront[7];
            pokemons[3].Image = Resources.pokeFront[152];
            pokemons[4].Image = Resources.pokeFront[155];
            pokemons[5].Image = Resources.pokeFront[158];
            pokemons[6].Image = Resources.pokeFront[252];
            pokemons[7].Image = Resources.pokeFront[255];
            pokemons[8].Image = Resources.pokeFront[258];
            pokemons[9].Image = Resources.pokeFront[387];
            pokemons[10].Image = Resources.pokeFront[390];
            pokemons[11].Image = Resources.pokeFront[393];
        }

        #region Close Application

        bool applicationIsRunning;
        private void FormCharacterCreation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!applicationIsRunning)
                Application.Exit();
        }
        #endregion

        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            if (selectedNr < 0)
            {
                labelError.Text = "Select a starter pokemon";
                labelError.Show();
                return;
            }

            if (tbName.Text == "")
            {
                labelError.Text = "Select a name for your trainer";
                labelError.Show();
                return;
            }

            string[] words = tbName.Text.Split();
            if(words[0] != tbName.Text)
            {
                labelError.Text = "Your name can not contain spaces";
                labelError.Show();
                return;
            }

            applicationIsRunning = true;

            Character player = new Character(tbName.Text);

            int pokeNr;

            switch (selectedNr)
            {
                case 0: pokeNr = 1; break;
                case 1: pokeNr = 4; break;
                case 2: pokeNr = 7; break;
                case 3: pokeNr = 152; break;
                case 4: pokeNr = 155; break;
                case 5: pokeNr = 158; break;
                case 6: pokeNr = 252; break;
                case 7: pokeNr = 255; break;
                case 8: pokeNr = 258; break;
                case 9: pokeNr = 387; break;
                case 10: pokeNr = 390; break;
                default: pokeNr = 393; break;
            }

            Pokemons pokemon = new Pokemons(pokeNr, 5);

            FormPokedex.SeePokemon(pokeNr);
            FormPokedex.OwnPokemon(pokeNr);

            // starting pokemon
            pokemon.lovebase = 50;
            pokemon.EarnLove(100, 20, 30);
            player.AddPokemon(pokemon);

            // starting items
            Pokeball pball = new Pokeball(1);
            pball.number = 5;
            player.pokeballs.Add(pball);

            // starting money
            player.money = 1000;

            // starting food
            player.food = 10;

            new FormGame(player).Show();

            this.Close();
        }

        private int selectedNr = -1;

        private void check_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 12; ++i)
                if ((RadioButton)sender == check[i])
                {
                    selectedNr = i;
                    break;
                }
        }

        private void poke_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < 12; ++i)
                if ((PictureBox)sender == pokemons[i])
                {
                    selectedNr = i;
                    check[i].Checked = true;
                    break;
                }
        }
    }
}
