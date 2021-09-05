using Battleships.ShipConstraintsConfiguration;

namespace BattleshipsTests.Mocks
{
    public class ShipConstraintsMock : ShipConstraintsBase
    {
        public override void BuildAllowedShipsCollection()
        {
            AllowedShips.Add(2, 1); // one ship of length 2
            AllowedShips.Add(3, 1); // one ship of length 3
        }
    }
}
