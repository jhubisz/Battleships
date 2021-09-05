using System.Collections.Generic;

namespace Battleships.Fields
{
    public interface IPlacable
    {
        List<(int x, int y)> Fields { get; }

        void AddPosition(int x, int y);
        bool CheckIfPositionExists(int x, int y);
    }
}
