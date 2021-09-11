using System;

namespace BattleshipsAIPlayer
{
    public class PositionRandomizer
    {
        public (int x, int y) ReturnRandomPosition((int x, int y)[] positions)
        {
            Random rand = new Random();
            return positions[rand.Next(positions.Length)];
        }
    }
}