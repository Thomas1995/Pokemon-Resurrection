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
    public partial class FormQuestion : Form
    {
        Random rand = new Random();
        int questionNr;

        string[] questions = new string[5] { "How many evolutions does Eevee have?", 
                "How many pokemon types do currently exist?", "How many pokemons have branched evolution lines?", 
                "Which is the heaviest pokemon?", "Which is the lightest pokemon?" };

        string[] answers = new string[5] { "7", "17", "10", "Groudon", "Gastly" };

        FormGame mainForm;

        public FormQuestion(FormGame form)
        {
            InitializeComponent();

            mainForm = form;

            questionNr = rand.Next(0, 5);

            labelQuestion.Text = questions[questionNr] + " (1st-4th gen)";
        }

        private void buttonAnswer_Click(object sender, EventArgs e)
        {
            if (tbAnswer.Text == answers[questionNr])
            {
                FormGame.player.AddPokemon(new Pokemons(360, 14));
                FormPokedex.SeePokemon(360);
                FormPokedex.OwnPokemon(360);
                mainForm.ShowMessage("Cameraman: Congratulations! Here is your prize! A pokemon!");
            }
            else
            {
                mainForm.ShowMessage("Cameraman: Sorry, that is incorrect...");
            }

            this.Close();
        }
    }
}
