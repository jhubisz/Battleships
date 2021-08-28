﻿using Battleship;
using Battleship.Interfaces;
using Battleship.Exceptions;
using Xunit;

namespace BattleshipTests
{
    public class ShipTests
    {
        public Ship Ship { get; private set; }

        public ShipTests()
        {
            Ship = new Ship();
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
