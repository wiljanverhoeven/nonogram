namespace nonogram
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        /// 

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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            timer1 = new System.Windows.Forms.Timer(components);
            tableLayoutPanel1 = new TableLayoutPanel();
            panel13 = new Panel();
            button2 = new Button();
            label2 = new Label();
            label1 = new Label();
            button3 = new Button();
            button1 = new Button();
            numericGridSize = new NumericUpDown();
            panelColClues = new Panel();
            panelRowClues = new Panel();
            panelMainLayout = new TableLayoutPanel();
            panel13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericGridSize).BeginInit();
            SuspendLayout();
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanel1.Location = new Point(63, 63);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tableLayoutPanel1.Size = new Size(406, 406);
            tableLayoutPanel1.TabIndex = 0;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
           
            // 
            // panel13
            // 
            panel13.BackColor = SystemColors.ActiveCaption;
            panel13.BorderStyle = BorderStyle.FixedSingle;
            panel13.Controls.Add(button2);
            panel13.Controls.Add(label2);
            panel13.Controls.Add(label1);
            panel13.Controls.Add(button3);
            panel13.Controls.Add(button1);
            panel13.Controls.Add(numericGridSize);
            panel13.Location = new Point(606, 62);
            panel13.Name = "panel13";
            panel13.Size = new Size(158, 340);
            panel13.TabIndex = 13;
            // 
            // button2
            // 
            button2.Location = new Point(32, 107);
            button2.Name = "button2";
            button2.Size = new Size(92, 27);
            button2.TabIndex = 5;
            button2.Text = "Start";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(58, 70);
            label2.Name = "label2";
            label2.Size = new Size(38, 15);
            label2.TabIndex = 4;
            label2.Text = "label2";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(17, 23);
            label1.Name = "label1";
            label1.Size = new Size(125, 30);
            label1.TabIndex = 3;
            label1.Text = "puzzelname";
            label1.Click += label1_Click_1;
            // 
            // button3
            // 
            button3.Location = new Point(32, 176);
            button3.Name = "button3";
            button3.Size = new Size(92, 30);
            button3.TabIndex = 2;
            button3.Text = "Pauzeer";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button1
            // 
            button1.Location = new Point(32, 140);
            button1.Name = "button1";
            button1.Size = new Size(92, 30);
            button1.TabIndex = 0;
            button1.Text = "Reset";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // numericGridSize
            // 
            numericGridSize.Location = new Point(32, 220);
            numericGridSize.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            numericGridSize.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            numericGridSize.Name = "numericGridSize";
            numericGridSize.Size = new Size(120, 23);
            numericGridSize.TabIndex = 6;
            numericGridSize.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // panelColClues
            // 
            panelColClues.Location = new Point(63, 3);
            panelColClues.Name = "panelColClues";
            panelColClues.Size = new Size(134, 49);
            panelColClues.TabIndex = 14;
            // 
            // panelRowClues
            // 
            panelRowClues.Location = new Point(3, 63);
            panelRowClues.Name = "panelRowClues";
            panelRowClues.Size = new Size(54, 134);
            panelRowClues.TabIndex = 15;
            // panelMainLayout
            panelMainLayout = new TableLayoutPanel();
            panelMainLayout.ColumnCount = 2;
            panelMainLayout.RowCount = 2;
            panelMainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F)); // left clues
            panelMainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F)); // grid + top clues
            panelMainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F)); // top clues
            panelMainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // grid
            panelMainLayout.Location = new Point(70, 20);
            panelMainLayout.Size = new Size(480, 480);
            panelMainLayout.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
            panelMainLayout.Dock = DockStyle.None;

            // add subpanels
            panelMainLayout.Controls.Add(new Panel(), 0, 0); // empty top-left
            panelMainLayout.Controls.Add(panelColClues, 1, 0); // top clues
            panelMainLayout.Controls.Add(panelRowClues, 0, 1); // left clues
            panelMainLayout.Controls.Add(tableLayoutPanel1, 1, 1); // main grid
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelMainLayout);
            Controls.Add(panel13);
            Cursor = Cursors.Cross;
            Name = "Form1";
            Text = "nonogram";
            Load += Form1_Load;
            panel13.ResumeLayout(false);
            panel13.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericGridSize).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel13;
        private Button button3;
        private Button button1;
        private Label label1;
        private Label label2;
        private Button button2;
        private NumericUpDown numericGridSize;
        private Panel panelColClues;
        private Panel panelRowClues;
        private TableLayoutPanel panelMainLayout;
    }

}
