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
    public partial class FormPokemons : Form
    {
        PictureBox[] pbPokemon = new PictureBox[6];
        PictureBox[] pbHealthRed = new PictureBox[6];
        PictureBox[] pbHealthGreen = new PictureBox[6];
        Button[] attack = new Button[4];

        PictureBox[] pbBadge = new PictureBox[8];

        static public FormPokemons lastForm;

        public FormPokemons()
        {
            if (lastForm != null) lastForm.Close();
            lastForm = this;

            InitializeComponent();
            Initialize();
        }

        Potion potion;

        public FormPokemons(Potion pot)
        {
            if (lastForm != null) lastForm.Close();
            lastForm = this;

            potion = pot;

            InitializeComponent();
            Initialize();
        }

        Attack move;
        public FormPokemons(Attack _move)
        {
            if (lastForm != null) lastForm.Close();
            lastForm = this;

            move = _move;

            InitializeComponent();
            Initialize();
        }

        EvolutionItem evolitem;
        public FormPokemons(EvolutionItem _evolitem)
        {
            if (lastForm != null) lastForm.Close();
            lastForm = this;

            evolitem = _evolitem;

            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            // pokemons
            pbPokemon[0] = pbPokemon1;
            pbPokemon[1] = pbPokemon2;
            pbPokemon[2] = pbPokemon3;
            pbPokemon[3] = pbPokemon4;
            pbPokemon[4] = pbPokemon5;
            pbPokemon[5] = pbPokemon6;

            // health bars

            pbHealthRed[0] = pbHealthRed1;
            pbHealthRed[1] = pbHealthRed2;
            pbHealthRed[2] = pbHealthRed3;
            pbHealthRed[3] = pbHealthRed4;
            pbHealthRed[4] = pbHealthRed5;
            pbHealthRed[5] = pbHealthRed6;

            pbHealthGreen[0] = pbHealthGreen1;
            pbHealthGreen[1] = pbHealthGreen2;
            pbHealthGreen[2] = pbHealthGreen3;
            pbHealthGreen[3] = pbHealthGreen4;
            pbHealthGreen[4] = pbHealthGreen5;
            pbHealthGreen[5] = pbHealthGreen6;

            // attacks
            attack[0] = attack1;
            attack[1] = attack2;
            attack[2] = attack3;
            attack[3] = attack4;

            // badges
            pbBadge[0] = pbBadge1;
            pbBadge[1] = pbBadge2;
            pbBadge[2] = pbBadge3;
            pbBadge[3] = pbBadge4;
            pbBadge[4] = pbBadge5;
            pbBadge[5] = pbBadge6;
            pbBadge[6] = pbBadge7;
            pbBadge[7] = pbBadge8;

            buttonFeed.Text = "Feed (" + FormGame.player.food + ")";

            for (int i = FormGame.player.badges; i < 8; ++i)
                pbBadge[i].BackgroundImage = null;

            for (int i = 0; i < FormGame.player.pokemons.Count; ++i)
            {
                // change health
                pbHealthGreen[i].Width = Convert.ToInt32(FormGame.player.pokemons[i].hp * 80 / FormGame.player.pokemons[i].hpmax);

                if (move != null)
                {
                    // check if pokemon can learn a move by TM
                    if(DataBase.PokeCanLearnTM(move.index, FormGame.player.pokemons[i].index))
                        pbPokemon[i].BackColor = Color.LightGreen;
                    else
                        pbPokemon[i].BackColor = Color.Red;

                    // check if pokemon has already learnt the move
                    for(int j=0;j<FormGame.player.pokemons[i].moves.Count;++j)
                    {
                        if(FormGame.player.pokemons[i].moves[j].index == move.index)
                            pbPokemon[i].BackColor = Color.Red;
                    }
                }
                pbPokemon[i].Image = Resources.pokeFront[FormGame.player.pokemons[i].index];

                if(evolitem != null)
                {
                    // check if pokemon can evolve with the evolution item
                    pbPokemon[i].BackColor = Color.Red;
                    for(int j=0;j<evolitem.evolvedPokemons.Count;++j)
                    {
                        if(FormGame.player.pokemons[i].index == evolitem.evolvedPokemons[j])
                        {
                            if (FormGame.player.pokemons[i].index == 281 && FormGame.player.pokemons[i].gender != 1) continue;
                            pbPokemon[i].BackColor = Color.LightGreen;
                            break;
                        }
                    }
                }
            }
            for(int i=FormGame.player.pokemons.Count;i<6;++i)
            {
                pbPokemon[i].Image = null;
                pbHealthRed[i].Visible = false;
                pbHealthGreen[i].Visible = false;
            }
        }

        Pokemons crtPokemon;
        int crtPokemonIndex;

        private void pbPokemon_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < FormGame.player.pokemons.Count; ++i)
            {
                if (pbPokemon[i] == (PictureBox)sender)
                {
                    if(potion != null)
                    {
                        if (FormGame.player.pokemons[i].hp > 0)
                            potion.UsePotion(FormGame.player.pokemons[i]);
                        else MessageBox.Show("You can not use a potion on a dead pokemon", "", MessageBoxButtons.OK);

                        this.Close();
                    }

                    if(move != null)
                    {
                        if (pbPokemon[i].BackColor == Color.LightGreen)
                        {
                            if (MessageBox.Show("Are you sure you want " + FormGame.player.pokemons[i].name + " to learn " + move.name + "?", "",
                                MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                            {
                                FormGame.player.pokemons[i].LearnMove(new Attack(move.index));
                                if (!move.isHM) FormGame.player.TMs.Remove(move);
                                this.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show(FormGame.player.pokemons[i].name + " can not learn " + move.name, "", MessageBoxButtons.OK);
                        }
                    }

                    if (evolitem != null)
                    {
                        if (pbPokemon[i].BackColor == Color.LightGreen)
                        {
                            if (MessageBox.Show("Are you sure you want " + FormGame.player.pokemons[i].name + " to evolve?", "",
                                MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                            {
                                int pokeIndex = FormGame.player.pokemons[i].index;

                                int pokeEvolution = Resources.evolution[pokeIndex].evolution;

                                if(pokeIndex == 133) // Eevee
                                {
                                    if (evolitem.type == 1) pokeEvolution = 135;
                                    if (evolitem.type == 2) pokeEvolution = 136;
                                    if (evolitem.type == 3) pokeEvolution = 134;
                                }
                                
                                if(pokeIndex == 44) // Gloom
                                {
                                    if (evolitem.type == 8) pokeEvolution = 182;
                                    if (evolitem.type == 9) pokeEvolution = 45;
                                }

                                if(pokeIndex == 61) // Poliwhirl
                                {
                                    if (evolitem.type == 3) pokeEvolution = 62;
                                    if (evolitem.type == 10) pokeEvolution = 186;
                                }

                                if(pokeIndex == 79) // Slowpoke
                                {
                                    if (evolitem.type == 10) pokeEvolution = 199;
                                }

                                if(pokeIndex == 281) // Kirlia
                                {
                                    if(evolitem.type == 4) pokeEvolution = 475;
                                }

                                if(pokeIndex == 361) // Snorunt
                                {
                                    if(evolitem.type == 4) pokeEvolution = 478;
                                }

                                if (pokeIndex == 366) // Clamperl
                                {
                                    if (evolitem.type == 20) pokeEvolution = 367;
                                    if (evolitem.type == 19) pokeEvolution = 368;
                                }

                                FormGame.player.pokemons[i].Evolve(pokeEvolution);
                                new FormEvolution(pokeIndex, pokeEvolution).ShowDialog();
                                FormGame.player.evolutionItems.Remove(evolitem);
                                this.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show(FormGame.player.pokemons[i].name + " can not evolve with this item", "", MessageBoxButtons.OK);
                        }
                    }

                    crtPokemonIndex = i;

                    crtPokemon = FormGame.player.pokemons[i];

                    ChangeDetails(crtPokemon);

                    break;
                }
            }
        }

        public void ChangeDetails(Pokemons poke)
        {
            tbName.Text = poke.name;

            if (poke.gender == 0) tbName.Text += " ♀ ";
            if (poke.gender == 1) tbName.Text += " ♂ ";

            tbName.Text += "\n(" + Pokemons.GetType(poke.type1);

            if (poke.type2 != 0) tbName.Text += " / " + Pokemons.GetType(poke.type2);

            tbName.Text += ")";

            tbStats.Text = "Level: " + poke.level;

            if(poke.level < 100) 
                tbStats.Text += "\nXP: " + poke.xp + " / " + Resources.xpneeded[poke.level];

            tbStats.Text += "\n\nHP: " + (int)poke.hp + " / " + (int)poke.hpmax
                        + "\nATK: " + (int)poke.atk + "\nDEF: " + (int)poke.def
                        + "\nSPATK: " + (int)poke.spatk + "\nSPDEF: " + (int)poke.spdef
                        + "\nSpeed: " + (int)poke.speed + "\n\n";

            if (poke.lovebase <= 10) tbStats.Text += poke.name + " is a cold-hearted pokemon.\n";
            else if (poke.lovebase <= 20) tbStats.Text += poke.name + " is a grumpy pokemon.\n";
            else if (poke.lovebase <= 30) tbStats.Text += poke.name + " is a neutral pokemon.\n";
            else if (poke.lovebase <= 40) tbStats.Text += poke.name + " is a friendly pokemon.\n";
            else tbStats.Text += poke.name + " is an affectionate pokemon.\n";

            if(poke.trust <= 20) tbStats.Text += poke.name + " has trust issues.\n";
            else if (poke.trust >= 80) tbStats.Text += poke.name + " trusts you more than anything.\n";

            if (poke.hunger <= 5) tbStats.Text += poke.name + " is hungry.\n";

            if (poke.joy <= 5) tbStats.Text += poke.name + " is very sad.\n";
            else if (poke.joy >= 20) tbStats.Text += poke.name + " is very happy.\n";

            for (int j = 0; j < 4; ++j)
            {
                attack[j].Text = "-";
                attack[j].Enabled = false;
                attack[j].BackColor = Color.BurlyWood;
            }

            for (int j = 0; j < poke.moves.Count; ++j)
            {
                attack[j].Text = poke.moves[j].name;
                attack[j].Enabled = true;
                attack[j].BackColor = poke.moves[j].color;
            }
        }

        private void attack_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; ++i)
            {
                if(attack[i] == (Button)sender)
                {
                    tbAttack.Text = "Type: " + Pokemons.GetType(crtPokemon.moves[i].type) + "\nCategory: ";

                    switch(crtPokemon.moves[i].category)
                    {
                        case 1: tbAttack.Text += "Physical"; break;
                        case 2: tbAttack.Text += "Special"; break;
                        case 3: tbAttack.Text += "Status"; break;
                    }

                    tbAttack.Text += "\nDamage: " + crtPokemon.moves[i].damage
                        + "\nAccuracy: " + crtPokemon.moves[i].accuracy
                        + "\nPP: " + crtPokemon.moves[i].PP + " / " + crtPokemon.moves[i].PPmax
                        + "\nDescription:\n" + crtPokemon.moves[i].description;

                    break;
                }
            }
        }

        private void buttonRelease_Click(object sender, EventArgs e)
        {
            if (crtPokemon == null) return;

            if(FormGame.player.pokemons.Count == 1)
            {
                MessageBox.Show("You must have at least one pokemon in your team!", "" , MessageBoxButtons.OK);
                return;
            }

            if (MessageBox.Show("Are you sure you want to release " + crtPokemon.name + "?", "",
                        MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No) return;

            FormGame.player.pokemons.Remove(crtPokemon);

            crtPokemon = null;

            Initialize();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonNickname_Click(object sender, EventArgs e)
        {
            if (crtPokemon == null) return;

            new FormNickname(crtPokemon).ShowDialog();

            ChangeDetails(crtPokemon);
        }

        private void buttonMoveLeft_Click(object sender, EventArgs e)
        {
            if(crtPokemonIndex > 0)
            {
                Pokemons aux = FormGame.player.pokemons[crtPokemonIndex-1];
                FormGame.player.pokemons[crtPokemonIndex-1] = FormGame.player.pokemons[crtPokemonIndex];
                FormGame.player.pokemons[crtPokemonIndex] = aux;
                crtPokemonIndex -= 1;

                Initialize();
            }
        }

        private void buttonMoveRight_Click(object sender, EventArgs e)
        {
            if (crtPokemonIndex < FormGame.player.pokemons.Count - 1)
            {
                Pokemons aux = FormGame.player.pokemons[crtPokemonIndex + 1];
                FormGame.player.pokemons[crtPokemonIndex + 1] = FormGame.player.pokemons[crtPokemonIndex];
                FormGame.player.pokemons[crtPokemonIndex] = aux;
                crtPokemonIndex += 1;

                Initialize();
            }
        }

        private void buttonFeed_Click(object sender, EventArgs e)
        {
            if (FormGame.player.food <= 0 || crtPokemon == null) return;

            FormGame.player.food -= 1;
            buttonFeed.Text = "Feed (" + FormGame.player.food + ")";

            crtPokemon.EarnLove(0, 5, 0);

            ChangeDetails(crtPokemon);
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            if(crtPokemon != null)
            {
                new FormMiniGame(this, crtPokemon).ShowDialog();
            }
        }

        private void FormPokemons_FormClosing(object sender, FormClosingEventArgs e)
        {
            lastForm = null;
        }
    }
}
