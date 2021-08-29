using Battleships;
using Battleships.Enums;
using Xunit;

namespace BattleshipsTests
{
    public class ShipPlacementTests
    {
        private const int SHIP_LENGTH = 3;
        public Ship Ship { get; private set; }

        public ShipPlacementTests()
        {
            Ship = new Ship(SHIP_LENGTH);
        }

        [Fact]
        public void ShipGetsCorrectNumberOfFieldsBasedOnLenth()
        {
            var initialPosiotion = (x: 1, y: 1);
            Ship.Place(initialPosiotion, ShipDirection.Horizontal);

            Assert.Equal(SHIP_LENGTH, Ship.Fields.Count);
        }

        [Fact]
        public void ShipGetsHorizontalFieldsForHorizontalDirection()
        {
            var initialPosiotion = (x: 1, y: 1);
            Ship.Place(initialPosiotion, ShipDirection.Horizontal);

            for (int i = 0; i < SHIP_LENGTH; i++)
            {
                Assert.Equal((initialPosiotion.x + i, initialPosiotion.y), Ship.Fields[i]);
            }
        }

        [Fact]
        public void ShipGetsHorizontalFieldsForVerticalDirection()
        {
            var initialPosiotion = (x: 1, y: 1);
            Ship.Place(initialPosiotion, ShipDirection.Vertical);

            for (int i = 0; i < SHIP_LENGTH; i++)
            {
                Assert.Equal((initialPosiotion.x, initialPosiotion.y + i), Ship.Fields[i]);
            }
        }
    }
}
