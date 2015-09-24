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
    public partial class FormNickname : Form
    {
        Pokemons pokemon;

        public FormNickname(Pokemons poke)
        {
            InitializeComponent();

            pokemon = poke;
            tbNickname.Text = pokemon.name;
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            string nickname = tbNickname.Text;
            string[] words = nickname.Split();
            nickname = words[0];

            if (nickname == "") return;

            pokemon.name = nickname;
            this.Close();
        }
    }
}
