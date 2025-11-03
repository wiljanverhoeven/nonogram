using System;
using System.Windows.Forms;

namespace nonogram
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            AppTheme.ApplyTheme(this);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void btnRegister_Click_1(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            string email = txtEmail.Text.Trim();

            // Validation
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match!");
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long.");
                return;
            }

            //MessageBox.Show($"DEBUG: Attempting registration for user: {username}");

            // Attempt registration
            if (UserService.Register(username, password, email))
            {
                //MessageBox.Show($"DEBUG: Registration successful for: {username}");

                User user = UserService.GetUser(username);
                if (user != null)
                {
                    //MessageBox.Show($"DEBUG: Retrieved user from DB - UserId: {user.UserId}");

                    UserSession.Login(user);
                    MessageBox.Show($"Registration successful! Welcome {username}!");

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error: Could not retrieve user after registration.");
                }
            }
            else
            {
                MessageBox.Show("Username already exists. Please choose a different username.");
            }
        }
    }
}