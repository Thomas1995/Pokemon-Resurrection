namespace Pokemon
{
    partial class FormGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGame));
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.panelMenu = new System.Windows.Forms.Panel();
            this.tbDialog = new System.Windows.Forms.RichTextBox();
            this.panelDialog = new System.Windows.Forms.Panel();
            this.pbMenuMaximize = new System.Windows.Forms.PictureBox();
            this.pbMenuHelp = new System.Windows.Forms.PictureBox();
            this.pbMenuDig = new System.Windows.Forms.PictureBox();
            this.pbMenuSurf = new System.Windows.Forms.PictureBox();
            this.pbMenuCut = new System.Windows.Forms.PictureBox();
            this.pbMenuSave = new System.Windows.Forms.PictureBox();
            this.pbMenuPokedex = new System.Windows.Forms.PictureBox();
            this.pbMenuPokemon = new System.Windows.Forms.PictureBox();
            this.pbMenuBag = new System.Windows.Forms.PictureBox();
            this.pbCanvas = new System.Windows.Forms.PictureBox();
            this.panelMenu.SuspendLayout();
            this.panelDialog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMenuMaximize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMenuHelp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMenuDig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMenuSurf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMenuCut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMenuSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMenuPokedex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMenuPokemon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMenuBag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // timerRefresh
            // 
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.Goldenrod;
            this.panelMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMenu.Controls.Add(this.pbMenuHelp);
            this.panelMenu.Controls.Add(this.pbMenuDig);
            this.panelMenu.Controls.Add(this.pbMenuSurf);
            this.panelMenu.Controls.Add(this.pbMenuCut);
            this.panelMenu.Controls.Add(this.pbMenuSave);
            this.panelMenu.Controls.Add(this.pbMenuPokedex);
            this.panelMenu.Controls.Add(this.pbMenuPokemon);
            this.panelMenu.Controls.Add(this.pbMenuBag);
            this.panelMenu.Location = new System.Drawing.Point(274, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(258, 31);
            this.panelMenu.TabIndex = 1;
            this.panelMenu.MouseLeave += new System.EventHandler(this.panelMenu_MouseLeave);
            // 
            // tbDialog
            // 
            this.tbDialog.BackColor = System.Drawing.Color.White;
            this.tbDialog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbDialog.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDialog.Location = new System.Drawing.Point(11, 10);
            this.tbDialog.Name = "tbDialog";
            this.tbDialog.ReadOnly = true;
            this.tbDialog.Size = new System.Drawing.Size(505, 82);
            this.tbDialog.TabIndex = 3;
            this.tbDialog.Text = "";
            this.tbDialog.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbDialog_KeyPress);
            // 
            // panelDialog
            // 
            this.panelDialog.BackColor = System.Drawing.Color.White;
            this.panelDialog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDialog.Controls.Add(this.tbDialog);
            this.panelDialog.Location = new System.Drawing.Point(148, 467);
            this.panelDialog.Name = "panelDialog";
            this.panelDialog.Size = new System.Drawing.Size(530, 106);
            this.panelDialog.TabIndex = 4;
            // 
            // pbMenuMaximize
            // 
            this.pbMenuMaximize.BackColor = System.Drawing.Color.Transparent;
            this.pbMenuMaximize.Image = global::Pokemon.Properties.Resources.menuarrowdown;
            this.pbMenuMaximize.Location = new System.Drawing.Point(392, 0);
            this.pbMenuMaximize.Name = "pbMenuMaximize";
            this.pbMenuMaximize.Size = new System.Drawing.Size(25, 25);
            this.pbMenuMaximize.TabIndex = 2;
            this.pbMenuMaximize.TabStop = false;
            this.pbMenuMaximize.MouseHover += new System.EventHandler(this.pbMenuMaximize_MouseHover);
            // 
            // pbMenuHelp
            // 
            this.pbMenuHelp.BackgroundImage = global::Pokemon.Properties.Resources.menuhelp;
            this.pbMenuHelp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbMenuHelp.Location = new System.Drawing.Point(224, 2);
            this.pbMenuHelp.Name = "pbMenuHelp";
            this.pbMenuHelp.Size = new System.Drawing.Size(25, 25);
            this.pbMenuHelp.TabIndex = 7;
            this.pbMenuHelp.TabStop = false;
            this.pbMenuHelp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbMenuHelp_MouseClick);
            this.pbMenuHelp.MouseEnter += new System.EventHandler(this.pbMenu_MouseEnter);
            this.pbMenuHelp.MouseLeave += new System.EventHandler(this.pbMenu_MouseLeave);
            // 
            // pbMenuDig
            // 
            this.pbMenuDig.BackgroundImage = global::Pokemon.Properties.Resources.menudig;
            this.pbMenuDig.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbMenuDig.Location = new System.Drawing.Point(194, 2);
            this.pbMenuDig.Name = "pbMenuDig";
            this.pbMenuDig.Size = new System.Drawing.Size(25, 25);
            this.pbMenuDig.TabIndex = 6;
            this.pbMenuDig.TabStop = false;
            this.pbMenuDig.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbMenuDig_MouseClick);
            this.pbMenuDig.MouseEnter += new System.EventHandler(this.pbMenu_MouseEnter);
            this.pbMenuDig.MouseLeave += new System.EventHandler(this.pbMenu_MouseLeave);
            // 
            // pbMenuSurf
            // 
            this.pbMenuSurf.BackgroundImage = global::Pokemon.Properties.Resources.menusurf;
            this.pbMenuSurf.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbMenuSurf.Location = new System.Drawing.Point(163, 2);
            this.pbMenuSurf.Name = "pbMenuSurf";
            this.pbMenuSurf.Size = new System.Drawing.Size(25, 25);
            this.pbMenuSurf.TabIndex = 5;
            this.pbMenuSurf.TabStop = false;
            this.pbMenuSurf.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbMenuSurf_MouseClick);
            this.pbMenuSurf.MouseEnter += new System.EventHandler(this.pbMenu_MouseEnter);
            this.pbMenuSurf.MouseLeave += new System.EventHandler(this.pbMenu_MouseLeave);
            // 
            // pbMenuCut
            // 
            this.pbMenuCut.BackgroundImage = global::Pokemon.Properties.Resources.menucut;
            this.pbMenuCut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbMenuCut.Location = new System.Drawing.Point(132, 2);
            this.pbMenuCut.Name = "pbMenuCut";
            this.pbMenuCut.Size = new System.Drawing.Size(25, 25);
            this.pbMenuCut.TabIndex = 4;
            this.pbMenuCut.TabStop = false;
            this.pbMenuCut.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbMenuCut_MouseClick);
            this.pbMenuCut.MouseEnter += new System.EventHandler(this.pbMenu_MouseEnter);
            this.pbMenuCut.MouseLeave += new System.EventHandler(this.pbMenu_MouseLeave);
            // 
            // pbMenuSave
            // 
            this.pbMenuSave.BackgroundImage = global::Pokemon.Properties.Resources.menusave;
            this.pbMenuSave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbMenuSave.Location = new System.Drawing.Point(101, 2);
            this.pbMenuSave.Name = "pbMenuSave";
            this.pbMenuSave.Size = new System.Drawing.Size(25, 25);
            this.pbMenuSave.TabIndex = 3;
            this.pbMenuSave.TabStop = false;
            this.pbMenuSave.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbMenuSave_MouseClick);
            this.pbMenuSave.MouseEnter += new System.EventHandler(this.pbMenu_MouseEnter);
            this.pbMenuSave.MouseLeave += new System.EventHandler(this.pbMenu_MouseLeave);
            // 
            // pbMenuPokedex
            // 
            this.pbMenuPokedex.BackgroundImage = global::Pokemon.Properties.Resources.menupokedex;
            this.pbMenuPokedex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbMenuPokedex.Location = new System.Drawing.Point(69, 2);
            this.pbMenuPokedex.Name = "pbMenuPokedex";
            this.pbMenuPokedex.Size = new System.Drawing.Size(25, 25);
            this.pbMenuPokedex.TabIndex = 2;
            this.pbMenuPokedex.TabStop = false;
            this.pbMenuPokedex.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbMenuPokedex_MouseClick);
            this.pbMenuPokedex.MouseEnter += new System.EventHandler(this.pbMenu_MouseEnter);
            this.pbMenuPokedex.MouseLeave += new System.EventHandler(this.pbMenu_MouseLeave);
            // 
            // pbMenuPokemon
            // 
            this.pbMenuPokemon.BackgroundImage = global::Pokemon.Properties.Resources.menupokemon;
            this.pbMenuPokemon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbMenuPokemon.Location = new System.Drawing.Point(37, 2);
            this.pbMenuPokemon.Name = "pbMenuPokemon";
            this.pbMenuPokemon.Size = new System.Drawing.Size(25, 25);
            this.pbMenuPokemon.TabIndex = 1;
            this.pbMenuPokemon.TabStop = false;
            this.pbMenuPokemon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbMenuPoke_MouseClick);
            this.pbMenuPokemon.MouseEnter += new System.EventHandler(this.pbMenu_MouseEnter);
            this.pbMenuPokemon.MouseLeave += new System.EventHandler(this.pbMenu_MouseLeave);
            // 
            // pbMenuBag
            // 
            this.pbMenuBag.BackgroundImage = global::Pokemon.Properties.Resources.menubag;
            this.pbMenuBag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbMenuBag.Location = new System.Drawing.Point(5, 2);
            this.pbMenuBag.Name = "pbMenuBag";
            this.pbMenuBag.Size = new System.Drawing.Size(25, 25);
            this.pbMenuBag.TabIndex = 0;
            this.pbMenuBag.TabStop = false;
            this.pbMenuBag.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbMenuBag_MouseClick);
            this.pbMenuBag.MouseEnter += new System.EventHandler(this.pbMenu_MouseEnter);
            this.pbMenuBag.MouseLeave += new System.EventHandler(this.pbMenu_MouseLeave);
            // 
            // pbCanvas
            // 
            this.pbCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbCanvas.Location = new System.Drawing.Point(0, 0);
            this.pbCanvas.Name = "pbCanvas";
            this.pbCanvas.Size = new System.Drawing.Size(800, 600);
            this.pbCanvas.TabIndex = 0;
            this.pbCanvas.TabStop = false;
            this.pbCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pbCanvas_Paint);
            // 
            // FormGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.panelDialog);
            this.Controls.Add(this.pbMenuMaximize);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.pbCanvas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pokémon Resurrection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormGame_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormGame_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormGame_KeyUp);
            this.panelMenu.ResumeLayout(false);
            this.panelDialog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMenuMaximize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMenuHelp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMenuDig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMenuSurf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMenuCut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMenuSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMenuPokedex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMenuPokemon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMenuBag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox pbCanvas;
        private System.Windows.Forms.Timer timerRefresh;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.PictureBox pbMenuBag;
        private System.Windows.Forms.PictureBox pbMenuPokemon;
        public System.Windows.Forms.PictureBox pbMenuMaximize;
        private System.Windows.Forms.PictureBox pbMenuPokedex;
        public System.Windows.Forms.RichTextBox tbDialog;
        private System.Windows.Forms.Panel panelDialog;
        private System.Windows.Forms.PictureBox pbMenuSave;
        private System.Windows.Forms.PictureBox pbMenuCut;
        private System.Windows.Forms.PictureBox pbMenuSurf;
        private System.Windows.Forms.PictureBox pbMenuDig;
        private System.Windows.Forms.PictureBox pbMenuHelp;
    }
}