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
    public partial class FormBag : Form
    {
        public FormBag()
        {
            InitializeComponent();
            Initialize();
        }

        FormBattle formBattle;
        public FormBag(FormBattle form)
        {
            InitializeComponent();
            Initialize();

            formBattle = form;
        }

        private void Initialize()
        {
            // money
            labelMoney.Text = FormGame.player.money + " ¥";

            // pokeballs
            for (int i = 0; i < FormGame.player.pokeballs.Count; ++i)
            {
                Pokeball tmp = FormGame.player.pokeballs[i];
                listBoxPokeballs.Items.Add(tmp.name + " x" + tmp.number);
            }

            // potions
            for (int i = 0; i < FormGame.player.potions.Count; ++i)
            {
                Potion tmp = FormGame.player.potions[i];
                listBoxPotions.Items.Add(tmp.name + " x" + tmp.number);
            }

            // TMs
            for(int i=0;i<FormGame.player.TMs.Count;++i)
            {
                listBoxOthers.Items.Add(FormGame.player.TMs[i].name);
            }

            // evolutionary items
            for (int i = 0; i < FormGame.player.evolutionItems.Count; ++i)
            {
                listBoxOthers.Items.Add(FormGame.player.evolutionItems[i].name);
            }
        }

        ListBox lastListBox;

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox lb = (ListBox)sender;
            if (lb.SelectedIndex > lb.Items.Count || lb.SelectedIndex < 0) return;

            if(lb == listBoxPokeballs)
            {
                listBoxPotions.ClearSelected();
                listBoxOthers.ClearSelected();

                tbDescription.Text = FormGame.player.pokeballs[lb.SelectedIndex].description;
            }
            if(lb == listBoxPotions)
            {
                listBoxPokeballs.ClearSelected();
                listBoxOthers.ClearSelected();

                tbDescription.Text = FormGame.player.potions[lb.SelectedIndex].description;
            }
            if (lb == listBoxOthers)
            {
                listBoxPokeballs.ClearSelected();
                listBoxPotions.ClearSelected();

                if (lb.SelectedIndex + 1 <= FormGame.player.TMs.Count) // is TM
                {
                    tbDescription.Text = FormGame.player.TMs[lb.SelectedIndex].description;
                }
                else if(lb.SelectedIndex + 1 <= FormGame.player.TMs.Count + FormGame.player.evolutionItems.Count) // is evolution item
                {
                    tbDescription.Text = FormGame.player.evolutionItems[lb.SelectedIndex - FormGame.player.TMs.Count].description;
                }
            }

            lastListBox = lb;
        }

        private void buttonUse_Click(object sender, EventArgs e)
        {
            if (lastListBox == null) return;

            if (formBattle != null) // check if user is in battle
            {
                if (lastListBox == listBoxPokeballs)
                {
                    formBattle.ThrowBall(FormGame.player.pokeballs[lastListBox.SelectedIndex]);
                    this.Close();
                }
                if(lastListBox == listBoxPotions)
                {
                    formBattle.UsePotion(FormGame.player.potions[lastListBox.SelectedIndex]);
                    this.Close();
                }
            }
            else
            {
                if (lastListBox == listBoxPotions)
                {
                    new FormPokemons(FormGame.player.potions[lastListBox.SelectedIndex]).ShowDialog();
                    this.Close();
                }
                if (lastListBox == listBoxOthers)
                {
                    if (lastListBox.SelectedIndex + 1 <= FormGame.player.TMs.Count) // is TM
                    {
                        new FormPokemons(FormGame.player.TMs[lastListBox.SelectedIndex]).ShowDialog();
                    }
                    else if (lastListBox.SelectedIndex + 1 <= FormGame.player.TMs.Count + FormGame.player.evolutionItems.Count) // is evolution item
                    {
                        new FormPokemons(FormGame.player.evolutionItems[lastListBox.SelectedIndex - FormGame.player.TMs.Count]).ShowDialog();
                    }
                    this.Close();
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
