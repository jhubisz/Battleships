namespace Battleship
{
    public class Board
    {
        const int DEFAULT_SIZE = 10;

        public int[,] Fields { get; set; }

        public Board() : this(DEFAULT_SIZE) { }
        public Board(int size)
        {
            Fields = new int[size, size];
        }
    }
}
