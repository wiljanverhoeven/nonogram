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
            ((System.ComponentModel.ISupportInitialize)dataGridViewPreGenerated).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewSpeedrun).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewPreGenerated
            // 
            dataGridViewPreGenerated.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewPreGenerated.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewPreGenerated.Location = new Point(55, 80);
            dataGridViewPreGenerated.Name = "dataGridViewPreGenerated";
            dataGridViewPreGenerated.Size = new Size(240, 150);
            dataGridViewPreGenerated.TabIndex = 0;
            // 
            // dataGridViewSpeedrun
            // 
            dataGridViewSpeedrun.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewSpeedrun.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewSpeedrun.Location = new Point(444, 80);
            dataGridViewSpeedrun.Name = "dataGridViewSpeedrun";
            dataGridViewSpeedrun.Size = new Size(240, 150);
            dataGridViewSpeedrun.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(142, 39);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 2;
            label1.Text = "label1";
            label1.Click += label1_Click;
            // 
            // LeaderboardForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(800, 450);
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
    }
}