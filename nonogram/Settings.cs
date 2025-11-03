using System;
using System.Windows.Forms;

namespace nonogram
{
    public partial class Settings : Form
    {
        private User _currentUser;
        private bool _showPasswordFields = false;

        public Settings(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            LoadUserData();
            TogglePasswordFields(false);
        }

        public Settings()
        {
        }

        private void LoadUserData()
        {
            AppTheme.ApplyTheme(this);
            // Laad user data
            username.Text = _currentUser.Username;
            email.Text = _currentUser.Email;

            try
            {
                var settings = UserService.GetUserSettings(_currentUser.UserId);
                chkDarkMode.Checked = settings.theme == "dark";
                grid_size.Text = settings.gridSize.ToString();

                // Pas het thema direct toe
                AppTheme.IsDarkMode = chkDarkMode.Checked;
                AppTheme.ApplyThemeToAllOpenForms();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fout bij laden instellingen: {ex.Message}");
                chkDarkMode.Checked = false;
                grid_size.Text = "10";

                AppTheme.IsDarkMode = false;
                AppTheme.ApplyThemeToAllOpenForms();
            }
        }

        private void TogglePasswordFields(bool show)
        {
            _showPasswordFields = show;

            lblCurrentPassword.Visible = show;
            txtCurrentPassword.Visible = show;
            lblNewPassword.Visible = show;
            txtNewPassword.Visible = show;

            this.Height = show ? 520 : 450;

            save.Location = new System.Drawing.Point(save.Location.X, show ? 420 : 344);
        }

        private void save_Click(object sender, EventArgs e)
        {
            try
            {
                bool changesMade = false;

                if (username.Text != _currentUser.Username || email.Text != _currentUser.Email)
                {
                    bool profileUpdated = UserService.UpdateUserProfile(
                        _currentUser.UserId,
                        username.Text,
                        email.Text
                    );

                    if (profileUpdated)
                    {
                        _currentUser.Username = username.Text;
                        _currentUser.Email = email.Text;
                        changesMade = true;
                    }
                    else
                    {
                        MessageBox.Show("Gebruikersnaam is al in gebruik!", "Fout",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                try
                {
                    bool settingsUpdated = UserService.UpdateUserSettings(
                        _currentUser.UserId,
                        chkDarkMode.Checked ? "dark" : "light",
                        int.Parse(grid_size.Text)
                    );
                    changesMade = changesMade || settingsUpdated;
                }
                catch (FormatException)
                {
                    MessageBox.Show("Grid grootte moet een getal zijn!", "Fout",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (_showPasswordFields && !string.IsNullOrWhiteSpace(txtCurrentPassword.Text))
                {
                    if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
                    {
                        MessageBox.Show("Vul een nieuw wachtwoord in!", "Fout",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (txtNewPassword.Text.Length < 6)
                    {
                        MessageBox.Show("Nieuw wachtwoord moet minimaal 4 karakters zijn!", "Fout",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    bool passwordUpdated = UserService.ChangePassword(
                        _currentUser.UserId,
                        txtCurrentPassword.Text,
                        txtNewPassword.Text
                    );

                    if (passwordUpdated)
                    {
                        changesMade = true;
                        txtCurrentPassword.Text = "";
                        txtNewPassword.Text = "";
                        TogglePasswordFields(false);
                        MessageBox.Show("Wachtwoord succesvol gewijzigd!", "Succes",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Huidig wachtwoord is incorrect!", "Fout",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (changesMade)
                {
                    MessageBox.Show("Alle wijzigingen opgeslagen!", "Succes",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Geen wijzigingen opgeslagen.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fout bij opslaan: {ex.Message}", "Fout",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void change_password_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TogglePasswordFields(true);
        }

        private void chkDarkMode_CheckedChanged(object sender, EventArgs e)
        {
            AppTheme.IsDarkMode = chkDarkMode.Checked;
            AppTheme.ApplyThemeToAllOpenForms();
        }

    }
}