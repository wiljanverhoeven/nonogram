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

            // Toon/verberg wachtwoord velden
            lblCurrentPassword.Visible = show;
            txtCurrentPassword.Visible = show;
            lblNewPassword.Visible = show;
            txtNewPassword.Visible = show;

            // Pas de hoogte van het form aan
            this.Height = show ? 520 : 450;

            // Verplaats de save knop naar beneden
            save.Location = new System.Drawing.Point(save.Location.X, show ? 420 : 344);
        }

        private void save_Click(object sender, EventArgs e)
        {
            try
            {
                bool changesMade = false;

                // Update profile (username en email)
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

                // Update settings
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

                // Update password als velden zichtbaar zijn en ingevuld
                if (_showPasswordFields && !string.IsNullOrWhiteSpace(txtCurrentPassword.Text))
                {
                    if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
                    {
                        MessageBox.Show("Vul een nieuw wachtwoord in!", "Fout",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (txtNewPassword.Text.Length < 4)
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
                        // Reset wachtwoord velden
                        txtCurrentPassword.Text = "";
                        txtNewPassword.Text = "";
                        TogglePasswordFields(false); // Verberg weer
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
            // Toon wachtwoord velden
            TogglePasswordFields(true);
        }

        private void chkDarkMode_CheckedChanged(object sender, EventArgs e)
        {
            AppTheme.IsDarkMode = chkDarkMode.Checked;
            AppTheme.ApplyThemeToAllOpenForms();
        }

    }
}