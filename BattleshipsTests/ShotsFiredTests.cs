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
        public void ReturnsFalseForMissedShot()
        {
            var result = Board.CheckFiredShot(1, 1);
            Assert.False(result);
        }

        [Fact]
        public void ReturnsTrueForHitShot()
        {
            Board.PlaceShip(shipLength: 3,
                            initialPosition: (x: 1, y: 1),
                            direction: ShipDirection.Horizontal);

            var result = Board.CheckFiredShot(1, 1);

            Assert.True(result);
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
