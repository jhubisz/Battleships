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

        public IField[,] Fields { get; set; }
        public IFieldsFactory FieldsFactory { get; }

        public List<IShip> Ships { get; set; }
        public List<IShip> SinkedShips { get; set; }
        public ShipConstraintsBase ShipConstraints { get; set; }

        public Board(ShipConstraintsBase shipConstraints, IFieldsFactory fieldsFactory)
            : this(DEFAULT_SIZE, shipConstraints, fieldsFactory) { }
        public Board(int size, ShipConstraintsBase shipConstraints, IFieldsFactory fieldsFactory)
        {
            ShipConstraints = shipConstraints;
            FieldsFactory = fieldsFactory;
            PopulateEmptyBoard(size);

            Ships = new List<IShip>();
            SinkedShips = new List<IShip>();
        }

        public FiredShotResult CheckFiredShot(int x, int y)
        {
            var result = Fields[x - 1, y - 1].CheckHit(x, y);
            Fields[x - 1, y - 1] = result.resultField;
            var firedShotResult = CheckAllShipsSinked(result.result);
            return firedShotResult;
        }

        private FiredShotResult CheckAllShipsSinked(FiredShotResult result)
        {
            if (result.ResultType != FiredShotResultType.ShipHitAndSink)
                return result;

            AddSinkedShip(result);

            if (SinkedShips.Count == Ships.Count)
                result.ResultType = FiredShotResultType.ShipHitAndAllShipsSinked;

            return result;
        }

        private void AddSinkedShip(FiredShotResult result)
        {
            if (!SinkedShips.Contains(result.SinkedShip))
                SinkedShips.Add(result.SinkedShip);
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

        private void PopulateEmptyBoard(int size)
        {
            Fields = new IField[size, size];
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    Fields[x, y] = FieldsFactory.CreateEmptyField();
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
                if (Fields[field.x - 1, field.y - 1] is IShip)
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
                            && Fields[x, y] is IShip && Fields[x, y] != ship)
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
