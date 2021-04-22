using System;

namespace Battleship
{
    public class Board
    {
        public int BoardWidth { get; set; }
        public int BoardLength { get; set; }
        public int ShipLength { get; set; }

        public Board(int width, int length)
        {
            BoardWidth = width;
            BoardLength = length;
            ValidateInput();
        }

        void ValidateInput()
        {
            if (BoardWidth < 1 || BoardLength < 1)
            {
                throw new ArgumentException("Board width or length is lesss than 1");
            }
        }
    }
}
