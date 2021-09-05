using Battleships.Enums;
using Battleships.Exceptions;
using Battleships.Factories;
using Battleships.ShipConstraintsConfiguration;
using System;

namespace Battleships
{
    public class BattleshipsGame<T> where T : ShipConstraintsBase, new()
    {
        public Board PlayerABoard { get; set; }
        public Board PlayerBBoard { get; set; }

        public GameStatus Status { get; set; }
        public GamePlayer PlayerTurn { get; set; }

        public bool AllShipsPlaced
        {
            get
            {
                return PlayerABoard.ShipConstraints.AllShipsPlaced
                    && PlayerBBoard.ShipConstraints.AllShipsPlaced;
            }
        }

        public void StartGame()
        {
            if (!AllShipsPlaced)
                throw new MissingShipsException();

            Status = GameStatus.Started;
        }

        public void InitializeGame()
        {
            var fieldsFactory = new FieldsFactory();
            Status = GameStatus.Initialized;
            PlayerABoard = new Board(new T(), fieldsFactory);
            PlayerBBoard = new Board(new T(), fieldsFactory);
        }

        public FiredShotResult PlayerAShot(int x, int y)
        {
            if (PlayerTurn != GamePlayer.PlayerA)
                throw new InvalidPlayerTurnException();

            var result = PlayerABoard.CheckFiredShot(x, y);
            PlayerTurn = GamePlayer.PlayerB;
            return result;
        }

        public FiredShotResult PlayerBShot(int x, int y)
        {
            if (PlayerTurn != GamePlayer.PlayerB)
                throw new InvalidPlayerTurnException();

            var result = PlayerBBoard.CheckFiredShot(x, y);
            PlayerTurn = GamePlayer.PlayerA;
            return result;
        }
    }
}
