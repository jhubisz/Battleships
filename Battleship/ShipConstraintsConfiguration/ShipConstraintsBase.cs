using System.Collections.Generic;

namespace Battleship.ShipConstraintsConfiguration
{
    public abstract class ShipConstraintsBase
    {
        /// <summary>
        /// index determines the length of ship, value determines the no of allowed ships
        /// </summary>
        public Dictionary<int, int> AllowedShips { get; }
        /// <summary>
        /// index determines the length of ship, value determines the no of allowed ships
        /// </summary>
        public Dictionary<int, int> ExistingShips { get; }

        public ShipConstraintsBase()
        {
            AllowedShips = new Dictionary<int, int>();
            ExistingShips = new Dictionary<int, int>();

            BuildAllowedShipsCollection();
        }

        public abstract void BuildAllowedShipsCollection();

        public bool CheckIfShipAllowed(int shipLength)
        {
            if (!AllowedShips.ContainsKey(shipLength))
                return false;

            if (ExistingShips.ContainsKey(shipLength)
                && ExistingShips[shipLength] >= AllowedShips[shipLength])
                return false;

            return true;
        }

        public void AddExistingShip(int shipLength)
        {
            if (!ExistingShips.ContainsKey(shipLength))
                ExistingShips.Add(shipLength, 0);

            ExistingShips[shipLength]++;
        }
    }
}
