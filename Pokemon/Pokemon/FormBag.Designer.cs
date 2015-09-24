namespace Pokemon
{
    partial class FormBag
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
            this.buttonClose = new System.Windows.Forms.Button();
            this.listBoxPokeballs = new System.Windows.Forms.ListBox();
            this.groupBoxPokeballs = new System.Windows.Forms.GroupBox();
            this.tbDescription = new System.Windows.Forms.RichTextBox();
            this.buttonUse = new System.Windows.Forms.Button();
            this.labelMoney = new System.Windows.Forms.Label();
            this.groupBoxPotions = new System.Windows.Forms.GroupBox();
            this.listBoxPotions = new System.Windows.Forms.ListBox();
            this.groupBoxOthers = new System.Windows.Forms.GroupBox();
            this.listBoxOthers = new System.Windows.Forms.ListBox();
            this.groupBoxPokeballs.SuspendLayout();
            this.groupBoxPotions.SuspendLayout();
            this.groupBoxOthers.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClose.Location = new System.Drawing.Point(12, 363);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(100, 25);
            this.buttonClose.TabIndex = 5;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // listBoxPokeballs
            // 
            this.listBoxPokeballs.BackColor = System.Drawing.Color.Plum;
            this.listBoxPokeballs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxPokeballs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxPokeballs.FormattingEnabled = true;
            this.listBoxPokeballs.Location = new System.Drawing.Point(18, 19);
            this.listBoxPokeballs.Name = "listBoxPokeballs";
            this.listBoxPokeballs.Size = new System.Drawing.Size(284, 78);
            this.listBoxPokeballs.TabIndex = 6;
            this.listBoxPokeballs.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            // 
            // groupBoxPokeballs
            // 
            this.groupBoxPokeballs.Controls.Add(this.listBoxPokeballs);
            this.groupBoxPokeballs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxPokeballs.ForeColor = System.Drawing.Color.White;
            this.groupBoxPokeballs.Location = new System.Drawing.Point(268, 12);
            this.groupBoxPokeballs.Name = "groupBoxPokeballs";
            this.groupBoxPokeballs.Size = new System.Drawing.Size(320, 116);
            this.groupBoxPokeballs.TabIndex = 7;
            this.groupBoxPokeballs.TabStop = false;
            this.groupBoxPokeballs.Text = "Pokéballs";
            // 
            // tbDescription
            // 
            this.tbDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbDescription.Location = new System.Drawing.Point(12, 214);
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.ReadOnly = true;
            this.tbDescription.Size = new System.Drawing.Size(214, 134);
            this.tbDescription.TabIndex = 8;
            this.tbDescription.Text = "";
            // 
            // buttonUse
            // 
            this.buttonUse.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonUse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUse.Location = new System.Drawing.Point(126, 363);
            this.buttonUse.Name = "buttonUse";
            this.buttonUse.Size = new System.Drawing.Size(100, 25);
            this.buttonUse.TabIndex = 9;
            this.buttonUse.Text = "Use";
            this.buttonUse.UseVisualStyleBackColor = true;
            this.buttonUse.Click += new System.EventHandler(this.buttonUse_Click);
            // 
            // labelMoney
            // 
            this.labelMoney.AutoSize = true;
            this.labelMoney.BackColor = System.Drawing.Color.Transparent;
            this.labelMoney.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMoney.Location = new System.Drawing.Point(3, 4);
            this.labelMoney.Name = "labelMoney";
            this.labelMoney.Size = new System.Drawing.Size(45, 25);
            this.labelMoney.TabIndex = 10;
            this.labelMoney.Text = "0 ¥";
            // 
            // groupBoxPotions
            // 
            this.groupBoxPotions.Controls.Add(this.listBoxPotions);
            this.groupBoxPotions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxPotions.ForeColor = System.Drawing.Color.White;
            this.groupBoxPotions.Location = new System.Drawing.Point(268, 139);
            this.groupBoxPotions.Name = "groupBoxPotions";
            this.groupBoxPotions.Size = new System.Drawing.Size(320, 116);
            this.groupBoxPotions.TabIndex = 11;
            this.groupBoxPotions.TabStop = false;
            this.groupBoxPotions.Text = "Potions";
            // 
            // listBoxPotions
            // 
            this.listBoxPotions.BackColor = System.Drawing.Color.Plum;
            this.listBoxPotions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxPotions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxPotions.FormattingEnabled = true;
            this.listBoxPotions.Location = new System.Drawing.Point(18, 19);
            this.listBoxPotions.Name = "listBoxPotions";
            this.listBoxPotions.Size = new System.Drawing.Size(284, 78);
            this.listBoxPotions.TabIndex = 6;
            this.listBoxPotions.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            // 
            // groupBoxOthers
            // 
            this.groupBoxOthers.Controls.Add(this.listBoxOthers);
            this.groupBoxOthers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxOthers.ForeColor = System.Drawing.Color.White;
            this.groupBoxOthers.Location = new System.Drawing.Point(268, 266);
            this.groupBoxOthers.Name = "groupBoxOthers";
            this.groupBoxOthers.Size = new System.Drawing.Size(320, 116);
            this.groupBoxOthers.TabIndex = 12;
            this.groupBoxOthers.TabStop = false;
            this.groupBoxOthers.Text = "Others";
            // 
            // listBoxOthers
            // 
            this.listBoxOthers.BackColor = System.Drawing.Color.Plum;
            this.listBoxOthers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxOthers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxOthers.FormattingEnabled = true;
            this.listBoxOthers.Location = new System.Drawing.Point(18, 19);
            this.listBoxOthers.Name = "listBoxOthers";
            this.listBoxOthers.Size = new System.Drawing.Size(284, 78);
            this.listBoxOthers.TabIndex = 6;
            this.listBoxOthers.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            // 
            // FormBag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Plum;
            this.BackgroundImage = global::Pokemon.Properties.Resources.bagbackground;
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.groupBoxOthers);
            this.Controls.Add(this.groupBoxPotions);
            this.Controls.Add(this.labelMoney);
            this.Controls.Add(this.buttonUse);
            this.Controls.Add(this.tbDescription);
            this.Controls.Add(this.groupBoxPokeballs);
            this.Controls.Add(this.buttonClose);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormBag";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormBag";
            this.groupBoxPokeballs.ResumeLayout(false);
            this.groupBoxPotions.ResumeLayout(false);
            this.groupBoxOthers.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ListBox listBoxPokeballs;
        private System.Windows.Forms.GroupBox groupBoxPokeballs;
        private System.Windows.Forms.RichTextBox tbDescription;
        private System.Windows.Forms.Button buttonUse;
        private System.Windows.Forms.Label labelMoney;
        private System.Windows.Forms.GroupBox groupBoxPotions;
        private System.Windows.Forms.ListBox listBoxPotions;
        private System.Windows.Forms.GroupBox groupBoxOthers;
        private System.Windows.Forms.ListBox listBoxOthers;
    }
}