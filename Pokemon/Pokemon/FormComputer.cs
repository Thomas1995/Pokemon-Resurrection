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
    public partial class FormComputer : Form
    {
        PictureBox[] pbPokemon = new PictureBox[6];
        PictureBox[] pbPokemonLab = new PictureBox[9];

        int pageNr = 1;

        public FormComputer()
        {
            InitializeComponent();

            pbPokemon[0] = pbPokemon1;
            pbPokemon[1] = pbPokemon2;
            pbPokemon[2] = pbPokemon3;
            pbPokemon[3] = pbPokemon4;
            pbPokemon[4] = pbPokemon5;
            pbPokemon[5] = pbPokemon6;

            pbPokemonLab[0] = pbPokemonLab1;
            pbPokemonLab[1] = pbPokemonLab2;
            pbPokemonLab[2] = pbPokemonLab3;
            pbPokemonLab[3] = pbPokemonLab4;
            pbPokemonLab[4] = pbPokemonLab5;
            pbPokemonLab[5] = pbPokemonLab6;
            pbPokemonLab[6] = pbPokemonLab7;
            pbPokemonLab[7] = pbPokemonLab8;
            pbPokemonLab[8] = pbPokemonLab9;

            InitializePB();
        }

        private void InitializePB()
        {
            tbPage.Text = pageNr + "";

            for (int i = 0; i < FormGame.player.pokemons.Count; ++i)
                pbPokemon[i].Image = Resources.pokeFront[FormGame.player.pokemons[i].index];
            for (int i = FormGame.player.pokemons.Count; i < 6; ++i)
                pbPokemon[i].Image = null;

            int lim = 9 * pageNr - 1;
            if (lim > FormGame.player.pokemonsLab.Count - 1)
                lim = FormGame.player.pokemonsLab.Count - 1;

            for (int i = 9 * (pageNr - 1); i <= lim; ++i)
                pbPokemonLab[i - 9 * (pageNr - 1)].Image = Resources.pokeFront[FormGame.player.pokemonsLab[i].index];

            if (lim + 1 - 9 * (pageNr - 1) < 0) lim = 9 * (pageNr - 1) - 1;

            for (int i = lim + 1; i < 9 * pageNr; ++i)
                pbPokemonLab[i - 9 * (pageNr - 1)].Image = null;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbPokemon_MouseClick(object sender, MouseEventArgs e)
        {
            if(((PictureBox)sender).Image == null || FormGame.player.pokemons.Count == 1) return;

            for(int i=0;i<6;++i)
            {
                if (pbPokemon[i] == (PictureBox)sender)
                {
                    FormGame.player.pokemonsLab.Add(FormGame.player.pokemons[i]);
                    FormGame.player.pokemons.RemoveAt(i);

                    InitializePB();
                    break;
                }
            }
        }

        private void pbPokemonLab_MouseClick(object sender, MouseEventArgs e)
        {
            if (((PictureBox)sender).Image == null || FormGame.player.pokemons.Count == 6) return;

            for (int i = 0; i < 9; ++i)
            {
                if (pbPokemonLab[i] == (PictureBox)sender)
                {
                    FormGame.player.pokemons.Add(FormGame.player.pokemonsLab[i + (pageNr - 1) * 9]);
                    FormGame.player.pokemonsLab.RemoveAt(i + (pageNr - 1) * 9);

                    InitializePB();
                    break;
                }
            }
        }

        private void buttonMoveLeft_Click(object sender, EventArgs e)
        {
            if (pageNr > 1)
            {
                pageNr -= 1;
                InitializePB();
            }
        }

        private void buttonMoveRight_Click(object sender, EventArgs e)
        {
            pageNr += 1;
            InitializePB();
        }
    }
}
