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
    public partial class FormShop : Form
    {
        public FormShop()
        {
            InitializeComponent();
            ChangeMoney();

            listBoxPokeballs.Items.Add("Poké Ball - 200 ¥");
            listBoxPokeballs.Items.Add("Great Ball - 400 ¥");
            listBoxPokeballs.Items.Add("Ultra Ball - 600 ¥");

            listBoxPotions.Items.Add("Potion - 300 ¥");
            listBoxPotions.Items.Add("Super Potion - 600 ¥");
            listBoxPotions.Items.Add("Hyper Potion - 900 ¥");
            listBoxPotions.Items.Add("Max Potion - 1200 ¥");
            listBoxPotions.Items.Add("Max Elixir - 1500 ¥");
            listBoxPotions.Items.Add("Ice Heal - 100 ¥");
            listBoxPotions.Items.Add("Paralyze Heal - 100 ¥");
            listBoxPotions.Items.Add("Awakening - 100 ¥");
            listBoxPotions.Items.Add("Antidote - 100 ¥");
            listBoxPotions.Items.Add("Burn Heal - 100 ¥");
        }

        private void ChangeMoney()
        {
            labelMoney.Text = "Your money: " + FormGame.player.money + " ¥";
        }

        ListBox lastListBox;

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox lb = (ListBox)sender;
            if (lb.SelectedIndex > lb.Items.Count || lb.SelectedIndex < 0) return;

            if (lb == listBoxPokeballs)
            {
                listBoxPotions.ClearSelected();
                listBoxOthers.ClearSelected();

                Pokeball tmp = new Pokeball(lb.SelectedIndex + 1);
                tbDescription.Text = tmp.description;
            }
            if (lb == listBoxPotions)
            {
                listBoxPokeballs.ClearSelected();
                listBoxOthers.ClearSelected();

                Potion tmp = new Potion(lb.SelectedIndex + 1);
                tbDescription.Text = tmp.description;
            }

            lastListBox = lb;
        }

        private void buttonBuy_Click(object sender, EventArgs e)
        {
            bool newItem = true;
            int j = 0;

            if (lastListBox == listBoxPokeballs)
            {
                Pokeball aux = new Pokeball(lastListBox.SelectedIndex + 1);

                if (FormGame.player.money >= aux.price)
                    FormGame.player.money -= aux.price;
                else return;

                List<Pokeball> tmp = FormGame.player.pokeballs;
                for (int i = 0; i < tmp.Count; ++i)
                    if (tmp[i].type == lastListBox.SelectedIndex + 1)
                    {
                        j = i;
                        newItem = false;
                        break;
                    }

                if (newItem)
                {
                    tmp.Add(new Pokeball(lastListBox.SelectedIndex + 1));
                    tmp[tmp.Count - 1].number = 1;
                }
                else tmp[j].number += 1;
            }
            if(lastListBox == listBoxPotions)
            {
                Potion aux = new Potion(lastListBox.SelectedIndex + 1);

                if (FormGame.player.money >= aux.price)
                    FormGame.player.money -= aux.price;
                else return;

                List<Potion> tmp = FormGame.player.potions;
                for (int i = 0; i < tmp.Count; ++i)
                    if (tmp[i].type == lastListBox.SelectedIndex + 1)
                    {
                        j = i;
                        newItem = false;
                        break;
                    }

                if (newItem)
                {
                    tmp.Add(new Potion(lastListBox.SelectedIndex + 1));
                    tmp[tmp.Count - 1].number = 1;
                }
                else tmp[j].number += 1;
            }

            ChangeMoney();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
