using Battleships;
using Battleships.Enums;
using System.Collections.Generic;

namespace TestDoubles.Mocks
{
    public class AvailableShipPositionsFinderMock : IAvailableShipPositionsFinder
    {
        private Dictionary<int, (int x, int y, ShipDirection direction)[]> positions;

        public AvailableShipPositionsFinderMock(Dictionary<int, (int x, int y, ShipDirection direction)[]> injectedPositions)
        {
            positions = injectedPositions;
        }

        public (int x, int y, ShipDirection direction)[] FindAllPositions(int shipLength, Board board)
        {
            if (positions.ContainsKey(shipLength))
                return positions[shipLength];

            return new (int x, int y, ShipDirection direction)[0];
        }
    }
}
