using System;

namespace Battleship
{
    class Program
    {
        public static int BoardWidth { get; set; }
        public static int BoardLength { get; set; }
        public static int ShipSize { get; set; }
        public static StarterPlayer Starter { get; set; }

        static void Main(string[] args)
        {
            BoardWidth = args.Length >= 1 && int.TryParse(args[0], out var inputWidth) ? inputWidth : Constants.DefaultBoardWith;
            BoardLength = args.Length >= 2 && int.TryParse(args[1], out var inputLength) ? inputLength : Constants.DefaultBoardLength;
            ShipSize = args.Length >= 3 && int.TryParse(args[2], out var inputShipSize) ? inputShipSize : Constants.DefaultShipSize;

            Console.WriteLine($"Battleship game started{(args.Length == 0 ? " with default values" : null)}. Board width: [{BoardWidth}], length: [{BoardLength}], ship size: [{ShipSize}]");

            Console.WriteLine();


            Console.WriteLine("Player 1 name:");
            var player1Name = Console.ReadLine();
            player1Name = !string.IsNullOrEmpty(player1Name) ? player1Name : "Player 1";
            Console.WriteLine();



            Console.WriteLine("Player 2 name:");
            var player2Name = Console.ReadLine();
            player2Name = !string.IsNullOrEmpty(player2Name) ? player2Name : "Player 2";
            Console.WriteLine();

            try
            {

                var player1Board = new Board(BoardWidth, BoardLength, ShipSize, player1Name);
                player1Board.PlaceShipOnBoard();

                var player2Board = new Board(BoardWidth, BoardLength, ShipSize, player2Name);
                player2Board.PlaceShipOnBoard();

                while (true)
                {
                    if (player2Board.Shoot())
                    {
                        FinishMessage(player1Name);
                        break;
                    }

                    if (player1Board.Shoot())
                    {
                        FinishMessage(player2Name);
                        break;
                    }
                }
            }
            catch (CoordinateUserInputException)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Repeating user input error. Please read the instructions and try again.");
            }

        }

        internal static void FinishMessage(string name) => Console.WriteLine($"Congratulatuons {name}! The enemy Battleship sunk, you won the game.");
    }
}
