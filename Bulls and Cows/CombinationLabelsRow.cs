namespace DN_IDC_2017B_Ex05
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    // $G$ DSN-002 (-30) The Logic should not depend on a UI in order to function.
    internal class CombinationLabelsRow
    {
        private readonly List<Label> m_CombinationLabels = new List<Label>();
        // $G$ CSS-999 (-5) Bad readonly variable name (should be r_CamelCased)
        private readonly List<Label> m_BlackCovers = new List<Label>();
        private FormGame m_Form;
        // $G$ CSS-999 (-3) This kind of field should be readonly.
        private int m_BufferSpace;
        private int m_CombinationLength;
        private int m_ElementLength;

        public CombinationLabelsRow(FormGame i_Form, int i_BufferSpace, int i_CombinationLength, int i_ElementLength)
        {
            m_Form = i_Form;
            m_BufferSpace = i_BufferSpace;
            m_CombinationLength = i_CombinationLength;
            m_ElementLength = i_ElementLength;

            drawCombinationLabelsAndCovers();
            generateCombinationLabels();
        }

        internal bool ContainsColor(Color i_Color)
        {
            bool foundDuplicate = false;
            foreach (Label combinationLabel in m_CombinationLabels)
            {
                if (combinationLabel.BackColor.Equals(i_Color))
                {
                    foundDuplicate = true;
                    break;
                }
            }

            return foundDuplicate;
        }

        internal void RemoveBlackCovers()
        {
            foreach (Label blackCover in m_BlackCovers)
            {
                blackCover.Visible = false;
            }
        }

        internal Color GetColorInIndex(int i_ColorIndex)
        {
            return m_CombinationLabels[i_ColorIndex].BackColor;
        }

        private void drawCombinationLabelsAndCovers()
        {
            Label combinationLabel;
            Label blackCover;
            int offsetX = m_BufferSpace;

            for (int i = 0; i < m_CombinationLength; i++)
            {
                combinationLabel = new Label();
                blackCover = new Label();
                combinationLabel.Location = new Point(offsetX, m_BufferSpace);
                blackCover.Location = new Point(offsetX, m_BufferSpace);
                combinationLabel.Size = new Size(m_ElementLength, m_ElementLength);
                blackCover.Size = new Size(m_ElementLength, m_ElementLength);
                blackCover.BackColor = Color.Black;
                blackCover.Visible = true;
                m_Form.Controls.Add(blackCover);
                m_Form.Controls.Add(combinationLabel);
                m_CombinationLabels.Add(combinationLabel);
                m_BlackCovers.Add(blackCover);
                offsetX += m_ElementLength + m_BufferSpace;
            }
        }

        private void generateCombinationLabels()
        {
            int combinationIndex = 0;
            int colorIndex;
            List<int> listOfColorsIntex = new List<int>();
            FormPickAColor colors = new FormPickAColor();
            Random random = new Random();

            while (combinationIndex < 4)
            {
                colorIndex = random.Next(colors.AmountOfColors());
                if (listOfColorsIntex.Contains(colorIndex) == false)
                {
                    listOfColorsIntex.Add(colorIndex);
                    m_CombinationLabels[combinationIndex].BackColor = colors.GetColorInIndex(colorIndex);
                    combinationIndex++;
                }
            }
        }
    }
}