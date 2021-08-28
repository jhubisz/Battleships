using Battleship.Exceptions;
using Battleship.Interfaces;
using System.Collections.Generic;

namespace Battleship
{
    public class Ship : IPlacable
    {
        public List<(int x, int y)> Fields { get; set; }

        public Ship()
        {
            Fields = new List<(int x, int y)>();
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
