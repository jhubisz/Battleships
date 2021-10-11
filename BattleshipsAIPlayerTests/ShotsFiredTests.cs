using Battleships;
using Battleships.Enums;
using BattleshipsAIPlayer;
using BattleshipsTests.TestDoubles.Mocks;
using System.Collections.Generic;
using TestDoubles.Mocks;
using Xunit;

namespace BattleshipsAIPlayerTests
{
    public class ShotsFiredTests
    {
        public BattleshipsGame<ShipConstraintsMock> Game { get; set; }
        public AIPlayer Player;

        public ShotsFiredTests()
        {
            Game = new BattleshipsGame<ShipConstraintsMock>();
            Game.InitializeGame();

            var randomizer = new PositionRandomizer(new RandomGeneratorMock(new int[] { 0 }));
            var positions = new Dictionary<int, (int x, int y, ShipDirection direction)[]>();
            positions.Add(1, new (int x, int y, ShipDirection direction)[] { (1, 1, ShipDirection.Horizontal) });
            positions.Add(2, new (int x, int y, ShipDirection direction)[] { (1, 3, ShipDirection.Horizontal) });
            var positionFinder = new AvailableShipPositionsFinderMock(positions);

            Player = new AIPlayer(Game.PlayerABoard, randomizer, positionFinder);
            Player.PlaceShips();
        }

        [Fact]
        public void PlayerMakesAShot()
        {
            var positionToShoot = 2;
            var position = Player.AvailableShotPositions[positionToShoot];
            var randomizer = new PositionRandomizer(new RandomGeneratorMock(new int[] { positionToShoot }));
            Player.PositionRandomizer = randomizer;

            var shot = Player.MakeShot();
            Assert.Equal(position.x, shot.x);
            Assert.Equal(position.y, shot.y);
        }

        [Fact]
        public void PositionRemovedFromAvailablePositionsAfterMissedShot()
        {
            var positionShot = (x: 1, y: 1);
            var firedShotResult = new FiredShotResult() { ResultType = FiredShotResultType.ShotMissed };
            Player.ProcessShotResult(firedShotResult, positionShot);

            Assert.DoesNotContain(positionShot, Player.AvailableShotPositions);
        }
    }
}
