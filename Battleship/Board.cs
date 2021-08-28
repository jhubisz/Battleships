using Battleship.Enums;
using Battleship.Interfaces;
using System;
using System.Collections.Generic;

namespace Battleship
{
    public class Board
    {
        const int DEFAULT_SIZE = 10;

        public IPlacable[,] Fields { get; set; }

        public List<Ship> Ships { get; set; }

        public Board() : this(DEFAULT_SIZE) { }
        public Board(int size)
        {
            Fields = new IPlacable[size, size];
            Ships = new List<Ship>();
        }

        public Ship PlaceShip(int shipLength, (int x, int y) initialPosition, ShipDirection direction)
        {
            var ship = new Ship(shipLength);
            ship.Place(initialPosition, direction);

            Ships.Add(ship);
            PopulateShipFields(ship);
            
            return ship;
        }

        private void PopulateShipFields(Ship ship)
        {
            foreach (var field in ship.Fields)
                Fields[field.x - 1, field.y - 1] = ship;
        }
    }
}
