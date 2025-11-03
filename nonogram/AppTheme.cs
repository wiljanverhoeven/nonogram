using System;
using System.Drawing;
using System.Windows.Forms;

namespace nonogram
{
    public static class AppTheme
    {
        private static bool _isDarkMode = false;
        private const string ThemeFile = "theme.txt"; // saved locally next to .exe

        public static bool IsDarkMode
        {
            get => _isDarkMode;
            set
            {
                _isDarkMode = value;
                SaveThemeSetting();
            }
        }

        public static void ApplyTheme(Form form)
        {
            // Load saved theme if not loaded yet
            LoadThemeSetting();

            Color backColor = _isDarkMode ? Color.FromArgb(30, 30, 30) : Color.White;
            Color foreColor = _isDarkMode ? Color.White : Color.Black;

            form.BackColor = backColor;
            form.ForeColor = foreColor;

            ApplyThemeToControls(form.Controls, backColor, foreColor);
        }

        private static void ApplyThemeToControls(Control.ControlCollection controls, Color backColor, Color foreColor)
        {
            foreach (Control ctrl in controls)
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor = _isDarkMode ? Color.FromArgb(50, 50, 50) : Color.White;
                    btn.ForeColor = foreColor;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderColor = _isDarkMode ? Color.Gray : Color.DarkGray;
                }
                else if (ctrl is ComboBox combo)
                {
                    combo.BackColor = _isDarkMode ? Color.FromArgb(45, 45, 45) : Color.White;
                    combo.ForeColor = foreColor;
                    combo.FlatStyle = FlatStyle.Flat;
                }
                else if (ctrl is DataGridView grid)
                {
                    grid.BackgroundColor = backColor;
                    grid.DefaultCellStyle.BackColor = backColor;
                    grid.DefaultCellStyle.ForeColor = foreColor;
                    grid.ColumnHeadersDefaultCellStyle.BackColor = _isDarkMode ? Color.FromArgb(40, 40, 40) : Color.LightGray;
                    grid.ColumnHeadersDefaultCellStyle.ForeColor = foreColor;
                }
                else
                {
                    ctrl.BackColor = backColor;
                    ctrl.ForeColor = foreColor;
                }

                if (ctrl.HasChildren)
                    ApplyThemeToControls(ctrl.Controls, backColor, foreColor);
            }
        }

        public static void ApplyThemeToAllOpenForms()
        {
            foreach (Form form in Application.OpenForms)
                ApplyTheme(form);
        }

        private static void SaveThemeSetting()
        {
            try
            {
                System.IO.File.WriteAllText(ThemeFile, _isDarkMode ? "dark" : "light");
            }
            catch { }
        }

        private static void LoadThemeSetting()
        {
            try
            {
                if (System.IO.File.Exists(ThemeFile))
                    _isDarkMode = System.IO.File.ReadAllText(ThemeFile).Trim().Equals("dark", StringComparison.OrdinalIgnoreCase);
            }
            catch { }
        }
    }
}
