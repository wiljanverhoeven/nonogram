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
            int rows = (int)numericGridSize.Value;
            int cols = (int)numericGridSize.Value;

            solution = GenerateRandomSolution(rows, cols);

            gridButtons = new Button[rows, cols]; // Initialize gridButtons array

            tableLayoutPanel1.SuspendLayout();

            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.RowCount = rows;
            tableLayoutPanel1.ColumnCount = cols;

            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();

            for (int i = 0; i < cols; i++)
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / cols));

            for (int i = 0; i < rows; i++)
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / rows));

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Button btn = new Button();
                    panelRowClues.Dock = DockStyle.Fill;
                    panelColClues.Dock = DockStyle.Fill;
                    tableLayoutPanel1.Dock = DockStyle.Fill;
                    btn.Tag = (r, c);
                    btn.BackColor = Color.White;
                    btn.MouseDown += GridButton_MouseDown;
                    btn.Enabled = true;

                    gridButtons[r, c] = btn;
                    tableLayoutPanel1.Controls.Add(btn, c, r);
                }
            }

            panelRowClues.Controls.Clear();
            panelColClues.Controls.Clear();

            // Row clues (left side)
            for (int r = 0; r < rows; r++)
            {
                string clue = GenerateClueForLine(GetRow(solution, r));
                Label lbl = new Label
                {
                    Text = clue,
                    AutoSize = true,
                    Location = new Point(0, r * (tableLayoutPanel1.Height / rows))
                };
                panelRowClues.Controls.Add(lbl);
            }

            // Column clues (top)
            for (int c = 0; c < cols; c++)
            {
                string clue = GenerateClueForLine(GetColumn(solution, c));
                Label lbl = new Label
                {
                    Text = clue,
                    AutoSize = true,
                    Location = new Point(c * (tableLayoutPanel1.Width / cols), 0)
                };
                panelColClues.Controls.Add(lbl);
            }

            tableLayoutPanel1.ResumeLayout();


           

            elapsedSeconds = 0;
            label2.Text = "Time: 00:00";
            timer1.Interval = 1000;
            timer1.Start();

            button1.Enabled = true; // Reset
            button3.Enabled = true; // Pause

            // After tableLayoutPanel1.ResumeLayout();
            int gridSize = (int)numericGridSize.Value;
            int cellSize = Math.Min(400 / gridSize, 80); // adjust if needed for your layout
            int totalGridSize = cellSize * gridSize;

            // Resize grid dynamically
            tableLayoutPanel1.Size = new Size(totalGridSize, totalGridSize);

            // Reposition and resize clue panels accordingly
            panelRowClues.Size = new Size(60, totalGridSize);
            panelRowClues.Location = new Point(tableLayoutPanel1.Left - 65, tableLayoutPanel1.Top);

            panelColClues.Size = new Size(totalGridSize, 60);
            panelColClues.Location = new Point(tableLayoutPanel1.Left, tableLayoutPanel1.Top - 65);
        }

        private int[,] GenerateRandomSolution(int rows, int cols)
        {
            Random rand = new Random();
            int[,] grid = new int[rows, cols];

            // Maximum possible run length increases with grid size
            int maxRunLength = Math.Max(2, rows / 3); // e.g. 3 for 9x9, 6 for 18x18, etc.

            for (int r = 0; r < rows; r++)
            {
                int filled = 0;
                while (filled < cols * 0.5) // roughly half the row filled
                {
                    // Pick a random run length between 1 and maxRunLength
                    int runLength = rand.Next(1, maxRunLength + 1);

                    // Pick a starting column (make sure it fits)
                    int start = rand.Next(0, cols - runLength);

                    // Fill that run
                    for (int c = start; c < start + runLength; c++)
                        grid[r, c] = 1;

                    // Skip a random gap before starting next run
                    filled += runLength + rand.Next(1, 3);
                }
            }

            // Make sure each column has at least one 1
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
