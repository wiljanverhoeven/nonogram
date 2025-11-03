using System;
using System.Windows.Forms;

namespace nonogram
{

    public partial class LoginForm : Form
    {
        public static string CurrentUser { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            if (UserService.Login(username, password))
            {
                CurrentUser = username;
                MessageBox.Show($"Welcome back, {username}!");

                // Open your main Nonogram form
                Form1 mainForm = new Form1();
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password!");
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Registration will be added later. Use: admin/admin123");
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();

            this.Hide();
            registerForm.FormClosed += (s, args) => this.Show();
        }

        // Add this property to expose the logged-in user
        public string LoggedInUser
        {
            get { return CurrentUser; }
        }
    }
}