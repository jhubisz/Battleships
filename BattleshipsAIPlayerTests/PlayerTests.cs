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
        }
        
        [Fact]
        public void PlayerAIInstanceIsCreatedAndAcceptsBoard()
        {
            var player = new AIPlayer();
            player.Board = Game.PlayerABoard;
        }
    }
}
