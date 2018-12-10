namespace DN_IDC_2017B_Ex05
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    internal class OneGameRow
    {
        private const string k_ArrowTextString = "-->>";
        private const string k_WinMessage = "Congratulations, You Are A Winner!";
        private const string k_LoseMessage = "You Lost, Better Luck Next Time!";
        private readonly Button[] r_RowGuessButtons;
        private Button m_ArrowButton;
        private int m_ElementLength;
        private int m_SpaceBetweenElements;
        private ResultSquaresQuartette m_GuessResult;
        private Point m_StartingPoint;
        private FormGame m_FormToDrawOn;
        private FormPickAColor m_ColorsForm = new FormPickAColor();

        public OneGameRow(FormGame i_FormToDrawOn, int i_NumberOfGuessButtons, Point i_StartingPoint, int i_ElementLength, int i_SpaceBetweenElements)
        {
            m_ElementLength = i_ElementLength;
            r_RowGuessButtons = new Button[i_NumberOfGuessButtons];
            m_FormToDrawOn = i_FormToDrawOn;
            m_StartingPoint = i_StartingPoint;
            m_SpaceBetweenElements = i_SpaceBetweenElements;
            addGuessButtonsToForm();
            addArrowButtonToForm();
            addResultSquaresToForm();
        }

        internal void ChangeRowEnabled(bool i_NewEnabled)
        {
            for (int i = 0; i < r_RowGuessButtons.Length; i++)
            {
                r_RowGuessButtons[i].Enabled = i_NewEnabled;
            }

            if (i_NewEnabled == false)
            {
                m_ArrowButton.Enabled = i_NewEnabled;
            }
        }

        internal void DisableRestOfGuessLines()
        {
            int indexOfRow = m_FormToDrawOn.RowsOfGuesses.IndexOf(this);
            for (int i = indexOfRow + 1; i < m_FormToDrawOn.NumberOfGuesses; i++)
            {
                m_FormToDrawOn.RowsOfGuesses[i].ChangeRowEnabled(false);
            }
        }

        private void addGuessButtonsToForm()
        {
            Point currentItemLocation = m_StartingPoint;
            for (int i = 0; i < r_RowGuessButtons.Length; i++)
            {
                Button currentButton = new Button();
                currentButton.Location = new Point(currentItemLocation.X, currentItemLocation.Y);
                currentButton.Size = new Size(m_ElementLength, m_ElementLength);
                currentButton.Enabled = false;
                m_FormToDrawOn.Controls.Add(currentButton);
                r_RowGuessButtons[i] = currentButton;
                currentItemLocation.X += m_SpaceBetweenElements + m_ElementLength;
                r_RowGuessButtons[i].Click += new EventHandler(currentButton_Click);
            }
        }

        private void addArrowButtonToForm()
        {
            int drawOffsetX = m_StartingPoint.X + (r_RowGuessButtons.Length * (m_ElementLength + m_SpaceBetweenElements));
            Point arrowButtonLocation = new Point(drawOffsetX, m_StartingPoint.Y + (m_ElementLength / 4));
            Button arrowButton = new Button();
            arrowButton.Location = arrowButtonLocation;
            arrowButton.Text = k_ArrowTextString;
            arrowButton.Size = new Size(m_ElementLength, m_ElementLength / 2);
            arrowButton.Enabled = false;
            m_ArrowButton = arrowButton;
            m_FormToDrawOn.Controls.Add(arrowButton);
            m_ArrowButton.Click += new EventHandler(arrowButton_Click);
        }

        private void addResultSquaresToForm()
        {
            int drawOffsetX = m_StartingPoint.X + ((r_RowGuessButtons.Length + 1) * (m_ElementLength + m_SpaceBetweenElements));
            Point resultSquaresLocation = new Point(drawOffsetX, m_StartingPoint.Y);
            ResultSquaresQuartette theResultForThisLine = new ResultSquaresQuartette(m_FormToDrawOn, resultSquaresLocation, m_ElementLength);
            m_GuessResult = theResultForThisLine;
        }

        private void currentButton_Click(object sender, EventArgs e)
        {
            m_ColorsForm.ShowDialog();

            if (asThisColorInRow(m_ColorsForm.PickedColor) == false)
            {
                (sender as Button).BackColor = m_ColorsForm.PickedColor;
            }

            if (isRowButtonColored())
            {
                m_ArrowButton.Enabled = true;
            }
        }

        private bool asThisColorInRow(Color i_Color)
        {
            bool asColorInRow = false;

            for (int i = 0; i < r_RowGuessButtons.Length; i++)
            {
                if (r_RowGuessButtons[i].BackColor == i_Color)
                {
                    asColorInRow = true;
                    break;
                }
            }

            return asColorInRow;
        }

        private bool isRowButtonColored()
        {
            bool isRowButtonColored = true;

            for (int i = 0; i < r_RowGuessButtons.Length; i++)
            {
                if (r_RowGuessButtons[i].BackColor == m_FormToDrawOn.BackColor)
                {
                    isRowButtonColored = false;
                    break;
                }
            }

            return isRowButtonColored;
        }

        private void arrowButton_Click(object sender, EventArgs e)
        {
            bool isWin = false;
            int indexOfRow = m_FormToDrawOn.RowsOfGuesses.IndexOf(this);
            ChangeRowEnabled(false);
            if (indexOfRow != m_FormToDrawOn.NumberOfGuesses - 1)
            {
                m_FormToDrawOn.RowsOfGuesses[indexOfRow + 1].ChangeRowEnabled(true);
            }

            isWin = checkGuessInRow();

            if (isWin)
            {
                m_FormToDrawOn.ShowCombinationLabels();
                DisableRestOfGuessLines();
                MessageBox.Show(k_WinMessage);
            }
            else
            {
                if (indexOfRow == m_FormToDrawOn.NumberOfGuesses - 1)
                {
                    m_FormToDrawOn.ShowCombinationLabels();
                    MessageBox.Show(k_LoseMessage);
                }
            }
        }

        private bool checkGuessInRow()
        {
            bool isWin = false;
            int bol = 0;
            int pgia = 0;
            computeResult(ref bol, ref pgia);
            m_GuessResult.DrawResults(bol, pgia);

            if (bol == m_FormToDrawOn.CombinationLength)
            {
                isWin = true;
            }

            return isWin;
        }

        // $G$ CSS-013 (-5) Bad variable name (should be in the form of i_PascalCase).
        private void computeResult(ref int r_bol, ref int r_pgia)
        {
            for (int i = 0; i < m_FormToDrawOn.CombinationLength; i++)
            {
                if (r_RowGuessButtons[i].BackColor.Equals(m_FormToDrawOn.CombinationLabels.GetColorInIndex(i)))
                {
                    r_bol++;
                }
                else
                {
                    if (m_FormToDrawOn.CombinationLabels.ContainsColor(r_RowGuessButtons[i].BackColor))
                    {
                        r_pgia++;
                    }
                }
            }
        }
    }
}