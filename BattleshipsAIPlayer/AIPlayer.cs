using Battleships;
using Battleships.Enums;
using Battleships.Exceptions;
using Battleships.Fields.Interfaces;
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

        public delegate FiredShotResult PerformShot(int x, int y);
        private PerformShot ShotDelegate;

        public AIPlayer(Board board, IPositionRandomizer positionRandomizer, IAvailableShipPositionsFinder positionFinder) 
            : this(board, positionRandomizer, positionFinder, null) { }

        public AIPlayer(Board board, IPositionRandomizer positionRandomizer, IAvailableShipPositionsFinder positionFinder, PerformShot shotDelegate)
        {
            AvailableShotPositions = new List<(int x, int y)>();
            PreferredShotPositions = new List<(int x, int y)>();

            Board = board;
            GetInitialAvailablePositions();
            PositionRandomizer = positionRandomizer;
            PositionFinder = positionFinder;

            ShotDelegate = shotDelegate;
        }

        private void GetInitialAvailablePositions()
        {
            for (int x = 1; x <= Board.Fields.GetUpperBound(0) + 1; x++)
            {
                for (int y = 1; y <= Board.Fields.GetUpperBound(1) + 1; y++)
                {
                    AvailableShotPositions.Add((x, y));
                }
            }
        }

        public (FiredShotResult result, (int x, int y) position) ExecuteTurn()
        {
            var shot = MakeShot();
            var result = ShotDelegate.Invoke(shot.x, shot.y);
            ProcessShotResult(result, shot);
            return (result, position: shot);
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

        public (int x, int y) MakeShot()
        {
            if (PreferredShotPositions.Count > 0)
                return PositionRandomizer.ReturnRandomShot(PreferredShotPositions.ToArray());

            return PositionRandomizer.ReturnRandomShot(AvailableShotPositions.ToArray());
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
                case FiredShotResultType.ShipHitAndSink:
                    ProcessShipHitAndSkinkShotResult(positionShot, result.SinkedShip);
                    break;
                case FiredShotResultType.ShipHitAndAllShipsSinked:
                    ProcessShipHitAndAllSkinkedShotResult();
                    break;
            }
        }

        private void ProcessMissedShotResult((int x, int y) position)
        {
            RemovePositionFromShotsLists(position);
        }
        private void ProcessShipHitShotResult((int x, int y) position)
        {
            RemovePositionFromShotsLists(position);
            PopulatePreferredPositionsAroundField(position);
        }
        private void ProcessShipHitAndSkinkShotResult((int x, int y) position, IShip ship)
        {
            RemovePositionFromShotsLists(position);
            RemovePositionsFromSinkedShip(ship);
        }
        private void ProcessShipHitAndAllSkinkedShotResult()
        {
            AvailableShotPositions.Clear();
            PreferredShotPositions.Clear();
        }

        private void RemovePositionFromShotsLists((int x, int y) position)
        {
            if (AvailableShotPositions.Contains(position))
                AvailableShotPositions.Remove(position);

            if (PreferredShotPositions.Contains(position))
                PreferredShotPositions.Remove(position);
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
        private void RemovePositionsFromSinkedShip(IShip ship)
        {
            foreach(var field in ship.Fields)
            {
                for (int x = field.x - 1; x <= field.x + 1; x++)
                {
                    for (int y = field.y - 1; y <= field.y + 1; y++)
                    {
                        RemovePositionFromShotsLists((x, y));
                    }
                }
            }
        }
    }
}
