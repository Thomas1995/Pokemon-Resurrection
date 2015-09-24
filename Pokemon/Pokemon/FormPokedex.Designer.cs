namespace Pokemon
{
    partial class FormPokedex
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
            this.listPokemons = new System.Windows.Forms.ListBox();
            this.pbPokemon = new System.Windows.Forms.PictureBox();
            this.buttonExit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelSeen = new System.Windows.Forms.Label();
            this.labelOwned = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbPokemon)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listPokemons
            // 
            this.listPokemons.FormattingEnabled = true;
            this.listPokemons.Location = new System.Drawing.Point(12, 12);
            this.listPokemons.Name = "listPokemons";
            this.listPokemons.Size = new System.Drawing.Size(147, 212);
            this.listPokemons.TabIndex = 0;
            this.listPokemons.SelectedIndexChanged += new System.EventHandler(this.listPokemons_SelectedIndexChanged);
            // 
            // pbPokemon
            // 
            this.pbPokemon.BackColor = System.Drawing.Color.White;
            this.pbPokemon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbPokemon.Location = new System.Drawing.Point(184, 12);
            this.pbPokemon.Name = "pbPokemon";
            this.pbPokemon.Size = new System.Drawing.Size(80, 80);
            this.pbPokemon.TabIndex = 11;
            this.pbPokemon.TabStop = false;
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.Color.Red;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.ForeColor = System.Drawing.Color.White;
            this.buttonExit.Location = new System.Drawing.Point(271, -1);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(20, 20);
            this.buttonExit.TabIndex = 13;
            this.buttonExit.Text = "X";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.labelSeen);
            this.panel1.Controls.Add(this.labelOwned);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(184, 114);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(80, 109);
            this.panel1.TabIndex = 14;
            // 
            // labelSeen
            // 
            this.labelSeen.AutoSize = true;
            this.labelSeen.Location = new System.Drawing.Point(33, 69);
            this.labelSeen.Name = "labelSeen";
            this.labelSeen.Size = new System.Drawing.Size(14, 13);
            this.labelSeen.TabIndex = 3;
            this.labelSeen.Text = "0";
            // 
            // labelOwned
            // 
            this.labelOwned.AutoSize = true;
            this.labelOwned.Location = new System.Drawing.Point(32, 31);
            this.labelOwned.Name = "labelOwned";
            this.labelOwned.Size = new System.Drawing.Size(14, 13);
            this.labelOwned.TabIndex = 2;
            this.labelOwned.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Seen";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Owned";
            // 
            // FormPokedex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Pokemon.Properties.Resources.pokemonsbackground;
            this.ClientSize = new System.Drawing.Size(290, 235);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.pbPokemon);
            this.Controls.Add(this.listPokemons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormPokedex";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormPokedex";
            ((System.ComponentModel.ISupportInitialize)(this.pbPokemon)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listPokemons;
        private System.Windows.Forms.PictureBox pbPokemon;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelSeen;
        private System.Windows.Forms.Label labelOwned;
    }
}