namespace ConnectFour
{
    /// <summary>
    /// Application entry point.
    /// Launches the GameController which manages all game logic.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            GameController controller = new GameController();
            controller.Run();
        }
    }
}
