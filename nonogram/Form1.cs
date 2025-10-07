namespace nonogram
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private Button[,] gridButtons;
        private int[,] solution;
        private int elapsedSeconds = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Enabled = false; // Reset
            button3.Enabled = false; // Pause
            label2.Text = "Time: 00:00";
            tableLayoutPanel1.Enabled = false;
        }

        private void StartGame()
        {
            int rows = 5;
            int cols = 5;

            solution = GenerateRandomSolution(rows, cols);

            gridButtons = new Button[rows, cols]; // Initialize gridButtons array

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

            // Update row clues (labels 3 to 7, from bottom to top)
            // So label3 is bottom row, label7 is top row
            for (int r = 0; r < rows; r++)
            {
                string clue = GenerateClueForLine(GetRow(solution, r));
                // labels 3-7 bottom to top => label3 for row 4, label4 for row 3, ..., label7 for row 0
                int labelIndex = 7 - r;
                SetRowLabel(labelIndex, clue);
            }

            // Update column clues (labels 8 to 12, left to right)
            for (int c = 0; c < cols; c++)
            {
                string clue = GenerateClueForLine(GetColumn(solution, c));
                SetColumnLabel(8 + c, clue);
            }

            elapsedSeconds = 0;
            label2.Text = "Time: 00:00";
            timer1.Interval = 1000;
            timer1.Start();

            button1.Enabled = true; // Reset
            button3.Enabled = true; // Pause
        }

        private int[,] GenerateRandomSolution(int rows, int cols, double fillProbability = 0.3)
            {
                Random rand = new Random();
                int[,] grid = new int[rows, cols];

                // Step 1: Random fill based on fillProbability
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        grid[r, c] = (rand.NextDouble() < fillProbability) ? 1 : 0;
                    }
                }

                // Step 2: Ensure each row has at least one '1'
                for (int r = 0; r < rows; r++)
                {
                    bool hasOne = false;
                    for (int c = 0; c < cols; c++)
                    {
                        if (grid[r, c] == 1)
                        {
                            hasOne = true;
                            break;
                        }
                    }
                    if (!hasOne)
                    {
                        // Pick random column and set to 1
                        int colIndex = rand.Next(cols);
                        grid[r, colIndex] = 1;
                    }
                }

                // Step 3: Ensure each column has at least one '1'
                for (int c = 0; c < cols; c++)
                {
                    bool hasOne = false;
                    for (int r = 0; r < rows; r++)
                    {
                        if (grid[r, c] == 1)
                        {
                            hasOne = true;
                            break;
                        }
                    }
                    if (!hasOne)
                    {
                        // Pick random row and set to 1
                        int rowIndex = rand.Next(rows);
                        grid[rowIndex, c] = 1;
                    }
                }

                return grid;
            }


        // Extract a row as an int array
        private int[] GetRow(int[,] grid, int row)
        {
            int cols = grid.GetLength(1);
            int[] result = new int[cols];
            for (int c = 0; c < cols; c++)
            {
                result[c] = grid[row, c];
            }
            return result;
        }

        // Extract a column as an int array
        private int[] GetColumn(int[,] grid, int col)
        {
            int rows = grid.GetLength(0);
            int[] result = new int[rows];
            for (int r = 0; r < rows; r++)
            {
                result[r] = grid[r, col];
            }
            return result;
        }

        // Generate clues string for a line of 0/1 values
        private string GenerateClueForLine(int[] line)
        {
            List<int> clues = new List<int>();
            int count = 0;

            foreach (var cell in line)
            {
                if (cell == 1)
                {
                    count++;
                }
                else
                {
                    if (count > 0)
                    {
                        clues.Add(count);
                        count = 0;
                    }
                }
            }
            if (count > 0) clues.Add(count);

            if (clues.Count == 0) return "0"; // no filled cells, show 0 or blank

            return string.Join("-", clues);
        }

        // Set the row clue label based on label index (3 to 7)
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

        // Set the column clue label based on label index (8 to 12)
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

            // Puzzle is solved
            timer1.Stop(); // stop the timer
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
                    gridButtons[r, c].Enabled = false; // disable until start
                }
            }

            elapsedSeconds = 0;
            label2.Text = "Time: 00:00";
            timer1.Stop();

            tableLayoutPanel1.Enabled = false;
            button2.Enabled = true;  // re-enable start button
            button3.Text = "Pause";  // reset pause button text
            button1.Enabled = false;
            button3.Enabled = false;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            StartGame();
            tableLayoutPanel1.Enabled = true; // now enable the puzzle
            button2.Enabled = false; // prevent multiple starts
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                // Timer is running → pause it
                timer1.Stop();
                button3.Text = "Resume";
            }
            else
            {
                // Timer is stopped → resume it
                timer1.Start();
                button3.Text = "Pause";
            }
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }



        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
