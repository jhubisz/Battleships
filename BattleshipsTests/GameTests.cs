using Battleships;
using Battleships.Enums;
using Battleships.Exceptions;
using BattleshipsTests.TestDoubles.Mocks;
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
        public void HasTwoBoardsAndCorrectPropertiesAfterGameInitializeIsRun()
        {
            Game.InitializeGame();

            Assert.IsType<Board>(Game.PlayerABoard);
            Assert.IsType<Board>(Game.PlayerBBoard);
            Assert.Equal(GameStatus.Initialized, Game.Status);
            Assert.Null(Game.Winner);
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

        [Fact]
        public void ReturnsAllShipsSinkedAndSetsGameWinnerToPlayerAWhenAllShipsSinked()
        {
            StartGame();

            Game.PlayerAShot(1, 1);
            Game.PlayerBShot(5, 5);

            Game.PlayerAShot(1, 3);
            Game.PlayerBShot(5, 5);

            var result = Game.PlayerAShot(2, 3);

            Assert.Equal(FiredShotResultType.ShipHitAndAllShipsSinked, result.ResultType);
            Assert.Equal(GameStatus.Finished, Game.Status);
            Assert.Equal(GamePlayer.PlayerA, Game.Winner);
        }

        [Fact]
        public void ReturnsAllShipsSinkedAndSetsGameWinnerToPlayerBWhenAllShipsSinked()
        {
            StartGame();

            Game.PlayerAShot(5, 5);
            Game.PlayerBShot(1, 1);

            Game.PlayerAShot(5, 5);
            Game.PlayerBShot(1, 3);
            
            Game.PlayerAShot(5, 5);
            var result = Game.PlayerBShot(2, 3);

            Assert.Equal(FiredShotResultType.ShipHitAndAllShipsSinked, result.ResultType);
            Assert.Equal(GameStatus.Finished, Game.Status);
            Assert.Equal(GamePlayer.PlayerB, Game.Winner);
        }

        [Fact]
        public void ThrowsGameFinishedExceptionAfterGameIsFinished()
        {
            StartGame();

            Game.PlayerAShot(5, 5);
            Game.PlayerBShot(1, 1);

            Game.PlayerAShot(5, 5);
            Game.PlayerBShot(1, 3);

            Game.PlayerAShot(5, 5);
            var result = Game.PlayerBShot(2, 3);

            Assert.Equal(FiredShotResultType.ShipHitAndAllShipsSinked, result.ResultType);
            Assert.Equal(GameStatus.Finished, Game.Status);
            Assert.Throws<GameFinishedException>(() => Game.PlayerAShot(1,1));
            Assert.Throws<GameFinishedException>(() => Game.PlayerBShot(1,1));
        }

        [Fact]
        public void GameIsProperlyRestartedWhenInitializeIsRun()
        {
            StartGame();

            Game.PlayerAShot(5, 5);
            Game.PlayerBShot(1, 1);

            Game.PlayerAShot(5, 5);
            Game.PlayerBShot(1, 3);

            Game.PlayerAShot(5, 5);
            Game.PlayerBShot(2, 3);

            Game.InitializeGame();

            Assert.IsType<Board>(Game.PlayerABoard);
            Assert.IsType<Board>(Game.PlayerBBoard);
            Assert.Empty(Game.PlayerABoard.Ships);
            Assert.Empty(Game.PlayerBBoard.Ships);
            Assert.Equal(GameStatus.Initialized, Game.Status);
            Assert.Null(Game.Winner);
        }

        private void StartGame()
        {
            Game.InitializeGame();

            Game.PlayerABoard.PlaceShip(1, (1, 1), ShipDirection.Horizontal);
            Game.PlayerABoard.PlaceShip(2, (1, 3), ShipDirection.Horizontal);

            Game.PlayerBBoard.PlaceShip(1, (1, 1), ShipDirection.Horizontal);
            Game.PlayerBBoard.PlaceShip(2, (1, 3), ShipDirection.Horizontal);

            Game.StartGame();
        }
    }
}
