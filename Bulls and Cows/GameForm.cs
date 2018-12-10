namespace DN_IDC_2017B_Ex05
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    public class FormGame : Form
    {
        private const int k_ElementLength = 50;
        private const int k_BufferSpace = 5;
        private const int k_CombinationLength = 4;
        private const int k_NumberOfAdditionalElements = 2;
        private readonly List<OneGameRow> m_RowsOfGuesses;
        private int m_NumberOfGuesses;
        private CombinationLabelsRow m_CombinationLabels;

        public FormGame(int i_NumOfGuesses)
        {
            m_NumberOfGuesses = i_NumOfGuesses;
            m_RowsOfGuesses = new List<OneGameRow>();
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.StartPosition = FormStartPosition.CenterScreen;
            buildGameElements();
        }

        public int NumberOfGuesses
        {
            get { return m_NumberOfGuesses; }
        }

        public int CombinationLength
        {
            get { return k_CombinationLength; }
        }

        internal CombinationLabelsRow CombinationLabels
        {
            get { return m_CombinationLabels; }
        }

        internal List<OneGameRow> RowsOfGuesses
        {
            get { return m_RowsOfGuesses; }
        }

        internal void ShowCombinationLabels()
        {
            m_CombinationLabels.RemoveBlackCovers();
        }

        private void buildGameElements()
        {
            Point lineDrawLocation = new Point(k_BufferSpace, k_ElementLength + k_BufferSpace);
            int gameWidth = ((k_CombinationLength + k_NumberOfAdditionalElements) * (k_ElementLength + k_BufferSpace)) + k_BufferSpace;
            int gameHeight = (m_NumberOfGuesses + k_NumberOfAdditionalElements) * (k_ElementLength + k_BufferSpace);

            this.ClientSize = new Size(gameWidth, gameHeight);

            for (int i = 0; i < m_NumberOfGuesses; i++)
            {
                lineDrawLocation.Y += k_ElementLength + k_BufferSpace;
                m_RowsOfGuesses.Add(new OneGameRow(this, k_CombinationLength, lineDrawLocation, k_ElementLength, k_BufferSpace));
                if (i == 0)
                {
                    m_RowsOfGuesses[i].ChangeRowEnabled(true);
                }
            }

            m_CombinationLabels = new CombinationLabelsRow(this, k_BufferSpace, k_CombinationLength, k_ElementLength);
        }
    }
}