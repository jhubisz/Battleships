using Battleships;
using Battleships.Enums;
using Battleships.Factories;
using Battleships.Fields;
using Battleships.ShipConstraintsConfiguration;
using Xunit;

namespace BattleshipsTests
{
    public class ShotsFiredTests
    {
        public Board Board { get; private set; }

        public ShotsFiredTests()
        {
            var shipConstraints = new ShipConstraints();
            var fieldsFactory = new FieldsFactory();
            Board = new Board(shipConstraints, fieldsFactory);
        }

        [Fact]
        public void ReturnsFiredShotResultWithHitInformationForHitShot()
        {
            Board.PlaceShip(shipLength: 3,
                            initialPosition: (x: 1, y: 1),
                            direction: ShipDirection.Horizontal);

            var result = Board.CheckFiredShot(1, 1);

            Assert.True(result.Hit);
            Assert.IsType<FiredShotResult>(result);
            Assert.Equal(FiredShotResultType.ShipHit, result.ResultType);
        }

        [Fact]
        public void ReturnsFiredShotResultWithMissedInformationForMissedShot()
        {
            var result = Board.CheckFiredShot(1, 1);

            Assert.False(result.Hit);
            Assert.IsType<FiredShotResult>(result);
            Assert.Equal(FiredShotResultType.ShotMissed, result.ResultType);
        }

        [Fact]
        public void ReturnsFiredShotResultWithMissedInformationForPreviouslyMissedShot()
        {
            var result = Board.CheckFiredShot(1, 1);
            var result2 = Board.CheckFiredShot(1, 1);

            Assert.False(result2.Hit);
            Assert.IsType<FiredShotResult>(result2);
            Assert.Equal(FiredShotResultType.ShotMissed, result2.ResultType);
        }

        [Fact]
        public void SetsMissedMarkerOnBoardForMissedShot()
        {
            var shotCoordinates = (x: 1, y: 1);
            var result = Board.CheckFiredShot(shotCoordinates.x, shotCoordinates.y);

            Assert.NotNull(Board.ReturnField(shotCoordinates.x, shotCoordinates.y));
            Assert.IsType<MissedShotMarker>(Board.ReturnField(shotCoordinates.x, shotCoordinates.y));
        }
    }
}
