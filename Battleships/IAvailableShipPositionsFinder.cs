using Battleships.Enums;

namespace Battleships
{
    public interface IAvailableShipPositionsFinder
    {
        (int x, int y, ShipDirection direction)[] FindAllPositions(int shipLength, Board board);
    }
}
