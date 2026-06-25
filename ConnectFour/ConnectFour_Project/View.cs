namespace ConnectFour
{
    /// <summary>
    /// Handles all console input/output and rendering.
    /// Keeps UI logic separate from game logic (separation of concerns).
    /// Demonstrates ENCAPSULATION of display logic.
    /// </summary>
    public class View
    {
        // Console colors for each player symbol
        private static readonly Dictionary<char, ConsoleColor> SymbolColors = new()
        {
            { 'X', ConsoleColor.Red },
            { 'O', ConsoleColor.Yellow },
            { '.', ConsoleColor.DarkGray }
        };

        public void ShowWelcome()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════╗");
            Console.WriteLine("║      CONNECT FOUR  v1.0      ║");
            Console.WriteLine("║     SODV 1202 - OOP Project  ║");
            Console.WriteLine("╚══════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
        }

        public void ShowBoard(Board board)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("  1   2   3   4   5   6   7");
            Console.WriteLine("┌───┬───┬───┬───┬───┬───┬───┐");

            for (int row = 0; row < Board.Rows; row++)
            {
                Console.Write("│");
                for (int col = 0; col < Board.Columns; col++)
                {
                    char cell = board.GetCell(row, col);
                    Console.Write(" ");

                    if (SymbolColors.TryGetValue(cell, out ConsoleColor color))
                        Console.ForegroundColor = color;

                    Console.Write(cell == '.' ? "·" : cell.ToString());
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" │");
                }
                Console.WriteLine();

                if (row < Board.Rows - 1)
                    Console.WriteLine("├───┼───┼───┼───┼───┼───┼───┤");
            }

            Console.WriteLine("└───┴───┴───┴───┴───┴───┴───┘");
            Console.ResetColor();
            Console.WriteLine();
        }

        public void ShowWinner(Player winner)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"🎉  {winner.Name} ({winner.Symbol}) wins!  🎉");
            Console.ResetColor();
        }

        public void ShowDraw()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("It's a draw! The board is full.");
            Console.ResetColor();
        }

        public void ShowScores(Player p1, Player p2)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"── Scores ── {p1.Name}: {p1.Score}  |  {p2.Name}: {p2.Score}");
            Console.ResetColor();
        }

        public void ShowColumnFull()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  That column is full! Please choose another.");
            Console.ResetColor();
        }

        /// <summary>
        /// Asks the player to select a game mode.
        /// Returns true for Human vs Human, false for Human vs Computer.
        /// </summary>
        public bool AskGameMode()
        {
            Console.WriteLine("Select game mode:");
            Console.WriteLine("  1. Human vs Human");
            Console.WriteLine("  2. Human vs Computer");

            while (true)
            {
                Console.Write("Enter choice (1 or 2): ");
                string? input = Console.ReadLine();
                if (input == "1") return true;
                if (input == "2") return false;
                Console.WriteLine("  Please enter 1 or 2.");
            }
        }

        /// <summary>
        /// Overloaded method — asks for a player name with a default suggestion.
        /// Demonstrates METHOD OVERLOADING.
        /// </summary>
        public string AskPlayerName(int playerNumber)
        {
            return AskPlayerName(playerNumber, $"Player {playerNumber}");
        }

        public string AskPlayerName(int playerNumber, string defaultName)
        {
            Console.Write($"Enter name for Player {playerNumber} (default: {defaultName}): ");
            string? input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? defaultName : input.Trim();
        }

        public bool AskPlayAgain()
        {
            Console.Write("\nPlay again? (y/n): ");
            string? input = Console.ReadLine();
            return input?.Trim().ToLower() == "y";
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
