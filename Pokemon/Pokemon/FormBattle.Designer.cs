namespace Pokemon
{
    partial class FormBattle
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
            this.battleScreen = new System.Windows.Forms.PictureBox();
            this.tbHistory = new System.Windows.Forms.RichTextBox();
            this.panelAttack = new System.Windows.Forms.Panel();
            this.attack4 = new System.Windows.Forms.Button();
            this.attack3 = new System.Windows.Forms.Button();
            this.attack2 = new System.Windows.Forms.Button();
            this.attack1 = new System.Windows.Forms.Button();
            this.pbPokemon1 = new System.Windows.Forms.PictureBox();
            this.pbPokemon2 = new System.Windows.Forms.PictureBox();
            this.pbPokemon3 = new System.Windows.Forms.PictureBox();
            this.pbPokemon6 = new System.Windows.Forms.PictureBox();
            this.pbPokemon5 = new System.Windows.Forms.PictureBox();
            this.pbPokemon4 = new System.Windows.Forms.PictureBox();
            this.pbHealthGreen1 = new System.Windows.Forms.PictureBox();
            this.pbHealthRed1 = new System.Windows.Forms.PictureBox();
            this.pbHealthRed2 = new System.Windows.Forms.PictureBox();
            this.pbHealthGreen2 = new System.Windows.Forms.PictureBox();
            this.pbHealthRed3 = new System.Windows.Forms.PictureBox();
            this.pbHealthGreen3 = new System.Windows.Forms.PictureBox();
            this.pbHealthRed4 = new System.Windows.Forms.PictureBox();
            this.pbHealthGreen4 = new System.Windows.Forms.PictureBox();
            this.pbHealthRed5 = new System.Windows.Forms.PictureBox();
            this.pbHealthGreen5 = new System.Windows.Forms.PictureBox();
            this.pbHealthRed6 = new System.Windows.Forms.PictureBox();
            this.pbHealthGreen6 = new System.Windows.Forms.PictureBox();
            this.buttonRun = new System.Windows.Forms.Button();
            this.buttonBag = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.battleScreen)).BeginInit();
            this.panelAttack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPokemon1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPokemon2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPokemon3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPokemon6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPokemon5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPokemon4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHealthGreen1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHealthRed1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHealthRed2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHealthGreen2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHealthRed3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHealthGreen3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHealthRed4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHealthGreen4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHealthRed5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHealthGreen5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHealthRed6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHealthGreen6)).BeginInit();
            this.SuspendLayout();
            // 
            // battleScreen
            // 
            this.battleScreen.Location = new System.Drawing.Point(0, 0);
            this.battleScreen.Name = "battleScreen";
            this.battleScreen.Size = new System.Drawing.Size(600, 280);
            this.battleScreen.TabIndex = 0;
            this.battleScreen.TabStop = false;
            this.battleScreen.Paint += new System.Windows.Forms.PaintEventHandler(this.battleScreen_Paint);
            // 
            // tbHistory
            // 
            this.tbHistory.BackColor = System.Drawing.Color.Black;
            this.tbHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbHistory.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tbHistory.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbHistory.ForeColor = System.Drawing.Color.White;
            this.tbHistory.Location = new System.Drawing.Point(0, 280);
            this.tbHistory.Name = "tbHistory";
            this.tbHistory.ReadOnly = true;
            this.tbHistory.Size = new System.Drawing.Size(415, 140);
            this.tbHistory.TabIndex = 1;
            this.tbHistory.Text = "";
            // 
            // panelAttack
            // 
            this.panelAttack.BackColor = System.Drawing.Color.BurlyWood;
            this.panelAttack.Controls.Add(this.attack4);
            this.panelAttack.Controls.Add(this.attack3);
            this.panelAttack.Controls.Add(this.attack2);
            this.panelAttack.Controls.Add(this.attack1);
            this.panelAttack.Location = new System.Drawing.Point(415, 280);
            this.panelAttack.Name = "panelAttack";
            this.panelAttack.Size = new System.Drawing.Size(185, 140);
            this.panelAttack.TabIndex = 2;
            this.panelAttack.Paint += new System.Windows.Forms.PaintEventHandler(this.panelAttack_Paint);
            // 
            // attack4
            // 
            this.attack4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.attack4.Location = new System.Drawing.Point(96, 74);
            this.attack4.Name = "attack4";
            this.attack4.Size = new System.Drawing.Size(80, 50);
            this.attack4.TabIndex = 3;
            this.attack4.Text = "Attack4";
            this.attack4.UseVisualStyleBackColor = true;
            this.attack4.Click += new System.EventHandler(this.attack_Click);
            // 
            // attack3
            // 
            this.attack3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.attack3.Location = new System.Drawing.Point(8, 74);
            this.attack3.Name = "attack3";
            this.attack3.Size = new System.Drawing.Size(80, 50);
            this.attack3.TabIndex = 2;
            this.attack3.Text = "Attack3";
            this.attack3.UseVisualStyleBackColor = true;
            this.attack3.Click += new System.EventHandler(this.attack_Click);
            // 
            // attack2
            // 
            this.attack2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.attack2.Location = new System.Drawing.Point(96, 13);
            this.attack2.Name = "attack2";
            this.attack2.Size = new System.Drawing.Size(80, 50);
            this.attack2.TabIndex = 1;
            this.attack2.Text = "Attack2";
            this.attack2.UseVisualStyleBackColor = true;
            this.attack2.Click += new System.EventHandler(this.attack_Click);
            // 
            // attack1
            // 
            this.attack1.BackColor = System.Drawing.Color.BurlyWood;
            this.attack1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.attack1.Location = new System.Drawing.Point(8, 13);
            this.attack1.Name = "attack1";
            this.attack1.Size = new System.Drawing.Size(80, 50);
            this.attack1.TabIndex = 0;
            this.attack1.Text = "Attack1";
            this.attack1.UseVisualStyleBackColor = false;
            this.attack1.Click += new System.EventHandler(this.attack_Click);
            // 
            // pbPokemon1
            // 
            this.pbPokemon1.Location = new System.Drawing.Point(12, 428);
            this.pbPokemon1.Name = "pbPokemon1";
            this.pbPokemon1.Size = new System.Drawing.Size(80, 80);
            this.pbPokemon1.TabIndex = 3;
            this.pbPokemon1.TabStop = false;
            this.pbPokemon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbPokemon_MouseClick);
            // 
            // pbPokemon2
            // 
            this.pbPokemon2.Location = new System.Drawing.Point(110, 428);
            this.pbPokemon2.Name = "pbPokemon2";
            this.pbPokemon2.Size = new System.Drawing.Size(80, 80);
            this.pbPokemon2.TabIndex = 4;
            this.pbPokemon2.TabStop = false;
            this.pbPokemon2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbPokemon_MouseClick);
            // 
            // pbPokemon3
            // 
            this.pbPokemon3.Location = new System.Drawing.Point(208, 428);
            this.pbPokemon3.Name = "pbPokemon3";
            this.pbPokemon3.Size = new System.Drawing.Size(80, 80);
            this.pbPokemon3.TabIndex = 5;
            this.pbPokemon3.TabStop = false;
            this.pbPokemon3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbPokemon_MouseClick);
            // 
            // pbPokemon6
            // 
            this.pbPokemon6.Location = new System.Drawing.Point(508, 428);
            this.pbPokemon6.Name = "pbPokemon6";
            this.pbPokemon6.Size = new System.Drawing.Size(80, 80);
            this.pbPokemon6.TabIndex = 8;
            this.pbPokemon6.TabStop = false;
            this.pbPokemon6.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbPokemon_MouseClick);
            // 
            // pbPokemon5
            // 
            this.pbPokemon5.Location = new System.Drawing.Point(406, 428);
            this.pbPokemon5.Name = "pbPokemon5";
            this.pbPokemon5.Size = new System.Drawing.Size(80, 80);
            this.pbPokemon5.TabIndex = 7;
            this.pbPokemon5.TabStop = false;
            this.pbPokemon5.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbPokemon_MouseClick);
            // 
            // pbPokemon4
            // 
            this.pbPokemon4.Location = new System.Drawing.Point(307, 428);
            this.pbPokemon4.Name = "pbPokemon4";
            this.pbPokemon4.Size = new System.Drawing.Size(80, 80);
            this.pbPokemon4.TabIndex = 6;
            this.pbPokemon4.TabStop = false;
            this.pbPokemon4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbPokemon_MouseClick);
            // 
            // pbHealthGreen1
            // 
            this.pbHealthGreen1.BackColor = System.Drawing.Color.Lime;
            this.pbHealthGreen1.Location = new System.Drawing.Point(12, 514);
            this.pbHealthGreen1.Name = "pbHealthGreen1";
            this.pbHealthGreen1.Size = new System.Drawing.Size(40, 8);
            this.pbHealthGreen1.TabIndex = 9;
            this.pbHealthGreen1.TabStop = false;
            // 
            // pbHealthRed1
            // 
            this.pbHealthRed1.BackColor = System.Drawing.Color.Red;
            this.pbHealthRed1.Location = new System.Drawing.Point(12, 514);
            this.pbHealthRed1.Name = "pbHealthRed1";
            this.pbHealthRed1.Size = new System.Drawing.Size(80, 8);
            this.pbHealthRed1.TabIndex = 10;
            this.pbHealthRed1.TabStop = false;
            // 
            // pbHealthRed2
            // 
            this.pbHealthRed2.BackColor = System.Drawing.Color.Red;
            this.pbHealthRed2.Location = new System.Drawing.Point(110, 514);
            this.pbHealthRed2.Name = "pbHealthRed2";
            this.pbHealthRed2.Size = new System.Drawing.Size(80, 8);
            this.pbHealthRed2.TabIndex = 12;
            this.pbHealthRed2.TabStop = false;
            // 
            // pbHealthGreen2
            // 
            this.pbHealthGreen2.BackColor = System.Drawing.Color.Lime;
            this.pbHealthGreen2.Location = new System.Drawing.Point(110, 514);
            this.pbHealthGreen2.Name = "pbHealthGreen2";
            this.pbHealthGreen2.Size = new System.Drawing.Size(40, 8);
            this.pbHealthGreen2.TabIndex = 11;
            this.pbHealthGreen2.TabStop = false;
            // 
            // pbHealthRed3
            // 
            this.pbHealthRed3.BackColor = System.Drawing.Color.Red;
            this.pbHealthRed3.Location = new System.Drawing.Point(208, 514);
            this.pbHealthRed3.Name = "pbHealthRed3";
            this.pbHealthRed3.Size = new System.Drawing.Size(80, 8);
            this.pbHealthRed3.TabIndex = 14;
            this.pbHealthRed3.TabStop = false;
            // 
            // pbHealthGreen3
            // 
            this.pbHealthGreen3.BackColor = System.Drawing.Color.Lime;
            this.pbHealthGreen3.Location = new System.Drawing.Point(208, 514);
            this.pbHealthGreen3.Name = "pbHealthGreen3";
            this.pbHealthGreen3.Size = new System.Drawing.Size(40, 8);
            this.pbHealthGreen3.TabIndex = 13;
            this.pbHealthGreen3.TabStop = false;
            // 
            // pbHealthRed4
            // 
            this.pbHealthRed4.BackColor = System.Drawing.Color.Red;
            this.pbHealthRed4.Location = new System.Drawing.Point(307, 514);
            this.pbHealthRed4.Name = "pbHealthRed4";
            this.pbHealthRed4.Size = new System.Drawing.Size(80, 8);
            this.pbHealthRed4.TabIndex = 16;
            this.pbHealthRed4.TabStop = false;
            // 
            // pbHealthGreen4
            // 
            this.pbHealthGreen4.BackColor = System.Drawing.Color.Lime;
            this.pbHealthGreen4.Location = new System.Drawing.Point(307, 514);
            this.pbHealthGreen4.Name = "pbHealthGreen4";
            this.pbHealthGreen4.Size = new System.Drawing.Size(40, 8);
            this.pbHealthGreen4.TabIndex = 15;
            this.pbHealthGreen4.TabStop = false;
            // 
            // pbHealthRed5
            // 
            this.pbHealthRed5.BackColor = System.Drawing.Color.Red;
            this.pbHealthRed5.Location = new System.Drawing.Point(406, 514);
            this.pbHealthRed5.Name = "pbHealthRed5";
            this.pbHealthRed5.Size = new System.Drawing.Size(80, 8);
            this.pbHealthRed5.TabIndex = 18;
            this.pbHealthRed5.TabStop = false;
            // 
            // pbHealthGreen5
            // 
            this.pbHealthGreen5.BackColor = System.Drawing.Color.Lime;
            this.pbHealthGreen5.Location = new System.Drawing.Point(406, 514);
            this.pbHealthGreen5.Name = "pbHealthGreen5";
            this.pbHealthGreen5.Size = new System.Drawing.Size(40, 8);
            this.pbHealthGreen5.TabIndex = 17;
            this.pbHealthGreen5.TabStop = false;
            // 
            // pbHealthRed6
            // 
            this.pbHealthRed6.BackColor = System.Drawing.Color.Red;
            this.pbHealthRed6.Location = new System.Drawing.Point(508, 514);
            this.pbHealthRed6.Name = "pbHealthRed6";
            this.pbHealthRed6.Size = new System.Drawing.Size(80, 8);
            this.pbHealthRed6.TabIndex = 20;
            this.pbHealthRed6.TabStop = false;
            // 
            // pbHealthGreen6
            // 
            this.pbHealthGreen6.BackColor = System.Drawing.Color.Lime;
            this.pbHealthGreen6.Location = new System.Drawing.Point(508, 514);
            this.pbHealthGreen6.Name = "pbHealthGreen6";
            this.pbHealthGreen6.Size = new System.Drawing.Size(40, 8);
            this.pbHealthGreen6.TabIndex = 19;
            this.pbHealthGreen6.TabStop = false;
            // 
            // buttonRun
            // 
            this.buttonRun.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonRun.Location = new System.Drawing.Point(12, 12);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(80, 25);
            this.buttonRun.TabIndex = 4;
            this.buttonRun.Text = "Run";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // buttonBag
            // 
            this.buttonBag.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonBag.Location = new System.Drawing.Point(98, 12);
            this.buttonBag.Name = "buttonBag";
            this.buttonBag.Size = new System.Drawing.Size(80, 25);
            this.buttonBag.TabIndex = 21;
            this.buttonBag.Text = "Bag";
            this.buttonBag.UseVisualStyleBackColor = false;
            this.buttonBag.Click += new System.EventHandler(this.buttonBag_Click);
            // 
            // FormBattle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.BurlyWood;
            this.ClientSize = new System.Drawing.Size(600, 528);
            this.Controls.Add(this.buttonBag);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.pbHealthGreen6);
            this.Controls.Add(this.pbHealthGreen5);
            this.Controls.Add(this.pbHealthGreen4);
            this.Controls.Add(this.pbHealthGreen3);
            this.Controls.Add(this.pbHealthGreen2);
            this.Controls.Add(this.pbHealthGreen1);
            this.Controls.Add(this.pbHealthRed6);
            this.Controls.Add(this.pbHealthRed5);
            this.Controls.Add(this.pbHealthRed4);
            this.Controls.Add(this.pbHealthRed3);
            this.Controls.Add(this.pbHealthRed2);
            this.Controls.Add(this.pbHealthRed1);
            this.Controls.Add(this.pbPokemon6);
            this.Controls.Add(this.pbPokemon5);
            this.Controls.Add(this.pbPokemon4);
            this.Controls.Add(this.pbPokemon3);
            this.Controls.Add(this.pbPokemon2);
            this.Controls.Add(this.pbPokemon1);
            this.Controls.Add(this.panelAttack);
            this.Controls.Add(this.tbHistory);
            this.Controls.Add(this.battleScreen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormBattle";
            this.Opacity = 0D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormBattle";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormBattle_FormClosed);
            this.Load += new System.EventHandler(this.FormBattle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.battleScreen)).EndInit();
            this.panelAttack.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPokemon1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPokemon2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPokemon3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPokemon6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPokemon5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPokemon4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHealthGreen1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHealthRed1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHealthRed2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHealthGreen2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHealthRed3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHealthGreen3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHealthRed4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHealthGreen4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHealthRed5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHealthGreen5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHealthRed6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHealthGreen6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox battleScreen;
        private System.Windows.Forms.RichTextBox tbHistory;
        private System.Windows.Forms.Panel panelAttack;
        private System.Windows.Forms.Button attack4;
        private System.Windows.Forms.Button attack3;
        private System.Windows.Forms.Button attack2;
        private System.Windows.Forms.Button attack1;
        private System.Windows.Forms.PictureBox pbPokemon1;
        private System.Windows.Forms.PictureBox pbPokemon2;
        private System.Windows.Forms.PictureBox pbPokemon3;
        private System.Windows.Forms.PictureBox pbPokemon6;
        private System.Windows.Forms.PictureBox pbPokemon5;
        private System.Windows.Forms.PictureBox pbPokemon4;
        private System.Windows.Forms.PictureBox pbHealthGreen1;
        private System.Windows.Forms.PictureBox pbHealthRed1;
        private System.Windows.Forms.PictureBox pbHealthRed2;
        private System.Windows.Forms.PictureBox pbHealthGreen2;
        private System.Windows.Forms.PictureBox pbHealthRed3;
        private System.Windows.Forms.PictureBox pbHealthGreen3;
        private System.Windows.Forms.PictureBox pbHealthRed4;
        private System.Windows.Forms.PictureBox pbHealthGreen4;
        private System.Windows.Forms.PictureBox pbHealthRed5;
        private System.Windows.Forms.PictureBox pbHealthGreen5;
        private System.Windows.Forms.PictureBox pbHealthRed6;
        private System.Windows.Forms.PictureBox pbHealthGreen6;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Button buttonBag;
    }
}