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

        private void btnRegister_Click(object sender, EventArgs e)
        {
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void btnRegister_Click_1(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtConfirmPassword.Text;
            string confirmPassword = txtPassword.Text;
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

            // Attempt registration
            if (UserService.Register(username, password, email))
            {
                MessageBox.Show("Registration successful! You can now login.");
                this.Close(); // Close the registration form
            }
            else
            {
                MessageBox.Show("Username already exists. Please choose a different username.");
            }
        }
    }
}