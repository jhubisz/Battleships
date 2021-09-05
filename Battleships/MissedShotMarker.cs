using Battleships.Exceptions;
using Battleships.Interfaces;
using System.Collections.Generic;

namespace Battleships
{
    public class MissedShotMarker : IPlacable
    {
        public List<(int x, int y)> Fields { get; set; }

        public MissedShotMarker(int x, int y)
        {
            Fields = new List<(int x, int y)>();
            AddPosition(x, y);
        }

        public void AddPosition(int x, int y)
        {
            if (CheckIfPositionExists(x, y))
                throw new PositionExistsException("Position Exists");
            Fields.Add((x, y));
        }

        public bool CheckIfPositionExists(int x, int y)
        {
            return Fields.Contains((x, y));
        }
    }
}
