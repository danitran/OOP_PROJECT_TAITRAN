namespace ConnectFour
{
    /// <summary>
    /// Represents the Connect Four board (7 columns x 6 rows).
    /// Handles all game logic: dropping discs, checking wins, detecting draws.
    /// Demonstrates ENCAPSULATION — internal grid is private; state is exposed via methods.
    /// </summary>
    public class Board
    {
        public const int Rows = 6;
        public const int Columns = 7;

        // Private grid — encapsulated state
        private readonly char[,] _grid;

        public Board()
        {
            _grid = new char[Rows, Columns];
            // Initialize every cell to empty
            for (int r = 0; r < Rows; r++)
                for (int c = 0; c < Columns; c++)
                    _grid[r, c] = '.';
        }

        /// <summary>
        /// Drops a disc into the specified column.
        /// Returns the row where it landed, or -1 if the column is full.
        /// </summary>
        public int DropDisc(int col, char symbol)
        {
            // Discs fall to the lowest available row
            for (int row = Rows - 1; row >= 0; row--)
            {
                if (_grid[row, col] == '.')
                {
                    _grid[row, col] = symbol;
                    return row;
                }
            }
            return -1; // Column is full
        }

        /// <summary>
        /// Returns which row a disc would land in without placing it.
        /// Used by ComputerPlayer to simulate moves.
        /// </summary>
        public int SimulateDrop(int col)
        {
            for (int row = Rows - 1; row >= 0; row--)
                if (_grid[row, col] == '.')
                    return row;
            return -1;
        }

        public bool IsColumnAvailable(int col) =>
            col >= 0 && col < Columns && _grid[0, col] == '.';

        public bool IsFull()
        {
            for (int c = 0; c < Columns; c++)
                if (IsColumnAvailable(c)) return false;
            return true;
        }

        /// <summary>
        /// Checks whether the given symbol has four-in-a-row through (row, col).
        /// Checks all four directions: horizontal, vertical, and both diagonals.
        /// </summary>
        public bool CheckWinAt(int row, int col, char symbol)
        {
            return CountDirection(row, col, 0, 1, symbol) + CountDirection(row, col, 0, -1, symbol) >= 3  // Horizontal
                || CountDirection(row, col, 1, 0, symbol) >= 3                                             // Vertical (down only)
                || CountDirection(row, col, 1, 1, symbol) + CountDirection(row, col, -1, -1, symbol) >= 3  // Diagonal \
                || CountDirection(row, col, 1, -1, symbol) + CountDirection(row, col, -1, 1, symbol) >= 3; // Diagonal /
        }

        /// <summary>
        /// Counts consecutive matching symbols in one direction from (row, col),
        /// not counting the origin cell itself.
        /// </summary>
        private int CountDirection(int row, int col, int dr, int dc, char symbol)
        {
            int count = 0;
            int r = row + dr;
            int c = col + dc;
            while (r >= 0 && r < Rows && c >= 0 && c < Columns && _grid[r, c] == symbol)
            {
                count++;
                r += dr;
                c += dc;
            }
            return count;
        }

        /// <summary>
        /// Returns a copy of a cell's value — read-only access for the View.
        /// </summary>
        public char GetCell(int row, int col) => _grid[row, col];
    }
}
