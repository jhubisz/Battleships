using Battleships.Enums;
using System.Collections.Generic;

namespace Battleships.Fields.Interfaces
{
    public interface IShip : IPlacable
    {
        int Length { get; }
        List<(int x, int y)> Place((int x, int y) initialPosition, ShipDirection shipDirection);
    }
}
