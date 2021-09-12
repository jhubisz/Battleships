using Battleships.Enums;

namespace BattleshipsAIPlayer.Interfaces
{
    public interface IPositionRandomizer
    {
        (int x, int y, ShipDirection direction) ReturnRandomPosition((int x, int y, ShipDirection direction)[] positions);
    }
}