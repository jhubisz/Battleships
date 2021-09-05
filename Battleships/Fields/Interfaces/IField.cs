using System.Collections.Generic;

namespace Battleships.Fields.Interfaces
{
    public interface IField
    {
        List<(int x, int y)> Fields { get; }

        void AddPosition(int x, int y);
        bool CheckIfPositionExists(int x, int y);

        (FiredShotResult result, IField resultField) CheckHit(int x, int y);
    }
}
