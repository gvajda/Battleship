using System;
using System.Linq;
using System.Collections.Generic;

namespace Battleship
{
    public class Board
    {
        public string PlayerName { get; }
        private int BoardWidth { get; }
        private int BoardLength { get; }
        private int ShipSize { get; }
        private List<Tuple<int, int>> ShipCoordinates { get; }
        private List<Tuple<int, int>> ShotHistory { get; }
        private int HitCount { get; set; }

        public Board(int boardWidth, int boardLength, int shipSize, string playerName)
        {
            PlayerName = playerName;
            BoardWidth = boardWidth;
            BoardLength = boardLength;
            ShipSize = shipSize;
            ValidateInput();

            ShipCoordinates = new List<Tuple<int, int>>();
            ShotHistory = new List<Tuple<int, int>>();
            HitCount = 0;
        }

        internal void ValidateInput()
        {
            if (BoardWidth < 1 || BoardLength < 1)
            {
                throw new ArgumentException("Board width or length cannot be less than 1");
            }

            if (BoardWidth < ShipSize && BoardLength < ShipSize)
            {
                throw new ArgumentException($"Ship size [{ShipSize}] doesn't fit board [{BoardLength},{BoardWidth}]");
            }
        }

        public void PlaceShipOnBoard()
        {
            Console.WriteLine($"{PlayerName} - Battleship placement");

            if (ShipCoordinates.Any())
            {
                Console.WriteLine("Battleship is already on the board!");
                return;
            }


            //in case the ship only fits horizontally, make sure it has enough space for the end
            var widthLimit = BoardWidth;
            if (ShipSize > BoardLength)
            {
                widthLimit = BoardWidth - ShipSize;
            }

            var x1Msg = $"Please type the X coordinate (column#) of the battleship staring point - a number between [1 - {widthLimit}] - then press ENTER";
            var x1 = GetCoordinateAsConsoleInput(x1Msg, widthLimit);


            //in case the ship only fits vertically, make sure it has enough space for the end
            var lengthLimit = BoardLength;
            if (ShipSize > BoardWidth)
            {
                lengthLimit = BoardWidth - ShipSize;
            }

            var x2Msg = $"Please type the Y coordinate (row#) of the battleship staring point - a number between [1 - {lengthLimit}] - then press ENTER";
            var x2 = GetCoordinateAsConsoleInput(x2Msg, widthLimit);


            //only give viavle options for the ship endpoint
            var possibleShipEndpoints = new List<Tuple<int, int>>();
            if (x1 - (ShipSize - 1) > 0)
            {
                possibleShipEndpoints.Add(Tuple.Create(x1 - (ShipSize - 1), x2));
            }
            if (x1 + (ShipSize - 1) <= BoardWidth)
            {
                possibleShipEndpoints.Add(Tuple.Create(x1 + (ShipSize - 1), x2));
            }
            if (x2 - (ShipSize - 1) > 0)
            {
                possibleShipEndpoints.Add(Tuple.Create(x1, x2 - (ShipSize - 1)));
            }
            if (x2 + (ShipSize - 1) <= BoardLength)
            {
                possibleShipEndpoints.Add(Tuple.Create(x1, x2 + (ShipSize - 1)));
            }

            Console.WriteLine();
            Console.WriteLine("Your options as endpoint of your ship are the following:");

            foreach (var endpoint in possibleShipEndpoints)
            {
                Console.WriteLine($"{possibleShipEndpoints.IndexOf(endpoint)} - [{endpoint.Item1},{endpoint.Item2}]");
            }

            var endCoordinatesMsg = $"Please type in the number of your chocice for the end of battleship.";
            var enpointChoiceIndex = GetCoordinateAsConsoleInput(endCoordinatesMsg, possibleShipEndpoints.Count);
            Console.WriteLine();
            var enpointChoice = possibleShipEndpoints[enpointChoiceIndex];


            if (x1 != enpointChoice.Item1)
            {
                for (int i = Math.Min(x1, enpointChoice.Item1); i <= Math.Max(x1, enpointChoice.Item1); i++)
                {
                    ShipCoordinates.Add(Tuple.Create(i, x2));
                }
            }
            else
            {
                for (int i = Math.Min(x2, enpointChoice.Item2); i <= Math.Max(x2, enpointChoice.Item2); i++)
                {
                    ShipCoordinates.Add(Tuple.Create(x1, i));
                }
            }

        }

        public bool Shoot()
        {
            Console.WriteLine($"{PlayerName}'s board - Turn {ShotHistory.Count + 1}");

            var shot = GetShotCoordinates();

            while (ShotHistory.Contains(shot))
            {
                Console.WriteLine($"You have already made the shot [{shot.Item1},{shot.Item2}]. Would you like to see your shot history? (Y/N)");
                var historyDisplayChoice = Console.ReadKey();

                if (historyDisplayChoice.Key == ConsoleKey.Y)
                {
                    Console.WriteLine();
                    foreach (var shotHistoryItem in ShotHistory.OrderBy(h => h.Item2).OrderBy(h => h.Item1))
                    {
                        Console.WriteLine($"[{shotHistoryItem.Item1},{shotHistoryItem.Item2}]{(ShipCoordinates.Contains(shotHistoryItem) ? " - HIT" : null)}");
                    }
                }

                Console.WriteLine();

                shot = GetShotCoordinates();
            }

            ShotHistory.Add(shot);

            if (ShipCoordinates.Contains(shot))
            {
                Console.WriteLine("It's a HIT!");
                Console.WriteLine();
                HitCount++;
            }
            else
            {
                Console.WriteLine("No luck this time :(");
                Console.WriteLine();
            }

            if (HitCount == ShipSize)
            {
                return true;
            }

            return false;
        }

        internal int GetCoordinateAsConsoleInput(string consoleMessage, int range, int tryLimit = 3)
        {
            var userInput = "";
            var validatedChoice = 0;

            for (int tryCount = 0; tryCount < tryLimit; tryCount++)
            {
                if (tryCount != 0)
                {
                    Console.WriteLine("Something went wrong, please try again!");
                }

                Console.WriteLine(consoleMessage);
                userInput = Console.ReadLine();
                if (int.TryParse(userInput, out validatedChoice) && validatedChoice <= range)
                {
                    return validatedChoice;
                }
            }
            throw new CoordinateUserInputException("User input doesn't fulfill coordinate requirements");
        }

        internal Tuple<int, int> GetShotCoordinates()
        {
            var x1Msg = $"Please type the X coordinate (column#) of your next shot - a number between [1 - {BoardWidth}] - then press ENTER";
            var x1 = GetCoordinateAsConsoleInput(x1Msg, BoardWidth);

            var x2Msg = $"Please type the X coordinate (column#) of your next shot - a number between [1 - {BoardLength}] - then press ENTER";
            var x2 = GetCoordinateAsConsoleInput(x2Msg, BoardLength);

            return Tuple.Create(x1, x2);
        }
    }

    public class CoordinateUserInputException : Exception
    {
        internal CoordinateUserInputException(string message) : base(message)
        {
        }

        public CoordinateUserInputException()
        {
        }

        public CoordinateUserInputException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
