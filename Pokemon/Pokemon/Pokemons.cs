using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokemon
{
    public class Pokemons
    {
        public int index;

        public string name;

        public int gender, // -1 none 0 female 1 male
            type1, type2;

        public int level, xp, 
            catchrate;

        public float
            atk, def,
            spatk, spdef,
            hp, hpmax,
            speed;

        public float lovebase, love,
            trust, hunger, joy;

        public float
            tatk, tdef,
            tspatk, tspdef,
            tspeed;

        public float accuracy, evasiveness;
        public int critical;

        public int xpgiven;

        public int isBurnt, isPoisoned, Flinch,
            isFrozen, isParalyzed, isAsleep, 
            isConfused;

        public int weight;

        public bool isSeeded, isCharged, isRegeneratingLife,
            isWaterSported, canNotAttack, isProtected,
            Nightmare, isCritChanted, isAttracted,
            canUseItems, canBeSwitched;

        public int isMist, isSafeGuard;
        public int Stockpile;
        public int Reflect, LightScreen;
        public int HealBlock;
        public int MagnetRise;
        public int isTrapped;
        public int FuryCutter;
        public int sameAttack;
        public Attack sameAttackDef;

        public float twoTurnAttack;

        Random rand = new Random();

        public List<Attack> moves = new List<Attack>();

        static public string GetType(int index)
        {
            switch(index)
            {
                case 1: return "Normal";
                case 2: return "Fire";
                case 3: return "Water";
                case 4: return "Electric";
                case 5: return "Grass";
                case 6: return "Ice";    
                case 7: return "Fight";
                case 8: return "Poison";
                case 9: return "Ground";
                case 10: return "Flying";
                case 11: return "Psychic"; 
                case 12: return "Bug";
                case 13: return "Rock";
                case 14: return "Ghost";
                case 15: return "Dragon";
                case 16: return "Dark";
                case 17: return "Steel";
                default: return "";
            }
        }

        public Pokemons(int _index, int _level)
        {
            Initialize(_index, _level);
        }

        public int X, Y;
        public Pokemons(int _index, int _level, int x, int y)
        {
            X = x;
            Y = y;
            Initialize(_index, _level);
        }

        private void Initialize(int _index, int _level)
        {
            index = _index;
            level = _level;
            xp = 0;

            List<string> ret = DataBase.GetPokemon(index);

            name = ret[0];

            int genderChance = Convert.ToInt32(ret[1]);

            if (genderChance == -1)
            {
                gender = -1;
            }
            else
            {
                if (rand.Next() % 100 + 1 <= genderChance) gender = 1;
                else gender = 0;
            }

            hpmax = Convert.ToInt32(ret[2]) + 1.5f * (level - 1);
            hp = hpmax;

            atk = Convert.ToInt32(ret[3]) + 0.5f * (level - 1);
            def = Convert.ToInt32(ret[4]) + 0.5f * (level - 1);
            spatk = Convert.ToInt32(ret[5]) + 0.5f * (level - 1);
            spdef = Convert.ToInt32(ret[6]) + 0.5f * (level - 1);
            speed = Convert.ToInt32(ret[7]) + 0.5f * (level - 1);

            lovebase = rand.Next() % 51;
            love = lovebase;
            trust = 0;
            joy = 0;
            hunger = 0;

            type1 = Convert.ToInt32(ret[8]);
            type2 = Convert.ToInt32(ret[9]);

            catchrate = Convert.ToInt32(ret[10]);

            xpgiven = Convert.ToInt32(ret[11]);

            weight = Convert.ToInt32(ret[12]);

            GetStartingMoves();
        }

        private void GetStartingMoves()
        {
            List<List<string>> ret = DataBase.GetAttack(index, level, true);

            if(ret.Count <= 4)
            {
                for (int i = 0; i < ret.Count; ++i)
                {
                    int moveID = Convert.ToInt32(ret[i][0]);
                    LearnMove(new Attack(moveID));
                }
            }
            else
            {
                List<int> att = new List<int>();

                while (att.Count < 4)
                {
                    int x = rand.Next() % ret.Count;
                    if (!att.Contains(x)) att.Add(x);
                }

                att.Sort();

                for (int i = 0; i < 4; ++i)
                {
                    int moveID = Convert.ToInt32(ret[att[i]][0]);
                    LearnMove(new Attack(moveID));
                }
            }
        }

        public void LearnMove(Attack move)
        {
            if (moves.Count < 4)
            {
                moves.Add(move);
            }
            else
            {
                new FormMoveDelete(this, move).ShowDialog();
            }
        }

        public void Evolve(int evolutionIndex)
        {
            FormPokedex.SeePokemon(evolutionIndex);
            FormPokedex.OwnPokemon(evolutionIndex);

            List<string> ret = DataBase.GetPokemon(index);

            string originalName = ret[0];
            hpmax -= Convert.ToInt32(ret[2]);
            hp -= Convert.ToInt32(ret[2]);
            atk -= Convert.ToInt32(ret[3]);
            def -= Convert.ToInt32(ret[4]);
            spatk -= Convert.ToInt32(ret[5]);
            spdef -= Convert.ToInt32(ret[6]);
            speed -= Convert.ToInt32(ret[7]);

            index = evolutionIndex;

            ret = DataBase.GetPokemon(index);

            if(name == originalName) name = ret[0];

            hpmax += Convert.ToInt32(ret[2]);
            hp += Convert.ToInt32(ret[2]);
            atk += Convert.ToInt32(ret[3]);
            def += Convert.ToInt32(ret[4]);
            spatk += Convert.ToInt32(ret[5]);
            spdef += Convert.ToInt32(ret[6]);
            speed += Convert.ToInt32(ret[7]);

            type1 = Convert.ToInt32(ret[8]);
            type2 = Convert.ToInt32(ret[9]);

            catchrate = Convert.ToInt32(ret[10]);

            xpgiven = Convert.ToInt32(ret[11]);
        }

        public void SetTemporaryStats()
        {
            isAttracted = false;
            isProtected = false;
            isSeeded = false;
            isCharged = false;
            isRegeneratingLife = false;
            isWaterSported = false;
            isCritChanted = false;
            canNotAttack = false;
            canUseItems = true;
            Nightmare = false;
            canBeSwitched = true;
            isTrapped = 0;
            isMist = 0;
            isSafeGuard = 0;
            Reflect = 0;
            LightScreen = 0;
            HealBlock = 0;
            FuryCutter = 0;
            MagnetRise = 0;
            Stockpile = 0;
            sameAttack = -1;
            twoTurnAttack = -1;

            tatk = atk;
            tdef = def;
            tspatk = spatk;
            tspdef = spdef;
            tspeed = speed;
            accuracy = 100;
            evasiveness = 100;
            critical = 5;
        }

        public void EraseAffections()
        {
            isConfused = 0;
            isFrozen = 0;
            isBurnt = 0;
            isParalyzed = 0;
            isPoisoned = 0;
            isAsleep = 0;
            Flinch = 0;
        }

        public void LevelUp()
        {
            xp -= Resources.xpneeded[level];
            level += 1;

            hpmax += 1.5f;
            hp += 1.5f;
            atk += 0.5f;
            def += 0.5f;
            spatk += 0.5f;
            spdef += 0.5f;
            speed += 0.5f;

            float bonus = 0;

            if (love > 100) bonus = ((love - 100) / 100f);

            float hpbonus = (bonus + rand.Next(0, 15) / 100f) * 1.5f;
            hpmax += hpbonus;
            hp += hpbonus;
            atk += (bonus + rand.Next(0, 15) / 100f) * 1.5f;
            def += (bonus + rand.Next(0, 15) / 100f) * 1.5f;
            spatk += (bonus + rand.Next(0, 15) / 100f) * 1.5f;
            spdef += (bonus + rand.Next(0, 15) / 100f) * 1.5f;
            speed += (bonus + rand.Next(0, 15) / 100f) * 1f;

            // check for evolution
            if (Resources.evolution[index].level > 0 && level >= Resources.evolution[index].level)
            {
                int evolutionIndex = Resources.evolution[index].evolution;

                if(evolutionIndex == -2)
                {
                    if(index == 412) // Burmy
                    {
                        if (gender == 0) evolutionIndex = 413;
                        else evolutionIndex = 414;
                    }
                    
                    if(index == 415) // Combee
                    {
                        if (gender == 0) evolutionIndex = 416;
                        else evolutionIndex = 0;
                    }
                }

                if(evolutionIndex == -1)
                {
                    if (index == 236) // Tyrogue
                    {
                        if ((int)atk > (int)def) evolutionIndex = 106;
                        else if ((int)atk < (int)def) evolutionIndex = 107;
                        else evolutionIndex = 237;
                    }

                    if(index == 265) // Wurmple
                    {
                        if (rand.Next() % 2 == 0) evolutionIndex = 266;
                        else evolutionIndex = 268;
                    }

                    if(index == 290) // Nincada
                    {
                        evolutionIndex = 291;
                        if (FormGame.player.pokemons.Count < 6)
                        {
                            FormGame.player.AddPokemon(new Pokemons(292, 20));
                        }
                    }
                }

                if (evolutionIndex != 0)
                {
                    new FormEvolution(index, evolutionIndex).ShowDialog();
                    Evolve(evolutionIndex);
                }
            }

            // check for new attacks
            List<List<string>> ret = DataBase.GetAttack(index, level);
            for (int i = 0; i < ret.Count; ++i)
            {
                int moveID = Convert.ToInt32(ret[i][0]);
                LearnMove(new Attack(moveID));
            }
        }
        
        public void EarnLove(float t, float h, float j)
        {
            trust += t;
            if (trust < 0) trust = 0;
            if (trust > 100) trust = 100;

            hunger += h;
            if (hunger < 0) hunger = 0;
            if (hunger > 20) hunger = 20;

            joy += j;
            if (joy < 0) joy = 0;
            if (joy > 30) joy = 30;

            if (trust >= 99f && hunger >= 19f && joy >= 29f) // level up with love
            {
                if (Resources.evolution[index].level == -3)
                {
                    if (index == 458) // Mantyke
                    {
                        for (int i = 0; i < FormGame.player.pokemons.Count; ++i)
                        {
                            if (FormGame.player.pokemons[i].index == 223)
                            {
                                int indexCopy = index;
                                Evolve(Resources.evolution[index].evolution);
                                new FormEvolution(indexCopy, index).ShowDialog();
                                if (FormPokemons.lastForm != null) new FormPokemons().ShowDialog();
                                break;
                            }
                        }
                    }
                }

                if (Resources.evolution[index].level == -2)
                {
                    int indexCopy = index;
                    Evolve(Resources.evolution[index].evolution);
                    new FormEvolution(indexCopy, index).ShowDialog();
                    if (FormPokemons.lastForm != null) new FormPokemons().ShowDialog();
                }

                if (index == 133) // Eevee
                {
                    int indexCopy = index;
                    Evolve(196 + rand.Next() % 2);
                    new FormEvolution(indexCopy, index).ShowDialog();
                    if (FormPokemons.lastForm != null) new FormPokemons().ShowDialog();
                }
            }

            love = lovebase + trust + hunger + joy;
        }
    }
}
