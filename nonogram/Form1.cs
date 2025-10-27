using System;
using System.Drawing;
using System.Windows.Forms;

namespace nonogram
{
    public partial class Form1 : Form
    {
        private NonogramLogic logic;         // Instance of your logic class
        private Button[,] gridButtons;       // Button grid for the puzzle
        private int[,] solution;             // Holds the puzzle solution
        private int elapsedSeconds = 0;      // Timer counter

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            logic = new NonogramLogic();     // Initialize logic when form loads

            button1.Enabled = false; // Reset
            button3.Enabled = false; // Pause
            label2.Text = "Time: 00:00";
            tableLayoutPanel1.Enabled = false;
        }

        private void StartGame()
        {
            int rows = 5;
            int cols = 5;

            // Use the logic class for generation
            solution = logic.GenerateRandomSolution(rows, cols);

            gridButtons = new Button[rows, cols];

            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.RowCount = rows;
            tableLayoutPanel1.ColumnCount = cols;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Button btn = new Button();
                    btn.Dock = DockStyle.Fill;
                    btn.Tag = (r, c);
                    btn.BackColor = Color.White;
                    btn.MouseDown += GridButton_MouseDown;
                    btn.Enabled = true;

                    gridButtons[r, c] = btn;
                    tableLayoutPanel1.Controls.Add(btn, c, r);
                }
            }

            tableLayoutPanel1.ResumeLayout();

            // Row clues (labels 3 to 7, bottom to top)
            for (int r = 0; r < rows; r++)
            {
                string clue = logic.GenerateClueForLine(logic.GetRow(solution, r));
                int labelIndex = 7 - r;
                SetRowLabel(labelIndex, clue);
            }

            // Column clues (labels 8 to 12, left to right)
            for (int c = 0; c < cols; c++)
            {
                string clue = logic.GenerateClueForLine(logic.GetColumn(solution, c));
                SetColumnLabel(8 + c, clue);
            }

            elapsedSeconds = 0;
            label2.Text = "Time: 00:00";
            timer1.Interval = 1000;
            timer1.Start();

            button1.Enabled = true; // Reset
            button3.Enabled = true; // Pause
        }

        private void SetRowLabel(int labelNumber, string text)
        {
            switch (labelNumber)
            {
                case 3: label3.Text = text; break;
                case 4: label4.Text = text; break;
                case 5: label5.Text = text; break;
                case 6: label6.Text = text; break;
                case 7: label7.Text = text; break;
            }
        }

        private void SetColumnLabel(int labelNumber, string text)
        {
            switch (labelNumber)
            {
                case 8: label8.Text = text; break;
                case 9: label9.Text = text; break;
                case 10: label10.Text = text; break;
                case 11: label11.Text = text; break;
                case 12: label12.Text = text; break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            elapsedSeconds++;
            TimeSpan time = TimeSpan.FromSeconds(elapsedSeconds);
            label2.Text = $"Time: {time:mm\\:ss}";
        }

        private void GridButton_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;

            if (e.Button == MouseButtons.Left)
            {
                btn.BackColor = btn.BackColor == Color.Black ? Color.White : Color.Black;
                btn.Text = "";

                if (CheckWin())
                {
                    MessageBox.Show($"Congratulations! You solved the puzzle in {label2.Text.Replace("Time: ", "")}!");
                }
            }
        }

        private bool CheckWin()
        {
            int rows = solution.GetLength(0);
            int cols = solution.GetLength(1);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    bool isFilled = gridButtons[r, c].BackColor == Color.Black;
                    if (isFilled != (solution[r, c] == 1))
                        return false;
                }
            }

            timer1.Stop();
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int rows = gridButtons.GetLength(0);
            int cols = gridButtons.GetLength(1);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    gridButtons[r, c].BackColor = Color.White;
                    gridButtons[r, c].Text = "";
                    gridButtons[r, c].Enabled = false;
                }
            }

            elapsedSeconds = 0;
            label2.Text = "Time: 00:00";
            timer1.Stop();

            tableLayoutPanel1.Enabled = false;
            button2.Enabled = true;
            button3.Text = "Pause";
            button1.Enabled = false;
            button3.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StartGame();
            tableLayoutPanel1.Enabled = true;
            button2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
                button3.Text = "Resume";
            }
            else
            {
                timer1.Start();
                button3.Text = "Pause";
            }
        }

        // You can leave the other empty label click methods as-is
    }
}
