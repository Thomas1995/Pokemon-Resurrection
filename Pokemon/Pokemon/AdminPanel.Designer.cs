namespace Pokemon
{
    partial class AdminPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbPokemon = new System.Windows.Forms.ComboBox();
            this.pbPokemon = new System.Windows.Forms.PictureBox();
            this.tbID = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbAtk = new System.Windows.Forms.TextBox();
            this.tbDef = new System.Windows.Forms.TextBox();
            this.tbSpatk = new System.Windows.Forms.TextBox();
            this.tbSpdef = new System.Windows.Forms.TextBox();
            this.tbSpeed = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbHP = new System.Windows.Forms.TextBox();
            this.tbGender = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbXP = new System.Windows.Forms.TextBox();
            this.tbWeight = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tbCapture = new System.Windows.Forms.TextBox();
            this.cbType1 = new System.Windows.Forms.ComboBox();
            this.cbType2 = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.panel = new System.Windows.Forms.Panel();
            this.buttonNewAttack = new System.Windows.Forms.Button();
            this.buttonDeleteAttack = new System.Windows.Forms.Button();
            this.buttonChangePokemon = new System.Windows.Forms.Button();
            this.buttonAddPokemon = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbPokemon)).BeginInit();
            this.SuspendLayout();
            // 
            // cbPokemon
            // 
            this.cbPokemon.FormattingEnabled = true;
            this.cbPokemon.Location = new System.Drawing.Point(12, 12);
            this.cbPokemon.Name = "cbPokemon";
            this.cbPokemon.Size = new System.Drawing.Size(141, 21);
            this.cbPokemon.TabIndex = 0;
            this.cbPokemon.SelectedIndexChanged += new System.EventHandler(this.cbPokemon_SelectedIndexChanged);
            // 
            // pbPokemon
            // 
            this.pbPokemon.BackColor = System.Drawing.Color.White;
            this.pbPokemon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbPokemon.Location = new System.Drawing.Point(12, 39);
            this.pbPokemon.Name = "pbPokemon";
            this.pbPokemon.Size = new System.Drawing.Size(80, 80);
            this.pbPokemon.TabIndex = 1;
            this.pbPokemon.TabStop = false;
            // 
            // tbID
            // 
            this.tbID.Location = new System.Drawing.Point(118, 57);
            this.tbID.Name = "tbID";
            this.tbID.Size = new System.Drawing.Size(45, 20);
            this.tbID.TabIndex = 2;
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(169, 57);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(100, 20);
            this.tbName.TabIndex = 3;
            // 
            // tbAtk
            // 
            this.tbAtk.Location = new System.Drawing.Point(331, 57);
            this.tbAtk.Name = "tbAtk";
            this.tbAtk.Size = new System.Drawing.Size(45, 20);
            this.tbAtk.TabIndex = 5;
            // 
            // tbDef
            // 
            this.tbDef.Location = new System.Drawing.Point(382, 57);
            this.tbDef.Name = "tbDef";
            this.tbDef.Size = new System.Drawing.Size(45, 20);
            this.tbDef.TabIndex = 6;
            // 
            // tbSpatk
            // 
            this.tbSpatk.Location = new System.Drawing.Point(433, 57);
            this.tbSpatk.Name = "tbSpatk";
            this.tbSpatk.Size = new System.Drawing.Size(45, 20);
            this.tbSpatk.TabIndex = 7;
            // 
            // tbSpdef
            // 
            this.tbSpdef.Location = new System.Drawing.Point(484, 57);
            this.tbSpdef.Name = "tbSpdef";
            this.tbSpdef.Size = new System.Drawing.Size(45, 20);
            this.tbSpdef.TabIndex = 8;
            // 
            // tbSpeed
            // 
            this.tbSpeed.Location = new System.Drawing.Point(535, 57);
            this.tbSpeed.Name = "tbSpeed";
            this.tbSpeed.Size = new System.Drawing.Size(45, 20);
            this.tbSpeed.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(128, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(205, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(340, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "ATK";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(391, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "DEF";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(434, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "SPATK";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(485, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "SPDEF";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(535, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "SPEED";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(292, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(22, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "HP";
            // 
            // tbHP
            // 
            this.tbHP.Location = new System.Drawing.Point(280, 57);
            this.tbHP.Name = "tbHP";
            this.tbHP.Size = new System.Drawing.Size(45, 20);
            this.tbHP.TabIndex = 4;
            // 
            // tbGender
            // 
            this.tbGender.Location = new System.Drawing.Point(195, 99);
            this.tbGender.Name = "tbGender";
            this.tbGender.Size = new System.Drawing.Size(45, 20);
            this.tbGender.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(190, 83);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Male Ratio";
            // 
            // tbXP
            // 
            this.tbXP.Location = new System.Drawing.Point(329, 99);
            this.tbXP.Name = "tbXP";
            this.tbXP.Size = new System.Drawing.Size(45, 20);
            this.tbXP.TabIndex = 12;
            // 
            // tbWeight
            // 
            this.tbWeight.Location = new System.Drawing.Point(393, 99);
            this.tbWeight.Name = "tbWeight";
            this.tbWeight.Size = new System.Drawing.Size(45, 20);
            this.tbWeight.TabIndex = 13;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(328, 83);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "XP Given";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(396, 83);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "Weight";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(248, 83);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 13);
            this.label12.TabIndex = 25;
            this.label12.Text = "Capture Rate";
            // 
            // tbCapture
            // 
            this.tbCapture.Location = new System.Drawing.Point(260, 99);
            this.tbCapture.Name = "tbCapture";
            this.tbCapture.Size = new System.Drawing.Size(45, 20);
            this.tbCapture.TabIndex = 11;
            // 
            // cbType1
            // 
            this.cbType1.FormattingEnabled = true;
            this.cbType1.Location = new System.Drawing.Point(615, 56);
            this.cbType1.Name = "cbType1";
            this.cbType1.Size = new System.Drawing.Size(95, 21);
            this.cbType1.TabIndex = 26;
            // 
            // cbType2
            // 
            this.cbType2.FormattingEnabled = true;
            this.cbType2.Location = new System.Drawing.Point(615, 98);
            this.cbType2.Name = "cbType2";
            this.cbType2.Size = new System.Drawing.Size(95, 21);
            this.cbType2.TabIndex = 27;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(640, 39);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(40, 13);
            this.label13.TabIndex = 28;
            this.label13.Text = "Type 1";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(640, 80);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(40, 13);
            this.label14.TabIndex = 29;
            this.label14.Text = "Type 2";
            // 
            // panel
            // 
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel.Location = new System.Drawing.Point(8, 135);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(708, 210);
            this.panel.TabIndex = 30;
            // 
            // buttonNewAttack
            // 
            this.buttonNewAttack.Location = new System.Drawing.Point(12, 352);
            this.buttonNewAttack.Name = "buttonNewAttack";
            this.buttonNewAttack.Size = new System.Drawing.Size(75, 23);
            this.buttonNewAttack.TabIndex = 31;
            this.buttonNewAttack.Text = "New Attack";
            this.buttonNewAttack.UseVisualStyleBackColor = true;
            this.buttonNewAttack.Click += new System.EventHandler(this.buttonNewAttack_Click);
            // 
            // buttonDeleteAttack
            // 
            this.buttonDeleteAttack.Location = new System.Drawing.Point(93, 352);
            this.buttonDeleteAttack.Name = "buttonDeleteAttack";
            this.buttonDeleteAttack.Size = new System.Drawing.Size(84, 23);
            this.buttonDeleteAttack.TabIndex = 32;
            this.buttonDeleteAttack.Text = "Delete Attack";
            this.buttonDeleteAttack.UseVisualStyleBackColor = true;
            this.buttonDeleteAttack.Click += new System.EventHandler(this.buttonDeleteAttack_Click);
            // 
            // buttonChangePokemon
            // 
            this.buttonChangePokemon.Location = new System.Drawing.Point(615, 352);
            this.buttonChangePokemon.Name = "buttonChangePokemon";
            this.buttonChangePokemon.Size = new System.Drawing.Size(101, 23);
            this.buttonChangePokemon.TabIndex = 33;
            this.buttonChangePokemon.Text = "Change Pokemon";
            this.buttonChangePokemon.UseVisualStyleBackColor = true;
            this.buttonChangePokemon.Click += new System.EventHandler(this.buttonChangePokemon_Click);
            // 
            // buttonAddPokemon
            // 
            this.buttonAddPokemon.Location = new System.Drawing.Point(508, 352);
            this.buttonAddPokemon.Name = "buttonAddPokemon";
            this.buttonAddPokemon.Size = new System.Drawing.Size(101, 23);
            this.buttonAddPokemon.TabIndex = 34;
            this.buttonAddPokemon.Text = "Add Pokemon";
            this.buttonAddPokemon.UseVisualStyleBackColor = true;
            this.buttonAddPokemon.Click += new System.EventHandler(this.buttonAddPokemon_Click);
            // 
            // AdminPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 384);
            this.Controls.Add(this.buttonAddPokemon);
            this.Controls.Add(this.buttonChangePokemon);
            this.Controls.Add(this.buttonDeleteAttack);
            this.Controls.Add(this.buttonNewAttack);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cbType2);
            this.Controls.Add(this.cbType1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.tbCapture);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tbWeight);
            this.Controls.Add(this.tbXP);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbGender);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbHP);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbSpeed);
            this.Controls.Add(this.tbSpdef);
            this.Controls.Add(this.tbSpatk);
            this.Controls.Add(this.tbDef);
            this.Controls.Add(this.tbAtk);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.tbID);
            this.Controls.Add(this.pbPokemon);
            this.Controls.Add(this.cbPokemon);
            this.Name = "AdminPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin Panel";
            ((System.ComponentModel.ISupportInitialize)(this.pbPokemon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbPokemon;
        private System.Windows.Forms.PictureBox pbPokemon;
        private System.Windows.Forms.TextBox tbID;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbAtk;
        private System.Windows.Forms.TextBox tbDef;
        private System.Windows.Forms.TextBox tbSpatk;
        private System.Windows.Forms.TextBox tbSpdef;
        private System.Windows.Forms.TextBox tbSpeed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbHP;
        private System.Windows.Forms.TextBox tbGender;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbXP;
        private System.Windows.Forms.TextBox tbWeight;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbCapture;
        private System.Windows.Forms.ComboBox cbType1;
        private System.Windows.Forms.ComboBox cbType2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button buttonNewAttack;
        private System.Windows.Forms.Button buttonDeleteAttack;
        private System.Windows.Forms.Button buttonChangePokemon;
        private System.Windows.Forms.Button buttonAddPokemon;
    }
}