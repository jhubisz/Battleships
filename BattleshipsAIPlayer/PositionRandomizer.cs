using BattleshipsAIPlayer.Interfaces;
using System;

namespace BattleshipsAIPlayer
{
    public class PositionRandomizer : IPositionRandomizer
    {
        public IRandomGenerator RandomGenerator { get; set; }

        public PositionRandomizer(IRandomGenerator randomGenerator)
        {
            RandomGenerator = randomGenerator;
        }

        public (int x, int y) ReturnRandomPosition((int x, int y)[] positions)
        {
            return positions[RandomGenerator.Next(positions.Length)];
        }
    }
}