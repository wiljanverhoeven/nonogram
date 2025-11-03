using System;
using System.Windows.Forms;

namespace nonogram
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var loginForm = new LoginForm();
            Application.Run(loginForm);

            if (UserSession.IsLoggedIn)
            {
                Application.Run(new Form1());
            }
        }
    }
}