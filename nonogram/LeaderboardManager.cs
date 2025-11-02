using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace nonogram
{
    public class LeaderboardManager
    {
        // Pre-generated puzzle leaderboard entry
        public class PreGeneratedLeaderboardEntry
        {
            public string Username { get; set; }
            public string PuzzleName { get; set; }
            public int GridSize { get; set; }
            public int CompletionTime { get; set; } // seconds
            public int MovesCount { get; set; }
        }

        // Speedrun leaderboard entry
        public class SpeedrunLeaderboardEntry
        {
            public string Username { get; set; }
            public int TotalTime { get; set; } // seconds
            public decimal AvgTimePerPuzzle { get; set; } // seconds
            public int TotalMoves { get; set; }
            public int GridSize { get; set; }
        }

        // ✅ Pre-generated leaderboard
        public List<PreGeneratedLeaderboardEntry> GetPreGeneratedLeaderboard(int top = 10)
        {
            var list = new List<PreGeneratedLeaderboardEntry>();
            using var conn = DatabaseHelper.GetConnection();
            string query = @"
                SELECT u.username, p.name AS puzzleName, p.grid_size, l.completion_time, l.moves_count
                FROM leaderboard_pre_generated l
                JOIN user u ON l.userId = u.userId
                JOIN pre_generated_puzzles p ON l.puzzleId = p.puzzleId
                ORDER BY l.completion_time ASC
                LIMIT @top
            ";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@top", top);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new PreGeneratedLeaderboardEntry
                {
                    Username = reader.GetString("username"),
                    PuzzleName = reader.GetString("puzzleName"),
                    GridSize = reader.GetInt32("grid_size"),
                    CompletionTime = reader.GetInt32("completion_time"),
                    MovesCount = reader.GetInt32("moves_count")
                });
            }
            return list;
        }

        // ✅ Speedrun leaderboard
        public List<SpeedrunLeaderboardEntry> GetSpeedrunLeaderboard(int top = 10)
        {
            var list = new List<SpeedrunLeaderboardEntry>();
            using var conn = DatabaseHelper.GetConnection();
            string query = @"
                SELECT u.username, s.total_time, s.average_time_per_puzzle, s.total_moves_count, s.grid_size
                FROM leaderboard_speedrun s
                JOIN user u ON s.userId = u.userId
                ORDER BY s.total_time ASC
                LIMIT @top
            ";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@top", top);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new SpeedrunLeaderboardEntry
                {
                    Username = reader.GetString("username"),
                    TotalTime = reader.GetInt32("total_time"),
                    AvgTimePerPuzzle = reader.GetDecimal("average_time_per_puzzle"),
                    TotalMoves = reader.GetInt32("total_moves_count"),
                    GridSize = reader.GetInt32("grid_size")
                });
            }
            return list;
        }
    }
}
