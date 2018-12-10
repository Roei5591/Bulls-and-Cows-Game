namespace DN_IDC_2017B_Ex05
{
    using System.Drawing;
    using System.Windows.Forms;

    internal class ResultSquaresQuartette
    {
        private const int k_NumberOfLabelsPerLine = 2;
        private const int k_NumberOfLines = 2;
        private Label[] m_ResultLabels = new Label[k_NumberOfLabelsPerLine * k_NumberOfLines];
        private Point m_LocationOnForm;
        private Form m_FormToDrawOn;
        private int m_LabelSideLength;
        private int m_SpaceBetweenLabels;

        public ResultSquaresQuartette(Form i_FormToDrawOn, Point i_LocationOnForm, int i_DrawingSpaceSize)
        {
            m_LocationOnForm = new Point(i_LocationOnForm.X, i_LocationOnForm.Y);
            m_FormToDrawOn = i_FormToDrawOn;
            m_SpaceBetweenLabels = i_DrawingSpaceSize / 10;
            m_LabelSideLength = (i_DrawingSpaceSize / 2) - m_SpaceBetweenLabels;
            AddResultSquaresToForm();
        }

        public void DrawResults(int i_BoolHit, int i_Hit)
        {
            for (int i = 0; i < m_ResultLabels.Length; i++)
            {
                if (i_BoolHit > 0)
                {
                    m_ResultLabels[i].BackColor = Color.Black;
                    i_BoolHit--;
                }
                else
                {
                    if (i_Hit > 0)
                    {
                        m_ResultLabels[i].BackColor = Color.Yellow;
                        i_Hit--;
                    }
                }
            }
        }

        // $G$ CSS-999 (-3) Private methods should start with a lowercase letter.
        private void AddResultSquaresToForm()
        {
            Point nextLabelLocation = new Point(m_LocationOnForm.X, m_LocationOnForm.Y);
            for (int i = 0; i < k_NumberOfLines; i++)
            {
                for (int j = 0; j < k_NumberOfLabelsPerLine; j++)
                {
                    Label currentLabel = new Label();
                    currentLabel.Location = nextLabelLocation;
                    currentLabel.BorderStyle = BorderStyle.Fixed3D;
                    currentLabel.Size = new Size(m_LabelSideLength, m_LabelSideLength);
                    m_FormToDrawOn.Controls.Add(currentLabel);
                    m_ResultLabels[(2 * i) + j] = currentLabel;
                    nextLabelLocation.X += m_LabelSideLength + m_SpaceBetweenLabels;
                }

                nextLabelLocation.X = m_LocationOnForm.X;
                nextLabelLocation.Y += m_LabelSideLength + m_SpaceBetweenLabels;
            }
        }
    }
}
