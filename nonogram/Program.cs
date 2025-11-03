using System;
using System.Windows.Forms;

namespace nonogram
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            //ApplicationConfiguration.Initialize();
            //Console.WriteLine("Hello, World!");
            //Application.Run(new LoginForm());
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
            //ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());
        }
    }
}