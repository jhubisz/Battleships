using Battleships;
using Battleships.Enums;
using Battleships.Exceptions;
using BattleshipsAIPlayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleshipsAIPlayer
{
    public class AIPlayer
    {
        public Board Board { get; }
        public List<(int x, int y)> AvailableShotPositions { get; private set; }
        public List<(int x, int y)> PreferredShotPositions { get; private set; }

        public IPositionRandomizer PositionRandomizer { get; set; }
        public IAvailableShipPositionsFinder PositionFinder { get; }

        public AIPlayer(Board board, IPositionRandomizer positionRandomizer, IAvailableShipPositionsFinder positionFinder)
        {
            AvailableShotPositions = new List<(int x, int y)>();
            PreferredShotPositions = new List<(int x, int y)>();

            Board = board;
            GetInitialAvailablePositions();
            PositionRandomizer = positionRandomizer;
            PositionFinder = positionFinder;
        }

        private void GetInitialAvailablePositions()
        {
            for (int x = 1; x < Board.Fields.GetUpperBound(0) + 1; x++)
            {
                for (int y = 1; y < Board.Fields.GetUpperBound(0) + 1; y++)
                {
                    AvailableShotPositions.Add((x, y));
                }
            }
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

        public void ProcessShotResult(FiredShotResult result, (int x, int y) positionShot)
        {
            switch (result.ResultType)
            {
                case FiredShotResultType.ShotMissed:
                    ProcessMissedShotResult(positionShot);
                    break;
                case FiredShotResultType.ShipHit:
                    ProcessShipHitShotResult(positionShot);
                    break;
            }
        }

        private void ProcessMissedShotResult((int x, int y) position)
        {
            if (AvailableShotPositions.Contains(position))
                AvailableShotPositions.Remove(position);
        }

        private void ProcessShipHitShotResult((int x, int y) position)
        {
            if (AvailableShotPositions.Contains(position))
                AvailableShotPositions.Remove(position);

            PopulatePreferredPositionsAroundField(position);
        }

        private void PopulatePreferredPositionsAroundField((int x, int y) position)
        {
            for (int x = position.x - 1; x <= position.x + 1; x++)
            {
                for (int y = position.y - 1; y <= position.y + 1; y++)
                {
                    if (AvailableShotPositions.Contains((x, y))
                        && !PreferredShotPositions.Contains((x, y)))
                        PreferredShotPositions.Add((x, y));
                        
                }
            }
        }

        public (int x, int y) MakeShot()
        {
            var shotPosition = PositionRandomizer.ReturnRandomShot(AvailableShotPositions.ToArray());
            return shotPosition;
        }
    }
}
