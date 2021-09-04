using Battleships;
using Battleships.Enums;
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
            Board = new Board(shipConstraints);
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
    }
}
