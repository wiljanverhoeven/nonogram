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
                        btn.Tag = (r, c); // store position
                        btn.BackColor = Color.White;
                        btn.MouseDown += GridButton_MouseDown;

                    gridButtons[r, c] = btn;
                        tableLayoutPanel1.Controls.Add(btn, c, r);
                    }
                }
        }

        private void GridButton_MouseDown(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;

            if (e.Button == MouseButtons.Left)
            {
                // toggle black fill
                btn.BackColor = btn.BackColor == Color.Black ? Color.White : Color.Black;
                btn.Text = "";
            }

            // Check if the puzzle is solved
            if (CheckWin())
            {
                MessageBox.Show("Congratulations! You solved the puzzle!");
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
                    // 1 in solution = black, 0 = white
                    bool isBlack = gridButtons[r, c].BackColor == Color.Black;
                    if ((solution[r, c] == 1 && !isBlack) || (solution[r, c] == 0 && isBlack))
                    {
                        return false; // mismatch found
                    }
                }
            }
            return true; // all match
        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
