namespace Lab_Work_1._4
{
    internal static class Program
    {
        [STAThread]

        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Main_Form());
        }
    }
}