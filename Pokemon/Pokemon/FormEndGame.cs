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
    public partial class FormEndGame : Form
    {
        static public bool endgame = false;
        PictureBox[] pbPokemon = new PictureBox[6];

        public FormEndGame(int place)
        {
            endgame = true;

            InitializeComponent();

            pbPokemon[0] = pbPokemon1;
            pbPokemon[1] = pbPokemon2;
            pbPokemon[2] = pbPokemon3;
            pbPokemon[3] = pbPokemon4;
            pbPokemon[4] = pbPokemon5;
            pbPokemon[5] = pbPokemon6;

            for (int i = 0; i < FormGame.player.pokemons.Count; ++i)
                pbPokemon[i].Image = Resources.pokeFront[FormGame.player.pokemons[i].index];

            if (place > 2)
                tbMessage.Text = "Congratulations! You finished the tournament in Top " + place + ". ";
            if (place == 2)
                tbMessage.Text = "Congratulations! You finished the tournament in Top " + place + ". You were so close! My team was the team that I used to beat the game. ";
            if(place == 1)
                tbMessage.Text = "Congratulations! You won the tournament! My team was the team that I used to beat the game, but you were better. ";

            tbMessage.Text += "I am Thomas, the creator of this game, and I am grateful that you played my game until the end. Below is the hall of fame, your team deserves it!";
        }

        private void FormEndGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
