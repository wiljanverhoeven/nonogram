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
            int rows = 5;
            int cols = 5;

            solution = new int[,]
            {
            {1,0,1,0,0},
            {1,1,1,0,0},
            {0,0,1,0,1},
            {0,1,1,1,0},
            {1,0,0,1,1}
            };

            gridButtons = new Button[rows, cols];

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

                    gridButtons[r, c] = btn;
                    tableLayoutPanel1.Controls.Add(btn, c, r);
                }
            }

            // Timer setup
            elapsedSeconds = 0;
            label2.Text = "Time: 00:00";
            timer1.Interval = 1000; // 1 second
            timer1.Tick += Timer1_Tick;
            timer1.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
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
            timer1.Stop(); // ?? stop the timer
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
            {
                int rows = gridButtons.GetLength(0);
                int cols = gridButtons.GetLength(1);

                // Reset all buttons to white and clear text
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        gridButtons[r, c].BackColor = Color.White;
                        gridButtons[r, c].Text = "";
                    }
                }

                // Reset and restart the timer
                elapsedSeconds = 0;
                label2.Text = "Time: 00:00";
                timer1.Stop();    // Stop in case it was paused
                timer1.Start();   // Start the timer fresh
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                // Timer is running ? pause it
                timer1.Stop();
                button3.Text = "Resume";
            }
            else
            {
                // Timer is stopped ? resume it
                timer1.Start();
                button3.Text = "Pause";
            }
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

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
