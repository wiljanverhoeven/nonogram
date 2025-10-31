using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace nonogram
{
    public class SpeedrunManager
    {
        public int CurrentIndex { get; private set; } = 0;
        private List<int[,]> puzzles = new();
        private List<(int time, int moves)> stats = new();

        public void LoadSpeedrunPuzzles(int count)
        {
            Random rand = new Random();
            NonogramLogic logic = new();
            for (int i = 0; i < count; i++)
                puzzles.Add(logic.GenerateRandomSolution(5, 5));
        }

        public (int id, int[,] grid) GetCurrentPuzzle()
        {
            return (CurrentIndex, puzzles[CurrentIndex]);
        }

        public void AddStats(int time, int moves)
        {
            stats.Add((time, moves));
        }

        public bool NextPuzzle()
        {
            if (CurrentIndex + 1 < puzzles.Count)
            {
                CurrentIndex++;
                return true;
            }
            return false;
        }

        public void SaveResults(int userId)
        {
            using var conn = DatabaseHelper.GetConnection();

            for (int i = 0; i < stats.Count; i++)
            {
                var cmd = new MySqlCommand(
                    "INSERT INTO speedrun_results (user_id, puzzle_index, time_taken, moves) VALUES (@uid, @idx, @time, @moves)",
                    conn);
                cmd.Parameters.AddWithValue("@uid", userId);
                cmd.Parameters.AddWithValue("@idx", i);
                cmd.Parameters.AddWithValue("@time", stats[i].time);
                cmd.Parameters.AddWithValue("@moves", stats[i].moves);
                cmd.ExecuteNonQuery();
            }
        }

        // Add these properties:
        public int TotalMoves
        {
            get
            {
                int total = 0;
                foreach (var s in stats)
                    total += s.moves;
                return total;
            }
        }

        public int TotalTime
        {
            get
            {
                int total = 0;
                foreach (var s in stats)
                    total += s.time;
                return total;
            }
        }
    }

}
