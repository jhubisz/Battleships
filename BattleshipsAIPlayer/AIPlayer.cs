using Battleships;
using Battleships.Enums;
using BattleshipsAIPlayer.Interfaces;
using System;

namespace BattleshipsAIPlayer
{
    public class AIPlayer
    {
        public Board Board { get; set; }

        public IPositionRandomizer PositionRandomizer { get; set; }

        public AIPlayer(IPositionRandomizer positionRandomizer)
        {
            PositionRandomizer = positionRandomizer;
        }

        public void PlaceShips()
        {
            foreach(var ship in Board.ShipConstraints.AllowedShips.Keys)
            {
                for (int i = 0; i < Board.ShipConstraints.AllowedShips[ship]; i++)
                {
                    var positionsPlaceholder = new (int x, int y)[] { (1, ship * 2 ) };
                    var position = PositionRandomizer.ReturnRandomPosition(positionsPlaceholder);
                    Board.PlaceShip(ship, position, ShipDirection.Horizontal);
                }
            }
        }
    }
}
