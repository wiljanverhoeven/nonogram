using System;
using System.Windows.Forms;

namespace nonogram
{
    public partial class LeaderboardForm : Form
    {
        public LeaderboardForm()
        {
            InitializeComponent();
            LoadLeaderboards();
        }

        private void LoadLeaderboards()
        {
            var leaderboardManager = new LeaderboardManager();

            // Pre-generated leaderboard
            var preGenList = leaderboardManager.GetPreGeneratedLeaderboard(10);
            dataGridViewPreGenerated.DataSource = preGenList;

            // Speedrun leaderboard
            var speedrunList = leaderboardManager.GetSpeedrunLeaderboard(10);
            dataGridViewSpeedrun.DataSource = speedrunList;

            dataGridViewPreGenerated.Columns["Username"].HeaderText = "Player";
            dataGridViewPreGenerated.Columns["PuzzleName"].HeaderText = "Puzzle";
            dataGridViewPreGenerated.Columns["GridSize"].HeaderText = "Size";
            dataGridViewPreGenerated.Columns["CompletionTime"].HeaderText = "Time (s)";
            dataGridViewPreGenerated.Columns["MovesCount"].HeaderText = "Moves";

            dataGridViewSpeedrun.Columns["Username"].HeaderText = "Player";
            dataGridViewSpeedrun.Columns["TotalTime"].HeaderText = "Total Time (s)";
            dataGridViewSpeedrun.Columns["AvgTimePerPuzzle"].HeaderText = "Avg Time/Puzzle";
            dataGridViewSpeedrun.Columns["TotalMoves"].HeaderText = "Total Moves";
            dataGridViewSpeedrun.Columns["GridSize"].HeaderText = "Grid Size";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
