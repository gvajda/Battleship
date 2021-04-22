using System;

namespace Battleship
{
    class Program
    {
        public static int Width { get; set; }
        public static int Length { get; set; }
        public static StarterPlayer Starter { get; set; }

        static void Main(string[] args)
        {
            Width = args.Length >= 1 && int.TryParse(args[0], out var inputWidth) ? inputWidth : Constants.DefaultBoardWith;
            Length = args.Length >= 2 && int.TryParse(args[1], out var inputLength) ? inputLength : Constants.DefaultShipLength;
            Starter = args.Length >= 3 && Enum.TryParse<StarterPlayer>(args[2], out var inputStarter) ? inputStarter : Constants.Starter;

            Console.WriteLine($"Battleship game started{(args.Length == 0 ? " with default values" : null)}. Board width: [{Width}], length: [{Length}], starting player: [{Starter}]");

            var line = Console.ReadLine();

        }
    }
}
