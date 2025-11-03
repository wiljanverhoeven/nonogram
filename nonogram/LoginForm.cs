using System;
using System.Windows.Forms;

namespace nonogram
{
    public partial class LoginForm : Form
    {
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

            //MessageBox.Show($"DEBUG: Attempting login for user: {username}");

            User user = UserService.LoginUser(username, password);

            if (user != null)
            {
                //MessageBox.Show($"DEBUG: Login successful - UserId: {user.UserId}");

                UserSession.Login(user);
                MessageBox.Show($"Welcome back, {username}!");

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password!");
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Hide();
            registerForm.FormClosed += (s, args) => this.Show();
        }

    }
}