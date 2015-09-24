using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;

namespace Pokemon
{
    public partial class FormStart : Form
    {
        #region Initialization

        public FormStart()
        {
            InitializeComponent();

            labelCopyright_Initialize();
            panel_Initialize();
            backgroundImage_Initialize();

            // check if loading file exists
            if (!File.Exists(Application.StartupPath + "\\game.sav")) 
                buttonLoadGame.Enabled = false;
        }

        #endregion

        #region Images&Colors

        Image[] backgroundImage;
        int backgroundImageIndex, backgroundImageSize;
        private void backgroundImage_Initialize() // background animation
        {
            backgroundImageSize = 4;

            backgroundImage = new Image[backgroundImageSize];
            backgroundImage[0] = Pokemon.Properties.Resources.BackgroundPikachu;
            backgroundImage[1] = Pokemon.Properties.Resources.BackgroundStartersGen2;
            backgroundImage[2] = Pokemon.Properties.Resources.BackgroundBadges;
            backgroundImage[3] = Pokemon.Properties.Resources.BackgroundArceus;

            backgroundImageIndex = 0;
            pbBackground.BackgroundImage = backgroundImage[backgroundImageIndex];

            timerBackgroundPicture.Interval = 3000;
            timerBackgroundPicture.Start();
        }

        private void pbMenu_Paint(object sender, PaintEventArgs e) // colored menu (linear gradient brush)
        {
            Graphics canvasGraphic = e.Graphics;
            Rectangle canvasRectangle = new Rectangle(0, 0, pbMenu.Width - 1, pbMenu.Height - 1);

            LinearGradientBrush backColor = new LinearGradientBrush(canvasRectangle, Color.DarkOrange, Color.SandyBrown, 260);
            LinearGradientBrush outlineBrush = new LinearGradientBrush(canvasRectangle, Color.Coral, Color.LightSalmon, 210);

            canvasGraphic.FillRectangle(backColor, canvasRectangle);
            canvasGraphic.DrawRectangle(new Pen(outlineBrush), canvasRectangle);
        }

        private void timerBackgroundPicture_Tick(object sender, EventArgs e) // timer for background animation
        {
            backgroundImageIndex = (backgroundImageIndex + 1) % backgroundImageSize;

            pbBackground.BackgroundImage = backgroundImage[backgroundImageIndex];
        }

        private void pbBackground_Paint(object sender, PaintEventArgs e) // drawing logo
        {
            e.Graphics.DrawImage(Pokemon.Properties.Resources.logo, new Point(20, 30));
        }

        #endregion

        #region Copyright

        private void labelCopyright_Initialize()
        {
            int currentYear = Convert.ToInt32(DateTime.Now.ToString("yyyy"));

            labelCopyright.Parent = pbBackground;
            labelCopyright.BackColor = Color.Transparent;
            labelCopyright.Text = "Pokémon is Copyright © 1995-" + currentYear + " Nintendo/Creatures Inc./GAME FREAK Inc.\n" +
                                  "Pokémon Resurrection is not affiliated with Nintendo, Creatures Inc. and GAME FREAK Inc.\n" +
                                  "Pokémon Resurrection is created using Microsoft Visual Studio Express 2013 C# and is open source.\n" +
                                  "Pokémon Resurrection is created by Suditu Thomas-Cristian";
        }

        #endregion

        #region Buttons Animation

        private void panel_Initialize() // panels size to 0
        {
            panelSinglePlayer.Size = new Size(buttonSinglePlayer.Width, 0);
            panelMultiPlayer.Size = new Size(buttonMultiPlayer.Width, 0);
        }

        bool[] buttonPlay = new bool[2];
        Button clickedPlayButon;
        private void buttonPlay_Click(object sender, EventArgs e) // begin to extend the panels
        {
            if (timerButton.Enabled) return;

            clickedPlayButon = ((Button)sender);

            timerTicks = 0;
            timerButton.Interval = 10;
            timerButton.Start();
        }

        int timerTicks;
        private void timerButton_Tick(object sender, EventArgs e) // timer for extending the panels
        {
            int buttonIndex;
            bool otherIsExpanded = false;

            if (clickedPlayButon == buttonSinglePlayer)
            {
                buttonIndex = 0;
                ButtonPanelMove(buttonSinglePlayer, panelSinglePlayer, buttonIndex);

                if (buttonPlay[1 - buttonIndex] == true)
                {
                    otherIsExpanded = true;
                    ButtonPanelMove(buttonMultiPlayer, panelMultiPlayer, 1 - buttonIndex);
                }
            }
            else
            {
                buttonIndex = 1;
                ButtonPanelMove(buttonMultiPlayer, panelMultiPlayer, buttonIndex);

                if (buttonPlay[1 - buttonIndex] == true)
                {
                    otherIsExpanded = true;
                    ButtonPanelMove(buttonSinglePlayer, panelSinglePlayer, 1 - buttonIndex);
                }
            }

            if (timerTicks == 15)
            {
                buttonPlay[buttonIndex] = !buttonPlay[buttonIndex];
                
                if(otherIsExpanded)
                    buttonPlay[1-buttonIndex] = !buttonPlay[1-buttonIndex];

                timerButton.Stop();
                return;
            }

            timerTicks += 1;
        }

        private void ButtonPanelMove(Button crtButton, Panel crtPanel, int buttonIndex)
        {
            if (!buttonPlay[buttonIndex])
            {
                crtButton.Location = new Point(crtButton.Location.X, crtButton.Location.Y - 3);
                crtPanel.Size = new Size(crtButton.Width, timerTicks * 8);
            }
            else
            {
                crtButton.Location = new Point(crtButton.Location.X, crtButton.Location.Y + 3);
                crtPanel.Size = new Size(crtButton.Width, (15 - timerTicks) * 8);
            }

            crtPanel.Location = new Point(crtButton.Location.X, crtButton.Location.Y + crtButton.Height);
        }

        #endregion

        #region Buttons Functions
        
        private void buttonNewGame_Click(object sender, EventArgs e)
        {
            applicationIsRunning = true;

            new FormLoadingScreen(true).Show();

            this.Close();
        }

        private void buttonLoadGame_Click(object sender, EventArgs e)
        {
            applicationIsRunning = true;

            new FormLoadingScreen(false).Show();

            this.Close();
        }

        #endregion

        #region Close Application

        bool applicationIsRunning;
        private void FormStart_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!applicationIsRunning)
                Application.Exit();
        }

        #endregion
    }
}
