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
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            username = new TextBox();
            email = new TextBox();
            change_password = new LinkLabel();
            theme = new TextBox();
            grid_size = new TextBox();
            save = new Button();
            lblCurrentPassword = new Label();
            txtCurrentPassword = new TextBox();
            lblNewPassword = new Label();
            txtNewPassword = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(496, 111);
            label1.Name = "label1";
            label1.Size = new Size(208, 31);
            label1.TabIndex = 0;
            label1.Text = "Game instellingen";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(180, 102);
            label2.Name = "label2";
            label2.Size = new Size(202, 31);
            label2.TabIndex = 1;
            label2.Text = "Profiel aanpassen";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(496, 159);
            label3.Name = "label3";
            label3.Size = new Size(66, 20);
            label3.TabIndex = 2;
            label3.Text = "Thema's:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(180, 159);
            label4.Name = "label4";
            label4.Size = new Size(119, 20);
            label4.TabIndex = 3;
            label4.Text = "Gebruikersnaam:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(180, 221);
            label5.Name = "label5";
            label5.Size = new Size(46, 20);
            label5.TabIndex = 4;
            label5.Text = "Email";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(496, 221);
            label6.Name = "label6";
            label6.Size = new Size(82, 20);
            label6.TabIndex = 5;
            label6.Text = "Grid-grote:";
            // 
            // username
            // 
            username.Location = new Point(180, 182);
            username.Name = "username";
            username.Size = new Size(178, 27);
            username.TabIndex = 1;
            // 
            // email
            // 
            email.Location = new Point(180, 244);
            email.Name = "email";
            email.Size = new Size(178, 27);
            email.TabIndex = 2;
            // 
            // change_password
            // 
            change_password.AutoSize = true;
            change_password.Location = new Point(180, 288);
            change_password.Name = "change_password";
            change_password.Size = new Size(166, 20);
            change_password.TabIndex = 3;
            change_password.TabStop = true;
            change_password.Text = "Wachtwoord aanpassen";
            change_password.LinkClicked += change_password_LinkClicked;
            // 
            // theme
            // 
            theme.Location = new Point(496, 182);
            theme.Name = "theme";
            theme.Size = new Size(178, 27);
            theme.TabIndex = 4;
            // 
            // grid_size
            // 
            grid_size.Location = new Point(496, 244);
            grid_size.Name = "grid_size";
            grid_size.Size = new Size(178, 27);
            grid_size.TabIndex = 5;
            // 
            // save
            // 
            save.Location = new Point(180, 344);
            save.Name = "save";
            save.Size = new Size(178, 29);
            save.TabIndex = 6;
            save.Text = "Opslaan";
            save.UseVisualStyleBackColor = true;
            save.Click += save_Click;
            // 
            // lblCurrentPassword
            // 
            lblCurrentPassword.AutoSize = true;
            lblCurrentPassword.Location = new Point(180, 320);
            lblCurrentPassword.Name = "lblCurrentPassword";
            lblCurrentPassword.Size = new Size(147, 20);
            lblCurrentPassword.TabIndex = 14;
            lblCurrentPassword.Text = "Huidig wachtwoord:";
            lblCurrentPassword.Visible = false;
            // 
            // txtCurrentPassword
            // 
            txtCurrentPassword.Location = new Point(180, 343);
            txtCurrentPassword.Name = "txtCurrentPassword";
            txtCurrentPassword.PasswordChar = '*';
            txtCurrentPassword.Size = new Size(178, 27);
            txtCurrentPassword.TabIndex = 7;
            txtCurrentPassword.Visible = false;
            // 
            // lblNewPassword
            // 
            lblNewPassword.AutoSize = true;
            lblNewPassword.Location = new Point(180, 380);
            lblNewPassword.Name = "lblNewPassword";
            lblNewPassword.Size = new Size(129, 20);
            lblNewPassword.TabIndex = 16;
            lblNewPassword.Text = "Nieuw wachtwoord:";
            lblNewPassword.Visible = false;
            // 
            // txtNewPassword
            // 
            txtNewPassword.Location = new Point(180, 403);
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.PasswordChar = '*';
            txtNewPassword.Size = new Size(178, 27);
            txtNewPassword.TabIndex = 8;
            txtNewPassword.Visible = false;
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtNewPassword);
            Controls.Add(lblNewPassword);
            Controls.Add(txtCurrentPassword);
            Controls.Add(lblCurrentPassword);
            Controls.Add(save);
            Controls.Add(grid_size);
            Controls.Add(theme);
            Controls.Add(change_password);
            Controls.Add(email);
            Controls.Add(username);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Settings";
            Text = "Settings";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox username;
        private TextBox email;
        private LinkLabel change_password;
        private TextBox theme;
        private TextBox grid_size;
        private Button save;
        private Label lblCurrentPassword;
        private TextBox txtCurrentPassword;
        private Label lblNewPassword;
        private TextBox txtNewPassword;
    }
}