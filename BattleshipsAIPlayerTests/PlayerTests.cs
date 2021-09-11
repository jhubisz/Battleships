using Battleships;
using BattleshipsAIPlayer;
using BattleshipsTests.TestDoubles.Mocks;
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
            var player = new AIPlayer();
            player.Board = Game.PlayerABoard;
            player.PlaceShips();
        }
    }
}
