using Battleship.Interfaces;
using System.Collections.Generic;

namespace Battleship
{
    public class Ship : IPlacable
    {
        public List<(int x, int y)> Fields { get; set; }

        public void AddPosition(int x, int y)
        {
            throw new System.NotImplementedException();
        }
    }
}
