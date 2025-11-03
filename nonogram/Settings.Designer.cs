namespace nonogram
{
    partial class Settings
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            username = new TextBox();
            email = new TextBox();
            change_password = new LinkLabel();
            grid_size = new TextBox();
            save = new Button();
            lblCurrentPassword = new Label();
            txtCurrentPassword = new TextBox();
            lblNewPassword = new Label();
            txtNewPassword = new TextBox();
            chkDarkMode = new CheckBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(434, 83);
            label1.Name = "label1";
            label1.Size = new Size(171, 25);
            label1.TabIndex = 0;
            label1.Text = "Game instellingen";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(158, 76);
            label2.Name = "label2";
            label2.Size = new Size(168, 25);
            label2.TabIndex = 1;
            label2.Text = "Profiel aanpassen";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(158, 119);
            label4.Name = "label4";
            label4.Size = new Size(96, 15);
            label4.TabIndex = 3;
            label4.Text = "Gebruikersnaam:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(158, 166);
            label5.Name = "label5";
            label5.Size = new Size(36, 15);
            label5.TabIndex = 4;
            label5.Text = "Email";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(434, 166);
            label6.Name = "label6";
            label6.Size = new Size(65, 15);
            label6.TabIndex = 5;
            label6.Text = "Grid-grote:";
            // 
            // username
            // 
            username.Location = new Point(158, 136);
            username.Margin = new Padding(3, 2, 3, 2);
            username.Name = "username";
            username.Size = new Size(156, 23);
            username.TabIndex = 1;
            // 
            // email
            // 
            email.Location = new Point(158, 183);
            email.Margin = new Padding(3, 2, 3, 2);
            email.Name = "email";
            email.Size = new Size(156, 23);
            email.TabIndex = 2;
            // 
            // change_password
            // 
            change_password.AutoSize = true;
            change_password.Location = new Point(158, 216);
            change_password.Name = "change_password";
            change_password.Size = new Size(133, 15);
            change_password.TabIndex = 3;
            change_password.TabStop = true;
            change_password.Text = "Wachtwoord aanpassen";
            change_password.LinkClicked += change_password_LinkClicked;
            // 
            // grid_size
            // 
            grid_size.Location = new Point(434, 183);
            grid_size.Margin = new Padding(3, 2, 3, 2);
            grid_size.Name = "grid_size";
            grid_size.Size = new Size(156, 23);
            grid_size.TabIndex = 5;
            // 
            // save
            // 
            save.Location = new Point(158, 258);
            save.Margin = new Padding(3, 2, 3, 2);
            save.Name = "save";
            save.Size = new Size(156, 22);
            save.TabIndex = 6;
            save.Text = "Opslaan";
            save.UseVisualStyleBackColor = true;
            save.Click += save_Click;
            // 
            // lblCurrentPassword
            // 
            lblCurrentPassword.AutoSize = true;
            lblCurrentPassword.Location = new Point(158, 240);
            lblCurrentPassword.Name = "lblCurrentPassword";
            lblCurrentPassword.Size = new Size(115, 15);
            lblCurrentPassword.TabIndex = 14;
            lblCurrentPassword.Text = "Huidig wachtwoord:";
            lblCurrentPassword.Visible = false;
            // 
            // txtCurrentPassword
            // 
            txtCurrentPassword.Location = new Point(158, 257);
            txtCurrentPassword.Margin = new Padding(3, 2, 3, 2);
            txtCurrentPassword.Name = "txtCurrentPassword";
            txtCurrentPassword.PasswordChar = '*';
            txtCurrentPassword.Size = new Size(156, 23);
            txtCurrentPassword.TabIndex = 7;
            txtCurrentPassword.Visible = false;
            // 
            // lblNewPassword
            // 
            lblNewPassword.AutoSize = true;
            lblNewPassword.Location = new Point(158, 285);
            lblNewPassword.Name = "lblNewPassword";
            lblNewPassword.Size = new Size(113, 15);
            lblNewPassword.TabIndex = 16;
            lblNewPassword.Text = "Nieuw wachtwoord:";
            lblNewPassword.Visible = false;
            // 
            // txtNewPassword
            // 
            txtNewPassword.Location = new Point(158, 302);
            txtNewPassword.Margin = new Padding(3, 2, 3, 2);
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.PasswordChar = '*';
            txtNewPassword.Size = new Size(156, 23);
            txtNewPassword.TabIndex = 8;
            txtNewPassword.Visible = false;
            // 
            // chkDarkMode
            // 
            // 
            // chkDarkMode
            // 
            chkDarkMode = new CheckBox();
            chkDarkMode.AutoSize = true;
            chkDarkMode.Location = new Point(434, 220);
            chkDarkMode.Name = "chkDarkMode";
            chkDarkMode.Size = new Size(110, 19);
            chkDarkMode.TabIndex = 9;
            chkDarkMode.Text = "Donkere modus";
            chkDarkMode.UseVisualStyleBackColor = true;
            chkDarkMode.CheckedChanged += chkDarkMode_CheckedChanged;

            // Add to form
            Controls.Add(chkDarkMode);
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 338);
            Controls.Add(chkDarkMode);
            Controls.Add(txtNewPassword);
            Controls.Add(lblNewPassword);
            Controls.Add(txtCurrentPassword);
            Controls.Add(lblCurrentPassword);
            Controls.Add(save);
            Controls.Add(grid_size);
            Controls.Add(change_password);
            Controls.Add(email);
            Controls.Add(username);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Settings";
            Text = "Settings";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox username;
        private TextBox email;
        private LinkLabel change_password;
        private TextBox grid_size;
        private Button save;
        private Label lblCurrentPassword;
        private TextBox txtCurrentPassword;
        private Label lblNewPassword;
        private TextBox txtNewPassword;
        private CheckBox chkDarkMode;


    }
}