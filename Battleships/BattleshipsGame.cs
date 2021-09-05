using Battleships.Enums;
using Battleships.Exceptions;
using Battleships.Factories;
using Battleships.ShipConstraintsConfiguration;

namespace Battleships
{
    public class BattleshipsGame<T> where T : ShipConstraintsBase, new()
    {
        public Board PlayerABoard { get; set; }
        public Board PlayerBBoard { get; set; }

        public GameStatus Status { get; set; }

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
    }
}
