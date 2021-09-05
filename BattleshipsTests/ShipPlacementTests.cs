using Battleships.Enums;
using Battleships.Factories;
using Battleships.Fields.Interfaces;
using Xunit;

namespace BattleshipsTests
{
    public class ShipPlacementTests
    {
        private const int DEFAULT_SHIP_LENGTH = 3;
        public IFieldsFactory FieldsFactory { get; set; }
        public IShip Ship { get; private set; }

        public ShipPlacementTests()
        {
            FieldsFactory = new FieldsFactory();
            Ship = FieldsFactory.CreateShip(DEFAULT_SHIP_LENGTH);
        }

        [Fact]
        public void ShipGetsCorrectNumberOfFieldsBasedOnLenth()
        {
            var initialPosiotion = (x: 1, y: 1);
            Ship.Place(initialPosiotion, ShipDirection.Horizontal);

            Assert.Equal(DEFAULT_SHIP_LENGTH, Ship.Fields.Count);
        }

        [Fact]
        public void ShipGetsHorizontalFieldsForHorizontalDirection()
        {
            var initialPosiotion = (x: 1, y: 1);
            Ship.Place(initialPosiotion, ShipDirection.Horizontal);

            for (int i = 0; i < DEFAULT_SHIP_LENGTH; i++)
            {
                Assert.Equal((initialPosiotion.x + i, initialPosiotion.y), Ship.Fields[i]);
            }
        }

        [Fact]
        public void ShipGetsHorizontalFieldsForVerticalDirection()
        {
            var initialPosiotion = (x: 1, y: 1);
            Ship.Place(initialPosiotion, ShipDirection.Vertical);

            for (int i = 0; i < DEFAULT_SHIP_LENGTH; i++)
            {
                Assert.Equal((initialPosiotion.x, initialPosiotion.y + i), Ship.Fields[i]);
            }
        }
    }
}
