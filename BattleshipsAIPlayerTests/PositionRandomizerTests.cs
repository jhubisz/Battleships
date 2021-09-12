using Battleships.Enums;
using BattleshipsAIPlayer;
using TestDoubles.Mocks;
using Xunit;

namespace BattleshipsAIPlayerTests
{
    public class PositionRandomizerTests
    {
        [Fact]
        public void PositionRandomizerIsUsingIRandomGeneratorAndReturnsFirstPosition()
        {
            var randomGenerator = new RandomGeneratorMock(new int[] { 0 });
            var positionRandomizer = new PositionRandomizer(randomGenerator);
            var position = (x: 1, y: 1, ShipDirection.Horizontal);
            var returnPosition = positionRandomizer.ReturnRandomPosition(new (int x, int y, ShipDirection direction)[] { position });

            Assert.Equal(position, returnPosition);
        }

        [Theory]
        [InlineData(new int[] { 0 })]
        [InlineData(new int[] { 1, 1, 2 })]
        [InlineData(new int[] { 4, 1, 2 })]
        [InlineData(new int[] { 3, 1, 2, 0, 1, 4, 3, 3, 1, 2, 0, 0, 1, 2, 3 })]
        public void ReturnsPositionFromArrayBasedOnNonRandomNumbersArray(int[] numbersArray)
        {
            var positionsArray = new (int x, int y, ShipDirection direction)[] { 
                (x: 1, y: 1, ShipDirection.Horizontal),
                (x: 2, y: 2, ShipDirection.Horizontal),
                (x: 3, y: 3, ShipDirection.Horizontal),
                (x: 4, y: 4, ShipDirection.Horizontal),
                (x: 5, y: 5, ShipDirection.Horizontal) 
            };

            var randomGenerator = new RandomGeneratorMock(numbersArray);
            var positionRandomizer = new PositionRandomizer(randomGenerator);

            foreach (var number in numbersArray)
                Assert.Equal(positionsArray[number], positionRandomizer.ReturnRandomPosition(positionsArray));
        }
    }
}
