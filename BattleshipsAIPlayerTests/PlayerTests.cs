using Battleships;
using BattleshipsAIPlayer;
using BattleshipsTests.TestDoubles.Mocks;
using TestDoubles.Mocks;
using Xunit;

namespace BattleshipsAIPlayerTests
{
    public class PlayerTests
    {
        public BattleshipsGame<ShipConstraintsMock> Game { get; set; }

        public PlayerTests()
        {
            Game = new BattleshipsGame<ShipConstraintsMock>();
            Game.InitializeGame();
        }
        
        [Fact]
        public void PlayerAIInstanceIsCreatedAndAcceptsBoardAndPlacesShips()
        {
            var player = new AIPlayer(new PositionRandomizer(new RandomGeneratorMock(new int[] { 1 })));
            player.Board = Game.PlayerABoard;
            player.PlaceShips();
        }
    }
}
