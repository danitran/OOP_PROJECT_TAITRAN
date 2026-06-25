namespace ConnectFour
{
    /// <summary>
    /// Computer player with a simple rule-based AI strategy.
    /// Demonstrates INHERITANCE from Player and POLYMORPHISM.
    /// </summary>
    public class ComputerPlayer : Player
    {
        private readonly Random _random = new Random();

        public ComputerPlayer(string name, char symbol) : base(name, symbol) { }

        /// <summary>
        /// OVERRIDES abstract method — uses rule-based logic to pick a column.
        /// Strategy priority:
        ///   1. Win if possible
        ///   2. Block opponent from winning
        ///   3. Prefer center columns
        ///   4. Fall back to a random valid column
        /// </summary>
        public override int ChooseColumn(Board board)
        {
            Console.WriteLine($"{Name} ({Symbol}) is thinking...");
            System.Threading.Thread.Sleep(700); // Short pause for realism

            // 1. Check if computer can win immediately
            int winCol = FindWinningMove(board, Symbol);
            if (winCol >= 0) return winCol;

            // 2. Block opponent from winning
            char opponentSymbol = Symbol == 'X' ? 'O' : 'X';
            int blockCol = FindWinningMove(board, opponentSymbol);
            if (blockCol >= 0) return blockCol;

            // 3. Prefer center columns (indices 3, 2, 4, 1, 5, 0, 6)
            int[] preferred = { 3, 2, 4, 1, 5, 0, 6 };
            foreach (int col in preferred)
            {
                if (board.IsColumnAvailable(col))
                    return col;
            }

            // 4. Fallback — pick any valid column (should never reach here)
            return PickRandomColumn(board);
        }

        /// <summary>
        /// Simulates placing a disc in each column to find a winning move.
        /// </summary>
        private int FindWinningMove(Board board, char symbol)
        {
            for (int col = 0; col < Board.Columns; col++)
            {
                if (!board.IsColumnAvailable(col)) continue;

                // Simulate the move
                int row = board.SimulateDrop(col);
                if (row >= 0 && board.CheckWinAt(row, col, symbol))
                    return col;
            }
            return -1;
        }

        private int PickRandomColumn(Board board)
        {
            List<int> available = new List<int>();
            for (int col = 0; col < Board.Columns; col++)
                if (board.IsColumnAvailable(col))
                    available.Add(col);

            return available.Count > 0 ? available[_random.Next(available.Count)] : -1;
        }
    }
}
