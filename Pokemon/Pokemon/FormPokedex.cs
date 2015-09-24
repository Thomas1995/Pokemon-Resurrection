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
    public partial class FormPokedex : Form
    {
        static public int owned, seen;
        static public bool[] inPokedex = new bool[494];
        static public bool[] ownedPokemon = new bool[494];
        static public string[] names = new string[494];

        public FormPokedex()
        {
            InitializeComponent();

            labelOwned.Text = owned + "";
            labelSeen.Text = seen + "";

            for(int i=1;i<=493;++i)
            {
                string nr = i + "";
                if (i < 10) nr = "00" + i;
                else if (i < 100) nr = "0" + i;

                string name = "???";

                if (inPokedex[i]) name = names[i];

                listPokemons.Items.Add("No" + nr + " " + name);
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        static public void SeePokemon(int index)
        {
            if (inPokedex[index]) return;

            inPokedex[index] = true;
            
            List<string> ret = DataBase.GetPokemon(index);
            names[index] = ret[0];

            ++seen;
        }

        static public void OwnPokemon(int index)
        {
            if (ownedPokemon[index]) return;

            ownedPokemon[index] = true;

            ++owned;
        }

        private void listPokemons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (inPokedex[listPokemons.SelectedIndex + 1])
                pbPokemon.Image = Resources.pokeFront[listPokemons.SelectedIndex + 1];
            else pbPokemon.Image = null;
        }
    }
}
