using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using MySql.Data.MySqlClient;

namespace nonogram
{
    public class SpeedrunManager
    {
        public List<(int puzzleId, int[,] grid)> Puzzles { get; private set; } = new();
        public int CurrentIndex { get; private set; } = 0;
        public int TotalTime { get; private set; } = 0;
        public int TotalMoves { get; private set; } = 0;
        public bool IsComplete => CurrentIndex >= Puzzles.Count;

        public void LoadSpeedrunPuzzles(int count = 3)
        {
            Puzzles.Clear();
            using (var conn = DatabaseHelper.GetConnection())
            {
                string query = $"SELECT puzzleId, solution_data FROM pre_generated_puzzles ORDER BY RAND() LIMIT {count}";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("puzzleId");
                        int[,] grid = JsonSerializer.Deserialize<int[,]>(reader.GetString("solution_data"));
                        Puzzles.Add((id, grid));
                    }
                }
            }
        }

        public (int puzzleId, int[,] grid) GetCurrentPuzzle() => Puzzles[CurrentIndex];

        public bool NextPuzzle()
        {
            CurrentIndex++;
            return !IsComplete;
        }

        public void AddStats(int time, int moves)
        {
            TotalTime += time;
            TotalMoves += moves;
        }

        public void SaveResults(int userId)
        {
            if (Puzzles.Count == 0) return;

            using (var conn = DatabaseHelper.GetConnection())
            {
                string query = @"INSERT INTO leaderboard_speedrun 
                    (userId, total_time, average_time_per_puzzle, total_moves_count, puzzles_seeds, grid_size)
                    VALUES (@u, @t, @avg, @m, @p, 5)";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@u", userId);
                cmd.Parameters.AddWithValue("@t", TotalTime);
                cmd.Parameters.AddWithValue("@avg", TotalTime / (double)Puzzles.Count);
                cmd.Parameters.AddWithValue("@m", TotalMoves);
                cmd.Parameters.AddWithValue("@p", JsonSerializer.Serialize(Puzzles.Select(p => p.puzzleId)));
                cmd.ExecuteNonQuery();
            }
        }
    }
}
