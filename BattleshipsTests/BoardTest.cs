using Battleships;
using Battleships.ShipConstraintsConfiguration;
using Xunit;

namespace BattleshipsTests
{
    public class BoardTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        public void BoardFieldsPropHasCorrectSize(int size)
        {
            var shipConstraints = new ShipConstraints();
            var board = new Board(size, shipConstraints);
            var fields = board.Fields;
            const int NO_OF_BOARD_DIMENSIONS = 2;
            Assert.Equal(NO_OF_BOARD_DIMENSIONS, fields.Rank);
            Assert.Equal(size, fields.GetUpperBound(0) + 1);
            Assert.Equal(size, fields.GetUpperBound(1) + 1);
        }
    }
}
