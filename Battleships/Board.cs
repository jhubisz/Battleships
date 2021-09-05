using Battleships.Enums;
using Battleships.Exceptions;
using Battleships.Factories;
using Battleships.Fields.Interfaces;
using Battleships.ShipConstraintsConfiguration;
using System.Collections.Generic;

namespace Battleships
{
    public class Board
    {
        const int DEFAULT_SIZE = 10;

        public IPlacable[,] Fields { get; set; }
        public IFieldsFactory FieldsFactory { get; }

        public List<IShip> Ships { get; set; }
        public ShipConstraintsBase ShipConstraints { get; set; }

        public Board(ShipConstraintsBase shipConstraints, IFieldsFactory fieldsFactory)
            : this(DEFAULT_SIZE, shipConstraints, fieldsFactory) { }
        public Board(int size, ShipConstraintsBase shipConstraints, IFieldsFactory fieldsFactory)
        {
            ShipConstraints = shipConstraints;
            FieldsFactory = fieldsFactory;

            Fields = new IPlacable[size, size];
            Ships = new List<IShip>();
        }

        public FiredShotResult CheckFiredShot(int x, int y)
        {
            if (Fields[x - 1, y - 1] == null)
            {
                Fields[x - 1, y - 1] = FieldsFactory.CreateMissedShotMarker(x, y);
                return new FiredShotResult { Hit = false };
            }

            return new FiredShotResult { Hit = true, ResultType = FiredShotResultType.ShipHit };
        }

        public IShip PlaceShip(int shipLength, (int x, int y) initialPosition, ShipDirection direction)
        {
            CheckIfShipIsAllowed(shipLength);

            var ship = FieldsFactory.CreateShip(shipLength);
            ship.Place(initialPosition, direction);

            CheckIfShipOutOfBounds(ship);
            CheckIfShipOverlaps(ship);
            CheckIfShipInCloseProximityWithOtherShip(ship);

            AddShipToBoard(ship);
            PopulateShipFields(ship);

            return ship;
        }

        public object ReturnField(int x, int y)
        {
            return Fields[x - 1, y - 1];
        }

        private void CheckIfShipIsAllowed(int shipLength)
        {
            if (!ShipConstraints.CheckIfShipAllowed(shipLength))
                throw new ShipNotAllowedException();
        }

        private void CheckIfShipOutOfBounds(IShip ship)
        {
            foreach (var field in ship.Fields)
            {
                if (field.x < Fields.GetLowerBound(0) + 1
                    || field.y < Fields.GetLowerBound(1) + 1
                    || field.x > Fields.GetUpperBound(0) + 1
                    || field.y > Fields.GetUpperBound(1) + 1)
                    throw new InvalidPositionOutOfBoundsException("Ship out of bounds");
            }
        }

        private void CheckIfShipOverlaps(IShip ship)
        {
            foreach (var field in ship.Fields)
            {
                if (Fields[field.x - 1, field.y - 1] != null)
                    throw new InvalidPositionOverlapException("Ship overlaps with other ship");
            }
        }

        private void CheckIfShipInCloseProximityWithOtherShip(IShip ship)
        {
            foreach (var field in ship.Fields)
            {
                for (int x = field.x - 2; x <= field.x + 1; x++)
                {
                    for (int y = field.y - 2; y <= field.y; y++)
                    {
                        if (x >= Fields.GetLowerBound(0) && y >= Fields.GetLowerBound(1)
                            && x < Fields.GetUpperBound(0) && y < Fields.GetUpperBound(1)
                            && Fields[x, y] != null && Fields[x, y] != ship)
                            throw new InvalidPositionProximityException("Ship to close to other ship.");
                    }
                }
            }
        }

        private void AddShipToBoard(IShip ship)
        {
            Ships.Add(ship);
            ShipConstraints.AddExistingShip(ship.Length);
        }

        private void PopulateShipFields(IShip ship)
        {
            foreach (var field in ship.Fields)
                Fields[field.x - 1, field.y - 1] = ship;
        }
    }
}
