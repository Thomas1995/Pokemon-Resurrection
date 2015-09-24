using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace Pokemon
{
    class Resources
    {
        #region Terrain
        private static Size unitSize = new Size(40, 40);
        private static Size screenSize = new Size(800, 600);

        public static Image grass = new Bitmap(Properties.Resources.grass, unitSize);
        public static Image townpavement = new Bitmap(Properties.Resources.townpavement, unitSize);
        public static Image waterwaves = new Bitmap(Properties.Resources.waterwaves, unitSize);
        public static Image ground = new Bitmap(Properties.Resources.ground, unitSize);
        public static Image snow = new Bitmap(Properties.Resources.snow, unitSize);

        public static Image mountain = new Bitmap(Properties.Resources.mountain, screenSize);
        public static Image ancientcave = new Bitmap(Properties.Resources.ancientcave, screenSize);

        public static Image biggrass = new Bitmap(Properties.Resources.biggrass, unitSize);

        public static Image[] flower = new Image[] 
            { new Bitmap(Properties.Resources.flower1, unitSize), 
              new Bitmap(Properties.Resources.flower2, unitSize) };

        public static Image hill = new Bitmap(Properties.Resources.hill);

        public static Image smalltree = new Bitmap(Properties.Resources.smalltree, new Size(48, 62));
        public static Image tree = new Bitmap(Properties.Resources.tree, new Size(80, 90));
        public static Image treesnow = new Bitmap(Properties.Resources.treesnow, new Size(80, 90));

        public static Image[] path = new Image[9];
        public static Image[,] water = new Image[2, 9];

        public static Image[] fence = new Image[] 
            {new Bitmap(Properties.Resources.fencefront, new Size(80, 40)),
             new Bitmap(Properties.Resources.fenceside, new Size(8, 40))};

        public static Image lantern = new Bitmap(Properties.Resources.lantern, new Size(40, 80));

        public static Image stone = new Bitmap(Properties.Resources.stone, new Size(60, 60));

        public static Image surfboard = new Bitmap(Properties.Resources.surfboard, new Size(80, 60));
        #endregion

        #region Buildings
        private static Size buildingSize = new Size(180, 135);

        public static Image gym = new Bitmap(Properties.Resources.gym, buildingSize);
        public static Image gymInside = new Bitmap(Properties.Resources.gyminter, screenSize);

        public static Image healingcenter = new Bitmap(Properties.Resources.healingcenter, buildingSize);
        public static Image healingcenterInside = new Bitmap(Properties.Resources.healingcenterinter, screenSize);

        public static Image shop = new Bitmap(Properties.Resources.shop, buildingSize);
        public static Image shopInside = new Bitmap(Properties.Resources.shopinter, screenSize);

        public static Image[] house = new Image[]
            { new Bitmap(Properties.Resources.house1, buildingSize),
             new Bitmap(Properties.Resources.house2, buildingSize), 
             new Bitmap(Properties.Resources.house3, buildingSize),
             new Bitmap(Properties.Resources.house4, buildingSize),
             new Bitmap(Properties.Resources.house4, buildingSize),
             new Bitmap(Properties.Resources.house5, buildingSize),
             new Bitmap(Properties.Resources.house6, buildingSize)};

        public static Image hauntedhouseInside = new Bitmap(Properties.Resources.hauntedhouseinter, screenSize);

        public static Image trickhouse = new Bitmap(Properties.Resources.misterioushouseinter, screenSize);

        public static Image arenaInside = new Bitmap(Properties.Resources.arenainter, screenSize);

        public static Image cave = new Bitmap(Properties.Resources.cave, buildingSize);
        public static Image caveInside = new Bitmap(Properties.Resources.caveinter, screenSize);

        public static Image tournamentInside = new Bitmap(Properties.Resources.tournamentinter, screenSize);
        #endregion

        #region Character sprites
        public static Image character_boy = new Bitmap(Properties.Resources.boy_sprite, new Size(148, 192));

        private static Size characterSize = new Size(48, 62);
        public static Image character_farmer = new Bitmap(Properties.Resources.farmer_sprite, characterSize);
        public static Image character_fighter = new Bitmap(Properties.Resources.fighter_sprite, characterSize);
        public static Image character_fighter2 = new Bitmap(Properties.Resources.fighter2_sprite, characterSize);
        public static Image character_fighter3 = new Bitmap(Properties.Resources.fighter3_sprite, characterSize);
        public static Image character_bugcatcher = new Bitmap(Properties.Resources.bugcatcher_sprite, characterSize);
        public static Image character_bugcatcher2 = new Bitmap(Properties.Resources.bugcatcher2_sprite, characterSize);
        public static Image character_kid = new Bitmap(Properties.Resources.kid_sprite, characterSize);
        public static Image character_kid2 = new Bitmap(Properties.Resources.kid2_sprite, characterSize);
        public static Image character_littleboy = new Bitmap(Properties.Resources.littleboy_sprite, characterSize);
        public static Image character_littlegirl = new Bitmap(Properties.Resources.littlegirl_sprite, characterSize);
        public static Image character_dad = new Bitmap(Properties.Resources.dad_sprite, characterSize);
        public static Image character_dad2 = new Bitmap(Properties.Resources.dad2_sprite, characterSize);
        public static Image character_mom = new Bitmap(Properties.Resources.mom_sprite, characterSize);
        public static Image character_oldman = new Bitmap(Properties.Resources.oldman_sprite, characterSize);
        public static Image character_oldman2 = new Bitmap(Properties.Resources.oldman2_sprite, characterSize);
        public static Image character_oldman3 = new Bitmap(Properties.Resources.oldman3_sprite, characterSize);
        public static Image character_oldwoman = new Bitmap(Properties.Resources.oldwoman_sprite, characterSize);
        public static Image character_explorer = new Bitmap(Properties.Resources.explorer_sprite, characterSize);
        public static Image character_magician = new Bitmap(Properties.Resources.magician_sprite, characterSize);
        public static Image character_swimmer = new Bitmap(Properties.Resources.swimmer_sprite, characterSize);
        public static Image character_swimmer2 = new Bitmap(Properties.Resources.swimmer2_sprite, characterSize);
        public static Image character_swimmer3 = new Bitmap(Properties.Resources.swimmer3_sprite, characterSize);
        public static Image character_swimmer4 = new Bitmap(Properties.Resources.swimmer4_sprite, characterSize);
        public static Image character_fisherman = new Bitmap(Properties.Resources.fisherman_sprite, characterSize);
        public static Image character_dino = new Bitmap(Properties.Resources.dino_sprite, characterSize);
        public static Image character_cameraman = new Bitmap(Properties.Resources.cameraman_sprite, characterSize);
        public static Image character_painter = new Bitmap(Properties.Resources.painter_sprite, characterSize);
        public static Image character_teamrocket = new Bitmap(Properties.Resources.teamrocket_sprite, characterSize);
        public static Image character_teamrocket2 = new Bitmap(Properties.Resources.teamrocket2_sprite, characterSize);
        public static Image character_policeman = new Bitmap(Properties.Resources.policeman_sprite, characterSize);
        public static Image character_woman = new Bitmap(Properties.Resources.woman_sprite, characterSize);
        public static Image character_woman2 = new Bitmap(Properties.Resources.woman2_sprite, characterSize);
        public static Image character_spouses = new Bitmap(Properties.Resources.spouses_sprite, characterSize);
        public static Image character_giovanni = new Bitmap(Properties.Resources.giovanni_sprite, characterSize);
        public static Image character_tobias = new Bitmap(Properties.Resources.tobias_sprite, characterSize);
        public static Image character_ash = new Bitmap(Properties.Resources.ash_sprite, characterSize);
        public static Image character_gary = new Bitmap(Properties.Resources.gary_sprite, characterSize);
        public static Image character_thomas = new Bitmap(Properties.Resources.thomas_sprite, characterSize);
        public static Image character_official = new Bitmap(Properties.Resources.official_sprite, characterSize);
        #endregion

        #region Pokemon spirtes
        public static Image[] pokeFront = new Image[494];
        public static Image[] pokeBack = new Image[494];
        #endregion

        #region Battle
        public static Image battlebg = new Bitmap(Properties.Resources.battlebackground);
        static public float[][] effectiveness = new float[18][];
        #endregion

        #region Minigames
        public static Image minigame1bg = new Bitmap(Properties.Resources.background_minigame1, new Size(600, 400));
        #endregion

        #region Evolution
        static public int[] xpneeded;

        public struct evol
        {
            public int level, evolution;

            public evol(int x, int y)
            {
                evolution = x;
                level = y;
            }
        };

        static public evol[] evolution = new evol[494];
        #endregion

        #region Items
        private static Size itemSize = new Size(24, 24);
        public static Image pokeball = new Bitmap(Properties.Resources.pokeball, itemSize);
        public static Image greatball = new Bitmap(Properties.Resources.greatball, itemSize);
        public static Image ultraball = new Bitmap(Properties.Resources.ultraball, itemSize);
        public static Image masterball = new Bitmap(Properties.Resources.masterball, itemSize);
        #endregion

        public Resources()
        {
            #region Evolution
            for (int i = 1; i <= 493; ++i)
                evolution[i] = new evol(0, 0);

            // evolution: -1 random evolution, -2 gender
            // level: -1 item, -2 love, -3 special

            List<List<string>> ret = DataBase.AdminGetData("SELECT * FROM EvolutionDex");

            for (int i = 0; i < ret.Count; ++i)
                evolution[Convert.ToInt32(ret[i][0])] =
                    new evol(Convert.ToInt32(ret[i][1]), Convert.ToInt32(ret[i][2]));

            #endregion
            
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_DoWork;
            bw.RunWorkerAsync();
        }

        public static bool resourcesDone;

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            #region Effectiveness
            effectiveness[1] = new float[18] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0.5f, 0, 1, 1, 0.5f };
            effectiveness[2] = new float[18] { 1, 1, 0.5f, 0.5f, 1, 2, 2, 1, 1, 1, 1, 1, 2, 0.5f, 1, 0.5f, 1, 2 };
            effectiveness[3] = new float[18] { 1, 1, 2, 0.5f, 1, 0.5f, 1, 1, 1, 2, 1, 1, 1, 2, 1, 0.5f, 1, 1 };
            effectiveness[4] = new float[18] { 1, 1, 1, 2, 0.5f, 0.5f, 1, 1, 1, 0, 2, 1, 1, 1, 1, 0.5f, 1, 1 };
            effectiveness[5] = new float[18] { 1, 1, 0.5f, 2, 1, 0.5f, 1, 1, 0.5f, 2, 0.5f, 1, 0.5f, 2, 1, 0.5f, 1, 0.5f };
            effectiveness[6] = new float[18] { 1, 1, 0.5f, 0.5f, 1, 2, 0.5f, 1, 1, 2, 2, 1, 1, 1, 1, 2, 1, 0.5f };
            effectiveness[7] = new float[18] { 1, 2, 1, 1, 1, 1, 2, 1, 0.5f, 1, 0.5f, 0.5f, 0.5f, 2, 0, 1, 2, 2 };
            effectiveness[8] = new float[18] { 1, 1, 1, 1, 1, 2, 1, 1, 0.5f, 0.5f, 1, 1, 1, 0.5f, 0.5f, 1, 1, 0 };
            effectiveness[9] = new float[18] { 1, 1, 2, 1, 2, 0.5f, 1, 1, 2, 1, 0, 1, 0.5f, 2, 1, 1, 1, 2 };
            effectiveness[10] = new float[18] { 1, 1, 1, 1, 0.5f, 2, 1, 2, 1, 1, 1, 1, 2, 0.5f, 1, 1, 1, 0.5f };
            effectiveness[11] = new float[18] { 1, 1, 1, 1, 1, 1, 1, 2, 2, 1, 1, 0.5f, 1, 1, 1, 1, 0, 0.5f };
            effectiveness[12] = new float[18] { 1, 1, 0.5f, 1, 1, 2, 1, 0.5f, 0.5f, 1, 0.5f, 2, 1, 1, 0.5f, 1, 2, 0.5f };
            effectiveness[13] = new float[18] { 1, 1, 2, 1, 1, 1, 2, 0.5f, 1, 0.5f, 2, 1, 2, 1, 1, 1, 1, 0.5f };
            effectiveness[14] = new float[18] { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 0.5f, 0.5f };
            effectiveness[15] = new float[18] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 0.5f };
            effectiveness[16] = new float[18] { 1, 1, 1, 1, 1, 1, 1, 0.5f, 1, 1, 1, 2, 1, 1, 2, 1, 0.5f, 0.5f };
            effectiveness[17] = new float[18] { 1, 1, 0.5f, 0.5f, 0.5f, 1, 2, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 0.5f };
            #endregion

            #region XP needed
            xpneeded = new int[100] {0,9,48,39,39,44,57,78,105,141,182,231,288,351,423,500,585,678,777,885,
                                    998,1119,1248,1383,1527,1676,1833,1998,2169,2349,2534,2727,2928,3135,3351,
                                    3572,3801,4038,4281,4533,4790,5055,5328,5607,5895,6188,6489,6798,7113,7437,
                                    7766,8103,8448,8799,9159,9524,9897,10278,10665,11061,11462,11871,12288,12711,
                                    13143,13580,14025,14478,14937,15405,15878,16359,16848,17343,17847,18356,18873,
                                    19398,19929,20469,21014,21567,22128,22695,23271,23852,24441,25038,25641,26253,
                                    26870,27495,28128,28767,29415,30068,30729,31398,32073,32757};
            #endregion

            #region Generate path and water

            for (int p1 = 0; p1 < 3; ++p1)
                for (int p2 = 0; p2 < 3; ++p2)
                {
                    int number = p2 + 3 * (2 - p1);

                    // path
                    path[number] = new Bitmap(40, 40);
                    Graphics G = Graphics.FromImage(path[number]);

                    G.DrawImage(Properties.Resources.path, new Rectangle(0, 0, 40, 40),
                        new Rectangle(40 * p2, 40 * p1, 40, 40), GraphicsUnit.Pixel);

                    // clean water
                    water[0, number] = new Bitmap(40, 40);
                    G = Graphics.FromImage(water[0, number]);

                    G.DrawImage(Properties.Resources.water, new Rectangle(0, 0, 40, 40),
                        new Rectangle(40 * p2, 40 * p1, 40, 40), GraphicsUnit.Pixel);

                    // polluted water
                    water[1, number] = new Bitmap(40, 40);
                    G = Graphics.FromImage(water[1, number]);

                    G.DrawImage(Properties.Resources.waterpolluted, new Rectangle(0, 0, 40, 40),
                        new Rectangle(40 * p2, 40 * p1, 40, 40), GraphicsUnit.Pixel);
                }
            #endregion

            #region Generate pokemon front sprites
            int W1 = Properties.Resources.pokemon_front.Width / 28;
            int H1 = Properties.Resources.pokemon_front.Height / 18;

            for (int p1 = 0; p1 <= 17; ++p1)
                for (int p2 = 0; p2 <= 27; ++p2)
                {
                    int number = 28 * p1 + p2 + 1;
                    if (number > 493) break;

                    pokeFront[number] = new Bitmap(W1, H1);
                    Graphics G = Graphics.FromImage(pokeFront[number]);

                    G.DrawImage(Properties.Resources.pokemon_front, new Rectangle(0, 0, W1, H1),
                        new Rectangle(W1 * p2, H1 * p1, W1, H1), GraphicsUnit.Pixel);
                }
            #endregion

            #region Generate pokemon back sprites
            int W2 = Properties.Resources.pokemon_back.Width / 31;
            int H2 = Properties.Resources.pokemon_back.Height / 16;

            for (int p1 = 0; p1 <= 15; ++p1)
                for (int p2 = 0; p2 <= 30; ++p2)
                {
                    int number = 31 * p1 + p2 + 1;
                    if (number > 493) break;

                    pokeBack[number] = new Bitmap(W2, H2);
                    Graphics G = Graphics.FromImage(pokeBack[number]);

                    G.DrawImage(Properties.Resources.pokemon_back, new Rectangle(0, 0, W2, H2),
                        new Rectangle(W2 * p2, H2 * p1, W2, H2), GraphicsUnit.Pixel);
                }
            #endregion

            resourcesDone = true;
        }
    }
}
