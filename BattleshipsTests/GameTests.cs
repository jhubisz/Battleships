using Battleships;
using Battleships.Enums;
using Battleships.Exceptions;
using BattleshipsTests.Mocks;
using Xunit;

namespace BattleshipsTests
{
    public class GameTests
    {
        public BattleshipsGame<ShipConstraintsMock> Game { get; private set; }

        public GameTests()
        {
            Game = new BattleshipsGame<ShipConstraintsMock>();
        }

        [Fact]
        public void HasTwoBoardsAfterGameInitializeIsRun()
        {
            Game.InitializeGame();

            Assert.IsType<Board>(Game.PlayerABoard);
            Assert.IsType<Board>(Game.PlayerBBoard);
            Assert.Equal(GameStatus.Initialized, Game.Status);
        }

        [Fact]
        public void ThrowMissingShipsExceptionIfNotAllShipsPlacedBeforeGameStart()
        {
            Game.InitializeGame();

            Assert.Throws<MissingShipsException>(() => Game.StartGame());
        }

        [Fact]
        public void ThrowMissingShipsExceptionIfNotAllShipsForOneOfThePlayersPlacedBeforeGameStart()
        {
            Game.InitializeGame();

            Assert.Throws<MissingShipsException>(() => Game.StartGame());
        }

        [Fact]
        public void HasStartedGameStatusAfterGameStart()
        {
            StartGame();

            Assert.Equal(GameStatus.Started, Game.Status);
        }

        [Fact]
        public void HasPlayerTurnSetToPlayerAAfterGameStart()
        {
            StartGame();

            Assert.Equal(GamePlayer.PlayerA, Game.PlayerTurn);
        }

        [Fact]
        public void ReturnsMissedShotAndChangesTurnAfterPlayerAMissedShot()
        {
            StartGame();

            var result = Game.PlayerAShot(5, 5);

            Assert.IsType<FiredShotResult>(result);
            Assert.Equal(FiredShotResultType.ShotMissed, result.ResultType);
            Assert.Equal(GamePlayer.PlayerB, Game.PlayerTurn);
        }

        [Fact]
        public void ReturnsMissedShotAndChangesTurnAfterPlayerBMissedShot()
        {
            StartGame();

            Game.PlayerAShot(5, 5);
            var result = Game.PlayerBShot(5, 5);

            Assert.IsType<FiredShotResult>(result);
            Assert.Equal(FiredShotResultType.ShotMissed, result.ResultType);
            Assert.Equal(GamePlayer.PlayerA, Game.PlayerTurn);
        }

        [Fact]
        public void ThrowsInvalidPlayerTurnExceptionIfPlayerAShotsTwice()
        {
            StartGame();

            Game.PlayerAShot(5, 5);

            Assert.Throws<InvalidPlayerTurnException>(() => Game.PlayerAShot(5, 5));
        }

        [Fact]
        public void ThrowsInvalidPlayerTurnExceptionIfPlayerBShotsTwice()
        {
            StartGame();

            Game.PlayerAShot(5, 5);
            Game.PlayerBShot(5, 5);

            Assert.Throws<InvalidPlayerTurnException>(() => Game.PlayerBShot(5, 5));
        }

        private void StartGame()
        {
            Game.InitializeGame();

            Game.PlayerABoard.PlaceShip(2, (1, 1), ShipDirection.Horizontal);
            Game.PlayerABoard.PlaceShip(3, (1, 3), ShipDirection.Horizontal);

            Game.PlayerBBoard.PlaceShip(2, (1, 1), ShipDirection.Horizontal);
            Game.PlayerBBoard.PlaceShip(3, (1, 3), ShipDirection.Horizontal);

            Game.StartGame();
        }
    }
}
