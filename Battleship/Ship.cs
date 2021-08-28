using Battleship.Exceptions;
using Battleship.Interfaces;
using System.Collections.Generic;

namespace Battleship
{
    public class Ship : IPlacable
    {
        private const int DEFAULT_LENGTH = 1;

        public List<(int x, int y)> Fields { get; set; }
        public int Length { get; set; }

        public Ship() : this(DEFAULT_LENGTH)
        {
        }
        public Ship(int length)
        {
            Length = length;
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
