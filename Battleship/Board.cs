using Battleship.Enums;
using Battleship.Exceptions;
using Battleship.Interfaces;
using Battleship.ShipConstraintsConfiguration;
using System;
using System.Collections.Generic;

namespace Battleship
{
    public class Board
    {
        const int DEFAULT_SIZE = 10;

        public IPlacable[,] Fields { get; set; }

        public List<Ship> Ships { get; set; }
        public ShipConstraintsBase ShipConstraints { get; set; }

        public Board(ShipConstraintsBase shipConstraints) : this(DEFAULT_SIZE, shipConstraints) { }
        public Board(int size, ShipConstraintsBase shipConstraints)
        {
            ShipConstraints = shipConstraints;
            Fields = new IPlacable[size, size];
            Ships = new List<Ship>();
        }

        public Ship PlaceShip(int shipLength, (int x, int y) initialPosition, ShipDirection direction)
        {
            CheckIfShipIsAllowed(shipLength);

            var ship = new Ship(shipLength);
            ship.Place(initialPosition, direction);

            CheckIfShipOutOfBounds(ship);
            CheckIfShipOverlaps(ship);
            CheckIfShipInCloseProximityWithOtherShip(ship);
            
            AddShipToBoard(ship);
            PopulateShipFields(ship);

            return ship;
        }

        private void CheckIfShipIsAllowed(int shipLength)
        {
            if (!ShipConstraints.CheckIfShipAllowed(shipLength))
                throw new ShipNotAllowedException();
        }

        private void CheckIfShipOutOfBounds(Ship ship)
        {
            foreach(var field in ship.Fields)
            {
                if (field.x < Fields.GetLowerBound(0) + 1 
                    || field.y < Fields.GetLowerBound(1) + 1
                    || field.x > Fields.GetUpperBound(0) + 1
                    || field.y > Fields.GetUpperBound(1) + 1)
                    throw new InvalidPositionOutOfBoundsException("Ship out of bounds");
            }
        }

        private void CheckIfShipOverlaps(Ship ship)
        {
            foreach (var field in ship.Fields)
            {
                if (Fields[field.x - 1, field.y - 1] != null)
                    throw new InvalidPositionOverlapException("Ship overlaps with other ship");
            }
        }

        private void CheckIfShipInCloseProximityWithOtherShip(Ship ship)
        {
            foreach(var field in ship.Fields)
            {
                for(int x = field.x - 2; x <= field.x + 1; x++)
                {
                    for(int y = field.y - 2; y <= field.y; y++)
                    {
                        if (x >= Fields.GetLowerBound(0) && y >= Fields.GetLowerBound(1)
                            && x < Fields.GetUpperBound(0) && y < Fields.GetUpperBound(1)
                            && Fields[x, y] != null && Fields[x, y] != ship)
                            throw new InvalidPositionProximityException("Ship to close to other ship.");
                    }
                }
            }
        }

        private void AddShipToBoard(Ship ship)
        {
            Ships.Add(ship);
            ShipConstraints.AddExistingShip(ship.Length);
        }

        private void PopulateShipFields(Ship ship)
        {
            foreach (var field in ship.Fields)
                Fields[field.x - 1, field.y - 1] = ship;
        }
    }
}
