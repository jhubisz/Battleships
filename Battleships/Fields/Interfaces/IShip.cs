using Battleships.Enums;
using System.Collections.Generic;

namespace Battleships.Fields.Interfaces
{
    public interface IShip : IField
    {
        int Length { get; }
        List<(int x, int y)> Place((int x, int y) initialPosition, ShipDirection shipDirection);
    }
}
