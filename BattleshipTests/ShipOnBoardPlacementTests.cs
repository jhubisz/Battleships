﻿using Battleship;
using Xunit;
using Battleship.Enums;
using Battleship.Exceptions;

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
        public void ThrowsInvalidPositionExceptionIfShipPlacedOutOfBounds(int x, int y, int shipLength, ShipDirection shipDirection)
        {
            var initialPosition = (x, y);

            Assert.Throws<InvalidPositionException>(() => Board.PlaceShip(shipLength, initialPosition, shipDirection));
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
        public void ThrowsInvalidPositionExceptionIfShipOverlapsWithOtherShip(
            int ship1x, int ship1y, int ship1Length, ShipDirection ship1Direction,
            int ship2x, int ship2y, int ship2Length, ShipDirection ship2Direction)
        {
            var ship1Position = (x: ship1x, y: ship1y);
            var ship2Position = (x: ship2x, y: ship2y);
            Board.PlaceShip(ship1Length, ship1Position, ship1Direction);

            Assert.Throws<InvalidPositionException>(() => Board.PlaceShip(ship2Length, ship2Position, ship2Direction));
        }
    }
}
