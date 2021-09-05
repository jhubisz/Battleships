using Battleships.Fields.Interfaces;

namespace Battleships.Factories
{
    public interface IFieldsFactory
    {
        IShip CreateShip(int shipLength);
        IField CreateMissedShotMarker(int x, int y);
        IField CreateEmptyField();
    }
}
