namespace DN_IDC_2017B_Ex05
{
    public class Program
    {
        public static void Main()
        {
            FormStart formStart = new FormStart();
            formStart.ShowDialog();
            FormGame gameForm = new FormGame(formStart.NumberOfChances);
            gameForm.ShowDialog();
        }
    }
}