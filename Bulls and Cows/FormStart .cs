namespace DN_IDC_2017B_Ex05
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    // $G$ CSS-016 (-3) Bad class name - The name of classes derived from Form should end with Form.
    public class FormStart : Form
    {
        private const string k_TitleText = "Bool Pgia";
        private const int k_InitialNumberOfGuesses = 4;
        private const int k_NumberOfElements = 2;
        private const int k_MaximumNumberOfGuesses = 10;
        private const int k_BasicElementLength = 50;
        private const int k_BufferSize = 10;
        private Button m_ButtonNumberOfChances = new Button();
        private Button m_ButtonStart = new Button();
        private int m_NumberOfChances;

        public FormStart()
        {
            this.Text = k_TitleText;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.m_NumberOfChances = k_InitialNumberOfGuesses;
            InitControls();
            this.ClientSize = new Size(m_ButtonStart.Width + m_ButtonNumberOfChances.Width + (3 * k_BufferSize), m_ButtonStart.Height + (2 * k_BufferSize));
        }

        public int NumberOfChances
        {
            get { return m_NumberOfChances; }
        }

        private void InitControls()
        {
            m_ButtonNumberOfChances.Text = string.Format("Number Of Guesses: {0}", m_NumberOfChances);
            m_ButtonNumberOfChances.Size = new Size(k_BasicElementLength * 3, k_BasicElementLength);
            m_ButtonNumberOfChances.Location = new Point(k_BufferSize, k_BufferSize);
            m_ButtonStart.Text = "Start";
            m_ButtonStart.Size = new Size(k_BasicElementLength, k_BasicElementLength);
            m_ButtonStart.Location = new Point(
                m_ButtonNumberOfChances.Location.X + m_ButtonNumberOfChances.Width + k_BufferSize,
                m_ButtonNumberOfChances.Location.Y);
            this.Controls.AddRange(new Control[] { m_ButtonNumberOfChances, m_ButtonStart });
            this.m_ButtonNumberOfChances.Click += new EventHandler(m_ButtonNumberOfChances_Click);
            this.m_ButtonStart.Click += new EventHandler(m_ButtonStart_Click);
        }

        private void m_ButtonStart_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void m_ButtonNumberOfChances_Click(object sender, EventArgs e)
        {
            if (m_NumberOfChances == k_MaximumNumberOfGuesses)
            {
                m_NumberOfChances = k_InitialNumberOfGuesses;
            }
            else
            {
                m_NumberOfChances++;
            }

            m_ButtonNumberOfChances.Text = string.Format("Number Of Guesses: {0}", m_NumberOfChances);
        }
    }
}