using Battleships;
using Battleships.Enums;
using BattleshipsTests.TestDoubles.Mocks;
using System;
using Xunit;

namespace BattleshipsTests
{
    public class AvailableShipPositionsFinderTests
    {
        public BattleshipsGame<ShipConstraintsMock> Game { get; set; }

        public AvailableShipPositionsFinderTests()
        {
            Game = new BattleshipsGame<ShipConstraintsMock>();
            Game.InitializeGame();
        }

        [Fact]
        public void ReturnsFirstPositionForEmptyBoard()
        {
            var positionFinder = new AvailableShipPositionsFinder();
            var availablePositions = positionFinder.FindAllPositions(1, Game.PlayerABoard);

            Assert.Contains(availablePositions,
                item => item.x == 1 && item.y == 1 && item.direction == ShipDirection.Horizontal);
        }

        [Fact]
        public void ReturnsAllPositionsForEmptyBoard()
        {
            var positionFinder = new AvailableShipPositionsFinder();
            var availablePositions = positionFinder.FindAllPositions(2, Game.PlayerABoard);

            for(int x = 1; x <= 9; x++)
            {
                for(int y = 1; y <= 9; y++)
                {
                    Assert.Contains(availablePositions,
                        item => item.x == x && item.y == y && item.direction == ShipDirection.Horizontal);
                    Assert.Contains(availablePositions,
                        item => item.x == x && item.y == y && item.direction == ShipDirection.Vertical);
                }
            }
        }

        [Fact]
        public void DoesNotReturnInvalidPositionsForEmptyBoard()
        {
            var positionFinder = new AvailableShipPositionsFinder();
            var availablePositions = positionFinder.FindAllPositions(2, Game.PlayerABoard);

            for (int i = 1; i <= 10; i++)
            {
                Assert.DoesNotContain(availablePositions,
                    item => item.x == 10 && item.y == i && item.direction == ShipDirection.Horizontal);
                Assert.DoesNotContain(availablePositions,
                    item => item.x == i && item.y == 10 && item.direction == ShipDirection.Vertical);
            }
        }

        [Fact]
        public void DoesNotReturnAnyPositionsForNotAllowedShip()
        {
            var positionFinder = new AvailableShipPositionsFinder();
            Game.PlayerABoard.PlaceShip(1, (x: 1, y: 1), ShipDirection.Horizontal);
            var availablePositions = positionFinder.FindAllPositions(1, Game.PlayerABoard);

            Assert.Empty(availablePositions);
        }

        [Fact]
        public void DoesNotReturnOccupiedPositionsForBoardWithShip()
        {
            var positionFinder = new AvailableShipPositionsFinder();
            Game.PlayerABoard.PlaceShip(1, (x: 1, y: 1), ShipDirection.Horizontal);
            var availablePositions = positionFinder.FindAllPositions(2, Game.PlayerABoard);

            Assert.DoesNotContain(availablePositions,
                    item => item.x == 1 && item.y == 1 && item.direction == ShipDirection.Horizontal);
            Assert.DoesNotContain(availablePositions,
                    item => item.x == 1 && item.y == 2 && item.direction == ShipDirection.Horizontal);
            Assert.DoesNotContain(availablePositions,
                    item => item.x == 2 && item.y == 1 && item.direction == ShipDirection.Horizontal);
            Assert.DoesNotContain(availablePositions,
                    item => item.x == 2 && item.y == 2 && item.direction == ShipDirection.Horizontal);

            Assert.DoesNotContain(availablePositions,
                    item => item.x == 1 && item.y == 1 && item.direction == ShipDirection.Vertical);
            Assert.DoesNotContain(availablePositions,
                    item => item.x == 1 && item.y == 2 && item.direction == ShipDirection.Vertical);
            Assert.DoesNotContain(availablePositions,
                    item => item.x == 2 && item.y == 1 && item.direction == ShipDirection.Vertical);
            Assert.DoesNotContain(availablePositions,
                    item => item.x == 2 && item.y == 2 && item.direction == ShipDirection.Vertical);
        }
    }
}