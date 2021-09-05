using Battleships.Fields;
using Battleships.Fields.Interfaces;

namespace Battleships.Factories
{
    public class FieldsFactory : IFieldsFactory
    {
        public IShip CreateShip(int shipLength)
        {
            return new Ship(shipLength);
        }

        public IField CreateMissedShotMarker(int x, int y)
        {
            return new MissedShotMarker(x, y);
        }
    }
}
