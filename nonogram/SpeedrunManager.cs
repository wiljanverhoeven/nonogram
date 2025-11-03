using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Text.Json;

namespace nonogram
{
    public class SpeedrunManager
    {
        public int CurrentIndex { get; private set; } = 0;
        private readonly List<int[,]> puzzles = new();
        private readonly List<int> puzzleIds = new(); // store IDs from DB
        public readonly List<(int time, int moves)> stats = new();

        // Load 5 pre-generated puzzles from database
        public void LoadSpeedrunPuzzles(int count)
        {
            puzzles.Clear();
            puzzleIds.Clear();
            CurrentIndex = 0;

            using var conn = DatabaseHelper.GetConnection();
            string query = $"SELECT puzzleId, solution_data FROM pre_generated_puzzles ORDER BY RAND() LIMIT {count}";
            using var cmd = new MySqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int puzzleId = reader.GetInt32("puzzleId");
                string solutionJson = reader.GetString("solution_data");
                int[][] jagged = JsonSerializer.Deserialize<int[][]>(solutionJson);

                int rows = jagged.Length;
                int cols = jagged[0].Length;
                int[,] grid = new int[rows, cols];
                for (int r = 0; r < rows; r++)
                    for (int c = 0; c < cols; c++)
                        grid[r, c] = jagged[r][c];

                puzzleIds.Add(puzzleId);
                puzzles.Add(grid);
            }
        }

        //Single clear method — returns both ID and grid
        public (int id, int[,] grid) GetCurrentPuzzle()
        {
            return (puzzleIds[CurrentIndex], puzzles[CurrentIndex]);
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

        //saves results to DB
        public void SaveResults(int userId)
        {
            using var conn = DatabaseHelper.GetConnection();
            string jsonSeeds = JsonSerializer.Serialize(puzzleIds);

            var cmd = new MySqlCommand(
                "INSERT INTO leaderboard_speedrun (userId, total_time, average_time_per_puzzle, total_moves_count, puzzles_seeds, grid_size) " +
                "VALUES (@uid, @time, @avg, @moves, @seeds, 5)",
                conn
            );

            cmd.Parameters.AddWithValue("@uid", userId);
            cmd.Parameters.AddWithValue("@time", TotalTime);
            cmd.Parameters.AddWithValue("@avg", (double)TotalTime / stats.Count);
            cmd.Parameters.AddWithValue("@moves", TotalMoves);
            cmd.Parameters.AddWithValue("@seeds", jsonSeeds);

            cmd.ExecuteNonQuery();
        }

        //resets
        public void Reset()
        {
            CurrentIndex = 0;
            stats.Clear();
        }

        //gets total time and moves
        public int TotalMoves
        {
            get { int total = 0; foreach (var s in stats) total += s.moves; return total; }
        }

        public int TotalTime
        {
            get { int total = 0; foreach (var s in stats) total += s.time; return total; }
        }
    }
}
