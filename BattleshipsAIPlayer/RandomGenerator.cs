using BattleshipsAIPlayer.Interfaces;
using System;

namespace BattleshipsAIPlayer
{
    public class RandomGenerator : IRandomGenerator
    {
        private Random Random { get; set; }

        public RandomGenerator()
        {
            Random = new Random();
        }

        public int Next(int length)
        {
            return Random.Next(length);            
        }
    }
}
