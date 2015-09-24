using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Pokemon
{
    public partial class FormBattle : Form
    {
        #region Initialization

        Timer timerOpacity = new Timer();

        Character player = FormGame.player;

        PictureBox[] pbPokemon = new PictureBox[6];
        PictureBox[] pbHealthRed = new PictureBox[6];
        PictureBox[] pbHealthGreen = new PictureBox[6];
        Button[] attack = new Button[4];
        ToolTip[] tip = new ToolTip[4];

        int crtPokemon;
        Pokemons enemyPokemon;
        Character enemyTrainer;
        bool enemyPokemonEvent;

        public int weatherType = 0; // 1 rainy 2 sunny 3 sandstorm 4 hail
        public int weatherDuration = 0;

        int trickRoom = 0;

        // pokemons engaged in battle that did not faint
        List<Pokemons> notFaintedPokemons = new List<Pokemons>();

        Random rand = new Random();

        public FormBattle(Pokemons poke, bool isEvent)
        {
            InitializeComponent();

            player.usedMasterBall = false;

            enemyPokemonEvent = isEvent;

            enemyPokemon = poke;
            tbHistory.Text = "A wild " + enemyPokemon.name + " appeared!";

            // set temporary stats
            enemyPokemon.SetTemporaryStats();
            enemyPokemon.EraseAffections();

            // see pokemon
            FormPokedex.SeePokemon(poke.index);

            Initialize();
        }

        public FormBattle(Character trainer)
        {
            InitializeComponent();

            enemyTrainer = trainer;
            enemyPokemon = trainer.pokemons[0];
            tbHistory.Text = "You started a pokemon battle with " + trainer.name + "!";

            // set temporary stats
            for (int i = 0; i < trainer.pokemons.Count; ++i)
            {
                trainer.pokemons[i].SetTemporaryStats();
                trainer.pokemons[i].EraseAffections();
            }

            // see pokemon
            for (int i = 0; i < trainer.pokemons.Count; ++i)
            {
                FormPokedex.SeePokemon(trainer.pokemons[i].index);
            }

            Initialize();
        }

        private void Initialize()
        {
            // opacity
            timerOpacity.Interval = FormGame.refreshRate;
            timerOpacity.Tick += timerOpacity_Tick;
            timerOpacity.Start();

            // battlescreen image
            battleScreen.BackgroundImage = new Bitmap(Resources.battlebg, battleScreen.Size);

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

            attackTimer.Interval = 20;
            attackTimer.Tick += attackTimer_Tick;

            waitAITimer.Interval = 500;
            waitAITimer.Tick += waitAITimer_Tick;

            pokeballTimer.Interval = 30;
            pokeballTimer.Tick += pokeballTimer_Tick;

            endBattleTimer.Interval = 3500;
            endBattleTimer.Tick += endBattleTimer_Tick;

            ChangeHealth();

            // hide health bars of empty slots
            for (int i = player.pokemons.Count; i < 6; ++i)
            {
                pbHealthRed[i].Visible = false;
                pbHealthGreen[i].Visible = false;
            }

            // set temporary stats
            for (int i = 0; i < player.pokemons.Count; ++i)
            {
                player.pokemons[i].SetTemporaryStats();
                player.pokemons[i].EraseAffections();
            }

            // choose the first pokemon engaged in battle
            for (int i = 0; i < player.pokemons.Count; ++i)
                if (player.pokemons[i].hp > 0)
                {
                    player.pokemons[i].EarnLove(5, 0, 0);

                    ChangeCrtPokemon(i);
                    break;
                }
        }

        private void FormBattle_Load(object sender, EventArgs e)
        {
            Keyboard.ClearStack();
            FormGame.gameIsPaused = true;
        }

        private void panelAttack_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect = new Rectangle(0, 0, panelAttack.Width, panelAttack.Height);

            LinearGradientBrush brush = new LinearGradientBrush(rect, Color.Black, panelAttack.BackColor, -85);

            e.Graphics.FillRectangle(brush, rect);
        }

        #endregion

        #region Opacity
        void timerOpacity_Tick(object sender, EventArgs e)
        {
            if (this.Opacity >= 1)
            {
                this.Opacity = 1;
                timerOpacity.Stop();
                return;
            }
            this.Opacity += 0.067;
        }
        #endregion

        #region Paint

        PointF
            attackerPoint = new PointF(115, 200),
            defenderPoint = new PointF(400, 100);

        private void battleScreen_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            // draw pokemon sprites
            canvas.DrawImage(Resources.pokeBack[player.pokemons[crtPokemon].index], attackerPoint);

            // draw names and levels
            string pokeString = player.pokemons[crtPokemon].name;

            if (player.pokemons[crtPokemon].gender == 1) pokeString += " ♂ ";
            else pokeString += " ♀ ";

            pokeString += "Lv." + player.pokemons[crtPokemon].level;

            canvas.DrawString(pokeString, new Font("Arial", 7), Brushes.Black, 115, 178);

            // draw health bars
            canvas.FillRectangle(Brushes.Red, 115, 192, 80, 8);
            canvas.FillRectangle(Brushes.Lime, 115, 192, 80 * player.pokemons[crtPokemon].hp / player.pokemons[crtPokemon].hpmax, 8);

            if (!drawBall) // draw enemy pokemon
            {
                canvas.DrawImage(Resources.pokeFront[enemyPokemon.index], defenderPoint);

                pokeString = enemyPokemon.name;

                if (enemyPokemon.gender == 1) pokeString += " ♂ ";
                else pokeString += " ♀ ";

                pokeString += "Lv." + enemyPokemon.level;

                canvas.DrawString(pokeString, new Font("Arial", 7), Brushes.Black, 400, 78);

                canvas.FillRectangle(Brushes.Red, 400, 92, 80, 8);
                canvas.FillRectangle(Brushes.Lime, 400, 92, 80 * enemyPokemon.hp / enemyPokemon.hpmax, 8);
            }
            else // draw pokeball
            {
                canvas.DrawImage(pokeball.image, pokeballPoint);
            }
        }

        #endregion

        #region Changes (Pokemon, Moves, Health)

        void ChangeHealth()
        {
            for (int i = 0; i < player.pokemons.Count; ++i)
            {
                //change image
                pbPokemon[i].Image = Resources.pokeFront[player.pokemons[i].index];

                // change health
                pbHealthGreen[i].Width = Convert.ToInt32(player.pokemons[i].hp * 80 / player.pokemons[i].hpmax);
            }
        }

        bool firstChangeCrtPokemon = true; // check if pokemon is switched of first
        bool justChangeCrtPokemon; // if pokemon is switched, it does not attack that turn

        private void ChangeCrtPokemon(int id)
        {
            if (!notFaintedPokemons.Contains(player.pokemons[id]))
                notFaintedPokemons.Add(player.pokemons[id]);

            crtPokemon = id;

            tbHistory.Text = player.pokemons[crtPokemon].name + " came into battle.\n" + tbHistory.Text;

            ChangeMoves();

            battleScreen.Invalidate();

            // if pokemon is switched, AI takes a turn
            if (!firstChangeCrtPokemon) AI_Turn();

            firstChangeCrtPokemon = false;
            justChangeCrtPokemon = true;
        }

        private void ChangeMoves()
        {
            for (int i = 0; i < 4; ++i)
            {
                if (i < player.pokemons[crtPokemon].moves.Count)
                {
                    Attack tmp = player.pokemons[crtPokemon].moves[i];

                    if (tip[i] != null) tip[i].Dispose();
                    tip[i] = new ToolTip();
                    tip[i].SetToolTip(attack[i], tmp.description);
                    attack[i].Text = tmp.name + "\n" + tmp.PP + "/" + tmp.PPmax;

                    attack[i].BackColor = player.pokemons[crtPokemon].moves[i].color;
                    attack[i].Enabled = true;

                    if (tmp.PP == 0) attack[i].Enabled = false;
                }
                else
                {
                    attack[i].BackColor = Color.BurlyWood;
                    attack[i].Text = "-";
                    attack[i].Enabled = false;
                }
            }
        }

        #endregion

        #region Buttons

        // switch pokemons
        private void pbPokemon_MouseClick(object sender, MouseEventArgs e)
        {
            if (waitTime) return;
            if (!player.pokemons[crtPokemon].canBeSwitched) return;

            for (int i = 0; i < player.pokemons.Count; ++i)
                if (pbPokemon[i] == (PictureBox)sender)
                {
                    if (crtPokemon == i) break;

                    if (player.pokemons[i].hp > 0)
                        ChangeCrtPokemon(i);

                    break;
                }
        }

        bool youAttackFirst;

        // attack buttons
        private void attack_Click(object sender, EventArgs e)
        {
            if (waitTime) return;

            AIfirst = -1;
            justChangeCrtPokemon = false;

            for (int i = 0; i < 4; ++i)
            {
                if (attack[i] == (Button)sender)
                {
                    Attack move = player.pokemons[crtPokemon].moves[i];

                    move.PP -= 1;

                    // weather
                    if (weatherDuration > 0)
                    {
                        if (weatherType == 1)
                            tbHistory.Text = "The rain is heavy!\n" + tbHistory.Text;
                        if (weatherType == 2)
                            tbHistory.Text = "The sun is shining brightly!\n" + tbHistory.Text;
                        if (weatherType == 3)
                            tbHistory.Text = "The sandstorm is raging!\n" + tbHistory.Text;
                        if (weatherType == 4)
                            tbHistory.Text = "Hail falls heavily!\n" + tbHistory.Text;

                        weatherDuration -= 1;

                        if (weatherDuration == 0)
                            weatherType = 0;
                    }

                    // pokemon is trapped
                    if (player.pokemons[crtPokemon].isTrapped > 0)
                        player.pokemons[crtPokemon].isTrapped -= 1;

                    float yourSpeed = player.pokemons[crtPokemon].tspeed,
                        enemySpeed = enemyPokemon.tspeed;

                    // lower speed priority for paralyzed pokemons
                    if (player.pokemons[crtPokemon].isParalyzed > 0)
                        yourSpeed *= 0.75f;
                    if (enemyPokemon.isParalyzed > 0)
                        enemySpeed *= 0.75f;

                    // Trick Room
                    if (trickRoom > 0)
                    {
                        trickRoom -= 1;
                        float aux = yourSpeed;
                        yourSpeed = enemySpeed;
                        enemySpeed = aux;
                    }

                    if (move.index == 453 || move.index == 418 || move.index == 420
                        || move.index == 389 || move.index == 183 || move.index == 410
                        || move.index == 425 || move.index == 245 || move.index == 98)
                        // Attack first: Aqua Jet, Bullet Punch, Ice Shard, Sucker Punch, Mach Punch, Vacuum Wave
                        // Shadow Sneak, Extreme Speed, Quick Attack
                        enemySpeed = -1;

                    if (move.index == 233) // Attack last: Vital Throw
                        yourSpeed = -1;

                    if (yourSpeed >= enemySpeed) // check who is first
                    {
                        youAttackFirst = true;
                        yourPokemonIsAttacking = true;
                        Battle(player.pokemons[crtPokemon], enemyPokemon, player.pokemons[crtPokemon].moves[i]);
                    }
                    else
                    {
                        youAttackFirst = false;
                        AIfirst = i;
                        AI_Turn();
                    }

                    ChangeMoves();

                    break;
                }
            }
        }

        // Bag button
        private void buttonBag_Click(object sender, EventArgs e)
        {
            if (waitTime) return;

            new FormBag(this).ShowDialog();
        }

        // Run button
        private void buttonRun_Click(object sender, EventArgs e)
        {
            if (waitTime) return;

            if (enemyTrainer != null)
            {
                tbHistory.Text = "You can not run away in a trainer battle.\n" + tbHistory.Text;

                return;
            }

            if (player.pokemons[crtPokemon].isTrapped > 0)
            {
                tbHistory.Text = player.pokemons[crtPokemon].name + " is trapped and can not escape!\n" + tbHistory.Text;

                return;
            }

            if (rand.Next() % 100 <= 50)
            {
                isEndBattle = true;

                tbHistory.Text = "You succeeded to escape!\n" + tbHistory.Text;

                waitTime = true;
                endBattleTimer.Start();
            }
            else
            {
                tbHistory.Text = "You failed to escape!\n" + tbHistory.Text;

                justChangeCrtPokemon = true;
                AI_Turn();
            }
        }

        #endregion

        #region Battle

        #region Calculate Damage
        private float CalculateDamage(Pokemons attacker, Pokemons defender, Attack move)
        {
            if (move.category == 3 || move.index == 150) return 0; // Status move or splash

            float basedamage = move.damage;

            #region Effects

            if (move.index == 179 || move.index == 175) // Reversal, Flail
            {
                float x = attacker.hp * 100f / attacker.hpmax;
                if (x > 70) basedamage = 20;
                else if (x > 35) basedamage = 40;
                else if (x > 20) basedamage = 80;
                else if (x > 10) basedamage = 100;
                else if (x > 4) basedamage = 150;
                else basedamage = 200;
            }

            if (move.index == 222) // Magnitude
            {
                int z, x = rand.Next() % 100 + 1;
                if (x <= 5) { z = 4; basedamage = 10; }
                else if (x <= 15) { z = 5; basedamage = 30; }
                else if (x <= 35) { z = 6; basedamage = 50; }
                else if (x <= 65) { z = 7; basedamage = 70; }
                else if (x <= 85) { z = 8; basedamage = 90; }
                else if (x <= 95) { z = 9; basedamage = 110; }
                else { z = 10; basedamage = 150; }

                tbHistory.Text = "Magnitude " + z + "!\n" + tbHistory.Text;
            }

            if (move.index == 67 || move.index == 447) // Low Kick, Grass Knot
            {
                if (defender.weight <= 10) basedamage = 20;
                else if (defender.weight <= 25) basedamage = 40;
                else if (defender.weight <= 50) basedamage = 60;
                else if (defender.weight <= 100) basedamage = 80;
                else if (defender.weight <= 200) basedamage = 100;
                else basedamage = 120;
            }

            if (move.index == 255) // Spit Up
            {
                basedamage = 100 * attacker.Stockpile;
                attacker.Stockpile = 0;
            }

            if (move.index == 218) // Frustration
            {
                basedamage = (300 - attacker.love) / 2.5f;
            }

            if (move.index == 216) // Return
            {
                basedamage = (100 + attacker.love) / 2.5f;
            }

            if (move.index == 360) // Gyro Ball
            {
                basedamage = 25f * defender.tspeed / attacker.tspeed;
            }

            if (move.index == 323 || move.index == 284) // Water Spout, Eruption
            {
                basedamage = attacker.hp * move.damage / attacker.hpmax;
            }

            if (move.index == 462 || move.index == 378) // Crush Grip, Wring Out
            {
                basedamage = defender.hp * move.damage / defender.hpmax;
            }

            if (move.index == 376) // Trump Card
            {
                if (move.PP >= 4) basedamage = 40;
                else if (move.PP == 3) basedamage = 50;
                else if (move.PP == 2) basedamage = 60;
                else if (move.PP == 1) basedamage = 75;
                else if (move.PP == 0) basedamage = 190;
            }

            if (move.index == 386) // Punishment
            {
                float x;
                x = attacker.tatk / attacker.atk;
                while (x > 1.1)
                {
                    x /= 1.4f;
                    basedamage += 20;
                }
                x = attacker.tdef / attacker.def;
                while (x > 1.1)
                {
                    x /= 1.4f;
                    basedamage += 20;
                }
                x = attacker.tspatk / attacker.spatk;
                while (x > 1.1)
                {
                    x /= 1.4f;
                    basedamage += 20;
                }
                x = attacker.tspdef / attacker.spdef;
                while (x > 1.1)
                {
                    x /= 1.4f;
                    basedamage += 20;
                }
                x = attacker.tspeed / attacker.speed;
                while (x > 1.1)
                {
                    x /= 1.4f;
                    basedamage += 20;
                }
            }

            if (move.index == 217) // Present
            {
                int x = rand.Next() % 100 + 1;
                if (x <= 40) basedamage = 40;
                else if (x <= 70) basedamage = 80;
                else if (x <= 80) basedamage = 120;
                else basedamage = 0;
            }
            #endregion

            // base damage formula
            float damage = (2 * attacker.level + 10) * basedamage / 250f;

            // attack category (physical, special)
            switch (move.category)
            {
                case 1: damage *= attacker.tatk * 1f / defender.tdef; break;
                case 2: damage *= attacker.tspatk * 1f / defender.tspdef; break;
            }

            damage += 2;

            // same-type attack bonus
            if (move.type == attacker.type1 || move.type == attacker.type2)
                damage *= 1.5f;

            // effectiveness bonus
            damage *= Resources.effectiveness[move.type][defender.type1] * Resources.effectiveness[move.type][defender.type2];

            // love bonus
            if (attacker.love >= 100)
                damage *= (1 + (attacker.love - 100) / 400f);

            // random factor x [0.85,1]
            damage *= (100 - rand.Next() % 16) / 100f;

            damage = (float)Convert.ToDouble(damage.ToString("n2"));

            if (basedamage == 0 && move.index == 217) // Present
            {
                tbHistory.Text = defender.name + " was healed!\n" + tbHistory.Text;
                defender.hp += 80;
                if (defender.hp > defender.hpmax)
                    defender.hp = defender.hpmax;
                damage = 0;
            }

            return damage;
        }
        #endregion

        #region Pokemon dies

        private void YourPokemonDies(Pokemons poke)
        {
            poke.hp = 0;
            ChangeHealth();

            poke.EarnLove(-5, 0, 0);

            // remove from not fainted list
            notFaintedPokemons.Remove(poke);

            bool found = false;

            // check if another pokemon is still alive
            for (int i = 0; i < player.pokemons.Count; ++i)
                if (player.pokemons[i].hp > 0)
                {
                    found = true;
                    firstChangeCrtPokemon = true;
                    ChangeCrtPokemon(i);
                    break;
                }

            // you lose
            if (!found)
            {
                waitTime = true;

                tbHistory.Text = "You lost the battle!\n" + tbHistory.Text;

                EndBattle(false);
            }
        }

        private void EnemyPokemonDies(Pokemons poke)
        {
            poke.hp = 0;
            ChangeHealth();

            // add win money
            winMoney += (poke.level / 5) * 50;

            // calculate xp
            float exp = poke.level * poke.xpgiven / (7f * notFaintedPokemons.Count);
            if (enemyTrainer != null) exp *= 1.5f; // more xp from trainer battle
            int xp = (int)exp;

            // gain xp for every pokemon engaged in battle that did not faint
            for (int i = 0; i < notFaintedPokemons.Count; ++i)
            {
                notFaintedPokemons[i].EarnLove(5, 0, 0);

                notFaintedPokemons[i].xp += xp;

                while (notFaintedPokemons[i].level < 100 &&
                    notFaintedPokemons[i].xp >= Resources.xpneeded[notFaintedPokemons[i].level])
                {
                    notFaintedPokemons[i].LevelUp();
                }

                tbHistory.Text = notFaintedPokemons[i].name + " gained " + xp + " XP.\n" + tbHistory.Text;
            }

            if (enemyTrainer != null)
                for (int i = 0; i < enemyTrainer.pokemons.Count; ++i)
                {
                    if (enemyTrainer.pokemons[i].hp > 0)
                    {
                        notFaintedPokemons = new List<Pokemons>();
                        notFaintedPokemons.Add(player.pokemons[crtPokemon]);
                        enemyPokemon = enemyTrainer.pokemons[i];
                        battleScreen.Invalidate();
                        battleTurns = 0;
                        return;
                    }
                }

            // you win
            tbHistory.Text = "You won the battle!\n" + tbHistory.Text;
            battleScreen.Invalidate();

            EndBattle(true);

            waitTime = true;
        }

        #endregion

        Attack lastmove;
        private void Battle(Pokemons attacker, Pokemons defender, Attack move)
        {
            tbHistory.Text = "\n" + tbHistory.Text;

            // check if same attack
            if (attacker.sameAttack == 0)
            {
                attacker.sameAttack = -1;

                tbHistory.Text = attacker.name + " became confused!\n" + tbHistory.Text;
                attacker.EraseAffections();
                attacker.isConfused = 3;
            }
            else if (attacker.sameAttack > 0)
            {
                move = attacker.sameAttackDef;
                attacker.sameAttack -= 1;
            }

            if (move.index == 166) // Sketch
            {
                for (int i = 0; i < attacker.moves.Count; ++i)
                    if (attacker.moves[i].index == 166 && attacker.moves[i].PP == 0)
                    {
                        if (lastmove != null)
                            attacker.moves[i] = lastmove;
                        break;
                    }
            }

            lastmove = move;

            #region Affections

            // check for weather damage

            // sandstorm does not damage rock, ground and steel
            if (weatherType == 3 && attacker.type1 != 9 && attacker.type1 != 13 && attacker.type1 != 17
                && attacker.type2 != 9 && attacker.type2 != 13 && attacker.type2 != 17)
            {
                attacker.hp -= attacker.hpmax / 8f;
            }

            // hail does not damage ice
            if (weatherType == 4 && attacker.type1 != 6 && attacker.type2 != 6)
            {
                attacker.hp -= attacker.hpmax / 16f;
            }

            // check if attacker regenerates life
            if (attacker.isRegeneratingLife)
                if (attacker.HealBlock == 0)
                {
                    attacker.hp += attacker.hpmax / 16f;
                    if (attacker.hp > attacker.hpmax)
                        attacker.hp = attacker.hpmax;
                }

            // check if attacker is burnt or poisoned or seeded
            if (attacker.isBurnt > 0 || attacker.isPoisoned > 0 || attacker.isSeeded)
            {
                if (attacker.isBurnt > 0)
                {
                    tbHistory.Text = attacker.name + " is affected by burn!\n" + tbHistory.Text;
                    attacker.isBurnt -= 1;
                    attacker.hp -= attacker.hpmax / 8f;
                }

                if (attacker.isPoisoned > 0)
                {
                    tbHistory.Text = attacker.name + " is affected by poison!\n" + tbHistory.Text;
                    attacker.isPoisoned -= 1;
                    attacker.hp -= attacker.hpmax / 16f;
                }

                if (attacker.isSeeded)
                {
                    tbHistory.Text = attacker.name + " had his HP absorbed!\n" + tbHistory.Text;
                    float x = attacker.hpmax / 12f;
                    attacker.hp -= x;
                    if (defender.HealBlock == 0)
                    {
                        defender.hp += x;
                        if (defender.hp > defender.hpmax)
                            defender.hp = defender.hpmax;
                    }
                }
            }

            // check if pokemon is out of PP
            int PPtotal = 0;
            for (int i = 0; i < attacker.moves.Count; ++i)
            {
                PPtotal += attacker.moves[i].PP;
            }
            if (PPtotal == 0)
            {
                tbHistory.Text = attacker.name + " is out of PPs!\n" + tbHistory.Text;
                attacker.hp = 0;
            }

            bool userCanAttack = true;

            // check if attacker is confused
            if (attacker.isConfused > 0)
            {
                if (rand.Next() % 100 + 1 <= 50)
                {
                    tbHistory.Text = attacker.name + " is confused and hurt itself in confusion!\n" + tbHistory.Text;
                    attacker.hp -= 40;
                    userCanAttack = false;
                }
                attacker.isConfused -= 1;
            }

            ChangeHealth();

            // stop attacking if the attacker is not alive anymore
            if (attacker.hp <= 0)
            {
                if (yourPokemonIsAttacking)
                {
                    YourPokemonDies(attacker);
                }
                else
                {
                    EnemyPokemonDies(attacker);
                }

                return;
            }
            #endregion

            // check if attacker is paralyzed or frozen or asleep or flinch or cannot attack or is attracted
            if (attacker.isParalyzed > 0 || attacker.isFrozen > 0 || attacker.isAsleep > 0 || attacker.Flinch > 0 || attacker.canNotAttack || attacker.isAttracted)
            {
                #region Affections
                if (attacker.isParalyzed > 0)
                {
                    if (rand.Next() % 100 + 1 <= 50)
                    {
                        tbHistory.Text = attacker.name + " is paralyzed!\n" + tbHistory.Text;
                        userCanAttack = false;
                    }
                    attacker.isParalyzed -= 1;
                }
                if (attacker.isFrozen > 0)
                {
                    tbHistory.Text = attacker.name + " is frozen!\n" + tbHistory.Text;
                    attacker.isFrozen -= 1;
                    userCanAttack = false;
                }
                if (attacker.isAsleep > 0)
                {
                    if (rand.Next() % 100 + 1 >= 80) // wake up earlier
                    {
                        attacker.isAsleep = 0;
                        attacker.Nightmare = false;
                    }
                    else
                    {
                        if (attacker.Nightmare)
                        {
                            attacker.hp *= 0.75f;
                            tbHistory.Text = attacker.name + " is having a horrible nightmare!\n" + tbHistory.Text;
                        }

                        tbHistory.Text = attacker.name + " is asleep!\n" + tbHistory.Text;
                        attacker.isAsleep -= 1;

                        if (move.index != 173) // you can use Snore while asleep
                            userCanAttack = false;
                    }

                    if (attacker.isAsleep == 0)
                    {
                        tbHistory.Text = attacker.name + " woke up!\n" + tbHistory.Text;
                    }
                }
                if (attacker.Flinch > 0)
                {
                    tbHistory.Text = attacker.name + " flinched and couldn't move!\n" + tbHistory.Text;
                    attacker.Flinch -= 1;
                    userCanAttack = false;
                }
                if (attacker.canNotAttack)
                {
                    tbHistory.Text = attacker.name + " can not attack this turn!\n" + tbHistory.Text;
                    attacker.canNotAttack = false;
                    userCanAttack = false;
                }
                if (attacker.isAttracted)
                {
                    if (rand.Next() % 100 + 1 < 30)
                    {
                        tbHistory.Text = attacker.name + " is in love with " + defender.name + "!\n" + tbHistory.Text;
                        userCanAttack = false;
                    }
                }
                #endregion
            }

            if (defender.MagnetRise > 0)
                defender.MagnetRise -= 1;

            // two turn attack was intrerupted
            if (!userCanAttack && attacker.twoTurnAttack != -1)
                attacker.twoTurnAttack = -1;

            // Mist
            if (defender.isMist > 0)
                defender.isMist -= 1;

            // Reflect
            if (defender.Reflect > 0)
                defender.Reflect -= 1;

            // Light Screen
            if (defender.LightScreen > 0)
                defender.LightScreen -= 1;

            // Safe Guard
            if (defender.isSafeGuard > 0)
                defender.isSafeGuard -= 1;

            // Heal Block
            if (attacker.HealBlock > 0)
                attacker.HealBlock -= 1;

            if (userCanAttack)
            {
                if (attacker.twoTurnAttack != -1)
                {
                    tbHistory.Text = attacker.name + " dealt " + attacker.twoTurnAttack + " damage!\n" + tbHistory.Text;
                    defender.hp -= attacker.twoTurnAttack;

                    attacker.twoTurnAttack = -1;

                    ChangeHealth();

                    if (defender.hp <= 0)
                    {
                        if (!yourPokemonIsAttacking)
                        {
                            YourPokemonDies(defender);
                        }
                        else
                        {
                            EnemyPokemonDies(defender);
                        }
                        return;
                    }
                }
                else
                {
                    // check if attack miss

                    bool attackMissed = true;

                    if (rand.Next() % 100 + 1 <= move.accuracy * attacker.accuracy / defender.evasiveness)
                        attackMissed = false;

                    if (move.index == 87) // Thunder's accuracy depends on weather
                    {
                        int thunderAccuracy = move.accuracy;
                        if (weatherType == 1) thunderAccuracy = 100;
                        if (weatherType == 2) thunderAccuracy = 50;

                        attackMissed = true;
                        if (rand.Next() % 100 + 1 <= thunderAccuracy && rand.Next() % 100 + 1 <= attacker.accuracy)
                            attackMissed = false;
                    }

                    if (move.index == 351 || move.index == 443 || move.index == 185
                        || move.index == 396 || move.index == 233 || move.index == 332
                        || move.index == 325 || move.index == 345 || move.index == 129)
                        // Never miss: Shock Wave, Magnet Bomb, Faint Attack, Aura Sphere, Vital Throw, Aerial Ace
                        // Shadow Punch, Magical Leaf, Swift
                        attackMissed = false;

                    if (!attackMissed)
                    {
                        if (move.type == 2 && defender.isFrozen > 0) // Fire-type moves defrost defender
                        {
                            tbHistory.Text = move.name + " dispelled the frozen effect from " + defender.name + "!\n" + tbHistory.Text;
                            defender.isFrozen = 0;
                        }

                        if (move.index == 210) // Fury Cutter
                        {
                            if (attacker.FuryCutter < 5)
                                attacker.FuryCutter += 1;
                        }
                        else attacker.FuryCutter = 0;

                        if (move.category != 3) // Physical, Special moves
                        {
                            float damage = CalculateDamage(attacker, defender, move);

                            #region Effects

                            int criticalChance = attacker.critical;

                            if (move.index == 75 || move.index == 2 || move.index == 152
                                || move.index == 444 || move.index == 440 || move.index == 342
                                || move.index == 460 || move.index == 454 || move.index == 400
                                || move.index == 238 || move.index == 299 || move.index == 177
                                || move.index == 314 || move.index == 421 || move.index == 163
                                || move.index == 348 || move.index == 427)
                                // Critical chance: Razor Leaf, Karate Chop, Crabhammer, Stone Edge, Cross Poison, 
                                // Poison Tail, Special Rend, Attack Order, Night Slash, Cross Chop, Blaze Kick,
                                // Aeroblast, Air Cutter, Shadow Claw, Slash, Leaf Blade, Psycho Cut
                                criticalChance += 20;

                            if (attacker.isCritChanted)
                                criticalChance = 0;

                            if (rand.Next() % 100 + 1 <= criticalChance)
                            {
                                tbHistory.Text = "Critical Hit!\n" + tbHistory.Text;
                                damage *= 2;
                            }

                            if (weatherType == 1 && move.type == 3)
                                damage *= 1.125f;

                            if (weatherType == 2 && move.type == 2)
                                damage *= 1.125f;

                            if (defender.isWaterSported && move.type == 2)
                                damage *= 0.75f;

                            if (attacker.isCharged)
                            {
                                attacker.isCharged = false;
                                if (move.type == 4) damage *= 1.5f;
                            }

                            if (move.index == 387) // Last Resort
                            {
                                bool tmp = true;
                                for (int i = 0; i < attacker.moves.Count; ++i)
                                {
                                    if (attacker.moves[i].index != 387 && attacker.moves[i].PP > 0)
                                        tmp = false;
                                }
                                if (!tmp) damage = 0;
                            }

                            if (move.index == 263) // Bonus for affections: Facade
                            {
                                if (attacker.isPoisoned > 0 || attacker.isParalyzed > 0 || attacker.isBurnt > 0)
                                    damage *= 2;
                            }

                            if (move.index == 283) // Enemy life = your life: Endeavor
                            {
                                if (defender.hp > attacker.hp) damage = defender.hp - attacker.hp;
                                else damage = 0;
                            }

                            if (move.index == 12 || move.index == 32 || move.index == 329
                                || move.index == 90) // K.O.: Guillotine, Horn Drill, Sheer Cold, Fissure
                            {
                                damage = defender.hpmax;
                            }

                            if (move.index == 362) // Double attack if hp lower than half: Brine
                                if (defender.hp <= defender.hpmax / 2f)
                                {
                                    damage *= 2;
                                }

                            if (move.index == 24 || move.index == 41 || move.index == 155
                                || move.index == 458) // 2 attacks: Double Kick, Twineedle, Bonemerang, Double Hit
                            {
                                int hits = 2;
                                tbHistory.Text = move.name + " hit " + hits + " times!\n" + tbHistory.Text;
                                damage *= hits;
                            }

                            if (move.index == 167) // 1-3 attacks: Triple Kick
                            {
                                int hits = rand.Next() % 2 + 1;
                                tbHistory.Text = move.name + " hit " + hits + " times!\n" + tbHistory.Text;
                                damage *= hits;
                            }

                            if (move.index == 3 || move.index == 4 || move.index == 31
                                || move.index == 42 || move.index == 350 || move.index == 333
                                || move.index == 292 || move.index == 331 || move.index == 198
                                || move.index == 140 || move.index == 154 || move.index == 131)
                            // 2-5 attacks: Double Slap, Comet Punch, Fury Attack, Pin Missile, Rock Blast, 
                            // Icicle Spear, Arm Thrust, Bullet Seed, Bone Rush, Barrage, Fury Swipes, Spike Cannon
                            {
                                int hits = rand.Next() % 4 + 2;
                                tbHistory.Text = move.name + " hit " + hits + " times!\n" + tbHistory.Text;
                                damage *= hits;
                            }

                            if (move.index == 71 || move.index == 72 || move.index == 141
                                || move.index == 409 || move.index == 202)
                                // Life drain: Absorb, Mega Drain, Leech Life, Drain Punch, Giga Drain
                                if (attacker.HealBlock == 0)
                                {
                                    attacker.hp += damage * 0.5f;
                                    if (attacker.hp > attacker.hpmax)
                                        attacker.hp = attacker.hpmax;
                                }

                            if (move.index == 138) // Attack while enemy asleep: Dream Eater
                            {
                                if (defender.isAsleep > 0)
                                {
                                    if (attacker.HealBlock == 0)
                                    {
                                        attacker.hp += damage * 0.5f;
                                        if (attacker.hp > attacker.hpmax)
                                            attacker.hp = attacker.hpmax;
                                    }
                                }
                                else damage = 0;
                            }

                            if (move.index == 173) // Attack while asleep: Snore
                            {
                                if (attacker.isAsleep > 0)
                                {
                                    if (rand.Next() % 100 + 1 <= 10)
                                    {
                                        defender.Flinch = 1;
                                    }
                                }
                                else damage = 0;
                            }

                            if (move.index == 69 || move.index == 101 || move.index == 149)
                                // damage = level: Seismic Toss, Night Shade, Psywave
                                damage = attacker.level;

                            if (move.index == 149) // Bonus 1-1.5: Psywave
                                damage *= (rand.Next() % 6 + 10) / 10f;

                            if (move.index == 265) // Bonus if enemy paralyzed: Smelling Salts
                            {
                                if (defender.isParalyzed > 0)
                                    damage *= 2;
                            }

                            if (move.index == 162) // 1/2 HP damage: Super Fang
                                damage = defender.hp * 0.5f;

                            if (move.index == 82) // 40 damage: Dragon Rage
                                damage = 40;

                            if (move.index == 49) // 20 damage: Sonic Boom
                                damage = 20;

                            if (move.index == 6) // Bonus money: Pay Day
                                winMoney += 100;

                            if (move.index == 264) // Attack second - failed attack: Focus Punch
                                if (!youAttackFirst)
                                {
                                    damage = 0;
                                    tbHistory.Text = attacker.name + " flinched before attacking!\n" + tbHistory.Text;
                                }

                            if (move.index == 371 || move.index == 228 || move.index == 279
                                || move.index == 372 || move.index == 419)
                                // Attack second - double damage: Payback, Pursuit, Revenge, Assurance, Avalanche
                                if (!youAttackFirst) damage *= 2;

                            if (move.index == 63 || move.index == 308 || move.index == 439
                                || move.index == 459 || move.index == 307 || move.index == 338
                                || move.index == 416)
                            // Cannot attack next turn: Hyper Beam, Hydro Cannon, Rock Wrecker, Roar of Time, 
                            // Blast Burn, Frenzy Plant, Giga Impact
                            {
                                attacker.canNotAttack = true;
                            }

                            if (defender.isSafeGuard == 0)
                            {
                                #region Affections

                                if (move.index == 7 || move.index == 52 || move.index == 53
                                    || move.index == 299 || move.index == 126 || move.index == 424
                                    || move.index == 257 || move.index == 436 || move.index == 221
                                    || move.index == 161)
                                    // Burn: Fire Punch, Ember, Flamethrower, Blaze Kick, Fire Blast, Fire Fang, 
                                    // Heat Wave, Sacred Fire, Tri Attack
                                    if (rand.Next() % 100 + 1 <= 10 && defender.type1 != 2 && defender.type2 != 2)
                                    {
                                        tbHistory.Text = defender.name + " was burnt!\n" + tbHistory.Text;
                                        defender.EraseAffections();
                                        defender.isBurnt = 3;
                                    }

                                if (move.index == 40 || move.index == 41 || move.index == 440 || move.index == 441
                                    || move.index == 305 || move.index == 398 || move.index == 342 || move.index == 124
                                    || move.index == 188)
                                    // Poison: Poison Sting, Twineedle, Cross Poison, Gunk Shot, Poison Fang, Poison Jab
                                    // Poison Tail, Sludge, Sludge Bomb
                                    if (rand.Next() % 100 + 1 <= 10 && defender.type1 != 8 && defender.type2 != 8
                                        && defender.type1 != 17 && defender.type2 != 17)
                                    {
                                        tbHistory.Text = defender.name + " was poisoned!\n" + tbHistory.Text;
                                        defender.EraseAffections();
                                        defender.isPoisoned = 3;
                                    }

                                if (move.index == 8 || move.index == 58 || move.index == 59
                                    || move.index == 423 || move.index == 181 || move.index == 161)
                                    // Freeze: Ice Punch, Ice Beam, Blizzard, Ice Fang, Powder Snow, Tri Attack
                                    if (rand.Next() % 100 + 1 <= 10 && defender.type1 != 6 && defender.type2 != 6)
                                    {
                                        tbHistory.Text = defender.name + " was frozen!\n" + tbHistory.Text;
                                        defender.EraseAffections();
                                        defender.isFrozen = 3;
                                    }

                                if (move.index == 60 || move.index == 352 || move.index == 324
                                    || move.index == 448 || move.index == 93 || move.index == 146
                                    || move.index == 431)
                                    // Confusion: Psybeam, Water Pulse, Signal Beam, Chatter, Confusion, Dizzy Punch,
                                    // Rock Climb
                                    if (rand.Next() % 100 + 1 <= 10)
                                    {
                                        tbHistory.Text = defender.name + " became confused!\n" + tbHistory.Text;
                                        defender.EraseAffections();
                                        defender.isConfused = 3;
                                    }

                                if (move.index == 223) // Confusion 100%: Dynamic Punch
                                {
                                    tbHistory.Text = defender.name + " became confused!\n" + tbHistory.Text;
                                    defender.EraseAffections();
                                    defender.isConfused = 3;
                                }

                                if (move.index == 9 || move.index == 34 || move.index == 84
                                    || move.index == 85 || move.index == 87 || move.index == 435
                                    || move.index == 209 || move.index == 422 || move.index == 344
                                    || move.index == 225 || move.index == 395 || move.index == 340
                                    || move.index == 122 || move.index == 161)
                                    // Paralyze: Thunder Punch, Body Slam, Thunder Shock, Thunderbolt, Thunder
                                    // Discharge, Spark, Thunder Fang, Volt Tackle, Dragon Breath, Force Palm,
                                    // Bounce, Lick, Tri Attack
                                    if (rand.Next() % 100 + 1 <= 10 && defender.type1 != 4 && defender.type2 != 4)
                                    {
                                        tbHistory.Text = defender.name + " was paralyzed!\n" + tbHistory.Text;
                                        defender.EraseAffections();
                                        defender.isParalyzed = 3;
                                    }

                                if (move.index == 192) // Paralyze 100%: Zap Cannon
                                    if (defender.type1 != 4 && defender.type2 != 4)
                                    {
                                        tbHistory.Text = defender.name + " was paralyzed!\n" + tbHistory.Text;
                                        defender.EraseAffections();
                                        defender.isParalyzed = 3;
                                    }

                                #endregion
                            }

                            if (move.index == 23 || move.index == 27 || move.index == 29
                                || move.index == 44 || move.index == 422 || move.index == 127
                                || move.index == 442 || move.index == 157 || move.index == 407
                                || move.index == 423 || move.index == 399 || move.index == 424
                                || move.index == 403 || move.index == 310 || move.index == 302
                                || move.index == 125 || move.index == 326 || move.index == 428
                                || move.index == 158)
                                // Flinch: Stomp, Rolling Kick, Headbutt, Bite, Thunder Fang, Waterfall
                                // Iron Head, Rock Slide, Dragon Rush, Ice Fang, Dark Pulse, Fire Fang,
                                // Air Slash, Sky Attack, Astonish, Needle Arm, Bone Club, Extrasensory,
                                // Zen Headbutt, Hyper Fang
                                if (rand.Next() % 100 + 1 <= 10)
                                {
                                    defender.Flinch = 1;
                                }

                            if (defender.isMist == 0)
                            {
                                #region Stats Reduction

                                if (move.index == 51) // Decrease attack: Aurora Beam
                                    if (rand.Next() % 100 + 1 <= 10)
                                    {
                                        if (defender.tatk <= defender.tatk * 0.41f)
                                        {
                                            tbHistory.Text = defender.name + "'s attack can not be decreased anymore.\n" + tbHistory.Text;
                                        }
                                        else
                                        {
                                            defender.tatk *= 0.8f;
                                            tbHistory.Text = defender.name + " had his attack lowered.\n" + tbHistory.Text;
                                        }
                                    }

                                if (move.index == 296) // Decrease special attack: Mist Ball
                                    if (rand.Next() % 100 + 1 <= 10)
                                    {
                                        if (defender.tspatk <= defender.spatk * 0.41f)
                                        {
                                            tbHistory.Text = defender.name + "'s special attack can not be decreased anymore.\n" + tbHistory.Text;
                                        }
                                        else
                                        {
                                            defender.tspatk *= 0.8f;
                                            tbHistory.Text = defender.name + " had his special attack lowered.\n" + tbHistory.Text;
                                        }
                                    }

                                if (move.index == 231 || move.index == 242 || move.index == 249
                                    || move.index == 306) // Decrease defense: Iron Tail, Crunch, Rock Smash, Crush Claw
                                    if (rand.Next() % 100 + 1 <= 10)
                                    {
                                        if (defender.tdef <= defender.def * 0.41f)
                                        {
                                            tbHistory.Text = defender.name + "'s defense can not be decreased anymore.\n" + tbHistory.Text;
                                        }
                                        else
                                        {
                                            defender.tdef *= 0.8f;
                                            tbHistory.Text = defender.name + " had his defense lowered.\n" + tbHistory.Text;
                                        }
                                    }

                                if (move.index == 370) // Decrease defense 100%: Close Combat
                                {
                                    if (defender.tdef <= defender.def * 0.41f)
                                    {
                                        tbHistory.Text = defender.name + "'s defense can not be decreased anymore.\n" + tbHistory.Text;
                                    }
                                    else
                                    {
                                        defender.tdef *= 0.8f;
                                        tbHistory.Text = defender.name + " had his defense lowered.\n" + tbHistory.Text;
                                    }
                                }

                                if (move.index == 330 || move.index == 190 || move.index == 429
                                    || move.index == 426 || move.index == 189)
                                    // Decrease accuracy: Muddy Water, Octazooka, Mirror Shot, Mud Bomb, Mud Slap
                                    if (rand.Next() % 100 + 1 <= 10)
                                    {
                                        if (defender.accuracy <= 41)
                                        {
                                            tbHistory.Text = defender.name + "'s accuracy can not be decreased anymore.\n" + tbHistory.Text;
                                        }
                                        else
                                        {
                                            defender.accuracy *= 0.8f;
                                            tbHistory.Text = defender.name + " had his accuracy lowered.\n" + tbHistory.Text;
                                        }
                                    }

                                if (move.index == 51 || move.index == 430 || move.index == 405
                                    || move.index == 411 || move.index == 247 || move.index == 412
                                    || move.index == 414 || move.index == 295 || move.index == 94)
                                    // Decrease special defense: Acid, Flash Cannon, Bug Buzz, Focus Blast, 
                                    // Shadow Ball, Energy Ball, Earth Power, Luster Purge, Psychic
                                    if (rand.Next() % 100 + 1 <= 10)
                                    {
                                        if (defender.tspdef <= defender.spdef * 0.41f)
                                        {
                                            tbHistory.Text = defender.name + "'s special defense can not be decreased anymore.\n" + tbHistory.Text;
                                        }
                                        else
                                        {
                                            defender.tspdef *= 0.8f;
                                            tbHistory.Text = defender.name + " had his special defense lowered.\n" + tbHistory.Text;
                                        }
                                    }

                                if (move.index == 465) // Decrease special defense x2: Seed Flare
                                    if (rand.Next() % 100 + 1 <= 10)
                                    {
                                        if (defender.tspdef <= defender.spdef * 0.41f)
                                        {
                                            tbHistory.Text = defender.name + "'s special defense can not be decreased anymore.\n" + tbHistory.Text;
                                        }
                                        else
                                        {
                                            defender.tspdef *= 0.64f;
                                            tbHistory.Text = defender.name + " had his special defense lowered.\n" + tbHistory.Text;
                                        }
                                    }

                                if (move.index == 370) // Decrease special defense 100%: Close Combat
                                {
                                    if (defender.tspdef <= defender.spdef * 0.41f)
                                    {
                                        tbHistory.Text = defender.name + "'s special defense can not be decreased anymore.\n" + tbHistory.Text;
                                    }
                                    else
                                    {
                                        defender.tspdef *= 0.8f;
                                        tbHistory.Text = defender.name + " had his special defense lowered.\n" + tbHistory.Text;
                                    }
                                }

                                if (move.index == 61 || move.index == 145 || move.index == 317
                                    || move.index == 196 || move.index == 132 || move.index == 341)
                                    // Decrease speed: Bubble Beam, Bubble, Rock Tomb, Icy Wind, Constrict, Mud Shot
                                    if (rand.Next() % 100 + 1 <= 10)
                                    {
                                        if (defender.tspeed <= defender.speed * 0.41f)
                                        {
                                            tbHistory.Text = defender.name + "'s speed can not be decreased anymore.\n" + tbHistory.Text;
                                        }
                                        else
                                        {
                                            defender.tspeed *= 0.8f;
                                            tbHistory.Text = defender.name + " had his speed lowered.\n" + tbHistory.Text;
                                        }
                                    }

                                #endregion
                            }

                            #region Stats Raised

                            if (move.index == 130) // Raised defense 100%: Skull Bash
                            {
                                if (attacker.tdef >= 3.8f * attacker.def)
                                {
                                    tbHistory.Text = attacker.name + "'s defense can not be increased anymore!\n" + tbHistory.Text;
                                }
                                else
                                {
                                    attacker.tdef *= 1.4f;
                                    tbHistory.Text = attacker.name + "'s defense rose!\n" + tbHistory.Text;
                                }
                            }

                            if (move.index == 451) // Raised special attack: Charge Beam
                                if (rand.Next() % 100 + 1 <= 10)
                                {
                                    if (attacker.tspatk >= 3.8f * attacker.spatk)
                                    {
                                        tbHistory.Text = attacker.name + "'s special attack can not be increased anymore!\n" + tbHistory.Text;
                                    }
                                    else
                                    {
                                        attacker.tspatk *= 1.4f;
                                        tbHistory.Text = attacker.name + "'s special attack rose!\n" + tbHistory.Text;
                                    }
                                }

                            if (move.index == 232 || move.index == 309) // Raised attack: Metal Claw, Meteor Mash
                                if (rand.Next() % 100 + 1 <= 10)
                                {
                                    if (attacker.tatk >= 3.8f * attacker.atk)
                                    {
                                        tbHistory.Text = attacker.name + "'s attack can not be increased anymore!\n" + tbHistory.Text;
                                    }
                                    else
                                    {
                                        attacker.tatk *= 1.4f;
                                        tbHistory.Text = attacker.name + "'s attack rose!\n" + tbHistory.Text;
                                    }
                                }

                            if (move.index == 99) // Raised attack 100%: Rage
                            {
                                if (attacker.tatk >= 3.8f * attacker.atk)
                                {
                                    tbHistory.Text = attacker.name + "'s attack can not be increased anymore!\n" + tbHistory.Text;
                                }
                                else
                                {
                                    attacker.tatk *= 1.4f;
                                    tbHistory.Text = attacker.name + "'s attack rose!\n" + tbHistory.Text;
                                }
                            }

                            if (move.index == 246 || move.index == 318) // Raised all stats: Ancient Power, Silver Wind
                                if (rand.Next() % 100 + 1 <= 10)
                                {
                                    if (attacker.tatk <= 3.8f * attacker.atk) attacker.tatk *= 1.4f;
                                    if (attacker.tdef <= 3.8f * attacker.atk) attacker.tdef *= 1.4f;
                                    if (attacker.tspatk <= 3.8f * attacker.atk) attacker.tspatk *= 1.4f;
                                    if (attacker.tspdef <= 3.8f * attacker.atk) attacker.tspdef *= 1.4f;
                                    if (attacker.tspeed >= 3.8f * attacker.speed) attacker.tspeed *= 1.4f;

                                    tbHistory.Text = attacker.name + " had his stats increased!\n" + tbHistory.Text;
                                }

                            #endregion

                            if (move.index == 434 || move.index == 315 || move.index == 437
                                || move.index == 354)
                            // Recoil - decrease special attack x2: Draco Meteor, Overheat, Leaf Storm, Psycho Boost
                            {
                                if (attacker.tspatk >= attacker.spatk * 0.41f)
                                    attacker.tspatk *= 0.64f;
                            }

                            if (move.index == 359) // Recoil - decrease speed: Hammer Arm
                            {
                                if (attacker.tspeed >= attacker.speed * 0.41f)
                                    attacker.tspeed *= 0.8f;
                            }

                            if (move.index == 276) // Recoil - decrease attack: Superpower
                            {
                                if (attacker.tatk >= attacker.atk * 0.41f)
                                    attacker.tatk *= 0.64f;
                            }

                            if (move.index == 276) // Recoil - decrease defense: Superpower
                            {
                                if (attacker.tdef >= attacker.def * 0.41f)
                                    attacker.tdef *= 0.64f;
                            }

                            if (move.index == 36) // Self-damage: Take Down
                            {
                                attacker.hp *= 0.84f;
                            }

                            if (move.index == 38 || move.index == 344 || move.index == 457
                                || move.index == 394 || move.index == 413 || move.index == 452)
                            // Self-damage: Double-Edge, Volt Tackle, Head Smash, Flare Blitz, Brave Bird, Wood Hammer
                            {
                                attacker.hp *= 0.67f;
                            }

                            if (move.index == 66) // Self-damage: Submission
                            {
                                attacker.hp -= damage * 0.25f;
                                if (attacker.hp <= 1) attacker.hp = 1;
                            }

                            if (move.index == 20 || move.index == 35 || move.index == 83
                                || move.index == 128 || move.index == 250 || move.index == 463
                                || move.index == 328)
                            // Trapped: Bind, Wrap, Fire Spin, Clamp, Whirlpool, Magma Storm, Sand Tomb
                            {
                                int x = rand.Next() % 4 + 2;
                                if (x > defender.isTrapped) defender.isTrapped = x;
                            }

                            if (move.index == 358) // Wake-up Slap
                                if (defender.isAsleep > 0)
                                {
                                    damage *= 2;
                                    defender.isAsleep = 0;
                                    tbHistory.Text = defender.name + " woke up!\n" + tbHistory.Text;
                                }

                            if (move.index == 280) // Destroy LightScreen/Reflect: Brick Break
                            {
                                defender.LightScreen = 0;
                                defender.Reflect = 0;
                            }

                            if (move.index == 229) // Free from trapped: Rapid Spin
                                attacker.isTrapped = 0;

                            if (move.index == 229) // Free from Leech Seed: Rapid Spin
                                attacker.isSeeded = false;

                            if (move.index == 210) // Fury Cutter
                            {
                                damage *= attacker.FuryCutter;
                            }

                            if (move.type == 9 && defender.MagnetRise > 0) // Magnet Rise: Immune to ground-attacks
                            {
                                damage = 0;
                            }

                            if (move.index == 205 || move.index == 301)
                            // Same attack 5 turns - stronger: Rollout, Ice Ball
                            {
                                if (attacker.sameAttack == -1)
                                {
                                    attacker.sameAttack = 4;
                                    attacker.sameAttackDef = move;
                                }
                                else
                                {
                                    damage += damage * (4 - attacker.sameAttack) * 0.5f;
                                }
                            }

                            if (move.index == 80 || move.index == 37 || move.index == 200
                                || move.index == 353)
                                // Same attack 2-3 turns: Petal Dance, Thrash, Outrage, Doom Desire
                                if (attacker.sameAttack == -1)
                                {
                                    attacker.sameAttack = rand.Next() % 2 + 1;
                                    attacker.sameAttackDef = move;
                                }

                            if (move.category == 1 && defender.Reflect > 0) // Reflect
                                damage *= 0.5f;

                            if (move.category == 2 && defender.LightScreen > 0) // Light Screen
                                damage *= 0.5f;

                            if (move.index == 467 || move.index == 19 || move.index == 291
                                || move.index == 91)
                            // Protected for the next attack: Shadow Force, Fly, Dive, Dig
                            {
                                attacker.isProtected = true;
                            }

                            if (move.index == 76 || move.index == 467 || move.index == 19
                                || move.index == 291 || move.index == 13 || move.index == 130
                                || move.index == 91)
                            // Two-turn attack: Solar Beam, Shadow Force, Fly, Dive, Razor Wind, Skull Bash, Dig
                            {
                                if (move.index == 13 && rand.Next() % 100 + 1 <= 25) // Razor Wind
                                    damage *= 2;

                                attacker.twoTurnAttack = damage;
                                tbHistory.Text = attacker.name + " used " + move.name + ".\n" + tbHistory.Text;
                            }

                            if (move.index == 153 || move.index == 120) // Faint: Explosion, Self-Destruct
                            {
                                attacker.hp = 0;

                                if (yourPokemonIsAttacking)
                                {
                                    YourPokemonDies(attacker);
                                }
                                else
                                {
                                    EnemyPokemonDies(attacker);
                                }
                            }

                            if (move.index == 206) // Enemy can not faint
                            {
                                if (defender.hp - damage < 1) damage = defender.hp - 1;
                            }

                            #endregion

                            if (!defender.isProtected)
                            {
                                if (attacker.twoTurnAttack == -1)
                                {
                                    defender.hp -= damage;
                                    tbHistory.Text = attacker.name + " used " + move.name + " and dealt " + damage + " damage.\n" + tbHistory.Text;
                                }
                            }
                            else
                            {
                                tbHistory.Text = attacker.name + " used " + move.name + ".\n" + tbHistory.Text;
                                tbHistory.Text = defender.name + " was protected.\n" + tbHistory.Text;
                                defender.isProtected = false;
                            }

                            ChangeHealth();

                            if (defender.hp <= 0)
                            {
                                if (!yourPokemonIsAttacking)
                                {
                                    YourPokemonDies(defender);
                                }
                                else
                                {
                                    EnemyPokemonDies(defender);
                                    return;
                                }
                            }

                        } // Status moves
                        else
                        {
                            tbHistory.Text = attacker.name + " used " + move.name + "!\n" + tbHistory.Text;

                            #region Effects

                            if (defender.isSafeGuard == 0)
                            {
                                #region Affections
                                if (move.index == 47 || move.index == 79 || move.index == 464
                                    || move.index == 320 || move.index == 147 || move.index == 95
                                    || move.index == 142)
                                // Sleep: Sing, Sleep Powder, Dark Void, Grass Whistle, Spore, Hypnosis, Lovely Kiss
                                {
                                    tbHistory.Text = defender.name + " fell asleep!\n" + tbHistory.Text;
                                    defender.EraseAffections();
                                    defender.isAsleep = 3;
                                }

                                if (move.index == 281) // Sleep both: Yawn
                                {
                                    tbHistory.Text = defender.name + " fell asleep!\n" + tbHistory.Text;
                                    defender.EraseAffections();
                                    defender.isAsleep = 1;

                                    tbHistory.Text = attacker.name + " fell asleep!\n" + tbHistory.Text;
                                    attacker.EraseAffections();
                                    attacker.isAsleep = 1;
                                }

                                if (move.index == 48) // Confusion: Supersonic
                                    if (rand.Next() % 100 + 1 <= 75)
                                    {
                                        tbHistory.Text = defender.name + " became confused!\n" + tbHistory.Text;
                                        defender.EraseAffections();
                                        defender.isConfused = 3;
                                    }

                                if (move.index == 260 || move.index == 109 || move.index == 207
                                    || move.index == 186 || move.index == 298)
                                // Confusion 100%: Flatter, Confuse Ray, Swagger, Sweet Kiss, Teeter Dance
                                {
                                    tbHistory.Text = defender.name + " became confused!\n" + tbHistory.Text;
                                    defender.EraseAffections();
                                    defender.isConfused = 3;
                                }

                                if (move.index == 298) // Self-Confusion: Teeter Dance
                                {
                                    tbHistory.Text = attacker.name + " became confused!\n" + tbHistory.Text;
                                    attacker.EraseAffections();
                                    attacker.isConfused = 3;
                                }

                                if (move.index == 77 || move.index == 139) // Poison: Poison Powder, Poison Gas
                                    if (rand.Next() % 100 + 1 <= 75 && defender.type1 != 8 && defender.type2 != 8
                                        && defender.type1 != 17 && defender.type2 != 17)
                                    {
                                        tbHistory.Text = defender.name + " was poisoned!\n" + tbHistory.Text;
                                        defender.EraseAffections();
                                        defender.isPoisoned = 3;
                                    }

                                if (move.index == 92) // Poison 100%: Toxic
                                    if (defender.type1 != 8 && defender.type2 != 8 && defender.type1 != 17 && defender.type2 != 17)
                                    {
                                        tbHistory.Text = defender.name + " was poisoned!\n" + tbHistory.Text;
                                        defender.EraseAffections();
                                        defender.isPoisoned = 3;
                                    }

                                if (move.index == 261) // Burn: Will-o-wisp
                                    if (defender.type1 != 2 && defender.type2 != 2)
                                    {
                                        tbHistory.Text = defender.name + " was burnt!\n" + tbHistory.Text;
                                        defender.EraseAffections();
                                        defender.isBurnt = 3;
                                    }

                                if (move.index == 78) // Paralyze: Stun Spore
                                    if (rand.Next() % 100 + 1 <= 75 && defender.type1 != 4 && defender.type2 != 4)
                                    {
                                        tbHistory.Text = defender.name + " was paralyzed!\n" + tbHistory.Text;
                                        defender.EraseAffections();
                                        defender.isParalyzed = 3;
                                    }

                                if (move.index == 86 || move.index == 137) // Paralyze 100%: Thunder Wave, Glare
                                    if (defender.type1 != 4 && defender.type2 != 4)
                                    {
                                        tbHistory.Text = defender.name + " was paralyzed!\n" + tbHistory.Text;
                                        defender.EraseAffections();
                                        defender.isParalyzed = 3;
                                    }
                                #endregion
                            }

                            if (move.index == 182 || move.index == 197 || move.index == 203) // Protected for the next attack: Protect, Detect, Endure
                                if (rand.Next() % 100 + 1 <= 100 / move.PPmax * (move.PP + 1))
                                {
                                    attacker.isProtected = true;
                                }

                            if (move.index == 219) // Safe Guard
                            {
                                attacker.isSafeGuard = 5;
                            }

                            if (move.index == 433) // Trick Room
                            {
                                if (trickRoom < 5) trickRoom = 5;
                            }

                            if (move.index == 115) // Reflect
                            {
                                attacker.Reflect = 5;
                            }

                            if (move.index == 113) // LightScreen
                            {
                                attacker.LightScreen = 5;
                            }

                            if (move.index == 432) // Remove Reflect/Light Screen/Safeguard/Mist: Defog
                            {
                                defender.Reflect = 0;
                                defender.LightScreen = 0;
                                defender.isSafeGuard = 0;
                                defender.isMist = 0;
                            }

                            if (move.index == 54) // Mist
                            {
                                attacker.isMist = 5;
                            }

                            if (defender.isMist == 0)
                            {
                                #region Stats Reduction

                                if (move.index == 45 || move.index == 321) // Decrease attack: Growl, Tickle
                                {
                                    if (defender.tatk <= defender.atk * 0.41f)
                                    {
                                        tbHistory.Text = defender.name + "'s attack can not be decreased anymore.\n" + tbHistory.Text;
                                    }
                                    else
                                    {
                                        defender.tatk *= 0.8f;
                                        tbHistory.Text = defender.name + " had his attack lowered.\n" + tbHistory.Text;
                                    }
                                }

                                if (move.index == 297 || move.index == 262 || move.index == 204) // Decrease attack x2: Feather Dance, Memento, Charm
                                {
                                    if (defender.tatk <= defender.atk * 0.41f)
                                    {
                                        tbHistory.Text = defender.name + "'s attack can not be decreased anymore.\n" + tbHistory.Text;
                                    }
                                    else
                                    {
                                        defender.tatk *= 0.64f;
                                        tbHistory.Text = defender.name + " had his attack lowered.\n" + tbHistory.Text;
                                    }
                                }

                                if ((move.index == 445 && defender.gender == 1 - attacker.gender) || move.index == 262) // Decrease special attack x2: Captivate, Memento
                                {
                                    if (defender.tspatk <= defender.spatk * 0.41f)
                                    {
                                        tbHistory.Text = defender.name + "'s special attack can not be decreased anymore.\n" + tbHistory.Text;
                                    }
                                    else
                                    {
                                        defender.tspatk *= 0.64f;
                                        tbHistory.Text = defender.name + " had his special attack lowered.\n" + tbHistory.Text;
                                    }
                                }

                                if (move.index == 39 || move.index == 43 || move.index == 321) // Decrease defense: Tail Whip, Leer, Tickle
                                {
                                    if (defender.tdef <= defender.def * 0.41f)
                                    {
                                        tbHistory.Text = defender.name + "'s defense can not be decreased anymore.\n" + tbHistory.Text;
                                    }
                                    else
                                    {
                                        defender.tdef *= 0.8f;
                                        tbHistory.Text = defender.name + " had his defense lowered.\n" + tbHistory.Text;
                                    }
                                }

                                if (move.index == 319 || move.index == 103) // Decrease defense x2: Metal Sound, Screech
                                {
                                    if (defender.tdef <= defender.def * 0.41f)
                                    {
                                        tbHistory.Text = defender.name + "'s defense can not be decreased anymore.\n" + tbHistory.Text;
                                    }
                                    else
                                    {
                                        defender.tdef *= 0.64f;
                                        tbHistory.Text = defender.name + " had his defense lowered.\n" + tbHistory.Text;
                                    }
                                }

                                if (move.index == 313) // Decrease special defense x2: Fake Tears
                                {
                                    if (defender.tspdef <= defender.spdef * 0.41f)
                                    {
                                        tbHistory.Text = defender.name + "'s special defense can not be decreased anymore.\n" + tbHistory.Text;
                                    }
                                    else
                                    {
                                        defender.tspdef *= 0.64f;
                                        tbHistory.Text = defender.name + " had his special defense lowered.\n" + tbHistory.Text;
                                    }
                                }

                                if (move.index == 108 || move.index == 28 || move.index == 134
                                    || move.index == 148) // Decrease accuracy: Smokescreen, Sand Attack, Kinesis, Flash
                                {
                                    if (defender.accuracy <= 41)
                                    {
                                        tbHistory.Text = defender.name + "'s accuracy can not be decreased anymore.\n" + tbHistory.Text;
                                    }
                                    else
                                    {
                                        defender.accuracy *= 0.8f;
                                        tbHistory.Text = defender.name + " had his accuracy lowered.\n" + tbHistory.Text;
                                    }
                                }

                                if (move.index == 81) // Decrease speed: String Shot
                                {
                                    if (defender.tspeed <= defender.speed * 0.41f)
                                    {
                                        tbHistory.Text = defender.name + "'s speed can not be decreased anymore.\n" + tbHistory.Text;
                                    }
                                    else
                                    {
                                        defender.tspeed *= 0.8f;
                                        tbHistory.Text = defender.name + " had his speed lowered.\n" + tbHistory.Text;
                                    }
                                }

                                if (move.index == 184 || move.index == 178) // Decrease speed x2: Scary Face, Cotton Spore
                                {
                                    if (defender.tspeed <= defender.speed * 0.41f)
                                    {
                                        tbHistory.Text = defender.name + "'s speed can not be decreased anymore.\n" + tbHistory.Text;
                                    }
                                    else
                                    {
                                        defender.tspeed *= 0.64f;
                                        tbHistory.Text = defender.name + " had his speed lowered.\n" + tbHistory.Text;
                                    }
                                }

                                if (move.index == 230) // Decrease evasiveness: Sweet Scent
                                {
                                    if (defender.evasiveness <= 41)
                                    {
                                        tbHistory.Text = defender.name + "'s evasiveness can not be decreased anymore.\n" + tbHistory.Text;
                                    }
                                    else
                                    {
                                        defender.evasiveness *= 0.8f;
                                        tbHistory.Text = defender.name + " had his evasiveness lowered.\n" + tbHistory.Text;
                                    }
                                }

                                #endregion
                            }

                            #region Stats Raised

                            int randomStat = 0;
                            if (move.index == 367) // Raised random stats x2: Acupressure
                            {
                                randomStat = rand.Next() % 5 + 1;
                            }

                            if (move.index == 397 || move.index == 294 || move.index == 97
                                || randomStat == 1) // Raised speed x2: Rock Polish, Tail Glow, Agility
                            {
                                if (attacker.tspeed >= 3.8f * attacker.speed)
                                {
                                    tbHistory.Text = attacker.name + "'s speed can not be increased anymore!\n" + tbHistory.Text;
                                }
                                else
                                {
                                    attacker.tspeed *= 1.96f;
                                    tbHistory.Text = attacker.name + "'s speed sharply rose!\n" + tbHistory.Text;
                                }
                            }

                            if (move.index == 187) // Raised attack x4: Belly Drum
                            {
                                if (attacker.hp > attacker.hpmax * 0.5f)
                                {
                                    attacker.hp -= attacker.hpmax * 0.5f;
                                    attacker.tatk = attacker.atk * 3.8416f;
                                    tbHistory.Text = attacker.name + "'s attack was maximized!\n" + tbHistory.Text;
                                }
                                else tbHistory.Text = attacker.name + "'s HP is too low to use Belly Drum!\n" + tbHistory.Text;
                            }

                            if (move.index == 14 || randomStat == 2) // Raised attack x2: Swords Dance
                            {
                                if (attacker.tatk >= 3.8f * attacker.atk)
                                {
                                    tbHistory.Text = attacker.name + "'s attack can not be increased anymore!\n" + tbHistory.Text;
                                }
                                else
                                {
                                    attacker.tatk *= 1.96f;
                                    tbHistory.Text = attacker.name + "'s attack sharply rose!\n" + tbHistory.Text;
                                }
                            }

                            if (move.index == 334 || move.index == 151 || move.index == 112
                                || randomStat == 3) // Raised defense x2: Iron Defense, Acid Armor, Barrier
                            {
                                if (attacker.tdef >= 3.8f * attacker.def)
                                {
                                    tbHistory.Text = attacker.name + "'s defense can not be increased anymore!\n" + tbHistory.Text;
                                }
                                else
                                {
                                    attacker.tdef *= 1.96f;
                                    tbHistory.Text = attacker.name + "'s defense sharply rose!\n" + tbHistory.Text;
                                }
                            }

                            if (move.index == 110 || move.index == 455 || move.index == 339
                                || move.index == 106 || move.index == 322 || move.index == 111
                                || move.index == 254)
                            // Raised defense: Withdraw, Defend Order, Bulk Up, Harden, Cosmic Power, Defense Curl
                            // Stockpile
                            {
                                if (attacker.tdef >= 3.8f * attacker.def)
                                {
                                    tbHistory.Text = attacker.name + "'s defense can not be increased anymore!\n" + tbHistory.Text;
                                }
                                else
                                {
                                    attacker.tdef *= 1.4f;
                                    tbHistory.Text = attacker.name + "'s defense rose!\n" + tbHistory.Text;
                                }
                            }

                            if (move.index == 349 || move.index == 339 || move.index == 96
                                || move.index == 336 || move.index == 159)
                            // Raised attack: Dragon Dance, Bulk Up, Meditate, Howl, Sharpen
                            {
                                if (attacker.tatk >= 3.8f * attacker.atk)
                                {
                                    tbHistory.Text = attacker.name + "'s attack can not be increased anymore!\n" + tbHistory.Text;
                                }
                                else
                                {
                                    attacker.tatk *= 1.4f;
                                    tbHistory.Text = attacker.name + "'s attack rose!\n" + tbHistory.Text;
                                }
                            }

                            if (move.index == 74 || move.index == 347) // Raised special attack: Growth, Calm Mind
                            {
                                if (attacker.tspatk >= 3.8f * attacker.spatk)
                                {
                                    tbHistory.Text = attacker.name + "'s special attack can not be increased anymore!\n" + tbHistory.Text;
                                }
                                else
                                {
                                    attacker.tspatk *= 1.4f;
                                    tbHistory.Text = attacker.name + "'s special attack rose!\n" + tbHistory.Text;
                                }
                            }

                            if (move.index == 417 || randomStat == 4) // Raised special attack x2: Nasty Plot
                            {
                                if (attacker.tspatk >= 3.8f * attacker.spatk)
                                {
                                    tbHistory.Text = attacker.name + "'s special attack can not be increased anymore!\n" + tbHistory.Text;
                                }
                                else
                                {
                                    attacker.tspatk *= 1.96f;
                                    tbHistory.Text = attacker.name + "'s special attack sharply rose!\n" + tbHistory.Text;
                                }
                            }

                            if (move.index == 268 || move.index == 455 || move.index == 347
                                || move.index == 322 || move.index == 254)
                            // Raised special defense: Charge, Defend Order, Calm Mind, Cosmic Power, Stockpile
                            {
                                if (attacker.tspdef >= 3.8f * attacker.spdef)
                                {
                                    tbHistory.Text = attacker.name + "'s special defense can not be increased anymore!\n" + tbHistory.Text;
                                }
                                else
                                {
                                    attacker.tspdef *= 1.4f;
                                    tbHistory.Text = attacker.name + "'s special defense rose!\n" + tbHistory.Text;
                                }
                            }

                            if (move.index == 133 || randomStat == 5) // Raised special defense x2: Amnesia
                            {
                                if (attacker.tspdef >= 3.8f * attacker.spdef)
                                {
                                    tbHistory.Text = attacker.name + "'s special defense can not be increased anymore!\n" + tbHistory.Text;
                                }
                                else
                                {
                                    attacker.tspdef *= 1.96f;
                                    tbHistory.Text = attacker.name + "'s special defense sharply rose!\n" + tbHistory.Text;
                                }
                            }

                            if (move.index == 349 || move.index == 366) // Raised speed: Dragon Dance, Tailwind
                            {
                                if (attacker.tspeed >= 3.8f * attacker.speed)
                                {
                                    tbHistory.Text = attacker.name + "'s speed can not be increased anymore!\n" + tbHistory.Text;
                                }
                                else
                                {
                                    attacker.tspeed *= 1.4f;
                                    tbHistory.Text = attacker.name + "'s speed rose!\n" + tbHistory.Text;
                                }
                            }

                            if (move.index == 107 || move.index == 104) // Raised evasiveness: Minimize, Double Team
                            {
                                if (attacker.evasiveness >= 380)
                                {
                                    tbHistory.Text = attacker.name + "'s evasiveness can not be increased anymore!\n" + tbHistory.Text;
                                }
                                else
                                {
                                    attacker.evasiveness *= 1.4f;
                                    tbHistory.Text = attacker.name + "'s evasiveness rose!\n" + tbHistory.Text;
                                }
                            }

                            if (move.index == 260) // Raised opponent's special attack x2: Flatter
                            {
                                if (defender.tspatk >= 3.8f * defender.spatk)
                                {
                                    tbHistory.Text = defender.name + "'s special attack can not be increased anymore!\n" + tbHistory.Text;
                                }
                                else
                                {
                                    defender.tspatk *= 1.96f;
                                    tbHistory.Text = defender.name + "'s special attack sharply rose!\n" + tbHistory.Text;
                                }
                            }

                            if (move.index == 207) // Raised opponent's attack x2: Swagger
                            {
                                if (defender.tatk >= 3.8f * defender.atk)
                                {
                                    tbHistory.Text = defender.name + "'s attack can not be increased anymore!\n" + tbHistory.Text;
                                }
                                else
                                {
                                    defender.tatk *= 1.96f;
                                    tbHistory.Text = defender.name + "'s attack sharply rose!\n" + tbHistory.Text;
                                }
                            }

                            #endregion

                            if (move.index == 114) // Reset all stats: Haze
                            {
                                attacker.tatk = attacker.atk;
                                attacker.tdef = attacker.def;
                                attacker.tspatk = attacker.spatk;
                                attacker.tspdef = attacker.spdef;
                                attacker.tspeed = attacker.speed;

                                defender.tatk = defender.atk;
                                defender.tdef = defender.def;
                                defender.tspatk = defender.spatk;
                                defender.tspdef = defender.spdef;
                                defender.tspeed = defender.speed;
                            }

                            if (move.index == 316 || move.index == 193) // Reset evasiveness: Odor Sleuth, Foresight
                            {
                                defender.evasiveness = 100;
                            }

                            if (move.index == 466) // Raised all stats: Ominous Wind
                            {
                                if (attacker.tatk <= 3.8f * attacker.atk) attacker.tatk *= 1.4f;
                                if (attacker.tdef <= 3.8f * attacker.atk) attacker.tdef *= 1.4f;
                                if (attacker.tspatk <= 3.8f * attacker.atk) attacker.tspatk *= 1.4f;
                                if (attacker.tspdef <= 3.8f * attacker.atk) attacker.tspdef *= 1.4f;
                                if (attacker.tspeed >= 3.8f * attacker.speed) attacker.tspeed *= 1.4f;

                                tbHistory.Text = attacker.name + " has his stats increased!\n" + tbHistory.Text;
                            }

                            if (move.index == 375) // Transfer status problems: Psycho Shift
                            {
                                if (attacker.isAsleep > 0) { defender.EraseAffections(); defender.isAsleep = attacker.isAsleep; }
                                if (attacker.isBurnt > 0) { defender.EraseAffections(); defender.isBurnt = attacker.isBurnt; }
                                if (attacker.isPoisoned > 0) { defender.EraseAffections(); defender.isPoisoned = attacker.isPoisoned; }
                                if (attacker.isConfused > 0) { defender.EraseAffections(); defender.isConfused = attacker.isConfused; }
                                if (attacker.isFrozen > 0) { defender.EraseAffections(); defender.isFrozen = attacker.isFrozen; }
                                if (attacker.isParalyzed > 0) { defender.EraseAffections(); defender.isParalyzed = attacker.isParalyzed; }
                                attacker.EraseAffections();
                            }

                            if (move.index == 244) // Copy stats change: Psych Up
                            {
                                attacker.tatk *= defender.tatk / defender.atk;
                                attacker.tdef *= defender.tdef / defender.def;
                                attacker.tspatk *= defender.tspatk / defender.spatk;
                                attacker.tspdef *= defender.tspdef / defender.spdef;
                                attacker.tspeed *= defender.tspeed / defender.speed;
                            }

                            if (move.index == 384 || move.index == 391) // Swap atk: Power Swap, Heart Swap
                            {
                                float x1 = attacker.tatk / attacker.atk;
                                float x2 = defender.tatk / defender.atk;

                                attacker.tatk = attacker.atk * x2;
                                defender.tatk = defender.atk * x1;
                            }

                            if (move.index == 384 || move.index == 391) // Swap spatk: Power Swap, Heart Swap
                            {
                                float x1 = attacker.tspatk / attacker.spatk;
                                float x2 = defender.tspatk / defender.spatk;

                                attacker.tspatk = attacker.spatk * x2;
                                defender.tspatk = defender.spatk * x1;
                            }

                            if (move.index == 385 || move.index == 391) // Swap def: Guard Swap, Heart Swap
                            {
                                float x1 = attacker.tdef / attacker.def;
                                float x2 = defender.tdef / defender.def;

                                attacker.tdef = attacker.def * x2;
                                defender.tdef = defender.def * x1;
                            }

                            if (move.index == 385 || move.index == 391) // Swap spdef: Guard Swap, Heart Swap
                            {
                                float x1 = attacker.tspdef / attacker.spdef;
                                float x2 = defender.tspdef / defender.spdef;

                                attacker.tspdef = attacker.spdef * x2;
                                defender.tspdef = defender.spdef * x1;
                            }

                            if (move.index == 391) // Swap speed: Heart Swap
                            {
                                float x1 = attacker.tspeed / attacker.speed;
                                float x2 = defender.tspeed / defender.speed;

                                attacker.tspeed = attacker.speed * x2;
                                defender.tspeed = defender.speed * x1;
                            }

                            if (move.index == 379) // Swap atk and def: Power Trick
                            {
                                float aux = attacker.tdef;
                                attacker.tdef = attacker.tatk;
                                attacker.tatk = aux;
                            }

                            if (move.index == 116) // Raised Critical Chance: Focus Energy
                            {
                                if (attacker.critical < 45) attacker.critical += 10;
                            }

                            if (move.index == 381) // Prevent Critical Hits: Lucky Chant
                            {
                                defender.isCritChanted = true;
                            }

                            if (move.index == 220) // Pain Split
                            {
                                float aux = (attacker.hp + defender.hp) * 0.5f;
                                attacker.hp = aux;
                                defender.hp = aux;

                                if (attacker.hp > attacker.hpmax)
                                    attacker.hp = attacker.hpmax;
                                if (defender.hp > defender.hpmax)
                                    defender.hp = defender.hpmax;
                            }

                            if (move.index == 456 || move.index == 355 || move.index == 208
                                || move.index == 105 || move.index == 303 || move.index == 135)
                                // Heal 1/2 HP: Heal Order, Roost, Milk Drink, Recover, Slack Off, Soft-Boiled
                                if (attacker.HealBlock == 0)
                                {
                                    attacker.hp += attacker.hpmax * 0.5f;
                                    if (attacker.hp > attacker.hpmax)
                                        attacker.hp = attacker.hpmax;
                                }

                            if (move.index == 287) // Cure poison, burn, paralysis: Refresh
                            {
                                attacker.isBurnt = 0;
                                attacker.isParalyzed = 0;
                                attacker.isPoisoned = 0;
                            }

                            if (move.index == 312 || move.index == 215) // Aromatherapy, Heal Bell
                            {
                                if (yourPokemonIsAttacking) // erase your affections
                                {
                                    for (int i = 0; i < player.pokemons.Count; ++i)
                                    {
                                        player.pokemons[i].EraseAffections();
                                    }
                                }
                                else // erase enemy's affections
                                {
                                    if (enemyTrainer == null)
                                        enemyPokemon.EraseAffections();
                                    else for (int i = 0; i < enemyTrainer.pokemons.Count; ++i)
                                            enemyTrainer.pokemons[i].EraseAffections();
                                }
                            }

                            if (move.index == 235 || move.index == 236 || move.index == 234) // Heal: Synthesis, Moonlight, Morning Sun
                                if (attacker.HealBlock == 0)
                                {
                                    if (weatherType == 2)
                                        attacker.hp += attacker.hpmax * 0.67f;
                                    else if (weatherType == 0)
                                        attacker.hp += attacker.hpmax * 0.5f;
                                    else attacker.hp += attacker.hpmax * 0.25f;

                                    if (attacker.hp > attacker.hpmax)
                                        attacker.hp = attacker.hpmax;
                                }

                            if (move.index == 156) // Heal: Rest
                                if (attacker.HealBlock == 0)
                                {
                                    attacker.hp = attacker.hpmax;
                                    attacker.EraseAffections();
                                    attacker.isAsleep = 2;
                                }

                            if (move.index == 169 || move.index == 335 || move.index == 212)
                            // Trapped forever: Spider Web, Block, Mean Look
                            {
                                defender.isTrapped = 100;
                            }

                            if (move.index == 73) // Leech Seed
                            {
                                defender.isSeeded = true;
                            }

                            if (move.index == 377) // Heal Block
                            {
                                defender.HealBlock = 5;
                            }

                            if (move.index == 213 && defender.gender == 1 - attacker.gender) // Attract
                            {
                                defender.isAttracted = true;
                            }

                            if (move.index == 254) // Stockpile
                            {
                                if (attacker.Stockpile < 3)
                                    attacker.Stockpile += 1;
                            }

                            if (move.index == 256) // Heal: Swallow
                                if (attacker.HealBlock == 0)
                                {
                                    float hpSwallowed = attacker.hpmax;

                                    if (attacker.Stockpile == 0) hpSwallowed = 0;
                                    else if (attacker.Stockpile == 1) hpSwallowed *= 0.25f;
                                    else if (attacker.Stockpile == 2) hpSwallowed *= 0.5f;

                                    attacker.hp += hpSwallowed;
                                    if (attacker.hp > attacker.hpmax)
                                        attacker.hp = attacker.hpmax;

                                    attacker.Stockpile = 0;
                                }

                            if (move.index == 268) // Charge user
                            {
                                attacker.isCharged = true;
                            }

                            if (move.index == 373 || move.index == 282) // Cannot use items: Embargo, Knock Off
                            {
                                defender.canUseItems = false;
                            }

                            if (move.index == 275) // Can not switch: Ingrain
                            {
                                attacker.canBeSwitched = false;
                            }

                            if (move.index == 392 || move.index == 275) // Regenerate life every turn: Aqua Ring, Ingrain
                            {
                                attacker.isRegeneratingLife = true;
                            }

                            if (move.index == 346) // Weaken fire moves: Water Sport
                            {
                                attacker.isWaterSported = true;
                            }

                            if (move.index == 240) // Weather - rain: Rain Dance
                            {
                                // can not change weather if it's already raining more
                                if (weatherType == 1 && weatherDuration > 5) ;
                                else
                                {
                                    weatherType = 1;
                                    weatherDuration = 5;
                                }
                            }

                            if (move.index == 241) // Weather - sunny: Sunny Day
                            {
                                if (weatherType == 2 && weatherDuration > 5) ;
                                else
                                {
                                    weatherType = 2;
                                    weatherDuration = 5;
                                }
                            }

                            if (move.index == 201) // Weather - sandstorm: Sandstorm
                            {
                                if (weatherType == 3 && weatherDuration > 5) ;
                                else
                                {
                                    weatherType = 3;
                                    weatherDuration = 5;
                                }
                            }

                            if (move.index == 258) // Weather - hail: Hail
                            {
                                if (weatherType == 4 && weatherDuration > 5) ;
                                else
                                {
                                    weatherType = 4;
                                    weatherDuration = 5;
                                }
                            }

                            if (move.index == 171) // Nightmare
                                if (defender.isAsleep > 0)
                                {
                                    defender.Nightmare = true;
                                }

                            if (move.index == 393) // Magnet Rise
                            {
                                attacker.MagnetRise = 5;
                            }

                            if (move.index == 262) // Faint: Memento
                            {
                                attacker.hp = 0;

                                if (yourPokemonIsAttacking)
                                {
                                    YourPokemonDies(attacker);
                                }
                                else
                                {
                                    EnemyPokemonDies(attacker);
                                }
                                return;
                            }

                            ChangeHealth();

                            #endregion
                        }

                        if (move.index == 18 || move.index == 46 || move.index == 100
                            || move.index == 369 || move.index == 226)
                        // Swap pokemon: Whirlwind, Roar, Teleport, U-Turn, Baton Pass
                        {
                            if (move.index == 18)
                                tbHistory.Text = defender.name + " was blown away!\n" + tbHistory.Text;
                            if (move.index == 46)
                                tbHistory.Text = defender.name + " ran away!\n" + tbHistory.Text;

                            bool tmp = yourPokemonIsAttacking;
                            if (move.index == 100 || move.index == 369 || move.index == 226) tmp = !tmp;

                            if (tmp)
                            {
                                if (enemyTrainer != null)
                                {
                                    int crtEnemyPokemon = 0;

                                    for (int i = 0; i < enemyTrainer.pokemons.Count; ++i)
                                        if (enemyTrainer.pokemons[i] == enemyPokemon)
                                        {
                                            crtEnemyPokemon = i;
                                            break;
                                        }

                                    for (int i = 0; i < enemyTrainer.pokemons.Count; ++i)
                                        if (enemyTrainer.pokemons[i].hp > 0 && i != crtEnemyPokemon)
                                        {
                                            enemyPokemon = enemyTrainer.pokemons[i];
                                            battleScreen.Invalidate();
                                            if (move.index == 100 || move.index == 369 || move.index == 226) break;
                                            else return;
                                        }
                                }
                                else
                                {
                                    if (enemyPokemon.isTrapped == 0)
                                    {
                                        waitTime = true;
                                        endBattleTimer.Start();
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                bool changed = false;

                                firstChangeCrtPokemon = true;

                                for (int i = 0; i < player.pokemons.Count; ++i)
                                    if (player.pokemons[i].hp > 0 && i != crtPokemon)
                                    {
                                        changed = true;
                                        ChangeCrtPokemon(i);
                                        break;
                                    }

                                if (!changed && enemyTrainer == null)
                                {
                                    waitTime = true;
                                    endBattleTimer.Start();
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        // attack missed
                        tbHistory.Text = attacker.name + " used " + move.name + " but it missed!\n" + tbHistory.Text;

                        attacker.FuryCutter = 0; // Fury Cutter resets

                        if (move.index == 26 || move.index == 136) // Jump Kick, Hight Jump Kick
                        {
                            tbHistory.Text = attacker.name + " hurt himself!\n" + tbHistory.Text;
                            attacker.hp -= attacker.hpmax / 8f;
                            if (attacker.hp <= 0)
                            {
                                if (yourPokemonIsAttacking)
                                {
                                    YourPokemonDies(attacker);
                                }
                                else
                                {
                                    EnemyPokemonDies(attacker);
                                }
                                return;
                            }
                        }
                    }
                }
            }

            waitTime = true;
            attackTimerCount = 0;
            attackTimer.Start();

            battleScreen.Invalidate();
        }

        bool yourPokemonIsAttacking;

        #endregion

        #region Throw Pokeball

        bool drawBall;
        Pokeball pokeball;
        PointF pokeballPoint;
        Timer pokeballTimer = new Timer();
        int pokeballTimerCount;
        bool pokemonWasCaught;

        public void ThrowBall(Pokeball ball)
        {
            if (enemyTrainer != null)
            {
                tbHistory.Text = "You can not use a pokeball in a battle with a trainer!\n" + tbHistory.Text;
                return;
            }

            pokeball = ball;

            // Master Ball was used
            if (pokeball.type == 4) 
                player.usedMasterBall = true;

            // update the numbers of pokeballs
            ball.number -= 1;
            if (ball.number == 0)
                player.pokeballs.Remove(ball);

            // catch chance formula
            float catchChance = (enemyPokemon.catchrate / 255f)
                * (enemyPokemon.hpmax / enemyPokemon.hp) * 4.5f * ball.chance;

            // pause game
            waitTime = true;

            drawBall = true;
            pokeballPoint = new PointF(430, 70);
            pokeballTimerCount = 0;
            pokeballTimer.Start();
            battleScreen.Invalidate();

            if (rand.Next() % 100 + 1 < catchChance) pokemonWasCaught = true;
            else pokemonWasCaught = false;
        }

        void pokeballTimer_Tick(object sender, EventArgs e)
        {
            battleScreen.Invalidate();

            if (++pokeballTimerCount >= 40)
            {
                if (pokeballTimerCount == 80)
                {
                    pokeballTimer.Stop();

                    if (pokemonWasCaught)
                    {
                        tbHistory.Text = enemyPokemon.name + " was caught!\n" + tbHistory.Text;
                        player.AddPokemon(enemyPokemon);

                        FormPokedex.OwnPokemon(enemyPokemon.index);

                        waitTime = true;
                        endBattleTimer.Start();
                    }
                    else
                    {
                        tbHistory.Text = enemyPokemon.name + " escaped!\n" + tbHistory.Text;
                        justChangeCrtPokemon = true;
                        AI_Turn();
                        drawBall = false;
                    }
                }
                return;
            }

            pokeballPoint.Y += 2;
        }

        #endregion

        #region Use Potion

        public void UsePotion(Potion pot)
        {
            if (player.pokemons[crtPokemon].canUseItems)
            {
                // Heal Block
                if (pot.heal > 0 && player.pokemons[crtPokemon].HealBlock > 0) return;

                pot.UsePotion(player.pokemons[crtPokemon]);

                ChangeMoves();

                battleScreen.Invalidate();

                // AI Turn
                justChangeCrtPokemon = true;
                AI_Turn();
            }
            else
            {
                tbHistory.Text = "You can not use items on " + player.pokemons[crtPokemon].name + "!\n" + tbHistory.Text;
            }
        }

        #endregion

        #region Attack Animation

        Timer attackTimer = new Timer();
        int attackTimerCount;
        bool waitTime = false;

        // attack animation
        void attackTimer_Tick(object sender, EventArgs e)
        {
            if (++attackTimerCount <= 12)
            {
                int dir = -1;
                if (attackTimerCount <= 3 || attackTimerCount >= 10) dir = 1;

                if (yourPokemonIsAttacking)
                    attackerPoint.X += 5 * dir;
                else
                    defenderPoint.X += 5 * dir;

                battleScreen.Invalidate();
            }
            else
            {
                attackTimer.Stop();
                waitTime = false;

                battleTurns += 1;

                //MessageBox.Show("am ajuns!");

                if (battleTurns >= 2 || (battleTurns == 1 && justChangeCrtPokemon))
                {
                    battleTurns = 0;
                    return;
                }

                //MessageBox.Show("am ajuns si aici!");

                if (yourPokemonIsAttacking) AI_Turn();
                else if (AIfirst != -1 && !justChangeCrtPokemon)
                {
                    yourPokemonIsAttacking = true;
                    Battle(player.pokemons[crtPokemon], enemyPokemon, player.pokemons[crtPokemon].moves[AIfirst]);
                }
            }
        }

        #endregion

        #region AI

        Timer waitAITimer = new Timer(); // wait before attacking
        int AIfirst; // keeps the index of the attack if the AI is the first attacker
        int battleTurns; // 1-2 turns

        void waitAITimer_Tick(object sender, EventArgs e)
        {
            waitAITimer.Stop();

            waitTime = false;
            AI_Attack();
        }

        void AI_Turn() // wait before attack
        {
            waitTime = true;
            waitAITimer.Start();
        }

        void AI_Attack()
        {
            yourPokemonIsAttacking = false;

            List<int> AIStatusMoves = new List<int>();

            float maxDamage = 0;
            int maxDamageIndex = 0;

            for (int i = 0; i < 4; ++i)
            {
                // check for status moves
                if (i < enemyPokemon.moves.Count)
                {
                    Attack tmp = enemyPokemon.moves[i];

                    if (tmp.PP > tmp.PPmax - 2 && tmp.category == 3)
                    {
                        AIStatusMoves.Add(i);
                    }
                }
            }

            if (AIStatusMoves.Count > 0 && rand.Next() % 100 + 1 >= 75)
            {
                maxDamageIndex = AIStatusMoves[rand.Next() % AIStatusMoves.Count];
            }
            else
            {
                for (int i = 0; i < 4; ++i)
                {
                    // calculate all the possibilities
                    if (i < enemyPokemon.moves.Count)
                    {
                        Attack tmp = enemyPokemon.moves[i];

                        if (tmp.PP > 0)
                        {
                            float damage = CalculateDamage(enemyPokemon, player.pokemons[crtPokemon], tmp);
                            if (damage > maxDamage)
                            {
                                maxDamage = damage;
                                maxDamageIndex = i;
                            }
                        }
                    }
                }
            }

            // AI attacks
            enemyPokemon.moves[maxDamageIndex].PP -= 1;
            Battle(enemyPokemon, player.pokemons[crtPokemon], enemyPokemon.moves[maxDamageIndex]);
        }

        #endregion

        #region End of Battle

        int winMoney;

        Timer endBattleTimer = new Timer();
        bool isEndBattle;
        bool outOfPokemons;

        private void EndBattle(bool winner)
        {
            if (winner)
            {
                winMoney += 50;

                tbHistory.Text = "You earned " + winMoney + "¥!\n" + tbHistory.Text;
                player.money += winMoney;
                outOfPokemons = false;

                if (rand.Next() % 100 + 1 < 15)
                {
                    tbHistory.Text = "You found food!\n" + tbHistory.Text;
                    player.food += 1;
                }
            }
            else
            {
                tbHistory.Text = "You are out of pokemons!\n" + tbHistory.Text;
                outOfPokemons = true;
            }

            endBattleTimer.Start();
        }

        void endBattleTimer_Tick(object sender, EventArgs e)
        {
            endBattleTimer.Stop();

            isEndBattle = true;

            if (outOfPokemons)
            {
                if (Map.temporaryReserve == null)
                {
                    if (!FormGame.stopMovement) // not the end of the game
                    {
                        FormGame.map = new Map(1, player.healingcenter);
                        player.HealTeam();
                    }
                    else // the end of the game
                    {
                        int pow = 122 - enemyTrainer.index;
                        int place = 1;
                        for (int i = 1; i <= pow; ++i)
                            place *= 2;

                        new FormEndGame(place).Show();
                        Map.mainForm.Close();
                    }
                }
                else
                {
                    player.pokemons = Map.temporaryReserve;
                    Map.temporaryReserve = null;
                    FormGame.map = new Map(18, 500, 180);
                }
            }
            else
            {
                if (enemyTrainer != null)
                {
                    FormGame.map.StopActionFromTrainer(enemyTrainer);
                }
                else if (enemyPokemonEvent)
                {
                    FormGame.map.StopActionFromPokemon(enemyPokemon);
                }
            }

            this.Close();
        }

        private void FormBattle_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!isEndBattle) Application.Exit();

            FormGame.gameIsPaused = false;
        }
        #endregion
    }
}
