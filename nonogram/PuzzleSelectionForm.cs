using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace nonogram
{
    public partial class PuzzleSelectionForm : Form
    {
        public int SelectedPuzzleId { get; private set; } = -1;

        public PuzzleSelectionForm()
        {
            InitializeComponent();
        }

        private void PuzzleSelectionForm_Load(object sender, EventArgs e)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                string query = "SELECT puzzleId, name, grid_size FROM pre_generated_puzzles";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("puzzleId");
                        string name = reader.GetString("name");
                        int size = reader.GetInt32("grid_size");
                        listBox1.Items.Add(new PuzzleItem(id, $"{name} ({size}x{size})"));
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            AppTheme.ApplyTheme(this);
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.ButtonSelect = new System.Windows.Forms.Button();

            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Size = new System.Drawing.Size(360, 200);
            this.listBox1.TabIndex = 0;

            // 
            // ButtonSelect
            // 
            this.ButtonSelect.Location = new System.Drawing.Point(12, 225);
            this.ButtonSelect.Size = new System.Drawing.Size(360, 30);
            this.ButtonSelect.TabIndex = 1;
            this.ButtonSelect.Text = "Select Puzzle";
            this.ButtonSelect.UseVisualStyleBackColor = true;
            this.ButtonSelect.Click += new System.EventHandler(this.buttonSelect_Click);

            // 
            // PuzzleSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 271);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.ButtonSelect);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select a Puzzle";
            this.Load += new System.EventHandler(this.PuzzleSelectionForm_Load);
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is PuzzleItem item)
            {
                SelectedPuzzleId = item.Id;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Please select a puzzle.");
            }
        }

        private class PuzzleItem
        {
            public int Id { get; }
            public string Display { get; }

            public PuzzleItem(int id, string display)
            {
                Id = id;
                Display = display;
            }

            public override string ToString() => Display;
        }
        private ListBox listBox1;
        private Button ButtonSelect;
    }
}
