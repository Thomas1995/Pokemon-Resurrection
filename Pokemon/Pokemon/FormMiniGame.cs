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
    public partial class FormMiniGame : Form
    {
        Pokemons poke;
        FormPokemons mainForm;
        Random rand = new Random();
        int score;
        bool infoScreen = true;

        public FormMiniGame(FormPokemons _mainForm, Pokemons _poke)
        {
            mainForm = _mainForm;
            poke = _poke;

            InitializeComponent();

            labelInfo.Parent = pbCanvas;

            Game1();
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            pbCanvas.Invalidate();
        }

        private void timerEndGame_Tick(object sender, EventArgs e)
        {
            timerEndGame.Stop();
            poke.EarnLove(0, 0, score);
            mainForm.ChangeDetails(poke);
            endgame = true;
            this.Close();
        }

        List<int> g1_posList = new List<int>();
        List<int> g1_idList = new List<int>();
        int g1_spawnMax = 1000;

        private void GameStart()
        {
            infoScreen = false;
            labelInfo.Visible = false;
            buttonStart.Visible = false;
        }

        private void Game1()
        {
            pbCanvas.BackgroundImage = Resources.minigame1bg;

            labelInfo.Text = "Try to catch Caterpie, Metapod and Butterfree!\nBe careful, Weedle is poisonous and will make\nyou lose points.";
        }

        private void Game1_Start()
        {
            GameStart();

            timerRefresh.Interval = 20;
            timerRefresh.Start();

            g1_posList.Add(-30);
            g1_posList.Add(-120);
            g1_posList.Add(-240);
            g1_posList.Add(-340);
            g1_posList.Add(-450);
            g1_posList.Add(-540);

            g1_idList.Add(10);
            g1_idList.Add(10);
            g1_idList.Add(10);
            g1_idList.Add(10);
            g1_idList.Add(10);
            g1_idList.Add(10);

            g1_spawnMax = 6;
        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            if (infoScreen)
            {
                canvas.DrawImage(Resources.pokeFront[10], 50, 150);
                canvas.DrawImage(Resources.pokeFront[11], 100, 150);
                canvas.DrawImage(Resources.pokeFront[12], 150, 150);
                canvas.DrawImage(Resources.pokeFront[13], 220, 150);
            }
            else
            {
                for (int i = 0; i < g1_posList.Count; ++i)
                {
                    g1_posList[i] += 15;

                    canvas.DrawImage(Resources.pokeFront[g1_idList[i]], g1_posList[i], 100);
                }

                if (g1_posList.Count == 0)
                {
                    timerRefresh.Stop();
                    timerEndGame.Start();
                }
                else if (g1_posList[0] > 530)
                {
                    if (g1_idList[0] == 13) score += 1;
                    else score -= (g1_idList[0] - 9);
                    labelScore.Text = "Score: " + score;

                    g1_posList.RemoveAt(0);
                    g1_idList.RemoveAt(0);

                    if (g1_spawnMax < 100)
                    {
                        g1_posList.Add(g1_posList[g1_posList.Count - 1] - rand.Next(90, 121));
                        g1_idList.Add(rand.Next(10, 14));
                        g1_spawnMax += 1;
                    }
                }

                int x = this.PointToClient(MousePosition).X - 50;
                if (x < -20) x = -20;
                if (x > 530) x = 530;
                canvas.DrawImage(Resources.pokeBack[poke.index], x, 320);
            }
        }

        private void pbCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < g1_posList.Count; ++i)
            {
                if(e.X >= g1_posList[i] && e.X <= g1_posList[i] + 80 && e.Y >= 100 && e.Y <= 180)
                {
                    if (g1_spawnMax < 100)
                    {
                        g1_posList.Add(g1_posList[g1_posList.Count - 1] - rand.Next(90, 121));
                        g1_idList.Add(rand.Next(10, 14));
                        g1_spawnMax += 1;
                    }

                    if (g1_idList[i] == 13) score -= 100;
                    else score += 3 * (g1_idList[i] - 9);

                    g1_posList.RemoveAt(i);
                    g1_idList.RemoveAt(i);

                    labelScore.Text = "Score: " + score;
                    return;
                }
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Game1_Start();
        }

        bool endgame = false;
        private void FormMiniGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!endgame) Application.Exit();
        }
    }
}
