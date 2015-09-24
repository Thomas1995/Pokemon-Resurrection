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
    public partial class AdminPanel : Form
    {
        public AdminPanel()
        {
            InitializeComponent();

            cbPokemon_Populate();
            cbType_Populate(cbType1);
            cbType_Populate(cbType2);
        }

        private void cbPokemon_Populate()
        {
            cbPokemon.Items.Clear();

            List<List<string>> ret = DataBase.AdminGetData("SELECT Name FROM PokeDex");
            for(int i=0;i<ret.Count;++i)
            {
                cbPokemon.Items.Add(ret[i][0]);
            }
        }

        private void cbAttack_Populate(ComboBox cb)
        {
            List<List<string>> ret = DataBase.AdminGetData("SELECT Name FROM AttackDex ORDER BY Name");
            for (int i = 0; i < ret.Count; ++i)
            {
                cb.Items.Add(ret[i][0]);
            }
        }

        private void cbType_Populate(ComboBox cb)
        {
            cb.Items.Add("Normal");
            cb.Items.Add("Fire");
            cb.Items.Add("Water");
            cb.Items.Add("Electricity");
            cb.Items.Add("Grass");
            cb.Items.Add("Ice");
            cb.Items.Add("Fight");
            cb.Items.Add("Poison");
            cb.Items.Add("Ground");
            cb.Items.Add("Flying");
            cb.Items.Add("Psychic");
            cb.Items.Add("Bug");
            cb.Items.Add("Rock");
            cb.Items.Add("Ghost");
            cb.Items.Add("Dragon");
            cb.Items.Add("Dark");
            cb.Items.Add("Steel");
        }

        List<ComboBox> cbList = new List<ComboBox>();
        List<NumericUpDown> nrList = new List<NumericUpDown>();

        int ID;

        private void cbPokemon_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = cbPokemon.Items[cbPokemon.SelectedIndex] + "";

            List<List<string>> ret = DataBase.AdminGetData("SELECT * FROM PokeDex WHERE Name = '" + name + "'");

            ID = Convert.ToInt32(ret[0][0]);

            tbID.Text = ret[0][0];
            tbName.Text = ret[0][1];
            tbGender.Text = ret[0][2];
            tbHP.Text = ret[0][3];
            tbAtk.Text = ret[0][4];
            tbDef.Text = ret[0][5];
            tbSpatk.Text = ret[0][6];
            tbSpdef.Text = ret[0][7];
            tbSpeed.Text = ret[0][8];
            tbCapture.Text = ret[0][11];
            tbXP.Text = ret[0][12];
            tbWeight.Text = ret[0][13];

            cbType1.SelectedIndex = Convert.ToInt32(ret[0][9]) - 1;
            cbType2.SelectedIndex = Convert.ToInt32(ret[0][10]) - 1;

            pbPokemon.Image = Resources.pokeFront[ID];

            for(int i=0;i<cbList.Count;++i)
            {
                panel.Controls.Remove(cbList[i]);
                panel.Controls.Remove(nrList[i]);
            }

            cbList = new List<ComboBox>();
            nrList = new List<NumericUpDown>();

            ret = DataBase.AdminGetData("SELECT AttackID, Level FROM PokeAttack WHERE PokeID = " + ID + " ORDER BY Level");

            for (int i = 0; i < ret.Count; ++i)
            {
                cbList.Add(new ComboBox());
                ComboBox cb = cbList[cbList.Count - 1];
                cbAttack_Populate(cb);

                nrList.Add(new NumericUpDown());
                NumericUpDown nrud = nrList[nrList.Count - 1];

                int atkID = Convert.ToInt32(ret[i][0]);

                List<List<string>> ret2 = DataBase.AdminGetData("SELECT Name FROM AttackDex WHERE ID = " + atkID);
                cb.Text = ret2[0][0];
                
                nrud.Value = Convert.ToInt32(ret[i][1]);

                cb.Location = new Point((30 * i / 210) * 180, 30 * i % 210);
                nrud.Location = new Point((30 * i / 210) * 180 + 130, 30 * i % 210);
                nrud.Size = new Size(40, 21);
                panel.Controls.Add(cb);
                panel.Controls.Add(nrud);
            }
        }

        private void buttonNewAttack_Click(object sender, EventArgs e)
        {
            cbList.Add(new ComboBox());
            nrList.Add(new NumericUpDown());
            int i = cbList.Count - 1;

            ComboBox cb = cbList[i];
            NumericUpDown nrud = nrList[i];

            cbAttack_Populate(cb);

            cb.Location = new Point((30 * i / 210) * 180, 30 * i % 210);
            nrud.Location = new Point((30 * i / 210) * 180 + 130, 30 * i % 210);
            nrud.Size = new Size(40, 21);
            panel.Controls.Add(cb);
            panel.Controls.Add(nrud);
        }

        private void buttonDeleteAttack_Click(object sender, EventArgs e)
        {
            int i = cbList.Count - 1;

            if (i == -1) return;

            panel.Controls.Remove(cbList[i]);
            panel.Controls.Remove(nrList[i]);
            cbList.RemoveAt(i);
            nrList.RemoveAt(i);
        }

        private void buttonChangePokemon_Click(object sender, EventArgs e)
        {
            ChangeMoves();
        }

        private void ChangeMoves()
        {
            DataBase.AdminUpdate("DELETE FROM PokeAttack WHERE PokeID = " + tbID.Text);

            for (int i = 0; i < cbList.Count; ++i)
            {
                List<List<string>> ret = DataBase.AdminGetData("SELECT ID FROM AttackDex WHERE Name = '" + cbList[i].Text + "'");
                if (ret.Count > 0 && ret[0].Count > 0)
                {
                    int atkID = Convert.ToInt32(ret[0][0]);
                    DataBase.AdminUpdate("INSERT INTO PokeAttack VALUES (" + tbID.Text + " , " + atkID + "," + nrList[i].Value + ")");
                }
            }
        }

        private void buttonAddPokemon_Click(object sender, EventArgs e)
        {
            DataBase.AdminUpdate("INSERT INTO PokeDex VALUES (" + tbID.Text + " , '" + tbName.Text + "' , "
                + tbGender.Text + " , " + tbHP.Text + " , " + tbAtk.Text + " , " + tbDef.Text + " , "
                + tbSpatk.Text + " , " + tbSpdef.Text + " , " + tbSpeed.Text + " , " + (cbType1.SelectedIndex + 1) + " , "
                + (cbType2.SelectedIndex + 1) + " , " + tbCapture.Text + " , " + tbXP.Text + " , " + tbWeight.Text + ")");

            cbPokemon_Populate();

            ChangeMoves();
        }
    }
}
