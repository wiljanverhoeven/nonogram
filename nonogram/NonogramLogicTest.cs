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

            for (int r = 0; r < 5; r++)
                Assert.Contains(1, _logic.GetRow(grid, r));

            for (int c = 0; c < 5; c++)
                Assert.Contains(1, _logic.GetColumn(grid, c));
        }

        [Fact]
        public void GetRow_ReturnsCorrectRow()
        {
            int[,] grid = {
                {1,0,1},
                {0,1,0},
                {1,1,1}
            };
            Assert.Equal(new[] { 0, 1, 0 }, _logic.GetRow(grid, 1));
        }

        [Fact]
        public void GetColumn_ReturnsCorrectColumn()
        {
            int[,] grid = {
                {1,0,1},
                {0,1,0},
                {1,1,1}
            };
            Assert.Equal(new[] { 1, 0, 1 }, _logic.GetColumn(grid, 2));
        }

        [Fact]
        public void GenerateClueForLine_ReturnsCorrectClues()
        {
            int[] line = { 1, 1, 0, 1, 0, 1, 1, 1 };
            Assert.Equal("2-1-3", _logic.GenerateClueForLine(line));
        }

        [Fact]
        public void GenerateClueForLine_ReturnsZero_WhenNoFilledCells()
        {
            int[] line = { 0, 0, 0, 0, 0 };
            Assert.Equal("0", _logic.GenerateClueForLine(line));
        }
    }
}
