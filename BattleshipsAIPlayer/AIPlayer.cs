using Battleships;
using Battleships.Enums;
using Battleships.Exceptions;
using BattleshipsAIPlayer.Interfaces;
using System;
using System.Linq;

namespace BattleshipsAIPlayer
{
    public class AIPlayer
    {
        public Board Board { get; set; }

        public IPositionRandomizer PositionRandomizer { get; }
        public IAvailableShipPositionsFinder PositionFinder { get; }

        public AIPlayer(IPositionRandomizer positionRandomizer, IAvailableShipPositionsFinder positionFinder)
        {
            PositionRandomizer = positionRandomizer;
            PositionFinder = positionFinder;
        }

        public void PlaceShips()
        {
            var ships = Board.ShipConstraints.AllowedShips.Keys.ToArray();
            Array.Sort(ships);
            Array.Reverse(ships);

            foreach (var ship in ships)
            {
                for (int i = 0; i < Board.ShipConstraints.AllowedShips[ship]; i++)
                {
                    var positions = PositionFinder.FindAllPositions(ship, Board);

                    if (positions.Length == 0)
                        throw new NoAvailableShipPositionsException();

                        var shipsPosition = PositionRandomizer.ReturnRandomPosition(positions);
                    var position = (shipsPosition.x, shipsPosition.y);
                    Board.PlaceShip(ship, position, shipsPosition.direction);
                }
            }
        }
    }
}
