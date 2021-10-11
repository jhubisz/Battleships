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

        [Fact]
        public void PositionRemovedFromAvailablePositionsAfterHitShot()
        {
            var positionShot = (x: 1, y: 1);
            var firedShotResult = new FiredShotResult() { ResultType = FiredShotResultType.ShipHit, Hit = true };
            Player.ProcessShotResult(firedShotResult, positionShot);

            Assert.DoesNotContain(positionShot, Player.AvailableShotPositions);
        }

        [Fact]
        public void PositionsAddedToPreferredPositionsAfterHitShot()
        {
            var positionShot = (x: 1, y: 1);
            var firedShotResult = new FiredShotResult() { ResultType = FiredShotResultType.ShipHit, Hit = true };
            Player.ProcessShotResult(firedShotResult, positionShot);

            Assert.Contains((x: 1, y: 2), Player.PreferredShotPositions);
            Assert.Contains((x: 2, y: 1), Player.PreferredShotPositions);
            Assert.Contains((x: 2, y: 2), Player.PreferredShotPositions);

            Assert.DoesNotContain((x: 0, y: 0), Player.PreferredShotPositions);
            Assert.DoesNotContain((x: 0, y: 1), Player.PreferredShotPositions);
            Assert.DoesNotContain((x: 0, y: 2), Player.PreferredShotPositions);
            Assert.DoesNotContain((x: 1, y: 0), Player.PreferredShotPositions);
            Assert.DoesNotContain((x: 1, y: 1), Player.PreferredShotPositions);
            Assert.DoesNotContain((x: 2, y: 0), Player.PreferredShotPositions);
        }

        [Fact]
        public void FiresShotFromthePreferredPositionsAfterHitShot()
        {
            var positionShot = (x: 1, y: 1);
            var firedShotResult = new FiredShotResult() { ResultType = FiredShotResultType.ShipHit, Hit = true };
            Player.ProcessShotResult(firedShotResult, positionShot);

            var positionToShoot = 2;
            var position = Player.PreferredShotPositions[positionToShoot];
            var randomizer = new PositionRandomizer(new RandomGeneratorMock(new int[] { positionToShoot }));
            Player.PositionRandomizer = randomizer;

            var shot = Player.MakeShot();
            Assert.Equal(position.x, shot.x);
            Assert.Equal(position.y, shot.y);
        }

        [Fact]
        public void PositonRemovedFromAvailableAndPreferredShotsListsAfterMissedShot()
        {
            var positionHit = (x: 1, y: 1);
            var firedShotResult = new FiredShotResult() { ResultType = FiredShotResultType.ShipHit, Hit = true };
            Player.ProcessShotResult(firedShotResult, positionHit);

            var firedShotResult2 = new FiredShotResult() { ResultType = FiredShotResultType.ShotMissed };
            var positionMissed = (x: 2, y: 2);
            Player.ProcessShotResult(firedShotResult2, positionMissed);

            Assert.DoesNotContain(positionMissed, Player.AvailableShotPositions);
            Assert.DoesNotContain(positionMissed, Player.PreferredShotPositions);
        }
    }
}
