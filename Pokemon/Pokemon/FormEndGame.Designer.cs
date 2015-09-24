namespace Pokemon
{
    partial class FormEndGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEndGame));
            this.pbThomas = new System.Windows.Forms.PictureBox();
            this.tbMessage = new System.Windows.Forms.RichTextBox();
            this.pbPokemon6 = new System.Windows.Forms.PictureBox();
            this.pbPokemon5 = new System.Windows.Forms.PictureBox();
            this.pbPokemon4 = new System.Windows.Forms.PictureBox();
            this.pbPokemon3 = new System.Windows.Forms.PictureBox();
            this.pbPokemon2 = new System.Windows.Forms.PictureBox();
            this.pbPokemon1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbThomas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPokemon6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPokemon5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPokemon4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPokemon3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPokemon2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPokemon1)).BeginInit();
            this.SuspendLayout();
            // 
            // pbThomas
            // 
            this.pbThomas.Image = global::Pokemon.Properties.Resources.thomas_sprite;
            this.pbThomas.Location = new System.Drawing.Point(12, 12);
            this.pbThomas.Name = "pbThomas";
            this.pbThomas.Size = new System.Drawing.Size(48, 62);
            this.pbThomas.TabIndex = 0;
            this.pbThomas.TabStop = false;
            // 
            // tbMessage
            // 
            this.tbMessage.BackColor = System.Drawing.Color.White;
            this.tbMessage.Location = new System.Drawing.Point(70, 12);
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.ReadOnly = true;
            this.tbMessage.Size = new System.Drawing.Size(584, 130);
            this.tbMessage.TabIndex = 1;
            this.tbMessage.Text = "";
            // 
            // pbPokemon6
            // 
            this.pbPokemon6.BackColor = System.Drawing.Color.White;
            this.pbPokemon6.Location = new System.Drawing.Point(545, 324);
            this.pbPokemon6.Name = "pbPokemon6";
            this.pbPokemon6.Size = new System.Drawing.Size(80, 80);
            this.pbPokemon6.TabIndex = 14;
            this.pbPokemon6.TabStop = false;
            // 
            // pbPokemon5
            // 
            this.pbPokemon5.BackColor = System.Drawing.Color.White;
            this.pbPokemon5.Location = new System.Drawing.Point(443, 324);
            this.pbPokemon5.Name = "pbPokemon5";
            this.pbPokemon5.Size = new System.Drawing.Size(80, 80);
            this.pbPokemon5.TabIndex = 13;
            this.pbPokemon5.TabStop = false;
            // 
            // pbPokemon4
            // 
            this.pbPokemon4.BackColor = System.Drawing.Color.White;
            this.pbPokemon4.Location = new System.Drawing.Point(344, 324);
            this.pbPokemon4.Name = "pbPokemon4";
            this.pbPokemon4.Size = new System.Drawing.Size(80, 80);
            this.pbPokemon4.TabIndex = 12;
            this.pbPokemon4.TabStop = false;
            // 
            // pbPokemon3
            // 
            this.pbPokemon3.BackColor = System.Drawing.Color.White;
            this.pbPokemon3.Location = new System.Drawing.Point(245, 324);
            this.pbPokemon3.Name = "pbPokemon3";
            this.pbPokemon3.Size = new System.Drawing.Size(80, 80);
            this.pbPokemon3.TabIndex = 11;
            this.pbPokemon3.TabStop = false;
            // 
            // pbPokemon2
            // 
            this.pbPokemon2.BackColor = System.Drawing.Color.White;
            this.pbPokemon2.Location = new System.Drawing.Point(147, 324);
            this.pbPokemon2.Name = "pbPokemon2";
            this.pbPokemon2.Size = new System.Drawing.Size(80, 80);
            this.pbPokemon2.TabIndex = 10;
            this.pbPokemon2.TabStop = false;
            // 
            // pbPokemon1
            // 
            this.pbPokemon1.BackColor = System.Drawing.Color.White;
            this.pbPokemon1.Location = new System.Drawing.Point(49, 324);
            this.pbPokemon1.Name = "pbPokemon1";
            this.pbPokemon1.Size = new System.Drawing.Size(80, 80);
            this.pbPokemon1.TabIndex = 9;
            this.pbPokemon1.TabStop = false;
            // 
            // FormEndGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::Pokemon.Properties.Resources.BackgroundEndgame;
            this.ClientSize = new System.Drawing.Size(666, 416);
            this.Controls.Add(this.pbPokemon6);
            this.Controls.Add(this.pbPokemon5);
            this.Controls.Add(this.pbPokemon4);
            this.Controls.Add(this.pbPokemon3);
            this.Controls.Add(this.pbPokemon2);
            this.Controls.Add(this.pbPokemon1);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.pbThomas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormEndGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pokémon Resurrection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEndGame_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pbThomas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPokemon6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPokemon5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPokemon4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPokemon3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPokemon2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPokemon1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbThomas;
        private System.Windows.Forms.RichTextBox tbMessage;
        private System.Windows.Forms.PictureBox pbPokemon6;
        private System.Windows.Forms.PictureBox pbPokemon5;
        private System.Windows.Forms.PictureBox pbPokemon4;
        private System.Windows.Forms.PictureBox pbPokemon3;
        private System.Windows.Forms.PictureBox pbPokemon2;
        private System.Windows.Forms.PictureBox pbPokemon1;
    }
}