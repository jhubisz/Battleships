using Battleships.ShipConstraintsConfiguration;

namespace BattleshipsTests.Mocks
{
    public class ShipConstraintsMock : ShipConstraintsBase
    {
        public override void BuildAllowedShipsCollection()
        {
            AllowedShips.Add(1, 1); // one ship of length 1
            AllowedShips.Add(2, 1); // one ship of length 2
        }
    }
}
