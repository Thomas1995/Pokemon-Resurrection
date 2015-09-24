namespace Pokemon
{
    partial class FormMoveDelete
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
            this.attack4 = new System.Windows.Forms.Button();
            this.attack3 = new System.Windows.Forms.Button();
            this.attack2 = new System.Windows.Forms.Button();
            this.attack1 = new System.Windows.Forms.Button();
            this.newAttack = new System.Windows.Forms.Button();
            this.tbAttack = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // attack4
            // 
            this.attack4.BackColor = System.Drawing.Color.BurlyWood;
            this.attack4.Enabled = false;
            this.attack4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.attack4.Location = new System.Drawing.Point(20, 150);
            this.attack4.Name = "attack4";
            this.attack4.Size = new System.Drawing.Size(150, 30);
            this.attack4.TabIndex = 8;
            this.attack4.Text = "-";
            this.attack4.UseVisualStyleBackColor = false;
            this.attack4.Click += new System.EventHandler(this.attack_Click);
            // 
            // attack3
            // 
            this.attack3.BackColor = System.Drawing.Color.BurlyWood;
            this.attack3.Enabled = false;
            this.attack3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.attack3.Location = new System.Drawing.Point(19, 116);
            this.attack3.Name = "attack3";
            this.attack3.Size = new System.Drawing.Size(150, 30);
            this.attack3.TabIndex = 7;
            this.attack3.Text = "-";
            this.attack3.UseVisualStyleBackColor = false;
            this.attack3.Click += new System.EventHandler(this.attack_Click);
            // 
            // attack2
            // 
            this.attack2.BackColor = System.Drawing.Color.BurlyWood;
            this.attack2.Enabled = false;
            this.attack2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.attack2.Location = new System.Drawing.Point(20, 82);
            this.attack2.Name = "attack2";
            this.attack2.Size = new System.Drawing.Size(150, 30);
            this.attack2.TabIndex = 6;
            this.attack2.Text = "-";
            this.attack2.UseVisualStyleBackColor = false;
            this.attack2.Click += new System.EventHandler(this.attack_Click);
            // 
            // attack1
            // 
            this.attack1.BackColor = System.Drawing.Color.BurlyWood;
            this.attack1.Enabled = false;
            this.attack1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.attack1.Location = new System.Drawing.Point(20, 48);
            this.attack1.Name = "attack1";
            this.attack1.Size = new System.Drawing.Size(150, 30);
            this.attack1.TabIndex = 5;
            this.attack1.Text = "-";
            this.attack1.UseVisualStyleBackColor = false;
            this.attack1.Click += new System.EventHandler(this.attack_Click);
            // 
            // newAttack
            // 
            this.newAttack.BackColor = System.Drawing.Color.BurlyWood;
            this.newAttack.Enabled = false;
            this.newAttack.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.newAttack.Location = new System.Drawing.Point(206, 48);
            this.newAttack.Name = "newAttack";
            this.newAttack.Size = new System.Drawing.Size(150, 30);
            this.newAttack.TabIndex = 9;
            this.newAttack.Text = "-";
            this.newAttack.UseVisualStyleBackColor = false;
            // 
            // tbAttack
            // 
            this.tbAttack.BackColor = System.Drawing.Color.White;
            this.tbAttack.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbAttack.Location = new System.Drawing.Point(206, 84);
            this.tbAttack.Name = "tbAttack";
            this.tbAttack.ReadOnly = true;
            this.tbAttack.Size = new System.Drawing.Size(149, 96);
            this.tbAttack.TabIndex = 10;
            this.tbAttack.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(362, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Detele a move in order to learn a new one";
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.Color.Red;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.ForeColor = System.Drawing.Color.White;
            this.buttonExit.Location = new System.Drawing.Point(360, 0);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(20, 20);
            this.buttonExit.TabIndex = 12;
            this.buttonExit.Text = "X";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // FormMoveDelete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Pokemon.Properties.Resources.evolutionbackground;
            this.ClientSize = new System.Drawing.Size(380, 200);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbAttack);
            this.Controls.Add(this.newAttack);
            this.Controls.Add(this.attack4);
            this.Controls.Add(this.attack3);
            this.Controls.Add(this.attack2);
            this.Controls.Add(this.attack1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormMoveDelete";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormMoveDelete";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button attack4;
        private System.Windows.Forms.Button attack3;
        private System.Windows.Forms.Button attack2;
        private System.Windows.Forms.Button attack1;
        private System.Windows.Forms.Button newAttack;
        private System.Windows.Forms.RichTextBox tbAttack;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonExit;
    }
}