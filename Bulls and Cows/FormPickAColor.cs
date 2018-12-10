namespace DN_IDC_2017B_Ex05
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    internal class FormPickAColor : Form
    {
        private const int k_ColorButtonHeight = 50;
        private const int k_ColorButtonWidth = 50;
        private const int k_BufferSpace = 10;
        private const int k_NumberOfButtonsInRow = 4;
        private const int k_NumberOfButtonsInColumn = 2;
        private readonly List<Color> r_ColorOptions;
        private Color m_picketColor;

        public FormPickAColor()
        {
            r_ColorOptions = initializePossibleColors();
            InitButtons();
            Text = "Pick A Color:";
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void InitButtons()
        {
            Button currentButton;
            int currentLeftToDrawButton = k_BufferSpace;
            int currentTopToDrawButton = k_BufferSpace;
            int gameWidth = (k_NumberOfButtonsInRow * (k_ColorButtonWidth + k_BufferSpace)) + k_BufferSpace;
            int gameHeight = (k_NumberOfButtonsInColumn * (k_ColorButtonHeight + k_BufferSpace)) + k_BufferSpace;
            this.ClientSize = new Size(gameWidth, gameHeight);
            for (int i = 0; i < k_NumberOfButtonsInColumn; i++)
            {
                for (int j = 0; j < k_NumberOfButtonsInRow; j++)
                {
                    currentButton = new Button();
                    Point currentButtonLocation = new Point(currentLeftToDrawButton, currentTopToDrawButton);
                    currentButton.Height = k_ColorButtonHeight;
                    currentButton.Width = k_ColorButtonWidth;
                    currentButton.Location = currentButtonLocation;
                    currentButton.BackColor = r_ColorOptions[(i * k_NumberOfButtonsInRow) + j];
                    this.Controls.Add(currentButton);
                    currentButton.Click += new EventHandler(currentButton_Click);
                    currentLeftToDrawButton += k_BufferSpace + k_ColorButtonWidth;
                }

                currentLeftToDrawButton = k_BufferSpace;
                currentTopToDrawButton += k_BufferSpace + k_ColorButtonHeight;
            }
        }

        private void currentButton_Click(object sender, EventArgs e)
        {
            m_picketColor = (sender as Button).BackColor;
            this.Close();
        }

        private List<Color> initializePossibleColors()
        {
            List<Color> colorList = new List<Color>();
            colorList.Add(Color.SpringGreen);
            colorList.Add(Color.Gold);
            colorList.Add(Color.Pink);
            colorList.Add(Color.Red);
            colorList.Add(Color.Blue);
            colorList.Add(Color.Purple);
            colorList.Add(Color.Orange);
            colorList.Add(Color.BlanchedAlmond);

            return colorList;
        }

        public Color PickedColor
        {
            get { return m_picketColor; }
        }

        public Color GetColorInIndex(int i_ColorIndex)
        {
            return r_ColorOptions[i_ColorIndex];
        }

        public int AmountOfColors()
        {
            return r_ColorOptions.Capacity;
        }
    }
}