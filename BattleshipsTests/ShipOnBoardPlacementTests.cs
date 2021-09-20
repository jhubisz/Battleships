using Battleships;
using Battleships.Enums;
using Battleships.Exceptions;
using Battleships.Factories;
using Battleships.ShipConstraintsConfiguration;
using Xunit;

namespace BattleshipsTests
{
    public class ShipOnBoardPlacementTests
    {
        private const int SHIP_LENGTH = 3;
        public Board Board { get; set; }

        public ShipOnBoardPlacementTests()
        {
            var shipConstraints = new ShipConstraints();
            var fieldsFactory = new FieldsFactory();
            Board = new Board(shipConstraints, fieldsFactory);
        }

        [Fact]
        public void BoardAllowsPlacingOfAShip()
        {
            var shipLength = 2;
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
            var initialPosition1 = (x: 1, y: 1);
            var initialPosition2 = (x: 1, y: 3);
            var ship = Board.PlaceShip(shipLength, initialPosition1, ShipDirection.Horizontal);
            var ship2 = Board.PlaceShip(shipLength, initialPosition2, ShipDirection.Horizontal);

            Assert.Collection(Board.Ships,
                item => Assert.Equal(ship, item),
                item => Assert.Equal(ship2, item));
        }

        [Theory]
        [InlineData(-1, 1, 2, ShipDirection.Horizontal)]
        [InlineData(10, 1, 3, ShipDirection.Horizontal)]
        [InlineData(1, -1, 2, ShipDirection.Vertical)]
        [InlineData(1, 10, 3, ShipDirection.Vertical)]
        public void ThrowsInvalidPositionOutOfBoundsExceptionIfShipPlacedOutOfBounds(int x, int y, int shipLength, ShipDirection shipDirection)
        {
            var initialPosition = (x, y);

            Assert.Throws<InvalidPositionOutOfBoundsException>(() => Board.PlaceShip(shipLength, initialPosition, shipDirection));
        }

        [Theory]
        [InlineData(1, 1, 3, ShipDirection.Horizontal,
                    1, 1, 3, ShipDirection.Horizontal)] // full overlap
        [InlineData(1, 1, 3, ShipDirection.Horizontal,
                    3, 1, 3, ShipDirection.Horizontal)] // overlap 1 field horizontaly
        [InlineData(3, 1, 3, ShipDirection.Horizontal,
                    1, 1, 3, ShipDirection.Horizontal)] // overlap 1 field horizontaly
        [InlineData(1, 1, 3, ShipDirection.Horizontal,
                    1, 1, 3, ShipDirection.Vertical)] // overlap 1 field horizontaly / verticaly
        [InlineData(1, 1, 3, ShipDirection.Vertical,
                    1, 3, 3, ShipDirection.Horizontal)] // overlap 1 field verticaly / horizontaly
        [InlineData(2, 1, 3, ShipDirection.Vertical,
                    1, 2, 3, ShipDirection.Horizontal)] // cross overlap 1 field
        public void ThrowsInvalidPositionOverlapExceptionIfShipOverlapsWithOtherShip(
            int ship1x, int ship1y, int ship1Length, ShipDirection ship1Direction,
            int ship2x, int ship2y, int ship2Length, ShipDirection ship2Direction)
        {
            var ship1Position = (x: ship1x, y: ship1y);
            var ship2Position = (x: ship2x, y: ship2y);
            Board.PlaceShip(ship1Length, ship1Position, ship1Direction);

            Assert.Throws<InvalidPositionOverlapException>(() => Board.PlaceShip(ship2Length, ship2Position, ship2Direction));
        }

        [Theory]
        [InlineData(1, 1, 3, ShipDirection.Horizontal,
                    4, 1, 3, ShipDirection.Horizontal)] // ship to the left to close
        [InlineData(4, 1, 3, ShipDirection.Horizontal,
                    1, 1, 3, ShipDirection.Horizontal)] // ship to the right to close
        [InlineData(2, 1, 3, ShipDirection.Horizontal,
                    2, 2, 3, ShipDirection.Vertical)] // ship on upper row to close
        [InlineData(2, 4, 3, ShipDirection.Horizontal,
                    3, 1, 3, ShipDirection.Vertical)] // ship on lower row to close
        [InlineData(1, 10, 3, ShipDirection.Horizontal,
                    4, 10, 3, ShipDirection.Horizontal)] // last row horizontaly to close
        [InlineData(1, 9, 3, ShipDirection.Horizontal,
                    1, 10, 3, ShipDirection.Horizontal)] // last two rows horizontaly to close
        [InlineData(10, 1, 3, ShipDirection.Vertical,
                    10, 4, 3, ShipDirection.Vertical)] // last column vertically to close
        [InlineData(9, 1, 3, ShipDirection.Vertical,
                    10, 1, 3, ShipDirection.Vertical)] // last two columns vertically to close
        public void ThrowsInvalidPositionProximityExceptionIfShipCloserThanOneFieldFromOtherShip(
            int ship1x, int ship1y, int ship1Length, ShipDirection ship1Direction,
            int ship2x, int ship2y, int ship2Length, ShipDirection ship2Direction)
        {
            var ship1Position = (x: ship1x, y: ship1y);
            var ship2Position = (x: ship2x, y: ship2y);
            Board.PlaceShip(ship1Length, ship1Position, ship1Direction);

            Assert.Throws<InvalidPositionProximityException>(() => Board.PlaceShip(ship2Length, ship2Position, ship2Direction));
        }

        /// <summary>
        ///    1 2 3 4 5 6 7 8 9 10   
        /// 1  █ █ █ ░ █ █ ░ ░ ░ ░ ­­
        /// 2  ░ ░ ░ ░ ░ ░ ░ ░ █ ░
        /// 3  ░ ░ █ █ █ █ █ ░ █ ░
        /// 4  ░ ░ ░ ░ ░ ░ ░ ░ █ ░
        /// 5  ░ █ ░ █ █ █ █ ░ ░ ░
        /// 6  ░ █ ░ ░ ░ ░ ░ ░ ░ █
        /// 7  ░ █ ░ ░ ░ █ █ █ ░ █
        /// 8  ░ █ ░ ░ ░ ░ ░ ░ ░ ░ 
        /// 9  ░ ░ ░ █ █ ░ ░ █ ░ ░ 
        /// 10 ░ ░ ░ ░ ░ ░ ░ █ ░ ░            
        /// </summary>
        [Fact]
        public void BoardAllowsPlacingAllShips()
        {
            var ship51 = Board.PlaceShip(5, (x: 3, y: 3), ShipDirection.Horizontal);

            var ship41 = Board.PlaceShip(4, (x: 2, y: 5), ShipDirection.Vertical);
            var ship42 = Board.PlaceShip(4, (x: 4, y: 5), ShipDirection.Horizontal);

            var ship31 = Board.PlaceShip(3, (x: 1, y: 1), ShipDirection.Horizontal);
            var ship32 = Board.PlaceShip(3, (x: 9, y: 2), ShipDirection.Vertical);
            var ship33 = Board.PlaceShip(3, (x: 6, y: 7), ShipDirection.Horizontal);

            var ship21 = Board.PlaceShip(2, (x: 5, y: 1), ShipDirection.Horizontal);
            var ship22 = Board.PlaceShip(2, (x: 10, y: 6), ShipDirection.Vertical);
            var ship23 = Board.PlaceShip(2, (x: 4, y: 9), ShipDirection.Horizontal);
            var ship24 = Board.PlaceShip(2, (x: 8, y: 9), ShipDirection.Vertical);

            Assert.Collection(Board.Ships,
                item => Assert.Equal(ship51, item),

                item => Assert.Equal(ship41, item),
                item => Assert.Equal(ship42, item),

                item => Assert.Equal(ship31, item),
                item => Assert.Equal(ship32, item),
                item => Assert.Equal(ship33, item),

                item => Assert.Equal(ship21, item),
                item => Assert.Equal(ship22, item),
                item => Assert.Equal(ship23, item),
                item => Assert.Equal(ship24, item));
        }
    }
}
