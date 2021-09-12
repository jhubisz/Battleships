using Battleships;
using Battleships.Enums;
using Battleships.Exceptions;
using Battleships.Fields.Interfaces;
using BattleshipsAIPlayer;
using BattleshipsTests.TestDoubles.Mocks;
using System.Collections.Generic;
using TestDoubles.Mocks;
using Xunit;

namespace BattleshipsAIPlayerTests
{
    public class AIPlayerTests
    {
        public BattleshipsGame<ShipConstraintsMock> Game { get; set; }

        public AIPlayerTests()
        {
            Game = new BattleshipsGame<ShipConstraintsMock>();
            Game.InitializeGame();
        }
        
        [Fact]
        public void PlayerAIInstanceIsCreatedAndAcceptsBoardAndPlacesShips()
        {
            var randomizer = new PositionRandomizer(new RandomGeneratorMock(new int[] { 0 }));

            var positions = new Dictionary<int, (int x, int y, ShipDirection direction)[]>();
            positions.Add(1, new (int x, int y, ShipDirection direction)[] { (1, 1, ShipDirection.Horizontal) });
            positions.Add(2, new (int x, int y, ShipDirection direction)[] { (1, 3, ShipDirection.Horizontal) });
            var positionFinder = new AvailableShipPositionsFinderMock(positions);
            
            var player = new AIPlayer(randomizer, positionFinder);
            player.Board = Game.PlayerABoard;
            player.PlaceShips();

            Assert.Collection(Game.PlayerABoard.Ships,
                item => Assert.True(((IShip)item).CheckIfPositionExists(1, 1)),
                item => Assert.True(((IShip)item).CheckIfPositionExists(1, 3)));
        }

        [Fact]
        public void ThrowsNoAvailablePositionsExceptionWhenShipCantBePlaced()
        {
            var randomizer = new PositionRandomizer(new RandomGeneratorMock(new int[] { 0 }));

            var positions = new Dictionary<int, (int x, int y, ShipDirection direction)[]>();
            var positionFinder = new AvailableShipPositionsFinderMock(positions);

            var player = new AIPlayer(randomizer, positionFinder);
            player.Board = Game.PlayerABoard;

            Assert.Throws<NoAvailableShipPositionsException>(() => player.PlaceShips());
        }
    }
}
