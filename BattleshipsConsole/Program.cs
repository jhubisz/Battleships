using Battleships;
using Battleships.Enums;
using Battleships.Fields;
using Battleships.Fields.Interfaces;
using Battleships.ShipConstraintsConfiguration;
using BattleshipsAIPlayer;
using System;
using System.Threading;

namespace BattleshipsConsole
{
    class Program
    {
        const int MOVES_DELAY_IN_MILLISECONDS = 1000;
        private static BattleshipsGame<ShipConstraints> game;

        static void Main(string[] args)
        {
            Thread thread = new Thread(RunGame);
            thread.Start();
        }

        private static void RunGame()
        {
            try
            {
                game = InitializeGame();
                var players = InitializeAIPlayers(game);

                RenderBoards(game.PlayerABoard, game.PlayerBBoard);

                game.StartGame();
                while (game.Status != GameStatus.Finished)
                {
                    if (game.PlayerTurn == GamePlayer.PlayerA)
                        RenderMove(game.PlayerTurn, players.playerA.ExecuteTurn());
                    else
                        RenderMove(game.PlayerTurn, players.playerB.ExecuteTurn());

                    RenderBoards(game.PlayerABoard, game.PlayerBBoard);

                    Thread.Sleep(MOVES_DELAY_IN_MILLISECONDS);
                }

                Console.WriteLine("And the winner is: " + game.Winner.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static BattleshipsGame<ShipConstraints> InitializeGame()
        {
            var game = new BattleshipsGame<ShipConstraints>();
            game.InitializeGame();
            return game;
        }

        private static (AIPlayer playerA, AIPlayer playerB) InitializeAIPlayers(BattleshipsGame<ShipConstraints> game)
        {
            var randomGenerator = new RandomGenerator();
            var randomizer = new PositionRandomizer(randomGenerator);
            var positionFinder = new AvailableShipPositionsFinder();

            var playerA = new AIPlayer(game.PlayerABoard, randomizer, positionFinder, new AIPlayer.PerformShot(game.PlayerAShot));
            playerA.PlaceShips();
            var playerB = new AIPlayer(game.PlayerBBoard, randomizer, positionFinder, new AIPlayer.PerformShot(game.PlayerBShot));
            playerB.PlaceShips();

            return (playerA, playerB);
        }

        private static void RenderMove(GamePlayer player, (FiredShotResult result, (int x, int y) position) move)
        {
            Console.WriteLine($"{player} shoots x: {move.position.x}, y: {move.position.y}, and {move.result.ResultType}");
        }

        private static void RenderBoards(Board boardA, Board boardB)
        {
            for (int line = 0; line < boardA.Fields.GetLength(0); line++)
            {
                RenderBoardLine(boardA, line);
                Console.Write("\t");
                RenderBoardLine(boardB, line);

                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void RenderBoardLine(Board board, int line)
        {
            for (int field = 0; field < board.Fields.GetLength(1); field++)
            {
                Console.BackgroundColor = GetPrintColor(board.Fields[field, line], field, line);
                Console.Write(GetCharForPrinting(board.Fields[field, line]) + " ");
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        private static ConsoleColor GetPrintColor(IField field, int x, int y)
        {
            if (field is MissedShotMarker) return ConsoleColor.Red;
            if (field is Ship)
            {
                return ((Ship)field).HitFields.Contains((x + 1, y + 1)) ? ConsoleColor.Red : ConsoleColor.White;
            }

            return ConsoleColor.Blue;
        }

        private static char GetCharForPrinting(IField field)
        {
            if (field is MissedShotMarker) return 'X';
            if (field is Ship) return ((Ship)field).Length.ToString()[0];

            return '░';
        }
    }
}
