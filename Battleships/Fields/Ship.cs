using Battleships.Enums;
using Battleships.Exceptions;
using Battleships.Fields;
using Battleships.Fields.Interfaces;
using System.Collections.Generic;

namespace Battleships.Fields
{
    public class Ship : IShip
    {
        public List<(int x, int y)> Fields { get; set; }
        public int Length { get; set; }

        public Ship(int length)
        {
            Length = length;
            Fields = new List<(int x, int y)>();
        }

        public List<(int x, int y)> Place((int x, int y) initialPosition, ShipDirection shipDirection)
        {
            if (shipDirection == ShipDirection.Horizontal)
                PlaceHorizontal(initialPosition);

            if (shipDirection == ShipDirection.Vertical)
                PlaceVertical(initialPosition);

            return Fields;
        }
        private void PlaceHorizontal((int x, int y) initialPosition)
        {
            for (int i = 0; i < Length; i++)
            {
                AddPosition(initialPosition.x + i, initialPosition.y);
            }
        }
        private void PlaceVertical((int x, int y) initialPosition)
        {
            for (int i = 0; i < Length; i++)
            {
                AddPosition(initialPosition.x, initialPosition.y + i);
            }
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
