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
            Game.InitializeGame();

            Game.PlayerABoard.PlaceShip(2, (1, 1), ShipDirection.Horizontal);
            Game.PlayerABoard.PlaceShip(3, (1, 3), ShipDirection.Horizontal);
            
            Game.PlayerBBoard.PlaceShip(2, (1, 1), ShipDirection.Horizontal);
            Game.PlayerBBoard.PlaceShip(3, (1, 3), ShipDirection.Horizontal);

            Game.StartGame();

            Assert.Equal(GameStatus.Started, Game.Status);
        }
    }
}
