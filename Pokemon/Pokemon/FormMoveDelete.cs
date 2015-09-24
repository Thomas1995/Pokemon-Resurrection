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
    public partial class FormMoveDelete : Form
    {
        Pokemons poke;
        Attack move;
        Button[] attack = new Button[4];
        ToolTip[] tip = new ToolTip[4];

        public FormMoveDelete(Pokemons _poke, Attack _move)
        {
            poke = _poke;
            move = _move;

            InitializeComponent();

            attack[0] = attack1;
            attack[1] = attack2;
            attack[2] = attack3;
            attack[3] = attack4;

            for (int i = 0; i < poke.moves.Count; ++i)
            {
                attack[i].Text = poke.moves[i].name;
                attack[i].Enabled = true;
                attack[i].BackColor = poke.moves[i].color;

                tip[i] = new ToolTip();
                string tiptext = "Damage: " + poke.moves[i].damage
                    + "\nAccuracy: " + poke.moves[i].accuracy
                    + "\nDescription:\n" + poke.moves[i].description;

                tip[i].SetToolTip(attack[i], tiptext);
            }

            newAttack.Text = move.name;
            newAttack.Enabled = true;
            newAttack.BackColor = move.color;

            tbAttack.Text = "Type: " + Pokemons.GetType(move.type) + "\nCategory: ";

            switch (move.category)
            {
                case 1: tbAttack.Text += "Physical"; break;
                case 2: tbAttack.Text += "Special"; break;
                case 3: tbAttack.Text += "Status"; break;
            }

            tbAttack.Text += "\nDamage: " + move.damage
                + "\nAccuracy: " + move.accuracy
                + "\nPP: " + move.PP + " / " + move.PPmax
                + "\nDescription:\n" + move.description;
        }

        private void attack_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; ++i)
            {
                if (attack[i] == (Button)sender)
                {
                    if (MessageBox.Show("Are you sure you want to delete " + poke.moves[i].name + "?", "", 
                        MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No) return;

                    poke.moves.Remove(poke.moves[i]);
                    poke.moves.Add(move);
                    this.Close();
                    break;
                }
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
