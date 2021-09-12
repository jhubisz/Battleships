using Battleships.Enums;
using System.Collections.Generic;

namespace Battleships
{
    public class AvailableShipPositionsFinder : IAvailableShipPositionsFinder
    {
        public (int x, int y, ShipDirection direction)[] FindAllPositions(int shipLength, Board board)
        {
            var positions = new List<(int x, int y, ShipDirection direction)>();

            for (int x = 1; x <= board.Size; x++)
            {
                for (int y = 1; y <= board.Size; y++)
                {
                    if (board.CheckIfShipPositionValid(shipLength, (x, y), ShipDirection.Horizontal))
                        positions.Add((x, y, ShipDirection.Horizontal));
                    if (board.CheckIfShipPositionValid(shipLength, (x, y), ShipDirection.Vertical))
                        positions.Add((x, y, ShipDirection.Vertical));
                }
            }

            return positions.ToArray();
        }
    }
}
