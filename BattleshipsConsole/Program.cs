using Battleships;
using Battleships.Fields;
using Battleships.Fields.Interfaces;
using Battleships.ShipConstraintsConfiguration;
using BattleshipsAIPlayer;
using System;

namespace BattleshipsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintBoards(1);
        }

        private static void PrintBoards(int noOfBoards)
        {
            for(int i = 1; i <= noOfBoards; i++)
            {
                var game = new BattleshipsGame<ShipConstraints>();
                game.InitializeGame();

                var randomGenerator = new RandomGenerator();
                var randomizer = new PositionRandomizer(randomGenerator);
                var positionFinder = new AvailableShipPositionsFinder();
                var player = new AIPlayer(game.PlayerABoard, randomizer, positionFinder);
                player.PlaceShips();

                RenderBoard(game.PlayerABoard);
            }
        }

        private static void RenderBoard(Board board)
        {
            for (int x = 0; x < board.Fields.GetLength(0); x++)
            {
                for (int y = 0; y < board.Fields.GetLength(1); y++)
                {
                    Console.Write(GetCharForPrinting(board.Fields[x, y]) + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static char GetCharForPrinting(IField field)
        {
            if (field is MissedShotMarker) return 'X';
            if (field is Ship) return ((Ship)field).Length.ToString()[0];

            return '░';
        }
    }
}
