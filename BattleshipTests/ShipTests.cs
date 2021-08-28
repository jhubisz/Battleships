using Battleship;
using Battleship.Interfaces;
using Xunit;

namespace BattleshipTests
{
    public class ShipTests
    {
        [Fact]
        public void ShipImpolementsIPlaceableInterface()
        {
            var ship = new Ship();
            Assert.IsAssignableFrom<IPlacable>(ship);
        }
    }
}
