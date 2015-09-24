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
    public partial class AdminMapEditor : Form
    {
        PictureBox[] pbpath = new PictureBox[9];
        PictureBox[] pbwater = new PictureBox[9];
        Image[] trainers;
        string[] trainerstring;
        ComboBox[] trainerPokemon = new ComboBox[6];
        NumericUpDown[] trainerNumeric = new NumericUpDown[6];
        ComboBox[] wildpokemonNames = new ComboBox[10];
        NumericUpDown[] wildpokemonChance = new NumericUpDown[10];

        public AdminMapEditor()
        {
            InitializeComponent();

            cbBackground.Items.Add("grass");
            cbBackground.Items.Add("townpavement");
            cbBackground.Items.Add("mountain");
            cbBackground.Items.Add("waterwaves");
            cbBackground.Items.Add("ground");
            cbBackground.Items.Add("snow");
            cbBackground.Items.Add("ancientcave");
            cbBackground.SelectedIndex = 0;

            pbpath[0] = path1;
            pbpath[1] = path2;
            pbpath[2] = path3;
            pbpath[3] = path4;
            pbpath[4] = path5;
            pbpath[5] = path6;
            pbpath[6] = path7;
            pbpath[7] = path8;
            pbpath[8] = path9;

            pbwater[0] = water1;
            pbwater[1] = water2;
            pbwater[2] = water3;
            pbwater[3] = water4;
            pbwater[4] = water5;
            pbwater[5] = water6;
            pbwater[6] = water7;
            pbwater[7] = water8;
            pbwater[8] = water9;

            trainerPokemon[0] = cbPokemon1;
            trainerPokemon[1] = cbPokemon2;
            trainerPokemon[2] = cbPokemon3;
            trainerPokemon[3] = cbPokemon4;
            trainerPokemon[4] = cbPokemon5;
            trainerPokemon[5] = cbPokemon6;

            trainerNumeric[0] = numPokemon1;
            trainerNumeric[1] = numPokemon2;
            trainerNumeric[2] = numPokemon3;
            trainerNumeric[3] = numPokemon4;
            trainerNumeric[4] = numPokemon5;
            trainerNumeric[5] = numPokemon6;

            wildpokemonNames[0] = cbWild1;
            wildpokemonNames[1] = cbWild2;
            wildpokemonNames[2] = cbWild3;
            wildpokemonNames[3] = cbWild4;
            wildpokemonNames[4] = cbWild5;
            wildpokemonNames[5] = cbWild6;
            wildpokemonNames[6] = cbWild7;
            wildpokemonNames[7] = cbWild8;
            wildpokemonNames[8] = cbWild9;
            wildpokemonNames[9] = cbWild10;

            wildpokemonChance[0] = numChance1;
            wildpokemonChance[1] = numChance2;
            wildpokemonChance[2] = numChance3;
            wildpokemonChance[3] = numChance4;
            wildpokemonChance[4] = numChance5;
            wildpokemonChance[5] = numChance6;
            wildpokemonChance[6] = numChance7;
            wildpokemonChance[7] = numChance8;
            wildpokemonChance[8] = numChance9;
            wildpokemonChance[9] = numChance10;

            for (int i = 0; i < 6; ++i)
                cbPokemon_Populate(trainerPokemon[i]);

            for (int i = 0; i < 10; ++i)
                cbPokemon_Populate(wildpokemonNames[i]);

            for (int i = 0; i < 9; ++i)
                pbpath[i].Image = Resources.path[i];

            for (int i = 0; i < 9; ++i)
                pbwater[i].Image = Resources.water[0, i];

            pbTree.Image = Resources.tree;
            pbWildgrass.Image = Resources.biggrass;
            pbFlower.Image = Resources.flower[0];
            pbHill.Image = Resources.hill;

            numBuilding.Minimum = -1;
            numBuilding.Maximum = 8;
            numBuilding.Value = 1;

            trainers = new Image[30];
            trainerstring = new string[30];
            trainers[1] = Resources.character_farmer;
            trainers[2] = Resources.character_fighter;
            trainers[3] = Resources.character_bugcatcher;
            trainers[4] = Resources.character_bugcatcher2;
            trainers[5] = Resources.character_kid;
            trainers[6] = Resources.character_fighter2;
            trainers[7] = Resources.character_fighter3;
            trainers[8] = Resources.character_kid2;
            trainers[9] = Resources.character_littleboy;
            trainers[10] = Resources.character_littlegirl;
            trainers[11] = Resources.character_dad;
            trainers[12] = Resources.character_dad2;
            trainers[13] = Resources.character_mom;
            trainers[14] = Resources.character_oldman;
            trainers[15] = Resources.character_oldman2;
            trainers[16] = Resources.character_oldwoman;
            trainers[17] = Resources.character_explorer;
            trainers[18] = Resources.character_magician;
            trainers[19] = Resources.character_swimmer;
            trainers[20] = Resources.character_swimmer2;
            trainers[21] = Resources.character_swimmer3;
            trainers[22] = Resources.character_swimmer4;
            trainers[23] = Resources.character_fisherman;
            trainers[24] = Resources.character_dino;
            trainers[25] = Resources.character_cameraman;
            trainers[26] = Resources.character_painter;
            trainers[27] = Resources.character_teamrocket;
            trainers[28] = Resources.character_teamrocket2;
            trainers[29] = Resources.character_policeman;
            trainerstring[1] = "Resources.character_farmer";
            trainerstring[2] = "Resources.character_fighter";
            trainerstring[3] = "Resources.character_bugcatcher";
            trainerstring[4] = "Resources.character_bugcatcher2";
            trainerstring[5] = "Resources.character_kid";
            trainerstring[6] = "Resources.character_fighter2";
            trainerstring[7] = "Resources.character_fighter3";
            trainerstring[8] = "Resources.character_kid2";
            trainerstring[9] = "Resources.character_littleboy";
            trainerstring[10] = "Resources.character_littlegirl";
            trainerstring[11] = "Resources.character_dad";
            trainerstring[12] = "Resources.character_dad2";
            trainerstring[13] = "Resources.character_mom";
            trainerstring[14] = "Resources.character_oldman";
            trainerstring[15] = "Resources.character_oldman2";
            trainerstring[16] = "Resources.character_oldwoman";
            trainerstring[17] = "Resources.character_explorer";
            trainerstring[18] = "Resources.character_magician";
            trainerstring[19] = "Resources.character_swimmer";
            trainerstring[20] = "Resources.character_swimmer2";
            trainerstring[21] = "Resources.character_swimmer3";
            trainerstring[22] = "Resources.character_swimmer4";
            trainerstring[23] = "Resources.character_fisherman";
            trainerstring[24] = "Resources.character_dino";
            trainerstring[25] = "Resources.character_cameraman";
            trainerstring[26] = "Resources.character_painter";
            trainerstring[27] = "Resources.character_teamrocket";
            trainerstring[28] = "Resources.character_teamrocket2";
            trainerstring[29] = "Resources.character_policeman";

            numCharacter.Minimum = 1;
            numCharacter.Maximum = 29;
            numCharacter.Value = 1;

            numStructureType.Minimum = 1;
            numStructureType.Maximum = 3;
            numStructureType.Value = 1;

            numStructureSubtype.Minimum = 0;
            numStructureSubtype.Maximum = 1;
            numStructureSubtype.Value = 0;

            numTree.Minimum = 1;
            numTree.Maximum = 3;
            numTree.Value = 1;
        }

        private void cbBackground_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBackground.SelectedIndex == 0)
            {
                pbCanvas.BackgroundImage = Resources.grass;
            }
            if (cbBackground.SelectedIndex == 1)
            {
                pbCanvas.BackgroundImage = Resources.townpavement;
            }
            if (cbBackground.SelectedIndex == 2)
            {
                pbCanvas.BackgroundImage = Resources.mountain;
            }
            if (cbBackground.SelectedIndex == 3)
            {
                pbCanvas.BackgroundImage = Resources.waterwaves;
            }
            if (cbBackground.SelectedIndex == 4)
            {
                pbCanvas.BackgroundImage = Resources.ground;
            }
            if (cbBackground.SelectedIndex == 5)
            {
                pbCanvas.BackgroundImage = Resources.snow;
            }
            if (cbBackground.SelectedIndex == 6)
            {
                pbCanvas.BackgroundImage = Resources.ancientcave;
            }
        }

        List<Trees> trees = new List<Trees>(); // 1
        List<WildGrass> wildgrass = new List<WildGrass>(); // 2
        List<Flowers> flowers = new List<Flowers>(); // 3
        List<Hills> hills = new List<Hills>(); // 4
        List<Buildings> buildings = new List<Buildings>(); // 5
        List<ExitPoint> exit = new List<ExitPoint>(); // 6
        List<BlockedZone> blockedzone = new List<BlockedZone>(); // 7
        List<ActionPoint> actionpoint = new List<ActionPoint>(); // 8
        List<Paths> paths = new List<Paths>(); // 9
        List<Character> characters = new List<Character>(); // 10
        List<string> characterString = new List<string>();
        List<Structures> structures = new List<Structures>(); // 11

        int palette, type, subtype;
        Point position, position2;

        private void pbCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {

                if (palette == 1)
                {
                    position = new Point(e.X - 40, e.Y - 45);
                    trees.Add(new Trees(type, position.X, position.Y));
                }
                if (palette == 2)
                {
                    position = new Point(e.X - 20, e.Y - 20);
                    wildgrass.Add(new WildGrass(position.X, position.Y));
                }
                if (palette == 3)
                {
                    position = new Point(e.X - 20, e.Y - 20);
                    flowers.Add(new Flowers(position.X, position.Y));
                }
                if (palette == 4)
                {
                    if (type == 0)
                    {
                        type = 1;
                        position = new Point(e.X, e.Y - 10);
                    }
                    else
                    {
                        type = 0;
                        if (e.X - position.X > 0)
                            hills.Add(new Hills(position.X, position.Y, e.X - position.X));
                    }
                }
                if (palette == 5)
                {
                    position = new Point(e.X - 90, e.Y - 67);
                    buildings.Add(new Buildings(type, position.X, position.Y, checkInside.Checked));
                }
                if (palette == 6)
                {
                    if (type == 0)
                    {
                        type = 1;
                        position = new Point(e.X, e.Y);
                    }
                    else if (type == 1)
                    {
                        type = 2;
                        position2 = new Point(e.X, e.Y);
                    }
                    else
                    {
                        type = 0;
                        exit.Add(new ExitPoint(position.X, position.Y, position2.X, position2.Y, (int)numZones.Value, e.X, e.Y));
                    }
                }
                if (palette == 7)
                {
                    if (type == 0)
                    {
                        type = 1;
                        position = new Point(e.X, e.Y);
                    }
                    else
                    {
                        type = 0;
                        blockedzone.Add(new BlockedZone(position.X, position.Y, e.X, e.Y));
                    }
                }
                if (palette == 8)
                {
                    if (type == 0)
                    {
                        type = 1;
                        position = new Point(e.X, e.Y);
                    }
                    else
                    {
                        type = 0;
                        actionpoint.Add(new ActionPoint(position.X, position.Y, e.X, e.Y, (int)numZones.Value));
                    }
                }
                if (palette == 9)
                {
                    position = new Point(e.X - 20, e.Y - 20);
                    paths.Add(new Paths(type, position.X, position.Y));
                }
                if (palette == 10)
                {
                    position = new Point(e.X - 24, e.Y - 31);
                    characters.Add(new Character(tbName.Text, trainers[(int)numCharacter.Value], position.X, position.Y));
                    characterString.Add(trainerstring[(int)numCharacter.Value]);

                    for (int j = 0; j < 6; ++j)
                    {
                        if (trainerPokemon[j].Text != "")
                        {
                            List<List<string>> ret = DataBase.AdminGetData("SELECT ID FROM PokeDex WHERE Name = '" + trainerPokemon[j].Text + "'");
                            characters[characters.Count - 1].pokemons.Add(new Pokemons(Convert.ToInt32(ret[0][0]), (int)trainerNumeric[j].Value));
                        }
                    }

                    //actionpoint.Add(new ActionPoint(position.X - 10, position.Y - 10, position.X + 60, position.Y + 50, characters.Count - 1));
                }
                if (palette == 11)
                {
                    if (subtype == -1)
                    {
                        position = new Point(e.X - 20, e.Y - 20);
                        structures.Add(new Structures(1, 1, type, position.X, position.Y));
                    }
                    else
                    {
                        structures.Add(new Structures(type, subtype, e.X, e.Y));
                        position = new Point(e.X - structures[structures.Count - 1].sizeX / 2,
                            e.Y - structures[structures.Count - 1].sizeY);
                        structures[structures.Count - 1].X = position.X;
                        structures[structures.Count - 1].Y = position.Y;
                    }
                }
            }
            else
            {
                for (int i = 0; i < trees.Count; ++i)
                {
                    if (trees[i].X <= e.X && trees[i].X + trees[i].sizeX >= e.X &&
                        trees[i].Y <= e.Y && trees[i].Y + trees[i].sizeY >= e.Y)
                    {
                        position = new Point(trees[i].X, trees[i].Y);
                        if (e.Button == System.Windows.Forms.MouseButtons.Middle) trees.RemoveAt(i);
                        break;
                    }
                }
                for (int i = 0; i < paths.Count; ++i)
                {
                    if (paths[i].X <= e.X && paths[i].X + 40 >= e.X &&
                        paths[i].Y <= e.Y && paths[i].Y + 40 >= e.Y)
                    {
                        position = new Point(paths[i].X, paths[i].Y);
                        if (e.Button == System.Windows.Forms.MouseButtons.Middle) paths.RemoveAt(i);
                        break;
                    }
                }
                for (int i = 0; i < structures.Count; ++i)
                {
                    if (structures[i].X <= e.X && structures[i].X + structures[i].sizeX >= e.X &&
                        structures[i].Y <= e.Y && structures[i].Y + structures[i].sizeY >= e.Y)
                    {
                        position = new Point(structures[i].X, structures[i].Y);
                        if (e.Button == System.Windows.Forms.MouseButtons.Middle) structures.RemoveAt(i);
                        break;
                    }
                }
                for (int i = 0; i < flowers.Count; ++i)
                {
                    if (flowers[i].X <= e.X && flowers[i].X + 40 >= e.X &&
                        flowers[i].Y <= e.Y && flowers[i].Y + 40 >= e.Y)
                    {
                        position = new Point(flowers[i].X, flowers[i].Y);
                        if (e.Button == System.Windows.Forms.MouseButtons.Middle) flowers.RemoveAt(i);
                        break;
                    }
                }
                for (int i = 0; i < wildgrass.Count; ++i)
                {
                    if (wildgrass[i].X <= e.X && wildgrass[i].X + 40 >= e.X &&
                        wildgrass[i].Y <= e.Y && wildgrass[i].Y + 40 >= e.Y)
                    {
                        position = new Point(wildgrass[i].X, wildgrass[i].Y);
                        if (e.Button == System.Windows.Forms.MouseButtons.Middle) wildgrass.RemoveAt(i);
                        break;
                    }
                }
                for (int i = 0; i < buildings.Count; ++i)
                {
                    if (buildings[i].X <= e.X && buildings[i].X + buildings[i].sizeX >= e.X &&
                        buildings[i].Y <= e.Y && buildings[i].Y + buildings[i].sizeY >= e.Y)
                    {
                        if (e.Button == System.Windows.Forms.MouseButtons.Middle) buildings.RemoveAt(i);
                        break;
                    }
                }
                for (int i = 0; i < hills.Count; ++i)
                {
                    if (hills[i].X <= e.X && hills[i].X + 40 >= e.X &&
                        hills[i].Y <= e.Y && hills[i].Y + 40 >= e.Y)
                    {
                        if (e.Button == System.Windows.Forms.MouseButtons.Middle) hills.RemoveAt(i);
                        break;
                    }
                }
                for (int i = 0; i < characters.Count; ++i)
                {
                    if (characters[i].X <= e.X && characters[i].X + 48 >= e.X &&
                        characters[i].Y <= e.Y && characters[i].Y + 62 >= e.Y)
                    {
                        if (e.Button == System.Windows.Forms.MouseButtons.Middle)
                        {
                            characters.RemoveAt(i);
                            characterString.RemoveAt(i);
                        }
                        break;
                    }
                }
                for (int i = 0; i < exit.Count; ++i)
                {
                    if (exit[i].Xstart <= e.X && exit[i].Xfin >= e.X &&
                        exit[i].Ystart <= e.Y && exit[i].Yfin >= e.Y)
                    {
                        if (e.Button == System.Windows.Forms.MouseButtons.Middle) exit.RemoveAt(i);
                        break;
                    }
                }
                for (int i = 0; i < blockedzone.Count; ++i)
                {
                    if (blockedzone[i].Xstart <= e.X && blockedzone[i].Xfin >= e.X &&
                        blockedzone[i].Ystart <= e.Y && blockedzone[i].Yfin >= e.Y)
                    {
                        if (e.Button == System.Windows.Forms.MouseButtons.Middle) blockedzone.RemoveAt(i);
                        break;
                    }
                }
                for (int i = 0; i < actionpoint.Count; ++i)
                {
                    if (actionpoint[i].Xstart <= e.X && actionpoint[i].Xfin >= e.X &&
                        actionpoint[i].Ystart <= e.Y && actionpoint[i].Yfin >= e.Y)
                    {
                        if (e.Button == System.Windows.Forms.MouseButtons.Middle) actionpoint.RemoveAt(i);
                        break;
                    }
                }
            }

            pbCanvas.Invalidate();
        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            trees = trees.OrderBy(o => o.Y).ToList();
            buildings = buildings.OrderBy(o => o.Y).ToList();

            for (int i = 0; i < blockedzone.Count; ++i) // draw blocked zones
            {
                canvas.FillRectangle(Brushes.DarkGray, blockedzone[i].Xstart, blockedzone[i].Ystart,
                    blockedzone[i].Xfin - blockedzone[i].Xstart, blockedzone[i].Yfin - blockedzone[i].Ystart);
            }

            for (int i = 0; i < exit.Count; ++i) // draw exits
            {
                canvas.FillRectangle(Brushes.PaleVioletRed, exit[i].Xstart, exit[i].Ystart,
                    exit[i].Xfin - exit[i].Xstart, exit[i].Yfin - exit[i].Ystart);
            }

            for (int i = 0; i < actionpoint.Count; ++i) // draw action points
            {
                canvas.FillRectangle(Brushes.OrangeRed, actionpoint[i].Xstart, actionpoint[i].Ystart,
                    actionpoint[i].Xfin - actionpoint[i].Xstart, actionpoint[i].Yfin - actionpoint[i].Ystart);
            }

            for (int i = 0; i < hills.Count; ++i) // draw hills
            {
                canvas.DrawImage(hills[i].image, hills[i].X, hills[i].Y);
            }

            for (int i = 0; i < flowers.Count; ++i) // draw flowers
            {
                flowers[i].ChangeSprite();
                canvas.DrawImage(flowers[i].image, flowers[i].X, flowers[i].Y);
            }

            for (int i = 0; i < wildgrass.Count; ++i) // draw wild grass
            {
                canvas.DrawImage(wildgrass[i].image, wildgrass[i].X, wildgrass[i].Y);
            }

            for (int i = 0; i < paths.Count; ++i) // draw paths
            {
                canvas.DrawImage(paths[i].image, paths[i].X, paths[i].Y);
            }

            for (int i = 0; i < trees.Count; ++i) // draw trees
            {
                canvas.DrawImage(trees[i].image, trees[i].X, trees[i].Y);
            }

            for (int i = 0; i < structures.Count; ++i) // draw structures
            {
                canvas.DrawImage(structures[i].image, structures[i].X, structures[i].Y);
            }

            for (int i = 0; i < buildings.Count; ++i) // draw buildings
            {
                canvas.DrawImage(buildings[i].image, buildings[i].X, buildings[i].Y);
            }

            for (int i = 0; i < characters.Count; ++i) // draw characters
            {
                canvas.DrawImage(characters[i].crtSprite, characters[i].X, characters[i].Y);
            }
        }

        private void DrawByButton(int x, int y)
        {
            if (palette == 1) { x *= 80; y *= 60; }
            if (palette == 2) { x *= 40; y *= 40; }
            if (palette == 3) { x *= 40; y *= 40; }
            if (palette == 9) { x *= 40; y *= 40; }
            if (palette == 11)
            {
                if (subtype == -1) { x *= 40; y *= 40; }
                else if (type == 1)
                {
                    if (subtype == 0) { x *= 80; y *= 60; }
                    else { x *= 20; y *= 30; }
                }
            }

            position = new Point(position.X + x, position.Y + y);

            if (palette == 1) trees.Add(new Trees(type, position.X, position.Y));
            if (palette == 2) wildgrass.Add(new WildGrass(position.X, position.Y));
            if (palette == 3) flowers.Add(new Flowers(position.X, position.Y));
            if (palette == 9) paths.Add(new Paths(type, position.X, position.Y));
            if (palette == 11)
            {
                if (subtype == -1)
                    structures.Add(new Structures(1, 1, type, position.X, position.Y));
                else
                    structures.Add(new Structures(type, subtype, position.X, position.Y));
            }

            pbCanvas.Invalidate();
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            DrawByButton(0, 1);
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            DrawByButton(-1, 0);
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {
            DrawByButton(1, 0);
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            DrawByButton(0, -1);
        }

        private void pbTree_MouseClick(object sender, MouseEventArgs e)
        {
            palette = 1;
            type = (int)numTree.Value;
        }

        private void pbWildgrass_MouseClick(object sender, MouseEventArgs e)
        {
            palette = 2;
        }

        private void pbFlower_MouseClick(object sender, MouseEventArgs e)
        {
            palette = 3;
        }

        private void pbHill_MouseClick(object sender, MouseEventArgs e)
        {
            palette = 4;
            type = 0;
        }

        private void pbBuilding_MouseClick(object sender, MouseEventArgs e)
        {
            palette = 5;
            type = (int)numBuilding.Value;
        }

        private void numBuilding_ValueChanged(object sender, EventArgs e)
        {
            int val = (int)numBuilding.Value;
            pbBuilding.Image = new Buildings(val, 0, 0, false).image;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            palette = 6;
            type = 0;
        }

        private void buttonBlocked_Click(object sender, EventArgs e)
        {
            palette = 7;
            type = 0;
        }

        private void buttonAction_Click(object sender, EventArgs e)
        {
            palette = 8;
            type = 0;
        }

        private void path_MouseClick(object sender, MouseEventArgs e)
        {
            palette = 9;

            for (int i = 0; i < 9; ++i)
                if ((PictureBox)sender == pbpath[i])
                {
                    type = i + 1;
                    break;
                }
        }

        private void pbCharacter_MouseClick(object sender, MouseEventArgs e)
        {
            palette = 10;
        }

        private void water_MouseClick(object sender, MouseEventArgs e)
        {
            palette = 11;

            for (int i = 0; i < 9; ++i)
                if ((PictureBox)sender == pbwater[i])
                {
                    type = i + 1;
                    subtype = -1;
                    break;
                }
        }

        private void numStructureType_ValueChanged(object sender, EventArgs e)
        {
            type = (int)numStructureType.Value;

            if (type > 1) numStructureSubtype.Value = 0;

            pbStructure.Image = new Structures(type, subtype, 0, 0).image;
        }

        private void numStructureSubtype_ValueChanged(object sender, EventArgs e)
        {
            subtype = (int)numStructureSubtype.Value;

            pbStructure.Image = new Structures(type, subtype, 0, 0).image;
        }

        private void pbStructure_MouseClick(object sender, MouseEventArgs e)
        {
            palette = 11;
            type = (int)numStructureType.Value;
            subtype = (int)numStructureSubtype.Value;
        }

        private void numCharacter_ValueChanged(object sender, EventArgs e)
        {
            int val = (int)numCharacter.Value;
            pbCharacter.Image = trainers[val];
        }

        private void cbPokemon_Populate(ComboBox cb)
        {
            cb.Items.Clear();

            List<List<string>> ret = DataBase.AdminGetData("SELECT Name FROM PokeDex");
            for (int i = 0; i < ret.Count; ++i)
            {
                cb.Items.Add(ret[i][0]);
            }
        }

        private void buttonCode_Click(object sender, EventArgs e)
        {
            tbCode.Text = "private void LoadMap()\n{\n";

            tbCode.Text += "    pbCanvas.BackgroundImage = Resources." + cbBackground.Text + ";\n\n";

            for (int i = 0; i < exit.Count; ++i)
                tbCode.Text += "    exit.Add(new ExitPoint(" + exit[i].Xstart + ", " + exit[i].Ystart + ", "
                + exit[i].Xfin + ", " + exit[i].Yfin + ", " + exit[i].mapNr + ", " + exit[i].Xmap + ", " + exit[i].Ymap + "));\n";
            if (exit.Count > 0) tbCode.Text += "\n";

            for (int i = 0; i < blockedzone.Count; ++i)
                tbCode.Text += "    blockedzone.Add(new BlockedZone(" + blockedzone[i].Xstart + ", "
                + blockedzone[i].Ystart + ", " + blockedzone[i].Xfin + ", " + blockedzone[i].Yfin + "));\n";
            if (blockedzone.Count > 0) tbCode.Text += "\n";

            for (int i = 0; i < actionpoint.Count; ++i)
                tbCode.Text += "    actionpoint.Add(new ActionPoint(" + actionpoint[i].Xstart + ", " + actionpoint[i].Ystart
                    + ", " + actionpoint[i].Xfin + ", " + actionpoint[i].Yfin + ", " + actionpoint[i].action + "));\n";
            if (actionpoint.Count > 0) tbCode.Text += "\n";

            bool wildPokemonsExist = false;
            int chanceSum = 0;
            for (int i = 0; i < 10; ++i)
            {
                if (wildpokemonNames[i].Text != "")
                {
                    wildPokemonsExist = true;
                    List<List<string>> ret = DataBase.AdminGetData("SELECT ID FROM PokeDex WHERE Name = '" + wildpokemonNames[i].Text + "'");
                    tbCode.Text += "    wildPokemons.Add(new ints(" + ret[0][0] + ", " + numLevelMin1.Value + ", " + numLevelMax1.Value + ", " + wildpokemonChance[i].Value + "));\n";
                    chanceSum += (int)wildpokemonChance[i].Value;
                }
            }
            if (wildPokemonsExist)
            {
                if(chanceSum != 100)
                {
                    tbCode.Text = "The sum of the chances is not 100.";
                    return;
                }
                tbCode.Text += "\n";
            }

            for (int i = 0; i < characters.Count; ++i)
            {
                tbCode.Text += "    characters.Add(new Character(\"" + characters[i].name + "\", " + characterString[i]
                    + " , " + characters[i].X + ", " + characters[i].Y + "));\n";

                for (int j = 0; j < characters[i].pokemons.Count; ++j)
                {
                    tbCode.Text += "    characters[" + i + "].pokemons.Add(new Pokemons(" + characters[i].pokemons[j].index + ", "
                            + characters[i].pokemons[j].level + "));\n";
                }

                tbCode.Text += "    characters[" + i + "].index = " + ((int)numCharIndex.Value + i) + ";\n";
                tbCode.Text += "    actionpoint.Add(new ActionPoint(" + (characters[i].X - 10) + ", " + (characters[i].Y - 10)
                    + ", " + (characters[i].X + 60) + ", " + (characters[i].Y + 50) + ", " + i + "));\n\n";
            }

            tbCode.Text += "    #region Environment\n\n";

            for (int i = 0; i < structures.Count; ++i)
            {
                if (structures[i].type == 1 && structures[i].subtype2 != 0)
                    tbCode.Text += "    structures.Add(new Structures(" + structures[i].type + ", " +
                        structures[i].subtype + ", " + structures[i].subtype2 + ", " + structures[i].X + ", " +
                        structures[i].Y + "));\n";
                else tbCode.Text += "    structures.Add(new Structures(" + structures[i].type + ", " +
                        structures[i].subtype + ", " + structures[i].X + ", " + structures[i].Y + "));\n";
            }
            if (structures.Count > 0) tbCode.Text += "\n";

            for (int i = 0; i < trees.Count; ++i)
                tbCode.Text += "    trees.Add(new Trees(" + trees[i].type + ", " + trees[i].X + ", " + trees[i].Y + "));\n";
            if (trees.Count > 0) tbCode.Text += "\n";

            for (int i = 0; i < buildings.Count; ++i)
                tbCode.Text += "    buildings.Add(new Buildings(" + buildings[i].type + ", " + buildings[i].X
                    + ", " + buildings[i].Y + ", " + buildings[i].hasInside.ToString().ToLower() + "));\n";
            if (buildings.Count > 0) tbCode.Text += "\n";

            for (int i = 0; i < flowers.Count; ++i)
                tbCode.Text += "    flowers.Add(new Flowers(" + flowers[i].X + ", " + flowers[i].Y + "));\n";
            if (flowers.Count > 0) tbCode.Text += "\n";

            for (int i = 0; i < wildgrass.Count; ++i)
                tbCode.Text += "    wildgrass.Add(new WildGrass(" + wildgrass[i].X + ", " + wildgrass[i].Y + "));\n";
            if (wildgrass.Count > 0) tbCode.Text += "\n";

            for (int i = 0; i < paths.Count; ++i)
                tbCode.Text += "    paths.Add(new Paths(" + paths[i].type + ", " + paths[i].X + ", " + paths[i].Y + "));\n";
            if (paths.Count > 0) tbCode.Text += "\n";

            for (int i = 0; i < hills.Count; ++i)
                tbCode.Text += "    hills.Add(new Hills(" + hills[i].X + ", " + hills[i].Y + ", " + hills[i].sizeX + "));\n";
            if (hills.Count > 0) tbCode.Text += "\n";

            tbCode.Text += "    #endregion\n";

            tbCode.Text += "}";
        }
    }
}
