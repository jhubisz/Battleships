using BattleshipsAIPlayer.Interfaces;
using System;

namespace BattleshipsAIPlayer
{
    class RandomGenerator : IRandomGenerator
    {
        private Random Random { get; set; }

        public RandomGenerator()
        {
            var Random = new Random();
        }

        public int Next(int length)
        {
            return Random.Next(length);            
        }
    }
}
