using BattleshipsAIPlayer.Interfaces;

namespace TestDoubles.Mocks
{
    public class RandomGeneratorMock : IRandomGenerator
    {
        private int[] returnNumbers;
        private int index;

        public RandomGeneratorMock(int[] returnNumbersList)
        {
            returnNumbers = returnNumbersList;
        }

        public int Next(int length)
        {
            var currentNumber = returnNumbers[index];
            index = ++index >= returnNumbers.Length ? 0 : index;

            return currentNumber >= length ? length - 1 : currentNumber;
        }
    }
}
