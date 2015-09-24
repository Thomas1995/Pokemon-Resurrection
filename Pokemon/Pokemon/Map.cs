using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Pokemon
{
    public class Map
    {
        #region Initialization
        Character player = FormGame.player;
        PictureBox pbCanvas;

        static public FormGame mainForm;

        List<Trees> trees = new List<Trees>();
        List<WildGrass> wildgrass = new List<WildGrass>();
        List<Flowers> flowers = new List<Flowers>();
        List<Hills> hills = new List<Hills>();
        List<Buildings> buildings = new List<Buildings>();
        List<ExitPoint> exit = new List<ExitPoint>();
        List<BlockedZone> blockedzone = new List<BlockedZone>();
        List<ActionPoint> actionpoint = new List<ActionPoint>();
        List<Paths> paths = new List<Paths>();
        List<Character> characters = new List<Character>();
        List<Structures> structures = new List<Structures>();
        List<Pokemons> wildpokemons = new List<Pokemons>();

        struct ints
        {
            public int index, chance;
            public int lmin, lmax;

            public ints(int _index, int _lmin, int _lmax, int _chance)
            {
                index = _index;
                chance = _chance;
                lmin = _lmin;
                lmax = _lmax;
            }
        };

        List<ints> wildPokemons = new List<ints>();

        public int mapNumber;

        static public bool isInBuilding = true;

        public Map(int mapNr, int X, int Y)
        {
            Initialization();

            mapNumber = mapNr;
            isInBuilding = false;

            FormGame.OX = X + 7;
            FormGame.OY = Y + 6;

            switch (mapNr)
            {
                case 1: LoadMap1(); break;
                case 2: LoadMap2(); break;
                case 3: LoadMap3(); break;
                case 4: LoadMap4(); break;
                case 5: LoadMap5(); break;
                case 6: LoadMap6(); break;
                case 7: LoadMap7(); break;
                case 8: LoadMap8(); break;
                case 9: LoadMap9(); break;
                case 10: LoadMap10(); break;
                case 11: LoadMap11(); break;
                case 12: LoadMap12(); break;
                case 13: LoadMap13(); break;
                case 14: LoadMap14(); break;
                case 15: LoadMap15(); break;
                case 16: LoadMap16(); break;
                case 17: LoadMap17(); break;
                case 18: LoadMap18(); break;
                case 19: LoadMap19(); break;
                case 20: LoadMap20(); break;
                case 21: LoadMap21(); break;
                case 22: LoadMap22(); break;
                case 23: LoadMap23(); break;
                case 24: LoadMap24(); break;
                case 25: LoadMap25(); break;
                case 26: LoadMap26(); break;
                case 27: LoadMap27(); break;
                case 28: LoadMap28(); break;
                case 29: LoadMap29(); break;
                case 30: LoadMap30(); break;
                case 31: LoadMap31(); break;
                case 32: LoadMap32(); break;
                case 33: LoadMap33(); break;
                case 34: LoadMap34(); break;
                case 35: LoadMap35(); break;
                case 36: LoadMap36(); break;
                case 37: LoadMap37(); break;
                case 38: LoadMap38(); break;
                case 39: LoadMap39(); break;
                case 40: LoadMap40(); break;
                case 41: LoadMap41(); break;
                case 42: LoadMap42(); break;
                case 43: LoadMap43(); break;
                case 44: LoadMap44(); break;
                case 45: LoadMap45(); break;
                case 46: LoadMap46(); break;
                case 47: LoadMap47(); break;
                case 48: LoadMap48(); break;
                case 49: LoadMap49(); break;
                case 50: LoadMap50(); break;
            }
        }

        public Map(int buildingNr, int mapNr)
        {
            Initialization();
            isInBuilding = true;

            if (buildingNr == -1) LoadCave(mapNr);
            if (buildingNr == 0) LoadGym(mapNr);
            if (buildingNr == 1) LoadHealingCenter(mapNr);
            if (buildingNr == 2) LoadShop(mapNr);
            if (buildingNr == 4 && mapNr == 37) LoadTrickHouse(mapNr);
            if (buildingNr == 5 && mapNr == 10) LoadHauntedHouse(mapNr);
            if (buildingNr == 6) LoadArena(mapNr);
            if (buildingNr == 7) LoadDinoHouse(mapNr);
            if (buildingNr == 8) LoadSafari(mapNr);
            if (buildingNr == 9) LoadTournament(1);
        }

        private void Initialization()
        {
            pbCanvas = mainForm.pbCanvas;

            // timer for Battle Event
            timerFormBattle = new Timer();
            timerFormBattle.Interval = 100;
            timerFormBattle.Tick += timerFormBattle_Tick;
        }

        #endregion

        #region Check Movement

        public bool CheckMovement(int X, int Y)
        {
            // out of screen
            if (X < -20 || Y < -20) return false;
            if (X > 770 || Y > 570) return false;

            bool bringCharacterToBack = false;

            // characters
            for (int i = 0; i < characters.Count; ++i)
            {
                if (X + 20 >= characters[i].X && X <= characters[i].X + 30 &&
                    Y + 38 >= characters[i].Y && Y <= characters[i].Y + 40) return false;
            }

            // pokemons
            for (int i = 0; i < wildpokemons.Count; ++i)
            {
                if (X + 20 >= wildpokemons[i].X && X <= wildpokemons[i].X + 50 &&
                    Y + 18 >= wildpokemons[i].Y && Y <= wildpokemons[i].Y + 40) return false;
            }

            // blocked zones
            for (int i = 0; i < blockedzone.Count; ++i)
            {
                if (X + 37 >= blockedzone[i].Xstart && X <= blockedzone[i].Xfin &&
                    Y + 48 >= blockedzone[i].Ystart && Y <= blockedzone[i].Yfin) return false;
            }

            // trees
            for (int i = 0; i < trees.Count; ++i)
            {
                int isAllowed = 0;
                if (X + 37 <= trees[i].X + 10 || X >= trees[i].X + trees[i].sizeX - 20) ++isAllowed;
                if (Y + 48 <= trees[i].Y + 30 || Y >= trees[i].Y + trees[i].sizeY - 40) ++isAllowed;

                if (Y + 48 >= trees[i].Y + 10 && Y + 48 <= trees[i].Y + 40 &&
                    X + 20 < trees[i].X + trees[i].sizeX && X + 37 > trees[i].X + 20)
                    bringCharacterToBack = true;

                if (isAllowed == 0) return false;
            }

            // hills
            for (int i = 0; i < hills.Count; ++i)
            {
                if (X >= hills[i].X && X <= hills[i].X + hills[i].sizeX - 30
                    && Y >= hills[i].Y - 35 && Y <= hills[i].Y - 15)
                {
                    if (Keyboard.GetDir() != 2) return false;
                    else
                    {
                        FormGame.OY += 30;
                    }
                }
            }

            // buildings
            for (int i = 0; i < buildings.Count; ++i)
            {
                if (X + 27 >= buildings[i].X + 60 && X <= buildings[i].X + buildings[i].sizeX - 110
                    && Y >= buildings[i].Y + buildings[i].sizeY - 60 && Y <= buildings[i].Y + buildings[i].sizeY - 35
                    && Keyboard.GetDir() == 1) // enter in the building
                {
                    bool canGoInside = true;

                    if (buildings[i].type == 0)
                    {
                        if (FormGame.gameTime - lastTimeGym < 1000)
                        {
                            mainForm.ShowMessage("The gym leader is taking a break. Try again a little later.");
                            canGoInside = false;
                        }
                        else
                        {
                            List<int> typesAllowed = new List<int>();

                            if (mapNumber == 5) // first gym
                            {
                                typesAllowed.Add(3); // water
                                typesAllowed.Add(10); // flying
                                typesAllowed.Add(12); // bug

                                if (!trainerDefeated[4]) // you have to beat John the Lumberjack in the woods first
                                {
                                    mainForm.ShowMessage("The gym leader is not here yet... Try again later.");
                                    canGoInside = false;
                                }
                            }

                            if (mapNumber == 8) // second gym
                            {
                                typesAllowed.Add(1); // normal
                                typesAllowed.Add(5); // grass
                                typesAllowed.Add(8); // poison
                            }

                            if (mapNumber == 14) // third gym
                            {
                                typesAllowed.Add(4); // electricity
                                typesAllowed.Add(13); // rock
                                typesAllowed.Add(14); // ghost
                            }

                            if (mapNumber == 20) // fourth gym
                            {
                                typesAllowed.Add(3); // water
                                typesAllowed.Add(10); // flying
                                typesAllowed.Add(11); // psychic

                                if (!trainerDefeated[38] || !trainerDefeated[44]) // you have to win in the Arena and DinoHouse first
                                {
                                    mainForm.ShowMessage("The gym leader is not here yet... Try again later.");
                                    canGoInside = false;
                                }
                            }

                            if (mapNumber == 24) // fifth gym
                            {
                                typesAllowed.Add(2); // fire
                                typesAllowed.Add(9); // ground
                                typesAllowed.Add(13); // rock
                            }

                            if (mapNumber == 29) // sixth gym
                            {
                                typesAllowed.Add(5); // grass
                                typesAllowed.Add(6); // ice
                                typesAllowed.Add(16); // dark
                            }

                            bool teamIsValid = true;

                            for (int j = 0; j < player.pokemons.Count; ++j)
                            {
                                bool pokemonIsValid = false;

                                for (int q = 0; q < typesAllowed.Count; ++q)
                                    if (player.pokemons[j].type1 == typesAllowed[q] ||
                                        player.pokemons[j].type2 == typesAllowed[q])
                                        pokemonIsValid = true;

                                if (!pokemonIsValid) teamIsValid = false;
                            }

                            if (mapNumber == 36) // seventh gym
                            {
                                teamIsValid = true;
                            }

                            if (mapNumber == 48) // eighth gym
                            {
                                if(trainerDefeated[99]) teamIsValid = true; // beat seventh gym first
                                else
                                {
                                    mainForm.ShowMessage("The gym leader is not here yet... Try again later.");
                                    canGoInside = false;
                                }
                            }

                            if(mapNumber == 40) // fake gym
                            {
                                teamIsValid = true;
                            }

                            if (!teamIsValid)
                            {
                                canGoInside = false;

                                if (mapNumber == 5) mainForm.ShowMessage("You can only use Water, Flying and Bug-type pokemons in this gym.");
                                if (mapNumber == 8) mainForm.ShowMessage("You can only use Grass, Poison and Normal-type pokemons in this gym.");
                                if (mapNumber == 14) mainForm.ShowMessage("You can only use Electric, Rock and Ghost-type pokemons in this gym.");
                                if (mapNumber == 20) mainForm.ShowMessage("You can only use Water, Flying and Psychic-type pokemons in this gym.");
                                if (mapNumber == 24) mainForm.ShowMessage("You can only use Fire, Ground and Rock-type pokemons in this gym.");
                                if (mapNumber == 29) mainForm.ShowMessage("You can only use Grass, Ice and Dark-type pokemons in this gym.");
                            }
                        }
                    }

                    if (buildings[i].type == 6)
                    {
                        if (FormGame.gameTime - lastTimeGym < 1000)
                        {
                            canGoInside = false;
                            mainForm.ShowMessage("Lucas the Fighter is taking a break. Try again a little later.");
                        }

                        if (player.pokemons.Count > 1 || (player.pokemons[0].type1 != 7 && player.pokemons[0].type2 != 7))
                        {
                            canGoInside = false;
                            mainForm.ShowMessage("You can only use a single Fight-type pokemon in the arena.");
                        }
                    }

                    if (buildings[i].type == 7)
                    {
                        if (FormGame.gameTime - lastTimeGym < 1000)
                        {
                            canGoInside = false;
                            mainForm.ShowMessage("Dino Senior is taking a break. Try again a little later.");
                        }
                    }

                    if (buildings[i].type == 8)
                    {
                        if (player.money > 1000) player.money -= 1000;
                        else canGoInside = false;
                    }

                    if (buildings[i].hasInside && canGoInside) // go inside
                        FormGame.map = new Map(buildings[i].type, mapNumber);

                    if (mapNumber == 3) // fight with John the Lumberjack
                    {
                        if (characters.Count == 0 && !trainerDefeated[4])
                        {
                            int x = buildings[i].X + 50;
                            int y = buildings[i].Y + buildings[i].sizeY - 30;

                            characters.Add(new Character("John the Lumberjack", Resources.character_fighter, x, y));
                            characters[0].index = 4;
                            actionpoint.Add(new ActionPoint(x, y + 40, x + 50, y + 55, 0));

                            FormGame.OY += 60;

                            mainForm.ShowMessage("John the Lumberjack: Prepare to fight!");
                            actionAfterDialog = 1;
                        }
                    }
                }

                if (X + 27 >= buildings[i].X && X <= buildings[i].X + buildings[i].sizeX - 10
                    && Y >= buildings[i].Y && Y <= buildings[i].Y + buildings[i].sizeY - 35)
                    return false;

                if (X + 27 >= buildings[i].X && X <= buildings[i].X + buildings[i].sizeX
                    && Y >= buildings[i].Y - 35 && Y <= buildings[i].Y)
                    bringCharacterToBack = true;
            }

            // structures
            for (int i = 0; i < structures.Count; ++i)
            {
                if (X + 27 >= structures[i].X && X + 27 <= structures[i].X + structures[i].sizeX &&
                    Y + 38 >= structures[i].Y && Y + 38 <= structures[i].Y + structures[i].sizeY)
                    return false;
            }

            // exits
            for (int i = 0; i < exit.Count; ++i)
            {
                if (X + 37 >= exit[i].Xstart && X + 37 <= exit[i].Xfin
                    && Y + 48 >= exit[i].Ystart && Y + 48 <= exit[i].Yfin)
                {
                    bool canLeaveMap = true;

                    if (mapNumber == 8 && exit[i].mapNr == 10) // can not leave town until second gym and Haunter are defeated
                    {
                        if (!pokemonDefeated[93] || !trainerDefeated[19])
                        {
                            canLeaveMap = false;
                            mainForm.ShowMessage("An evil presence is wandering through the air and the fear won't let you move forward.");
                        }
                    }

                    if (mapNumber == 19 && exit[i].mapNr == 20) // can not leave town until team rocket is defeated
                    {
                        if (!trainerDefeated[54] || !trainerDefeated[55] || !trainerDefeated[56] || !trainerDefeated[57]
                             || !trainerDefeated[58] || !trainerDefeated[59] || !trainerDefeated[60] || !trainerDefeated[61]
                             || !trainerDefeated[62])
                        {
                            canLeaveMap = false;
                            mainForm.ShowMessage("Team Rocket is robbing the town! How can you leave in a moment like this?");
                        }
                    }

                    if (mapNumber == 24 && exit[i].mapNr == 25) // can not leave town until you have goggles
                    {
                        if (!trainerDefeated[79])
                        {
                            canLeaveMap = false;
                            mainForm.ShowMessage("The sandstorm is raging in the desert! You must have a pair of goggles to go further.");
                        }
                    }

                    if (mapNumber == 33) // start battle with Aerodactyl
                    {
                        new FormBattle(new Pokemons(142, 30), true).ShowDialog();
                    }

                    if (mapNumber == 46) // start battle with Regirock/Regice/Registeel/Regigigas
                    {
                        int x = rand.Next() % 100;
                        int id = 486;
                        if (x < 90) id = 377 + x / 30;

                        new FormBattle(new Pokemons(id, 40), true).ShowDialog();
                    }

                    if (mapNumber == 48) // you must beat all the gym leaders
                    {
                        if (player.badges < 8)
                        {
                            canLeaveMap = false;
                            mainForm.ShowMessage("Acess denied. You need 8 badges to step further.");
                        }
                        else
                        {
                            // you need your team to be of 12 different types
                            bool[] types = new bool[18];
                            for (int j = 1; j <= 17; ++j) types[j] = false;
                            for (int j = 0; j < player.pokemons.Count; ++j)
                            {
                                types[player.pokemons[j].type1] = true;
                                types[player.pokemons[j].type2] = true;
                            }

                            int typecount = 0;
                            for (int j = 1; j <= 17; ++j) if (types[j]) typecount += 1;

                            // you need a Master Ball
                            bool playerHasMasterBall = false;
                            for (int j = 0; j < player.pokeballs.Count; ++j)
                                if (player.pokeballs[j].type == 4)
                                    playerHasMasterBall = true;

                            if (typecount == 12 && playerHasMasterBall)
                            {
                                player.healingcenter = 49;
                                FormGame.map = new Map(50, 380, 270);
                                canLeaveMap = false;
                            }
                        }
                    }

                    if (temporaryReserve != null)
                    {
                        player.pokemons = temporaryReserve;
                        temporaryReserve = null;
                    }

                    if(timerTrickHouse != null)
                    {
                        Keyboard.ClearStack();
                        timerTrickHouse.Stop();
                        timerTrickHouse = null;
                    }

                    if (canLeaveMap) FormGame.map = new Map(exit[i].mapNr, exit[i].Xmap, exit[i].Ymap);
                }
            }

            if (bringCharacterToBack) player.isInFront = false;
            else player.isInFront = true;

            return true;
        }
        #endregion

        #region Action

        public void DoAction()
        {
            for (int i = 0; i < actionpoint.Count; ++i)
            {
                int X = FormGame.OX;
                int Y = FormGame.OY;

                if (X + 37 >= actionpoint[i].Xstart && X <= actionpoint[i].Xfin &&
                    Y + 48 >= actionpoint[i].Ystart && Y <= actionpoint[i].Yfin)
                {
                    Keyboard.ClearStack();

                    if (actionpoint[i].action == -1) // heal team in healing center
                    {
                        player.HealTeam();
                        mainForm.ShowMessage("Nurse Joy: Your team is fully healed!");
                    }

                    if (actionpoint[i].action == -2) // open shop
                    {
                        new FormShop().ShowDialog();
                    }

                    if (actionpoint[i].action == -3 && !trainerDefeated[9]) // first gym
                    {
                        mainForm.ShowMessage("John the Lumberjack: You caught me on the wrong foot back in the woods. I was training new pokemons for my gym. But now I am more than ready!");
                        actionAfterDialog = 2;
                    }

                    if (actionpoint[i].action == -4 && !trainerDefeated[19]) // second gym
                    {
                        mainForm.ShowMessage("Desmond: You dare to challenge me, kid?");
                        actionAfterDialog = 2;
                    }

                    if (actionpoint[i].action == -5 && !trainerDefeated[25]) // third gym
                    {
                        mainForm.ShowMessage("Magic Stan: Let the games begin!");
                        actionAfterDialog = 2;
                    }

                    if (actionpoint[i].action == -6 && !trainerDefeated[64]) // fourth gym
                    {
                        mainForm.ShowMessage("Aviana: I just beat over 50 Team Rocket members by myself!");
                        actionAfterDialog = 2;
                    }

                    if (actionpoint[i].action == -7 && !trainerDefeated[79]) // fifth gym
                    {
                        mainForm.ShowMessage("Farmer: Yeah, I am the gym leader, too...");
                        actionAfterDialog = 2;
                    }

                    if (actionpoint[i].action == -8 && !trainerDefeated[93]) // sixth gym
                    {
                        mainForm.ShowMessage("Isolde: Oh, it's not the cold you fear? You'll fear me then!");
                        actionAfterDialog = 2;
                    }

                    if (actionpoint[i].action == -9 && !trainerDefeated[99]) // seventh gym
                    {
                        mainForm.ShowMessage("Charles&Clotilde Dubois: Aha, it is a bataille that you want? Très bien!");
                        actionAfterDialog = 2;
                    }

                    if (actionpoint[i].action == -10 && !trainerDefeated[116]) // eighth gym
                    {
                        mainForm.ShowMessage("Edmund the Wise: Behold the unsurpassed power of dragons!");
                        actionAfterDialog = 2;
                    }

                    if (actionpoint[i].action == -11) // access computer
                    {
                        new FormComputer().ShowDialog();
                    }

                    if (actionpoint[i].action == -12) // Old man warns you about the Haunted House
                    {
                        mainForm.ShowMessage("Old man: Don't go there, kiddy! Don't go in that house! My wife... my wife died there. I've spent years researching Ghost-type pokemons, but they are too deceitful...");
                    }

                    if (actionpoint[i].action == -13) // Fighter invites you in the arena
                    {
                        mainForm.ShowMessage("Fighter: Are you brave enough to enter the arena? There is an incredible prize! I tried it myself, but failed miserably...");
                    }

                    if (actionpoint[i].action == -14) // Dinoman invites you in the Dino House
                    {
                        mainForm.ShowMessage("Dinoman Jr.: Rawrrrr. Compete with a random team and win great prizes!");
                    }

                    if (actionpoint[i].action == -15) // Fish Vendor tricks you
                    {
                        if (player.money > 500)
                        {
                            mainForm.ShowMessage("Fish Vendor: Let me tell you a secret. I have the greatest pokemons in the world. Here, take a look. I give you this one and you give me 500 ¥.");
                            player.money -= 500;
                            player.AddPokemon(new Pokemons(129, 1));
                        }
                        else mainForm.ShowMessage("Fish Vendor: Let me tell you a secret. I have the greatest pokemons in the world. But you will need more money...");
                    }

                    if (actionpoint[i].action == -16) // Cameraman asks you a question
                    {
                        if (!trainerDefeated[50])
                        {
                            mainForm.ShowMessage("Cameraman: Hello, " + player.name + ", you are at Pokemon TV. Would you mind answering a question? If you answer corectly you will recieve a prize!");
                            trainerDefeated[50] = true;
                            actionAfterDialog = 5;
                        }
                        else
                        {
                            mainForm.ShowMessage("Cameraman: Smile for the camera!");
                        }
                    }

                    if (actionpoint[i].action == -17) // Steve gives you Grass Knot
                    {
                        if (!trainerDefeated[51])
                        {
                            mainForm.ShowMessage("Steve: My child had a lot of fun with this move: Grass Knot. He laughs everytime when heavy pokemons trip. Take this TM!");
                            player.TMs.Add(new Attack(447)); // give Grass Knot
                            trainerDefeated[51] = true;
                        }
                        else
                        {
                            mainForm.ShowMessage("Steve: Are you having fun?");
                        }
                    }

                    if (actionpoint[i].action == -18) // Painter gives you a Smeargle
                    {
                        if (!trainerDefeated[52])
                        {
                            mainForm.ShowMessage("Painter: Have you ever heard about Smeargle? It is a pokemon that can copy any move. You should have this one.");
                            player.AddPokemon(new Pokemons(235, 15));
                            FormPokedex.SeePokemon(235);
                            FormPokedex.OwnPokemon(235);
                            trainerDefeated[52] = true;
                        }
                        else
                        {
                            mainForm.ShowMessage("Painter: So how is your Smeargle?");
                        }
                    }

                    if (actionpoint[i].action == -19) // Policeman asks for help
                    {
                        mainForm.ShowMessage("Policeman: Please help! We are outnumbered!");
                    }

                    if (actionpoint[i].action == -20) // Cameraman records the robbery
                    {
                        mainForm.ShowMessage("Cameraman: That will be a hell of a story!");
                    }

                    if (actionpoint[i].action == -21) // Policeman thanks you
                    {
                        mainForm.ShowMessage("Policeman: You are a hero!");
                    }

                    if (actionpoint[i].action == -22) // Farmer
                    {
                        mainForm.ShowMessage("Farmer: There are not many thing here... we had that shop, but no one came and they closed it!");
                    }

                    if (actionpoint[i].action == -23) // Dinoman invites you in the Dino House
                    {
                        mainForm.ShowMessage("Dinoman Jr.: Rawrrrr. Don't you want to catch brand new pokemons in the Safari Zone? You pay a fee of 1000¥ and then have fun for a while.");
                    }

                    if (actionpoint[i].action == -24) // Explorer tells you about Master Ball
                    {
                        mainForm.ShowMessage("Explorer: Hello, traveller. Are you interested in this house? A legendary pokemon is said to be there. Strange things happen when you are too close to legendary pokemons. And even if you try to catch one, he will break free. It is also a very rare event to even see one. But there is one way to catch them. The legend says that you need a Master Ball. But the only one I know about is in the possession of Team Rocket...");
                    }

                    if (actionpoint[i].action == -25) // Old man warns you about the island
                    {
                        mainForm.ShowMessage("Old man: Do not...go... We are so afraid. A dark presence comes every night. Our minds are ruined by terrifying nightmares. And the moon... the moon's color is different.");
                    }

                    if (actionpoint[i].action == -26 && !trainerDefeated[103]) // fake gym
                    {
                        mainForm.ShowMessage(player.name + ": Something strange is happening here.");
                        actionAfterDialog = 2;
                    }

                    if (actionpoint[i].action == -27) // Official tells you about the tournament
                    {
                        mainForm.ShowMessage("Official: Congratulations! You are finally here! When you are ready go in the building on the right. Once you go there, the tournament begins and you can not go back.");
                    }

                    if (actionpoint[i].action == -28) // Official tells you about training
                    {
                        mainForm.ShowMessage("Official: The gym leaders are here and they want to help you with the training. You can battle with them how many times you want. Good luck!");
                    }

                    if (actionpoint[i].action == -29) // Official evolves your pokemons
                    {
                        mainForm.ShowMessage("Official: There are few pokemons that can evolve only in special conditions. Fortunatelly, we have the technology. Bring here your Magneton, Nosepass or Eevee and watch the magic!");

                        for(int j=0;j<player.pokemons.Count;++j)
                        {
                            Pokemons poke = player.pokemons[j];

                            if (poke.index == 82) poke.Evolve(462);
                            if (poke.index == 299) poke.Evolve(476);
                            if (poke.index == 133) poke.Evolve(470 + rand.Next() % 2);
                        }
                    }

                    if (actionpoint[i].action >= 0 && actionpoint[i].action < 100) // start a battle with trainer
                    {
                        int x = actionpoint[i].action;
                        characters[x].HealTeam();

                        if (!trainerDefeated[characters[x].index])
                            new FormBattle(characters[x]).ShowDialog();
                    }

                    if (actionpoint[i].action >= 100) // start a battle with pokemon
                    {
                        int x = actionpoint[i].action - 100;

                        new FormBattle(wildpokemons[x], true).ShowDialog();
                    }

                    break;
                }
            }
        }

        int actionAfterDialog;
        public void DoActionAfterDialog()
        {
            if (actionAfterDialog > 0)
            {
                if (actionAfterDialog == 1) // fight with John the Lumberjack
                {
                    Character trainer = characters[0];
                    trainer.pokemons.Add(new Pokemons(285, 7));
                    trainer.pokemons.Add(new Pokemons(46, 8));
                    trainer.pokemons.Add(new Pokemons(163, 11));
                    trainer.pokemons.Add(new Pokemons(412, 12));

                    new FormBattle(trainer).ShowDialog();
                }

                if (actionAfterDialog == 2) // gym battles
                {
                    lastTimeGym = FormGame.gameTime;
                    new FormBattle(characters[0]).ShowDialog();
                }

                if (actionAfterDialog == 3) // Lucas the Fighter prize
                {
                    player.pokemons[0].LearnMove(new Attack(264));
                }

                if (actionAfterDialog == 4) // arena wait time
                {
                    lastTimeGym = FormGame.gameTime;
                }

                if (actionAfterDialog == 5) // Cameraman's question
                {
                    new FormQuestion(mainForm).Show();
                }

                actionAfterDialog = 0;
            }
        }

        public static bool[] trainerDefeated = new bool[200];
        public static bool[] pokemonDefeated = new bool[494];
        public static int lastTimeGym = -1000;

        public void StopActionFromTrainer(Character trainer)
        {
            if (trainer.index == 117)
            {
                FormGame.map = new Map(mapNumber, FormGame.OX - 7, FormGame.OY - 6);
                return;
            }

            trainerDefeated[trainer.index] = true;

            if(trainer.index >= 118 && trainer.index <= 120)
            {
                player.HealTeam();
                characters.Clear();
                actionpoint.Clear();
                LoadTournament(trainer.index - 116);
                return;
            }

            if(trainer.index == 121)
            {
                new FormEndGame(1).Show();
                mainForm.Close();
            }

            if (trainer.index == 9) // first gym
            {
                mainForm.ShowMessage("John the Lumberjack: Aaaaah, not again! I'm surprised. Congratulations! Here, take this badge, you deserve it! Oh, and take this, too. It is a HM: Cut. You can teach your pokemons this move how many times you want. Check your bag.");
                player.TMs.Add(new Attack(15)); // give Cut
                player.badges = 1;
            }

            if (trainer.index == 19) // second gym
            {
                mainForm.ShowMessage("Desmond: Excellent skills! Here is your badge and a pleasant surprise - TM: Calm Mind. But be careful! You can teach your pokemons a HM how many times you want, but after you teach one a TM, you will lose the TM forever. The Calm Mind should help you approach the Haunted House without fear.");
                player.TMs.Add(new Attack(347)); // give Calm Mind
                player.badges = 2;
            }

            if (trainer.index == 25) // third gym
            {
                mainForm.ShowMessage("Magic Stan: I guess you can't be a great magician in a small village like this. But you have talent. Here, take HM: Surf. With it you can surf on water and go towards new horizons!");
                player.TMs.Add(new Attack(57)); // give Surf
                player.badges = 3;
            }

            if (trainer.index == 64) // fourth gym
            {
                mainForm.ShowMessage("Aviana: Oh my God! I was so blinded by power that i didn't notice how polluted our waters were. I should be taking care of these problems right away!");
                player.badges = 4;
            }

            if (trainer.index == 79) // fifth gym
            {
                mainForm.ShowMessage("Farmer: YOOHOO! What a battle! Take a pair of goggles to go in the desert. Oh, and you should take this TM: Dig. Dig in the desert to find hidden secrets.");
                player.TMs.Add(new Attack(91)); // give Dig
                player.badges = 5;
            }

            if (trainer.index == 93) // sixth gym
            {
                mainForm.ShowMessage("Isolde: I feel something strange coming from the north.");
                player.badges = 6;
            }

            if (trainer.index == 99) // seventh gym
            {
                mainForm.ShowMessage("Charles&Clotilde Dubois: Mon dieu! We did not expect that! C'est dommage, you win...");
                player.badges = 7;
            }

            if (trainer.index == 116) // eighth gym
            {
                mainForm.ShowMessage("Edmund the Wise: Oh, you are gifted. I guess this is your last badge. Take it and become the tournament champion!");
                player.badges = 8;
            }

            if (trainer.index == 103) // fake gym
            {
                mainForm.ShowMessage(player.name + ": Oh, you are the leader of Team Rocket!\nGiovanni: How could a prick interfere with my plans?\n\nYou recovered a Master Ball from Team Rocket!");
                Pokeball pball = new Pokeball(4);
                pball.number = 1;
                player.pokeballs.Add(pball);
            }

            if (trainer.index >= 39 && trainer.index <= 43 && trainerDefeated[39] && trainerDefeated[40]
                && trainerDefeated[41] && trainerDefeated[42] && trainerDefeated[43]) // open gates for Lucas the Fighter
            {
                mainForm.ShowMessage("Lucas the Fighter: Hmm, I know you. You beat my older brother, John the Lumberjack. But can you handle my superior strength?");
                structures.Clear();
            }

            if (trainer.index == 38) // Lucas the Fighter
            {
                mainForm.ShowMessage("Lucas the Fighter: What the hell... whatever, you deserve the prize. I will teach your pokemon the best fighting move: Focus Punch. But be careful at its downsides.");
                actionAfterDialog = 3;
            }

            if (trainer.index >= 45 && trainer.index <= 49 && trainerDefeated[45] && trainerDefeated[46]
                && trainerDefeated[47] && trainerDefeated[48] && trainerDefeated[49]) // open gates for Dino Senior
            {
                mainForm.ShowMessage("Dino Senior: Haha, you're a shifty one!");
                structures.Clear();
            }

            if (trainer.index == 44) // Dino Senior
            {
                mainForm.ShowMessage("Dino Senior: Bravos! Take this TM: Draco Meteor. It is a very strong move, but not so many pokemons can learn it.");
                player.TMs.Add(new Attack(434)); // give Draco Meteor
            }

            if (trainer.index >= 54 && trainer.index <= 62) // Get evolution stones from Team Rocket
            {
                EvolutionItem evolitem = new EvolutionItem(trainer.index - 53);
                player.evolutionItems.Add(evolitem);
                mainForm.ShowMessage("You recovered " + evolitem.name + " from Team Rocket!");
            }

            if (trainer.index >= 54 && trainer.index <= 62 && trainerDefeated[54] && trainerDefeated[55]
                && trainerDefeated[56] && trainerDefeated[57] && trainerDefeated[58] && trainerDefeated[59]
                && trainerDefeated[60] && trainerDefeated[61] && trainerDefeated[62]) // Team Rocket Squad 1 defeated
            {
                FormGame.map = new Map(mapNumber, FormGame.OX - 7, FormGame.OY - 6);
            }

            if (trainer.index >= 88 && trainer.index <= 92) // Get other evolution items from Team Rocket
            {
                int aux = (trainer.index - 88) * 3;
                EvolutionItem evolitem1 = new EvolutionItem(aux + 10);
                EvolutionItem evolitem2 = new EvolutionItem(aux + 11);
                EvolutionItem evolitem3 = new EvolutionItem(aux + 12);
                player.evolutionItems.Add(evolitem1);
                player.evolutionItems.Add(evolitem2);
                player.evolutionItems.Add(evolitem3);
                mainForm.ShowMessage("You recovered " + evolitem1.name + ", " + evolitem2.name + " and " + evolitem3.name + " from Team Rocket!");
            }

            if (trainer.index >= 88 && trainer.index <= 92 && trainerDefeated[88] && trainerDefeated[89]
               && trainerDefeated[90] && trainerDefeated[91] && trainerDefeated[92]) // Team Rocket Squad 2 defeated
            {
                FormGame.map = new Map(mapNumber, FormGame.OX - 7, FormGame.OY - 6);
            }
        }

        public void StopActionFromPokemon(Pokemons poke)
        {
            pokemonDefeated[poke.index] = true;

            if (poke.index == 93) // Haunter (help Little Girl)
            {
                mainForm.ShowMessage("Little Girl: My hero! I was playing around and this evil Haunter appeared from nowhere. Maybe he came from the Haunted House next to the village...");
            }

            if (poke.index == 386 || poke.index == 488 || poke.index == 249) // Deoxys, Cresselia, Lugia will break free from your team
                PokemonBreakFree(poke.index);

            if (poke.index == 249 || (poke.index >= 144 && poke.index <= 146)) // legendary birds
            {
                wildpokemons.Clear();
                actionpoint.Clear();

                pokemonDefeated[249] = true;
                pokemonDefeated[144] = true;
                pokemonDefeated[145] = true;
                pokemonDefeated[146] = true;

                FormGame.map = new Map(43, 410, 150);
                mainForm.ShowMessage("That was strange...");
            }

            if(legendariesCount == -2) // legendary void
            {
                FormGame.map = new Map(49, 385, 515);
            }

            int j = -1;

            for (int i = 0; i < wildpokemons.Count; ++i)
            {
                if (wildpokemons[i].index == poke.index)
                {
                    j = i;
                    wildpokemons.RemoveAt(j);
                    break;
                }
            }

            for (int i = 0; i < actionpoint.Count; ++i)
            {
                if (actionpoint[i].action == j + 100)
                {
                    actionpoint.RemoveAt(i);
                    break;
                }
            }
        }

        public void PokemonBreakFree(int index)
        {
            if (player.usedMasterBall) return;
            
            for (int i = 0; i < player.pokemons.Count; ++i)
            {
                Pokemons poke = player.pokemons[i];

                if (poke.index == index)
                {
                    mainForm.ShowMessage(poke.name + " broke free from your team!");
                    player.pokemons.RemoveAt(i);
                    return;
                }
            }

            for (int i = 0; i < player.pokemonsLab.Count; ++i)
            {
                Pokemons poke = player.pokemonsLab[i];

                if (poke.index == index)
                {
                    mainForm.ShowMessage(poke.name + " broke free from the laboratory!");
                    player.pokemonsLab.RemoveAt(i);
                    return;
                }
            }
        }

        public void ActionCutTrees(int X, int Y)
        {
            for (int i = 0; i < trees.Count; ++i)
            {
                if (trees[i].type == 2)
                {
                    if (X - 40 <= trees[i].X && X + 77 >= trees[i].X + trees[i].sizeX
                        && Y - 60 <= trees[i].Y && Y + 98 >= trees[i].Y + trees[i].sizeY)
                    {
                        trees.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        public void ActionSurf(int X, int Y)
        {
            for (int i = 0; i < structures.Count; ++i)
            {
                if (structures[i].type == 1 && structures[i].subtype == 1 && structures[i].subtype2 != 0)
                {
                    if (X - 40 <= structures[i].X && X + 97 >= structures[i].X + structures[i].sizeX
                        && Y - 60 <= structures[i].Y && Y + 98 >= structures[i].Y + structures[i].sizeY)
                    {
                        int mapNr = 0, x = 0, y = 0;
                        if (mapNumber == 14) { mapNr = 15; x = 100; y = 35; }
                        if (mapNumber == 17) { mapNr = 16; x = 670; y = 370; }
                        if (mapNumber == 20) { mapNr = 21; x = 100; y = 35; }
                        if (mapNumber == 22) { mapNr = 21; x = 390; y = 480; }
                        if (mapNumber == 30) { mapNr = 31; x = 325; y = 480; }
                        if (mapNumber == 37 || mapNumber == 38) { mapNr = 41; x = 400; y = 70; }
                        if (mapNumber == 43) { mapNr = 42; x = 400; y = 380; }

                        FormGame.map = new Map(mapNr, x, y);
                        break;
                    }
                }
            }
        }

        public void ActionDig(int X, int Y)
        {
            if (rand.Next() % 100 + 1 < 90) return;

            if (mapNumber == 25) // desert
            {
                FormBattle fbattle = new FormBattle(new Pokemons(246, rand.Next(3, 6)), false);
                fbattle.weatherType = 3;
                fbattle.weatherDuration = 100;
                fbattle.ShowDialog();
            }

            if (!pokemonDefeated[142]) // ancient cave
            {
                if (mapNumber == 32)
                {
                    if (X >= 550 && X <= 660 && Y >= 10 && Y <= 60)
                    {
                        FormGame.map = new Map(33, 530, 450);
                    }
                }
                if (mapNumber == 33)
                {
                    int x = rand.Next() % 100 + 1;
                    int y = 374;
                    if (x <= 25) y = 345;
                    else if (x <= 50) y = 347;
                    else if (x <= 75) y = 408;
                    else if (x <= 90) y = 410;
                    else if (x <= 95) y = 436;
                    new FormBattle(new Pokemons(y, rand.Next(3, 6)), false).ShowDialog();
                }
            }

            if(mapNumber == 45) // ancient cave 2
            {
                if (!pokemonDefeated[377] && !pokemonDefeated[378] && !pokemonDefeated[379] && !pokemonDefeated[486])
                    FormGame.map = new Map(46, 530, 450);
            }
        }

        #endregion

        #region Paint

        public void Paint(Graphics canvas)
        {
            if (FormGame.gameIsPaused) return;

            // change character's animation
            if (pbCanvas.BackgroundImage == Resources.waterwaves) player.ChangeSpriteWater();
            else player.ChangeSprite();

            for (int i = 0; i < hills.Count; ++i) // draw hills
            {
                canvas.DrawImage(hills[i].image, hills[i].X, hills[i].Y);
            }

            for (int i = 0; i < flowers.Count; ++i) // draw flowers
            {
                flowers[i].ChangeSprite();
                canvas.DrawImage(flowers[i].image, flowers[i].X, flowers[i].Y);
            }

            if (pbCanvas.BackgroundImage == Resources.grass || pbCanvas.BackgroundImage == Resources.snow)
                for (int i = 0; i < wildgrass.Count; ++i) // draw wild grass
                {
                    canvas.DrawImage(wildgrass[i].image, wildgrass[i].X, wildgrass[i].Y);
                }

            for (int i = 0; i < paths.Count; ++i) // draw paths
            {
                canvas.DrawImage(paths[i].image, paths[i].X, paths[i].Y);
            }

            if (!player.isInFront) canvas.DrawImage(player.crtSprite, FormGame.OX, FormGame.OY);

            for (int i = 0; i < buildings.Count; ++i) // draw buildings
            {
                canvas.DrawImage(buildings[i].image, buildings[i].X, buildings[i].Y);
            }

            for (int i = 0; i < trees.Count; ++i) // draw trees
            {
                canvas.DrawImage(trees[i].image, trees[i].X, trees[i].Y);
            }

            for (int i = 0; i < structures.Count; ++i) // draw structures
            {
                canvas.DrawImage(structures[i].image, structures[i].X, structures[i].Y);
            }

            for (int i = 0; i < characters.Count; ++i) // draw characters
            {
                canvas.DrawImage(characters[i].crtSprite, characters[i].X, characters[i].Y);
            }

            for (int i = 0; i < wildpokemons.Count; ++i) // draw pokemons
            {
                canvas.DrawImage(Resources.pokeFront[wildpokemons[i].index], wildpokemons[i].X, wildpokemons[i].Y);
            }

            if (player.isInFront)
            {
                if (pbCanvas.BackgroundImage == Resources.waterwaves)
                    canvas.DrawImage(Resources.surfboard, FormGame.OX - 20, FormGame.OY + 10);

                canvas.DrawImage(player.crtSprite, FormGame.OX, FormGame.OY);
            }

            if (legendariesCount >= 0 && legendariesCount <= 13)
            {
                canvas.DrawImage(Resources.pokeFront[legendaries[legendariesCount]], rand.Next(100, 700), rand.Next(100, 500));
            }

            WildGrass(FormGame.OX, FormGame.OY); // check for battle events
        }

        #endregion

        #region Wild Grass - Battle Event

        Random rand = new Random();
        Timer timerFormBattle;

        private void WildGrass(int X, int Y)
        {
            bool oneIsVisited = false;

            for (int i = 0; i < wildgrass.Count; ++i)
            {
                if (X + 10 >= wildgrass[i].X + 0 && X + 27 <= wildgrass[i].X + 50 &&
                    Y + 48 >= wildgrass[i].Y + 10 && Y + 48 <= wildgrass[i].Y + 40 &&
                    !oneIsVisited)
                {
                    if (!wildgrass[i].isVisited)
                    {
                        wildgrass[i].isVisited = true;

                        int rate = 4;
                        if (pbCanvas.BackgroundImage != Resources.grass) rate = 10;

                        if (rand.Next() % rate == 0)
                        {
                            oneIsVisited = true;

                            timerFormBattle.Start();

                            Keyboard.ClearStack();
                            player.ChangeSprite();
                        }
                    }
                }
                else wildgrass[i].isVisited = false;
            }
        }

        void timerFormBattle_Tick(object sender, EventArgs e)
        {
            timerFormBattle.Stop();

            int id = 0;
            int x = rand.Next() % 100 + 1;
            while (x >= 0)
            {
                if (x > wildPokemons[id].chance)
                    x -= wildPokemons[id].chance;
                else break;

                ++id;
            }

            new FormBattle(new Pokemons(wildPokemons[id].index,
                rand.Next(wildPokemons[id].lmin, wildPokemons[id].lmax + 1)), false).ShowDialog();
        }

        #endregion

        /* 
         * 
         * 
         * 
         *  THE CODE BELOW IS GENERATED AUTOMATICALLY
         *  USING THE MAP EDITOR (AdminMapEditor.cs)
         *
         *  THE MAP EDITOR IS NOT UP-TO-DATE AND IT
         *  WAS CREATED TO MAKE THE MAP EDITING
         *  EASIER
         * 
         *  MAP EDITOR'S INTERFACE IS NOT INTUITIVE
         * 
         * 
         * 
         */

        #region Load Maps

        private void LoadMap1()
        {
            pbCanvas.BackgroundImage = Resources.grass;

            exit.Add(new ExitPoint(250, 600, 550, 650, 2, 375, 0));

            wildPokemons.Add(new ints(10, 1, 3, 28));
            wildPokemons.Add(new ints(13, 1, 3, 28));
            wildPokemons.Add(new ints(265, 1, 3, 28));
            wildPokemons.Add(new ints(401, 2, 4, 15));
            wildPokemons.Add(new ints(250, 50, 50, 1));

            #region Environment
            buildings.Add(new Buildings(4, 180, 120, false));
            buildings.Add(new Buildings(1, 430, 120, true));

            flowers.Add(new Flowers(180, 270));
            flowers.Add(new Flowers(220, 270));
            flowers.Add(new Flowers(260, 270));

            flowers.Add(new Flowers(490, 270));
            flowers.Add(new Flowers(530, 270));
            flowers.Add(new Flowers(570, 270));

            wildgrass.Add(new WildGrass(180, 360));
            wildgrass.Add(new WildGrass(220, 360));
            wildgrass.Add(new WildGrass(260, 360));
            wildgrass.Add(new WildGrass(180, 400));
            wildgrass.Add(new WildGrass(220, 400));
            wildgrass.Add(new WildGrass(260, 400));

            wildgrass.Add(new WildGrass(490, 360));
            wildgrass.Add(new WildGrass(530, 360));
            wildgrass.Add(new WildGrass(570, 360));
            wildgrass.Add(new WildGrass(490, 400));
            wildgrass.Add(new WildGrass(530, 400));
            wildgrass.Add(new WildGrass(570, 400));

            trees.Add(new Trees(1, 0, 0));
            trees.Add(new Trees(1, 0, 60));
            trees.Add(new Trees(1, 0, 120));
            trees.Add(new Trees(1, 0, 180));
            trees.Add(new Trees(1, 0, 240));
            trees.Add(new Trees(1, 0, 300));
            trees.Add(new Trees(1, 0, 360));
            trees.Add(new Trees(1, 0, 420));
            trees.Add(new Trees(1, 0, 480));
            trees.Add(new Trees(1, 0, 540));

            trees.Add(new Trees(1, 80, 0));
            trees.Add(new Trees(1, 160, 0));
            trees.Add(new Trees(1, 240, 0));
            trees.Add(new Trees(1, 320, 0));
            trees.Add(new Trees(1, 400, 0));
            trees.Add(new Trees(1, 480, 0));
            trees.Add(new Trees(1, 560, 0));
            trees.Add(new Trees(1, 640, 0));
            trees.Add(new Trees(1, 720, 0));

            trees.Add(new Trees(1, 720, 60));
            trees.Add(new Trees(1, 720, 120));
            trees.Add(new Trees(1, 720, 180));
            trees.Add(new Trees(1, 720, 240));
            trees.Add(new Trees(1, 720, 300));
            trees.Add(new Trees(1, 720, 360));
            trees.Add(new Trees(1, 720, 420));
            trees.Add(new Trees(1, 720, 480));
            trees.Add(new Trees(1, 720, 540));

            trees.Add(new Trees(1, 80, 540));
            trees.Add(new Trees(1, 160, 540));
            trees.Add(new Trees(1, 240, 540));
            trees.Add(new Trees(1, 480, 540));
            trees.Add(new Trees(1, 560, 540));
            trees.Add(new Trees(1, 640, 540));
            #endregion
        }

        private void LoadMap2()
        {
            pbCanvas.BackgroundImage = Resources.grass;

            exit.Add(new ExitPoint(250, 0, 550, 50, 1, 375, 550));
            exit.Add(new ExitPoint(0, 290, 40, 500, 3, 750, 390));
            exit.Add(new ExitPoint(300, 600, 530, 650, 4, 350, 0));

            wildPokemons.Add(new ints(10, 2, 3, 15));
            wildPokemons.Add(new ints(13, 2, 3, 15));
            wildPokemons.Add(new ints(265, 2, 3, 15));
            wildPokemons.Add(new ints(401, 2, 3, 10));
            wildPokemons.Add(new ints(290, 2, 3, 10));
            wildPokemons.Add(new ints(127, 3, 4, 5));
            wildPokemons.Add(new ints(16, 3, 4, 15));
            wildPokemons.Add(new ints(396, 3, 4, 15));

            characters.Add(new Character("Bug Catcher", Resources.character_bugcatcher, 470, 155));
            characters[0].pokemons.Add(new Pokemons(10, 3));
            characters[0].pokemons.Add(new Pokemons(10, 4));
            characters[0].pokemons.Add(new Pokemons(265, 3));
            characters[0].index = 0;
            actionpoint.Add(new ActionPoint(470, 155, 520, 200, 0));

            characters.Add(new Character("Bug Catcher", Resources.character_bugcatcher, 395, 470));
            characters[1].pokemons.Add(new Pokemons(13, 3));
            characters[1].pokemons.Add(new Pokemons(401, 4));
            characters[1].pokemons.Add(new Pokemons(401, 4));
            characters[1].pokemons.Add(new Pokemons(401, 4));
            characters[1].index = 1;
            actionpoint.Add(new ActionPoint(395, 470, 445, 515, 1));

            characters.Add(new Character("Bug Catcher", Resources.character_bugcatcher, 80, 450));
            characters[2].pokemons.Add(new Pokemons(127, 8));
            characters[2].index = 2;
            actionpoint.Add(new ActionPoint(80, 520, 130, 565, 2));

            characters.Add(new Character("Bird Observer", Resources.character_bugcatcher2, 725, 290));
            characters[3].pokemons.Add(new Pokemons(396, 5));
            characters[3].pokemons.Add(new Pokemons(16, 6));
            characters[3].index = 3;
            actionpoint.Add(new ActionPoint(700, 290, 775, 335, 3));

            #region Environment
            trees.Add(new Trees(1, 0, 0));
            trees.Add(new Trees(1, 80, 0));
            trees.Add(new Trees(1, 160, 0));
            trees.Add(new Trees(1, 240, 0));
            trees.Add(new Trees(1, 480, 0));
            trees.Add(new Trees(1, 560, 0));
            trees.Add(new Trees(1, 640, 0));
            trees.Add(new Trees(1, 720, 0));

            trees.Add(new Trees(1, -70, 140));
            trees.Add(new Trees(1, -40, 520));
            trees.Add(new Trees(1, 230, 570));
            trees.Add(new Trees(1, 80, 550));
            trees.Add(new Trees(1, 500, 570));
            trees.Add(new Trees(1, 640, 540));
            trees.Add(new Trees(1, 730, 560));
            trees.Add(new Trees(1, 30, 60));
            trees.Add(new Trees(1, 120, 130));
            trees.Add(new Trees(1, 5, 210));
            trees.Add(new Trees(1, 200, 350));
            trees.Add(new Trees(1, 340, 230));
            trees.Add(new Trees(1, 70, 400));
            trees.Add(new Trees(1, 560, 400));
            trees.Add(new Trees(1, 580, 260));
            trees.Add(new Trees(1, 550, 120));
            trees.Add(new Trees(1, 700, 380));
            trees.Add(new Trees(1, 750, 230));
            trees.Add(new Trees(1, 680, 60));

            wildgrass.Add(new WildGrass(320, 350));
            wildgrass.Add(new WildGrass(360, 350));
            wildgrass.Add(new WildGrass(400, 350));
            wildgrass.Add(new WildGrass(440, 350));
            wildgrass.Add(new WildGrass(480, 350));
            wildgrass.Add(new WildGrass(520, 350));
            wildgrass.Add(new WildGrass(320, 390));
            wildgrass.Add(new WildGrass(360, 390));
            wildgrass.Add(new WildGrass(400, 390));
            wildgrass.Add(new WildGrass(440, 390));
            wildgrass.Add(new WildGrass(480, 390));
            wildgrass.Add(new WildGrass(520, 390));
            wildgrass.Add(new WildGrass(320, 430));
            wildgrass.Add(new WildGrass(360, 430));
            wildgrass.Add(new WildGrass(400, 430));
            wildgrass.Add(new WildGrass(440, 430));
            wildgrass.Add(new WildGrass(480, 430));
            wildgrass.Add(new WildGrass(520, 430));

            wildgrass.Add(new WildGrass(230, 110));
            wildgrass.Add(new WildGrass(270, 110));
            wildgrass.Add(new WildGrass(230, 150));
            wildgrass.Add(new WildGrass(270, 150));
            wildgrass.Add(new WildGrass(230, 190));
            wildgrass.Add(new WildGrass(270, 190));
            wildgrass.Add(new WildGrass(230, 230));
            wildgrass.Add(new WildGrass(270, 230));
            wildgrass.Add(new WildGrass(230, 270));
            wildgrass.Add(new WildGrass(270, 270));
            wildgrass.Add(new WildGrass(190, 270));
            wildgrass.Add(new WildGrass(150, 270));
            wildgrass.Add(new WildGrass(110, 270));
            wildgrass.Add(new WildGrass(190, 230));
            wildgrass.Add(new WildGrass(150, 230));
            wildgrass.Add(new WildGrass(110, 230));

            wildgrass.Add(new WildGrass(180, 470));
            wildgrass.Add(new WildGrass(220, 470));
            wildgrass.Add(new WildGrass(260, 470));
            wildgrass.Add(new WildGrass(180, 510));
            wildgrass.Add(new WildGrass(220, 510));
            wildgrass.Add(new WildGrass(260, 510));

            wildgrass.Add(new WildGrass(660, 150));
            wildgrass.Add(new WildGrass(700, 190));
            wildgrass.Add(new WildGrass(700, 150));
            wildgrass.Add(new WildGrass(660, 190));

            flowers.Add(new Flowers(685, 300));
            flowers.Add(new Flowers(410, 140));
            flowers.Add(new Flowers(470, 250));
            #endregion
        }

        private void LoadMap3()
        {
            pbCanvas.BackgroundImage = Resources.grass;

            exit.Add(new ExitPoint(770, 290, 800, 520, 2, 0, 390));

            wildPokemons.Add(new ints(412, 2, 3, 50));
            wildPokemons.Add(new ints(163, 2, 4, 5));
            wildPokemons.Add(new ints(46, 2, 3, 10));
            wildPokemons.Add(new ints(285, 2, 3, 20));
            wildPokemons.Add(new ints(273, 2, 3, 15));

            #region Environment
            trees.Add(new Trees(1, 0, 0));
            trees.Add(new Trees(1, 0, 60));
            trees.Add(new Trees(1, 0, 120));
            trees.Add(new Trees(1, 0, 180));
            trees.Add(new Trees(1, 0, 240));
            trees.Add(new Trees(1, 0, 300));
            trees.Add(new Trees(1, 0, 360));
            trees.Add(new Trees(1, 0, 420));
            trees.Add(new Trees(1, 0, 480));
            trees.Add(new Trees(1, 0, 540));

            trees.Add(new Trees(1, 80, 0));
            trees.Add(new Trees(1, 80, 60));
            trees.Add(new Trees(1, 80, 120));
            trees.Add(new Trees(1, 80, 180));
            trees.Add(new Trees(1, 80, 240));
            trees.Add(new Trees(1, 80, 300));
            trees.Add(new Trees(1, 80, 360));
            trees.Add(new Trees(1, 80, 420));
            trees.Add(new Trees(1, 80, 480));
            trees.Add(new Trees(1, 80, 540));

            trees.Add(new Trees(1, 160, 0));
            trees.Add(new Trees(1, 240, 0));
            trees.Add(new Trees(1, 320, 0));
            trees.Add(new Trees(1, 400, 0));
            trees.Add(new Trees(1, 480, 0));
            trees.Add(new Trees(1, 560, 0));
            trees.Add(new Trees(1, 640, 0));
            trees.Add(new Trees(1, 720, 0));

            trees.Add(new Trees(1, 80, 540));
            trees.Add(new Trees(1, 160, 540));
            trees.Add(new Trees(1, 240, 540));
            trees.Add(new Trees(1, 320, 540));
            trees.Add(new Trees(1, 400, 540));
            trees.Add(new Trees(1, 480, 540));
            trees.Add(new Trees(1, 560, 540));
            trees.Add(new Trees(1, 640, 540));

            trees.Add(new Trees(1, 720, 60));
            trees.Add(new Trees(1, 680, 120));
            trees.Add(new Trees(1, 700, 200));
            trees.Add(new Trees(1, 650, 270));
            trees.Add(new Trees(1, 720, 480));
            trees.Add(new Trees(1, 720, 540));

            trees.Add(new Trees(1, 550, 125));

            trees.Add(new Trees(1, 200, 140));
            trees.Add(new Trees(1, 240, 420));

            buildings.Add(new Buildings(3, 300, 230, false));

            wildgrass.Add(new WildGrass(310, 130));
            wildgrass.Add(new WildGrass(350, 130));
            wildgrass.Add(new WildGrass(390, 130));
            wildgrass.Add(new WildGrass(430, 130));
            wildgrass.Add(new WildGrass(310, 170));
            wildgrass.Add(new WildGrass(350, 170));
            wildgrass.Add(new WildGrass(390, 170));
            wildgrass.Add(new WildGrass(430, 170));
            wildgrass.Add(new WildGrass(310, 210));
            wildgrass.Add(new WildGrass(350, 210));
            wildgrass.Add(new WildGrass(390, 210));
            wildgrass.Add(new WildGrass(430, 210));

            flowers.Add(new Flowers(310, 370));
            flowers.Add(new Flowers(390, 370));
            flowers.Add(new Flowers(430, 370));
            #endregion
        }

        private void LoadMap4()
        {
            pbCanvas.BackgroundImage = Resources.grass;

            exit.Add(new ExitPoint(300, 0, 530, 50, 2, 400, 570));
            exit.Add(new ExitPoint(520, 570, 640, 600, 5, 620, 0));

            wildPokemons.Add(new ints(19, 3, 4, 18));
            wildPokemons.Add(new ints(43, 3, 4, 10));
            wildPokemons.Add(new ints(161, 3, 5, 18));
            wildPokemons.Add(new ints(187, 3, 4, 8));
            wildPokemons.Add(new ints(261, 3, 4, 18));
            wildPokemons.Add(new ints(263, 3, 4, 18));
            wildPokemons.Add(new ints(276, 3, 5, 10));

            characters.Add(new Character("Bird Observer", Resources.character_bugcatcher2, 480, 290));
            characters[0].pokemons.Add(new Pokemons(276, 6));
            characters[0].index = 5;
            actionpoint.Add(new ActionPoint(480, 290, 530, 335, 0));

            characters.Add(new Character("Kid", Resources.character_kid, 300, 420));
            characters[1].pokemons.Add(new Pokemons(19, 4));
            characters[1].pokemons.Add(new Pokemons(161, 5));
            characters[1].index = 6;
            actionpoint.Add(new ActionPoint(300, 420, 350, 465, 1));

            characters.Add(new Character("Kid", Resources.character_kid, 600, 50));
            characters[2].pokemons.Add(new Pokemons(263, 4));
            characters[2].pokemons.Add(new Pokemons(261, 4));
            characters[2].pokemons.Add(new Pokemons(263, 6));
            characters[2].pokemons.Add(new Pokemons(261, 5));
            characters[2].index = 7;
            actionpoint.Add(new ActionPoint(550, 50, 650, 95, 2));

            characters.Add(new Character("Kid", Resources.character_kid, 200, 200));
            characters[3].pokemons.Add(new Pokemons(43, 4));
            characters[3].pokemons.Add(new Pokemons(187, 4));
            characters[3].index = 8;
            actionpoint.Add(new ActionPoint(200, 200, 250, 245, 3));

            #region Environment
            trees.Add(new Trees(1, 50, 30));
            trees.Add(new Trees(1, 60, 150));
            trees.Add(new Trees(1, 20, 270));
            trees.Add(new Trees(1, 35, 410));

            trees.Add(new Trees(1, 370, 50));
            trees.Add(new Trees(1, 370, 110));
            trees.Add(new Trees(1, 450, 70));

            trees.Add(new Trees(1, 700, 20));
            trees.Add(new Trees(1, 680, 160));
            trees.Add(new Trees(1, 720, 310));
            trees.Add(new Trees(1, 665, 430));

            trees.Add(new Trees(1, 200, 480));
            trees.Add(new Trees(1, 350, 510));

            wildgrass.Add(new WildGrass(370, 200));
            wildgrass.Add(new WildGrass(410, 200));
            wildgrass.Add(new WildGrass(450, 200));
            wildgrass.Add(new WildGrass(490, 200));
            wildgrass.Add(new WildGrass(530, 200));

            wildgrass.Add(new WildGrass(450, 160));
            wildgrass.Add(new WildGrass(490, 160));
            wildgrass.Add(new WildGrass(530, 160));

            wildgrass.Add(new WildGrass(170, 300));
            wildgrass.Add(new WildGrass(210, 300));
            wildgrass.Add(new WildGrass(250, 300));
            wildgrass.Add(new WildGrass(290, 300));
            wildgrass.Add(new WildGrass(330, 300));
            wildgrass.Add(new WildGrass(370, 300));

            wildgrass.Add(new WildGrass(170, 340));
            wildgrass.Add(new WildGrass(210, 340));
            wildgrass.Add(new WildGrass(250, 340));
            wildgrass.Add(new WildGrass(290, 340));
            wildgrass.Add(new WildGrass(330, 340));
            wildgrass.Add(new WildGrass(370, 340));

            paths.Add(new Paths(7, 600, 270));
            paths.Add(new Paths(9, 640, 270));
            paths.Add(new Paths(4, 600, 310));
            paths.Add(new Paths(6, 640, 310));
            paths.Add(new Paths(4, 600, 350));
            paths.Add(new Paths(6, 640, 350));
            paths.Add(new Paths(5, 600, 390));
            paths.Add(new Paths(6, 640, 390));
            paths.Add(new Paths(2, 600, 430));
            paths.Add(new Paths(3, 640, 430));

            paths.Add(new Paths(8, 570, 390));
            paths.Add(new Paths(2, 570, 430));
            paths.Add(new Paths(7, 530, 390));
            paths.Add(new Paths(4, 530, 430));

            paths.Add(new Paths(6, 570, 460));
            paths.Add(new Paths(4, 530, 460));
            paths.Add(new Paths(6, 570, 500));
            paths.Add(new Paths(4, 530, 500));
            paths.Add(new Paths(6, 570, 540));
            paths.Add(new Paths(4, 530, 540));
            paths.Add(new Paths(6, 570, 580));
            paths.Add(new Paths(4, 530, 580));

            hills.Add(new Hills(170, 100, 200));
            hills.Add(new Hills(270, 230, 100));
            #endregion
        }

        private void LoadMap5()
        {
            pbCanvas.BackgroundImage = Resources.grass;

            exit.Add(new ExitPoint(600, 0, 720, 50, 4, 560, 540));
            exit.Add(new ExitPoint(370, 570, 450, 600, 6, 370, 0));

            #region Environment

            trees.Add(new Trees(1, 500, -28));
            trees.Add(new Trees(1, 36, -7));
            trees.Add(new Trees(1, 733, -4));
            trees.Add(new Trees(1, 247, 4));
            trees.Add(new Trees(1, 404, 50));
            trees.Add(new Trees(1, 104, 101));
            trees.Add(new Trees(1, 285, 158));
            trees.Add(new Trees(1, 725, 169));

            buildings.Add(new Buildings(0, 60, 216, true));
            buildings.Add(new Buildings(1, 440, 250, true));
            buildings.Add(new Buildings(2, 445, 444, true));

            flowers.Add(new Flowers(291, 480));
            flowers.Add(new Flowers(251, 480));
            flowers.Add(new Flowers(211, 480));

            paths.Add(new Paths(6, 662, -12));
            paths.Add(new Paths(4, 622, -12));
            paths.Add(new Paths(4, 622, 28));
            paths.Add(new Paths(6, 662, 28));
            paths.Add(new Paths(6, 662, 68));
            paths.Add(new Paths(4, 622, 68));
            paths.Add(new Paths(4, 622, 108));
            paths.Add(new Paths(6, 662, 108));
            paths.Add(new Paths(6, 662, 148));
            paths.Add(new Paths(4, 622, 148));
            paths.Add(new Paths(4, 622, 188));
            paths.Add(new Paths(6, 662, 188));
            paths.Add(new Paths(6, 662, 228));
            paths.Add(new Paths(4, 622, 228));
            paths.Add(new Paths(4, 622, 268));
            paths.Add(new Paths(6, 662, 268));
            paths.Add(new Paths(6, 662, 308));
            paths.Add(new Paths(4, 622, 308));
            paths.Add(new Paths(4, 622, 348));
            paths.Add(new Paths(6, 658, 348));
            paths.Add(new Paths(6, 658, 388));
            paths.Add(new Paths(3, 658, 428));
            paths.Add(new Paths(2, 618, 428));
            paths.Add(new Paths(5, 618, 388));
            paths.Add(new Paths(8, 585, 386));
            paths.Add(new Paths(2, 585, 426));
            paths.Add(new Paths(2, 545, 426));
            paths.Add(new Paths(8, 545, 386));
            paths.Add(new Paths(8, 505, 386));
            paths.Add(new Paths(2, 505, 426));
            paths.Add(new Paths(2, 465, 426));
            paths.Add(new Paths(8, 465, 386));
            paths.Add(new Paths(8, 425, 386));
            paths.Add(new Paths(2, 425, 426));
            paths.Add(new Paths(5, 385, 426));
            paths.Add(new Paths(8, 385, 386));
            paths.Add(new Paths(8, 345, 386));
            paths.Add(new Paths(5, 345, 426));
            paths.Add(new Paths(2, 305, 426));
            paths.Add(new Paths(8, 305, 386));
            paths.Add(new Paths(8, 265, 386));
            paths.Add(new Paths(2, 265, 426));
            paths.Add(new Paths(2, 225, 426));
            paths.Add(new Paths(8, 225, 386));
            paths.Add(new Paths(8, 185, 386));
            paths.Add(new Paths(2, 185, 426));
            paths.Add(new Paths(2, 145, 426));
            paths.Add(new Paths(5, 145, 386));
            paths.Add(new Paths(4, 105, 386));
            paths.Add(new Paths(1, 105, 426));
            paths.Add(new Paths(6, 386, 463));
            paths.Add(new Paths(4, 346, 463));
            paths.Add(new Paths(4, 346, 503));
            paths.Add(new Paths(6, 386, 503));
            paths.Add(new Paths(6, 386, 543));
            paths.Add(new Paths(4, 346, 543));
            paths.Add(new Paths(4, 346, 583));
            paths.Add(new Paths(6, 386, 583));
            paths.Add(new Paths(7, 105, 348));
            paths.Add(new Paths(9, 145, 348));

            trees.Add(new Trees(2, 338, 510));
            trees.Add(new Trees(2, 386, 510));
            trees.Add(new Trees(2, 290, 550));
            trees.Add(new Trees(2, 434, 550));

            #endregion
        }

        private void LoadMap6()
        {
            pbCanvas.BackgroundImage = Resources.grass;

            wildPokemons.Add(new ints(19, 4, 7, 30));
            wildPokemons.Add(new ints(161, 4, 7, 20));
            wildPokemons.Add(new ints(263, 4, 7, 30));
            wildPokemons.Add(new ints(29, 5, 8, 10));
            wildPokemons.Add(new ints(32, 5, 8, 10));

            exit.Add(new ExitPoint(350, 0, 430, 50, 5, 370, 530));
            exit.Add(new ExitPoint(776, 305, 797, 404, 7, 45, 327));

            characters.Add(new Character("Dad", Resources.character_dad, 201, 118));
            characters[0].index = 10;
            actionpoint.Add(new ActionPoint(191, 108, 261, 168, 0));
            characters[0].pokemons.Add(new Pokemons(34, 16));

            characters.Add(new Character("Little Girl", Resources.character_littlegirl, 230, 277));
            characters[1].index = 11;
            actionpoint.Add(new ActionPoint(220, 267, 290, 327, 1));
            characters[1].pokemons.Add(new Pokemons(29, 12));

            characters.Add(new Character("Little Boy", Resources.character_littleboy, 320, 430));
            characters[2].index = 12;
            actionpoint.Add(new ActionPoint(310, 420, 380, 480, 2));
            characters[2].pokemons.Add(new Pokemons(32, 12));

            characters.Add(new Character("Mom", Resources.character_mom, 644, 433));
            characters[3].index = 13;
            actionpoint.Add(new ActionPoint(634, 423, 704, 483, 3));
            characters[3].pokemons.Add(new Pokemons(31, 16));

            #region Environment

            trees.Add(new Trees(1, 18, 7));
            trees.Add(new Trees(1, 105, 196));
            trees.Add(new Trees(1, 26, 411));
            trees.Add(new Trees(1, 461, 457));
            trees.Add(new Trees(1, 225, 501));
            trees.Add(new Trees(1, 615, 513));

            flowers.Add(new Flowers(349, 514));
            flowers.Add(new Flowers(132, 498));
            flowers.Add(new Flowers(107, 353));
            flowers.Add(new Flowers(57, 126));
            flowers.Add(new Flowers(595, 431));
            flowers.Add(new Flowers(692, 431));
            flowers.Add(new Flowers(236, 383));

            wildgrass.Add(new WildGrass(503, 28));
            wildgrass.Add(new WildGrass(543, 28));
            wildgrass.Add(new WildGrass(583, 28));
            wildgrass.Add(new WildGrass(623, 28));
            wildgrass.Add(new WildGrass(663, 28));
            wildgrass.Add(new WildGrass(703, 28));
            wildgrass.Add(new WildGrass(703, 68));
            wildgrass.Add(new WildGrass(663, 68));
            wildgrass.Add(new WildGrass(623, 68));
            wildgrass.Add(new WildGrass(583, 68));
            wildgrass.Add(new WildGrass(543, 68));
            wildgrass.Add(new WildGrass(503, 68));
            wildgrass.Add(new WildGrass(503, 108));
            wildgrass.Add(new WildGrass(543, 108));
            wildgrass.Add(new WildGrass(583, 108));
            wildgrass.Add(new WildGrass(623, 108));
            wildgrass.Add(new WildGrass(663, 108));
            wildgrass.Add(new WildGrass(703, 108));
            wildgrass.Add(new WildGrass(703, 148));
            wildgrass.Add(new WildGrass(663, 148));
            wildgrass.Add(new WildGrass(623, 148));
            wildgrass.Add(new WildGrass(583, 148));
            wildgrass.Add(new WildGrass(543, 148));
            wildgrass.Add(new WildGrass(503, 148));
            wildgrass.Add(new WildGrass(503, 188));
            wildgrass.Add(new WildGrass(543, 188));
            wildgrass.Add(new WildGrass(583, 188));
            wildgrass.Add(new WildGrass(623, 188));
            wildgrass.Add(new WildGrass(663, 188));
            wildgrass.Add(new WildGrass(703, 188));
            wildgrass.Add(new WildGrass(703, 228));
            wildgrass.Add(new WildGrass(663, 228));
            wildgrass.Add(new WildGrass(623, 228));
            wildgrass.Add(new WildGrass(583, 228));
            wildgrass.Add(new WildGrass(543, 228));
            wildgrass.Add(new WildGrass(503, 228));

            paths.Add(new Paths(6, 379, -6));
            paths.Add(new Paths(6, 379, 34));
            paths.Add(new Paths(6, 379, 74));
            paths.Add(new Paths(6, 379, 114));
            paths.Add(new Paths(6, 379, 154));
            paths.Add(new Paths(4, 339, -6));
            paths.Add(new Paths(4, 339, 34));
            paths.Add(new Paths(4, 339, 74));
            paths.Add(new Paths(4, 339, 114));
            paths.Add(new Paths(4, 339, 154));
            paths.Add(new Paths(4, 339, 194));
            paths.Add(new Paths(4, 339, 234));
            paths.Add(new Paths(4, 339, 274));
            paths.Add(new Paths(4, 339, 314));
            paths.Add(new Paths(1, 339, 354));
            paths.Add(new Paths(6, 379, 194));
            paths.Add(new Paths(6, 379, 234));
            paths.Add(new Paths(6, 379, 274));
            paths.Add(new Paths(5, 379, 314));
            paths.Add(new Paths(8, 409, 313));
            paths.Add(new Paths(2, 379, 354));
            paths.Add(new Paths(2, 409, 353));
            paths.Add(new Paths(2, 449, 353));
            paths.Add(new Paths(2, 489, 353));
            paths.Add(new Paths(2, 529, 353));
            paths.Add(new Paths(2, 569, 353));
            paths.Add(new Paths(2, 609, 353));
            paths.Add(new Paths(2, 649, 353));
            paths.Add(new Paths(2, 689, 353));
            paths.Add(new Paths(2, 729, 353));
            paths.Add(new Paths(2, 769, 353));
            paths.Add(new Paths(8, 449, 313));
            paths.Add(new Paths(8, 489, 313));
            paths.Add(new Paths(8, 529, 313));
            paths.Add(new Paths(8, 569, 313));
            paths.Add(new Paths(8, 609, 313));
            paths.Add(new Paths(8, 649, 313));
            paths.Add(new Paths(8, 689, 313));
            paths.Add(new Paths(8, 729, 313));
            paths.Add(new Paths(8, 769, 313));

            #endregion
        }

        private void LoadMap7()
        {
            pbCanvas.BackgroundImage = Resources.grass;

            wildPokemons.Add(new ints(261, 4, 8, 30));
            wildPokemons.Add(new ints(276, 4, 8, 20));
            wildPokemons.Add(new ints(316, 4, 8, 25));
            wildPokemons.Add(new ints(453, 5, 8, 5));
            wildPokemons.Add(new ints(23, 4, 8, 10));
            wildPokemons.Add(new ints(434, 4, 8, 10));

            exit.Add(new ExitPoint(0, 306, 47, 408, 6, 733, 327));
            exit.Add(new ExitPoint(427, 568, 538, 597, 8, 467, 32));

            characters.Add(new Character("Stinky", Resources.character_kid, 68, 137));
            characters[0].index = 14;
            actionpoint.Add(new ActionPoint(58, 127, 128, 187, 0));
            characters[0].pokemons.Add(new Pokemons(434, 10));
            characters[0].pokemons.Add(new Pokemons(453, 12));

            characters.Add(new Character("Bug Catcher", Resources.character_bugcatcher, 227, 211));
            characters[1].index = 15;
            actionpoint.Add(new ActionPoint(217, 201, 287, 261, 1));
            characters[1].pokemons.Add(new Pokemons(12, 10));
            characters[1].pokemons.Add(new Pokemons(15, 10));
            characters[1].pokemons.Add(new Pokemons(23, 11));

            characters.Add(new Character("Kid", Resources.character_kid, 412, 145));
            characters[2].index = 16;
            actionpoint.Add(new ActionPoint(402, 135, 472, 195, 2));
            characters[2].pokemons.Add(new Pokemons(316, 7));
            characters[2].pokemons.Add(new Pokemons(316, 8));
            characters[2].pokemons.Add(new Pokemons(316, 9));
            characters[2].pokemons.Add(new Pokemons(316, 10));
            characters[2].pokemons.Add(new Pokemons(316, 11));
            characters[2].pokemons.Add(new Pokemons(316, 12));

            characters.Add(new Character("Kid", Resources.character_kid, 573, 246));
            characters[3].index = 17;
            actionpoint.Add(new ActionPoint(563, 236, 633, 296, 3));
            characters[3].pokemons.Add(new Pokemons(276, 10));
            characters[3].pokemons.Add(new Pokemons(263, 10));
            characters[3].pokemons.Add(new Pokemons(261, 11));
            characters[3].pokemons.Add(new Pokemons(274, 14));

            characters.Add(new Character("Bug Catcher", Resources.character_bugcatcher, 570, 442));
            characters[4].index = 18;
            actionpoint.Add(new ActionPoint(560, 432, 620, 492, 4));
            characters[4].pokemons.Add(new Pokemons(267, 10));
            characters[4].pokemons.Add(new Pokemons(269, 10));
            characters[4].pokemons.Add(new Pokemons(402, 10));

            #region Environment

            trees.Add(new Trees(1, 678, 7));
            trees.Add(new Trees(1, 460, 9));
            trees.Add(new Trees(1, 108, 25));
            trees.Add(new Trees(1, 296, 86));
            trees.Add(new Trees(1, 696, 144));
            trees.Add(new Trees(1, 628, 278));
            trees.Add(new Trees(1, 692, 456));

            flowers.Add(new Flowers(211, 128));
            flowers.Add(new Flowers(392, 38));
            flowers.Add(new Flowers(510, 182));
            flowers.Add(new Flowers(591, 77));
            flowers.Add(new Flowers(729, 306));
            flowers.Add(new Flowers(621, 400));
            flowers.Add(new Flowers(650, 525));

            wildgrass.Add(new WildGrass(45, 412));
            wildgrass.Add(new WildGrass(85, 412));
            wildgrass.Add(new WildGrass(125, 412));
            wildgrass.Add(new WildGrass(165, 412));
            wildgrass.Add(new WildGrass(205, 412));
            wildgrass.Add(new WildGrass(245, 412));
            wildgrass.Add(new WildGrass(285, 412));
            wildgrass.Add(new WildGrass(325, 412));
            wildgrass.Add(new WildGrass(365, 412));
            wildgrass.Add(new WildGrass(365, 452));
            wildgrass.Add(new WildGrass(365, 492));
            wildgrass.Add(new WildGrass(365, 532));
            wildgrass.Add(new WildGrass(285, 532));
            wildgrass.Add(new WildGrass(245, 532));
            wildgrass.Add(new WildGrass(205, 532));
            wildgrass.Add(new WildGrass(165, 532));
            wildgrass.Add(new WildGrass(125, 532));
            wildgrass.Add(new WildGrass(85, 532));
            wildgrass.Add(new WildGrass(45, 532));
            wildgrass.Add(new WildGrass(45, 492));
            wildgrass.Add(new WildGrass(45, 452));
            wildgrass.Add(new WildGrass(85, 452));
            wildgrass.Add(new WildGrass(125, 452));
            wildgrass.Add(new WildGrass(165, 452));
            wildgrass.Add(new WildGrass(205, 452));
            wildgrass.Add(new WildGrass(245, 452));
            wildgrass.Add(new WildGrass(285, 452));
            wildgrass.Add(new WildGrass(325, 452));
            wildgrass.Add(new WildGrass(325, 492));
            wildgrass.Add(new WildGrass(325, 532));
            wildgrass.Add(new WildGrass(285, 492));
            wildgrass.Add(new WildGrass(245, 492));
            wildgrass.Add(new WildGrass(205, 492));
            wildgrass.Add(new WildGrass(165, 492));
            wildgrass.Add(new WildGrass(125, 492));
            wildgrass.Add(new WildGrass(85, 492));

            paths.Add(new Paths(8, 409, 313));
            paths.Add(new Paths(8, 369, 313));
            paths.Add(new Paths(8, 329, 313));
            paths.Add(new Paths(8, 289, 313));
            paths.Add(new Paths(8, 249, 313));
            paths.Add(new Paths(8, 209, 313));
            paths.Add(new Paths(8, 169, 313));
            paths.Add(new Paths(8, 129, 313));
            paths.Add(new Paths(8, 89, 313));
            paths.Add(new Paths(8, 49, 313));
            paths.Add(new Paths(8, 9, 313));
            paths.Add(new Paths(8, -31, 313));
            paths.Add(new Paths(2, 369, 353));
            paths.Add(new Paths(2, 329, 353));
            paths.Add(new Paths(2, 289, 353));
            paths.Add(new Paths(2, 249, 353));
            paths.Add(new Paths(2, 209, 353));
            paths.Add(new Paths(2, 169, 353));
            paths.Add(new Paths(2, 129, 353));
            paths.Add(new Paths(2, 89, 353));
            paths.Add(new Paths(2, 49, 353));
            paths.Add(new Paths(2, 9, 353));
            paths.Add(new Paths(2, -31, 353));
            paths.Add(new Paths(8, 449, 313));
            paths.Add(new Paths(9, 489, 313));
            paths.Add(new Paths(2, 409, 353));
            paths.Add(new Paths(5, 449, 353));
            paths.Add(new Paths(6, 489, 353));
            paths.Add(new Paths(4, 445, 389));
            paths.Add(new Paths(6, 485, 389));
            paths.Add(new Paths(6, 485, 429));
            paths.Add(new Paths(6, 485, 469));
            paths.Add(new Paths(6, 485, 509));
            paths.Add(new Paths(6, 485, 549));
            paths.Add(new Paths(6, 485, 589));
            paths.Add(new Paths(4, 445, 429));
            paths.Add(new Paths(4, 445, 469));
            paths.Add(new Paths(4, 445, 509));
            paths.Add(new Paths(4, 445, 549));
            paths.Add(new Paths(4, 445, 589));

            #endregion
        }

        private void LoadMap8()
        {
            pbCanvas.BackgroundImage = Resources.townpavement;

            exit.Add(new ExitPoint(0, -5, 800, 50, 7, 450, 530));
            exit.Add(new ExitPoint(1, 120, 52, 187, 9, 724, 110));
            exit.Add(new ExitPoint(770, 120, 800, 187, 10, 65, 110));

            wildPokemons.Add(new ints(52, 4, 8, 20));
            wildPokemons.Add(new ints(431, 4, 8, 20));
            wildPokemons.Add(new ints(209, 4, 8, 15));
            wildPokemons.Add(new ints(133, 4, 8, 10));
            wildPokemons.Add(new ints(453, 4, 8, 5));
            wildPokemons.Add(new ints(439, 4, 8, 10));
            wildPokemons.Add(new ints(440, 4, 8, 10));
            wildPokemons.Add(new ints(236, 4, 8, 10));

            #region Environment

            structures.Add(new Structures(1, 1, 20, 0));
            structures.Add(new Structures(1, 1, 20, 30));
            structures.Add(new Structures(1, 1, 20, 60));
            structures.Add(new Structures(1, 1, 20, 170));
            structures.Add(new Structures(1, 1, 20, 200));
            structures.Add(new Structures(1, 1, 20, 230));
            structures.Add(new Structures(1, 1, 20, 260));
            structures.Add(new Structures(1, 1, 20, 290));
            structures.Add(new Structures(1, 1, 20, 320));
            structures.Add(new Structures(1, 1, 20, 350));
            structures.Add(new Structures(1, 1, 20, 380));
            structures.Add(new Structures(1, 1, 20, 410));
            structures.Add(new Structures(1, 1, 20, 440));
            structures.Add(new Structures(1, 1, 20, 470));
            structures.Add(new Structures(1, 1, 20, 500));
            structures.Add(new Structures(1, 1, 20, 530));

            structures.Add(new Structures(1, 1, 786, 0));
            structures.Add(new Structures(1, 1, 786, 30));
            structures.Add(new Structures(1, 1, 786, 60));
            structures.Add(new Structures(1, 1, 786, 170));
            structures.Add(new Structures(1, 1, 786, 200));
            structures.Add(new Structures(1, 1, 786, 230));
            structures.Add(new Structures(1, 1, 786, 260));
            structures.Add(new Structures(1, 1, 786, 290));
            structures.Add(new Structures(1, 1, 786, 320));
            structures.Add(new Structures(1, 1, 786, 350));
            structures.Add(new Structures(1, 1, 786, 380));
            structures.Add(new Structures(1, 1, 786, 410));
            structures.Add(new Structures(1, 1, 786, 440));
            structures.Add(new Structures(1, 1, 786, 470));
            structures.Add(new Structures(1, 1, 786, 500));
            structures.Add(new Structures(1, 1, 786, 530));

            structures.Add(new Structures(1, 0, 20, 560));
            structures.Add(new Structures(2, 0, 110, 520));
            structures.Add(new Structures(1, 0, 170, 560));
            structures.Add(new Structures(1, 0, 250, 560));
            structures.Add(new Structures(1, 0, 330, 560));
            structures.Add(new Structures(1, 0, 410, 560));
            structures.Add(new Structures(1, 0, 490, 560));
            structures.Add(new Structures(1, 0, 570, 560));
            structures.Add(new Structures(2, 0, 660, 520));
            structures.Add(new Structures(1, 0, 720, 560));

            structures.Add(new Structures(2, 0, 127, 261));
            structures.Add(new Structures(2, 0, 657, 261));

            buildings.Add(new Buildings(5, 180, 115, false));
            buildings.Add(new Buildings(5, 450, 115, false));
            buildings.Add(new Buildings(1, 98, 374, true));
            buildings.Add(new Buildings(2, 554, 374, true));
            buildings.Add(new Buildings(0, 331, 374, true));

            wildgrass.Add(new WildGrass(138, 265));
            wildgrass.Add(new WildGrass(178, 265));
            wildgrass.Add(new WildGrass(218, 265));
            wildgrass.Add(new WildGrass(258, 265));
            wildgrass.Add(new WildGrass(298, 265));
            wildgrass.Add(new WildGrass(338, 265));
            wildgrass.Add(new WildGrass(378, 265));
            wildgrass.Add(new WildGrass(418, 265));
            wildgrass.Add(new WildGrass(458, 265));
            wildgrass.Add(new WildGrass(498, 265));
            wildgrass.Add(new WildGrass(538, 265));
            wildgrass.Add(new WildGrass(578, 265));
            wildgrass.Add(new WildGrass(618, 265));
            wildgrass.Add(new WildGrass(658, 265));
            wildgrass.Add(new WildGrass(698, 265));
            wildgrass.Add(new WildGrass(738, 265));
            wildgrass.Add(new WildGrass(738, 305));
            wildgrass.Add(new WildGrass(698, 305));
            wildgrass.Add(new WildGrass(658, 305));
            wildgrass.Add(new WildGrass(618, 305));
            wildgrass.Add(new WildGrass(578, 305));
            wildgrass.Add(new WildGrass(538, 305));
            wildgrass.Add(new WildGrass(498, 305));
            wildgrass.Add(new WildGrass(458, 305));
            wildgrass.Add(new WildGrass(418, 305));
            wildgrass.Add(new WildGrass(378, 305));
            wildgrass.Add(new WildGrass(338, 305));
            wildgrass.Add(new WildGrass(298, 305));
            wildgrass.Add(new WildGrass(258, 305));
            wildgrass.Add(new WildGrass(218, 305));
            wildgrass.Add(new WildGrass(178, 305));
            wildgrass.Add(new WildGrass(138, 305));
            wildgrass.Add(new WildGrass(98, 305));
            wildgrass.Add(new WildGrass(58, 305));
            wildgrass.Add(new WildGrass(58, 265));
            wildgrass.Add(new WildGrass(98, 265));
            wildgrass.Add(new WildGrass(58, 345));
            wildgrass.Add(new WildGrass(98, 345));
            wildgrass.Add(new WildGrass(138, 345));
            wildgrass.Add(new WildGrass(178, 345));
            wildgrass.Add(new WildGrass(218, 345));
            wildgrass.Add(new WildGrass(258, 345));
            wildgrass.Add(new WildGrass(298, 345));
            wildgrass.Add(new WildGrass(338, 345));
            wildgrass.Add(new WildGrass(378, 345));
            wildgrass.Add(new WildGrass(418, 345));
            wildgrass.Add(new WildGrass(458, 345));
            wildgrass.Add(new WildGrass(498, 345));
            wildgrass.Add(new WildGrass(538, 345));
            wildgrass.Add(new WildGrass(578, 345));
            wildgrass.Add(new WildGrass(618, 345));
            wildgrass.Add(new WildGrass(658, 345));
            wildgrass.Add(new WildGrass(698, 345));
            wildgrass.Add(new WildGrass(738, 345));
            wildgrass.Add(new WildGrass(738, 385));
            wildgrass.Add(new WildGrass(738, 425));
            wildgrass.Add(new WildGrass(738, 465));
            wildgrass.Add(new WildGrass(738, 505));
            wildgrass.Add(new WildGrass(738, 545));
            wildgrass.Add(new WildGrass(698, 545));
            wildgrass.Add(new WildGrass(658, 545));
            wildgrass.Add(new WildGrass(618, 545));
            wildgrass.Add(new WildGrass(578, 545));
            wildgrass.Add(new WildGrass(538, 545));
            wildgrass.Add(new WildGrass(498, 545));
            wildgrass.Add(new WildGrass(458, 545));
            wildgrass.Add(new WildGrass(418, 545));
            wildgrass.Add(new WildGrass(378, 545));
            wildgrass.Add(new WildGrass(338, 545));
            wildgrass.Add(new WildGrass(298, 545));
            wildgrass.Add(new WildGrass(258, 545));
            wildgrass.Add(new WildGrass(218, 545));
            wildgrass.Add(new WildGrass(178, 545));
            wildgrass.Add(new WildGrass(138, 545));
            wildgrass.Add(new WildGrass(98, 545));
            wildgrass.Add(new WildGrass(58, 545));
            wildgrass.Add(new WildGrass(58, 505));
            wildgrass.Add(new WildGrass(58, 465));
            wildgrass.Add(new WildGrass(58, 425));
            wildgrass.Add(new WildGrass(58, 385));
            wildgrass.Add(new WildGrass(418, 225));
            wildgrass.Add(new WildGrass(418, 185));
            wildgrass.Add(new WildGrass(418, 145));
            wildgrass.Add(new WildGrass(698, 145));
            wildgrass.Add(new WildGrass(698, 185));
            wildgrass.Add(new WildGrass(698, 225));
            wildgrass.Add(new WildGrass(138, 145));
            wildgrass.Add(new WildGrass(138, 185));
            wildgrass.Add(new WildGrass(138, 225));
            wildgrass.Add(new WildGrass(98, 225));
            wildgrass.Add(new WildGrass(98, 185));
            wildgrass.Add(new WildGrass(98, 145));

            #endregion

        }

        private void LoadMap9()
        {
            pbCanvas.BackgroundImage = Resources.townpavement;

            exit.Add(new ExitPoint(770, 137, 800, 187, 8, 65, 110));

            wildPokemons.Add(new ints(88, 4, 8, 30));
            wildPokemons.Add(new ints(109, 4, 8, 20));
            wildPokemons.Add(new ints(137, 4, 8, 15));
            wildPokemons.Add(new ints(353, 4, 8, 5));
            wildPokemons.Add(new ints(100, 4, 8, 30));

            if (!pokemonDefeated[93])
            {
                mainForm.ShowMessage("Little Girl: Help me!!");

                wildpokemons.Add(new Pokemons(93, 25, 600, 15));
                actionpoint.Add(new ActionPoint(580, 0, 650, 65, 100));
            }

            characters.Add(new Character("Little Girl", Resources.character_littlegirl, 560, 40));

            #region Environment

            structures.Add(new Structures(1, 1, 786, 0));
            structures.Add(new Structures(1, 1, 786, 30));
            structures.Add(new Structures(1, 1, 786, 60));
            structures.Add(new Structures(1, 1, 786, 170));
            structures.Add(new Structures(1, 1, 786, 200));
            structures.Add(new Structures(1, 1, 786, 230));
            structures.Add(new Structures(1, 1, 786, 260));
            structures.Add(new Structures(1, 1, 786, 290));
            structures.Add(new Structures(1, 1, 786, 320));
            structures.Add(new Structures(1, 1, 786, 350));
            structures.Add(new Structures(1, 1, 786, 380));
            structures.Add(new Structures(1, 1, 786, 410));
            structures.Add(new Structures(1, 1, 786, 440));
            structures.Add(new Structures(1, 1, 786, 470));
            structures.Add(new Structures(1, 1, 786, 500));
            structures.Add(new Structures(1, 1, 786, 530));
            structures.Add(new Structures(1, 1, 786, 560));

            structures.Add(new Structures(1, 2, 6, 200, 0));
            structures.Add(new Structures(1, 2, 6, 200, 40));
            structures.Add(new Structures(1, 2, 6, 200, 80));
            structures.Add(new Structures(1, 2, 6, 200, 120));
            structures.Add(new Structures(1, 2, 6, 200, 160));
            structures.Add(new Structures(1, 2, 6, 200, 200));
            structures.Add(new Structures(1, 2, 6, 200, 240));
            structures.Add(new Structures(1, 2, 6, 200, 280));
            structures.Add(new Structures(1, 2, 6, 200, 320));
            structures.Add(new Structures(1, 2, 6, 200, 360));
            structures.Add(new Structures(1, 2, 6, 200, 400));
            structures.Add(new Structures(1, 2, 6, 200, 440));
            structures.Add(new Structures(1, 2, 6, 200, 480));
            structures.Add(new Structures(1, 2, 6, 200, 520));
            structures.Add(new Structures(1, 2, 6, 200, 560));

            structures.Add(new Structures(1, 2, 5, 160, 0));
            structures.Add(new Structures(1, 2, 5, 160, 40));
            structures.Add(new Structures(1, 2, 5, 160, 80));
            structures.Add(new Structures(1, 2, 5, 160, 120));
            structures.Add(new Structures(1, 2, 5, 160, 160));
            structures.Add(new Structures(1, 2, 5, 160, 200));
            structures.Add(new Structures(1, 2, 5, 160, 240));
            structures.Add(new Structures(1, 2, 5, 160, 280));
            structures.Add(new Structures(1, 2, 5, 160, 320));
            structures.Add(new Structures(1, 2, 5, 160, 360));
            structures.Add(new Structures(1, 2, 5, 160, 400));
            structures.Add(new Structures(1, 2, 5, 160, 440));
            structures.Add(new Structures(1, 2, 5, 160, 480));
            structures.Add(new Structures(1, 2, 5, 160, 520));
            structures.Add(new Structures(1, 2, 5, 160, 560));

            structures.Add(new Structures(1, 2, 5, 120, 0));
            structures.Add(new Structures(1, 2, 5, 120, 40));
            structures.Add(new Structures(1, 2, 5, 120, 80));
            structures.Add(new Structures(1, 2, 5, 120, 120));
            structures.Add(new Structures(1, 2, 5, 120, 160));
            structures.Add(new Structures(1, 2, 5, 120, 200));
            structures.Add(new Structures(1, 2, 5, 120, 240));
            structures.Add(new Structures(1, 2, 5, 120, 280));
            structures.Add(new Structures(1, 2, 5, 120, 320));
            structures.Add(new Structures(1, 2, 5, 120, 360));
            structures.Add(new Structures(1, 2, 5, 120, 400));
            structures.Add(new Structures(1, 2, 5, 120, 440));
            structures.Add(new Structures(1, 2, 5, 120, 480));
            structures.Add(new Structures(1, 2, 5, 120, 520));
            structures.Add(new Structures(1, 2, 5, 120, 560));

            structures.Add(new Structures(1, 2, 5, 80, 0));
            structures.Add(new Structures(1, 2, 5, 80, 40));
            structures.Add(new Structures(1, 2, 5, 80, 80));
            structures.Add(new Structures(1, 2, 5, 80, 120));
            structures.Add(new Structures(1, 2, 5, 80, 160));
            structures.Add(new Structures(1, 2, 5, 80, 200));
            structures.Add(new Structures(1, 2, 5, 80, 240));
            structures.Add(new Structures(1, 2, 5, 80, 280));
            structures.Add(new Structures(1, 2, 5, 80, 320));
            structures.Add(new Structures(1, 2, 5, 80, 360));
            structures.Add(new Structures(1, 2, 5, 80, 400));
            structures.Add(new Structures(1, 2, 5, 80, 440));
            structures.Add(new Structures(1, 2, 5, 80, 480));
            structures.Add(new Structures(1, 2, 5, 80, 520));
            structures.Add(new Structures(1, 2, 5, 80, 560));

            structures.Add(new Structures(1, 2, 5, 40, 0));
            structures.Add(new Structures(1, 2, 5, 40, 40));
            structures.Add(new Structures(1, 2, 5, 40, 80));
            structures.Add(new Structures(1, 2, 5, 40, 120));
            structures.Add(new Structures(1, 2, 5, 40, 160));
            structures.Add(new Structures(1, 2, 5, 40, 200));
            structures.Add(new Structures(1, 2, 5, 40, 240));
            structures.Add(new Structures(1, 2, 5, 40, 280));
            structures.Add(new Structures(1, 2, 5, 40, 320));
            structures.Add(new Structures(1, 2, 5, 40, 360));
            structures.Add(new Structures(1, 2, 5, 40, 400));
            structures.Add(new Structures(1, 2, 5, 40, 440));
            structures.Add(new Structures(1, 2, 5, 40, 480));
            structures.Add(new Structures(1, 2, 5, 40, 520));
            structures.Add(new Structures(1, 2, 5, 40, 560));

            structures.Add(new Structures(1, 2, 5, 0, 0));
            structures.Add(new Structures(1, 2, 5, 0, 40));
            structures.Add(new Structures(1, 2, 5, 0, 80));
            structures.Add(new Structures(1, 2, 5, 0, 120));
            structures.Add(new Structures(1, 2, 5, 0, 160));
            structures.Add(new Structures(1, 2, 5, 0, 200));
            structures.Add(new Structures(1, 2, 5, 0, 240));
            structures.Add(new Structures(1, 2, 5, 0, 280));
            structures.Add(new Structures(1, 2, 5, 0, 320));
            structures.Add(new Structures(1, 2, 5, 0, 360));
            structures.Add(new Structures(1, 2, 5, 0, 400));
            structures.Add(new Structures(1, 2, 5, 0, 440));
            structures.Add(new Structures(1, 2, 5, 0, 480));
            structures.Add(new Structures(1, 2, 5, 0, 520));
            structures.Add(new Structures(1, 2, 5, 0, 560));

            wildgrass.Add(new WildGrass(394, 82));
            wildgrass.Add(new WildGrass(394, 162));
            wildgrass.Add(new WildGrass(394, 242));
            wildgrass.Add(new WildGrass(394, 322));
            wildgrass.Add(new WildGrass(394, 402));
            wildgrass.Add(new WildGrass(394, 482));
            wildgrass.Add(new WildGrass(474, 482));
            wildgrass.Add(new WildGrass(554, 482));
            wildgrass.Add(new WildGrass(634, 482));
            wildgrass.Add(new WildGrass(714, 482));
            wildgrass.Add(new WildGrass(714, 402));
            wildgrass.Add(new WildGrass(714, 322));
            wildgrass.Add(new WildGrass(714, 242));
            wildgrass.Add(new WildGrass(714, 162));
            wildgrass.Add(new WildGrass(714, 82));
            wildgrass.Add(new WildGrass(634, 82));
            wildgrass.Add(new WildGrass(554, 82));
            wildgrass.Add(new WildGrass(474, 82));
            wildgrass.Add(new WildGrass(434, 122));
            wildgrass.Add(new WildGrass(434, 202));
            wildgrass.Add(new WildGrass(434, 282));
            wildgrass.Add(new WildGrass(434, 362));
            wildgrass.Add(new WildGrass(434, 442));
            wildgrass.Add(new WildGrass(514, 442));
            wildgrass.Add(new WildGrass(594, 442));
            wildgrass.Add(new WildGrass(674, 442));
            wildgrass.Add(new WildGrass(674, 362));
            wildgrass.Add(new WildGrass(674, 282));
            wildgrass.Add(new WildGrass(674, 202));
            wildgrass.Add(new WildGrass(674, 122));
            wildgrass.Add(new WildGrass(594, 122));
            wildgrass.Add(new WildGrass(514, 122));
            wildgrass.Add(new WildGrass(474, 162));
            wildgrass.Add(new WildGrass(474, 242));
            wildgrass.Add(new WildGrass(474, 322));
            wildgrass.Add(new WildGrass(474, 402));
            wildgrass.Add(new WildGrass(554, 402));
            wildgrass.Add(new WildGrass(634, 402));
            wildgrass.Add(new WildGrass(634, 322));
            wildgrass.Add(new WildGrass(634, 242));
            wildgrass.Add(new WildGrass(634, 162));
            wildgrass.Add(new WildGrass(554, 162));
            wildgrass.Add(new WildGrass(514, 202));
            wildgrass.Add(new WildGrass(514, 282));
            wildgrass.Add(new WildGrass(514, 362));
            wildgrass.Add(new WildGrass(594, 362));
            wildgrass.Add(new WildGrass(594, 282));
            wildgrass.Add(new WildGrass(594, 202));
            wildgrass.Add(new WildGrass(554, 242));
            wildgrass.Add(new WildGrass(554, 322));


            #endregion
        }

        private void LoadMap10()
        {
            pbCanvas.BackgroundImage = Resources.grass;

            exit.Add(new ExitPoint(1, 61, 34, 151, 8, 753, 105));
            exit.Add(new ExitPoint(287, 562, 395, 596, 11, 300, 38));

            characters.Add(new Character("Old man", Resources.character_oldman2, 500, 110));
            actionpoint.Add(new ActionPoint(480, 90, 550, 160, -12));

            #region Environment

            trees.Add(new Trees(1, 199, 248));
            trees.Add(new Trees(1, 433, 300));
            trees.Add(new Trees(1, 614, 362));
            trees.Add(new Trees(1, 55, 379));
            trees.Add(new Trees(1, 183, 471));
            trees.Add(new Trees(1, 467, 495));

            buildings.Add(new Buildings(5, 573, 16, true));

            structures.Add(new Structures(1, 2, 7, 550, 210));
            structures.Add(new Structures(1, 2, 8, 590, 210));
            structures.Add(new Structures(1, 2, 8, 630, 210));
            structures.Add(new Structures(1, 2, 8, 670, 210));
            structures.Add(new Structures(1, 2, 9, 710, 210));
            structures.Add(new Structures(1, 2, 1, 550, 250));
            structures.Add(new Structures(1, 2, 2, 590, 250));
            structures.Add(new Structures(1, 2, 2, 630, 250));
            structures.Add(new Structures(1, 2, 2, 670, 250));
            structures.Add(new Structures(1, 2, 3, 710, 250));

            flowers.Add(new Flowers(169, 407));
            flowers.Add(new Flowers(80, 498));
            flowers.Add(new Flowers(83, 276));
            flowers.Add(new Flowers(436, 440));
            flowers.Add(new Flowers(604, 499));
            flowers.Add(new Flowers(721, 450));
            flowers.Add(new Flowers(584, 301));

            paths.Add(new Paths(8, -13, 72));
            paths.Add(new Paths(8, 27, 72));
            paths.Add(new Paths(8, 67, 72));
            paths.Add(new Paths(8, 107, 72));
            paths.Add(new Paths(8, 147, 72));
            paths.Add(new Paths(8, 187, 72));
            paths.Add(new Paths(8, 227, 72));
            paths.Add(new Paths(8, 267, 72));
            paths.Add(new Paths(8, 307, 72));
            paths.Add(new Paths(9, 347, 72));
            paths.Add(new Paths(6, 347, 112));
            paths.Add(new Paths(6, 347, 152));
            paths.Add(new Paths(6, 347, 192));
            paths.Add(new Paths(6, 347, 232));
            paths.Add(new Paths(6, 347, 272));
            paths.Add(new Paths(6, 347, 312));
            paths.Add(new Paths(6, 347, 352));
            paths.Add(new Paths(6, 347, 392));
            paths.Add(new Paths(6, 347, 432));
            paths.Add(new Paths(6, 347, 472));
            paths.Add(new Paths(6, 347, 512));
            paths.Add(new Paths(6, 347, 552));
            paths.Add(new Paths(6, 347, 592));
            paths.Add(new Paths(4, 307, 592));
            paths.Add(new Paths(4, 307, 552));
            paths.Add(new Paths(4, 307, 512));
            paths.Add(new Paths(4, 307, 472));
            paths.Add(new Paths(4, 307, 432));
            paths.Add(new Paths(4, 307, 392));
            paths.Add(new Paths(4, 307, 352));
            paths.Add(new Paths(4, 307, 312));
            paths.Add(new Paths(4, 307, 272));
            paths.Add(new Paths(4, 307, 232));
            paths.Add(new Paths(4, 307, 192));
            paths.Add(new Paths(4, 307, 152));
            paths.Add(new Paths(4, 307, 112));
            paths.Add(new Paths(2, 276, 105));
            paths.Add(new Paths(2, 236, 105));
            paths.Add(new Paths(2, 196, 105));
            paths.Add(new Paths(2, 156, 105));
            paths.Add(new Paths(2, 116, 105));
            paths.Add(new Paths(2, 76, 105));
            paths.Add(new Paths(2, 36, 105));
            paths.Add(new Paths(2, -4, 105));

            #endregion
        }

        private void LoadMap11()
        {
            pbCanvas.BackgroundImage = Resources.grass;

            exit.Add(new ExitPoint(267, 2, 379, 60, 10, 322, 520));
            exit.Add(new ExitPoint(265, 563, 379, 594, 12, 300, 47));

            wildPokemons.Add(new ints(69, 6, 12, 20));
            wildPokemons.Add(new ints(193, 6, 12, 4));
            wildPokemons.Add(new ints(165, 6, 12, 16));
            wildPokemons.Add(new ints(167, 6, 12, 16));
            wildPokemons.Add(new ints(287, 6, 12, 16));
            wildPokemons.Add(new ints(300, 6, 12, 16));
            wildPokemons.Add(new ints(204, 6, 12, 12));

            #region Environment

            trees.Add(new Trees(1, 137, 0));
            trees.Add(new Trees(1, 408, 4));
            trees.Add(new Trees(1, 41, 27));
            trees.Add(new Trees(1, 698, 11));
            trees.Add(new Trees(1, 593, 34));
            trees.Add(new Trees(1, 19, 101));
            trees.Add(new Trees(1, 439, 130));
            trees.Add(new Trees(1, 137, 132));
            trees.Add(new Trees(1, 709, 140));
            trees.Add(new Trees(1, 554, 154));
            trees.Add(new Trees(1, 57, 180));
            trees.Add(new Trees(1, 174, 244));
            trees.Add(new Trees(1, 728, 313));
            trees.Add(new Trees(1, 105, 328));
            trees.Add(new Trees(1, 597, 379));
            trees.Add(new Trees(1, 11, 408));
            trees.Add(new Trees(1, 190, 422));
            trees.Add(new Trees(1, 479, 435));
            trees.Add(new Trees(1, 690, 461));
            trees.Add(new Trees(1, 94, 492));
            trees.Add(new Trees(1, 380, 501));
            trees.Add(new Trees(1, 590, 521));

            wildgrass.Add(new WildGrass(400, 282));
            wildgrass.Add(new WildGrass(440, 282));
            wildgrass.Add(new WildGrass(480, 282));
            wildgrass.Add(new WildGrass(520, 282));
            wildgrass.Add(new WildGrass(560, 282));
            wildgrass.Add(new WildGrass(560, 322));
            wildgrass.Add(new WildGrass(560, 362));
            wildgrass.Add(new WildGrass(520, 362));
            wildgrass.Add(new WildGrass(480, 362));
            wildgrass.Add(new WildGrass(440, 362));
            wildgrass.Add(new WildGrass(400, 362));
            wildgrass.Add(new WildGrass(520, 322));
            wildgrass.Add(new WildGrass(480, 322));
            wildgrass.Add(new WildGrass(440, 322));
            wildgrass.Add(new WildGrass(400, 322));
            wildgrass.Add(new WildGrass(499, 70));
            wildgrass.Add(new WildGrass(499, 110));
            wildgrass.Add(new WildGrass(539, 110));
            wildgrass.Add(new WildGrass(539, 70));
            wildgrass.Add(new WildGrass(643, 137));
            wildgrass.Add(new WildGrass(643, 177));
            wildgrass.Add(new WildGrass(662, 336));
            wildgrass.Add(new WildGrass(662, 376));
            wildgrass.Add(new WildGrass(222, 78));
            wildgrass.Add(new WildGrass(222, 118));
            wildgrass.Add(new WildGrass(222, 158));
            wildgrass.Add(new WildGrass(24, 297));
            wildgrass.Add(new WildGrass(64, 297));
            wildgrass.Add(new WildGrass(64, 337));
            wildgrass.Add(new WildGrass(24, 337));
            wildgrass.Add(new WildGrass(579, 483));
            wildgrass.Add(new WildGrass(619, 483));

            paths.Add(new Paths(4, 285, -11));
            paths.Add(new Paths(4, 285, 29));
            paths.Add(new Paths(4, 285, 69));
            paths.Add(new Paths(4, 285, 109));
            paths.Add(new Paths(4, 285, 149));
            paths.Add(new Paths(4, 285, 189));
            paths.Add(new Paths(4, 285, 229));
            paths.Add(new Paths(4, 285, 269));
            paths.Add(new Paths(4, 285, 309));
            paths.Add(new Paths(4, 285, 349));
            paths.Add(new Paths(4, 285, 389));
            paths.Add(new Paths(4, 285, 429));
            paths.Add(new Paths(4, 285, 469));
            paths.Add(new Paths(4, 285, 509));
            paths.Add(new Paths(4, 285, 549));
            paths.Add(new Paths(4, 285, 589));
            paths.Add(new Paths(6, 325, 589));
            paths.Add(new Paths(6, 325, 549));
            paths.Add(new Paths(6, 325, 509));
            paths.Add(new Paths(6, 325, 469));
            paths.Add(new Paths(6, 325, 429));
            paths.Add(new Paths(6, 325, 389));
            paths.Add(new Paths(6, 325, 349));
            paths.Add(new Paths(6, 325, 309));
            paths.Add(new Paths(6, 325, 269));
            paths.Add(new Paths(6, 325, 229));
            paths.Add(new Paths(6, 325, 189));
            paths.Add(new Paths(6, 325, 149));
            paths.Add(new Paths(6, 325, 109));
            paths.Add(new Paths(6, 325, 69));
            paths.Add(new Paths(6, 325, 29));
            paths.Add(new Paths(6, 325, -11));

            #endregion
        }

        private void LoadMap12()
        {
            pbCanvas.BackgroundImage = Resources.grass;

            exit.Add(new ExitPoint(267, 2, 379, 60, 11, 300, 520));
            exit.Add(new ExitPoint(265, 563, 379, 594, 13, 300, 47));

            wildPokemons.Add(new ints(172, 6, 12, 13));
            wildPokemons.Add(new ints(417, 6, 12, 13));
            wildPokemons.Add(new ints(403, 6, 12, 15));
            wildPokemons.Add(new ints(48, 6, 12, 12));
            wildPokemons.Add(new ints(177, 6, 12, 12));
            wildPokemons.Add(new ints(427, 6, 12, 12));
            wildPokemons.Add(new ints(190, 6, 12, 3));
            wildPokemons.Add(new ints(313, 6, 12, 10));
            wildPokemons.Add(new ints(314, 6, 12, 10));

            #region Environment

            trees.Add(new Trees(1, 404, 12));
            trees.Add(new Trees(1, 171, 14));
            trees.Add(new Trees(1, 589, 32));
            trees.Add(new Trees(1, 62, 37));
            trees.Add(new Trees(1, 726, 67));
            trees.Add(new Trees(1, 150, 124));
            trees.Add(new Trees(1, 453, 144));
            trees.Add(new Trees(1, 40, 155));
            trees.Add(new Trees(1, 604, 162));
            trees.Add(new Trees(1, 173, 241));
            trees.Add(new Trees(1, 37, 264));
            trees.Add(new Trees(1, 682, 277));
            trees.Add(new Trees(1, 412, 280));
            trees.Add(new Trees(1, 15, 365));
            trees.Add(new Trees(1, 175, 365));
            trees.Add(new Trees(1, 95, 365));
            trees.Add(new Trees(1, -65, 365));
            trees.Add(new Trees(1, 383, 403));
            trees.Add(new Trees(1, 463, 403));
            trees.Add(new Trees(1, 543, 403));
            trees.Add(new Trees(1, 623, 403));
            trees.Add(new Trees(1, 703, 403));
            trees.Add(new Trees(1, 783, 403));
            trees.Add(new Trees(1, 15, 425));
            trees.Add(new Trees(1, 175, 425));
            trees.Add(new Trees(1, 95, 425));
            trees.Add(new Trees(1, -65, 425));
            trees.Add(new Trees(1, 783, 463));
            trees.Add(new Trees(1, 383, 463));
            trees.Add(new Trees(1, 463, 463));
            trees.Add(new Trees(1, 543, 463));
            trees.Add(new Trees(1, 623, 463));
            trees.Add(new Trees(1, 703, 463));
            trees.Add(new Trees(1, 15, 485));
            trees.Add(new Trees(1, 175, 485));
            trees.Add(new Trees(1, 95, 485));
            trees.Add(new Trees(1, -65, 485));
            trees.Add(new Trees(1, 783, 523));
            trees.Add(new Trees(1, 703, 523));
            trees.Add(new Trees(1, 623, 523));
            trees.Add(new Trees(1, 543, 523));
            trees.Add(new Trees(1, 463, 523));
            trees.Add(new Trees(1, 383, 523));
            trees.Add(new Trees(1, 15, 545));
            trees.Add(new Trees(1, 95, 545));
            trees.Add(new Trees(1, 175, 545));
            trees.Add(new Trees(1, -65, 545));

            wildgrass.Add(new WildGrass(526, 264));
            wildgrass.Add(new WildGrass(486, 264));
            wildgrass.Add(new WildGrass(446, 264));
            wildgrass.Add(new WildGrass(406, 264));
            wildgrass.Add(new WildGrass(566, 264));
            wildgrass.Add(new WildGrass(566, 224));
            wildgrass.Add(new WildGrass(526, 224));
            wildgrass.Add(new WildGrass(750, 175));
            wildgrass.Add(new WildGrass(750, 215));
            wildgrass.Add(new WildGrass(710, 215));
            wildgrass.Add(new WildGrass(710, 175));
            wildgrass.Add(new WildGrass(630, 276));
            wildgrass.Add(new WildGrass(630, 316));
            wildgrass.Add(new WildGrass(716, 14));
            wildgrass.Add(new WildGrass(676, 14));
            wildgrass.Add(new WildGrass(500, 45));
            wildgrass.Add(new WildGrass(540, 45));
            wildgrass.Add(new WildGrass(540, 85));
            wildgrass.Add(new WildGrass(500, 85));

            paths.Add(new Paths(4, 285, -11));
            paths.Add(new Paths(4, 285, 29));
            paths.Add(new Paths(4, 285, 69));
            paths.Add(new Paths(4, 285, 109));
            paths.Add(new Paths(4, 285, 149));
            paths.Add(new Paths(4, 285, 189));
            paths.Add(new Paths(4, 285, 229));
            paths.Add(new Paths(4, 285, 269));
            paths.Add(new Paths(4, 285, 309));
            paths.Add(new Paths(4, 285, 349));
            paths.Add(new Paths(4, 285, 389));
            paths.Add(new Paths(4, 285, 429));
            paths.Add(new Paths(4, 285, 469));
            paths.Add(new Paths(4, 285, 509));
            paths.Add(new Paths(4, 285, 549));
            paths.Add(new Paths(4, 285, 589));
            paths.Add(new Paths(6, 325, 589));
            paths.Add(new Paths(6, 325, 549));
            paths.Add(new Paths(6, 325, 509));
            paths.Add(new Paths(6, 325, 469));
            paths.Add(new Paths(6, 325, 429));
            paths.Add(new Paths(6, 325, 389));
            paths.Add(new Paths(6, 325, 349));
            paths.Add(new Paths(6, 325, 309));
            paths.Add(new Paths(6, 325, 269));
            paths.Add(new Paths(6, 325, 229));
            paths.Add(new Paths(6, 325, 189));
            paths.Add(new Paths(6, 325, 149));
            paths.Add(new Paths(6, 325, 109));
            paths.Add(new Paths(6, 325, 69));
            paths.Add(new Paths(6, 325, 29));
            paths.Add(new Paths(6, 325, -11));

            #endregion
        }

        private void LoadMap13()
        {
            pbCanvas.BackgroundImage = Resources.mountain;

            exit.Add(new ExitPoint(267, 2, 379, 60, 12, 300, 520));

            wildPokemons.Add(new ints(74, 7, 12, 30));
            wildPokemons.Add(new ints(213, 7, 12, 10));
            wildPokemons.Add(new ints(56, 7, 12, 8));
            wildPokemons.Add(new ints(66, 7, 12, 8));
            wildPokemons.Add(new ints(296, 7, 12, 8));
            wildPokemons.Add(new ints(307, 7, 12, 8));
            wildPokemons.Add(new ints(447, 7, 12, 2));
            wildPokemons.Add(new ints(173, 7, 12, 6));
            wildPokemons.Add(new ints(327, 7, 12, 20));

            characters.Add(new Character("Fighter", Resources.character_fighter2, 154, 269));
            characters[0].index = 21;
            actionpoint.Add(new ActionPoint(144, 259, 214, 319, 0));
            characters[0].pokemons.Add(new Pokemons(66, 16));
            characters[0].pokemons.Add(new Pokemons(296, 16));
            characters[0].pokemons.Add(new Pokemons(307, 16));

            characters.Add(new Character("Explorer", Resources.character_explorer, 392, 395));
            characters[1].index = 22;
            actionpoint.Add(new ActionPoint(382, 385, 452, 445, 1));
            characters[1].pokemons.Add(new Pokemons(74, 14));
            characters[1].pokemons.Add(new Pokemons(327, 12));
            characters[1].pokemons.Add(new Pokemons(74, 15));
            characters[1].pokemons.Add(new Pokemons(213, 14));
            characters[1].pokemons.Add(new Pokemons(74, 16));

            characters.Add(new Character("Fighter", Resources.character_fighter2, 579, 238));
            characters[2].index = 23;
            actionpoint.Add(new ActionPoint(569, 228, 639, 288, 2));
            characters[2].pokemons.Add(new Pokemons(448, 24));

            characters.Add(new Character("Explorer", Resources.character_explorer, 705, 155));
            characters[3].index = 24;
            actionpoint.Add(new ActionPoint(695, 145, 765, 205, 3));
            characters[3].pokemons.Add(new Pokemons(41, 13));
            characters[3].pokemons.Add(new Pokemons(41, 12));
            characters[3].pokemons.Add(new Pokemons(41, 13));
            characters[3].pokemons.Add(new Pokemons(41, 14));
            characters[3].pokemons.Add(new Pokemons(41, 13));
            characters[3].pokemons.Add(new Pokemons(41, 15));


            #region Environment

            structures.Add(new Structures(3, 0, 230, 2));
            structures.Add(new Structures(3, 0, 379, 2));

            structures.Add(new Structures(3, 0, 50, 50));
            structures.Add(new Structures(3, 0, 250, 459));
            structures.Add(new Structures(3, 0, 540, 298));
            structures.Add(new Structures(3, 0, 420, 89));
            structures.Add(new Structures(3, 0, 340, 191));
            structures.Add(new Structures(3, 0, 270, 298));
            structures.Add(new Structures(3, 0, 500, 394));
            structures.Add(new Structures(3, 0, 120, 84));
            structures.Add(new Structures(3, 0, 65, 410));

            buildings.Add(new Buildings(-1, 565, 55, true));

            wildgrass.Add(new WildGrass(72, 55));
            wildgrass.Add(new WildGrass(152, 135));
            wildgrass.Add(new WildGrass(192, 175));
            wildgrass.Add(new WildGrass(232, 215));
            wildgrass.Add(new WildGrass(272, 255));
            wildgrass.Add(new WildGrass(312, 295));
            wildgrass.Add(new WildGrass(352, 335));
            wildgrass.Add(new WildGrass(392, 375));
            wildgrass.Add(new WildGrass(472, 455));
            wildgrass.Add(new WildGrass(512, 495));
            wildgrass.Add(new WildGrass(152, 55));
            wildgrass.Add(new WildGrass(232, 55));
            wildgrass.Add(new WildGrass(312, 55));
            wildgrass.Add(new WildGrass(392, 55));
            wildgrass.Add(new WildGrass(472, 55));
            wildgrass.Add(new WildGrass(472, 135));
            wildgrass.Add(new WildGrass(472, 215));
            wildgrass.Add(new WildGrass(472, 295));
            wildgrass.Add(new WildGrass(472, 375));
            wildgrass.Add(new WildGrass(512, 415));
            wildgrass.Add(new WildGrass(592, 415));
            wildgrass.Add(new WildGrass(672, 415));
            wildgrass.Add(new WildGrass(712, 375));
            wildgrass.Add(new WildGrass(712, 295));
            wildgrass.Add(new WildGrass(712, 215));
            wildgrass.Add(new WildGrass(512, 255));
            wildgrass.Add(new WildGrass(512, 335));
            wildgrass.Add(new WildGrass(632, 375));
            wildgrass.Add(new WildGrass(672, 335));
            wildgrass.Add(new WildGrass(672, 255));
            wildgrass.Add(new WildGrass(592, 255));
            wildgrass.Add(new WildGrass(552, 375));
            wildgrass.Add(new WildGrass(592, 335));
            wildgrass.Add(new WildGrass(632, 295));
            wildgrass.Add(new WildGrass(432, 95));
            wildgrass.Add(new WildGrass(352, 95));
            wildgrass.Add(new WildGrass(272, 95));
            wildgrass.Add(new WildGrass(192, 95));
            wildgrass.Add(new WildGrass(232, 135));
            wildgrass.Add(new WildGrass(312, 135));
            wildgrass.Add(new WildGrass(392, 135));
            wildgrass.Add(new WildGrass(352, 175));
            wildgrass.Add(new WildGrass(272, 175));
            wildgrass.Add(new WildGrass(312, 215));
            wildgrass.Add(new WildGrass(392, 215));
            wildgrass.Add(new WildGrass(352, 255));
            wildgrass.Add(new WildGrass(392, 295));
            wildgrass.Add(new WildGrass(112, 95));
            wildgrass.Add(new WildGrass(72, 135));
            wildgrass.Add(new WildGrass(72, 215));
            wildgrass.Add(new WildGrass(72, 295));
            wildgrass.Add(new WildGrass(72, 375));
            wildgrass.Add(new WildGrass(72, 455));
            wildgrass.Add(new WildGrass(112, 495));
            wildgrass.Add(new WildGrass(192, 495));
            wildgrass.Add(new WildGrass(272, 495));
            wildgrass.Add(new WildGrass(352, 495));
            wildgrass.Add(new WildGrass(112, 255));
            wildgrass.Add(new WildGrass(112, 335));
            wildgrass.Add(new WildGrass(112, 415));
            wildgrass.Add(new WildGrass(152, 215));
            wildgrass.Add(new WildGrass(152, 295));
            wildgrass.Add(new WildGrass(152, 375));
            wildgrass.Add(new WildGrass(152, 455));
            wildgrass.Add(new WildGrass(192, 255));
            wildgrass.Add(new WildGrass(192, 335));
            wildgrass.Add(new WildGrass(192, 415));
            wildgrass.Add(new WildGrass(232, 295));
            wildgrass.Add(new WildGrass(232, 375));
            wildgrass.Add(new WildGrass(232, 455));
            wildgrass.Add(new WildGrass(272, 335));
            wildgrass.Add(new WildGrass(272, 415));
            wildgrass.Add(new WildGrass(312, 375));
            wildgrass.Add(new WildGrass(312, 455));
            wildgrass.Add(new WildGrass(352, 415));
            wildgrass.Add(new WildGrass(112, 175));
            wildgrass.Add(new WildGrass(392, 455));
            wildgrass.Add(new WildGrass(432, 175));
            wildgrass.Add(new WildGrass(432, 255));
            wildgrass.Add(new WildGrass(432, 335));
            wildgrass.Add(new WildGrass(432, 415));
            wildgrass.Add(new WildGrass(432, 495));
            wildgrass.Add(new WildGrass(552, 215));
            wildgrass.Add(new WildGrass(552, 295));
            wildgrass.Add(new WildGrass(632, 215));
            wildgrass.Add(new WildGrass(552, 455));
            wildgrass.Add(new WildGrass(632, 455));
            wildgrass.Add(new WildGrass(592, 495));
            wildgrass.Add(new WildGrass(672, 495));
            wildgrass.Add(new WildGrass(712, 455));

            #endregion
        }

        private void LoadMap14()
        {
            pbCanvas.BackgroundImage = Resources.grass;

            wildPokemons.Add(new ints(298, 6, 12, 60));
            wildPokemons.Add(new ints(399, 6, 12, 40));

            #region Environment

            trees.Add(new Trees(1, 695, -40));
            trees.Add(new Trees(1, 395, 42));
            trees.Add(new Trees(1, 402, 219));
            trees.Add(new Trees(1, 716, 224));
            trees.Add(new Trees(1, 45, 270));
            trees.Add(new Trees(1, 260, 332));

            buildings.Add(new Buildings(-1, 87, 56, true));
            buildings.Add(new Buildings(0, 563, 72, true));
            buildings.Add(new Buildings(1, 559, 276, true));

            wildgrass.Add(new WildGrass(350, 410));
            wildgrass.Add(new WildGrass(390, 410));
            wildgrass.Add(new WildGrass(430, 410));
            wildgrass.Add(new WildGrass(470, 410));

            flowers.Add(new Flowers(162, 356));
            flowers.Add(new Flowers(428, 356));
            flowers.Add(new Flowers(245, 264));
            flowers.Add(new Flowers(461, 160));
            flowers.Add(new Flowers(549, 229));
            flowers.Add(new Flowers(651, 225));
            flowers.Add(new Flowers(342, 138));

            paths.Add(new Paths(7, 40, 72));
            paths.Add(new Paths(8, 80, 72));
            paths.Add(new Paths(8, 120, 72));
            paths.Add(new Paths(8, 160, 72));
            paths.Add(new Paths(8, 200, 72));
            paths.Add(new Paths(8, 240, 72));
            paths.Add(new Paths(9, 280, 72));
            paths.Add(new Paths(6, 280, 112));
            paths.Add(new Paths(5, 240, 112));
            paths.Add(new Paths(5, 200, 112));
            paths.Add(new Paths(5, 160, 112));
            paths.Add(new Paths(5, 120, 112));
            paths.Add(new Paths(5, 80, 112));
            paths.Add(new Paths(4, 40, 112));
            paths.Add(new Paths(4, 40, 152));
            paths.Add(new Paths(5, 80, 152));
            paths.Add(new Paths(5, 120, 152));
            paths.Add(new Paths(5, 160, 152));
            paths.Add(new Paths(5, 200, 152));
            paths.Add(new Paths(5, 240, 152));
            paths.Add(new Paths(6, 280, 152));
            paths.Add(new Paths(3, 280, 192));
            paths.Add(new Paths(2, 240, 192));
            paths.Add(new Paths(2, 200, 192));
            paths.Add(new Paths(2, 160, 192));
            paths.Add(new Paths(2, 120, 192));
            paths.Add(new Paths(2, 80, 192));
            paths.Add(new Paths(1, 40, 192));

            structures.Add(new Structures(1, 1, 8, 0, 460));
            structures.Add(new Structures(1, 1, 8, 40, 460));
            structures.Add(new Structures(1, 1, 8, 80, 460));
            structures.Add(new Structures(1, 1, 8, 120, 460));
            structures.Add(new Structures(1, 1, 8, 160, 460));
            structures.Add(new Structures(1, 1, 8, 200, 460));
            structures.Add(new Structures(1, 1, 8, 240, 460));
            structures.Add(new Structures(1, 1, 8, 280, 460));
            structures.Add(new Structures(1, 1, 8, 320, 460));
            structures.Add(new Structures(1, 1, 8, 360, 460));
            structures.Add(new Structures(1, 1, 8, 400, 460));
            structures.Add(new Structures(1, 1, 8, 440, 460));
            structures.Add(new Structures(1, 1, 8, 480, 460));
            structures.Add(new Structures(1, 1, 8, 520, 460));
            structures.Add(new Structures(1, 1, 8, 560, 460));
            structures.Add(new Structures(1, 1, 8, 600, 460));
            structures.Add(new Structures(1, 1, 8, 640, 460));
            structures.Add(new Structures(1, 1, 8, 680, 460));
            structures.Add(new Structures(1, 1, 8, 720, 460));
            structures.Add(new Structures(1, 1, 8, 760, 460));

            structures.Add(new Structures(1, 1, 5, 0, 500));
            structures.Add(new Structures(1, 1, 5, 40, 500));
            structures.Add(new Structures(1, 1, 5, 80, 500));
            structures.Add(new Structures(1, 1, 5, 120, 500));
            structures.Add(new Structures(1, 1, 5, 160, 500));
            structures.Add(new Structures(1, 1, 5, 200, 500));
            structures.Add(new Structures(1, 1, 5, 240, 500));
            structures.Add(new Structures(1, 1, 5, 280, 500));
            structures.Add(new Structures(1, 1, 5, 320, 500));
            structures.Add(new Structures(1, 1, 5, 360, 500));
            structures.Add(new Structures(1, 1, 5, 400, 500));
            structures.Add(new Structures(1, 1, 5, 440, 500));
            structures.Add(new Structures(1, 1, 5, 480, 500));
            structures.Add(new Structures(1, 1, 5, 520, 500));
            structures.Add(new Structures(1, 1, 5, 560, 500));
            structures.Add(new Structures(1, 1, 5, 600, 500));
            structures.Add(new Structures(1, 1, 5, 640, 500));
            structures.Add(new Structures(1, 1, 5, 680, 500));
            structures.Add(new Structures(1, 1, 5, 720, 500));
            structures.Add(new Structures(1, 1, 5, 760, 500));

            structures.Add(new Structures(1, 1, 5, 0, 540));
            structures.Add(new Structures(1, 1, 5, 40, 540));
            structures.Add(new Structures(1, 1, 5, 80, 540));
            structures.Add(new Structures(1, 1, 5, 120, 540));
            structures.Add(new Structures(1, 1, 5, 160, 540));
            structures.Add(new Structures(1, 1, 5, 200, 540));
            structures.Add(new Structures(1, 1, 5, 240, 540));
            structures.Add(new Structures(1, 1, 5, 280, 540));
            structures.Add(new Structures(1, 1, 5, 320, 540));
            structures.Add(new Structures(1, 1, 5, 360, 540));
            structures.Add(new Structures(1, 1, 5, 400, 540));
            structures.Add(new Structures(1, 1, 5, 440, 540));
            structures.Add(new Structures(1, 1, 5, 480, 540));
            structures.Add(new Structures(1, 1, 5, 520, 540));
            structures.Add(new Structures(1, 1, 5, 560, 540));
            structures.Add(new Structures(1, 1, 5, 600, 540));
            structures.Add(new Structures(1, 1, 5, 640, 540));
            structures.Add(new Structures(1, 1, 5, 680, 540));
            structures.Add(new Structures(1, 1, 5, 720, 540));
            structures.Add(new Structures(1, 1, 5, 760, 540));

            structures.Add(new Structures(1, 1, 5, 0, 580));
            structures.Add(new Structures(1, 1, 5, 40, 580));
            structures.Add(new Structures(1, 1, 5, 80, 580));
            structures.Add(new Structures(1, 1, 5, 120, 580));
            structures.Add(new Structures(1, 1, 5, 160, 580));
            structures.Add(new Structures(1, 1, 5, 200, 580));
            structures.Add(new Structures(1, 1, 5, 240, 580));
            structures.Add(new Structures(1, 1, 5, 280, 580));
            structures.Add(new Structures(1, 1, 5, 320, 580));
            structures.Add(new Structures(1, 1, 5, 360, 580));
            structures.Add(new Structures(1, 1, 5, 400, 580));
            structures.Add(new Structures(1, 1, 5, 440, 580));
            structures.Add(new Structures(1, 1, 5, 480, 580));
            structures.Add(new Structures(1, 1, 5, 520, 580));
            structures.Add(new Structures(1, 1, 5, 560, 580));
            structures.Add(new Structures(1, 1, 5, 600, 580));
            structures.Add(new Structures(1, 1, 5, 640, 580));
            structures.Add(new Structures(1, 1, 5, 680, 580));
            structures.Add(new Structures(1, 1, 5, 720, 580));
            structures.Add(new Structures(1, 1, 5, 760, 580));

            #endregion
        }

        private void LoadMap15()
        {
            pbCanvas.BackgroundImage = Resources.waterwaves;

            exit.Add(new ExitPoint(3, 2, 193, 50, 14, 120, 375));
            exit.Add(new ExitPoint(733, 373, 795, 500, 16, 46, 412));

            wildPokemons.Add(new ints(129, 10, 16, 50));
            wildPokemons.Add(new ints(118, 10, 16, 15));
            wildPokemons.Add(new ints(339, 10, 16, 5));
            wildPokemons.Add(new ints(349, 10, 16, 20));
            wildPokemons.Add(new ints(456, 10, 16, 10));

            characters.Add(new Character("Swimmer", Resources.character_swimmer3, 81, 315));
            characters[0].index = 26;
            actionpoint.Add(new ActionPoint(71, 305, 141, 365, 0));
            characters[0].pokemons.Add(new Pokemons(129, 15));
            characters[0].pokemons.Add(new Pokemons(129, 15));
            characters[0].pokemons.Add(new Pokemons(129, 15));
            characters[0].pokemons.Add(new Pokemons(129, 15));

            characters.Add(new Character("Swimmer", Resources.character_swimmer4, 135, 444));
            characters[1].index = 27;
            actionpoint.Add(new ActionPoint(125, 434, 195, 494, 1));
            characters[1].pokemons.Add(new Pokemons(129, 15));
            characters[1].pokemons.Add(new Pokemons(349, 15));
            characters[1].pokemons.Add(new Pokemons(118, 15));

            characters.Add(new Character("Swimmer", Resources.character_swimmer, 310, 508));
            characters[2].index = 28;
            actionpoint.Add(new ActionPoint(300, 498, 370, 558, 2));
            characters[2].pokemons.Add(new Pokemons(339, 25));
            characters[2].pokemons.Add(new Pokemons(340, 30));

            characters.Add(new Character("Swimmer", Resources.character_swimmer2, 440, 72));
            characters[3].index = 29;
            actionpoint.Add(new ActionPoint(430, 62, 500, 122, 3));
            characters[3].pokemons.Add(new Pokemons(349, 18));
            characters[3].pokemons.Add(new Pokemons(349, 20));
            characters[3].pokemons.Add(new Pokemons(456, 23));

            characters.Add(new Character("Swimmer", Resources.character_swimmer, 604, 34));
            characters[4].index = 30;
            actionpoint.Add(new ActionPoint(594, 24, 664, 84, 4));
            characters[4].pokemons.Add(new Pokemons(298, 10));
            characters[4].pokemons.Add(new Pokemons(183, 15));
            characters[4].pokemons.Add(new Pokemons(184, 20));

            characters.Add(new Character("Swimmer", Resources.character_swimmer, 616, 177));
            characters[5].index = 31;
            actionpoint.Add(new ActionPoint(606, 167, 676, 227, 5));
            characters[5].pokemons.Add(new Pokemons(350, 20));
            characters[5].pokemons.Add(new Pokemons(130, 20));

            characters.Add(new Character("Swimmer", Resources.character_swimmer2, 473, 521));
            characters[6].index = 32;
            actionpoint.Add(new ActionPoint(463, 511, 533, 571, 6));
            characters[6].pokemons.Add(new Pokemons(399, 17));
            characters[6].pokemons.Add(new Pokemons(400, 22));

            #region Environment

            structures.Add(new Structures(3, 0, 53, 78));
            structures.Add(new Structures(3, 0, 180, 34));
            structures.Add(new Structures(3, 0, 100, 181));
            structures.Add(new Structures(3, 0, 276, 118));
            structures.Add(new Structures(3, 0, 179, 256));
            structures.Add(new Structures(3, 0, 351, 178));
            structures.Add(new Structures(3, 0, 276, 327));
            structures.Add(new Structures(3, 0, 410, 222));
            structures.Add(new Structures(3, 0, 395, 396));
            structures.Add(new Structures(3, 0, 513, 284));
            structures.Add(new Structures(3, 0, 532, 455));
            structures.Add(new Structures(3, 0, 636, 310));
            structures.Add(new Structures(3, 0, 712, 492));
            structures.Add(new Structures(3, 0, 734, 331));

            LoadWater();

            #endregion
        }

        private void LoadMap16()
        {
            pbCanvas.BackgroundImage = Resources.waterwaves;

            exit.Add(new ExitPoint(5, 357, 57, 475, 15, 713, 385));
            exit.Add(new ExitPoint(730, 316, 789, 451, 17, 215, 215));

            wildPokemons.Add(new ints(72, 10, 16, 40));
            wildPokemons.Add(new ints(278, 10, 16, 20));
            wildPokemons.Add(new ints(170, 10, 16, 10));
            wildPokemons.Add(new ints(223, 10, 16, 10));
            wildPokemons.Add(new ints(370, 10, 16, 10));
            wildPokemons.Add(new ints(211, 10, 16, 10));

            characters.Add(new Character("Swimmer", Resources.character_swimmer, 65, 66));
            characters[0].pokemons.Add(new Pokemons(72, 18));
            characters[0].pokemons.Add(new Pokemons(72, 22));
            characters[0].pokemons.Add(new Pokemons(72, 25));
            characters[0].index = 33;
            actionpoint.Add(new ActionPoint(55, 56, 125, 116, 0));

            characters.Add(new Character("Swimmer", Resources.character_swimmer2, 262, 114));
            characters[1].pokemons.Add(new Pokemons(278, 20));
            characters[1].pokemons.Add(new Pokemons(279, 25));
            characters[1].index = 34;
            actionpoint.Add(new ActionPoint(252, 104, 322, 164, 1));

            characters.Add(new Character("Swimmer", Resources.character_swimmer, 458, 141));
            characters[2].pokemons.Add(new Pokemons(458, 21));
            characters[2].pokemons.Add(new Pokemons(223, 21));
            characters[2].pokemons.Add(new Pokemons(226, 21));
            characters[2].index = 35;
            actionpoint.Add(new ActionPoint(448, 131, 518, 191, 2));

            characters.Add(new Character("Swimmer", Resources.character_swimmer4, 689, 41));
            characters[3].pokemons.Add(new Pokemons(170, 18));
            characters[3].pokemons.Add(new Pokemons(370, 20));
            characters[3].pokemons.Add(new Pokemons(170, 20));
            characters[3].pokemons.Add(new Pokemons(370, 22));
            characters[3].index = 36;
            actionpoint.Add(new ActionPoint(679, 31, 749, 91, 3));

            characters.Add(new Character("Swimmer", Resources.character_swimmer3, 406, 25));
            characters[4].pokemons.Add(new Pokemons(211, 28));
            characters[4].index = 37;
            actionpoint.Add(new ActionPoint(396, 15, 466, 75, 4));

            #region Environment

            structures.Add(new Structures(3, 0, 17, 460));
            structures.Add(new Structures(3, 0, 16, 331));
            structures.Add(new Structures(3, 0, 102, 327));
            structures.Add(new Structures(3, 0, 143, 462));
            structures.Add(new Structures(3, 0, 234, 320));
            structures.Add(new Structures(3, 0, 292, 470));
            structures.Add(new Structures(3, 0, 364, 315));
            structures.Add(new Structures(3, 0, 439, 442));
            structures.Add(new Structures(3, 0, 517, 308));
            structures.Add(new Structures(3, 0, 582, 445));
            structures.Add(new Structures(3, 0, 667, 295));
            structures.Add(new Structures(3, 0, 715, 441));
            structures.Add(new Structures(3, 0, 162, 86));
            structures.Add(new Structures(3, 0, 370, 198));
            structures.Add(new Structures(3, 0, 587, 66));

            LoadWater();

            #endregion
        }

        private void LoadMap17()
        {
            pbCanvas.BackgroundImage = Resources.grass;

            exit.Add(new ExitPoint(751, 173, 794, 315, 18, 31, 229));

            wildPokemons.Add(new ints(98, 10, 16, 15));
            wildPokemons.Add(new ints(194, 10, 16, 15));
            wildPokemons.Add(new ints(270, 10, 16, 10));
            wildPokemons.Add(new ints(283, 10, 16, 10));
            wildPokemons.Add(new ints(79, 10, 16, 10));
            wildPokemons.Add(new ints(341, 10, 16, 8));
            wildPokemons.Add(new ints(54, 10, 16, 8));
            wildPokemons.Add(new ints(60, 10, 16, 8));
            wildPokemons.Add(new ints(418, 10, 16, 8));
            wildPokemons.Add(new ints(422, 10, 16, 8));

            #region Environment

            structures.Add(new Structures(1, 1, 6, 120, 0));
            structures.Add(new Structures(1, 1, 6, 120, 40));
            structures.Add(new Structures(1, 1, 6, 120, 80));
            structures.Add(new Structures(1, 1, 6, 120, 120));
            structures.Add(new Structures(1, 1, 6, 120, 160));
            structures.Add(new Structures(1, 1, 6, 120, 200));
            structures.Add(new Structures(1, 1, 6, 120, 240));
            structures.Add(new Structures(1, 1, 6, 120, 280));
            structures.Add(new Structures(1, 1, 6, 120, 320));
            structures.Add(new Structures(1, 1, 6, 120, 360));
            structures.Add(new Structures(1, 1, 6, 120, 400));
            structures.Add(new Structures(1, 1, 6, 120, 440));
            structures.Add(new Structures(1, 1, 6, 120, 480));
            structures.Add(new Structures(1, 1, 6, 120, 520));
            structures.Add(new Structures(1, 1, 6, 120, 560));

            structures.Add(new Structures(1, 1, 5, 80, 0));
            structures.Add(new Structures(1, 1, 5, 80, 40));
            structures.Add(new Structures(1, 1, 5, 80, 80));
            structures.Add(new Structures(1, 1, 5, 80, 120));
            structures.Add(new Structures(1, 1, 5, 80, 160));
            structures.Add(new Structures(1, 1, 5, 80, 200));
            structures.Add(new Structures(1, 1, 5, 80, 240));
            structures.Add(new Structures(1, 1, 5, 80, 280));
            structures.Add(new Structures(1, 1, 5, 80, 320));
            structures.Add(new Structures(1, 1, 5, 80, 360));
            structures.Add(new Structures(1, 1, 5, 80, 400));
            structures.Add(new Structures(1, 1, 5, 80, 440));
            structures.Add(new Structures(1, 1, 5, 80, 480));
            structures.Add(new Structures(1, 1, 5, 80, 520));
            structures.Add(new Structures(1, 1, 5, 80, 560));

            structures.Add(new Structures(1, 1, 5, 40, 0));
            structures.Add(new Structures(1, 1, 5, 40, 40));
            structures.Add(new Structures(1, 1, 5, 40, 80));
            structures.Add(new Structures(1, 1, 5, 40, 120));
            structures.Add(new Structures(1, 1, 5, 40, 160));
            structures.Add(new Structures(1, 1, 5, 40, 200));
            structures.Add(new Structures(1, 1, 5, 40, 240));
            structures.Add(new Structures(1, 1, 5, 40, 280));
            structures.Add(new Structures(1, 1, 5, 40, 320));
            structures.Add(new Structures(1, 1, 5, 40, 360));
            structures.Add(new Structures(1, 1, 5, 40, 400));
            structures.Add(new Structures(1, 1, 5, 40, 440));
            structures.Add(new Structures(1, 1, 5, 40, 480));
            structures.Add(new Structures(1, 1, 5, 40, 520));
            structures.Add(new Structures(1, 1, 5, 40, 560));

            structures.Add(new Structures(1, 1, 5, 0, 0));
            structures.Add(new Structures(1, 1, 5, 0, 40));
            structures.Add(new Structures(1, 1, 5, 0, 80));
            structures.Add(new Structures(1, 1, 5, 0, 120));
            structures.Add(new Structures(1, 1, 5, 0, 160));
            structures.Add(new Structures(1, 1, 5, 0, 200));
            structures.Add(new Structures(1, 1, 5, 0, 240));
            structures.Add(new Structures(1, 1, 5, 0, 280));
            structures.Add(new Structures(1, 1, 5, 0, 320));
            structures.Add(new Structures(1, 1, 5, 0, 360));
            structures.Add(new Structures(1, 1, 5, 0, 400));
            structures.Add(new Structures(1, 1, 5, 0, 440));
            structures.Add(new Structures(1, 1, 5, 0, 480));
            structures.Add(new Structures(1, 1, 5, 0, 520));
            structures.Add(new Structures(1, 1, 5, 0, 560));

            trees.Add(new Trees(1, 297, 43));
            trees.Add(new Trees(1, 592, 54));
            trees.Add(new Trees(1, 605, 375));
            trees.Add(new Trees(1, 339, 433));

            flowers.Add(new Flowers(379, 379));
            flowers.Add(new Flowers(486, 364));
            flowers.Add(new Flowers(477, 451));
            flowers.Add(new Flowers(535, 493));
            flowers.Add(new Flowers(436, 526));
            flowers.Add(new Flowers(637, 501));
            flowers.Add(new Flowers(723, 536));
            flowers.Add(new Flowers(724, 451));
            flowers.Add(new Flowers(692, 346));
            flowers.Add(new Flowers(560, 326));
            flowers.Add(new Flowers(557, 408));
            flowers.Add(new Flowers(689, 129));
            flowers.Add(new Flowers(740, 101));
            flowers.Add(new Flowers(707, 36));
            flowers.Add(new Flowers(676, 77));
            flowers.Add(new Flowers(620, -7));
            flowers.Add(new Flowers(466, 28));
            flowers.Add(new Flowers(413, 95));
            flowers.Add(new Flowers(507, 104));
            flowers.Add(new Flowers(524, 42));
            flowers.Add(new Flowers(559, 120));
            flowers.Add(new Flowers(373, 0));

            wildgrass.Add(new WildGrass(186, 326));
            wildgrass.Add(new WildGrass(186, 366));
            wildgrass.Add(new WildGrass(186, 406));
            wildgrass.Add(new WildGrass(186, 446));
            wildgrass.Add(new WildGrass(186, 486));
            wildgrass.Add(new WildGrass(186, 526));
            wildgrass.Add(new WildGrass(226, 526));
            wildgrass.Add(new WildGrass(226, 486));
            wildgrass.Add(new WildGrass(226, 446));
            wildgrass.Add(new WildGrass(226, 406));
            wildgrass.Add(new WildGrass(226, 366));
            wildgrass.Add(new WildGrass(226, 326));
            wildgrass.Add(new WildGrass(266, 326));
            wildgrass.Add(new WildGrass(266, 366));
            wildgrass.Add(new WildGrass(266, 406));
            wildgrass.Add(new WildGrass(266, 446));
            wildgrass.Add(new WildGrass(266, 486));
            wildgrass.Add(new WildGrass(266, 526));
            wildgrass.Add(new WildGrass(266, 126));
            wildgrass.Add(new WildGrass(266, 86));
            wildgrass.Add(new WildGrass(266, 46));
            wildgrass.Add(new WildGrass(266, 6));
            wildgrass.Add(new WildGrass(226, 6));
            wildgrass.Add(new WildGrass(186, 6));
            wildgrass.Add(new WildGrass(186, 46));
            wildgrass.Add(new WildGrass(186, 86));
            wildgrass.Add(new WildGrass(186, 126));
            wildgrass.Add(new WildGrass(226, 126));
            wildgrass.Add(new WildGrass(226, 86));
            wildgrass.Add(new WildGrass(226, 46));

            paths.Add(new Paths(7, 170, 183));
            paths.Add(new Paths(4, 170, 223));
            paths.Add(new Paths(1, 170, 263));
            paths.Add(new Paths(2, 210, 263));
            paths.Add(new Paths(2, 290, 263));
            paths.Add(new Paths(2, 330, 263));
            paths.Add(new Paths(2, 370, 263));
            paths.Add(new Paths(2, 410, 263));
            paths.Add(new Paths(2, 450, 263));
            paths.Add(new Paths(2, 490, 263));
            paths.Add(new Paths(2, 530, 263));
            paths.Add(new Paths(2, 570, 263));
            paths.Add(new Paths(2, 610, 263));
            paths.Add(new Paths(2, 650, 263));
            paths.Add(new Paths(2, 690, 263));
            paths.Add(new Paths(2, 730, 263));
            paths.Add(new Paths(2, 770, 263));
            paths.Add(new Paths(5, 770, 223));
            paths.Add(new Paths(5, 730, 223));
            paths.Add(new Paths(5, 690, 223));
            paths.Add(new Paths(5, 650, 223));
            paths.Add(new Paths(5, 610, 223));
            paths.Add(new Paths(5, 570, 223));
            paths.Add(new Paths(5, 530, 223));
            paths.Add(new Paths(5, 490, 223));
            paths.Add(new Paths(5, 450, 223));
            paths.Add(new Paths(5, 410, 223));
            paths.Add(new Paths(5, 370, 223));
            paths.Add(new Paths(5, 330, 223));
            paths.Add(new Paths(5, 290, 223));
            paths.Add(new Paths(5, 210, 223));
            paths.Add(new Paths(8, 210, 183));
            paths.Add(new Paths(8, 290, 183));
            paths.Add(new Paths(8, 330, 183));
            paths.Add(new Paths(8, 370, 183));
            paths.Add(new Paths(8, 410, 183));
            paths.Add(new Paths(8, 450, 183));
            paths.Add(new Paths(8, 490, 183));
            paths.Add(new Paths(8, 530, 183));
            paths.Add(new Paths(8, 570, 183));
            paths.Add(new Paths(8, 610, 183));
            paths.Add(new Paths(8, 650, 183));
            paths.Add(new Paths(8, 690, 183));
            paths.Add(new Paths(8, 730, 183));
            paths.Add(new Paths(8, 770, 183));
            paths.Add(new Paths(2, 250, 263));
            paths.Add(new Paths(5, 250, 223));
            paths.Add(new Paths(8, 250, 183));

            #endregion
        }

        private void LoadMap18()
        {
            pbCanvas.BackgroundImage = Resources.townpavement;

            exit.Add(new ExitPoint(0, 200, 50, 350, 17, 720, 200));
            exit.Add(new ExitPoint(750, 200, 800, 350, 19, 35, 250));

            characters.Add(new Character("Fighter", Resources.character_fighter2, 150, 180));
            actionpoint.Add(new ActionPoint(130, 160, 200, 230, -13));

            characters.Add(new Character("Dinoman Jr.", Resources.character_dino, 600, 180));
            actionpoint.Add(new ActionPoint(580, 160, 650, 230, -14));

            characters.Add(new Character("Fish Vendor", Resources.character_fisherman, 460, 320));
            actionpoint.Add(new ActionPoint(440, 300, 510, 370, -15));

            characters.Add(new Character("Cameraman", Resources.character_cameraman, 170, 450));
            actionpoint.Add(new ActionPoint(150, 430, 220, 500, -16));
            characters[3].index = 50;

            characters.Add(new Character("Steve", Resources.character_dad2, 380, 440));
            actionpoint.Add(new ActionPoint(360, 420, 430, 490, -17));
            characters[4].index = 51;

            characters.Add(new Character("Painter", Resources.character_painter, 640, 410));
            actionpoint.Add(new ActionPoint(620, 390, 690, 460, -18));
            characters[5].index = 52;

            characters.Add(new Character("Kid", Resources.character_kid2, 60, 400));
            actionpoint.Add(new ActionPoint(40, 380, 110, 450, 6));
            characters[6].index = 53;
            characters[6].pokemons.Add(new Pokemons(25, 20));

            #region Environment

            structures.Add(new Structures(2, 0, 30, 150));
            structures.Add(new Structures(2, 0, 30, 280));

            structures.Add(new Structures(2, 0, 730, 150));
            structures.Add(new Structures(2, 0, 730, 280));

            buildings.Add(new Buildings(6, 180, 55, true));
            buildings.Add(new Buildings(7, 450, 55, true));

            structures.Add(new Structures(1, 0, 260, 520));
            structures.Add(new Structures(1, 0, 340, 520));
            structures.Add(new Structures(1, 0, 420, 520));

            structures.Add(new Structures(1, 0, 260, 400));
            structures.Add(new Structures(1, 0, 420, 400));

            structures.Add(new Structures(1, 1, 260, 400));
            structures.Add(new Structures(1, 1, 260, 430));
            structures.Add(new Structures(1, 1, 260, 460));
            structures.Add(new Structures(1, 1, 260, 490));

            structures.Add(new Structures(1, 1, 486, 400));
            structures.Add(new Structures(1, 1, 486, 430));
            structures.Add(new Structures(1, 1, 486, 460));
            structures.Add(new Structures(1, 1, 486, 490));

            #endregion
        }

        private void LoadMap19()
        {
            pbCanvas.BackgroundImage = Resources.townpavement;

            exit.Add(new ExitPoint(0, 120, 50, 430, 18, 720, 229));
            exit.Add(new ExitPoint(750, 120, 800, 430, 20, 35, 229));

            if (!trainerDefeated[54] || !trainerDefeated[55] || !trainerDefeated[56] || !trainerDefeated[57]
               || !trainerDefeated[58] || !trainerDefeated[59] || !trainerDefeated[60] || !trainerDefeated[61]
               || !trainerDefeated[62])
            {

                characters.Add(new Character("Team Rocket Member", Resources.character_teamrocket, 630, 250));
                characters[0].pokemons.Add(new Pokemons(197, 20));
                characters[0].pokemons.Add(new Pokemons(228, 22));
                characters[0].index = 54;
                actionpoint.Add(new ActionPoint(620, 240, 690, 300, 0));

                characters.Add(new Character("Team Rocket Member", Resources.character_teamrocket, 121, 255));
                characters[1].pokemons.Add(new Pokemons(228, 20));
                characters[1].pokemons.Add(new Pokemons(228, 21));
                characters[1].pokemons.Add(new Pokemons(228, 22));
                characters[1].index = 55;
                actionpoint.Add(new ActionPoint(111, 245, 181, 305, 1));

                characters.Add(new Character("Team Rocket Member", Resources.character_teamrocket, 145, 393));
                characters[2].pokemons.Add(new Pokemons(228, 18));
                characters[2].pokemons.Add(new Pokemons(198, 22));
                characters[2].index = 56;
                actionpoint.Add(new ActionPoint(135, 383, 205, 443, 2));

                characters.Add(new Character("Team Rocket Member", Resources.character_teamrocket, 664, 361));
                characters[3].pokemons.Add(new Pokemons(229, 24));
                characters[3].index = 57;
                actionpoint.Add(new ActionPoint(654, 351, 724, 411, 3));

                characters.Add(new Character("Team Rocket Member", Resources.character_teamrocket, 706, 180));
                characters[4].pokemons.Add(new Pokemons(434, 16));
                characters[4].pokemons.Add(new Pokemons(318, 20));
                characters[4].index = 58;
                actionpoint.Add(new ActionPoint(696, 170, 766, 230, 4));

                characters.Add(new Character("Team Rocket Member", Resources.character_teamrocket2, 371, 445));
                characters[5].pokemons.Add(new Pokemons(261, 16));
                characters[5].pokemons.Add(new Pokemons(261, 17));
                characters[5].pokemons.Add(new Pokemons(262, 18));
                characters[5].index = 59;
                actionpoint.Add(new ActionPoint(361, 435, 431, 495, 5));

                characters.Add(new Character("Team Rocket Member", Resources.character_teamrocket2, 686, 445));
                characters[6].pokemons.Add(new Pokemons(275, 25));
                characters[6].index = 60;
                actionpoint.Add(new ActionPoint(676, 435, 746, 495, 6));

                characters.Add(new Character("Team Rocket Member", Resources.character_teamrocket2, 150, 143));
                characters[7].pokemons.Add(new Pokemons(261, 14));
                characters[7].pokemons.Add(new Pokemons(302, 21));
                characters[7].index = 61;
                actionpoint.Add(new ActionPoint(140, 133, 210, 193, 7));

                characters.Add(new Character("Team Rocket Member", Resources.character_teamrocket2, 489, 109));
                characters[8].pokemons.Add(new Pokemons(261, 15));
                characters[8].pokemons.Add(new Pokemons(261, 17));
                characters[8].pokemons.Add(new Pokemons(215, 21));
                characters[8].index = 62;
                actionpoint.Add(new ActionPoint(479, 99, 549, 159, 8));

                characters.Add(new Character("Policeman", Resources.character_policeman, 66, 169));
                actionpoint.Add(new ActionPoint(56, 159, 126, 219, -19));

                characters.Add(new Character("Cameraman", Resources.character_cameraman, 238, 409));
                actionpoint.Add(new ActionPoint(228, 399, 298, 459, -20));
            }
            else
            {
                characters.Add(new Character("Policeman", Resources.character_policeman, 66, 169));
                actionpoint.Add(new ActionPoint(56, 159, 126, 219, -21));
            }

            #region Environment

            structures.Add(new Structures(1, 0, 45, 15));
            structures.Add(new Structures(1, 0, 125, 15));
            structures.Add(new Structures(1, 0, 205, 15));
            structures.Add(new Structures(1, 0, 285, 15));
            structures.Add(new Structures(1, 0, 365, 15));
            structures.Add(new Structures(1, 0, 445, 15));
            structures.Add(new Structures(1, 0, 525, 15));
            structures.Add(new Structures(1, 0, 605, 15));
            structures.Add(new Structures(1, 0, 685, 15));
            structures.Add(new Structures(1, 1, 45, 45));
            structures.Add(new Structures(1, 1, 45, 75));
            structures.Add(new Structures(1, 1, 45, 105));
            structures.Add(new Structures(1, 1, 751, 31));
            structures.Add(new Structures(1, 1, 751, 61));
            structures.Add(new Structures(1, 1, 751, 91));
            structures.Add(new Structures(1, 0, 43, 507));
            structures.Add(new Structures(1, 0, 123, 507));
            structures.Add(new Structures(1, 0, 203, 507));
            structures.Add(new Structures(1, 0, 283, 507));
            structures.Add(new Structures(1, 0, 363, 507));
            structures.Add(new Structures(1, 0, 443, 507));
            structures.Add(new Structures(1, 0, 523, 507));
            structures.Add(new Structures(1, 0, 603, 507));
            structures.Add(new Structures(1, 0, 683, 507));
            structures.Add(new Structures(1, 1, 750, 474));
            structures.Add(new Structures(1, 1, 750, 444));
            structures.Add(new Structures(1, 1, 750, 414));
            structures.Add(new Structures(1, 1, 43, 477));
            structures.Add(new Structures(1, 1, 43, 447));
            structures.Add(new Structures(1, 1, 43, 417));
            structures.Add(new Structures(1, 0, 223, 381));
            structures.Add(new Structures(1, 0, 303, 381));
            structures.Add(new Structures(1, 0, 383, 381));
            structures.Add(new Structures(1, 0, 463, 381));
            structures.Add(new Structures(1, 1, 617, 376));
            structures.Add(new Structures(1, 1, 617, 346));
            structures.Add(new Structures(1, 1, 617, 226));
            structures.Add(new Structures(1, 0, 543, 381));
            structures.Add(new Structures(1, 0, 551, 211));
            structures.Add(new Structures(1, 0, 471, 211));
            structures.Add(new Structures(1, 0, 391, 211));
            structures.Add(new Structures(1, 0, 311, 211));
            structures.Add(new Structures(1, 0, 231, 211));
            structures.Add(new Structures(1, 1, 226, 223));
            structures.Add(new Structures(1, 1, 226, 343));

            trees.Add(new Trees(1, 267, 254));
            trees.Add(new Trees(1, 479, 288));

            buildings.Add(new Buildings(2, 86, 53, false));
            buildings.Add(new Buildings(2, 539, 53, false));
            buildings.Add(new Buildings(2, 314, 53, false));

            flowers.Add(new Flowers(266, 253));
            flowers.Add(new Flowers(306, 253));
            flowers.Add(new Flowers(346, 253));
            flowers.Add(new Flowers(386, 253));
            flowers.Add(new Flowers(426, 253));
            flowers.Add(new Flowers(466, 253));
            flowers.Add(new Flowers(466, 373));
            flowers.Add(new Flowers(386, 373));
            flowers.Add(new Flowers(306, 373));
            flowers.Add(new Flowers(226, 373));
            flowers.Add(new Flowers(506, 253));
            flowers.Add(new Flowers(546, 253));
            flowers.Add(new Flowers(546, 293));
            flowers.Add(new Flowers(546, 333));
            flowers.Add(new Flowers(546, 373));
            flowers.Add(new Flowers(266, 293));
            flowers.Add(new Flowers(306, 293));
            flowers.Add(new Flowers(346, 293));
            flowers.Add(new Flowers(386, 293));
            flowers.Add(new Flowers(426, 293));
            flowers.Add(new Flowers(506, 293));
            flowers.Add(new Flowers(506, 333));
            flowers.Add(new Flowers(466, 333));
            flowers.Add(new Flowers(426, 333));
            flowers.Add(new Flowers(386, 333));
            flowers.Add(new Flowers(346, 333));
            flowers.Add(new Flowers(266, 333));
            flowers.Add(new Flowers(306, 333));
            flowers.Add(new Flowers(466, 293));
            flowers.Add(new Flowers(266, 373));
            flowers.Add(new Flowers(346, 373));
            flowers.Add(new Flowers(426, 373));
            flowers.Add(new Flowers(506, 373));
            flowers.Add(new Flowers(226, 253));
            flowers.Add(new Flowers(226, 293));
            flowers.Add(new Flowers(226, 333));
            flowers.Add(new Flowers(586, 253));
            flowers.Add(new Flowers(586, 293));
            flowers.Add(new Flowers(586, 373));
            flowers.Add(new Flowers(586, 333));

            #endregion
        }

        private void LoadMap20()
        {
            pbCanvas.BackgroundImage = Resources.townpavement;

            exit.Add(new ExitPoint(0, 200, 50, 350, 19, 720, 200));

            wildPokemons.Add(new ints(63, 10, 15, 33));
            wildPokemons.Add(new ints(64, 16, 16, 1));
            wildPokemons.Add(new ints(238, 10, 16, 8));
            wildPokemons.Add(new ints(280, 10, 16, 3));
            wildPokemons.Add(new ints(133, 10, 16, 10));
            wildPokemons.Add(new ints(52, 10, 16, 20));
            wildPokemons.Add(new ints(441, 10, 16, 10));
            wildPokemons.Add(new ints(431, 10, 16, 15));

            characters.Add(new Character("Painter", Resources.character_painter, 281, 215));
            Pokemons poke = new Pokemons(235, 30);
            poke.moves = new List<Attack>();
            poke.moves.Add(new Attack(352));
            poke.moves.Add(new Attack(7));
            poke.moves.Add(new Attack(202));
            poke.moves.Add(new Attack(233));
            characters[0].pokemons.Add(poke);
            characters[0].index = 63;
            actionpoint.Add(new ActionPoint(271, 205, 341, 265, 0));

            #region Environment

            structures.Add(new Structures(2, 0, 30, 150));
            structures.Add(new Structures(2, 0, 30, 280));

            int waterType = 1;
            if (!trainerDefeated[64]) waterType = 2; // water is polluted until you beat the gym leader

            structures.Add(new Structures(1, waterType, 4, 676, -4));
            structures.Add(new Structures(1, waterType, 4, 676, 36));
            structures.Add(new Structures(1, waterType, 4, 676, 76));
            structures.Add(new Structures(1, waterType, 4, 676, 116));
            structures.Add(new Structures(1, waterType, 4, 676, 156));
            structures.Add(new Structures(1, waterType, 4, 676, 196));
            structures.Add(new Structures(1, waterType, 4, 676, 236));
            structures.Add(new Structures(1, waterType, 4, 676, 276));
            structures.Add(new Structures(1, waterType, 4, 676, 316));
            structures.Add(new Structures(1, waterType, 4, 676, 356));
            structures.Add(new Structures(1, waterType, 4, 676, 396));
            structures.Add(new Structures(1, waterType, 4, 676, 436));
            structures.Add(new Structures(1, waterType, 4, 676, 476));
            structures.Add(new Structures(1, waterType, 4, 676, 516));
            structures.Add(new Structures(1, waterType, 4, 676, 556));
            structures.Add(new Structures(1, waterType, 4, 676, 596));
            structures.Add(new Structures(1, waterType, 5, 716, 596));
            structures.Add(new Structures(1, waterType, 5, 756, 596));
            structures.Add(new Structures(1, waterType, 5, 756, 556));
            structures.Add(new Structures(1, waterType, 5, 796, 556));
            structures.Add(new Structures(1, waterType, 5, 796, 596));
            structures.Add(new Structures(1, waterType, 5, 716, 556));
            structures.Add(new Structures(1, waterType, 5, 716, 516));
            structures.Add(new Structures(1, waterType, 5, 716, 476));
            structures.Add(new Structures(1, waterType, 5, 716, 436));
            structures.Add(new Structures(1, waterType, 5, 716, 396));
            structures.Add(new Structures(1, waterType, 5, 716, 356));
            structures.Add(new Structures(1, waterType, 5, 716, 316));
            structures.Add(new Structures(1, waterType, 5, 716, 276));
            structures.Add(new Structures(1, waterType, 5, 716, 236));
            structures.Add(new Structures(1, waterType, 5, 716, 196));
            structures.Add(new Structures(1, waterType, 5, 716, 156));
            structures.Add(new Structures(1, waterType, 5, 716, 116));
            structures.Add(new Structures(1, waterType, 5, 716, 76));
            structures.Add(new Structures(1, waterType, 5, 716, 36));
            structures.Add(new Structures(1, waterType, 5, 716, -4));
            structures.Add(new Structures(1, waterType, 5, 756, -4));
            structures.Add(new Structures(1, waterType, 5, 796, -4));
            structures.Add(new Structures(1, waterType, 5, 796, 36));
            structures.Add(new Structures(1, waterType, 5, 796, 76));
            structures.Add(new Structures(1, waterType, 5, 796, 116));
            structures.Add(new Structures(1, waterType, 5, 796, 156));
            structures.Add(new Structures(1, waterType, 5, 796, 196));
            structures.Add(new Structures(1, waterType, 5, 796, 236));
            structures.Add(new Structures(1, waterType, 5, 796, 276));
            structures.Add(new Structures(1, waterType, 5, 796, 316));
            structures.Add(new Structures(1, waterType, 5, 796, 356));
            structures.Add(new Structures(1, waterType, 5, 796, 396));
            structures.Add(new Structures(1, waterType, 5, 796, 436));
            structures.Add(new Structures(1, waterType, 5, 796, 476));
            structures.Add(new Structures(1, waterType, 5, 796, 516));
            structures.Add(new Structures(1, waterType, 5, 756, 516));
            structures.Add(new Structures(1, waterType, 5, 756, 476));
            structures.Add(new Structures(1, waterType, 5, 756, 436));
            structures.Add(new Structures(1, waterType, 5, 756, 396));
            structures.Add(new Structures(1, waterType, 5, 756, 356));
            structures.Add(new Structures(1, waterType, 5, 756, 316));
            structures.Add(new Structures(1, waterType, 5, 756, 276));
            structures.Add(new Structures(1, waterType, 5, 756, 236));
            structures.Add(new Structures(1, waterType, 5, 756, 196));
            structures.Add(new Structures(1, waterType, 5, 756, 156));
            structures.Add(new Structures(1, waterType, 5, 756, 116));
            structures.Add(new Structures(1, waterType, 5, 756, 76));
            structures.Add(new Structures(1, waterType, 5, 756, 36));

            structures.Add(new Structures(1, 0, 556, 8));
            structures.Add(new Structures(1, 0, 476, 8));
            structures.Add(new Structures(1, 0, 396, 8));
            structures.Add(new Structures(1, 0, 316, 8));
            structures.Add(new Structures(1, 0, 236, 8));
            structures.Add(new Structures(1, 0, 156, 8));
            structures.Add(new Structures(1, 0, 76, 8));
            structures.Add(new Structures(1, 0, 565, 531));
            structures.Add(new Structures(1, 0, 485, 531));
            structures.Add(new Structures(1, 0, 405, 531));
            structures.Add(new Structures(1, 0, 325, 531));
            structures.Add(new Structures(1, 0, 245, 531));
            structures.Add(new Structures(1, 0, 165, 531));
            structures.Add(new Structures(1, 0, 85, 531));
            structures.Add(new Structures(2, 0, 617, 118));
            structures.Add(new Structures(2, 0, 396, 351));
            structures.Add(new Structures(2, 0, 628, 356));
            structures.Add(new Structures(2, 0, 392, 119));

            buildings.Add(new Buildings(1, 432, 48, true));
            buildings.Add(new Buildings(0, 445, 295, true));

            wildgrass.Add(new WildGrass(86, 90));
            wildgrass.Add(new WildGrass(126, 90));
            wildgrass.Add(new WildGrass(166, 90));
            wildgrass.Add(new WildGrass(206, 90));
            wildgrass.Add(new WildGrass(206, 130));
            wildgrass.Add(new WildGrass(206, 170));
            wildgrass.Add(new WildGrass(206, 210));
            wildgrass.Add(new WildGrass(206, 250));
            wildgrass.Add(new WildGrass(206, 290));
            wildgrass.Add(new WildGrass(206, 330));
            wildgrass.Add(new WildGrass(206, 370));
            wildgrass.Add(new WildGrass(206, 410));
            wildgrass.Add(new WildGrass(206, 450));
            wildgrass.Add(new WildGrass(166, 450));
            wildgrass.Add(new WildGrass(126, 450));
            wildgrass.Add(new WildGrass(86, 450));
            wildgrass.Add(new WildGrass(86, 410));
            wildgrass.Add(new WildGrass(86, 370));
            wildgrass.Add(new WildGrass(86, 330));
            wildgrass.Add(new WildGrass(86, 290));
            wildgrass.Add(new WildGrass(86, 250));
            wildgrass.Add(new WildGrass(86, 210));
            wildgrass.Add(new WildGrass(86, 170));
            wildgrass.Add(new WildGrass(86, 130));
            wildgrass.Add(new WildGrass(126, 130));
            wildgrass.Add(new WildGrass(166, 130));
            wildgrass.Add(new WildGrass(166, 170));
            wildgrass.Add(new WildGrass(166, 210));
            wildgrass.Add(new WildGrass(166, 250));
            wildgrass.Add(new WildGrass(166, 290));
            wildgrass.Add(new WildGrass(166, 330));
            wildgrass.Add(new WildGrass(166, 370));
            wildgrass.Add(new WildGrass(166, 410));
            wildgrass.Add(new WildGrass(126, 410));
            wildgrass.Add(new WildGrass(126, 370));
            wildgrass.Add(new WildGrass(126, 330));
            wildgrass.Add(new WildGrass(126, 290));
            wildgrass.Add(new WildGrass(126, 250));
            wildgrass.Add(new WildGrass(126, 210));
            wildgrass.Add(new WildGrass(126, 170));
            wildgrass.Add(new WildGrass(246, 90));
            wildgrass.Add(new WildGrass(286, 90));
            wildgrass.Add(new WildGrass(326, 90));
            wildgrass.Add(new WildGrass(326, 130));
            wildgrass.Add(new WildGrass(366, 210));
            wildgrass.Add(new WildGrass(406, 210));
            wildgrass.Add(new WildGrass(446, 210));
            wildgrass.Add(new WildGrass(486, 210));
            wildgrass.Add(new WildGrass(526, 210));
            wildgrass.Add(new WildGrass(566, 210));
            wildgrass.Add(new WildGrass(566, 250));
            wildgrass.Add(new WildGrass(526, 250));
            wildgrass.Add(new WildGrass(486, 250));
            wildgrass.Add(new WildGrass(446, 250));
            wildgrass.Add(new WildGrass(406, 250));
            wildgrass.Add(new WildGrass(366, 250));
            wildgrass.Add(new WildGrass(326, 330));
            wildgrass.Add(new WildGrass(326, 370));
            wildgrass.Add(new WildGrass(326, 410));
            wildgrass.Add(new WildGrass(326, 450));
            wildgrass.Add(new WildGrass(366, 450));
            wildgrass.Add(new WildGrass(406, 450));
            wildgrass.Add(new WildGrass(446, 450));
            wildgrass.Add(new WildGrass(486, 450));
            wildgrass.Add(new WildGrass(526, 450));
            wildgrass.Add(new WildGrass(566, 450));
            wildgrass.Add(new WildGrass(606, 450));
            wildgrass.Add(new WildGrass(286, 450));
            wildgrass.Add(new WildGrass(246, 450));
            wildgrass.Add(new WildGrass(246, 410));
            wildgrass.Add(new WildGrass(286, 410));
            wildgrass.Add(new WildGrass(286, 410));
            wildgrass.Add(new WildGrass(246, 410));
            wildgrass.Add(new WildGrass(246, 370));
            wildgrass.Add(new WildGrass(286, 370));
            wildgrass.Add(new WildGrass(286, 330));
            wildgrass.Add(new WildGrass(246, 330));
            wildgrass.Add(new WildGrass(246, 130));
            wildgrass.Add(new WildGrass(286, 130));

            #endregion
        }

        private void LoadMap21()
        {
            pbCanvas.BackgroundImage = Resources.waterwaves;

            exit.Add(new ExitPoint(4, 4, 350, 56, 20, 620, 205));
            exit.Add(new ExitPoint(304, 549, 474, 593, 22, 270, 115));

            wildPokemons.Add(new ints(90, 14, 20, 15));
            wildPokemons.Add(new ints(366, 14, 20, 15));
            wildPokemons.Add(new ints(222, 14, 20, 10));
            wildPokemons.Add(new ints(278, 14, 20, 20));
            wildPokemons.Add(new ints(72, 14, 20, 22));
            wildPokemons.Add(new ints(120, 14, 20, 8));
            wildPokemons.Add(new ints(458, 14, 20, 2));
            wildPokemons.Add(new ints(116, 14, 20, 8));

            characters.Add(new Character("Swimmer", Resources.character_swimmer, 348, 237));
            characters[0].pokemons.Add(new Pokemons(367, 25));
            characters[0].index = 65;
            actionpoint.Add(new ActionPoint(338, 227, 408, 287, 0));

            characters.Add(new Character("Swimmer", Resources.character_swimmer2, 283, 367));
            characters[1].pokemons.Add(new Pokemons(368, 25));
            characters[1].index = 66;
            actionpoint.Add(new ActionPoint(273, 357, 343, 417, 1));

            characters.Add(new Character("Swimmer", Resources.character_swimmer4, 197, 140));
            characters[2].pokemons.Add(new Pokemons(366, 20));
            characters[2].pokemons.Add(new Pokemons(222, 22));
            characters[2].index = 67;
            actionpoint.Add(new ActionPoint(187, 130, 257, 190, 2));

            characters.Add(new Character("Swimmer", Resources.character_swimmer3, 415, 313));
            characters[3].pokemons.Add(new Pokemons(90, 22));
            characters[3].pokemons.Add(new Pokemons(278, 18));
            characters[3].pokemons.Add(new Pokemons(116, 24));
            characters[3].index = 68;
            actionpoint.Add(new ActionPoint(405, 303, 475, 363, 3));

            characters.Add(new Character("Swimmer", Resources.character_swimmer2, 365, 117));
            characters[4].pokemons.Add(new Pokemons(120, 24));
            characters[4].pokemons.Add(new Pokemons(121, 26));
            characters[4].index = 69;
            actionpoint.Add(new ActionPoint(355, 107, 425, 167, 4));

            #region Environment

            structures.Add(new Structures(3, 0, 26, 90));
            structures.Add(new Structures(3, 0, 97, 145));
            structures.Add(new Structures(3, 0, 152, 207));
            structures.Add(new Structures(3, 0, 184, 279));
            structures.Add(new Structures(3, 0, 206, 367));
            structures.Add(new Structures(3, 0, 233, 468));
            structures.Add(new Structures(3, 0, 318, 49));
            structures.Add(new Structures(3, 0, 426, 141));
            structures.Add(new Structures(3, 0, 439, 240));
            structures.Add(new Structures(3, 0, 463, 349));
            structures.Add(new Structures(3, 0, 479, 451));
            structures.Add(new Structures(3, 0, 570, 63));
            structures.Add(new Structures(3, 0, 650, 225));
            structures.Add(new Structures(3, 0, 610, 341));
            structures.Add(new Structures(3, 0, 562, 146));
            structures.Add(new Structures(3, 0, 705, 22));
            structures.Add(new Structures(3, 0, 700, 388));
            structures.Add(new Structures(3, 0, 80, 280));
            structures.Add(new Structures(3, 0, 60, 395));
            structures.Add(new Structures(3, 0, 147, 459));
            structures.Add(new Structures(3, 0, 612, 453));

            LoadWater();

            #endregion
        }

        private void LoadMap22()
        {
            pbCanvas.BackgroundImage = Resources.ground;

            wildPokemons.Add(new ints(21, 14, 19, 23));
            wildPokemons.Add(new ints(22, 20, 20, 2));
            wildPokemons.Add(new ints(27, 14, 20, 25));
            wildPokemons.Add(new ints(231, 14, 20, 12));
            wildPokemons.Add(new ints(331, 14, 20, 12));
            wildPokemons.Add(new ints(328, 14, 20, 20));
            wildPokemons.Add(new ints(81, 14, 20, 6));

            characters.Add(new Character("Explorer", Resources.character_explorer, 564, 488));
            characters[0].pokemons.Add(new Pokemons(231, 25));
            characters[0].pokemons.Add(new Pokemons(81, 22));
            characters[0].index = 70;
            actionpoint.Add(new ActionPoint(554, 478, 624, 538, 0));

            characters.Add(new Character("Explorer", Resources.character_explorer, 303, 514));
            characters[1].pokemons.Add(new Pokemons(328, 21));
            characters[1].pokemons.Add(new Pokemons(331, 22));
            characters[1].pokemons.Add(new Pokemons(328, 24));
            characters[1].index = 71;
            actionpoint.Add(new ActionPoint(293, 504, 363, 564, 1));

            characters.Add(new Character("Bird Observer", Resources.character_bugcatcher2, 422, 230));
            characters[2].pokemons.Add(new Pokemons(21, 15));
            characters[2].pokemons.Add(new Pokemons(16, 15));
            characters[2].pokemons.Add(new Pokemons(22, 20));
            characters[2].index = 72;
            actionpoint.Add(new ActionPoint(412, 220, 482, 280, 2));

            characters.Add(new Character("Kid", Resources.character_kid, 156, 140));
            characters[3].pokemons.Add(new Pokemons(27, 23));
            characters[3].index = 73;
            actionpoint.Add(new ActionPoint(146, 130, 216, 190, 3));

            #region Environment

            structures.Add(new Structures(1, 1, 2, -3, 74));
            structures.Add(new Structures(1, 1, 2, 37, 74));
            structures.Add(new Structures(1, 1, 2, 77, 74));
            structures.Add(new Structures(1, 1, 2, 117, 74));
            structures.Add(new Structures(1, 1, 2, 157, 74));
            structures.Add(new Structures(1, 1, 2, 197, 74));
            structures.Add(new Structures(1, 1, 2, 237, 74));
            structures.Add(new Structures(1, 1, 2, 277, 74));
            structures.Add(new Structures(1, 1, 2, 317, 74));
            structures.Add(new Structures(1, 1, 2, 357, 74));
            structures.Add(new Structures(1, 1, 2, 397, 74));
            structures.Add(new Structures(1, 1, 2, 437, 74));
            structures.Add(new Structures(1, 1, 2, 477, 74));
            structures.Add(new Structures(1, 1, 2, 517, 74));
            structures.Add(new Structures(1, 1, 2, 557, 74));
            structures.Add(new Structures(1, 1, 2, 597, 74));
            structures.Add(new Structures(1, 1, 2, 637, 74));
            structures.Add(new Structures(1, 1, 2, 677, 74));
            structures.Add(new Structures(1, 1, 2, 717, 74));
            structures.Add(new Structures(1, 1, 2, 757, 74));
            structures.Add(new Structures(1, 1, 2, 797, 74));
            structures.Add(new Structures(1, 1, 5, 797, 34));
            structures.Add(new Structures(1, 1, 5, 797, -6));
            structures.Add(new Structures(1, 1, 5, 757, -6));
            structures.Add(new Structures(1, 1, 5, 717, -6));
            structures.Add(new Structures(1, 1, 5, 677, -6));
            structures.Add(new Structures(1, 1, 5, 637, -6));
            structures.Add(new Structures(1, 1, 5, 597, -6));
            structures.Add(new Structures(1, 1, 5, 557, -6));
            structures.Add(new Structures(1, 1, 5, 517, -6));
            structures.Add(new Structures(1, 1, 5, 477, -6));
            structures.Add(new Structures(1, 1, 5, 437, -6));
            structures.Add(new Structures(1, 1, 5, 397, -6));
            structures.Add(new Structures(1, 1, 5, 357, -6));
            structures.Add(new Structures(1, 1, 5, 317, -6));
            structures.Add(new Structures(1, 1, 5, 277, -6));
            structures.Add(new Structures(1, 1, 5, 237, -6));
            structures.Add(new Structures(1, 1, 5, 197, -6));
            structures.Add(new Structures(1, 1, 5, 157, -6));
            structures.Add(new Structures(1, 1, 5, 117, -6));
            structures.Add(new Structures(1, 1, 5, 77, -6));
            structures.Add(new Structures(1, 1, 5, 37, -6));
            structures.Add(new Structures(1, 1, 5, -3, -6));
            structures.Add(new Structures(1, 1, 5, -3, 34));
            structures.Add(new Structures(1, 1, 5, 37, 34));
            structures.Add(new Structures(1, 1, 5, 77, 34));
            structures.Add(new Structures(1, 1, 5, 117, 34));
            structures.Add(new Structures(1, 1, 5, 157, 34));
            structures.Add(new Structures(1, 1, 5, 197, 34));
            structures.Add(new Structures(1, 1, 5, 237, 34));
            structures.Add(new Structures(1, 1, 5, 277, 34));
            structures.Add(new Structures(1, 1, 5, 317, 34));
            structures.Add(new Structures(1, 1, 5, 357, 34));
            structures.Add(new Structures(1, 1, 5, 397, 34));
            structures.Add(new Structures(1, 1, 5, 437, 34));
            structures.Add(new Structures(1, 1, 5, 477, 34));
            structures.Add(new Structures(1, 1, 5, 517, 34));
            structures.Add(new Structures(1, 1, 5, 557, 34));
            structures.Add(new Structures(1, 1, 5, 597, 34));
            structures.Add(new Structures(1, 1, 5, 637, 34));
            structures.Add(new Structures(1, 1, 5, 677, 34));
            structures.Add(new Structures(1, 1, 5, 717, 34));
            structures.Add(new Structures(1, 1, 5, 757, 34));
            structures.Add(new Structures(3, 0, 28, 109));
            structures.Add(new Structures(3, 0, 24, 255));
            structures.Add(new Structures(3, 0, 168, 328));
            structures.Add(new Structures(3, 0, 77, 395));
            structures.Add(new Structures(3, 0, 687, 143));
            structures.Add(new Structures(3, 0, 239, 458));
            structures.Add(new Structures(3, 0, 387, 504));
            structures.Add(new Structures(3, 0, 632, 512));
            structures.Add(new Structures(3, 0, 705, 329));
            structures.Add(new Structures(3, 0, 442, 176));

            buildings.Add(new Buildings(-1, 462, 330, true));

            wildgrass.Add(new WildGrass(101, 183));
            wildgrass.Add(new WildGrass(101, 223));
            wildgrass.Add(new WildGrass(101, 263));
            wildgrass.Add(new WildGrass(101, 303));
            wildgrass.Add(new WildGrass(141, 303));
            wildgrass.Add(new WildGrass(181, 303));
            wildgrass.Add(new WildGrass(221, 303));
            wildgrass.Add(new WildGrass(261, 303));
            wildgrass.Add(new WildGrass(301, 303));
            wildgrass.Add(new WildGrass(341, 303));
            wildgrass.Add(new WildGrass(381, 303));
            wildgrass.Add(new WildGrass(421, 303));
            wildgrass.Add(new WildGrass(421, 343));
            wildgrass.Add(new WildGrass(421, 383));
            wildgrass.Add(new WildGrass(421, 423));
            wildgrass.Add(new WildGrass(421, 463));
            wildgrass.Add(new WildGrass(381, 463));
            wildgrass.Add(new WildGrass(341, 463));
            wildgrass.Add(new WildGrass(301, 463));
            wildgrass.Add(new WildGrass(301, 423));
            wildgrass.Add(new WildGrass(341, 423));
            wildgrass.Add(new WildGrass(381, 423));
            wildgrass.Add(new WildGrass(381, 343));
            wildgrass.Add(new WildGrass(341, 343));
            wildgrass.Add(new WildGrass(341, 383));
            wildgrass.Add(new WildGrass(301, 383));
            wildgrass.Add(new WildGrass(301, 343));
            wildgrass.Add(new WildGrass(261, 343));
            wildgrass.Add(new WildGrass(261, 383));
            wildgrass.Add(new WildGrass(261, 423));
            wildgrass.Add(new WildGrass(221, 423));
            wildgrass.Add(new WildGrass(181, 423));
            wildgrass.Add(new WildGrass(181, 463));
            wildgrass.Add(new WildGrass(181, 503));
            wildgrass.Add(new WildGrass(181, 543));
            wildgrass.Add(new WildGrass(141, 543));
            wildgrass.Add(new WildGrass(101, 543));
            wildgrass.Add(new WildGrass(61, 543));
            wildgrass.Add(new WildGrass(21, 543));
            wildgrass.Add(new WildGrass(21, 503));
            wildgrass.Add(new WildGrass(61, 503));
            wildgrass.Add(new WildGrass(101, 503));
            wildgrass.Add(new WildGrass(141, 503));
            wildgrass.Add(new WildGrass(141, 463));
            wildgrass.Add(new WildGrass(101, 463));
            wildgrass.Add(new WildGrass(61, 463));
            wildgrass.Add(new WildGrass(21, 463));
            wildgrass.Add(new WildGrass(101, 343));
            wildgrass.Add(new WildGrass(61, 343));
            wildgrass.Add(new WildGrass(21, 343));
            wildgrass.Add(new WildGrass(21, 383));
            wildgrass.Add(new WildGrass(21, 423));
            wildgrass.Add(new WildGrass(661, 463));
            wildgrass.Add(new WildGrass(701, 463));
            wildgrass.Add(new WildGrass(741, 463));
            wildgrass.Add(new WildGrass(741, 503));
            wildgrass.Add(new WildGrass(741, 543));
            wildgrass.Add(new WildGrass(701, 543));
            wildgrass.Add(new WildGrass(701, 503));
            wildgrass.Add(new WildGrass(741, 423));
            wildgrass.Add(new WildGrass(701, 423));
            wildgrass.Add(new WildGrass(661, 423));
            wildgrass.Add(new WildGrass(661, 383));
            wildgrass.Add(new WildGrass(661, 343));
            wildgrass.Add(new WildGrass(661, 303));
            wildgrass.Add(new WildGrass(701, 303));
            wildgrass.Add(new WildGrass(741, 303));
            wildgrass.Add(new WildGrass(741, 263));
            wildgrass.Add(new WildGrass(741, 223));
            wildgrass.Add(new WildGrass(701, 223));
            wildgrass.Add(new WildGrass(701, 263));
            wildgrass.Add(new WildGrass(661, 223));
            wildgrass.Add(new WildGrass(621, 223));
            wildgrass.Add(new WildGrass(621, 183));
            wildgrass.Add(new WildGrass(621, 143));
            wildgrass.Add(new WildGrass(581, 143));
            wildgrass.Add(new WildGrass(541, 143));
            wildgrass.Add(new WildGrass(501, 143));
            wildgrass.Add(new WildGrass(501, 183));
            wildgrass.Add(new WildGrass(501, 223));
            wildgrass.Add(new WildGrass(541, 223));
            wildgrass.Add(new WildGrass(581, 223));
            wildgrass.Add(new WildGrass(581, 183));
            wildgrass.Add(new WildGrass(541, 183));
            wildgrass.Add(new WildGrass(381, 383));

            hills.Add(new Hills(207, 240, 120));
            hills.Add(new Hills(551, 286, 115));

            #endregion
        }

        private void LoadMap23()
        {
            pbCanvas.BackgroundImage = Resources.mountain;

            exit.Add(new ExitPoint(214, 2, 357, 68, 24, 253, 500));

            wildPokemons.Add(new ints(74, 14, 20, 20));
            wildPokemons.Add(new ints(325, 14, 20, 15));
            wildPokemons.Add(new ints(104, 14, 20, 10));
            wildPokemons.Add(new ints(218, 14, 20, 20));
            wildPokemons.Add(new ints(322, 14, 20, 15));
            wildPokemons.Add(new ints(216, 14, 20, 7));
            wildPokemons.Add(new ints(207, 14, 20, 3));
            wildPokemons.Add(new ints(240, 14, 20, 2));
            wildPokemons.Add(new ints(304, 14, 20, 4));
            wildPokemons.Add(new ints(324, 14, 20, 4));

            characters.Add(new Character("Explorer", Resources.character_explorer, 591, 424));
            characters[0].pokemons.Add(new Pokemons(74, 21));
            characters[0].pokemons.Add(new Pokemons(75, 25));
            characters[0].pokemons.Add(new Pokemons(324, 28));
            characters[0].index = 74;
            actionpoint.Add(new ActionPoint(581, 414, 651, 474, 0));

            characters.Add(new Character("Explorer", Resources.character_explorer, 558, 27));
            characters[1].pokemons.Add(new Pokemons(104, 20));
            characters[1].pokemons.Add(new Pokemons(218, 23));
            characters[1].pokemons.Add(new Pokemons(322, 25));
            characters[1].index = 75;
            actionpoint.Add(new ActionPoint(548, 17, 618, 77, 1));

            characters.Add(new Character("Explorer", Resources.character_explorer, 694, 138));
            characters[2].pokemons.Add(new Pokemons(472, 30));
            characters[2].index = 76;
            actionpoint.Add(new ActionPoint(684, 128, 754, 188, 2));

            characters.Add(new Character("Explorer", Resources.character_explorer, 63, 225));
            characters[3].pokemons.Add(new Pokemons(304, 18));
            characters[3].pokemons.Add(new Pokemons(126, 26));
            characters[3].index = 77;
            actionpoint.Add(new ActionPoint(53, 215, 123, 275, 3));

            characters.Add(new Character("Kid", Resources.character_kid, 249, 407));
            characters[4].pokemons.Add(new Pokemons(216, 16));
            characters[4].pokemons.Add(new Pokemons(325, 18));
            characters[4].index = 78;
            actionpoint.Add(new ActionPoint(239, 397, 309, 457, 4));

            #region Environment

            structures.Add(new Structures(3, 0, 659, 368));
            structures.Add(new Structures(3, 0, 465, 329));
            structures.Add(new Structures(3, 0, 384, 131));
            structures.Add(new Structures(3, 0, 147, 308));
            structures.Add(new Structures(3, 0, 119, 433));
            structures.Add(new Structures(3, 0, 342, 475));
            structures.Add(new Structures(3, 0, 152, 3));
            structures.Add(new Structures(3, 0, 359, -1));
            structures.Add(new Structures(3, 0, 66, 114));

            buildings.Add(new Buildings(-1, 526, 209, true));

            wildgrass.Add(new WildGrass(158, 109));
            wildgrass.Add(new WildGrass(198, 109));
            wildgrass.Add(new WildGrass(238, 109));
            wildgrass.Add(new WildGrass(278, 109));
            wildgrass.Add(new WildGrass(318, 109));
            wildgrass.Add(new WildGrass(318, 149));
            wildgrass.Add(new WildGrass(278, 149));
            wildgrass.Add(new WildGrass(238, 149));
            wildgrass.Add(new WildGrass(198, 149));
            wildgrass.Add(new WildGrass(158, 149));
            wildgrass.Add(new WildGrass(158, 189));
            wildgrass.Add(new WildGrass(158, 229));
            wildgrass.Add(new WildGrass(158, 269));
            wildgrass.Add(new WildGrass(198, 269));
            wildgrass.Add(new WildGrass(198, 229));
            wildgrass.Add(new WildGrass(198, 189));
            wildgrass.Add(new WildGrass(238, 189));
            wildgrass.Add(new WildGrass(278, 189));
            wildgrass.Add(new WildGrass(318, 189));
            wildgrass.Add(new WildGrass(358, 189));
            wildgrass.Add(new WildGrass(398, 189));
            wildgrass.Add(new WildGrass(438, 189));
            wildgrass.Add(new WildGrass(478, 189));
            wildgrass.Add(new WildGrass(478, 229));
            wildgrass.Add(new WildGrass(478, 269));
            wildgrass.Add(new WildGrass(438, 269));
            wildgrass.Add(new WildGrass(398, 269));
            wildgrass.Add(new WildGrass(358, 269));
            wildgrass.Add(new WildGrass(318, 269));
            wildgrass.Add(new WildGrass(278, 269));
            wildgrass.Add(new WildGrass(238, 269));
            wildgrass.Add(new WildGrass(238, 229));
            wildgrass.Add(new WildGrass(278, 229));
            wildgrass.Add(new WildGrass(318, 229));
            wildgrass.Add(new WildGrass(358, 229));
            wildgrass.Add(new WildGrass(398, 229));
            wildgrass.Add(new WildGrass(438, 229));
            wildgrass.Add(new WildGrass(238, 309));
            wildgrass.Add(new WildGrass(238, 349));
            wildgrass.Add(new WildGrass(278, 349));
            wildgrass.Add(new WildGrass(278, 309));
            wildgrass.Add(new WildGrass(318, 309));
            wildgrass.Add(new WildGrass(358, 309));
            wildgrass.Add(new WildGrass(398, 309));
            wildgrass.Add(new WildGrass(398, 349));
            wildgrass.Add(new WildGrass(358, 349));
            wildgrass.Add(new WildGrass(318, 349));
            wildgrass.Add(new WildGrass(318, 389));
            wildgrass.Add(new WildGrass(318, 429));
            wildgrass.Add(new WildGrass(358, 429));
            wildgrass.Add(new WildGrass(358, 389));
            wildgrass.Add(new WildGrass(398, 389));
            wildgrass.Add(new WildGrass(398, 429));
            wildgrass.Add(new WildGrass(398, 469));
            wildgrass.Add(new WildGrass(398, 509));
            wildgrass.Add(new WildGrass(438, 509));
            wildgrass.Add(new WildGrass(478, 509));
            wildgrass.Add(new WildGrass(518, 509));
            wildgrass.Add(new WildGrass(558, 509));
            wildgrass.Add(new WildGrass(518, 389));
            wildgrass.Add(new WildGrass(478, 389));
            wildgrass.Add(new WildGrass(438, 389));
            wildgrass.Add(new WildGrass(438, 429));
            wildgrass.Add(new WildGrass(438, 469));
            wildgrass.Add(new WildGrass(478, 469));
            wildgrass.Add(new WildGrass(518, 469));
            wildgrass.Add(new WildGrass(518, 429));
            wildgrass.Add(new WildGrass(478, 429));
            wildgrass.Add(new WildGrass(598, 509));
            wildgrass.Add(new WildGrass(638, 509));
            wildgrass.Add(new WildGrass(678, 509));
            wildgrass.Add(new WildGrass(718, 509));
            wildgrass.Add(new WildGrass(718, 469));
            wildgrass.Add(new WildGrass(718, 429));
            wildgrass.Add(new WildGrass(678, 429));
            wildgrass.Add(new WildGrass(678, 469));
            wildgrass.Add(new WildGrass(718, 389));
            wildgrass.Add(new WildGrass(718, 349));
            wildgrass.Add(new WildGrass(718, 309));
            wildgrass.Add(new WildGrass(718, 269));
            wildgrass.Add(new WildGrass(718, 229));
            wildgrass.Add(new WildGrass(718, 69));
            wildgrass.Add(new WildGrass(718, 29));
            wildgrass.Add(new WildGrass(678, 69));
            wildgrass.Add(new WildGrass(678, 29));
            wildgrass.Add(new WildGrass(638, 29));
            wildgrass.Add(new WildGrass(638, 69));
            wildgrass.Add(new WildGrass(638, 109));
            wildgrass.Add(new WildGrass(598, 109));
            wildgrass.Add(new WildGrass(598, 149));
            wildgrass.Add(new WildGrass(558, 149));
            wildgrass.Add(new WildGrass(518, 149));
            wildgrass.Add(new WildGrass(478, 149));
            wildgrass.Add(new WildGrass(558, 109));
            wildgrass.Add(new WildGrass(518, 109));
            wildgrass.Add(new WildGrass(478, 109));
            wildgrass.Add(new WildGrass(478, 69));
            wildgrass.Add(new WildGrass(438, 69));
            wildgrass.Add(new WildGrass(398, 69));
            wildgrass.Add(new WildGrass(358, 69));
            wildgrass.Add(new WildGrass(78, 389));
            wildgrass.Add(new WildGrass(38, 389));
            wildgrass.Add(new WildGrass(38, 429));
            wildgrass.Add(new WildGrass(38, 469));
            wildgrass.Add(new WildGrass(38, 509));
            wildgrass.Add(new WildGrass(78, 509));
            wildgrass.Add(new WildGrass(78, 469));
            wildgrass.Add(new WildGrass(78, 429));
            wildgrass.Add(new WildGrass(78, 349));
            wildgrass.Add(new WildGrass(78, 309));
            wildgrass.Add(new WildGrass(38, 309));
            wildgrass.Add(new WildGrass(38, 349));
            wildgrass.Add(new WildGrass(78, 69));
            wildgrass.Add(new WildGrass(38, 69));
            wildgrass.Add(new WildGrass(38, 29));
            wildgrass.Add(new WildGrass(78, 29));

            #endregion
        }

        private void LoadMap24()
        {
            pbCanvas.BackgroundImage = Resources.ground;

            exit.Add(new ExitPoint(0, 550, 800, 600, 23, 270, 40));
            exit.Add(new ExitPoint(0, 0, 800, 50, 25, 253, 450));

            characters.Add(new Character("Farmer", Resources.character_farmer, 436, 326));
            actionpoint.Add(new ActionPoint(426, 316, 496, 396, -22));

            #region Environment

            buildings.Add(new Buildings(1, 59, 129, true));
            buildings.Add(new Buildings(2, 564, 129, false));
            buildings.Add(new Buildings(0, 319, 129, true));

            #endregion
        }

        private void LoadMap25()
        {
            pbCanvas.BackgroundImage = Resources.ground;

            exit.Add(new ExitPoint(198, 524, 360, 591, 24, 280, 65));
            exit.Add(new ExitPoint(252, 6, 319, 42, 26, 280, 450));

            #region Environment

            structures.Add(new Structures(3, 0, 86, 71));
            structures.Add(new Structures(3, 0, 173, 161));
            structures.Add(new Structures(3, 0, 66, 324));
            structures.Add(new Structures(3, 0, 189, 370));
            structures.Add(new Structures(3, 0, 337, 283));
            structures.Add(new Structures(3, 0, 366, 85));
            structures.Add(new Structures(3, 0, 476, 106));
            structures.Add(new Structures(3, 0, 474, 317));
            structures.Add(new Structures(3, 0, 432, 399));
            structures.Add(new Structures(3, 0, 668, 231));
            structures.Add(new Structures(3, 0, 658, 101));
            structures.Add(new Structures(3, 0, 581, 13));
            structures.Add(new Structures(3, 0, 574, 283));
            structures.Add(new Structures(3, 0, 626, 379));
            structures.Add(new Structures(3, 0, 630, 442));
            structures.Add(new Structures(3, 0, 499, 138));
            structures.Add(new Structures(3, 0, 265, 209));
            structures.Add(new Structures(3, 0, 165, 447));
            structures.Add(new Structures(3, 0, 54, 483));
            structures.Add(new Structures(3, 0, 424, 490));
            structures.Add(new Structures(3, 0, 310, 353));
            structures.Add(new Structures(3, 0, 121, 224));
            structures.Add(new Structures(3, 0, 188, -1));
            structures.Add(new Structures(3, 0, 386, 142));
            structures.Add(new Structures(3, 0, 574, 168));
            structures.Add(new Structures(3, 0, 712, 0));
            structures.Add(new Structures(3, 0, 322, -24));
            structures.Add(new Structures(3, 0, 378, -8));
            structures.Add(new Structures(3, 0, 455, -6));
            structures.Add(new Structures(3, 0, 101, -14));
            structures.Add(new Structures(3, 0, 21, 0));
            structures.Add(new Structures(3, 0, 742, 65));
            structures.Add(new Structures(3, 0, 731, 148));
            structures.Add(new Structures(3, 0, 733, 178));
            structures.Add(new Structures(3, 0, 745, 242));
            structures.Add(new Structures(3, 0, 745, 311));
            structures.Add(new Structures(3, 0, 747, 365));
            structures.Add(new Structures(3, 0, 742, 430));
            structures.Add(new Structures(3, 0, 689, 297));
            structures.Add(new Structures(3, 0, 743, 475));
            structures.Add(new Structures(3, 0, 744, 515));
            structures.Add(new Structures(3, 0, 2, 32));
            structures.Add(new Structures(3, 0, -11, 91));
            structures.Add(new Structures(3, 0, -6, 167));
            structures.Add(new Structures(3, 0, -1, 222));
            structures.Add(new Structures(3, 0, 4, 311));
            structures.Add(new Structures(3, 0, -3, 382));
            structures.Add(new Structures(3, 0, -4, 451));
            structures.Add(new Structures(3, 0, 4, 509));
            structures.Add(new Structures(3, 0, 133, 513));
            structures.Add(new Structures(3, 0, 362, 514));
            structures.Add(new Structures(3, 0, 461, 514));
            structures.Add(new Structures(3, 0, 524, 516));
            structures.Add(new Structures(3, 0, 607, 516));
            structures.Add(new Structures(3, 0, 671, 514));

            #endregion
        }

        private void LoadMap26()
        {
            pbCanvas.BackgroundImage = Resources.grass;

            exit.Add(new ExitPoint(3, 521, 795, 595, 25, 222, 84));
            exit.Add(new ExitPoint(298, 5, 398, 73, 27, 245, 461));

            wildPokemons.Add(new ints(84, 18, 26, 15));
            wildPokemons.Add(new ints(114, 18, 26, 5));
            wildPokemons.Add(new ints(406, 18, 26, 20));
            wildPokemons.Add(new ints(315, 18, 26, 5));
            wildPokemons.Add(new ints(191, 18, 26, 20));
            wildPokemons.Add(new ints(83, 18, 26, 10));
            wildPokemons.Add(new ints(108, 18, 26, 10));
            wildPokemons.Add(new ints(123, 18, 26, 5));
            wildPokemons.Add(new ints(179, 14, 14, 2));
            wildPokemons.Add(new ints(180, 18, 26, 8));

            characters.Add(new Character("Bug Catcher", Resources.character_bugcatcher, 527, 307));
            characters[0].pokemons.Add(new Pokemons(12, 27));
            characters[0].pokemons.Add(new Pokemons(15, 27));
            characters[0].pokemons.Add(new Pokemons(123, 30));
            characters[0].index = 80;
            actionpoint.Add(new ActionPoint(517, 297, 587, 357, 0));

            characters.Add(new Character("Little Boy", Resources.character_littleboy, 716, 183));
            characters[1].pokemons.Add(new Pokemons(84, 25));
            characters[1].pokemons.Add(new Pokemons(83, 27));
            characters[1].index = 81;
            actionpoint.Add(new ActionPoint(706, 173, 776, 233, 1));

            characters.Add(new Character("Little Girl", Resources.character_littlegirl, 740, 322));
            characters[2].pokemons.Add(new Pokemons(192, 20));
            characters[2].pokemons.Add(new Pokemons(315, 24));
            characters[2].pokemons.Add(new Pokemons(114, 28));
            characters[2].index = 82;
            actionpoint.Add(new ActionPoint(730, 312, 800, 372, 2));

            characters.Add(new Character("Little Girl", Resources.character_littlegirl, 405, 131));
            characters[3].pokemons.Add(new Pokemons(179, 14));
            characters[3].pokemons.Add(new Pokemons(180, 22));
            characters[3].index = 83;
            actionpoint.Add(new ActionPoint(395, 121, 465, 181, 3));

            #region Environment

            structures.Add(new Structures(3, 0, 70, 450));
            structures.Add(new Structures(3, 0, 322, 493));
            structures.Add(new Structures(3, 0, 594, 453));

            trees.Add(new Trees(1, 480, -14));
            trees.Add(new Trees(1, 128, -6));
            trees.Add(new Trees(1, 613, 3));
            trees.Add(new Trees(1, 37, 11));
            trees.Add(new Trees(1, 402, 21));
            trees.Add(new Trees(1, 221, 23));
            trees.Add(new Trees(1, 699, 86));
            trees.Add(new Trees(1, 53, 153));
            trees.Add(new Trees(1, 393, 217));
            trees.Add(new Trees(1, 656, 249));
            trees.Add(new Trees(1, 71, 303));

            buildings.Add(new Buildings(2, 487, 92, true));

            flowers.Add(new Flowers(440, 320));
            flowers.Add(new Flowers(555, 270));
            flowers.Add(new Flowers(622, 352));

            wildgrass.Add(new WildGrass(171, 136));
            wildgrass.Add(new WildGrass(211, 136));
            wildgrass.Add(new WildGrass(251, 136));
            wildgrass.Add(new WildGrass(291, 136));
            wildgrass.Add(new WildGrass(331, 136));
            wildgrass.Add(new WildGrass(331, 176));
            wildgrass.Add(new WildGrass(331, 216));
            wildgrass.Add(new WildGrass(331, 256));
            wildgrass.Add(new WildGrass(331, 296));
            wildgrass.Add(new WildGrass(331, 336));
            wildgrass.Add(new WildGrass(331, 376));
            wildgrass.Add(new WildGrass(291, 376));
            wildgrass.Add(new WildGrass(251, 376));
            wildgrass.Add(new WildGrass(211, 376));
            wildgrass.Add(new WildGrass(171, 376));
            wildgrass.Add(new WildGrass(171, 336));
            wildgrass.Add(new WildGrass(171, 296));
            wildgrass.Add(new WildGrass(171, 256));
            wildgrass.Add(new WildGrass(171, 216));
            wildgrass.Add(new WildGrass(171, 176));
            wildgrass.Add(new WildGrass(211, 176));
            wildgrass.Add(new WildGrass(251, 176));
            wildgrass.Add(new WildGrass(291, 176));
            wildgrass.Add(new WildGrass(291, 216));
            wildgrass.Add(new WildGrass(291, 256));
            wildgrass.Add(new WildGrass(291, 296));
            wildgrass.Add(new WildGrass(291, 336));
            wildgrass.Add(new WildGrass(251, 336));
            wildgrass.Add(new WildGrass(211, 336));
            wildgrass.Add(new WildGrass(211, 296));
            wildgrass.Add(new WildGrass(211, 256));
            wildgrass.Add(new WildGrass(211, 216));
            wildgrass.Add(new WildGrass(251, 216));
            wildgrass.Add(new WildGrass(251, 256));
            wildgrass.Add(new WildGrass(251, 296));

            paths.Add(new Paths(8, -10, 445));
            paths.Add(new Paths(8, 30, 445));
            paths.Add(new Paths(8, 70, 445));
            paths.Add(new Paths(8, 110, 445));
            paths.Add(new Paths(8, 150, 445));
            paths.Add(new Paths(8, 190, 445));
            paths.Add(new Paths(8, 230, 445));
            paths.Add(new Paths(8, 270, 445));
            paths.Add(new Paths(8, 310, 445));
            paths.Add(new Paths(8, 350, 445));
            paths.Add(new Paths(8, 390, 445));
            paths.Add(new Paths(8, 430, 445));
            paths.Add(new Paths(8, 470, 445));
            paths.Add(new Paths(8, 510, 445));
            paths.Add(new Paths(8, 550, 445));
            paths.Add(new Paths(8, 590, 445));
            paths.Add(new Paths(8, 630, 445));
            paths.Add(new Paths(8, 670, 445));
            paths.Add(new Paths(8, 710, 445));
            paths.Add(new Paths(8, 750, 445));
            paths.Add(new Paths(8, 790, 445));
            paths.Add(new Paths(5, 750, 485));
            paths.Add(new Paths(5, 790, 485));
            paths.Add(new Paths(5, 790, 525));
            paths.Add(new Paths(5, 790, 565));
            paths.Add(new Paths(5, 750, 565));
            paths.Add(new Paths(5, 710, 565));
            paths.Add(new Paths(5, 670, 565));
            paths.Add(new Paths(5, 630, 565));
            paths.Add(new Paths(5, 590, 565));
            paths.Add(new Paths(5, 550, 565));
            paths.Add(new Paths(5, 510, 565));
            paths.Add(new Paths(5, 470, 565));
            paths.Add(new Paths(5, 430, 565));
            paths.Add(new Paths(5, 390, 565));
            paths.Add(new Paths(5, 350, 565));
            paths.Add(new Paths(5, 310, 565));
            paths.Add(new Paths(5, 270, 565));
            paths.Add(new Paths(5, 230, 565));
            paths.Add(new Paths(5, 190, 565));
            paths.Add(new Paths(5, 110, 565));
            paths.Add(new Paths(5, 70, 565));
            paths.Add(new Paths(5, 30, 565));
            paths.Add(new Paths(5, -10, 565));
            paths.Add(new Paths(5, -10, 525));
            paths.Add(new Paths(5, -10, 485));
            paths.Add(new Paths(5, 30, 485));
            paths.Add(new Paths(5, 30, 525));
            paths.Add(new Paths(5, 70, 525));
            paths.Add(new Paths(5, 110, 525));
            paths.Add(new Paths(5, 150, 525));
            paths.Add(new Paths(5, 230, 525));
            paths.Add(new Paths(5, 270, 525));
            paths.Add(new Paths(5, 310, 525));
            paths.Add(new Paths(5, 350, 525));
            paths.Add(new Paths(5, 390, 525));
            paths.Add(new Paths(5, 430, 525));
            paths.Add(new Paths(5, 470, 525));
            paths.Add(new Paths(5, 510, 525));
            paths.Add(new Paths(5, 550, 525));
            paths.Add(new Paths(5, 590, 525));
            paths.Add(new Paths(5, 630, 525));
            paths.Add(new Paths(5, 670, 525));
            paths.Add(new Paths(5, 710, 525));
            paths.Add(new Paths(5, 750, 525));
            paths.Add(new Paths(5, 710, 485));
            paths.Add(new Paths(5, 670, 485));
            paths.Add(new Paths(5, 630, 485));
            paths.Add(new Paths(5, 590, 485));
            paths.Add(new Paths(5, 550, 485));
            paths.Add(new Paths(5, 510, 485));
            paths.Add(new Paths(5, 470, 485));
            paths.Add(new Paths(5, 430, 485));
            paths.Add(new Paths(5, 390, 485));
            paths.Add(new Paths(5, 350, 485));
            paths.Add(new Paths(5, 310, 485));
            paths.Add(new Paths(5, 270, 485));
            paths.Add(new Paths(5, 230, 485));
            paths.Add(new Paths(5, 190, 485));
            paths.Add(new Paths(5, 150, 485));
            paths.Add(new Paths(5, 110, 485));
            paths.Add(new Paths(5, 70, 485));
            paths.Add(new Paths(5, 190, 525));
            paths.Add(new Paths(5, 150, 565));

            #endregion
        }

        private void LoadMap27()
        {
            pbCanvas.BackgroundImage = Resources.grass;

            exit.Add(new ExitPoint(222, 535, 358, 592, 26, 314, 72));
            exit.Add(new ExitPoint(306, 3, 517, 45, 28, 321, 497));

            wildPokemons.Add(new ints(102, 18, 26, 10));
            wildPokemons.Add(new ints(438, 14, 16, 20));
            wildPokemons.Add(new ints(185, 18, 26, 5));
            wildPokemons.Add(new ints(333, 18, 26, 5));
            wildPokemons.Add(new ints(352, 18, 26, 15));
            wildPokemons.Add(new ints(357, 18, 26, 3));
            wildPokemons.Add(new ints(175, 18, 26, 7));
            wildPokemons.Add(new ints(285, 18, 22, 25));
            wildPokemons.Add(new ints(286, 23, 26, 10));

            characters.Add(new Character("Bird Observer", Resources.character_bugcatcher2, 119, 139));
            characters[0].pokemons.Add(new Pokemons(333, 26));
            characters[0].pokemons.Add(new Pokemons(17, 28));
            characters[0].pokemons.Add(new Pokemons(277, 32));
            characters[0].index = 84;
            actionpoint.Add(new ActionPoint(109, 129, 179, 189, 0));

            characters.Add(new Character("Kid", Resources.character_kid, 568, 93));
            characters[1].pokemons.Add(new Pokemons(102, 24));
            characters[1].pokemons.Add(new Pokemons(185, 33));
            characters[1].index = 85;
            actionpoint.Add(new ActionPoint(558, 83, 628, 143, 1));

            characters.Add(new Character("Little Girl", Resources.character_littlegirl, 370, 359));
            characters[2].pokemons.Add(new Pokemons(175, 18));
            characters[2].index = 86;
            actionpoint.Add(new ActionPoint(360, 349, 430, 409, 2));

            characters.Add(new Character("Fighter", Resources.character_fighter2, 135, 352));
            characters[3].pokemons.Add(new Pokemons(285, 19));
            characters[3].pokemons.Add(new Pokemons(286, 35));
            characters[3].index = 87;
            actionpoint.Add(new ActionPoint(125, 342, 195, 402, 3));

            #region Environment

            trees.Add(new Trees(1, 538, 2));
            trees.Add(new Trees(1, 219, 2));
            trees.Add(new Trees(1, 12, 3));
            trees.Add(new Trees(1, 666, 46));
            trees.Add(new Trees(1, 410, 50));
            trees.Add(new Trees(1, 177, 113));
            trees.Add(new Trees(1, 21, 128));
            trees.Add(new Trees(1, 517, 175));
            trees.Add(new Trees(1, 653, 178));
            trees.Add(new Trees(1, 50, 260));
            trees.Add(new Trees(1, 397, 263));
            trees.Add(new Trees(1, 694, 284));
            trees.Add(new Trees(1, 207, 326));
            trees.Add(new Trees(1, 595, 361));
            trees.Add(new Trees(1, 56, 401));
            trees.Add(new Trees(1, 455, 401));
            trees.Add(new Trees(1, 691, 476));
            trees.Add(new Trees(1, 140, 493));
            trees.Add(new Trees(1, 356, 496));
            trees.Add(new Trees(1, 535, 502));

            flowers.Add(new Flowers(166, 417));
            flowers.Add(new Flowers(586, 460));
            flowers.Add(new Flowers(722, 424));
            flowers.Add(new Flowers(150, 315));
            flowers.Add(new Flowers(123, 205));
            flowers.Add(new Flowers(514, 114));
            flowers.Add(new Flowers(619, 122));
            flowers.Add(new Flowers(608, 228));
            flowers.Add(new Flowers(428, 369));
            flowers.Add(new Flowers(494, 49));
            flowers.Add(new Flowers(630, 29));
            flowers.Add(new Flowers(23, 507));

            wildgrass.Add(new WildGrass(188, 217));
            wildgrass.Add(new WildGrass(228, 217));
            wildgrass.Add(new WildGrass(228, 257));
            wildgrass.Add(new WildGrass(188, 257));
            wildgrass.Add(new WildGrass(373, 158));
            wildgrass.Add(new WildGrass(413, 158));
            wildgrass.Add(new WildGrass(453, 158));
            wildgrass.Add(new WildGrass(453, 198));
            wildgrass.Add(new WildGrass(413, 198));
            wildgrass.Add(new WildGrass(373, 198));
            wildgrass.Add(new WildGrass(515, 286));
            wildgrass.Add(new WildGrass(555, 286));
            wildgrass.Add(new WildGrass(595, 286));
            wildgrass.Add(new WildGrass(595, 326));
            wildgrass.Add(new WildGrass(555, 326));
            wildgrass.Add(new WildGrass(515, 326));
            wildgrass.Add(new WildGrass(104, 45));
            wildgrass.Add(new WildGrass(144, 45));
            wildgrass.Add(new WildGrass(144, 85));
            wildgrass.Add(new WildGrass(104, 85));

            #endregion
        }

        private void LoadMap28()
        {
            pbCanvas.BackgroundImage = Resources.grass;

            exit.Add(new ExitPoint(311, 525, 462, 590, 27, 355, 31));
            exit.Add(new ExitPoint(252, 5, 450, 52, 29, 325, 502));

            wildPokemons.Add(new ints(234, 18, 26, 80));
            wildPokemons.Add(new ints(420, 18, 24, 10));
            wildPokemons.Add(new ints(421, 25, 26, 5));
            wildPokemons.Add(new ints(433, 18, 26, 5));

            #region Environment

            trees.Add(new Trees(3, 567, -15));
            trees.Add(new Trees(3, 727, -15));
            trees.Add(new Trees(3, 647, -15));
            trees.Add(new Trees(3, 487, -15));
            trees.Add(new Trees(3, 407, -15));
            trees.Add(new Trees(3, 167, -15));
            trees.Add(new Trees(3, 87, -15));
            trees.Add(new Trees(3, 7, -15));
            trees.Add(new Trees(3, 567, 45));
            trees.Add(new Trees(3, 727, 45));
            trees.Add(new Trees(3, 647, 45));
            trees.Add(new Trees(3, 487, 45));
            trees.Add(new Trees(3, 407, 45));
            trees.Add(new Trees(3, 167, 45));
            trees.Add(new Trees(3, 7, 45));
            trees.Add(new Trees(3, 87, 45));
            trees.Add(new Trees(3, 567, 105));
            trees.Add(new Trees(3, 727, 105));
            trees.Add(new Trees(3, 647, 105));
            trees.Add(new Trees(3, 487, 105));
            trees.Add(new Trees(3, 407, 105));
            trees.Add(new Trees(3, 167, 105));
            trees.Add(new Trees(3, 7, 105));
            trees.Add(new Trees(3, 87, 105));
            trees.Add(new Trees(3, 567, 165));
            trees.Add(new Trees(3, 727, 165));
            trees.Add(new Trees(3, 647, 165));
            trees.Add(new Trees(3, 487, 165));
            trees.Add(new Trees(3, 407, 165));
            trees.Add(new Trees(3, 167, 165));
            trees.Add(new Trees(3, 87, 165));
            trees.Add(new Trees(3, 7, 165));
            trees.Add(new Trees(3, 567, 225));
            trees.Add(new Trees(3, 647, 225));
            trees.Add(new Trees(3, 727, 225));
            trees.Add(new Trees(3, 487, 225));
            trees.Add(new Trees(3, 407, 225));
            trees.Add(new Trees(3, 7, 225));
            trees.Add(new Trees(3, 87, 225));
            trees.Add(new Trees(3, 167, 225));
            trees.Add(new Trees(1, 694, 331));
            trees.Add(new Trees(1, 22, 371));
            trees.Add(new Trees(1, 158, 379));
            trees.Add(new Trees(1, 450, 380));
            trees.Add(new Trees(1, 589, 400));
            trees.Add(new Trees(1, 112, 481));
            trees.Add(new Trees(1, 695, 489));
            trees.Add(new Trees(1, 225, 513));
            trees.Add(new Trees(1, 460, 514));

            wildgrass.Add(new WildGrass(265, 60));
            wildgrass.Add(new WildGrass(305, 60));
            wildgrass.Add(new WildGrass(345, 60));
            wildgrass.Add(new WildGrass(265, 100));
            wildgrass.Add(new WildGrass(305, 100));
            wildgrass.Add(new WildGrass(345, 100));
            wildgrass.Add(new WildGrass(265, 140));
            wildgrass.Add(new WildGrass(305, 140));
            wildgrass.Add(new WildGrass(345, 140));
            wildgrass.Add(new WildGrass(265, 180));
            wildgrass.Add(new WildGrass(305, 180));
            wildgrass.Add(new WildGrass(345, 180));

            #endregion
        }

        private void LoadMap29()
        {
            pbCanvas.BackgroundImage = Resources.snow;

            exit.Add(new ExitPoint(245, 550, 393, 593, 28, 313, 70));
            exit.Add(new ExitPoint(241, 2, 397, 51, 30, 325, 480));

            wildPokemons.Add(new ints(238, 18, 26, 20));
            wildPokemons.Add(new ints(215, 18, 26, 10));
            wildPokemons.Add(new ints(459, 18, 26, 30));
            wildPokemons.Add(new ints(359, 18, 26, 5));
            wildPokemons.Add(new ints(262, 18, 26, 20));
            wildPokemons.Add(new ints(174, 18, 26, 5));
            wildPokemons.Add(new ints(198, 18, 26, 10));

            #region Environment

            trees.Add(new Trees(3, 0, -20));
            trees.Add(new Trees(3, 80, -20));
            trees.Add(new Trees(3, 160, -20));
            trees.Add(new Trees(3, 400, -20));
            trees.Add(new Trees(3, 480, -20));
            trees.Add(new Trees(3, 560, -20));
            trees.Add(new Trees(3, 640, -20));
            trees.Add(new Trees(3, 720, -20));
            trees.Add(new Trees(3, 0, 40));
            trees.Add(new Trees(3, 720, 40));
            trees.Add(new Trees(3, 198, 80));
            trees.Add(new Trees(3, 640, 94));
            trees.Add(new Trees(3, 0, 100));
            trees.Add(new Trees(3, 720, 100));
            trees.Add(new Trees(3, 372, 100));
            trees.Add(new Trees(3, 0, 160));
            trees.Add(new Trees(3, 720, 160));
            trees.Add(new Trees(3, 0, 220));
            trees.Add(new Trees(3, 720, 220));
            trees.Add(new Trees(3, 191, 225));
            trees.Add(new Trees(3, 384, 234));
            trees.Add(new Trees(3, 0, 280));
            trees.Add(new Trees(3, 720, 280));
            trees.Add(new Trees(3, 0, 340));
            trees.Add(new Trees(3, 720, 340));
            trees.Add(new Trees(3, 212, 396));
            trees.Add(new Trees(3, 0, 400));
            trees.Add(new Trees(3, 720, 400));
            trees.Add(new Trees(3, 378, 400));
            trees.Add(new Trees(3, 0, 460));
            trees.Add(new Trees(3, 720, 460));
            trees.Add(new Trees(3, 0, 520));
            trees.Add(new Trees(3, 80, 520));
            trees.Add(new Trees(3, 160, 520));
            trees.Add(new Trees(3, 400, 520));
            trees.Add(new Trees(3, 480, 520));
            trees.Add(new Trees(3, 560, 520));
            trees.Add(new Trees(3, 640, 520));
            trees.Add(new Trees(3, 720, 520));

            buildings.Add(new Buildings(1, 471, 133, true));
            buildings.Add(new Buildings(0, 474, 323, true));

            wildgrass.Add(new WildGrass(93, 120));
            wildgrass.Add(new WildGrass(133, 120));
            wildgrass.Add(new WildGrass(133, 160));
            wildgrass.Add(new WildGrass(93, 160));
            wildgrass.Add(new WildGrass(93, 200));
            wildgrass.Add(new WildGrass(133, 200));
            wildgrass.Add(new WildGrass(133, 240));
            wildgrass.Add(new WildGrass(93, 240));
            wildgrass.Add(new WildGrass(93, 280));
            wildgrass.Add(new WildGrass(133, 280));
            wildgrass.Add(new WildGrass(133, 320));
            wildgrass.Add(new WildGrass(93, 320));
            wildgrass.Add(new WildGrass(93, 360));
            wildgrass.Add(new WildGrass(133, 360));
            wildgrass.Add(new WildGrass(133, 400));
            wildgrass.Add(new WildGrass(93, 400));
            wildgrass.Add(new WildGrass(93, 440));
            wildgrass.Add(new WildGrass(133, 440));
            wildgrass.Add(new WildGrass(133, 480));
            wildgrass.Add(new WildGrass(93, 480));
            wildgrass.Add(new WildGrass(93, 80));
            wildgrass.Add(new WildGrass(133, 80));

            hills.Add(new Hills(224, 289, 183));
            hills.Add(new Hills(242, 436, 177));
            hills.Add(new Hills(248, 133, 154));

            #endregion
        }

        private void LoadMap30()
        {
            pbCanvas.BackgroundImage = Resources.snow;

            exit.Add(new ExitPoint(195, 516, 442, 592, 29, 337, 55));

            buildings.Add(new Buildings(-1, 489, 125, true));

            if (trainerDefeated[88] && trainerDefeated[89] && trainerDefeated[90]
                && trainerDefeated[91] && trainerDefeated[92])
            {
                mainForm.ShowMessage("There seems to be something under the snow...");
                buildings.Add(new Buildings(-1, 489, 125, true));
            }
            else if (trainerDefeated[93])
            {
                characters.Add(new Character("Team Rocket Member", Resources.character_teamrocket, 163, 301));
                characters[0].pokemons.Add(new Pokemons(229, 24));
                characters[0].pokemons.Add(new Pokemons(198, 26));
                characters[0].pokemons.Add(new Pokemons(359, 30));
                characters[0].index = 88;
                actionpoint.Add(new ActionPoint(153, 291, 223, 351, 0));

                characters.Add(new Character("Team Rocket Member", Resources.character_teamrocket, 535, 366));
                characters[1].pokemons.Add(new Pokemons(229, 25));
                characters[1].pokemons.Add(new Pokemons(461, 31));
                characters[1].index = 89;
                actionpoint.Add(new ActionPoint(525, 356, 595, 416, 1));

                characters.Add(new Character("Team Rocket Member", Resources.character_teamrocket2, 299, 259));
                characters[2].pokemons.Add(new Pokemons(262, 22));
                characters[2].pokemons.Add(new Pokemons(435, 34));
                characters[2].index = 90;
                actionpoint.Add(new ActionPoint(289, 249, 359, 309, 2));

                characters.Add(new Character("Team Rocket Member", Resources.character_teamrocket2, 209, 122));
                characters[3].pokemons.Add(new Pokemons(262, 26));
                characters[3].pokemons.Add(new Pokemons(430, 28));
                characters[3].pokemons.Add(new Pokemons(302, 29));
                characters[3].index = 91;
                actionpoint.Add(new ActionPoint(199, 112, 269, 172, 3));

                characters.Add(new Character("Team Rocket Member", Resources.character_teamrocket2, 409, 202));
                characters[4].pokemons.Add(new Pokemons(452, 40));
                characters[4].index = 92;
                actionpoint.Add(new ActionPoint(399, 192, 469, 252, 4));
            }

            #region Environment

            structures.Add(new Structures(1, 1, 2, -1, 31));
            structures.Add(new Structures(1, 1, 2, 39, 31));
            structures.Add(new Structures(1, 1, 2, 79, 31));
            structures.Add(new Structures(1, 1, 2, 119, 31));
            structures.Add(new Structures(1, 1, 2, 159, 31));
            structures.Add(new Structures(1, 1, 2, 199, 31));
            structures.Add(new Structures(1, 1, 2, 239, 31));
            structures.Add(new Structures(1, 1, 2, 279, 31));
            structures.Add(new Structures(1, 1, 2, 319, 31));
            structures.Add(new Structures(1, 1, 2, 359, 31));
            structures.Add(new Structures(1, 1, 2, 399, 31));
            structures.Add(new Structures(1, 1, 2, 439, 31));
            structures.Add(new Structures(1, 1, 2, 479, 31));
            structures.Add(new Structures(1, 1, 2, 519, 31));
            structures.Add(new Structures(1, 1, 2, 559, 31));
            structures.Add(new Structures(1, 1, 2, 599, 31));
            structures.Add(new Structures(1, 1, 2, 639, 31));
            structures.Add(new Structures(1, 1, 2, 679, 31));
            structures.Add(new Structures(1, 1, 2, 719, 31));
            structures.Add(new Structures(1, 1, 2, 759, 31));
            structures.Add(new Structures(1, 1, 5, 759, -9));
            structures.Add(new Structures(1, 1, 5, 719, -9));
            structures.Add(new Structures(1, 1, 5, 679, -9));
            structures.Add(new Structures(1, 1, 5, 639, -9));
            structures.Add(new Structures(1, 1, 5, 599, -9));
            structures.Add(new Structures(1, 1, 5, 559, -9));
            structures.Add(new Structures(1, 1, 5, 519, -9));
            structures.Add(new Structures(1, 1, 5, 479, -9));
            structures.Add(new Structures(1, 1, 5, 439, -9));
            structures.Add(new Structures(1, 1, 5, 399, -9));
            structures.Add(new Structures(1, 1, 5, 359, -9));
            structures.Add(new Structures(1, 1, 5, 319, -9));
            structures.Add(new Structures(1, 1, 5, 279, -9));
            structures.Add(new Structures(1, 1, 5, 239, -9));
            structures.Add(new Structures(1, 1, 5, 199, -9));
            structures.Add(new Structures(1, 1, 5, 159, -9));
            structures.Add(new Structures(1, 1, 5, 119, -9));
            structures.Add(new Structures(1, 1, 5, 79, -9));
            structures.Add(new Structures(1, 1, 5, 39, -9));
            structures.Add(new Structures(1, 1, 5, -1, -9));
            structures.Add(new Structures(3, 0, -6, -44));
            structures.Add(new Structures(3, 0, 755, -52));

            trees.Add(new Trees(3, -5, 91));
            trees.Add(new Trees(3, 715, 91));
            trees.Add(new Trees(3, -5, 151));
            trees.Add(new Trees(3, 715, 151));
            trees.Add(new Trees(3, -5, 211));
            trees.Add(new Trees(3, 715, 211));
            trees.Add(new Trees(3, -5, 271));
            trees.Add(new Trees(3, 715, 271));
            trees.Add(new Trees(3, -5, 331));
            trees.Add(new Trees(3, 715, 331));
            trees.Add(new Trees(3, -5, 391));
            trees.Add(new Trees(3, 715, 391));
            trees.Add(new Trees(3, -5, 451));
            trees.Add(new Trees(3, 75, 451));
            trees.Add(new Trees(3, 155, 451));
            trees.Add(new Trees(3, 395, 451));
            trees.Add(new Trees(3, 475, 451));
            trees.Add(new Trees(3, 555, 451));
            trees.Add(new Trees(3, 635, 451));
            trees.Add(new Trees(3, 715, 451));
            trees.Add(new Trees(3, -5, 511));
            trees.Add(new Trees(3, 75, 511));
            trees.Add(new Trees(3, 155, 511));
            trees.Add(new Trees(3, 395, 511));
            trees.Add(new Trees(3, 475, 511));
            trees.Add(new Trees(3, 555, 511));
            trees.Add(new Trees(3, 635, 511));
            trees.Add(new Trees(3, 715, 511));

            #endregion
        }

        private void LoadMap31()
        {
            pbCanvas.BackgroundImage = Resources.waterwaves;

            exit.Add(new ExitPoint(5, 556, 792, 592, 30, 396, 100));

            wildPokemons.Add(new ints(86, 18, 26, 40));
            wildPokemons.Add(new ints(131, 18, 26, 5));
            wildPokemons.Add(new ints(363, 18, 26, 55));

            #region Environment

            structures.Add(new Structures(3, 0, 25, 470));
            structures.Add(new Structures(3, 0, 25, 301));
            structures.Add(new Structures(3, 0, 33, 116));
            structures.Add(new Structures(3, 0, 180, 8));
            structures.Add(new Structures(3, 0, 395, 6));
            structures.Add(new Structures(3, 0, 602, 20));
            structures.Add(new Structures(3, 0, 659, 103));
            structures.Add(new Structures(3, 0, 691, 210));
            structures.Add(new Structures(3, 0, 709, 326));
            structures.Add(new Structures(3, 0, 704, 458));
            structures.Add(new Structures(3, 0, 39, 355));
            structures.Add(new Structures(3, 0, 50, 186));
            structures.Add(new Structures(3, 0, 55, -6));
            structures.Add(new Structures(3, 0, 235, 79));
            structures.Add(new Structures(3, 0, 478, 101));
            structures.Add(new Structures(3, 0, 137, 236));
            structures.Add(new Structures(3, 0, 11, 524));
            structures.Add(new Structures(3, 0, 733, 512));

            LoadWater();

            #endregion
        }

        private void LoadMap32()
        {
            pbCanvas.BackgroundImage = Resources.ground;

            exit.Add(new ExitPoint(75, 537, 740, 588, 34, 401, 34));

            wildPokemons.Add(new ints(449, 22, 30, 40));
            wildPokemons.Add(new ints(343, 22, 30, 12));
            wildPokemons.Add(new ints(451, 22, 30, 3));
            wildPokemons.Add(new ints(111, 22, 30, 3));
            wildPokemons.Add(new ints(443, 22, 24, 1));
            wildPokemons.Add(new ints(371, 22, 29, 1));
            wildPokemons.Add(new ints(228, 22, 24, 30));
            wildPokemons.Add(new ints(229, 25, 30, 10));

            #region Environment

            structures.Add(new Structures(3, 0, 21, 5));
            structures.Add(new Structures(3, 0, 712, 4));
            structures.Add(new Structures(3, 0, 303, -40));
            structures.Add(new Structures(3, 0, 564, -48));
            structures.Add(new Structures(3, 0, 145, -46));
            structures.Add(new Structures(3, 0, 733, 154));
            structures.Add(new Structures(3, 0, 734, 296));
            structures.Add(new Structures(3, 0, 729, 443));
            structures.Add(new Structures(3, 0, -6, 151));
            structures.Add(new Structures(3, 0, 5, 307));
            structures.Add(new Structures(3, 0, 22, 438));
            structures.Add(new Structures(3, 0, 12, 537));
            structures.Add(new Structures(3, 0, 735, 523));
            structures.Add(new Structures(3, 0, 501, 347));
            structures.Add(new Structures(3, 0, 264, 248));
            structures.Add(new Structures(3, 0, 233, 400));

            buildings.Add(new Buildings(-1, 101, 118, true));

            wildgrass.Add(new WildGrass(106, 283));
            wildgrass.Add(new WildGrass(146, 283));
            wildgrass.Add(new WildGrass(186, 283));
            wildgrass.Add(new WildGrass(186, 323));
            wildgrass.Add(new WildGrass(186, 363));
            wildgrass.Add(new WildGrass(186, 403));
            wildgrass.Add(new WildGrass(186, 443));
            wildgrass.Add(new WildGrass(106, 443));
            wildgrass.Add(new WildGrass(106, 403));
            wildgrass.Add(new WildGrass(106, 363));
            wildgrass.Add(new WildGrass(106, 323));
            wildgrass.Add(new WildGrass(146, 323));
            wildgrass.Add(new WildGrass(146, 363));
            wildgrass.Add(new WildGrass(146, 403));
            wildgrass.Add(new WildGrass(146, 443));
            wildgrass.Add(new WildGrass(666, 443));
            wildgrass.Add(new WildGrass(666, 403));
            wildgrass.Add(new WildGrass(626, 403));
            wildgrass.Add(new WildGrass(586, 403));
            wildgrass.Add(new WildGrass(546, 403));
            wildgrass.Add(new WildGrass(506, 403));
            wildgrass.Add(new WildGrass(466, 403));
            wildgrass.Add(new WildGrass(426, 403));
            wildgrass.Add(new WildGrass(386, 403));
            wildgrass.Add(new WildGrass(346, 403));
            wildgrass.Add(new WildGrass(306, 403));
            wildgrass.Add(new WildGrass(306, 443));
            wildgrass.Add(new WildGrass(346, 443));
            wildgrass.Add(new WildGrass(386, 443));
            wildgrass.Add(new WildGrass(426, 443));
            wildgrass.Add(new WildGrass(466, 443));
            wildgrass.Add(new WildGrass(506, 443));
            wildgrass.Add(new WildGrass(546, 443));
            wildgrass.Add(new WildGrass(586, 443));
            wildgrass.Add(new WildGrass(626, 443));
            wildgrass.Add(new WildGrass(346, 323));
            wildgrass.Add(new WildGrass(346, 283));
            wildgrass.Add(new WildGrass(386, 283));
            wildgrass.Add(new WildGrass(426, 283));
            wildgrass.Add(new WildGrass(466, 283));
            wildgrass.Add(new WildGrass(506, 283));
            wildgrass.Add(new WildGrass(546, 283));
            wildgrass.Add(new WildGrass(586, 283));
            wildgrass.Add(new WildGrass(626, 283));
            wildgrass.Add(new WildGrass(666, 283));
            wildgrass.Add(new WildGrass(666, 323));
            wildgrass.Add(new WildGrass(626, 323));
            wildgrass.Add(new WildGrass(586, 323));
            wildgrass.Add(new WildGrass(546, 323));
            wildgrass.Add(new WildGrass(506, 323));
            wildgrass.Add(new WildGrass(466, 323));
            wildgrass.Add(new WildGrass(426, 323));
            wildgrass.Add(new WildGrass(386, 323));

            hills.Add(new Hills(270, 64, 248));
            hills.Add(new Hills(403, 141, 190));
            hills.Add(new Hills(497, 243, 211));

            #endregion
        }

        private void LoadMap33()
        {
            pbCanvas.BackgroundImage = Resources.ancientcave;

            exit.Add(new ExitPoint(399, 102, 473, 170, 34, 605, 305));

            mainForm.ShowMessage("This can't be good...");

            blockedzone.Add(new BlockedZone(1, 0, 790, 100));
            blockedzone.Add(new BlockedZone(481, 89, 787, 189));
            blockedzone.Add(new BlockedZone(629, 192, 792, 232));
            blockedzone.Add(new BlockedZone(364, 230, 791, 255));
            blockedzone.Add(new BlockedZone(682, 258, 790, 593));
            blockedzone.Add(new BlockedZone(657, 300, 678, 587));
            blockedzone.Add(new BlockedZone(607, 377, 649, 583));
            blockedzone.Add(new BlockedZone(598, 577, 3, 593));
            blockedzone.Add(new BlockedZone(1, 571, 602, 592));
            blockedzone.Add(new BlockedZone(5, 499, 289, 562));
            blockedzone.Add(new BlockedZone(0, 102, 112, 492));
            blockedzone.Add(new BlockedZone(116, 102, 236, 374));
            blockedzone.Add(new BlockedZone(120, 375, 164, 399));
            blockedzone.Add(new BlockedZone(116, 400, 516, 423));
            blockedzone.Add(new BlockedZone(483, 312, 519, 415));
            blockedzone.Add(new BlockedZone(450, 310, 486, 346));
            blockedzone.Add(new BlockedZone(354, 244, 392, 339));
            blockedzone.Add(new BlockedZone(237, 100, 300, 218));
            blockedzone.Add(new BlockedZone(319, 102, 391, 145));

            wildPokemons.Add(new ints(138, 5, 10, 25));
            wildPokemons.Add(new ints(140, 5, 10, 25));
            wildPokemons.Add(new ints(369, 5, 10, 50));

            #region Environment

            wildgrass.Add(new WildGrass(397, 485));
            wildgrass.Add(new WildGrass(437, 485));
            wildgrass.Add(new WildGrass(477, 485));
            wildgrass.Add(new WildGrass(477, 525));
            wildgrass.Add(new WildGrass(437, 525));
            wildgrass.Add(new WildGrass(397, 525));
            wildgrass.Add(new WildGrass(357, 525));
            wildgrass.Add(new WildGrass(517, 525));

            #endregion
        }

        private void LoadMap34()
        {
            pbCanvas.BackgroundImage = Resources.grass;

            exit.Add(new ExitPoint(274, 3, 463, 57, 32, 381, 454));
            exit.Add(new ExitPoint(262, 539, 475, 590, 35, 350, 50));

            wildPokemons.Add(new ints(415, 18, 20, 55));
            wildPokemons.Add(new ints(336, 22, 30, 20));
            wildPokemons.Add(new ints(335, 22, 30, 20));
            wildPokemons.Add(new ints(96, 22, 30, 5));

            characters.Add(new Character("Fighter", Resources.character_fighter2, 225, 368));
            characters[0].pokemons.Add(new Pokemons(23, 21));
            characters[0].pokemons.Add(new Pokemons(24, 24));
            characters[0].pokemons.Add(new Pokemons(336, 30));
            characters[0].index = 94;
            actionpoint.Add(new ActionPoint(215, 358, 285, 418, 0));

            characters.Add(new Character("Fighter", Resources.character_fighter2, 478, 368));
            characters[1].pokemons.Add(new Pokemons(216, 25));
            characters[1].pokemons.Add(new Pokemons(217, 30));
            characters[1].pokemons.Add(new Pokemons(335, 31));
            characters[1].index = 95;
            actionpoint.Add(new ActionPoint(468, 358, 538, 418, 1));

            characters.Add(new Character("Little Boy", Resources.character_littleboy, 93, 301));
            characters[2].pokemons.Add(new Pokemons(415, 19));
            characters[2].pokemons.Add(new Pokemons(415, 20));
            characters[2].pokemons.Add(new Pokemons(415, 19));
            characters[2].index = 96;
            actionpoint.Add(new ActionPoint(83, 291, 153, 351, 2));

            characters.Add(new Character("Little Boy", Resources.character_littleboy, 455, 134));
            characters[3].pokemons.Add(new Pokemons(343, 29));
            characters[3].pokemons.Add(new Pokemons(344, 36));
            characters[3].index = 97;
            actionpoint.Add(new ActionPoint(445, 124, 515, 184, 3));

            characters.Add(new Character("Little Girl", Resources.character_littlegirl, 32, 118));
            characters[4].pokemons.Add(new Pokemons(329, 35));
            characters[4].pokemons.Add(new Pokemons(328, 32));
            characters[4].index = 98;
            actionpoint.Add(new ActionPoint(22, 108, 92, 168, 4));

            #region Environment

            structures.Add(new Structures(3, 0, 736, 293));
            structures.Add(new Structures(3, 0, 739, 175));
            structures.Add(new Structures(3, 0, 527, 304));
            structures.Add(new Structures(3, 0, 654, 327));
            structures.Add(new Structures(3, 0, 525, 159));
            structures.Add(new Structures(3, 0, 665, 53));
            structures.Add(new Structures(3, 0, 561, 23));
            structures.Add(new Structures(3, 0, 446, -5));
            structures.Add(new Structures(3, 0, 721, -24));
            structures.Add(new Structures(3, 0, 221, -30));
            structures.Add(new Structures(3, 0, 37, 12));
            structures.Add(new Structures(3, 0, 137, 51));
            structures.Add(new Structures(3, 0, 132, -31));
            structures.Add(new Structures(3, 0, 257, 32));

            trees.Add(new Trees(1, 62, 180));
            trees.Add(new Trees(1, 168, 236));
            trees.Add(new Trees(1, 2, 318));
            trees.Add(new Trees(1, 731, 386));
            trees.Add(new Trees(1, 123, 400));
            trees.Add(new Trees(1, 584, 409));
            trees.Add(new Trees(1, 20, 499));
            trees.Add(new Trees(1, 676, 499));
            trees.Add(new Trees(1, 471, 508));
            trees.Add(new Trees(1, 181, 513));

            buildings.Add(new Buildings(-1, 570, 184, false));

            flowers.Add(new Flowers(303, 525));
            flowers.Add(new Flowers(505, 437));
            flowers.Add(new Flowers(619, 542));
            flowers.Add(new Flowers(59, 452));
            flowers.Add(new Flowers(125, 336));

            wildgrass.Add(new WildGrass(288, 192));
            wildgrass.Add(new WildGrass(328, 192));
            wildgrass.Add(new WildGrass(368, 192));
            wildgrass.Add(new WildGrass(408, 192));
            wildgrass.Add(new WildGrass(408, 232));
            wildgrass.Add(new WildGrass(408, 272));
            wildgrass.Add(new WildGrass(408, 312));
            wildgrass.Add(new WildGrass(408, 352));
            wildgrass.Add(new WildGrass(408, 392));
            wildgrass.Add(new WildGrass(368, 392));
            wildgrass.Add(new WildGrass(328, 392));
            wildgrass.Add(new WildGrass(288, 392));
            wildgrass.Add(new WildGrass(288, 352));
            wildgrass.Add(new WildGrass(288, 312));
            wildgrass.Add(new WildGrass(288, 272));
            wildgrass.Add(new WildGrass(288, 232));
            wildgrass.Add(new WildGrass(328, 232));
            wildgrass.Add(new WildGrass(368, 232));
            wildgrass.Add(new WildGrass(368, 272));
            wildgrass.Add(new WildGrass(368, 312));
            wildgrass.Add(new WildGrass(368, 352));
            wildgrass.Add(new WildGrass(328, 352));
            wildgrass.Add(new WildGrass(328, 312));
            wildgrass.Add(new WildGrass(328, 272));

            paths.Add(new Paths(2, 37, 68));
            paths.Add(new Paths(2, -3, 68));
            paths.Add(new Paths(2, 77, 68));
            paths.Add(new Paths(2, 117, 68));
            paths.Add(new Paths(2, 157, 68));
            paths.Add(new Paths(2, 197, 68));
            paths.Add(new Paths(2, 237, 68));
            paths.Add(new Paths(2, 277, 68));
            paths.Add(new Paths(2, 317, 68));
            paths.Add(new Paths(2, 357, 68));
            paths.Add(new Paths(2, 397, 68));
            paths.Add(new Paths(2, 437, 68));
            paths.Add(new Paths(2, 477, 68));
            paths.Add(new Paths(2, 517, 68));
            paths.Add(new Paths(2, 557, 68));
            paths.Add(new Paths(2, 597, 68));
            paths.Add(new Paths(2, 637, 68));
            paths.Add(new Paths(2, 677, 68));
            paths.Add(new Paths(2, 717, 68));
            paths.Add(new Paths(2, 757, 68));
            paths.Add(new Paths(2, 797, 68));
            paths.Add(new Paths(5, 797, 28));
            paths.Add(new Paths(5, 797, -12));
            paths.Add(new Paths(5, 757, -12));
            paths.Add(new Paths(5, 717, -12));
            paths.Add(new Paths(5, 677, -12));
            paths.Add(new Paths(5, 637, -12));
            paths.Add(new Paths(5, 597, -12));
            paths.Add(new Paths(5, 557, -12));
            paths.Add(new Paths(5, 517, -12));
            paths.Add(new Paths(5, 477, -12));
            paths.Add(new Paths(5, 437, -12));
            paths.Add(new Paths(5, 397, -12));
            paths.Add(new Paths(5, 357, -12));
            paths.Add(new Paths(5, 317, -12));
            paths.Add(new Paths(5, 277, -12));
            paths.Add(new Paths(5, 237, -12));
            paths.Add(new Paths(5, 197, -12));
            paths.Add(new Paths(5, 157, -12));
            paths.Add(new Paths(5, 117, -12));
            paths.Add(new Paths(5, 77, -12));
            paths.Add(new Paths(5, 37, -12));
            paths.Add(new Paths(5, -3, -12));
            paths.Add(new Paths(5, -3, 28));
            paths.Add(new Paths(5, 37, 28));
            paths.Add(new Paths(5, 77, 28));
            paths.Add(new Paths(5, 157, 28));
            paths.Add(new Paths(5, 197, 28));
            paths.Add(new Paths(5, 237, 28));
            paths.Add(new Paths(5, 277, 28));
            paths.Add(new Paths(5, 317, 28));
            paths.Add(new Paths(5, 357, 28));
            paths.Add(new Paths(5, 397, 28));
            paths.Add(new Paths(5, 437, 28));
            paths.Add(new Paths(5, 477, 28));
            paths.Add(new Paths(5, 517, 28));
            paths.Add(new Paths(5, 557, 28));
            paths.Add(new Paths(5, 597, 28));
            paths.Add(new Paths(5, 637, 28));
            paths.Add(new Paths(5, 677, 28));
            paths.Add(new Paths(5, 717, 28));
            paths.Add(new Paths(5, 757, 28));
            paths.Add(new Paths(7, 527, 211));
            paths.Add(new Paths(4, 527, 251));
            paths.Add(new Paths(4, 527, 291));
            paths.Add(new Paths(1, 527, 331));
            paths.Add(new Paths(2, 567, 331));
            paths.Add(new Paths(2, 607, 331));
            paths.Add(new Paths(2, 727, 331));
            paths.Add(new Paths(3, 767, 331));
            paths.Add(new Paths(6, 767, 291));
            paths.Add(new Paths(6, 767, 251));
            paths.Add(new Paths(9, 767, 211));
            paths.Add(new Paths(8, 727, 211));
            paths.Add(new Paths(8, 687, 211));
            paths.Add(new Paths(8, 647, 211));
            paths.Add(new Paths(8, 607, 211));
            paths.Add(new Paths(8, 567, 211));
            paths.Add(new Paths(5, 567, 251));
            paths.Add(new Paths(5, 567, 291));
            paths.Add(new Paths(5, 607, 291));
            paths.Add(new Paths(5, 607, 251));
            paths.Add(new Paths(5, 647, 251));
            paths.Add(new Paths(5, 647, 291));
            paths.Add(new Paths(5, 687, 291));
            paths.Add(new Paths(5, 687, 251));
            paths.Add(new Paths(5, 727, 251));
            paths.Add(new Paths(5, 727, 291));
            paths.Add(new Paths(2, 687, 331));
            paths.Add(new Paths(2, 647, 331));
            paths.Add(new Paths(5, 117, 28));

            hills.Add(new Hills(267, 451, 197));
            hills.Add(new Hills(142, 142, 150));

            #endregion
        }

        private void LoadMap35()
        {
            pbCanvas.BackgroundImage = Resources.grass;

            exit.Add(new ExitPoint(300, 3, 550, 57, 34, 381, 454));
            exit.Add(new ExitPoint(262, 560, 475, 600, 36, 350, 50));

            wildPokemons.Add(new ints(37, 22, 30, 25));
            wildPokemons.Add(new ints(77, 22, 30, 20));
            wildPokemons.Add(new ints(58, 22, 30, 15));
            wildPokemons.Add(new ints(455, 22, 30, 35));
            wildPokemons.Add(new ints(351, 22, 30, 5));

            characters.Add(new Character("Dinoman Jr.", Resources.character_dino, 515, 376));
            characters[0].index = 1;
            actionpoint.Add(new ActionPoint(505, 366, 575, 426, -23));

            #region Environment

            structures.Add(new Structures(1, 0, 483, 312));
            structures.Add(new Structures(1, 0, 403, 312));
            structures.Add(new Structures(1, 1, 403, 282));
            structures.Add(new Structures(1, 1, 403, 252));
            structures.Add(new Structures(1, 1, 403, 222));
            structures.Add(new Structures(1, 1, 403, 192));
            structures.Add(new Structures(1, 0, 730, 309));
            structures.Add(new Structures(1, 1, 403, 162));
            structures.Add(new Structures(1, 1, 403, 132));
            structures.Add(new Structures(1, 0, 483, 132));
            structures.Add(new Structures(1, 0, 563, 132));
            structures.Add(new Structures(1, 0, 643, 132));
            structures.Add(new Structures(1, 0, 723, 132));
            structures.Add(new Structures(1, 0, 403, 132));

            trees.Add(new Trees(1, 258, -9));
            trees.Add(new Trees(1, 520, 3));
            trees.Add(new Trees(1, 153, 28));
            trees.Add(new Trees(1, 53, 40));
            trees.Add(new Trees(1, 685, 46));
            trees.Add(new Trees(1, 156, 162));
            trees.Add(new Trees(1, 47, 210));
            trees.Add(new Trees(1, 120, 328));
            trees.Add(new Trees(1, 414, 359));
            trees.Add(new Trees(1, 25, 430));
            trees.Add(new Trees(1, 706, 438));
            trees.Add(new Trees(1, 597, 470));
            trees.Add(new Trees(1, 195, 492));
            trees.Add(new Trees(1, 456, 499));

            buildings.Add(new Buildings(8, 555, 263, true));

            flowers.Add(new Flowers(481, 193));
            flowers.Add(new Flowers(616, 196));
            flowers.Add(new Flowers(742, 227));
            flowers.Add(new Flowers(453, 256));
            flowers.Add(new Flowers(692, 191));
            flowers.Add(new Flowers(559, 220));
            flowers.Add(new Flowers(191, 278));
            flowers.Add(new Flowers(45, 350));
            flowers.Add(new Flowers(102, 152));
            flowers.Add(new Flowers(131, 467));
            flowers.Add(new Flowers(527, 449));
            flowers.Add(new Flowers(703, 549));
            flowers.Add(new Flowers(608, 81));
            flowers.Add(new Flowers(267, 91));
            flowers.Add(new Flowers(276, 440));
            flowers.Add(new Flowers(404, 477));

            wildgrass.Add(new WildGrass(251, 137));
            wildgrass.Add(new WildGrass(291, 137));
            wildgrass.Add(new WildGrass(331, 137));
            wildgrass.Add(new WildGrass(331, 177));
            wildgrass.Add(new WildGrass(331, 217));
            wildgrass.Add(new WildGrass(331, 257));
            wildgrass.Add(new WildGrass(331, 297));
            wildgrass.Add(new WildGrass(331, 337));
            wildgrass.Add(new WildGrass(291, 337));
            wildgrass.Add(new WildGrass(251, 337));
            wildgrass.Add(new WildGrass(251, 297));
            wildgrass.Add(new WildGrass(251, 257));
            wildgrass.Add(new WildGrass(251, 217));
            wildgrass.Add(new WildGrass(251, 177));
            wildgrass.Add(new WildGrass(291, 177));
            wildgrass.Add(new WildGrass(291, 217));
            wildgrass.Add(new WildGrass(291, 257));
            wildgrass.Add(new WildGrass(291, 297));

            #endregion
        }

        private void LoadMap36()
        {
            pbCanvas.BackgroundImage = Resources.townpavement;

            exit.Add(new ExitPoint(16, 1, 790, 55, 35, 412, 494));
            exit.Add(new ExitPoint(6, 552, 791, 593, 37, 395, 69));

            #region Environment

            structures.Add(new Structures(2, 0, 310, 40));
            structures.Add(new Structures(2, 0, 430, 40));
            structures.Add(new Structures(2, 0, 310, 180));
            structures.Add(new Structures(2, 0, 430, 180));
            structures.Add(new Structures(2, 0, 310, 320));
            structures.Add(new Structures(2, 0, 430, 320));
            structures.Add(new Structures(2, 0, 310, 460));
            structures.Add(new Structures(2, 0, 430, 460));

            structures.Add(new Structures(1, 1, 4, 58));
            structures.Add(new Structures(1, 1, 4, 28));
            structures.Add(new Structures(1, 1, 4, -2));
            structures.Add(new Structures(1, 1, 4, 88));
            structures.Add(new Structures(1, 1, 4, 118));
            structures.Add(new Structures(1, 1, 4, 148));
            structures.Add(new Structures(1, 1, 4, 178));
            structures.Add(new Structures(1, 1, 4, 208));
            structures.Add(new Structures(1, 1, 4, 238));
            structures.Add(new Structures(1, 1, 4, 268));
            structures.Add(new Structures(1, 1, 4, 298));
            structures.Add(new Structures(1, 1, 4, 328));
            structures.Add(new Structures(1, 1, 4, 358));
            structures.Add(new Structures(1, 1, 4, 388));
            structures.Add(new Structures(1, 1, 4, 418));
            structures.Add(new Structures(1, 1, 4, 448));
            structures.Add(new Structures(1, 1, 4, 478));
            structures.Add(new Structures(1, 1, 4, 508));
            structures.Add(new Structures(1, 1, 4, 538));
            structures.Add(new Structures(1, 1, 4, 568));
            structures.Add(new Structures(1, 1, 787, -23));
            structures.Add(new Structures(1, 1, 787, 7));
            structures.Add(new Structures(1, 1, 787, 37));
            structures.Add(new Structures(1, 1, 787, 67));
            structures.Add(new Structures(1, 1, 787, 97));
            structures.Add(new Structures(1, 1, 787, 127));
            structures.Add(new Structures(1, 1, 787, 157));
            structures.Add(new Structures(1, 1, 787, 187));
            structures.Add(new Structures(1, 1, 787, 217));
            structures.Add(new Structures(1, 1, 787, 247));
            structures.Add(new Structures(1, 1, 787, 277));
            structures.Add(new Structures(1, 1, 787, 307));
            structures.Add(new Structures(1, 1, 787, 337));
            structures.Add(new Structures(1, 1, 787, 367));
            structures.Add(new Structures(1, 1, 787, 397));
            structures.Add(new Structures(1, 1, 787, 427));
            structures.Add(new Structures(1, 1, 787, 457));
            structures.Add(new Structures(1, 1, 787, 487));
            structures.Add(new Structures(1, 1, 787, 517));
            structures.Add(new Structures(1, 1, 787, 547));
            structures.Add(new Structures(1, 1, 787, 577));
            structures.Add(new Structures(1, 0, 508, 397));
            structures.Add(new Structures(1, 0, 588, 397));
            structures.Add(new Structures(1, 0, 668, 397));
            structures.Add(new Structures(1, 0, 41, 473));
            structures.Add(new Structures(1, 0, 121, 473));
            structures.Add(new Structures(1, 0, 201, 473));
            structures.Add(new Structures(1, 0, 33, 14));
            structures.Add(new Structures(1, 0, 113, 14));
            structures.Add(new Structures(1, 0, 193, 14));
            structures.Add(new Structures(1, 0, 511, 143));
            structures.Add(new Structures(1, 0, 591, 143));
            structures.Add(new Structures(1, 0, 671, 143));

            buildings.Add(new Buildings(1, 75, 93, true));
            buildings.Add(new Buildings(0, 549, 226, true));
            buildings.Add(new Buildings(2, 75, 285, true));

            #endregion
        }

        private void LoadMap37()
        {
            pbCanvas.BackgroundImage = Resources.grass;

            exit.Add(new ExitPoint(258, 2, 412, 57, 36, 345, 513));
            exit.Add(new ExitPoint(757, 211, 796, 364, 38, 66, 282));

            characters.Add(new Character("Explorer", Resources.character_explorer, 232, 347));
            actionpoint.Add(new ActionPoint(222, 337, 292, 397, -24));


            #region Environment

            structures.Add(new Structures(1, 1, 8, -1, 480));
            structures.Add(new Structures(1, 1, 8, 39, 480));
            structures.Add(new Structures(1, 1, 8, 79, 480));
            structures.Add(new Structures(1, 1, 8, 119, 480));
            structures.Add(new Structures(1, 1, 8, 159, 480));
            structures.Add(new Structures(1, 1, 8, 199, 480));
            structures.Add(new Structures(1, 1, 8, 239, 480));
            structures.Add(new Structures(1, 1, 8, 279, 480));
            structures.Add(new Structures(1, 1, 8, 319, 480));
            structures.Add(new Structures(1, 1, 8, 359, 480));
            structures.Add(new Structures(1, 1, 8, 399, 480));
            structures.Add(new Structures(1, 1, 8, 439, 480));
            structures.Add(new Structures(1, 1, 8, 479, 480));
            structures.Add(new Structures(1, 1, 8, 519, 480));
            structures.Add(new Structures(1, 1, 8, 559, 480));
            structures.Add(new Structures(1, 1, 8, 599, 480));
            structures.Add(new Structures(1, 1, 8, 639, 480));
            structures.Add(new Structures(1, 1, 8, 679, 480));
            structures.Add(new Structures(1, 1, 8, 719, 480));
            structures.Add(new Structures(1, 1, 8, 759, 480));
            structures.Add(new Structures(1, 1, 5, 759, 520));
            structures.Add(new Structures(1, 1, 5, 759, 560));
            structures.Add(new Structures(1, 1, 5, 719, 560));
            structures.Add(new Structures(1, 1, 5, 679, 560));
            structures.Add(new Structures(1, 1, 5, 639, 560));
            structures.Add(new Structures(1, 1, 5, 599, 560));
            structures.Add(new Structures(1, 1, 5, 559, 560));
            structures.Add(new Structures(1, 1, 5, 519, 560));
            structures.Add(new Structures(1, 1, 5, 479, 560));
            structures.Add(new Structures(1, 1, 5, 439, 560));
            structures.Add(new Structures(1, 1, 5, 399, 560));
            structures.Add(new Structures(1, 1, 5, 359, 560));
            structures.Add(new Structures(1, 1, 5, 319, 560));
            structures.Add(new Structures(1, 1, 5, 279, 560));
            structures.Add(new Structures(1, 1, 5, 239, 560));
            structures.Add(new Structures(1, 1, 5, 199, 560));
            structures.Add(new Structures(1, 1, 5, 159, 560));
            structures.Add(new Structures(1, 1, 5, 119, 560));
            structures.Add(new Structures(1, 1, 5, 79, 560));
            structures.Add(new Structures(1, 1, 5, 39, 560));
            structures.Add(new Structures(1, 1, 5, -1, 560));
            structures.Add(new Structures(1, 1, 5, -1, 520));
            structures.Add(new Structures(1, 1, 5, 39, 520));
            structures.Add(new Structures(1, 1, 5, 79, 520));
            structures.Add(new Structures(1, 1, 5, 119, 520));
            structures.Add(new Structures(1, 1, 5, 159, 520));
            structures.Add(new Structures(1, 1, 5, 199, 520));
            structures.Add(new Structures(1, 1, 5, 239, 520));
            structures.Add(new Structures(1, 1, 5, 279, 520));
            structures.Add(new Structures(1, 1, 5, 319, 520));
            structures.Add(new Structures(1, 1, 5, 359, 520));
            structures.Add(new Structures(1, 1, 5, 399, 520));
            structures.Add(new Structures(1, 1, 5, 439, 520));
            structures.Add(new Structures(1, 1, 5, 479, 520));
            structures.Add(new Structures(1, 1, 5, 519, 520));
            structures.Add(new Structures(1, 1, 5, 559, 520));
            structures.Add(new Structures(1, 1, 5, 599, 520));
            structures.Add(new Structures(1, 1, 5, 639, 520));
            structures.Add(new Structures(1, 1, 5, 679, 520));
            structures.Add(new Structures(1, 1, 5, 719, 520));

            trees.Add(new Trees(1, 182, 24));
            trees.Add(new Trees(1, 595, 25));
            trees.Add(new Trees(1, 64, 66));
            trees.Add(new Trees(1, 705, 87));
            trees.Add(new Trees(1, 477, 101));
            trees.Add(new Trees(1, 171, 186));
            trees.Add(new Trees(1, 641, 353));
            trees.Add(new Trees(1, 356, 373));

            buildings.Add(new Buildings(4, 42, 285, true));

            flowers.Add(new Flowers(167, 134));
            flowers.Add(new Flowers(40, 195));
            flowers.Add(new Flowers(32, 25));
            flowers.Add(new Flowers(267, 377));
            flowers.Add(new Flowers(527, 376));
            flowers.Add(new Flowers(738, 417));
            flowers.Add(new Flowers(597, 154));
            flowers.Add(new Flowers(465, 46));
            flowers.Add(new Flowers(715, 14));
            flowers.Add(new Flowers(681, 173));

            paths.Add(new Paths(4, 272, -10));
            paths.Add(new Paths(5, 312, -10));
            paths.Add(new Paths(6, 352, -10));
            paths.Add(new Paths(6, 352, 30));
            paths.Add(new Paths(6, 352, 70));
            paths.Add(new Paths(6, 352, 110));
            paths.Add(new Paths(6, 352, 150));
            paths.Add(new Paths(6, 352, 190));
            paths.Add(new Paths(5, 312, 230));
            paths.Add(new Paths(5, 312, 190));
            paths.Add(new Paths(5, 312, 150));
            paths.Add(new Paths(5, 312, 110));
            paths.Add(new Paths(5, 312, 70));
            paths.Add(new Paths(5, 312, 30));
            paths.Add(new Paths(4, 272, 30));
            paths.Add(new Paths(4, 272, 70));
            paths.Add(new Paths(4, 272, 110));
            paths.Add(new Paths(4, 272, 150));
            paths.Add(new Paths(4, 272, 190));
            paths.Add(new Paths(4, 272, 230));
            paths.Add(new Paths(5, 352, 230));
            paths.Add(new Paths(8, 379, 228));
            paths.Add(new Paths(8, 419, 228));
            paths.Add(new Paths(8, 459, 228));
            paths.Add(new Paths(8, 499, 228));
            paths.Add(new Paths(8, 539, 228));
            paths.Add(new Paths(4, 272, 270));
            paths.Add(new Paths(1, 272, 310));
            paths.Add(new Paths(2, 312, 310));
            paths.Add(new Paths(5, 312, 270));
            paths.Add(new Paths(5, 352, 270));
            paths.Add(new Paths(5, 379, 268));
            paths.Add(new Paths(2, 352, 310));
            paths.Add(new Paths(2, 379, 308));
            paths.Add(new Paths(2, 419, 308));
            paths.Add(new Paths(2, 459, 308));
            paths.Add(new Paths(2, 499, 308));
            paths.Add(new Paths(2, 539, 308));
            paths.Add(new Paths(2, 579, 308));
            paths.Add(new Paths(2, 619, 308));
            paths.Add(new Paths(2, 659, 308));
            paths.Add(new Paths(2, 699, 308));
            paths.Add(new Paths(2, 739, 308));
            paths.Add(new Paths(2, 779, 308));
            paths.Add(new Paths(8, 579, 228));
            paths.Add(new Paths(8, 619, 228));
            paths.Add(new Paths(8, 659, 228));
            paths.Add(new Paths(8, 699, 228));
            paths.Add(new Paths(8, 739, 228));
            paths.Add(new Paths(8, 779, 228));
            paths.Add(new Paths(5, 419, 268));
            paths.Add(new Paths(5, 459, 268));
            paths.Add(new Paths(5, 499, 268));
            paths.Add(new Paths(5, 539, 268));
            paths.Add(new Paths(5, 579, 268));
            paths.Add(new Paths(5, 619, 268));
            paths.Add(new Paths(5, 659, 268));
            paths.Add(new Paths(5, 699, 268));
            paths.Add(new Paths(5, 739, 268));
            paths.Add(new Paths(5, 779, 268));

            #endregion
        }

        private void LoadMap38()
        {
            pbCanvas.BackgroundImage = Resources.grass;

            exit.Add(new ExitPoint(1, 217, 47, 365, 37, 710, 284));
            exit.Add(new ExitPoint(754, 214, 794, 370, 39, 93, 288));

            wildPokemons.Add(new ints(311, 22, 30, 24));
            wildPokemons.Add(new ints(312, 22, 30, 24));
            wildPokemons.Add(new ints(239, 22, 30, 2));
            wildPokemons.Add(new ints(309, 22, 25, 40));
            wildPokemons.Add(new ints(310, 26, 30, 10));

            characters.Add(new Character("Kid", Resources.character_kid, 575, 211));
            characters[0].pokemons.Add(new Pokemons(311, 28));
            characters[0].pokemons.Add(new Pokemons(312, 28));
            characters[0].index = 100;
            actionpoint.Add(new ActionPoint(565, 201, 635, 261, 0));

            characters.Add(new Character("Trainer", Resources.character_dad, 462, 346));
            characters[1].pokemons.Add(new Pokemons(125, 35));
            characters[1].pokemons.Add(new Pokemons(126, 35));
            characters[1].pokemons.Add(new Pokemons(123, 35));
            characters[1].index = 101;
            actionpoint.Add(new ActionPoint(452, 336, 522, 396, 1));

            characters.Add(new Character("Trainer", Resources.character_dad, 213, 390));
            characters[2].pokemons.Add(new Pokemons(310, 32));
            characters[2].pokemons.Add(new Pokemons(229, 35));
            characters[2].pokemons.Add(new Pokemons(59, 37));
            characters[2].index = 102;
            actionpoint.Add(new ActionPoint(203, 380, 273, 440, 2));

            #region Environment

            structures.Add(new Structures(1, 1, 8, -1, 480));
            structures.Add(new Structures(1, 1, 8, 39, 480));
            structures.Add(new Structures(1, 1, 5, 39, 560));
            structures.Add(new Structures(1, 1, 5, -1, 560));
            structures.Add(new Structures(1, 1, 5, -1, 520));
            structures.Add(new Structures(1, 1, 5, 39, 520));
            structures.Add(new Structures(1, 1, 9, 79, 480));
            structures.Add(new Structures(1, 1, 6, 79, 520));
            structures.Add(new Structures(1, 1, 3, 79, 560));

            trees.Add(new Trees(1, 197, 7));
            trees.Add(new Trees(1, 595, 25));
            trees.Add(new Trees(1, 41, 65));
            trees.Add(new Trees(1, 705, 87));
            trees.Add(new Trees(1, 477, 101));
            trees.Add(new Trees(1, 188, 129));
            trees.Add(new Trees(1, 352, 352));
            trees.Add(new Trees(1, 641, 353));
            trees.Add(new Trees(1, 522, 394));
            trees.Add(new Trees(1, 156, 469));
            trees.Add(new Trees(1, 408, 472));
            trees.Add(new Trees(1, 574, 496));
            trees.Add(new Trees(1, 262, 518));
            trees.Add(new Trees(1, 692, 520));

            flowers.Add(new Flowers(267, 377));
            flowers.Add(new Flowers(527, 376));
            flowers.Add(new Flowers(738, 417));
            flowers.Add(new Flowers(597, 154));
            flowers.Add(new Flowers(715, 14));
            flowers.Add(new Flowers(681, 173));
            flowers.Add(new Flowers(526, 525));
            flowers.Add(new Flowers(328, 462));
            flowers.Add(new Flowers(689, 479));
            flowers.Add(new Flowers(340, 566));
            flowers.Add(new Flowers(150, 566));
            flowers.Add(new Flowers(133, 395));
            flowers.Add(new Flowers(34, 368));
            flowers.Add(new Flowers(79, 168));
            flowers.Add(new Flowers(141, 126));
            flowers.Add(new Flowers(133, 61));
            flowers.Add(new Flowers(501, 39));

            wildgrass.Add(new WildGrass(301, 25));
            wildgrass.Add(new WildGrass(301, 65));
            wildgrass.Add(new WildGrass(301, 105));
            wildgrass.Add(new WildGrass(341, 105));
            wildgrass.Add(new WildGrass(381, 105));
            wildgrass.Add(new WildGrass(381, 65));
            wildgrass.Add(new WildGrass(381, 25));
            wildgrass.Add(new WildGrass(341, 25));
            wildgrass.Add(new WildGrass(341, 65));
            wildgrass.Add(new WildGrass(301, 145));
            wildgrass.Add(new WildGrass(341, 145));
            wildgrass.Add(new WildGrass(381, 145));
            wildgrass.Add(new WildGrass(421, 145));
            wildgrass.Add(new WildGrass(421, 105));
            wildgrass.Add(new WildGrass(421, 65));
            wildgrass.Add(new WildGrass(421, 25));

            paths.Add(new Paths(8, 379, 228));
            paths.Add(new Paths(8, 419, 228));
            paths.Add(new Paths(8, 459, 228));
            paths.Add(new Paths(8, 499, 228));
            paths.Add(new Paths(8, 539, 228));
            paths.Add(new Paths(5, 379, 268));
            paths.Add(new Paths(2, 379, 308));
            paths.Add(new Paths(2, 419, 308));
            paths.Add(new Paths(2, 459, 308));
            paths.Add(new Paths(2, 499, 308));
            paths.Add(new Paths(2, 539, 308));
            paths.Add(new Paths(2, 579, 308));
            paths.Add(new Paths(2, 619, 308));
            paths.Add(new Paths(2, 659, 308));
            paths.Add(new Paths(2, 699, 308));
            paths.Add(new Paths(2, 739, 308));
            paths.Add(new Paths(2, 779, 308));
            paths.Add(new Paths(8, 579, 228));
            paths.Add(new Paths(8, 619, 228));
            paths.Add(new Paths(8, 659, 228));
            paths.Add(new Paths(8, 699, 228));
            paths.Add(new Paths(8, 739, 228));
            paths.Add(new Paths(8, 779, 228));
            paths.Add(new Paths(5, 419, 268));
            paths.Add(new Paths(5, 459, 268));
            paths.Add(new Paths(5, 499, 268));
            paths.Add(new Paths(5, 539, 268));
            paths.Add(new Paths(5, 579, 268));
            paths.Add(new Paths(5, 619, 268));
            paths.Add(new Paths(5, 659, 268));
            paths.Add(new Paths(5, 699, 268));
            paths.Add(new Paths(5, 739, 268));
            paths.Add(new Paths(5, 779, 268));
            paths.Add(new Paths(8, 339, 228));
            paths.Add(new Paths(8, 299, 228));
            paths.Add(new Paths(8, 259, 228));
            paths.Add(new Paths(8, 219, 228));
            paths.Add(new Paths(8, 179, 228));
            paths.Add(new Paths(8, 139, 228));
            paths.Add(new Paths(8, 99, 228));
            paths.Add(new Paths(8, 59, 228));
            paths.Add(new Paths(8, 19, 228));
            paths.Add(new Paths(8, -21, 228));
            paths.Add(new Paths(2, 339, 308));
            paths.Add(new Paths(2, 299, 308));
            paths.Add(new Paths(2, 259, 308));
            paths.Add(new Paths(2, 219, 308));
            paths.Add(new Paths(2, 179, 308));
            paths.Add(new Paths(2, 139, 308));
            paths.Add(new Paths(2, 99, 308));
            paths.Add(new Paths(2, 59, 308));
            paths.Add(new Paths(2, 19, 308));
            paths.Add(new Paths(2, -21, 308));
            paths.Add(new Paths(5, 339, 268));
            paths.Add(new Paths(5, 299, 268));
            paths.Add(new Paths(5, 259, 268));
            paths.Add(new Paths(5, 219, 268));
            paths.Add(new Paths(5, 179, 268));
            paths.Add(new Paths(5, 139, 268));
            paths.Add(new Paths(5, 99, 268));
            paths.Add(new Paths(5, 59, 268));
            paths.Add(new Paths(5, 19, 268));
            paths.Add(new Paths(5, -21, 268));

            #endregion
        }

        private void LoadMap39()
        {
            pbCanvas.BackgroundImage = Resources.grass;

            exit.Add(new ExitPoint(3, 210, 38, 368, 38, 731, 274));
            exit.Add(new ExitPoint(760, 231, 791, 351, 40, 76, 280));

            wildPokemons.Add(new ints(311, 22, 30, 24));
            wildPokemons.Add(new ints(312, 22, 30, 24));
            wildPokemons.Add(new ints(239, 22, 30, 2));
            wildPokemons.Add(new ints(309, 22, 30, 40));
            wildPokemons.Add(new ints(310, 22, 30, 10));

            if (!pokemonDefeated[446])
            {
                mainForm.ShowMessage("A sleeping Munchlax is blocking the way.");

                wildpokemons.Add(new Pokemons(446, 10, 266, 232));
                actionpoint.Add(new ActionPoint(256, 222, 326, 282, 100));
            }

            #region Environment

            trees.Add(new Trees(1, 768, -10));
            trees.Add(new Trees(1, 688, -10));
            trees.Add(new Trees(1, 608, -10));
            trees.Add(new Trees(1, 528, -10));
            trees.Add(new Trees(1, 448, -10));
            trees.Add(new Trees(1, 368, -10));
            trees.Add(new Trees(1, 288, -10));
            trees.Add(new Trees(1, 208, -10));
            trees.Add(new Trees(1, 1, 3));
            trees.Add(new Trees(1, 768, 50));
            trees.Add(new Trees(1, 688, 50));
            trees.Add(new Trees(1, 608, 50));
            trees.Add(new Trees(1, 528, 50));
            trees.Add(new Trees(1, 448, 50));
            trees.Add(new Trees(1, 368, 50));
            trees.Add(new Trees(1, 288, 50));
            trees.Add(new Trees(1, 208, 50));
            trees.Add(new Trees(1, 97, 83));
            trees.Add(new Trees(1, 768, 110));
            trees.Add(new Trees(1, 688, 110));
            trees.Add(new Trees(1, 608, 110));
            trees.Add(new Trees(1, 528, 110));
            trees.Add(new Trees(1, 448, 110));
            trees.Add(new Trees(1, 368, 110));
            trees.Add(new Trees(1, 288, 110));
            trees.Add(new Trees(1, 208, 110));
            trees.Add(new Trees(1, 208, 170));
            trees.Add(new Trees(1, 288, 170));
            trees.Add(new Trees(1, 368, 170));
            trees.Add(new Trees(1, 448, 170));
            trees.Add(new Trees(1, 528, 170));
            trees.Add(new Trees(1, 608, 170));
            trees.Add(new Trees(1, 688, 170));
            trees.Add(new Trees(1, 768, 170));
            trees.Add(new Trees(1, 208, 290));
            trees.Add(new Trees(1, 288, 290));
            trees.Add(new Trees(1, 368, 290));
            trees.Add(new Trees(1, 448, 290));
            trees.Add(new Trees(1, 528, 290));
            trees.Add(new Trees(1, 608, 290));
            trees.Add(new Trees(1, 688, 290));
            trees.Add(new Trees(1, 768, 290));
            trees.Add(new Trees(1, 768, 350));
            trees.Add(new Trees(1, 208, 350));
            trees.Add(new Trees(1, 288, 350));
            trees.Add(new Trees(1, 368, 350));
            trees.Add(new Trees(1, 448, 350));
            trees.Add(new Trees(1, 528, 350));
            trees.Add(new Trees(1, 608, 350));
            trees.Add(new Trees(1, 688, 350));
            trees.Add(new Trees(1, 56, 352));
            trees.Add(new Trees(1, 768, 410));
            trees.Add(new Trees(1, 208, 410));
            trees.Add(new Trees(1, 288, 410));
            trees.Add(new Trees(1, 368, 410));
            trees.Add(new Trees(1, 448, 410));
            trees.Add(new Trees(1, 528, 410));
            trees.Add(new Trees(1, 608, 410));
            trees.Add(new Trees(1, 688, 410));
            trees.Add(new Trees(1, 768, 470));
            trees.Add(new Trees(1, 208, 470));
            trees.Add(new Trees(1, 288, 470));
            trees.Add(new Trees(1, 368, 470));
            trees.Add(new Trees(1, 448, 470));
            trees.Add(new Trees(1, 528, 470));
            trees.Add(new Trees(1, 608, 470));
            trees.Add(new Trees(1, 688, 470));
            trees.Add(new Trees(1, 83, 479));
            trees.Add(new Trees(1, 768, 530));
            trees.Add(new Trees(1, 688, 530));
            trees.Add(new Trees(1, 608, 530));
            trees.Add(new Trees(1, 528, 530));
            trees.Add(new Trees(1, 448, 530));
            trees.Add(new Trees(1, 368, 530));
            trees.Add(new Trees(1, 288, 530));
            trees.Add(new Trees(1, 208, 530));

            flowers.Add(new Flowers(18, 130));
            flowers.Add(new Flowers(101, 180));
            flowers.Add(new Flowers(133, 1));
            flowers.Add(new Flowers(162, 58));
            flowers.Add(new Flowers(15, 447));
            flowers.Add(new Flowers(29, 522));
            flowers.Add(new Flowers(140, 419));
            flowers.Add(new Flowers(160, 502));

            paths.Add(new Paths(8, 179, 228));
            paths.Add(new Paths(8, 139, 228));
            paths.Add(new Paths(8, 99, 228));
            paths.Add(new Paths(8, 59, 228));
            paths.Add(new Paths(8, 19, 228));
            paths.Add(new Paths(8, -21, 228));
            paths.Add(new Paths(2, 179, 308));
            paths.Add(new Paths(2, 139, 308));
            paths.Add(new Paths(2, 99, 308));
            paths.Add(new Paths(2, 59, 308));
            paths.Add(new Paths(2, 19, 308));
            paths.Add(new Paths(2, -21, 308));
            paths.Add(new Paths(5, 179, 268));
            paths.Add(new Paths(5, 139, 268));
            paths.Add(new Paths(5, 99, 268));
            paths.Add(new Paths(5, 59, 268));
            paths.Add(new Paths(5, 19, 268));
            paths.Add(new Paths(5, -21, 268));
            paths.Add(new Paths(6, 219, 268));
            paths.Add(new Paths(3, 219, 308));
            paths.Add(new Paths(9, 219, 228));

            #endregion
        }

        private void LoadMap40()
        {
            pbCanvas.BackgroundImage = Resources.mountain;

            exit.Add(new ExitPoint(1, 2, 40, 595, 39, 706, 235));

            #region Environment

            structures.Add(new Structures(3, 0, 454, 491));
            structures.Add(new Structures(3, 0, 562, 279));
            structures.Add(new Structures(3, 0, 392, 192));
            structures.Add(new Structures(3, 0, 667, 385));
            structures.Add(new Structures(3, 0, 719, 163));
            structures.Add(new Structures(3, 0, 692, 483));
            structures.Add(new Structures(3, 0, 95, 421));
            structures.Add(new Structures(3, 0, 380, 292));
            structures.Add(new Structures(3, 0, 270, 440));

            buildings.Add(new Buildings(2, 567, 9, true));
            buildings.Add(new Buildings(1, 66, 10, true));
            buildings.Add(new Buildings(0, 321, 17, true));
            buildings.Add(new Buildings(-1, 516, 195, false));
            buildings.Add(new Buildings(-1, 150, 236, true));
            buildings.Add(new Buildings(-1, 412, 401, false));

            #endregion
        }

        private void LoadMap41()
        {
            pbCanvas.BackgroundImage = Resources.waterwaves;

            exit.Add(new ExitPoint(2, 1, 795, 48, 37, 379, 442));
            exit.Add(new ExitPoint(1, 552, 795, 596, 42, 399, 91));

            wildPokemons.Add(new ints(318, 22, 29, 40));
            wildPokemons.Add(new ints(320, 22, 30, 56));
            wildPokemons.Add(new ints(147, 22, 30, 2));
            wildPokemons.Add(new ints(319, 30, 30, 2));

            characters.Add(new Character("Swimmer", Resources.character_swimmer, 231, 172));
            characters[0].pokemons.Add(new Pokemons(319, 32));
            characters[0].pokemons.Add(new Pokemons(130, 38));
            characters[0].index = 104;
            actionpoint.Add(new ActionPoint(221, 162, 291, 222, 0));

            characters.Add(new Character("Swimmer", Resources.character_swimmer, 440, 363));
            characters[1].pokemons.Add(new Pokemons(318, 28));
            characters[1].pokemons.Add(new Pokemons(320, 35));
            characters[1].pokemons.Add(new Pokemons(319, 40));
            characters[1].index = 105;
            actionpoint.Add(new ActionPoint(430, 353, 500, 413, 1));

            characters.Add(new Character("Swimmer", Resources.character_swimmer2, 557, 131));
            characters[2].pokemons.Add(new Pokemons(320, 35));
            characters[2].pokemons.Add(new Pokemons(321, 41));
            characters[2].index = 106;
            actionpoint.Add(new ActionPoint(547, 121, 617, 181, 2));

            characters.Add(new Character("Swimmer", Resources.character_swimmer3, 98, 51));
            characters[3].pokemons.Add(new Pokemons(147, 28));
            characters[3].pokemons.Add(new Pokemons(350, 36));
            characters[3].index = 107;
            actionpoint.Add(new ActionPoint(88, 41, 158, 101, 3));

            characters.Add(new Character("Swimmer", Resources.character_swimmer4, 675, 107));
            characters[4].pokemons.Add(new Pokemons(370, 32));
            characters[4].index = 108;
            actionpoint.Add(new ActionPoint(665, 97, 735, 157, 4));

            characters.Add(new Character("Swimmer", Resources.character_swimmer2, 229, 429));
            characters[5].pokemons.Add(new Pokemons(171, 38));
            characters[5].pokemons.Add(new Pokemons(230, 38));
            characters[5].pokemons.Add(new Pokemons(119, 40));
            characters[5].pokemons.Add(new Pokemons(340, 45));
            characters[5].index = 109;
            actionpoint.Add(new ActionPoint(219, 419, 289, 479, 5));

            #region Environment

            structures.Add(new Structures(3, 0, 141, 108));
            structures.Add(new Structures(3, 0, 332, 222));
            structures.Add(new Structures(3, 0, 154, 344));
            structures.Add(new Structures(3, 0, 509, 397));
            structures.Add(new Structures(3, 0, 656, 195));
            structures.Add(new Structures(3, 0, 509, 44));
            structures.Add(new Structures(3, 0, 686, 367));
            structures.Add(new Structures(3, 0, 673, 478));
            structures.Add(new Structures(3, 0, 478, 232));
            structures.Add(new Structures(3, 0, 69, 213));
            structures.Add(new Structures(3, 0, 39, 445));
            structures.Add(new Structures(3, 0, 310, 333));
            structures.Add(new Structures(3, 0, 640, 29));
            structures.Add(new Structures(3, 0, 25, 19));
            structures.Add(new Structures(3, 0, 734, 100));
            structures.Add(new Structures(3, 0, 738, 227));
            structures.Add(new Structures(3, 0, 740, 413));
            structures.Add(new Structures(3, 0, 670, 258));
            structures.Add(new Structures(3, 0, 22, 94));
            structures.Add(new Structures(3, 0, 2, 211));
            structures.Add(new Structures(3, 0, 31, 303));
            structures.Add(new Structures(3, 0, 20, 501));
            structures.Add(new Structures(3, 0, 82, 346));
            structures.Add(new Structures(3, 0, 121, 459));

            LoadWater();

            #endregion
        }

        private void LoadMap42()
        {
            pbCanvas.BackgroundImage = Resources.waterwaves;

            exit.Add(new ExitPoint(110, 465, 739, 595, 43, 417, 92));
            exit.Add(new ExitPoint(2, 0, 795, 53, 41, 427, 484));

            #region Environment

            structures.Add(new Structures(3, 0, 66, 61));
            structures.Add(new Structures(3, 0, 91, 192));
            structures.Add(new Structures(3, 0, 48, 296));
            structures.Add(new Structures(3, 0, 625, 25));
            structures.Add(new Structures(3, 0, 722, 89));
            structures.Add(new Structures(3, 0, 693, 227));
            structures.Add(new Structures(3, 0, 718, 348));
            structures.Add(new Structures(3, 0, 28, 402));
            structures.Add(new Structures(3, 0, 41, 483));
            structures.Add(new Structures(3, 0, 748, 480));
            structures.Add(new Structures(3, 0, 308, 171));
            structures.Add(new Structures(3, 0, 574, 312));
            structures.Add(new Structures(3, 0, 579, 115));
            structures.Add(new Structures(3, 0, 267, 31));
            structures.Add(new Structures(3, 0, 273, 293));
            structures.Add(new Structures(3, 0, 473, 203));

            paths.Add(new Paths(7, 122, 498));
            paths.Add(new Paths(4, 122, 538));
            paths.Add(new Paths(4, 122, 578));
            paths.Add(new Paths(5, 162, 578));
            paths.Add(new Paths(5, 202, 578));
            paths.Add(new Paths(5, 242, 578));
            paths.Add(new Paths(5, 282, 578));
            paths.Add(new Paths(5, 322, 578));
            paths.Add(new Paths(5, 362, 578));
            paths.Add(new Paths(5, 402, 578));
            paths.Add(new Paths(5, 442, 578));
            paths.Add(new Paths(5, 482, 578));
            paths.Add(new Paths(5, 522, 578));
            paths.Add(new Paths(5, 562, 578));
            paths.Add(new Paths(5, 602, 578));
            paths.Add(new Paths(5, 642, 578));
            paths.Add(new Paths(6, 682, 578));
            paths.Add(new Paths(6, 682, 538));
            paths.Add(new Paths(9, 682, 498));
            paths.Add(new Paths(8, 642, 498));
            paths.Add(new Paths(8, 602, 498));
            paths.Add(new Paths(8, 562, 498));
            paths.Add(new Paths(8, 522, 498));
            paths.Add(new Paths(8, 482, 498));
            paths.Add(new Paths(8, 442, 498));
            paths.Add(new Paths(8, 402, 498));
            paths.Add(new Paths(8, 362, 498));
            paths.Add(new Paths(8, 322, 498));
            paths.Add(new Paths(8, 282, 498));
            paths.Add(new Paths(8, 242, 498));
            paths.Add(new Paths(8, 202, 498));
            paths.Add(new Paths(8, 162, 498));
            paths.Add(new Paths(5, 162, 538));
            paths.Add(new Paths(5, 202, 538));
            paths.Add(new Paths(5, 242, 538));
            paths.Add(new Paths(5, 282, 538));
            paths.Add(new Paths(5, 322, 538));
            paths.Add(new Paths(5, 362, 538));
            paths.Add(new Paths(5, 402, 538));
            paths.Add(new Paths(5, 442, 538));
            paths.Add(new Paths(5, 482, 538));
            paths.Add(new Paths(5, 522, 538));
            paths.Add(new Paths(5, 562, 538));
            paths.Add(new Paths(5, 602, 538));
            paths.Add(new Paths(5, 642, 538));

            #endregion
        }

        private void LoadMap43()
        {
            pbCanvas.BackgroundImage = Resources.ground;

            exit.Add(new ExitPoint(749, 84, 795, 214, 44, 51, 240));

            characters.Add(new Character("Old man", Resources.character_oldman2, 241, 201));
            actionpoint.Add(new ActionPoint(231, 191, 301, 251, -25));

            #region Environment

            structures.Add(new Structures(1, 1, 6, 146, 0));
            structures.Add(new Structures(1, 1, 6, 146, 40));
            structures.Add(new Structures(1, 1, 6, 146, 80));
            structures.Add(new Structures(1, 1, 6, 146, 120));
            structures.Add(new Structures(1, 1, 6, 146, 160));
            structures.Add(new Structures(1, 1, 6, 146, 200));
            structures.Add(new Structures(1, 1, 6, 146, 240));
            structures.Add(new Structures(1, 1, 6, 146, 280));
            structures.Add(new Structures(1, 1, 6, 146, 320));
            structures.Add(new Structures(1, 1, 8, 186, 360));
            structures.Add(new Structures(1, 1, 8, 226, 360));
            structures.Add(new Structures(1, 1, 8, 266, 360));
            structures.Add(new Structures(1, 1, 8, 306, 360));
            structures.Add(new Structures(1, 1, 8, 346, 360));
            structures.Add(new Structures(1, 1, 8, 386, 360));
            structures.Add(new Structures(1, 1, 8, 426, 360));
            structures.Add(new Structures(1, 1, 8, 466, 360));
            structures.Add(new Structures(1, 1, 8, 506, 360));
            structures.Add(new Structures(1, 1, 8, 546, 360));
            structures.Add(new Structures(1, 1, 8, 586, 360));
            structures.Add(new Structures(1, 1, 8, 626, 360));
            structures.Add(new Structures(1, 1, 8, 666, 360));
            structures.Add(new Structures(1, 1, 8, 706, 360));
            structures.Add(new Structures(1, 1, 8, 746, 360));
            structures.Add(new Structures(1, 1, 8, 786, 360));
            structures.Add(new Structures(1, 1, 5, 146, 360));
            structures.Add(new Structures(1, 1, 5, 146, 400));
            structures.Add(new Structures(1, 1, 5, 146, 440));
            structures.Add(new Structures(1, 1, 5, 146, 480));
            structures.Add(new Structures(1, 1, 5, 146, 520));
            structures.Add(new Structures(1, 1, 5, 146, 560));
            structures.Add(new Structures(1, 1, 5, 186, 560));
            structures.Add(new Structures(1, 1, 5, 226, 560));
            structures.Add(new Structures(1, 1, 5, 266, 560));
            structures.Add(new Structures(1, 1, 5, 306, 560));
            structures.Add(new Structures(1, 1, 5, 346, 560));
            structures.Add(new Structures(1, 1, 5, 386, 560));
            structures.Add(new Structures(1, 1, 5, 426, 560));
            structures.Add(new Structures(1, 1, 5, 466, 560));
            structures.Add(new Structures(1, 1, 5, 506, 560));
            structures.Add(new Structures(1, 1, 5, 546, 560));
            structures.Add(new Structures(1, 1, 5, 586, 560));
            structures.Add(new Structures(1, 1, 5, 626, 560));
            structures.Add(new Structures(1, 1, 5, 666, 560));
            structures.Add(new Structures(1, 1, 5, 706, 560));
            structures.Add(new Structures(1, 1, 5, 746, 560));
            structures.Add(new Structures(1, 1, 5, 786, 560));
            structures.Add(new Structures(1, 1, 5, 786, 520));
            structures.Add(new Structures(1, 1, 5, 786, 480));
            structures.Add(new Structures(1, 1, 5, 786, 440));
            structures.Add(new Structures(1, 1, 5, 786, 400));
            structures.Add(new Structures(1, 1, 5, 746, 400));
            structures.Add(new Structures(1, 1, 5, 706, 400));
            structures.Add(new Structures(1, 1, 5, 666, 400));
            structures.Add(new Structures(1, 1, 5, 626, 400));
            structures.Add(new Structures(1, 1, 5, 586, 400));
            structures.Add(new Structures(1, 1, 5, 546, 400));
            structures.Add(new Structures(1, 1, 5, 506, 400));
            structures.Add(new Structures(1, 1, 5, 466, 400));
            structures.Add(new Structures(1, 1, 5, 426, 400));
            structures.Add(new Structures(1, 1, 5, 386, 400));
            structures.Add(new Structures(1, 1, 5, 346, 400));
            structures.Add(new Structures(1, 1, 5, 306, 400));
            structures.Add(new Structures(1, 1, 5, 266, 400));
            structures.Add(new Structures(1, 1, 5, 226, 400));
            structures.Add(new Structures(1, 1, 5, 186, 400));
            structures.Add(new Structures(1, 1, 5, 186, 440));
            structures.Add(new Structures(1, 1, 5, 186, 480));
            structures.Add(new Structures(1, 1, 5, 186, 520));
            structures.Add(new Structures(1, 1, 5, 226, 520));
            structures.Add(new Structures(1, 1, 5, 266, 520));
            structures.Add(new Structures(1, 1, 5, 306, 520));
            structures.Add(new Structures(1, 1, 5, 346, 520));
            structures.Add(new Structures(1, 1, 5, 386, 520));
            structures.Add(new Structures(1, 1, 5, 426, 520));
            structures.Add(new Structures(1, 1, 5, 466, 520));
            structures.Add(new Structures(1, 1, 5, 506, 520));
            structures.Add(new Structures(1, 1, 5, 546, 520));
            structures.Add(new Structures(1, 1, 5, 586, 520));
            structures.Add(new Structures(1, 1, 5, 626, 520));
            structures.Add(new Structures(1, 1, 5, 666, 520));
            structures.Add(new Structures(1, 1, 5, 706, 520));
            structures.Add(new Structures(1, 1, 5, 746, 520));
            structures.Add(new Structures(1, 1, 5, 746, 480));
            structures.Add(new Structures(1, 1, 5, 746, 440));
            structures.Add(new Structures(1, 1, 5, 706, 440));
            structures.Add(new Structures(1, 1, 5, 666, 440));
            structures.Add(new Structures(1, 1, 5, 626, 440));
            structures.Add(new Structures(1, 1, 5, 586, 440));
            structures.Add(new Structures(1, 1, 5, 546, 440));
            structures.Add(new Structures(1, 1, 5, 506, 440));
            structures.Add(new Structures(1, 1, 5, 466, 440));
            structures.Add(new Structures(1, 1, 5, 426, 440));
            structures.Add(new Structures(1, 1, 5, 386, 440));
            structures.Add(new Structures(1, 1, 5, 346, 440));
            structures.Add(new Structures(1, 1, 5, 306, 440));
            structures.Add(new Structures(1, 1, 5, 266, 440));
            structures.Add(new Structures(1, 1, 5, 226, 440));
            structures.Add(new Structures(1, 1, 5, 226, 480));
            structures.Add(new Structures(1, 1, 5, 266, 480));
            structures.Add(new Structures(1, 1, 5, 306, 480));
            structures.Add(new Structures(1, 1, 5, 346, 480));
            structures.Add(new Structures(1, 1, 5, 386, 480));
            structures.Add(new Structures(1, 1, 5, 426, 480));
            structures.Add(new Structures(1, 1, 5, 466, 480));
            structures.Add(new Structures(1, 1, 5, 506, 480));
            structures.Add(new Structures(1, 1, 5, 546, 480));
            structures.Add(new Structures(1, 1, 5, 586, 480));
            structures.Add(new Structures(1, 1, 5, 626, 480));
            structures.Add(new Structures(1, 1, 5, 666, 480));
            structures.Add(new Structures(1, 1, 5, 706, 480));
            structures.Add(new Structures(1, 1, 5, 106, 0));
            structures.Add(new Structures(1, 1, 5, 66, 0));
            structures.Add(new Structures(1, 1, 5, 26, 0));
            structures.Add(new Structures(1, 1, 5, -14, 0));
            structures.Add(new Structures(1, 1, 5, -14, 40));
            structures.Add(new Structures(1, 1, 5, -14, 80));
            structures.Add(new Structures(1, 1, 5, -14, 120));
            structures.Add(new Structures(1, 1, 5, -14, 160));
            structures.Add(new Structures(1, 1, 5, -14, 200));
            structures.Add(new Structures(1, 1, 5, -14, 240));
            structures.Add(new Structures(1, 1, 5, -14, 280));
            structures.Add(new Structures(1, 1, 5, -14, 320));
            structures.Add(new Structures(1, 1, 5, -14, 360));
            structures.Add(new Structures(1, 1, 5, -14, 400));
            structures.Add(new Structures(1, 1, 5, -14, 440));
            structures.Add(new Structures(1, 1, 5, -14, 480));
            structures.Add(new Structures(1, 1, 5, -14, 520));
            structures.Add(new Structures(1, 1, 5, -14, 560));
            structures.Add(new Structures(1, 1, 5, 26, 560));
            structures.Add(new Structures(1, 1, 5, 66, 560));
            structures.Add(new Structures(1, 1, 5, 106, 560));
            structures.Add(new Structures(1, 1, 5, 106, 520));
            structures.Add(new Structures(1, 1, 5, 106, 480));
            structures.Add(new Structures(1, 1, 5, 106, 440));
            structures.Add(new Structures(1, 1, 5, 106, 400));
            structures.Add(new Structures(1, 1, 5, 106, 360));
            structures.Add(new Structures(1, 1, 5, 106, 320));
            structures.Add(new Structures(1, 1, 5, 106, 280));
            structures.Add(new Structures(1, 1, 5, 106, 240));
            structures.Add(new Structures(1, 1, 5, 106, 200));
            structures.Add(new Structures(1, 1, 5, 106, 160));
            structures.Add(new Structures(1, 1, 5, 106, 120));
            structures.Add(new Structures(1, 1, 5, 106, 80));
            structures.Add(new Structures(1, 1, 5, 106, 40));
            structures.Add(new Structures(1, 1, 5, 66, 40));
            structures.Add(new Structures(1, 1, 5, 26, 40));
            structures.Add(new Structures(1, 1, 5, 26, 80));
            structures.Add(new Structures(1, 1, 5, 26, 120));
            structures.Add(new Structures(1, 1, 5, 26, 160));
            structures.Add(new Structures(1, 1, 5, 26, 200));
            structures.Add(new Structures(1, 1, 5, 26, 240));
            structures.Add(new Structures(1, 1, 5, 26, 280));
            structures.Add(new Structures(1, 1, 5, 26, 320));
            structures.Add(new Structures(1, 1, 5, 26, 360));
            structures.Add(new Structures(1, 1, 5, 26, 400));
            structures.Add(new Structures(1, 1, 5, 26, 440));
            structures.Add(new Structures(1, 1, 5, 26, 480));
            structures.Add(new Structures(1, 1, 5, 26, 520));
            structures.Add(new Structures(1, 1, 5, 66, 520));
            structures.Add(new Structures(1, 1, 5, 66, 480));
            structures.Add(new Structures(1, 1, 5, 66, 440));
            structures.Add(new Structures(1, 1, 5, 66, 400));
            structures.Add(new Structures(1, 1, 5, 66, 360));
            structures.Add(new Structures(1, 1, 5, 66, 320));
            structures.Add(new Structures(1, 1, 5, 66, 280));
            structures.Add(new Structures(1, 1, 5, 66, 240));
            structures.Add(new Structures(1, 1, 5, 66, 200));
            structures.Add(new Structures(1, 1, 5, 66, 160));
            structures.Add(new Structures(1, 1, 5, 66, 120));
            structures.Add(new Structures(1, 1, 5, 66, 80));
            structures.Add(new Structures(3, 0, 743, 51));
            structures.Add(new Structures(3, 0, 743, 177));

            #endregion
        }

        private void LoadMap44()
        {
            pbCanvas.BackgroundImage = Resources.ground;

            exit.Add(new ExitPoint(3, 187, 51, 362, 43, 676, 120));
            exit.Add(new ExitPoint(750, 187, 797, 342, 45, 108, 210));

            wildPokemons.Add(new ints(480, 1, 1, 1));
            wildPokemons.Add(new ints(481, 1, 1, 1));
            wildPokemons.Add(new ints(482, 1, 1, 1));
            wildPokemons.Add(new ints(490, 1, 1, 1));
            wildPokemons.Add(new ints(489, 1, 1, 1));
            wildPokemons.Add(new ints(50, 1, 1, 95));

            mainForm.ShowMessage("Old women: Go away!");

            characters.Add(new Character("Old woman", Resources.character_oldwoman, 64, 46));
            characters.Add(new Character("Old woman", Resources.character_oldwoman, 181, 56));
            characters.Add(new Character("Old woman", Resources.character_oldwoman, 309, 47));
            characters.Add(new Character("Old woman", Resources.character_oldwoman, 461, 47));
            characters.Add(new Character("Old woman", Resources.character_oldwoman, 589, 61));
            characters.Add(new Character("Old woman", Resources.character_oldwoman, 710, 51));

            #region Environment

            structures.Add(new Structures(3, 0, 4, 165));
            structures.Add(new Structures(3, 0, 98, 163));
            structures.Add(new Structures(3, 0, 220, 165));
            structures.Add(new Structures(3, 0, 348, 163));
            structures.Add(new Structures(3, 0, 470, 162));
            structures.Add(new Structures(3, 0, 624, 160));
            structures.Add(new Structures(3, 0, 738, 155));
            structures.Add(new Structures(3, 0, 9, 328));
            structures.Add(new Structures(3, 0, 115, 322));
            structures.Add(new Structures(3, 0, 230, 319));
            structures.Add(new Structures(3, 0, 343, 317));
            structures.Add(new Structures(3, 0, 443, 312));
            structures.Add(new Structures(3, 0, 579, 302));
            structures.Add(new Structures(3, 0, 687, 300));
            structures.Add(new Structures(1, 0, 54, 386));
            structures.Add(new Structures(1, 0, 134, 386));
            structures.Add(new Structures(1, 0, 214, 386));
            structures.Add(new Structures(1, 0, 294, 386));
            structures.Add(new Structures(1, 0, 374, 386));
            structures.Add(new Structures(1, 0, 454, 386));
            structures.Add(new Structures(1, 0, 534, 386));
            structures.Add(new Structures(1, 0, 614, 386));
            structures.Add(new Structures(1, 0, 694, 386));
            structures.Add(new Structures(1, 0, 50, 525));
            structures.Add(new Structures(1, 0, 130, 525));
            structures.Add(new Structures(1, 0, 210, 525));
            structures.Add(new Structures(1, 0, 290, 525));
            structures.Add(new Structures(1, 0, 370, 525));
            structures.Add(new Structures(1, 0, 450, 525));
            structures.Add(new Structures(1, 0, 530, 525));
            structures.Add(new Structures(1, 0, 610, 525));
            structures.Add(new Structures(1, 0, 690, 525));

            wildgrass.Add(new WildGrass(68, 425));
            wildgrass.Add(new WildGrass(68, 465));
            wildgrass.Add(new WildGrass(68, 505));
            wildgrass.Add(new WildGrass(108, 505));
            wildgrass.Add(new WildGrass(148, 505));
            wildgrass.Add(new WildGrass(188, 505));
            wildgrass.Add(new WildGrass(228, 505));
            wildgrass.Add(new WildGrass(268, 505));
            wildgrass.Add(new WildGrass(308, 505));
            wildgrass.Add(new WildGrass(348, 505));
            wildgrass.Add(new WildGrass(388, 505));
            wildgrass.Add(new WildGrass(428, 505));
            wildgrass.Add(new WildGrass(468, 505));
            wildgrass.Add(new WildGrass(508, 505));
            wildgrass.Add(new WildGrass(548, 505));
            wildgrass.Add(new WildGrass(588, 505));
            wildgrass.Add(new WildGrass(628, 505));
            wildgrass.Add(new WildGrass(668, 505));
            wildgrass.Add(new WildGrass(708, 505));
            wildgrass.Add(new WildGrass(708, 465));
            wildgrass.Add(new WildGrass(708, 425));
            wildgrass.Add(new WildGrass(668, 425));
            wildgrass.Add(new WildGrass(628, 425));
            wildgrass.Add(new WildGrass(588, 425));
            wildgrass.Add(new WildGrass(548, 425));
            wildgrass.Add(new WildGrass(508, 425));
            wildgrass.Add(new WildGrass(468, 425));
            wildgrass.Add(new WildGrass(428, 425));
            wildgrass.Add(new WildGrass(388, 425));
            wildgrass.Add(new WildGrass(348, 425));
            wildgrass.Add(new WildGrass(308, 425));
            wildgrass.Add(new WildGrass(268, 425));
            wildgrass.Add(new WildGrass(228, 425));
            wildgrass.Add(new WildGrass(188, 425));
            wildgrass.Add(new WildGrass(148, 425));
            wildgrass.Add(new WildGrass(108, 425));
            wildgrass.Add(new WildGrass(108, 465));
            wildgrass.Add(new WildGrass(148, 465));
            wildgrass.Add(new WildGrass(188, 465));
            wildgrass.Add(new WildGrass(228, 465));
            wildgrass.Add(new WildGrass(268, 465));
            wildgrass.Add(new WildGrass(308, 465));
            wildgrass.Add(new WildGrass(348, 465));
            wildgrass.Add(new WildGrass(388, 465));
            wildgrass.Add(new WildGrass(428, 465));
            wildgrass.Add(new WildGrass(468, 465));
            wildgrass.Add(new WildGrass(508, 465));
            wildgrass.Add(new WildGrass(548, 465));
            wildgrass.Add(new WildGrass(588, 465));
            wildgrass.Add(new WildGrass(628, 465));
            wildgrass.Add(new WildGrass(668, 465));

            #endregion
        }

        private void LoadMap45()
        {
            pbCanvas.BackgroundImage = Resources.ground;

            exit.Add(new ExitPoint(3, 3, 45, 588, 44, 694, 239));

            if (!pokemonDefeated[488])
            {
                mainForm.ShowMessage("Tobias: Quick! Defeat Cresselia, Darkrai is mine!");

                characters.Add(new Character("Tobias", Resources.character_tobias, 486, 206));

                wildpokemons.Add(new Pokemons(488, 65, 393, 110));
                actionpoint.Add(new ActionPoint(373, 100, 453, 160, 100));

                wildpokemons.Add(new Pokemons(491, 25, 510, 116));
            }

            #region Environment

            structures.Add(new Structures(3, 0, 713, 16));
            structures.Add(new Structures(3, 0, 523, 12));
            structures.Add(new Structures(3, 0, 554, 215));
            structures.Add(new Structures(3, 0, 384, 389));
            structures.Add(new Structures(3, 0, 240, 332));
            structures.Add(new Structures(3, 0, 228, 130));
            structures.Add(new Structures(3, 0, 514, 293));
            structures.Add(new Structures(3, 0, 666, 115));
            structures.Add(new Structures(3, 0, 691, 332));
            structures.Add(new Structures(3, 0, 644, 448));
            structures.Add(new Structures(3, 0, 535, 454));
            structures.Add(new Structures(3, 0, 337, 444));
            structures.Add(new Structures(3, 0, 190, 431));
            structures.Add(new Structures(3, 0, 32, 478));
            structures.Add(new Structures(3, 0, 276, 478));
            structures.Add(new Structures(3, 0, 94, 272));
            structures.Add(new Structures(3, 0, 95, 377));
            structures.Add(new Structures(3, 0, 90, 140));
            structures.Add(new Structures(3, 0, 166, 21));
            structures.Add(new Structures(3, 0, 360, 210));
            structures.Add(new Structures(3, 0, 436, 466));
            structures.Add(new Structures(3, 0, 663, 200));
            structures.Add(new Structures(3, 0, 714, 461));
            structures.Add(new Structures(3, 0, 411, 14));
            structures.Add(new Structures(3, 0, 579, 505));
            structures.Add(new Structures(3, 0, 279, 45));
            structures.Add(new Structures(3, 0, 598, 94));

            #endregion
        }

        private void LoadMap46()
        {
            pbCanvas.BackgroundImage = Resources.ancientcave;

            exit.Add(new ExitPoint(399, 102, 473, 170, 47, 605, 305));

            mainForm.ShowMessage("This can't be good...");

            blockedzone.Add(new BlockedZone(1, 0, 790, 100));
            blockedzone.Add(new BlockedZone(481, 89, 787, 189));
            blockedzone.Add(new BlockedZone(629, 192, 792, 232));
            blockedzone.Add(new BlockedZone(364, 230, 791, 255));
            blockedzone.Add(new BlockedZone(682, 258, 790, 593));
            blockedzone.Add(new BlockedZone(657, 300, 678, 587));
            blockedzone.Add(new BlockedZone(607, 377, 649, 583));
            blockedzone.Add(new BlockedZone(598, 577, 3, 593));
            blockedzone.Add(new BlockedZone(1, 571, 602, 592));
            blockedzone.Add(new BlockedZone(5, 499, 289, 562));
            blockedzone.Add(new BlockedZone(0, 102, 112, 492));
            blockedzone.Add(new BlockedZone(116, 102, 236, 374));
            blockedzone.Add(new BlockedZone(120, 375, 164, 399));
            blockedzone.Add(new BlockedZone(116, 400, 516, 423));
            blockedzone.Add(new BlockedZone(483, 312, 519, 415));
            blockedzone.Add(new BlockedZone(450, 310, 486, 346));
            blockedzone.Add(new BlockedZone(354, 244, 392, 339));
            blockedzone.Add(new BlockedZone(237, 100, 300, 218));
            blockedzone.Add(new BlockedZone(319, 102, 391, 145));
        }

        private void LoadMap47()
        {
            pbCanvas.BackgroundImage = Resources.mountain;

            if (!pokemonDefeated[249])
            {
                wildpokemons.Add(new Pokemons(144, 40, 155, 136));
                actionpoint.Add(new ActionPoint(145, 124, 215, 184, 100));

                wildpokemons.Add(new Pokemons(145, 40, 311, 136));
                actionpoint.Add(new ActionPoint(301, 126, 371, 186, 101));

                wildpokemons.Add(new Pokemons(146, 40, 455, 136));
                actionpoint.Add(new ActionPoint(445, 127, 515, 187, 102));

                wildpokemons.Add(new Pokemons(249, 65, 619, 136));
                actionpoint.Add(new ActionPoint(609, 126, 679, 186, 103));
            }
        }

        private void LoadMap48()
        {
            pbCanvas.BackgroundImage = Resources.mountain;

            exit.Add(new ExitPoint(290, 3, 515, 48, 49, 385, 515));

            characters.Add(new Character("Explorer", Resources.character_explorer, 384, 277));
            characters[0].pokemons.Add(new Pokemons(169, 40));
            characters[0].pokemons.Add(new Pokemons(41, 21));
            characters[0].pokemons.Add(new Pokemons(42, 30));
            characters[0].index = 110;
            actionpoint.Add(new ActionPoint(374, 267, 444, 327, 0));

            characters.Add(new Character("Explorer", Resources.character_explorer, 586, 176));
            characters[1].pokemons.Add(new Pokemons(208, 45));
            characters[1].pokemons.Add(new Pokemons(75, 28));
            characters[1].pokemons.Add(new Pokemons(76, 36));
            characters[1].index = 111;
            actionpoint.Add(new ActionPoint(576, 166, 646, 226, 1));

            characters.Add(new Character("Explorer", Resources.character_explorer, 117, 401));
            characters[2].pokemons.Add(new Pokemons(111, 41));
            characters[2].pokemons.Add(new Pokemons(112, 42));
            characters[2].index = 112;
            actionpoint.Add(new ActionPoint(107, 391, 177, 451, 2));

            characters.Add(new Character("Explorer", Resources.character_explorer, 136, 164));
            characters[3].pokemons.Add(new Pokemons(231, 24));
            characters[3].pokemons.Add(new Pokemons(232, 50));
            characters[3].index = 113;
            actionpoint.Add(new ActionPoint(126, 154, 196, 214, 3));

            characters.Add(new Character("Explorer", Resources.character_explorer, 439, 439));
            characters[4].pokemons.Add(new Pokemons(104, 27));
            characters[4].pokemons.Add(new Pokemons(105, 44));
            characters[4].index = 114;
            actionpoint.Add(new ActionPoint(429, 429, 499, 489, 4));

            characters.Add(new Character("Explorer", Resources.character_explorer, 671, 59));
            characters[5].pokemons.Add(new Pokemons(323, 38));
            characters[5].pokemons.Add(new Pokemons(219, 42));
            characters[5].pokemons.Add(new Pokemons(324, 47));
            characters[5].index = 115;
            actionpoint.Add(new ActionPoint(661, 49, 731, 109, 5));

            #region Environment

            structures.Add(new Structures(1, 0, 239, 3));
            structures.Add(new Structures(1, 0, 159, 3));
            structures.Add(new Structures(1, 0, 79, 3));
            structures.Add(new Structures(1, 0, -1, 3));
            structures.Add(new Structures(1, 0, 479, 3));
            structures.Add(new Structures(1, 0, 559, 3));
            structures.Add(new Structures(1, 0, 639, 3));
            structures.Add(new Structures(1, 0, 719, 3));

            trees.Add(new Trees(1, 41, 96));
            trees.Add(new Trees(1, 524, 131));
            trees.Add(new Trees(1, 667, 236));
            trees.Add(new Trees(1, 180, 247));
            trees.Add(new Trees(1, 9, 311));
            trees.Add(new Trees(1, 230, 441));
            trees.Add(new Trees(1, 683, 478));

            buildings.Add(new Buildings(0, 306, 75, true));
            buildings.Add(new Buildings(-1, 562, 332, true));

            hills.Add(new Hills(336, 370, 149));
            hills.Add(new Hills(74, 495, 117));
            hills.Add(new Hills(684, 170, 77));

            #endregion
        }

        private void LoadMap49()
        {
            pbCanvas.BackgroundImage = Resources.townpavement;

            player.healingcenter = 49;

            int medianlvl = 0;
            for (int i = 0; i < player.pokemons.Count; ++i)
                medianlvl += player.pokemons[i].level;
            medianlvl /= player.pokemons.Count;

            characters.Add(new Character("John the Lumberjack", Resources.character_fighter, 90, 245));
            characters[0].pokemons.Add(new Pokemons(182, medianlvl));
            characters[0].pokemons.Add(new Pokemons(455, medianlvl));
            characters[0].pokemons.Add(new Pokemons(421, medianlvl));
            characters[0].index = 117;
            actionpoint.Add(new ActionPoint(80, 238, 140, 320, 0));

            characters.Add(new Character("Desmond", Resources.character_oldman, 170, 245));
            characters[1].pokemons.Add(new Pokemons(438, medianlvl));
            characters[1].pokemons.Add(new Pokemons(185, medianlvl));
            characters[1].pokemons.Add(new Pokemons(299, medianlvl));
            characters[1].index = 117;
            actionpoint.Add(new ActionPoint(160, 233, 220, 320, 1));

            characters.Add(new Character("Magic Stan", Resources.character_magician, 250, 245));
            characters[2].pokemons.Add(new Pokemons(356, medianlvl));
            characters[2].pokemons.Add(new Pokemons(354, medianlvl));
            characters[2].pokemons.Add(new Pokemons(429, medianlvl));
            characters[2].index = 117;
            actionpoint.Add(new ActionPoint(240, 235, 300, 320, 2));

            characters.Add(new Character("Aviana", Resources.character_woman, 330, 245));
            characters[3].pokemons.Add(new Pokemons(282, medianlvl));
            characters[3].pokemons.Add(new Pokemons(65, medianlvl));
            characters[3].pokemons.Add(new Pokemons(97, medianlvl));
            characters[3].index = 117;
            actionpoint.Add(new ActionPoint(320, 233, 380, 320, 3));

            characters.Add(new Character("Farmer", Resources.character_farmer, 410, 245));
            characters[4].pokemons.Add(new Pokemons(68, medianlvl));
            characters[4].pokemons.Add(new Pokemons(297, medianlvl));
            characters[4].pokemons.Add(new Pokemons(237, medianlvl));
            characters[4].index = 117;
            actionpoint.Add(new ActionPoint(400, 233, 460, 320, 4));

            characters.Add(new Character("Isolde", Resources.character_woman2, 490, 245));
            characters[5].pokemons.Add(new Pokemons(186, medianlvl));
            characters[5].pokemons.Add(new Pokemons(184, medianlvl));
            characters[5].pokemons.Add(new Pokemons(55, medianlvl));
            characters[5].index = 117;
            actionpoint.Add(new ActionPoint(480, 234, 540, 320, 5));

            characters.Add(new Character("Charles&Clotilde Dubois", Resources.character_spouses, 570, 245));
            characters[6].pokemons.Add(new Pokemons(59, medianlvl));
            characters[6].pokemons.Add(new Pokemons(38, medianlvl));
            characters[6].pokemons.Add(new Pokemons(78, medianlvl));
            characters[6].index = 117;
            actionpoint.Add(new ActionPoint(560, 235, 620, 320, 6));

            characters.Add(new Character("Edmund the Wise", Resources.character_oldman3, 652, 245));
            characters[7].pokemons.Add(new Pokemons(372, medianlvl));
            characters[7].pokemons.Add(new Pokemons(147, medianlvl));
            characters[7].pokemons.Add(new Pokemons(148, medianlvl));
            characters[7].index = 117;
            actionpoint.Add(new ActionPoint(642, 236, 712, 320, 7));

            characters.Add(new Character("Official", Resources.character_official, 320, 389));
            actionpoint.Add(new ActionPoint(310, 379, 380, 439, -27));

            characters.Add(new Character("Official", Resources.character_official, 554, 360));
            actionpoint.Add(new ActionPoint(544, 350, 614, 410, -28));

            characters.Add(new Character("Official", Resources.character_official, 154, 376));
            actionpoint.Add(new ActionPoint(144, 366, 214, 426, -29));

            #region Environment

            trees.Add(new Trees(1, 253, 83));
            trees.Add(new Trees(1, 353, 83));
            trees.Add(new Trees(1, 453, 83));

            structures.Add(new Structures(1, 0, 1, 543));
            structures.Add(new Structures(1, 0, 81, 543));
            structures.Add(new Structures(1, 0, 161, 543));
            structures.Add(new Structures(1, 0, 241, 543));
            structures.Add(new Structures(1, 0, 481, 543));
            structures.Add(new Structures(1, 0, 561, 543));
            structures.Add(new Structures(1, 0, 641, 543));
            structures.Add(new Structures(1, 0, 721, 543));
            structures.Add(new Structures(2, 0, 124, 414));
            structures.Add(new Structures(2, 0, 653, 412));

            buildings.Add(new Buildings(1, 58, 41, true));
            buildings.Add(new Buildings(9, 540, 41, true));

            #endregion
        }

        private void LoadWater()
        {
            wildgrass.Add(new WildGrass(55, 31));
            wildgrass.Add(new WildGrass(55, 111));
            wildgrass.Add(new WildGrass(55, 191));
            wildgrass.Add(new WildGrass(55, 271));
            wildgrass.Add(new WildGrass(55, 351));
            wildgrass.Add(new WildGrass(55, 431));
            wildgrass.Add(new WildGrass(55, 511));
            wildgrass.Add(new WildGrass(95, 471));
            wildgrass.Add(new WildGrass(95, 391));
            wildgrass.Add(new WildGrass(95, 311));
            wildgrass.Add(new WildGrass(95, 231));
            wildgrass.Add(new WildGrass(95, 151));
            wildgrass.Add(new WildGrass(95, 71));
            wildgrass.Add(new WildGrass(135, 31));
            wildgrass.Add(new WildGrass(135, 111));
            wildgrass.Add(new WildGrass(135, 191));
            wildgrass.Add(new WildGrass(135, 271));
            wildgrass.Add(new WildGrass(135, 351));
            wildgrass.Add(new WildGrass(135, 431));
            wildgrass.Add(new WildGrass(135, 511));
            wildgrass.Add(new WildGrass(175, 471));
            wildgrass.Add(new WildGrass(175, 391));
            wildgrass.Add(new WildGrass(175, 311));
            wildgrass.Add(new WildGrass(175, 231));
            wildgrass.Add(new WildGrass(175, 151));
            wildgrass.Add(new WildGrass(175, 71));
            wildgrass.Add(new WildGrass(215, 31));
            wildgrass.Add(new WildGrass(295, 31));
            wildgrass.Add(new WildGrass(375, 31));
            wildgrass.Add(new WildGrass(455, 31));
            wildgrass.Add(new WildGrass(535, 31));
            wildgrass.Add(new WildGrass(615, 31));
            wildgrass.Add(new WildGrass(695, 31));
            wildgrass.Add(new WildGrass(695, 111));
            wildgrass.Add(new WildGrass(695, 191));
            wildgrass.Add(new WildGrass(695, 271));
            wildgrass.Add(new WildGrass(695, 351));
            wildgrass.Add(new WildGrass(695, 431));
            wildgrass.Add(new WildGrass(695, 511));
            wildgrass.Add(new WildGrass(615, 511));
            wildgrass.Add(new WildGrass(535, 511));
            wildgrass.Add(new WildGrass(455, 511));
            wildgrass.Add(new WildGrass(375, 511));
            wildgrass.Add(new WildGrass(295, 511));
            wildgrass.Add(new WildGrass(215, 511));
            wildgrass.Add(new WildGrass(215, 431));
            wildgrass.Add(new WildGrass(215, 351));
            wildgrass.Add(new WildGrass(215, 271));
            wildgrass.Add(new WildGrass(215, 191));
            wildgrass.Add(new WildGrass(215, 111));
            wildgrass.Add(new WildGrass(255, 71));
            wildgrass.Add(new WildGrass(335, 71));
            wildgrass.Add(new WildGrass(415, 71));
            wildgrass.Add(new WildGrass(495, 71));
            wildgrass.Add(new WildGrass(575, 71));
            wildgrass.Add(new WildGrass(655, 71));
            wildgrass.Add(new WildGrass(655, 151));
            wildgrass.Add(new WildGrass(655, 231));
            wildgrass.Add(new WildGrass(655, 311));
            wildgrass.Add(new WildGrass(655, 391));
            wildgrass.Add(new WildGrass(655, 471));
            wildgrass.Add(new WildGrass(575, 471));
            wildgrass.Add(new WildGrass(495, 471));
            wildgrass.Add(new WildGrass(415, 471));
            wildgrass.Add(new WildGrass(335, 471));
            wildgrass.Add(new WildGrass(255, 471));
            wildgrass.Add(new WildGrass(255, 391));
            wildgrass.Add(new WildGrass(255, 311));
            wildgrass.Add(new WildGrass(255, 231));
            wildgrass.Add(new WildGrass(255, 151));
            wildgrass.Add(new WildGrass(295, 111));
            wildgrass.Add(new WildGrass(375, 111));
            wildgrass.Add(new WildGrass(455, 111));
            wildgrass.Add(new WildGrass(535, 111));
            wildgrass.Add(new WildGrass(615, 111));
            wildgrass.Add(new WildGrass(615, 191));
            wildgrass.Add(new WildGrass(615, 271));
            wildgrass.Add(new WildGrass(615, 351));
            wildgrass.Add(new WildGrass(615, 431));
            wildgrass.Add(new WildGrass(535, 431));
            wildgrass.Add(new WildGrass(455, 431));
            wildgrass.Add(new WildGrass(375, 431));
            wildgrass.Add(new WildGrass(295, 431));
            wildgrass.Add(new WildGrass(335, 391));
            wildgrass.Add(new WildGrass(415, 391));
            wildgrass.Add(new WildGrass(495, 391));
            wildgrass.Add(new WildGrass(575, 391));
            wildgrass.Add(new WildGrass(575, 311));
            wildgrass.Add(new WildGrass(575, 231));
            wildgrass.Add(new WildGrass(575, 151));
            wildgrass.Add(new WildGrass(495, 151));
            wildgrass.Add(new WildGrass(415, 151));
            wildgrass.Add(new WildGrass(335, 151));
            wildgrass.Add(new WildGrass(295, 191));
            wildgrass.Add(new WildGrass(295, 271));
            wildgrass.Add(new WildGrass(295, 351));
            wildgrass.Add(new WildGrass(375, 351));
            wildgrass.Add(new WildGrass(455, 351));
            wildgrass.Add(new WildGrass(535, 351));
            wildgrass.Add(new WildGrass(535, 271));
            wildgrass.Add(new WildGrass(535, 191));
            wildgrass.Add(new WildGrass(455, 191));
            wildgrass.Add(new WildGrass(375, 191));
            wildgrass.Add(new WildGrass(335, 231));
            wildgrass.Add(new WildGrass(335, 311));
            wildgrass.Add(new WildGrass(415, 311));
            wildgrass.Add(new WildGrass(495, 311));
            wildgrass.Add(new WildGrass(495, 231));
            wildgrass.Add(new WildGrass(415, 231));
            wildgrass.Add(new WildGrass(375, 271));
            wildgrass.Add(new WildGrass(455, 271));
        }

        int[] legendaries = new int[] { 151, 243, 244, 245, 251, 382, 383, 384, 385, 483, 484, 487, 492, 493 };
        int legendariesCount = -1;
        Timer legendariesTimer;

        private void LoadMap50()
        {
            pbCanvas.BackgroundImage = null;
            pbCanvas.BackColor = Color.Black;

            legendariesCount = -1;
            legendariesTimer = new Timer();
            legendariesTimer.Interval = 1000;
            legendariesTimer.Tick += legendariesTimer_Tick;
            legendariesTimer.Start();
        }

        void legendariesTimer_Tick(object sender, EventArgs e)
        {
            legendariesCount += 1;
            pbCanvas.Invalidate();

            if(legendariesCount > 13)
            {
                legendariesTimer.Stop();
                legendariesCount = -2;
                new FormBattle(new Pokemons(legendaries[rand.Next() % 14], 65), true).ShowDialog();
            }
        }

        #endregion

        #region Load Buildings Insides

        private void LoadHealingCenter(int mapNr)
        {
            pbCanvas.BackgroundImage = Resources.healingcenterInside;

            player.healingcenter = mapNr;

            FormGame.OX = 377;
            FormGame.OY = 336;

            int X = 0, Y = 0;

            if (mapNr == 1) { X = 475; Y = 248; }
            if (mapNr == 5) { X = 492; Y = 366; }
            if (mapNr == 8) { X = 142; Y = 501; }
            if (mapNr == 14) { X = 607; Y = 381; }
            if (mapNr == 20) { X = 470; Y = 170; }
            if (mapNr == 24) { X = 100; Y = 260; }
            if (mapNr == 29) { X = 560; Y = 262; }
            if (mapNr == 36) { X = 110; Y = 215; }
            if (mapNr == 40) { X = 110; Y = 120; }
            if (mapNr == 49) { X = 105; Y = 165; }

            exit.Add(new ExitPoint(380, 410, 460, 440, mapNr, X, Y));

            blockedzone.Add(new BlockedZone(0, 0, 150, 600));
            blockedzone.Add(new BlockedZone(650, 0, 800, 600));
            blockedzone.Add(new BlockedZone(0, 0, 800, 200));
            blockedzone.Add(new BlockedZone(0, 425, 800, 600));

            blockedzone.Add(new BlockedZone(300, 200, 500, 250));
            blockedzone.Add(new BlockedZone(560, 340, 600, 360));

            actionpoint.Add(new ActionPoint(405, 260, 415, 275, -1));
            actionpoint.Add(new ActionPoint(500, 200, 520, 230, -11));
        }

        private void LoadShop(int mapNr)
        {
            pbCanvas.BackgroundImage = Resources.shopInside;

            FormGame.OX = 325;
            FormGame.OY = 330;

            int X = 0, Y = 0;

            if (mapNr == 5) { X = 499; Y = 547; }
            if (mapNr == 8) { X = 607; Y = 501; }
            if (mapNr == 26) { X = 530; Y = 230; }
            if (mapNr == 36) { X = 110; Y = 410; }
            if (mapNr == 40) { X = 605; Y = 120; }

            exit.Add(new ExitPoint(300, 410, 380, 440, mapNr, X, Y));

            blockedzone.Add(new BlockedZone(0, 0, 150, 600));
            blockedzone.Add(new BlockedZone(650, 0, 800, 600));
            blockedzone.Add(new BlockedZone(0, 0, 800, 200));
            blockedzone.Add(new BlockedZone(0, 425, 800, 600));

            blockedzone.Add(new BlockedZone(0, 0, 280, 300));
            blockedzone.Add(new BlockedZone(435, 315, 505, 355));
            blockedzone.Add(new BlockedZone(605, 255, 620, 355));

            actionpoint.Add(new ActionPoint(281, 252, 320, 299, -2));
        }

        private void LoadGym(int mapNr)
        {
            pbCanvas.BackgroundImage = Resources.gymInside;

            FormGame.OX = 400;
            FormGame.OY = 330;

            int X = 0, Y = 0;

            if (mapNr == 5) { X = 115; Y = 340; }
            if (mapNr == 8) { X = 382; Y = 501; }
            if (mapNr == 14) { X = 622; Y = 201; }
            if (mapNr == 20) { X = 500; Y = 425; }
            if (mapNr == 24) { X = 370; Y = 260; }
            if (mapNr == 29) { X = 560; Y = 442; }
            if (mapNr == 36) { X = 595; Y = 320; }
            if (mapNr == 40) { X = 380; Y = 120; }
            if (mapNr == 48) { X = 300; Y = 195; }

            exit.Add(new ExitPoint(380, 410, 500, 440, mapNr, X, Y));

            blockedzone.Add(new BlockedZone(0, 0, 150, 600));
            blockedzone.Add(new BlockedZone(650, 0, 800, 600));
            blockedzone.Add(new BlockedZone(0, 0, 800, 200));
            blockedzone.Add(new BlockedZone(0, 425, 800, 600));

            if (mapNr == 5)
            {
                characters.Add(new Character("John the Lumberjack", Resources.character_fighter, 390, 250));
                characters[0].index = 9;
                actionpoint.Add(new ActionPoint(370, 240, 440, 300, -3));

                Pokemons poke = new Pokemons(285, 10);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(71));
                poke.moves.Add(new Attack(73));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(163, 13);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(33));
                poke.moves.Add(new Attack(95));
                poke.moves.Add(new Attack(64));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(345, 15);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(331));
                poke.moves.Add(new Attack(317));
                poke.moves.Add(new Attack(156));
                characters[0].pokemons.Add(poke);
            }

            if (mapNr == 8)
            {
                characters.Add(new Character("Desmond", Resources.character_oldman, 390, 250));
                characters[0].index = 19;
                actionpoint.Add(new ActionPoint(370, 240, 440, 300, -4));

                Pokemons poke = new Pokemons(27, 14);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(229));
                poke.moves.Add(new Attack(111));
                poke.moves.Add(new Attack(205));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(231, 15);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(36));
                poke.moves.Add(new Attack(111));
                poke.moves.Add(new Attack(205));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(215, 18);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(98));
                poke.moves.Add(new Attack(420));
                poke.moves.Add(new Attack(103));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(34, 20);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(341));
                poke.moves.Add(new Attack(24));
                poke.moves.Add(new Attack(46));
                characters[0].pokemons.Add(poke);
            }

            if (mapNr == 14)
            {
                characters.Add(new Character("Magic Stan", Resources.character_magician, 390, 250));
                characters[0].index = 25;
                actionpoint.Add(new ActionPoint(370, 240, 440, 300, -5));

                Pokemons poke = new Pokemons(408, 20);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(428));
                poke.moves.Add(new Attack(36));
                poke.moves.Add(new Attack(43));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(302, 23);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(425));
                poke.moves.Add(new Attack(185));
                poke.moves.Add(new Attack(43));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(429, 25);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(310));
                poke.moves.Add(new Attack(149));
                poke.moves.Add(new Attack(109));
                characters[0].pokemons.Add(poke);
            }

            if (mapNr == 20)
            {
                characters.Add(new Character("Aviana", Resources.character_woman, 390, 250));
                characters[0].index = 64;
                actionpoint.Add(new ActionPoint(370, 240, 440, 300, -6));

                Pokemons poke = new Pokemons(84, 24);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(98));
                poke.moves.Add(new Attack(228));
                poke.moves.Add(new Attack(367));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(164, 26);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(36));
                poke.moves.Add(new Attack(93));
                poke.moves.Add(new Attack(95));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(178, 26);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(100));
                poke.moves.Add(new Attack(94));
                poke.moves.Add(new Attack(101));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(357, 28);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(75));
                poke.moves.Add(new Attack(403));
                poke.moves.Add(new Attack(18));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(227, 28);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(314));
                poke.moves.Add(new Attack(211));
                poke.moves.Add(new Attack(97));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(430, 30);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(17));
                poke.moves.Add(new Attack(114));
                poke.moves.Add(new Attack(399));
                characters[0].pokemons.Add(poke);
            }

            if (mapNr == 24)
            {
                characters.Add(new Character("Farmer", Resources.character_farmer, 390, 250));
                characters[0].index = 79;
                actionpoint.Add(new ActionPoint(370, 240, 440, 300, -7));

                Pokemons poke = new Pokemons(241, 28);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(208));
                poke.moves.Add(new Attack(34));
                poke.moves.Add(new Attack(428));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(128, 30);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(156));
                poke.moves.Add(new Attack(428));
                poke.moves.Add(new Attack(416));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(326, 33);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(316));
                poke.moves.Add(new Attack(93));
                characters[0].pokemons.Add(poke);
            }

            if (mapNr == 29)
            {
                characters.Add(new Character("Isolde", Resources.character_woman2, 390, 250));
                characters[0].index = 93;
                actionpoint.Add(new ActionPoint(370, 240, 440, 300, -8));

                Pokemons poke = new Pokemons(197, 40);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(185));
                poke.moves.Add(new Attack(103));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(470, 40);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(202));
                poke.moves.Add(new Attack(320));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(471, 40);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(423));
                poke.moves.Add(new Attack(28));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(136, 45);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(424));
                poke.moves.Add(new Attack(28));
                characters[0].pokemons.Add(poke);
            }

            if (mapNr == 36)
            {
                characters.Add(new Character("Charles&Clotilde Dubois", Resources.character_spouses, 390, 250));
                characters[0].index = 99;
                actionpoint.Add(new ActionPoint(370, 240, 440, 300, -9));

                Pokemons poke = new Pokemons(57, 57);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(238));
                poke.moves.Add(new Attack(2));
                poke.moves.Add(new Attack(207));
                poke.moves.Add(new Attack(372));
                characters[0].pokemons.Add(poke); 
                
                poke = new Pokemons(419, 60);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(423));
                poke.moves.Add(new Attack(242));
                poke.moves.Add(new Attack(453));
                poke.moves.Add(new Attack(97));
                characters[0].pokemons.Add(poke); 
                
                poke = new Pokemons(208, 63);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(422));
                poke.moves.Add(new Attack(423));
                poke.moves.Add(new Attack(424));
                poke.moves.Add(new Attack(106));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(437, 66);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(95));
                poke.moves.Add(new Attack(326));
                poke.moves.Add(new Attack(334));
                poke.moves.Add(new Attack(360));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(248, 69);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(444));
                poke.moves.Add(new Attack(89));
                poke.moves.Add(new Attack(242));
                poke.moves.Add(new Attack(184));
                characters[0].pokemons.Add(poke);
            }

            if (mapNr == 40)
            {
                characters.Add(new Character("Giovanni", Resources.character_giovanni, 390, 250));
                characters[0].index = 103;
                actionpoint.Add(new ActionPoint(370, 240, 440, 300, -26));

                Pokemons poke = new Pokemons(99, 65);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(152));
                poke.moves.Add(new Attack(21));
                poke.moves.Add(new Attack(182));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(112, 65);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(36));
                poke.moves.Add(new Attack(30));
                poke.moves.Add(new Attack(184));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(68, 65);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(238));
                poke.moves.Add(new Attack(279));
                poke.moves.Add(new Attack(116));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(76, 65);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(38));
                poke.moves.Add(new Attack(222));
                poke.moves.Add(new Attack(397));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(91, 65);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(56));
                poke.moves.Add(new Attack(48));
                poke.moves.Add(new Attack(62));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(150, 70);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(396));
                poke.moves.Add(new Attack(105));
                poke.moves.Add(new Attack(94));
                characters[0].pokemons.Add(poke);
            }

            if (mapNr == 48)
            {
                characters.Add(new Character("Edmund the Wise", Resources.character_oldman3, 390, 250));
                characters[0].index = 116;
                actionpoint.Add(new ActionPoint(370, 240, 440, 300, -10));

                Pokemons poke = new Pokemons(330, 70);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(225));
                poke.moves.Add(new Attack(103));
                poke.moves.Add(new Attack(185));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(445, 70);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(91));
                poke.moves.Add(new Attack(337));
                poke.moves.Add(new Attack(424));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(373, 70);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(182));
                poke.moves.Add(new Attack(242));
                poke.moves.Add(new Attack(225));
                poke.moves.Add(new Attack(422));
                characters[0].pokemons.Add(poke);
            }
        }

        private void LoadHauntedHouse(int mapNr)
        {
            pbCanvas.BackgroundImage = Resources.hauntedhouseInside;

            wildPokemons.Add(new ints(92, 6, 12, 23));
            wildPokemons.Add(new ints(200, 6, 12, 5));
            wildPokemons.Add(new ints(302, 6, 12, 5));
            wildPokemons.Add(new ints(353, 6, 12, 27));
            wildPokemons.Add(new ints(355, 6, 12, 23));
            wildPokemons.Add(new ints(442, 8, 12, 1));
            wildPokemons.Add(new ints(479, 8, 12, 1));
            wildPokemons.Add(new ints(425, 6, 12, 15));

            characters.Add(new Character("Old woman", Resources.character_oldwoman, 160, 220));
            characters[0].pokemons.Add(new Pokemons(353, 12));
            characters[0].pokemons.Add(new Pokemons(92, 12));
            characters[0].pokemons.Add(new Pokemons(355, 12));
            characters[0].pokemons.Add(new Pokemons(425, 12));
            characters[0].pokemons.Add(new Pokemons(200, 12));
            characters[0].pokemons.Add(new Pokemons(302, 12));
            characters[0].index = 20;
            actionpoint.Add(new ActionPoint(140, 200, 210, 270, 0));

            FormGame.OX = 580;
            FormGame.OY = 250;

            Keyboard.ChangeDir(2, true);
            player.ChangeSprite();
            Keyboard.ClearStack();

            bool pokemonEscaped = false;

            if (player.pokemons.Count > 1)
                for (int i = 0; i < player.pokemons.Count; ++i)
                {
                    Pokemons poke = player.pokemons[i];

                    if (poke.index == 93 || poke.index == 94)
                    {
                        mainForm.ShowMessage(poke.name + " broke free from your team!");
                        pokemonEscaped = true;
                        player.pokemons.RemoveAt(i);
                        break;
                    }
                }

            for (int i = 0; i < player.pokemonsLab.Count; ++i)
            {
                Pokemons poke = player.pokemonsLab[i];

                if (poke.index == 93 || poke.index == 94)
                {
                    mainForm.ShowMessage(poke.name + " broke free from the laboratory!");
                    pokemonEscaped = true;
                    player.pokemonsLab.RemoveAt(i);
                    break;
                }
            }

            if (!pokemonEscaped) mainForm.ShowMessage("Spooky...");

            exit.Add(new ExitPoint(600, 180, 640, 260, mapNr, 637, 141));

            blockedzone.Add(new BlockedZone(0, 0, 150, 600));
            blockedzone.Add(new BlockedZone(650, 0, 800, 600));
            blockedzone.Add(new BlockedZone(0, 0, 800, 200));
            blockedzone.Add(new BlockedZone(0, 425, 800, 600));

            wildgrass.Add(new WildGrass(235, 250));
            wildgrass.Add(new WildGrass(275, 250));
            wildgrass.Add(new WildGrass(315, 250));
            wildgrass.Add(new WildGrass(355, 250));
            wildgrass.Add(new WildGrass(395, 250));
            wildgrass.Add(new WildGrass(435, 250));

            wildgrass.Add(new WildGrass(235, 290));
            wildgrass.Add(new WildGrass(275, 290));
            wildgrass.Add(new WildGrass(315, 290));
            wildgrass.Add(new WildGrass(355, 290));
            wildgrass.Add(new WildGrass(395, 290));
            wildgrass.Add(new WildGrass(435, 290));

            wildgrass.Add(new WildGrass(235, 330));
            wildgrass.Add(new WildGrass(275, 330));
            wildgrass.Add(new WildGrass(315, 330));
            wildgrass.Add(new WildGrass(355, 330));
            wildgrass.Add(new WildGrass(395, 330));
            wildgrass.Add(new WildGrass(435, 330));

            wildgrass.Add(new WildGrass(235, 370));
            wildgrass.Add(new WildGrass(275, 370));
            wildgrass.Add(new WildGrass(315, 370));
            wildgrass.Add(new WildGrass(355, 370));
            wildgrass.Add(new WildGrass(395, 370));
            wildgrass.Add(new WildGrass(435, 370));
        }

        private void LoadCave(int mapNr)
        {
            pbCanvas.BackgroundImage = Resources.caveInside;

            int Xenter = 0, Yenter = 0;
            int Xexit = 0, Yexit = 0;
            int mapenter = 0, mapexit = 0;

            if (mapNr == 13 || mapNr == 14)
            {
                mapenter = 13;
                mapexit = 14;
                Xenter = 622; Yenter = 191;
                Xexit = 142; Yexit = 191;

                wildPokemons.Add(new ints(41, 7, 12, 60));
                wildPokemons.Add(new ints(95, 9, 12, 2));
                wildPokemons.Add(new ints(293, 7, 12, 10));
                wildPokemons.Add(new ints(206, 7, 12, 8));
                wildPokemons.Add(new ints(50, 7, 12, 20));
            }

            if (mapNr == 22 || mapNr == 23)
            {
                mapenter = 22;
                mapexit = 23;
                Xenter = 510; Yenter = 460;
                Xexit = 580; Yexit = 340;

                wildPokemons.Add(new ints(41, 14, 20, 50));
                wildPokemons.Add(new ints(293, 14, 20, 25));
                wildPokemons.Add(new ints(337, 14, 20, 6));
                wildPokemons.Add(new ints(338, 14, 20, 6));
                wildPokemons.Add(new ints(299, 14, 20, 10));
                wildPokemons.Add(new ints(303, 14, 20, 3));
            }

            if (mapNr == 30 || mapNr == 32)
            {
                mapenter = 30;
                mapexit = 32;
                Xenter = 540; Yenter = 260;
                Xexit = 150; Yexit = 240;

                wildPokemons.Add(new ints(220, 22, 30, 20));
                wildPokemons.Add(new ints(361, 22, 30, 10));
                wildPokemons.Add(new ints(225, 22, 30, 10));
                wildPokemons.Add(new ints(42, 22, 30, 60));
            }

            if (mapNr == 40 || mapNr == 48)
            {
                mapenter = 40;
                mapexit = 48;
                Xenter = 195; Yenter = 360;
                Xexit = 615; Yexit = 450;

                wildPokemons.Add(new ints(42, 22, 30, 99));
                wildPokemons.Add(new ints(485, 50, 50, 1));
            }

            if (mapNr == mapenter)
            {
                FormGame.OX = 450;
                FormGame.OY = 450;
            }
            else
            {
                FormGame.OX = 450;
                FormGame.OY = 75;
                Keyboard.ChangeDir(2, true);
                player.ChangeSprite();
                Keyboard.ClearStack();
            }

            exit.Add(new ExitPoint(433, 520, 519, 545, mapenter, Xenter, Yenter));
            exit.Add(new ExitPoint(433, 50, 518, 100, mapexit, Xexit, Yexit));

            blockedzone.Add(new BlockedZone(2, 0, 411, 160));
            blockedzone.Add(new BlockedZone(543, 1, 794, 160));
            blockedzone.Add(new BlockedZone(0, 440, 406, 592));
            blockedzone.Add(new BlockedZone(537, 440, 792, 594));
            blockedzone.Add(new BlockedZone(412, 0, 540, 41));
            blockedzone.Add(new BlockedZone(406, 558, 536, 591));

            #region Environment

            wildgrass.Add(new WildGrass(47, 189));
            wildgrass.Add(new WildGrass(47, 229));
            wildgrass.Add(new WildGrass(47, 269));
            wildgrass.Add(new WildGrass(47, 309));
            wildgrass.Add(new WildGrass(47, 349));
            wildgrass.Add(new WildGrass(87, 349));
            wildgrass.Add(new WildGrass(127, 349));
            wildgrass.Add(new WildGrass(167, 349));
            wildgrass.Add(new WildGrass(207, 349));
            wildgrass.Add(new WildGrass(247, 349));
            wildgrass.Add(new WildGrass(287, 349));
            wildgrass.Add(new WildGrass(327, 349));
            wildgrass.Add(new WildGrass(367, 349));
            wildgrass.Add(new WildGrass(407, 349));
            wildgrass.Add(new WildGrass(447, 349));
            wildgrass.Add(new WildGrass(487, 349));
            wildgrass.Add(new WildGrass(527, 349));
            wildgrass.Add(new WildGrass(567, 349));
            wildgrass.Add(new WildGrass(607, 349));
            wildgrass.Add(new WildGrass(647, 349));
            wildgrass.Add(new WildGrass(687, 349));
            wildgrass.Add(new WildGrass(687, 309));
            wildgrass.Add(new WildGrass(687, 269));
            wildgrass.Add(new WildGrass(687, 229));
            wildgrass.Add(new WildGrass(687, 189));
            wildgrass.Add(new WildGrass(647, 189));
            wildgrass.Add(new WildGrass(607, 189));
            wildgrass.Add(new WildGrass(567, 189));
            wildgrass.Add(new WildGrass(527, 189));
            wildgrass.Add(new WildGrass(487, 189));
            wildgrass.Add(new WildGrass(447, 189));
            wildgrass.Add(new WildGrass(407, 189));
            wildgrass.Add(new WildGrass(367, 189));
            wildgrass.Add(new WildGrass(327, 189));
            wildgrass.Add(new WildGrass(287, 189));
            wildgrass.Add(new WildGrass(247, 189));
            wildgrass.Add(new WildGrass(207, 189));
            wildgrass.Add(new WildGrass(167, 189));
            wildgrass.Add(new WildGrass(127, 189));
            wildgrass.Add(new WildGrass(87, 189));
            wildgrass.Add(new WildGrass(87, 229));
            wildgrass.Add(new WildGrass(87, 269));
            wildgrass.Add(new WildGrass(87, 309));
            wildgrass.Add(new WildGrass(127, 309));
            wildgrass.Add(new WildGrass(167, 309));
            wildgrass.Add(new WildGrass(207, 309));
            wildgrass.Add(new WildGrass(247, 309));
            wildgrass.Add(new WildGrass(287, 309));
            wildgrass.Add(new WildGrass(327, 309));
            wildgrass.Add(new WildGrass(367, 309));
            wildgrass.Add(new WildGrass(407, 309));
            wildgrass.Add(new WildGrass(447, 309));
            wildgrass.Add(new WildGrass(487, 309));
            wildgrass.Add(new WildGrass(527, 309));
            wildgrass.Add(new WildGrass(567, 309));
            wildgrass.Add(new WildGrass(607, 309));
            wildgrass.Add(new WildGrass(647, 309));
            wildgrass.Add(new WildGrass(647, 269));
            wildgrass.Add(new WildGrass(647, 229));
            wildgrass.Add(new WildGrass(607, 229));
            wildgrass.Add(new WildGrass(567, 229));
            wildgrass.Add(new WildGrass(527, 229));
            wildgrass.Add(new WildGrass(487, 229));
            wildgrass.Add(new WildGrass(447, 229));
            wildgrass.Add(new WildGrass(407, 229));
            wildgrass.Add(new WildGrass(367, 229));
            wildgrass.Add(new WildGrass(327, 229));
            wildgrass.Add(new WildGrass(287, 229));
            wildgrass.Add(new WildGrass(247, 229));
            wildgrass.Add(new WildGrass(207, 229));
            wildgrass.Add(new WildGrass(167, 229));
            wildgrass.Add(new WildGrass(127, 229));
            wildgrass.Add(new WildGrass(127, 269));
            wildgrass.Add(new WildGrass(167, 269));
            wildgrass.Add(new WildGrass(207, 269));
            wildgrass.Add(new WildGrass(247, 269));
            wildgrass.Add(new WildGrass(287, 269));
            wildgrass.Add(new WildGrass(327, 269));
            wildgrass.Add(new WildGrass(367, 269));
            wildgrass.Add(new WildGrass(407, 269));
            wildgrass.Add(new WildGrass(447, 269));
            wildgrass.Add(new WildGrass(487, 269));
            wildgrass.Add(new WildGrass(527, 269));
            wildgrass.Add(new WildGrass(567, 269));
            wildgrass.Add(new WildGrass(607, 269));

            #endregion
        }

        private void LoadArena(int mapNr)
        {
            pbCanvas.BackgroundImage = Resources.arenaInside;

            FormGame.OX = 320;
            FormGame.OY = 390;

            exit.Add(new ExitPoint(320, 470, 390, 500, mapNr, 230, 180));

            blockedzone.Add(new BlockedZone(0, 0, 800, 120));
            blockedzone.Add(new BlockedZone(0, 490, 800, 600));

            blockedzone.Add(new BlockedZone(90, 120, 110, 400));
            blockedzone.Add(new BlockedZone(240, 120, 260, 360));
            blockedzone.Add(new BlockedZone(390, 120, 400, 360));
            blockedzone.Add(new BlockedZone(530, 120, 550, 400));
            blockedzone.Add(new BlockedZone(680, 120, 690, 400));

            if (!trainerDefeated[38])
            {
                trainerDefeated[39] = false;
                trainerDefeated[40] = false;
                trainerDefeated[41] = false;
                trainerDefeated[42] = false;
                trainerDefeated[43] = false;

                structures.Add(new Structures(1, 0, 250, 360));
                structures.Add(new Structures(1, 0, 330, 360));

                mainForm.ShowMessage("Lucas the Fighter: Beat all my apprentices before fighting me!");
                actionAfterDialog = 4;
            }

            characters.Add(new Character("Lucas the Fighter", Resources.character_fighter3, 300, 140));
            characters[0].pokemons.Add(new Pokemons(68, player.pokemons[0].level + 3));
            characters[0].index = 38;
            actionpoint.Add(new ActionPoint(280, 120, 350, 220, 0));

            characters.Add(new Character("Apprentice Fighter", Resources.character_fighter2, 35, 160));
            actionpoint.Add(new ActionPoint(15, 140, 85, 210, 1));
            characters[1].index = 39;
            characters[1].pokemons.Add(new Pokemons(57, player.pokemons[0].level));

            characters.Add(new Character("Apprentice Fighter", Resources.character_fighter2, 170, 160));
            actionpoint.Add(new ActionPoint(150, 140, 220, 210, 2));
            characters[2].index = 40;
            characters[2].pokemons.Add(new Pokemons(106, player.pokemons[0].level));

            characters.Add(new Character("Apprentice Fighter", Resources.character_fighter2, 455, 160));
            actionpoint.Add(new ActionPoint(435, 140, 505, 210, 3));
            characters[3].index = 41;
            characters[3].pokemons.Add(new Pokemons(107, player.pokemons[0].level));

            characters.Add(new Character("Apprentice Fighter", Resources.character_fighter2, 590, 160));
            actionpoint.Add(new ActionPoint(570, 140, 640, 210, 4));
            characters[4].index = 42;
            characters[4].pokemons.Add(new Pokemons(237, player.pokemons[0].level));

            characters.Add(new Character("Apprentice Fighter", Resources.character_fighter2, 725, 160));
            actionpoint.Add(new ActionPoint(705, 140, 775, 210, 5));
            characters[5].index = 43;
            characters[5].pokemons.Add(new Pokemons(297, player.pokemons[0].level));
        }

        private List<Pokemons> DinoHouse_GenerateList(int level)
        {
            int[] tmpList = new int[32] { 25, 81, 100, 125, 170, 309, 417, 27, 74, 104, 231, 322, 449, 5, 37, 58, 
                126, 156, 228, 256, 391, 8, 120, 159, 341, 394, 418, 2, 114, 153, 253, 388 };

            List<Pokemons> tmpPokemonList = new List<Pokemons>();
            tmpPokemonList.Add(new Pokemons(tmpList[rand.Next(0, 32)], level));
            tmpPokemonList.Add(new Pokemons(tmpList[rand.Next(0, 32)], level));
            tmpPokemonList.Add(new Pokemons(tmpList[rand.Next(0, 32)], level));

            return tmpPokemonList;
        }

        public static List<Pokemons> temporaryReserve;
        private void LoadDinoHouse(int mapNr)
        {
            pbCanvas.BackgroundImage = Resources.arenaInside;

            temporaryReserve = player.pokemons;
            player.pokemons = DinoHouse_GenerateList(22);

            FormPokedex.SeePokemon(player.pokemons[0].index);
            FormPokedex.SeePokemon(player.pokemons[1].index);
            FormPokedex.SeePokemon(player.pokemons[2].index);

            FormGame.OX = 320;
            FormGame.OY = 390;

            exit.Add(new ExitPoint(320, 470, 390, 500, mapNr, 500, 180));

            blockedzone.Add(new BlockedZone(0, 0, 800, 120));
            blockedzone.Add(new BlockedZone(0, 490, 800, 600));

            blockedzone.Add(new BlockedZone(90, 120, 110, 400));
            blockedzone.Add(new BlockedZone(240, 120, 260, 360));
            blockedzone.Add(new BlockedZone(390, 120, 400, 360));
            blockedzone.Add(new BlockedZone(530, 120, 550, 400));
            blockedzone.Add(new BlockedZone(680, 120, 690, 400));

            if (!trainerDefeated[44])
            {
                trainerDefeated[45] = false;
                trainerDefeated[46] = false;
                trainerDefeated[47] = false;
                trainerDefeated[48] = false;
                trainerDefeated[49] = false;

                structures.Add(new Structures(1, 0, 250, 360));
                structures.Add(new Structures(1, 0, 330, 360));

                mainForm.ShowMessage("Dino Senior: Beat all my children before fighting me!");
                actionAfterDialog = 4;
            }

            characters.Add(new Character("Dino Senior", Resources.character_dino, 300, 140));
            characters[0].index = 44;
            actionpoint.Add(new ActionPoint(280, 120, 350, 220, 0));
            characters[0].pokemons = DinoHouse_GenerateList(20);

            characters.Add(new Character("Dino Jr.", Resources.character_dino, 35, 160));
            actionpoint.Add(new ActionPoint(15, 140, 85, 210, 1));
            characters[1].index = 45;
            characters[1].pokemons = DinoHouse_GenerateList(16);

            characters.Add(new Character("Dino Jr.", Resources.character_dino, 170, 160));
            actionpoint.Add(new ActionPoint(150, 140, 220, 210, 2));
            characters[2].index = 46;
            characters[2].pokemons = DinoHouse_GenerateList(16);

            characters.Add(new Character("Dino Jr.", Resources.character_dino, 455, 160));
            actionpoint.Add(new ActionPoint(435, 140, 505, 210, 3));
            characters[3].index = 47;
            characters[3].pokemons = DinoHouse_GenerateList(16);

            characters.Add(new Character("Dino Jr.", Resources.character_dino, 590, 160));
            actionpoint.Add(new ActionPoint(570, 140, 640, 210, 4));
            characters[4].index = 48;
            characters[4].pokemons = DinoHouse_GenerateList(16);

            characters.Add(new Character("Dino Jr.", Resources.character_dino, 725, 160));
            actionpoint.Add(new ActionPoint(705, 140, 775, 210, 5));
            characters[5].index = 49;
            characters[5].pokemons = DinoHouse_GenerateList(16);
        }

        private void LoadSafari(int mapNr)
        {
            pbCanvas.BackgroundImage = Resources.grass;
            wildPokemons.Add(new ints(115, 22, 30, 20));
            wildPokemons.Add(new ints(128, 22, 30, 30));
            wildPokemons.Add(new ints(203, 22, 30, 20));
            wildPokemons.Add(new ints(241, 22, 30, 20));
            wildPokemons.Add(new ints(214, 22, 30, 5));
            wildPokemons.Add(new ints(227, 22, 30, 5));

            FormGame.OX = 580;
            FormGame.OY = 200;

            Timer timerSafari = new Timer();
            timerSafari.Interval = 480000 + mapNr;
            timerSafari.Tick += timerSafari_Tick;
            timerSafari.Start();

            mainForm.ShowMessage("You are here for a limited time, so make it count.");

            #region Environment

            structures.Add(new Structures(1, 0, 81, 552));
            structures.Add(new Structures(1, 0, 161, 552));
            structures.Add(new Structures(1, 0, 241, 552));
            structures.Add(new Structures(1, 0, 321, 552));
            structures.Add(new Structures(1, 0, 401, 552));
            structures.Add(new Structures(1, 0, 481, 552));
            structures.Add(new Structures(1, 0, 561, 552));
            structures.Add(new Structures(1, 0, 641, 552));
            structures.Add(new Structures(1, 0, 721, 552));
            structures.Add(new Structures(1, 1, 1, 522));
            structures.Add(new Structures(1, 1, 1, 492));
            structures.Add(new Structures(1, 1, 1, 462));
            structures.Add(new Structures(1, 1, 1, 432));
            structures.Add(new Structures(1, 1, 1, 402));
            structures.Add(new Structures(1, 1, 1, 372));
            structures.Add(new Structures(1, 1, 1, 342));
            structures.Add(new Structures(1, 1, 1, 312));
            structures.Add(new Structures(1, 1, 1, 282));
            structures.Add(new Structures(1, 1, 1, 252));
            structures.Add(new Structures(1, 1, 1, 222));
            structures.Add(new Structures(1, 1, 1, 192));
            structures.Add(new Structures(1, 1, 1, 162));
            structures.Add(new Structures(1, 1, 1, 132));
            structures.Add(new Structures(1, 1, 1, 102));
            structures.Add(new Structures(1, 1, 1, 72));
            structures.Add(new Structures(1, 1, 1, 42));
            structures.Add(new Structures(1, 0, 0, -12));
            structures.Add(new Structures(1, 0, 80, -12));
            structures.Add(new Structures(1, 0, 160, -12));
            structures.Add(new Structures(1, 0, 240, -12));
            structures.Add(new Structures(1, 0, 320, -12));
            structures.Add(new Structures(1, 0, 400, -12));
            structures.Add(new Structures(1, 0, 480, -12));
            structures.Add(new Structures(1, 0, 560, -12));
            structures.Add(new Structures(1, 0, 640, -12));
            structures.Add(new Structures(1, 0, 720, -12));
            structures.Add(new Structures(1, 1, 1, 4));
            structures.Add(new Structures(1, 1, 787, 1));
            structures.Add(new Structures(1, 1, 787, 31));
            structures.Add(new Structures(1, 1, 787, 61));
            structures.Add(new Structures(1, 1, 787, 91));
            structures.Add(new Structures(1, 1, 787, 121));
            structures.Add(new Structures(1, 1, 787, 151));
            structures.Add(new Structures(1, 1, 787, 181));
            structures.Add(new Structures(1, 1, 787, 211));
            structures.Add(new Structures(1, 1, 787, 241));
            structures.Add(new Structures(1, 1, 787, 271));
            structures.Add(new Structures(1, 1, 787, 301));
            structures.Add(new Structures(1, 1, 787, 331));
            structures.Add(new Structures(1, 1, 787, 361));
            structures.Add(new Structures(1, 1, 787, 391));
            structures.Add(new Structures(1, 1, 787, 421));
            structures.Add(new Structures(1, 1, 787, 451));
            structures.Add(new Structures(1, 1, 787, 481));
            structures.Add(new Structures(1, 1, 787, 511));
            structures.Add(new Structures(1, 0, 1, 552));

            trees.Add(new Trees(1, 669, 68));
            trees.Add(new Trees(1, 71, 73));
            trees.Add(new Trees(1, 155, 153));
            trees.Add(new Trees(1, 265, 303));
            trees.Add(new Trees(1, 677, 322));
            trees.Add(new Trees(1, 452, 339));
            trees.Add(new Trees(1, 50, 424));

            flowers.Add(new Flowers(248, 231));
            flowers.Add(new Flowers(61, 186));
            flowers.Add(new Flowers(354, 384));
            flowers.Add(new Flowers(155, 464));
            flowers.Add(new Flowers(484, 473));
            flowers.Add(new Flowers(562, 274));
            flowers.Add(new Flowers(618, 55));
            flowers.Add(new Flowers(166, 71));
            flowers.Add(new Flowers(641, 292));

            wildgrass.Add(new WildGrass(316, 178));
            wildgrass.Add(new WildGrass(356, 178));
            wildgrass.Add(new WildGrass(396, 178));
            wildgrass.Add(new WildGrass(436, 178));
            wildgrass.Add(new WildGrass(476, 178));
            wildgrass.Add(new WildGrass(476, 218));
            wildgrass.Add(new WildGrass(476, 258));
            wildgrass.Add(new WildGrass(436, 258));
            wildgrass.Add(new WildGrass(396, 258));
            wildgrass.Add(new WildGrass(356, 258));
            wildgrass.Add(new WildGrass(316, 258));
            wildgrass.Add(new WildGrass(316, 218));
            wildgrass.Add(new WildGrass(356, 218));
            wildgrass.Add(new WildGrass(396, 218));
            wildgrass.Add(new WildGrass(436, 218));
            wildgrass.Add(new WildGrass(592, 341));
            wildgrass.Add(new WildGrass(592, 381));
            wildgrass.Add(new WildGrass(592, 421));
            wildgrass.Add(new WildGrass(592, 461));
            wildgrass.Add(new WildGrass(592, 501));
            wildgrass.Add(new WildGrass(632, 501));
            wildgrass.Add(new WildGrass(672, 501));
            wildgrass.Add(new WildGrass(712, 501));
            wildgrass.Add(new WildGrass(712, 461));
            wildgrass.Add(new WildGrass(712, 421));
            wildgrass.Add(new WildGrass(672, 421));
            wildgrass.Add(new WildGrass(632, 421));
            wildgrass.Add(new WildGrass(632, 461));
            wildgrass.Add(new WildGrass(672, 461));
            wildgrass.Add(new WildGrass(552, 381));
            wildgrass.Add(new WildGrass(552, 341));
            wildgrass.Add(new WildGrass(552, 421));
            wildgrass.Add(new WildGrass(552, 461));
            wildgrass.Add(new WildGrass(552, 501));
            wildgrass.Add(new WildGrass(374, 450));
            wildgrass.Add(new WildGrass(334, 450));
            wildgrass.Add(new WildGrass(294, 450));
            wildgrass.Add(new WildGrass(254, 450));
            wildgrass.Add(new WildGrass(214, 450));
            wildgrass.Add(new WildGrass(214, 490));
            wildgrass.Add(new WildGrass(254, 490));
            wildgrass.Add(new WildGrass(294, 490));
            wildgrass.Add(new WildGrass(334, 490));
            wildgrass.Add(new WildGrass(374, 490));
            wildgrass.Add(new WildGrass(48, 254));
            wildgrass.Add(new WildGrass(88, 254));
            wildgrass.Add(new WildGrass(128, 254));
            wildgrass.Add(new WildGrass(168, 254));
            wildgrass.Add(new WildGrass(168, 294));
            wildgrass.Add(new WildGrass(168, 334));
            wildgrass.Add(new WildGrass(168, 374));
            wildgrass.Add(new WildGrass(128, 374));
            wildgrass.Add(new WildGrass(88, 374));
            wildgrass.Add(new WildGrass(48, 374));
            wildgrass.Add(new WildGrass(48, 334));
            wildgrass.Add(new WildGrass(48, 294));
            wildgrass.Add(new WildGrass(88, 294));
            wildgrass.Add(new WildGrass(128, 294));
            wildgrass.Add(new WildGrass(128, 334));
            wildgrass.Add(new WildGrass(88, 334));
            wildgrass.Add(new WildGrass(221, 57));
            wildgrass.Add(new WildGrass(261, 57));
            wildgrass.Add(new WildGrass(301, 57));
            wildgrass.Add(new WildGrass(341, 57));
            wildgrass.Add(new WildGrass(381, 57));
            wildgrass.Add(new WildGrass(421, 57));
            wildgrass.Add(new WildGrass(461, 57));
            wildgrass.Add(new WildGrass(501, 57));
            wildgrass.Add(new WildGrass(541, 57));
            wildgrass.Add(new WildGrass(541, 97));
            wildgrass.Add(new WildGrass(501, 97));
            wildgrass.Add(new WildGrass(461, 97));
            wildgrass.Add(new WildGrass(421, 97));
            wildgrass.Add(new WildGrass(381, 97));
            wildgrass.Add(new WildGrass(341, 97));
            wildgrass.Add(new WildGrass(301, 97));
            wildgrass.Add(new WildGrass(261, 97));
            wildgrass.Add(new WildGrass(221, 97));
            wildgrass.Add(new WildGrass(670, 187));
            wildgrass.Add(new WildGrass(710, 187));
            wildgrass.Add(new WildGrass(710, 227));
            wildgrass.Add(new WildGrass(670, 227));

            paths.Add(new Paths(7, 581, 134));
            paths.Add(new Paths(9, 621, 134));
            paths.Add(new Paths(3, 621, 174));
            paths.Add(new Paths(1, 581, 174));

            #endregion
        }

        void timerSafari_Tick(object sender, EventArgs e)
        {
            Timer timerSafari = (Timer)sender;
            timerSafari.Stop();
            FormGame.map = new Map(timerSafari.Interval - 480000, 605, 390);
            mainForm.ShowMessage("Your time in the Safari Zone is over.");
        }

        Timer timerTrickHouse;
        private void LoadTrickHouse(int mapNr)
        {
            pbCanvas.BackgroundImage = Resources.trickhouse;

            timerTrickHouse = new Timer();
            timerTrickHouse.Interval = 200;
            timerTrickHouse.Tick += timerTrickHouse_Tick;
            timerTrickHouse.Start();

            exit.Add(new ExitPoint(180, 550, 280, 600, 37, 70, 410));

            FormGame.OX = 230;
            FormGame.OY = 500;

            blockedzone.Add(new BlockedZone(196, 1, 614, 39));
            blockedzone.Add(new BlockedZone(1, 1, 204, 594));
            blockedzone.Add(new BlockedZone(596, 3, 797, 596));

            wildPokemons.Add(new ints(201, 22, 30, 100));

            if (!pokemonDefeated[386])
            {
                wildpokemons.Add(new Pokemons(386, 65, 498, 33));
                actionpoint.Add(new ActionPoint(488, 23, 558, 83, 100));
            }
            
            #region Environment

            wildgrass.Add(new WildGrass(234, 125));
            wildgrass.Add(new WildGrass(234, 165));
            wildgrass.Add(new WildGrass(234, 205));
            wildgrass.Add(new WildGrass(234, 245));
            wildgrass.Add(new WildGrass(234, 285));
            wildgrass.Add(new WildGrass(234, 325));
            wildgrass.Add(new WildGrass(234, 365));
            wildgrass.Add(new WildGrass(234, 405));
            wildgrass.Add(new WildGrass(234, 445));
            wildgrass.Add(new WildGrass(234, 485));
            wildgrass.Add(new WildGrass(274, 485));
            wildgrass.Add(new WildGrass(314, 485));
            wildgrass.Add(new WildGrass(354, 485));
            wildgrass.Add(new WildGrass(394, 485));
            wildgrass.Add(new WildGrass(434, 485));
            wildgrass.Add(new WildGrass(474, 485));
            wildgrass.Add(new WildGrass(514, 485));
            wildgrass.Add(new WildGrass(514, 445));
            wildgrass.Add(new WildGrass(514, 405));
            wildgrass.Add(new WildGrass(514, 365));
            wildgrass.Add(new WildGrass(514, 325));
            wildgrass.Add(new WildGrass(514, 285));
            wildgrass.Add(new WildGrass(514, 245));
            wildgrass.Add(new WildGrass(514, 205));
            wildgrass.Add(new WildGrass(514, 165));
            wildgrass.Add(new WildGrass(514, 125));
            wildgrass.Add(new WildGrass(474, 125));
            wildgrass.Add(new WildGrass(434, 125));
            wildgrass.Add(new WildGrass(394, 125));
            wildgrass.Add(new WildGrass(354, 125));
            wildgrass.Add(new WildGrass(314, 125));
            wildgrass.Add(new WildGrass(274, 125));
            wildgrass.Add(new WildGrass(274, 165));
            wildgrass.Add(new WildGrass(274, 205));
            wildgrass.Add(new WildGrass(274, 245));
            wildgrass.Add(new WildGrass(274, 285));
            wildgrass.Add(new WildGrass(274, 325));
            wildgrass.Add(new WildGrass(274, 365));
            wildgrass.Add(new WildGrass(274, 405));
            wildgrass.Add(new WildGrass(274, 445));
            wildgrass.Add(new WildGrass(314, 445));
            wildgrass.Add(new WildGrass(354, 445));
            wildgrass.Add(new WildGrass(394, 445));
            wildgrass.Add(new WildGrass(434, 445));
            wildgrass.Add(new WildGrass(474, 445));
            wildgrass.Add(new WildGrass(474, 405));
            wildgrass.Add(new WildGrass(474, 365));
            wildgrass.Add(new WildGrass(474, 325));
            wildgrass.Add(new WildGrass(474, 285));
            wildgrass.Add(new WildGrass(474, 245));
            wildgrass.Add(new WildGrass(474, 205));
            wildgrass.Add(new WildGrass(474, 165));
            wildgrass.Add(new WildGrass(394, 165));
            wildgrass.Add(new WildGrass(354, 165));
            wildgrass.Add(new WildGrass(314, 165));
            wildgrass.Add(new WildGrass(314, 205));
            wildgrass.Add(new WildGrass(314, 245));
            wildgrass.Add(new WildGrass(314, 285));
            wildgrass.Add(new WildGrass(314, 325));
            wildgrass.Add(new WildGrass(314, 365));
            wildgrass.Add(new WildGrass(314, 405));
            wildgrass.Add(new WildGrass(354, 405));
            wildgrass.Add(new WildGrass(394, 405));
            wildgrass.Add(new WildGrass(434, 405));
            wildgrass.Add(new WildGrass(434, 365));
            wildgrass.Add(new WildGrass(434, 325));
            wildgrass.Add(new WildGrass(434, 285));
            wildgrass.Add(new WildGrass(434, 245));
            wildgrass.Add(new WildGrass(434, 205));
            wildgrass.Add(new WildGrass(434, 165));
            wildgrass.Add(new WildGrass(394, 205));
            wildgrass.Add(new WildGrass(354, 205));
            wildgrass.Add(new WildGrass(354, 245));
            wildgrass.Add(new WildGrass(354, 285));
            wildgrass.Add(new WildGrass(354, 325));
            wildgrass.Add(new WildGrass(354, 365));
            wildgrass.Add(new WildGrass(394, 365));
            wildgrass.Add(new WildGrass(394, 325));
            wildgrass.Add(new WildGrass(394, 285));
            wildgrass.Add(new WildGrass(394, 245));

            #endregion
        }

        void timerTrickHouse_Tick(object sender, EventArgs e)
        {
            Keyboard.ChangeDir(rand.Next(0, 5), true);
        }

        private void LoadTournament(int level)
        {
            pbCanvas.BackgroundImage = Resources.tournamentInside;

            FormGame.OX = 272;
            FormGame.OY = 296;

            Keyboard.ChangeDir(3, true);
            player.ChangeSprite();
            Keyboard.ClearStack();
            FormGame.stopMovement = true;

            player.pokeballs.Clear();
            player.potions.Clear();

            if(level == 1)
            {
                characters.Add(new Character("Gary", Resources.character_gary, 482, 280));

                Pokemons poke = new Pokemons(142, 70);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(18));
                poke.moves.Add(new Attack(63));
                poke.moves.Add(new Attack(16));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(9, 70);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(308));
                poke.moves.Add(new Attack(130));
                poke.moves.Add(new Attack(110));
                poke.moves.Add(new Attack(229));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(197, 70);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(94));
                poke.moves.Add(new Attack(247));
                poke.moves.Add(new Attack(104));
                poke.moves.Add(new Attack(130));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(466, 70);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(9));
                poke.moves.Add(new Attack(231));
                poke.moves.Add(new Attack(182));
                poke.moves.Add(new Attack(87));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(126, 70);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(53));
                poke.moves.Add(new Attack(126));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(212, 70);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(98));
                poke.moves.Add(new Attack(232));
                poke.moves.Add(new Attack(129));
                poke.moves.Add(new Attack(211));
                characters[0].pokemons.Add(poke);

                characters[0].index = 118;
            }

            if(level == 2)
            {
                characters.Add(new Character("Ash", Resources.character_ash, 482, 280));

                Pokemons poke = new Pokemons(25, 70);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(344));
                poke.moves.Add(new Attack(231));
                poke.moves.Add(new Attack(97));
                poke.moves.Add(new Attack(85));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(6, 70);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(315));
                poke.moves.Add(new Attack(225));
                poke.moves.Add(new Attack(53));
                poke.moves.Add(new Attack(130));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(7, 70);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(56));
                poke.moves.Add(new Attack(55));
                poke.moves.Add(new Attack(229));
                poke.moves.Add(new Attack(110));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(254, 70);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(348));
                poke.moves.Add(new Attack(97));
                poke.moves.Add(new Attack(98));
                poke.moves.Add(new Attack(437));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(398, 70);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(370));
                poke.moves.Add(new Attack(413));
                poke.moves.Add(new Attack(332));
                poke.moves.Add(new Attack(98));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(143, 70);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(156));
                poke.moves.Add(new Attack(8));
                poke.moves.Add(new Attack(5));
                poke.moves.Add(new Attack(29));
                characters[0].pokemons.Add(poke);

                characters[0].index = 119;
            }

            if (level == 3)
            {
                characters.Add(new Character("Tobias", Resources.character_tobias, 482, 280));

                Pokemons poke = new Pokemons(491, 70);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(58));
                poke.moves.Add(new Attack(464));
                poke.moves.Add(new Attack(138));
                poke.moves.Add(new Attack(399));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(380, 70);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(416));
                poke.moves.Add(new Attack(295));
                poke.moves.Add(new Attack(113));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(381, 70);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(416));
                poke.moves.Add(new Attack(295));
                poke.moves.Add(new Attack(113));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(389, 70);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(202));
                poke.moves.Add(new Attack(235));
                poke.moves.Add(new Attack(89));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(226, 70);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(392));
                poke.moves.Add(new Attack(36));
                poke.moves.Add(new Attack(340));
                poke.moves.Add(new Attack(61));
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(395, 70);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(56));
                poke.moves.Add(new Attack(65));
                poke.moves.Add(new Attack(54));
                characters[0].pokemons.Add(poke);

                characters[0].index = 120;
            }

            if (level == 4)
            {
                characters.Add(new Character("Thomas", Resources.character_thomas, 482, 280));

                Pokemons poke = new Pokemons(392, 80);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(172));
                poke.moves.Add(new Attack(264));
                poke.moves.Add(new Attack(370));
                poke.moves.Add(new Attack(394));
                poke.hpmax = 298;
                poke.hp = poke.hpmax;
                poke.atk = 248;
                poke.def = 203;
                poke.spatk = 248;
                poke.spdef = 213;
                poke.speed = 217;
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(384, 80);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(156));
                poke.moves.Add(new Attack(406));
                poke.moves.Add(new Attack(200));
                poke.moves.Add(new Attack(434));
                poke.hpmax = 225;
                poke.hp = poke.hpmax;
                poke.atk = 191;
                poke.def = 131;
                poke.spatk = 190;
                poke.spdef = 130;
                poke.speed = 135;
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(248, 80);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(242));
                poke.moves.Add(new Attack(89));
                poke.moves.Add(new Attack(444));
                poke.moves.Add(new Attack(63));
                poke.hpmax = 314;
                poke.hp = poke.hpmax;
                poke.atk = 269;
                poke.def = 245;
                poke.spatk = 231;
                poke.spdef = 237;
                poke.speed = 165;
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(376, 80);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(334));
                poke.moves.Add(new Attack(359));
                poke.moves.Add(new Attack(309));
                poke.moves.Add(new Attack(428));
                poke.hpmax = 244;
                poke.hp = poke.hpmax;
                poke.atk = 221;
                poke.def = 217;
                poke.spatk = 181;
                poke.spdef = 176;
                poke.speed = 140;
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(145, 80);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(355));
                poke.moves.Add(new Attack(113));
                poke.moves.Add(new Attack(65));
                poke.moves.Add(new Attack(87));
                poke.hpmax = 253;
                poke.hp = poke.hpmax;
                poke.atk = 174;
                poke.def = 170;
                poke.spatk = 209;
                poke.spdef = 174;
                poke.speed = 169;
                characters[0].pokemons.Add(poke);

                poke = new Pokemons(130, 80);
                poke.love = 200;
                poke.moves = new List<Attack>();
                poke.moves.Add(new Attack(56));
                poke.moves.Add(new Attack(349));
                poke.moves.Add(new Attack(63));
                poke.moves.Add(new Attack(57));
                poke.hpmax = 309;
                poke.hp = poke.hpmax;
                poke.atk = 260;
                poke.def = 214;
                poke.spatk = 195;
                poke.spdef = 236;
                poke.speed = 184;
                characters[0].pokemons.Add(poke);

                characters[0].index = 121;
            }

            ShuffleTeam(characters[0]);

            actionpoint.Add(new ActionPoint(0, 0, 800, 600, 0));
        }

        private void ShuffleTeam(Character trainer)
        {
            List<Pokemons> list = trainer.pokemons;
            for(int i=1;i<=1000;++i)
            {
                int x = rand.Next() % 6;
                int y = rand.Next() % 6;
                Pokemons aux;
                aux = list[x];
                list[x] = list[y];
                list[y] = aux;
            }
        }

        #endregion
    }
}
