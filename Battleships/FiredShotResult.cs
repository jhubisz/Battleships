using Battleships.Enums;
using Battleships.Fields.Interfaces;

namespace Battleships
{
    public class FiredShotResult
    {
        public bool Hit { get; set; }
        public FiredShotResultType ResultType { get; set;}
        public IShip SinkedShip { get; set; }
    }
}
