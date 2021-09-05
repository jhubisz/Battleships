using Battleships.Fields.Interfaces;

namespace Battleships.Factories
{
    public interface IFieldsFactory
    {
        IShip CreateShip(int shipLength);
        IPlacable CreateMissedShotMarker(int x, int y);
    }
}
