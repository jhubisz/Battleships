using Battleships;
using Battleships.Exceptions;
using Battleships.Interfaces;
using Xunit;

namespace BattleshipsTests
{
    public class ShipTests
    {
        public Ship Ship { get; private set; }

        public ShipTests()
        {
            Ship = new Ship();
        }

        [Fact]
        public void ShipGetsDefaultLengthOnCreation()
        {
            Assert.True(Ship.Length > 0);
        }

        [Fact]
        public void ShipGetsCorrectLengthOnCreation()
        {
            var shipLength = 3;
            Ship = new Ship(shipLength);
            Assert.Equal(shipLength, Ship.Length);
        }

        [Fact]
        public void ShipImpolementsIPlaceableInterface()
        {
            Assert.IsAssignableFrom<IPlacable>(Ship);
        }

        [Fact]
        public void ShipGetsLocationAdded()
        {
            Ship.AddPosition(1, 1);
            Assert.Single(Ship.Fields);
        }


        [Fact]
        public void ShipReturnsFalseIfLocationIsNotInCollection()
        {
            var x = 1;
            var y = 1;
            Ship.AddPosition(x, y);

            var positionExists = Ship.CheckIfPositionExists(x + 1, y + 1);
            Assert.False(positionExists);
        }
        [Fact]
        public void ShipReturnsTrueIfLocationIsInCollection()
        {
            var x = 1;
            var y = 1;
            Ship.AddPosition(x, y);

            var positionExists = Ship.CheckIfPositionExists(x, y);
            Assert.True(positionExists);
        }

        [Fact]
        public void ThrowsPositionExistsExceptionWhenExistingPositionAdded()
        {
            var x = 1;
            var y = 1;
            Ship.AddPosition(x, y);
            Assert.Throws<PositionExistsException>(() => Ship.AddPosition(x, y));
        }
    }
}
