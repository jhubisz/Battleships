using Battleship;
using Xunit;
using Battleship.Enums;

namespace BattleshipTests
{
    public class ShipOnBoardPlacementTests
    {
        private const int SHIP_LENGTH = 3;
        public Board Board { get; set; }

        public ShipOnBoardPlacementTests()
        {
            Board = new Board();
        }

        [Fact]
        public void BoardAllowsPlacingOfAShip()
        {
            var shipLength = 1;
            var initialPosition = (x: 1, y: 1);
            var ship = Board.PlaceShip(shipLength, initialPosition, ShipDirection.Horizontal);

            Assert.NotNull(ship); 
        }

        [Fact]
        public void BoardAllowsPlacingOfA3LengthHorizontalShip()
        {
            var shipLength = 3;
            var initialPosition = (x: 1, y: 1);
            var ship = Board.PlaceShip(shipLength, initialPosition, ShipDirection.Horizontal);

            Assert.Equal(ship, Board.Fields[0, 0]);
            Assert.Equal(ship, Board.Fields[1, 0]);
            Assert.Equal(ship, Board.Fields[2, 0]);
        }

        [Fact]
        public void BoardAllowsPlacingOfA3LengthVerticalShip()
        {
            var shipLength = 3;
            var initialPosition = (x: 1, y: 1);
            var ship = Board.PlaceShip(shipLength, initialPosition, ShipDirection.Vertical);

            Assert.Equal(ship, Board.Fields[0, 0]);
            Assert.Equal(ship, Board.Fields[0, 1]);
            Assert.Equal(ship, Board.Fields[0, 2]);
        }

        [Fact]
        public void BoardShipsCollectionContainsShipsAfterAddingToBoard()
        {
            var shipLength = 3;
            var initialPosition = (x: 1, y: 3);
            var ship = Board.PlaceShip(shipLength, initialPosition, ShipDirection.Horizontal);
            var ship2 = Board.PlaceShip(shipLength, initialPosition, ShipDirection.Horizontal);

            Assert.Collection(Board.Ships,
                item => Assert.Equal(ship, item),
                item => Assert.Equal(ship2, item));
        }
    }
}
