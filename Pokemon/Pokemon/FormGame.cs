using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Pokemon
{
    public partial class FormGame : Form
    {
        public static int refreshRate = 80;
        public static bool gameIsPaused;

        public static int gameTime;

        public static Character player;
        public static Map map;

        public static bool stopMovement;

        public FormGame(Character character)
        {
            InitializeComponent();

            player = character;

            Map.mainForm = this;

            tbDialog.Hide();
            panelDialog.Hide();

            map = new Map(1, 375, 270);

            new Paint_BlackScreen(this, pbCanvas.CreateGraphics(), pbCanvas.Width, pbCanvas.Height);

            timerRefresh.Interval = refreshRate;
            timerRefresh.Start();

            panelMenu.Location = new Point(panelMenu.Location.X, -31);

            panelMenuCloseTimer.Interval = 50;
            panelMenuCloseTimer.Tick += panelMenuCloseTimer_Tick;
        }

        private void FormGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!FormEndGame.endgame) Application.Exit();
        }

        bool isPainted = true;

        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            gameTime += 1;

            if (!isPainted) return;
            isPainted = false;

            map.Paint(e.Graphics);

            isPainted = true;
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            if (gameIsPaused) return;

            int dir = Keyboard.GetDir();

            if (dir == 1) if (map.CheckMovement(OX, OY - speed)) OY -= speed;
            if (dir == 2) if (map.CheckMovement(OX, OY + speed)) OY += speed;
            if (dir == 3) if (map.CheckMovement(OX + speed, OY)) OX += speed;
            if (dir == 4) if (map.CheckMovement(OX - speed, OY)) OX -= speed;

            // make team hungry and sad
            for (int i = 0; i < player.pokemons.Count; ++i)
                player.pokemons[i].EarnLove(0, -0.001f, -0.001f);

            pbCanvas.Invalidate();
        }

        static public int OX, OY, speed = 15;

        #region Keys

        private void FormGame_KeyDown(object sender, KeyEventArgs e)
        {
            KeyChange(true, e.KeyCode);

            if (e.KeyCode == Keys.B)
            {
                Keyboard.ClearStack();
                new FormBag().ShowDialog();
            }
            if (e.KeyCode == Keys.P)
            {
                Keyboard.ClearStack();
                new FormPokemons().ShowDialog();
            }
            if (e.KeyCode == Keys.D)
            {
                Keyboard.ClearStack();
                new FormPokedex().ShowDialog();
            }
            if (e.KeyCode == Keys.C)
            {
                MenuCut();
            }
            if (e.KeyCode == Keys.U)
            {
                MenuSurf();
            }
            if (e.KeyCode == Keys.S)
            {
                SaveGame();
            }
            if (e.KeyCode == Keys.G)
            {
                MenuDig();
            }
            if (e.KeyCode == Keys.H)
            {
                ShowHelp();
            }
            if (e.KeyCode == Keys.Space)
            {
                map.DoAction();
            }
        }

        private void FormGame_KeyUp(object sender, KeyEventArgs e)
        {
            KeyChange(false, e.KeyCode);
        }

        private void KeyChange(bool val, Keys key)
        {
            if (stopMovement) return;

            int dir = 0;

            switch (key)
            {
                case Keys.Up: dir = 1; break;
                case Keys.Down: dir = 2; break;
                case Keys.Right: dir = 3; break;
                case Keys.Left: dir = 4; break;
            }

            if (dir != 0) Keyboard.ChangeDir(dir, val);
        }

        private void tbDialog_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                panelDialog.Hide();
                tbDialog.Hide();
                this.Focus();
                map.DoActionAfterDialog();
            }
        }

        public void ShowMessage(string str)
        {
            Keyboard.ClearStack();

            panelDialog.Show();
            tbDialog.Text = str;
            tbDialog.Show();
            tbDialog.Focus();
        }

        #endregion

        #region Menu

        private void pbMenuBag_MouseClick(object sender, MouseEventArgs e)
        {
            Keyboard.ClearStack();
            new FormBag().ShowDialog();
            MinimizeMenu();
        }

        private void pbMenuPoke_MouseClick(object sender, MouseEventArgs e)
        {
            Keyboard.ClearStack();
            new FormPokemons().ShowDialog();
            MinimizeMenu();
        }

        private void pbMenuPokedex_MouseClick(object sender, MouseEventArgs e)
        {
            Keyboard.ClearStack();
            new FormPokedex().ShowDialog();
            MinimizeMenu();
        }

        private void pbMenuCut_MouseClick(object sender, MouseEventArgs e)
        {
            MenuCut();
            MinimizeMenu();
        }

        private void MenuCut()
        {
            Keyboard.ClearStack();

            bool canCut = false;

            for (int i = 0; i < player.pokemons.Count; ++i)
                for (int j = 0; j < player.pokemons[i].moves.Count; ++j)
                    if (player.pokemons[i].moves[j].index == 15)
                        canCut = true;

            if(canCut) map.ActionCutTrees(OX, OY);
            else ShowMessage("You need at least one of your pokemons to know Cut.");
        }

        private void pbMenuSurf_MouseClick(object sender, MouseEventArgs e)
        {
            MenuSurf();
            MinimizeMenu();
        }

        private void MenuSurf()
        {
            Keyboard.ClearStack();

            bool canSurf = false;

            for (int i = 0; i < player.pokemons.Count; ++i)
                for (int j = 0; j < player.pokemons[i].moves.Count; ++j)
                    if (player.pokemons[i].moves[j].index == 57)
                        canSurf = true;

            if (canSurf) map.ActionSurf(OX, OY);
            else ShowMessage("You need at least one of your pokemons to know Surf.");
        }

        private void pbMenuDig_MouseClick(object sender, MouseEventArgs e)
        {
            MenuDig();
            MinimizeMenu();
        }

        private void MenuDig()
        {
            Keyboard.ClearStack();

            bool canDig = false;

            for (int i = 0; i < player.pokemons.Count; ++i)
                for (int j = 0; j < player.pokemons[i].moves.Count; ++j)
                    if (player.pokemons[i].moves[j].index == 91)
                        canDig = true;

            if (canDig) map.ActionDig(OX, OY);
            else ShowMessage("You need at least one of your pokemons to know Dig.");
        }

        private void SaveGame()
        {
            if(Map.isInBuilding)
            {
                MessageBox.Show("You can not save the game from here!", "", MessageBoxButtons.OK);
                return;
            }

            if (MessageBox.Show("Are you sure you want to save the game? Your save file will be overwritten.", "", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            StreamWriter sw = new StreamWriter(Application.StartupPath + "\\game.sav");

            // player info
            sw.WriteLine(player.name + " " + player.money + " " + player.badges + " " + player.healingcenter + " " + player.food);

            // pokemons
            sw.WriteLine(player.pokemons.Count);
            for (int i = 0; i < player.pokemons.Count; ++i)
            {
                Pokemons poke = player.pokemons[i];
                sw.WriteLine(poke.index + " " + poke.level + " " + poke.gender + " " + poke.name + " " + poke.lovebase
                    + " " + poke.trust + " " + poke.hunger + " " + poke.joy + " " + poke.hp + " " + poke.hpmax + " "
                    + poke.atk + " " + poke.def + " " + poke.spatk + " " + poke.spdef + " " + poke.speed + " " + poke.xp);
                sw.Write(poke.moves.Count + " ");
                for (int j = 0; j < poke.moves.Count; ++j) sw.Write(poke.moves[j].index + " " + poke.moves[j].PP + " ");
                sw.WriteLine();
            }

            // pokemons in lab
            sw.WriteLine(player.pokemonsLab.Count);
            for (int i = 0; i < player.pokemonsLab.Count; ++i)
            {
                Pokemons poke = player.pokemonsLab[i];
                sw.WriteLine(poke.index + " " + poke.level + " " + poke.gender + " " + poke.name + " " + poke.lovebase
                    + " " + poke.trust + " " + poke.hunger + " " + poke.joy + " " + poke.hp + " " + poke.hpmax + " "
                    + poke.atk + " " + poke.def + " " + poke.spatk + " " + poke.spdef + " " + poke.speed + " " + poke.xp);
                sw.Write(poke.moves.Count + " ");
                for (int j = 0; j < poke.moves.Count; ++j) sw.Write(poke.moves[j].index + " " + poke.moves[j].PP + " ");
                sw.WriteLine();
            }

            // pokeballs
            sw.Write(player.pokeballs.Count + " ");
            for (int i = 0; i < player.pokeballs.Count; ++i)
            {
                Pokeball ball = player.pokeballs[i];
                sw.Write(ball.type + " " + ball.number + " ");
            }
            sw.WriteLine();

            // potions
            sw.Write(player.potions.Count + " ");
            for (int i = 0; i < player.potions.Count; ++i)
            {
                Potion potion = player.potions[i];
                sw.Write(potion.type + " " + potion.number + " ");
            }
            sw.WriteLine();

            // TMs
            sw.Write(player.TMs.Count + " ");
            for (int i = 0; i < player.TMs.Count; ++i)
            {
                sw.Write(player.TMs[i].index + " ");
            }
            sw.WriteLine();

            // evolution items
            sw.Write(player.evolutionItems.Count + " ");
            for (int i = 0; i < player.evolutionItems.Count; ++i)
            {
                sw.Write(player.evolutionItems[i].type + " ");
            }
            sw.WriteLine();

            // map details
            sw.WriteLine(map.mapNumber + " " + (OX - 7) + " " + (OY - 6) + " " + gameTime + " " + Map.lastTimeGym);

            // trainers defeated
            for (int i = 0; i < 200; ++i)
                if (Map.trainerDefeated[i]) sw.Write("1 ");
                else sw.Write("0 ");
            sw.WriteLine();

            // pokemons defeated
            for (int i = 1; i < 494; ++i)
                if (Map.pokemonDefeated[i]) sw.Write("1 ");
                else sw.Write("0 ");
            sw.WriteLine();

            // pokedex
            sw.WriteLine(FormPokedex.seen + " " + FormPokedex.owned);
            for (int i = 1; i < 494; ++i)
                if (FormPokedex.inPokedex[i]) sw.Write("1 ");
                else sw.Write("0 ");
            sw.WriteLine();
            for (int i = 1; i < 494; ++i)
                if (FormPokedex.ownedPokemon[i]) sw.Write("1 ");
                else sw.Write("0 ");
            sw.WriteLine();
            for (int i = 1; i < 494; ++i)
                if (FormPokedex.names[i] == null) sw.Write("- ");
                else sw.Write(FormPokedex.names[i] + " ");
            sw.WriteLine();

            sw.Close();

            MessageBox.Show("Game was saved!", "", MessageBoxButtons.OK);
        }

        private void pbMenuSave_MouseClick(object sender, MouseEventArgs e)
        {
            SaveGame();
            MinimizeMenu();
        }

        private void pbMenuHelp_MouseClick(object sender, MouseEventArgs e)
        {
            ShowHelp();
            MinimizeMenu();
        }

        private void ShowHelp()
        {
            ShowMessage("Press SPACE to close\nUseful tips:"
            + "\n1. You will find pokémons in tall grass. Battle them to train your team or catch them with Poké Balls. It is easier when their HP is low."
            + "\n2. To do an action, press Space. An action can be accessing a computer to build up your team, as the limit of pokémons you can carry with you is 6 and the rest of them go to the laboratory. Another actions are opening the shop, healing your team at a healing center, battle other trainers, etc."
            + "\n3. You also have a menu and shortcuts: P to view your team, badges, D to open the Pokédex, B to open the bag, S to save the game, C, U and G to use Cut, Surf and Dig (if you can)."
            + "\n4. Beat 8 gym leaders to compete in the tournament. Some gyms may require certain types of pokémons, so try to train pokémons of different types."
            + "\n5. Your pokémons will grow up stronger if they feel loved and some may even evolve with love. You must feed them (you will find food winning battles), play with them, in the Team Panel (P button). In the Team Panel you can also change their order. The pokémon in the first box will always be the first engaged in battles. You can earn their trust by winning battles and using them as much as you can."
            + "\n6. There are a few \"secret\" levels with ancient or legendary pokémons. Let's see if you can find them!"
            + "\n7. Good luck and have fun!");
        }


        private void pbMenuMaximize_MouseHover(object sender, EventArgs e)
        {
            panelMenu.Location = new Point(panelMenu.Location.X, 0);
            pbMenuMaximize.Hide();
            minimizePanelMenu = true;
        }

        bool minimizePanelMenu;
        Timer panelMenuCloseTimer = new Timer();

        private void panelMenu_MouseLeave(object sender, EventArgs e)
        {
            panelMenuCloseTimer.Start();
        }

        private void panelMenuCloseTimer_Tick(object sender, EventArgs e)
        {
            panelMenuCloseTimer.Stop();

            if (minimizePanelMenu) MinimizeMenu();
        }

        private void MinimizeMenu()
        {
            panelMenu.Location = new Point(panelMenu.Location.X, -31);
            pbMenuMaximize.Show();
        }

        private void pbMenu_MouseEnter(object sender, EventArgs e)
        {
            minimizePanelMenu = false;
        }

        private void pbMenu_MouseLeave(object sender, EventArgs e)
        {
            minimizePanelMenu = true;
        }

        #endregion
    }
}
