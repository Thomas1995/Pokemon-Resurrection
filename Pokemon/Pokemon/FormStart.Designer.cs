namespace Pokemon
{
    partial class FormStart
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStart));
            this.panelMenu = new System.Windows.Forms.Panel();
            this.panelMultiPlayer = new System.Windows.Forms.Panel();
            this.panelSinglePlayer = new System.Windows.Forms.Panel();
            this.buttonLoadGame = new System.Windows.Forms.Button();
            this.buttonNewGame = new System.Windows.Forms.Button();
            this.buttonMultiPlayer = new System.Windows.Forms.Button();
            this.buttonSinglePlayer = new System.Windows.Forms.Button();
            this.pbMenu = new System.Windows.Forms.PictureBox();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.buttonFocus = new System.Windows.Forms.Button();
            this.timerButton = new System.Windows.Forms.Timer(this.components);
            this.timerBackgroundPicture = new System.Windows.Forms.Timer(this.components);
            this.pbBackground = new System.Windows.Forms.PictureBox();
            this.buttonNotYet = new System.Windows.Forms.Button();
            this.panelMenu.SuspendLayout();
            this.panelMultiPlayer.SuspendLayout();
            this.panelSinglePlayer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBackground)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.Controls.Add(this.panelMultiPlayer);
            this.panelMenu.Controls.Add(this.panelSinglePlayer);
            this.panelMenu.Controls.Add(this.buttonMultiPlayer);
            this.panelMenu.Controls.Add(this.buttonSinglePlayer);
            this.panelMenu.Controls.Add(this.pbMenu);
            this.panelMenu.Location = new System.Drawing.Point(422, 173);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(350, 400);
            this.panelMenu.TabIndex = 1;
            // 
            // panelMultiPlayer
            // 
            this.panelMultiPlayer.BackColor = System.Drawing.Color.Linen;
            this.panelMultiPlayer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMultiPlayer.Controls.Add(this.buttonNotYet);
            this.panelMultiPlayer.Location = new System.Drawing.Point(70, 310);
            this.panelMultiPlayer.Name = "panelMultiPlayer";
            this.panelMultiPlayer.Size = new System.Drawing.Size(210, 120);
            this.panelMultiPlayer.TabIndex = 5;
            // 
            // panelSinglePlayer
            // 
            this.panelSinglePlayer.BackColor = System.Drawing.Color.Linen;
            this.panelSinglePlayer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSinglePlayer.Controls.Add(this.buttonLoadGame);
            this.panelSinglePlayer.Controls.Add(this.buttonNewGame);
            this.panelSinglePlayer.Location = new System.Drawing.Point(70, 140);
            this.panelSinglePlayer.Name = "panelSinglePlayer";
            this.panelSinglePlayer.Size = new System.Drawing.Size(210, 120);
            this.panelSinglePlayer.TabIndex = 4;
            // 
            // buttonLoadGame
            // 
            this.buttonLoadGame.BackColor = System.Drawing.Color.SandyBrown;
            this.buttonLoadGame.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonLoadGame.Font = new System.Drawing.Font("Palatino Linotype", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLoadGame.ForeColor = System.Drawing.Color.White;
            this.buttonLoadGame.Location = new System.Drawing.Point(24, 61);
            this.buttonLoadGame.Name = "buttonLoadGame";
            this.buttonLoadGame.Size = new System.Drawing.Size(160, 30);
            this.buttonLoadGame.TabIndex = 7;
            this.buttonLoadGame.Text = "Load Game";
            this.buttonLoadGame.UseVisualStyleBackColor = false;
            this.buttonLoadGame.Click += new System.EventHandler(this.buttonLoadGame_Click);
            // 
            // buttonNewGame
            // 
            this.buttonNewGame.BackColor = System.Drawing.Color.SandyBrown;
            this.buttonNewGame.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonNewGame.Font = new System.Drawing.Font("Palatino Linotype", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNewGame.ForeColor = System.Drawing.Color.White;
            this.buttonNewGame.Location = new System.Drawing.Point(25, 20);
            this.buttonNewGame.Name = "buttonNewGame";
            this.buttonNewGame.Size = new System.Drawing.Size(160, 30);
            this.buttonNewGame.TabIndex = 6;
            this.buttonNewGame.Text = "New Game";
            this.buttonNewGame.UseVisualStyleBackColor = false;
            this.buttonNewGame.Click += new System.EventHandler(this.buttonNewGame_Click);
            // 
            // buttonMultiPlayer
            // 
            this.buttonMultiPlayer.BackColor = System.Drawing.Color.SandyBrown;
            this.buttonMultiPlayer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonMultiPlayer.Font = new System.Drawing.Font("Palatino Linotype", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMultiPlayer.ForeColor = System.Drawing.Color.White;
            this.buttonMultiPlayer.Location = new System.Drawing.Point(70, 240);
            this.buttonMultiPlayer.Name = "buttonMultiPlayer";
            this.buttonMultiPlayer.Size = new System.Drawing.Size(210, 70);
            this.buttonMultiPlayer.TabIndex = 3;
            this.buttonMultiPlayer.Text = "Challenge Friends";
            this.buttonMultiPlayer.UseVisualStyleBackColor = false;
            this.buttonMultiPlayer.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // buttonSinglePlayer
            // 
            this.buttonSinglePlayer.BackColor = System.Drawing.Color.SandyBrown;
            this.buttonSinglePlayer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonSinglePlayer.Font = new System.Drawing.Font("Palatino Linotype", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSinglePlayer.ForeColor = System.Drawing.Color.White;
            this.buttonSinglePlayer.Location = new System.Drawing.Point(70, 70);
            this.buttonSinglePlayer.Name = "buttonSinglePlayer";
            this.buttonSinglePlayer.Size = new System.Drawing.Size(210, 70);
            this.buttonSinglePlayer.TabIndex = 2;
            this.buttonSinglePlayer.Text = "Single Player";
            this.buttonSinglePlayer.UseVisualStyleBackColor = false;
            this.buttonSinglePlayer.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // pbMenu
            // 
            this.pbMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbMenu.Location = new System.Drawing.Point(0, 0);
            this.pbMenu.Name = "pbMenu";
            this.pbMenu.Size = new System.Drawing.Size(350, 400);
            this.pbMenu.TabIndex = 0;
            this.pbMenu.TabStop = false;
            this.pbMenu.Paint += new System.Windows.Forms.PaintEventHandler(this.pbMenu_Paint);
            // 
            // labelCopyright
            // 
            this.labelCopyright.AutoSize = true;
            this.labelCopyright.BackColor = System.Drawing.Color.Transparent;
            this.labelCopyright.Font = new System.Drawing.Font("Perpetua", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCopyright.ForeColor = System.Drawing.Color.Chocolate;
            this.labelCopyright.Location = new System.Drawing.Point(9, 544);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(42, 13);
            this.labelCopyright.TabIndex = 2;
            this.labelCopyright.Text = "Copyright";
            // 
            // buttonFocus
            // 
            this.buttonFocus.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonFocus.Location = new System.Drawing.Point(0, 0);
            this.buttonFocus.Name = "buttonFocus";
            this.buttonFocus.Size = new System.Drawing.Size(0, 0);
            this.buttonFocus.TabIndex = 1;
            this.buttonFocus.UseVisualStyleBackColor = true;
            // 
            // timerButton
            // 
            this.timerButton.Tick += new System.EventHandler(this.timerButton_Tick);
            // 
            // timerBackgroundPicture
            // 
            this.timerBackgroundPicture.Tick += new System.EventHandler(this.timerBackgroundPicture_Tick);
            // 
            // pbBackground
            // 
            this.pbBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbBackground.Location = new System.Drawing.Point(0, 0);
            this.pbBackground.Name = "pbBackground";
            this.pbBackground.Size = new System.Drawing.Size(800, 600);
            this.pbBackground.TabIndex = 0;
            this.pbBackground.TabStop = false;
            this.pbBackground.Paint += new System.Windows.Forms.PaintEventHandler(this.pbBackground_Paint);
            // 
            // buttonNotYet
            // 
            this.buttonNotYet.BackColor = System.Drawing.Color.SandyBrown;
            this.buttonNotYet.Enabled = false;
            this.buttonNotYet.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonNotYet.Font = new System.Drawing.Font("Palatino Linotype", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNotYet.ForeColor = System.Drawing.Color.White;
            this.buttonNotYet.Location = new System.Drawing.Point(24, 44);
            this.buttonNotYet.Name = "buttonNotYet";
            this.buttonNotYet.Size = new System.Drawing.Size(160, 30);
            this.buttonNotYet.TabIndex = 7;
            this.buttonNotYet.Text = "Not Yet";
            this.buttonNotYet.UseVisualStyleBackColor = false;
            // 
            // FormStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.buttonFocus);
            this.Controls.Add(this.labelCopyright);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.pbBackground);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormStart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pokémon Resurrection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormStart_FormClosing);
            this.panelMenu.ResumeLayout(false);
            this.panelMultiPlayer.ResumeLayout(false);
            this.panelSinglePlayer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBackground)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbBackground;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.PictureBox pbMenu;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Button buttonSinglePlayer;
        private System.Windows.Forms.Button buttonMultiPlayer;
        private System.Windows.Forms.Button buttonFocus;
        private System.Windows.Forms.Timer timerButton;
        private System.Windows.Forms.Panel panelSinglePlayer;
        private System.Windows.Forms.Panel panelMultiPlayer;
        private System.Windows.Forms.Button buttonNewGame;
        private System.Windows.Forms.Button buttonLoadGame;
        private System.Windows.Forms.Timer timerBackgroundPicture;
        private System.Windows.Forms.Button buttonNotYet;
    }
}