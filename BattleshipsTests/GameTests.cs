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
    }
}
