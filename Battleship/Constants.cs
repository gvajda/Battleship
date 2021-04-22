namespace Battleship
{
    public class Constants
    {
        public const int DefaultBoardWith = 8;
        public const int DefaultBoardLength = 8;
        public const int DefaultShipLength = 8;
        public const StarterPlayer Starter = StarterPlayer.Random;
    }

    public enum StarterPlayer
    {
        Random,
        Player,
        Machine
    }
}
