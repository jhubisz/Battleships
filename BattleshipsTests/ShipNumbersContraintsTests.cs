using Battleships;
using Battleships.Enums;
using Battleships.Exceptions;
using Battleships.ShipConstraintsConfiguration;
using Xunit;

namespace BattleshipsTests
{
    public class ShipNumbersConstraintsTests
    {
        public Board Board { get; private set; }

        public ShipNumbersConstraintsTests()
        {
            var shipConstraints = new ShipConstraints();
            Board = new Board(shipConstraints);
        }

        [Fact]
        public void ThrowsShipNotAllowedExceptionForAShipOfLength6()
        {
            var shipLength = 6;
            var shipPosition = (x: 1, y: 1);
            var shipDirection = ShipDirection.Horizontal;

            Assert.Throws<ShipNotAllowedException>(() => Board.PlaceShip(shipLength, shipPosition, shipDirection));
        }

        [Fact]
        public void ThrowsShipNotAllowedExceptionForAShipOfLength1()
        {
            var shipLength = 1;
            var shipPosition = (x: 1, y: 1);
            var shipDirection = ShipDirection.Horizontal;

            Assert.Throws<ShipNotAllowedException>(() => Board.PlaceShip(shipLength, shipPosition, shipDirection));
        }

        [Theory]
        [InlineData(2, 5)]
        [InlineData(3, 4)]
        [InlineData(4, 3)]
        [InlineData(5, 2)]
        public void ThrowsShipNotAllowedExceptionForTooManyShipsOfCertainKind(int shipLength, int shipNo)
        {
            var shipPosition = (x: 1, y: 1);
            var shipDirection = ShipDirection.Horizontal;

            for (int i = 1; i <= shipNo; i++)
            {
                if (i < shipNo)
                    Board.PlaceShip(shipLength, shipPosition, shipDirection);
                else
                    Assert.Throws<ShipNotAllowedException>(() => Board.PlaceShip(shipLength, shipPosition, shipDirection));

                shipPosition = (x: shipPosition.x, y: shipPosition.y + 2);
            }
        }
    }
}
