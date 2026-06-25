namespace ConnectFour
{
    /// <summary>
    /// Human player — reads column choice from console input.
    /// Demonstrates INHERITANCE from Player.
    /// </summary>
    public class HumanPlayer : Player
    {
        public HumanPlayer(string name, char symbol) : base(name, symbol) { }

        /// <summary>
        /// OVERRIDES abstract method — gets input from the user via console.
        /// </summary>
        public override int ChooseColumn(Board board)
        {
            while (true)
            {
                Console.Write($"{Name}'s turn ({Symbol}) — Enter column (1-7): ");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int col) && col >= 1 && col <= 7)
                    return col - 1; // Convert to 0-based index

                Console.WriteLine("  Invalid input. Please enter a number between 1 and 7.");
            }
        }
    }
}
