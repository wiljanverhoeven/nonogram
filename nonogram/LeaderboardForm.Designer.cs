namespace nonogram
{
    partial class LeaderboardForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridViewPreGenerated = new DataGridView();
            dataGridViewSpeedrun = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPreGenerated).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewSpeedrun).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewPreGenerated
            // 
            dataGridViewPreGenerated.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewPreGenerated.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewPreGenerated.Location = new Point(30, 30);
            dataGridViewPreGenerated.Name = "dataGridViewPreGenerated";
            dataGridViewPreGenerated.Size = new Size(240, 150);
            dataGridViewPreGenerated.TabIndex = 0;
            // 
            // dataGridViewSpeedrun
            // 
            dataGridViewSpeedrun.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewSpeedrun.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewSpeedrun.Location = new Point(300, 30);
            dataGridViewSpeedrun.Name = "dataGridViewSpeedrun";
            dataGridViewSpeedrun.Size = new Size(240, 150);
            dataGridViewSpeedrun.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(90, 9);
            label1.Name = "label1";
            label1.Size = new Size(118, 15);
            label1.TabIndex = 2;
            label1.Text = "pregenerated puzzles";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(395, 9);
            label2.Name = "label2";
            label2.Size = new Size(56, 15);
            label2.TabIndex = 3;
            label2.Text = "speedrun";
            label2.Click += label2_Click;
            // 
            // LeaderboardForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dataGridViewSpeedrun);
            Controls.Add(dataGridViewPreGenerated);
            Name = "LeaderboardForm";
            Text = "LeaderboardForm";
            ((System.ComponentModel.ISupportInitialize)dataGridViewPreGenerated).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewSpeedrun).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewPreGenerated;
        private DataGridView dataGridViewSpeedrun;
        private Label label1;
        private Label label2;
    }
}