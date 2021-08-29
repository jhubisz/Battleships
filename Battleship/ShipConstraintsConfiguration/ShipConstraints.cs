namespace Battleships.ShipConstraintsConfiguration
{
    public class ShipConstraints : ShipConstraintsBase
    {
        public override void BuildAllowedShipsCollection()
        {
            AllowedShips.Add(2, 4); // 4 ships of length 2
            AllowedShips.Add(3, 3); // 3 ships of length 3
            AllowedShips.Add(4, 2); // 2 ships of length 4
            AllowedShips.Add(5, 1); // 1 ship of length 5
        }
    }
}
