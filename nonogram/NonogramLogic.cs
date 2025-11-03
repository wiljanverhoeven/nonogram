using System;
using System.Collections.Generic;

namespace nonogram
{
    public class NonogramLogic
    {
        private Random rand = new Random();

        public int[,] GenerateRandomSolution(int rows, int cols, double fillProbability = 0.3)
        {
            int[,] grid = new int[rows, cols];
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    grid[r, c] = (rand.NextDouble() < fillProbability) ? 1 : 0;

            // Ensure every row and column has at least one filled cell
            for (int r = 0; r < rows; r++)
                if (!RowHasOne(grid, r)) grid[r, rand.Next(cols)] = 1;

            for (int c = 0; c < cols; c++)
                if (!ColumnHasOne(grid, c)) grid[rand.Next(rows), c] = 1;

            return grid;
        }

        private bool RowHasOne(int[,] grid, int row)
        {
            for (int c = 0; c < grid.GetLength(1); c++)
                if (grid[row, c] == 1) return true;
            return false;
        }

        private bool ColumnHasOne(int[,] grid, int col)
        {
            for (int r = 0; r < grid.GetLength(0); r++)
                if (grid[r, col] == 1) return true;
            return false;
        }

        public string GenerateClueForLine(int[] line)
        {
            List<int> clues = new();
            int count = 0;
            foreach (int cell in line)
            {
                if (cell == 1) count++;
                else if (count > 0) { clues.Add(count); count = 0; }
            }
            if (count > 0) clues.Add(count);
            return clues.Count == 0 ? "0" : string.Join("-", clues);
        }

        public int[] GetRow(int[,] grid, int row)
        {
            int cols = grid.GetLength(1);
            int[] result = new int[cols];
            for (int c = 0; c < cols; c++) result[c] = grid[row, c];
            return result;
        }

        public int[] GetColumn(int[,] grid, int col)
        {
            int rows = grid.GetLength(0);
            int[] result = new int[rows];
            for (int r = 0; r < rows; r++) result[r] = grid[r, col];
            return result;
        }
    }
}
