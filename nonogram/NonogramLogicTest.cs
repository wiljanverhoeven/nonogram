using Xunit;
using nonogram;


namespace nonogram.Tests
{
    public class NonogramLogicTests
    {
        private readonly NonogramLogic _logic = new NonogramLogic();

        [Fact]
        public void GenerateRandomSolution_HasAtLeastOneFilledInEachRowAndColumn()
        {
            int[,] grid = _logic.GenerateRandomSolution(5, 5);

            // Each row has at least one filled
            for (int r = 0; r < 5; r++)
            {
                int[] row = _logic.GetRow(grid, r);
                Assert.Contains(1, row);
            }

            // Each column has at least one filled
            for (int c = 0; c < 5; c++)
            {
                int[] col = _logic.GetColumn(grid, c);
                Assert.Contains(1, col);
            }
        }

        [Fact]
        public void GenerateClueForLine_ReturnsCorrectClues()
        {
            int[] line = { 1, 1, 0, 1, 0, 1, 1, 1 };
            string result = _logic.GenerateClueForLine(line);
            Assert.Equal("2-1-3", result);
        }

        [Fact]
        public void GenerateClueForLine_ReturnsZero_WhenNoFilledCells()
        {
            int[] line = { 0, 0, 0, 0, 0 };
            string result = _logic.GenerateClueForLine(line);
            Assert.Equal("0", result);
        }
    }
}
