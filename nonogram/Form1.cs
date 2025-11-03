using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text.Json;

namespace nonogram
{
    public partial class Form1 : Form
    {
        private NonogramLogic logic;
        private Button[,] gridButtons;
        private int[,] solution;
        private int elapsedSeconds = 0;
        private int moveCount = 0;
        private int userId = 1;
        private int currentPuzzleId = -1;
        private SpeedrunManager speedrun;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            logic = new NonogramLogic();
            combomode.Items.Clear();
            combomode.Items.AddRange(new string[] { "Random", "Pre-generated", "Speedrun" });
            combomode.SelectedIndex = 0;
            button1.Enabled = false;
            button3.Enabled = false;
            label2.Text = "Time: 00:00";
            tableLayoutPanel1.Enabled = false;
        }


        private void StartGame()
        {
            int rows = 5;
            int cols = 5;
            solution = logic.GenerateRandomSolution(rows, cols);
            BuildGrid(rows, cols);

            for (int r = 0; r < rows; r++)
            {
                string clue = logic.GenerateClueForLine(logic.GetRow(solution, r));
                SetRowLabel(7 - r, clue);
            }

            for (int c = 0; c < cols; c++)
            {
                string clue = logic.GenerateClueForLine(logic.GetColumn(solution, c));
                SetColumnLabel(8 + c, clue);
            }

            moveCount = 0;
            StartTimer();
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
            moveCount++;

            if (e.Button == MouseButtons.Left)
            {
                btn.BackColor = btn.BackColor == Color.Black ? Color.White : Color.Black;
                btn.Text = "";

                if (CheckWin())
                {
                    // handled inside CheckWin
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

            string selectedMode = combomode.SelectedItem?.ToString() ?? "Random";

            if (selectedMode == "Speedrun")
            {
                OnSpeedrunPuzzleCompleted();
            }
            else
            {
                // Only stop timer for normal and pre-generated modes
                timer1.Stop();

                if (selectedMode == "Pre-generated")
                {
                    SavePreGeneratedScore(userId, currentPuzzleId, elapsedSeconds, moveCount);
                    MessageBox.Show($"Congratulations! You solved the puzzle in {label2.Text.Replace("Time: ", "")}!");
                }
                else
                {
                    MessageBox.Show($"Congratulations! You solved the puzzle in {label2.Text.Replace("Time: ", "")}!");
                }
            }

            return true;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string selectedMode = combomode.SelectedItem?.ToString() ?? "Random";

            int rows = gridButtons.GetLength(0);
            int cols = gridButtons.GetLength(1);

            // Clear all grid cells
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    gridButtons[r, c].BackColor = Color.White;
                    gridButtons[r, c].Text = "";
                }
            }

            moveCount = 0;

            if (selectedMode == "Speedrun")
            {
                // Reset timer to 0 but don't start it
                elapsedSeconds = 0;
                label2.Text = "Time: 00:00";
                timer1.Stop();

                // Reset progress and reload first puzzle
                speedrun.Reset();
                LoadSpeedrunPuzzle();

                tableLayoutPanel1.Enabled = false; // disable board until "Start" pressed
                button2.Enabled = true;            // enable Start button
                button3.Text = "Pause";
                button1.Enabled = false;           // disable Reset until game started
                button3.Enabled = false;           // disable Pause until game started
                return;
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
            if (combomode.SelectedItem == null)
            {
                MessageBox.Show("Please select a mode first!");
                return;
            }

            string selectedMode = combomode.SelectedItem.ToString();

            switch (selectedMode)
            {
                case "Random":
                    StartGame();
                    break;
                case "Pre-generated":
                    StartPreGeneratedGame();
                    break;
                case "Speedrun":
                    StartSpeedrunMode();
                    break;
            }

            tableLayoutPanel1.Enabled = true;
            button2.Enabled = false;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                // Pause
                timer1.Stop();
                tableLayoutPanel1.Enabled = false;
                button3.Text = "Resume";
            }
            else
            {
                // Resume
                timer1.Start();
                tableLayoutPanel1.Enabled = true;
                button3.Text = "Pause";
            }
        }


        private void StartPreGeneratedGame()
        {
            using (var selectionForm = new PuzzleSelectionForm())
            {
                if (selectionForm.ShowDialog() != DialogResult.OK)
                    return; // user canceled

                currentPuzzleId = selectionForm.SelectedPuzzleId;
            }

            using (var conn = DatabaseHelper.GetConnection())
            {
                string query = "SELECT * FROM pre_generated_puzzles WHERE puzzleId = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", currentPuzzleId);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string name = reader.GetString("name");
                        int gridSize = reader.GetInt32("grid_size");

                        string solutionJson = reader.GetString("solution_data");
                        int[][] jagged = JsonSerializer.Deserialize<int[][]>(solutionJson);

                        int rows = jagged.Length;
                        int cols = jagged[0].Length;
                        int[,] grid = new int[rows, cols];
                        for (int r = 0; r < rows; r++)
                            for (int c = 0; c < cols; c++)
                                grid[r, c] = jagged[r][c];

                        solution = grid;
                        label1.Text = name;

                        BuildGrid(gridSize, gridSize);

                        string rowHintsJson = reader.GetString("row_hints");
                        string colHintsJson = reader.GetString("column_hints");

                        int[][] rowHints = JsonSerializer.Deserialize<int[][]>(rowHintsJson);
                        int[][] columnHints = JsonSerializer.Deserialize<int[][]>(colHintsJson);

                        for (int i = 0; i < rowHints.Length && i < 5; i++)
                        {
                            string hintText = rowHints[i].Length > 0 ? string.Join(" ", rowHints[i]) : "0";
                            SetRowLabel(7 - i, hintText);
                        }

                        for (int i = 0; i < columnHints.Length && i < 5; i++)
                        {
                            string hintText = columnHints[i].Length > 0 ? string.Join(" ", columnHints[i]) : "0";
                            SetColumnLabel(8 + i, hintText);
                        }

                        moveCount = 0;
                        StartTimer();
                    }
                }
            }
        }


        private void BuildGrid(int rows, int cols)
        {
            gridButtons = new Button[rows, cols];
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel1.Controls.Clear();

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Button btn = new Button();
                    btn.Dock = DockStyle.Fill;
                    btn.Tag = (r, c);
                    btn.BackColor = Color.White;
                    btn.MouseDown += GridButton_MouseDown;
                    gridButtons[r, c] = btn;
                    tableLayoutPanel1.Controls.Add(btn, c, r);
                }
            }

            tableLayoutPanel1.ResumeLayout();
        }

        private void StartTimer()
        {
            elapsedSeconds = 0;
            label2.Text = "Time: 00:00";
            timer1.Interval = 1000;
            timer1.Start();

            button1.Enabled = true;
            button3.Enabled = true;
        }

        private void SavePreGeneratedScore(int userId, int puzzleId, int time, int moves)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                string query = "INSERT INTO leaderboard_pre_generated (userId, puzzleId, completion_time, moves_count) VALUES (@u, @p, @t, @m) ON DUPLICATE KEY UPDATE completion_time=@t, moves_count=@m";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@u", userId);
                cmd.Parameters.AddWithValue("@p", puzzleId);
                cmd.Parameters.AddWithValue("@t", time);
                cmd.Parameters.AddWithValue("@m", moves);
                cmd.ExecuteNonQuery();
            }
        }

        // --- SPEEDRUN MODE LOGIC BELOW ---

        private void StartSpeedrunMode()
        {
            speedrun = new SpeedrunManager();
            speedrun.LoadSpeedrunPuzzles(5); // Load 5 pre-generated puzzles from DB

            elapsedSeconds = 0;
            label2.Text = "Time: 00:00";
            timer1.Interval = 1000;

            button1.Enabled = true;   // Reset
            button3.Enabled = true;   // Pause/Resume
            button3.Text = "Pause";   // Make sure button says Pause

            timer1.Start();            // Start global timer
            LoadSpeedrunPuzzle();

        }


        private void LoadSpeedrunPuzzle()
        {
            var (puzzleId, grid) = speedrun.GetCurrentPuzzle();
            solution = grid;
            label1.Text = $"Speedrun Puzzle {speedrun.CurrentIndex + 1} of {5}";

            BuildGrid(grid.GetLength(0), grid.GetLength(1));

            for (int r = 0; r < grid.GetLength(0); r++)
                SetRowLabel(7 - r, logic.GenerateClueForLine(logic.GetRow(grid, r)));

            for (int c = 0; c < grid.GetLength(1); c++)
                SetColumnLabel(8 + c, logic.GenerateClueForLine(logic.GetColumn(grid, c)));

            moveCount = 0;
        }



        private void OnSpeedrunPuzzleCompleted()
        {
            int puzzleTime = speedrun.stats.Count == 0
                ? elapsedSeconds
                : elapsedSeconds - speedrun.TotalTime;

            speedrun.AddStats(puzzleTime, moveCount);

            if (speedrun.NextPuzzle())
            {
                MessageBox.Show("Puzzle completed! Loading next...");
                LoadSpeedrunPuzzle();

                // Ensure buttons remain usable and grid active
                tableLayoutPanel1.Enabled = true;
                button1.Enabled = true;   // reset
                button3.Enabled = true;   // pause/resume
            }
            else
            {
                timer1.Stop();
                speedrun.SaveResults(userId);
                MessageBox.Show($"🏁 Speedrun complete!\nTotal time: {elapsedSeconds} seconds\nTotal moves: {speedrun.TotalMoves}");
                ResetGameUI();
            }
        }

        private void btnShowLeaderboard_Click(object sender, EventArgs e)
        {
            LeaderboardForm leaderboardForm = new LeaderboardForm();
            leaderboardForm.ShowDialog();
        }




        private void ResetGameUI()
        {
            tableLayoutPanel1.Enabled = false;
            button2.Enabled = true;
            button1.Enabled = false;
            button3.Enabled = false;
            label1.Text = "Select Mode";
            label2.Text = "Time: 00:00";
        }
    }
}
