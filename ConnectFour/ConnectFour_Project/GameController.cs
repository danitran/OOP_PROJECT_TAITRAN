namespace ConnectFour
{
    /// <summary>
    /// Controls the overall game flow: setup, turn loop, win/draw detection,
    /// and replay. Coordinates Board, View, and Player objects.
    /// Demonstrates the CONTROLLER pattern and proper OOP structure.
    /// </summary>
    public class GameController
    {
        private readonly View _view;
        private Player _player1 = null!;
        private Player _player2 = null!;

        public GameController()
        {
            _view = new View();
        }

        /// <summary>
        /// Entry point — runs the full application loop including replay.
        /// </summary>
        public void Run()
        {
            _view.ShowWelcome();
            SetupPlayers();

            bool playAgain = true;
            while (playAgain)
            {
                PlayOneGame();
                _view.ShowScores(_player1, _player2);
                playAgain = _view.AskPlayAgain();
            }

            _view.ShowMessage("\nThanks for playing Connect Four! Goodbye.");
        }

        /// <summary>
        /// Configures players based on the chosen game mode.
        /// </summary>
        private void SetupPlayers()
        {
            bool humanVsHuman = _view.AskGameMode();
            Console.WriteLine();

            string name1 = _view.AskPlayerName(1, "Alice");
            _player1 = new HumanPlayer(name1, 'X');

            if (humanVsHuman)
            {
                string name2 = _view.AskPlayerName(2, "Bob");
                _player2 = new HumanPlayer(name2, 'O');
            }
            else
            {
                // Polymorphism in action: _player2 is a Player reference pointing to ComputerPlayer
                _player2 = new ComputerPlayer("CPU", 'O');
                _view.ShowMessage($"Computer player created: {_player2}");
            }

            Console.WriteLine($"\nStarting game: {_player1} vs {_player2}");
            Console.WriteLine("Press Enter to begin...");
            Console.ReadLine();
        }

        /// <summary>
        /// Runs a single game from start to finish.
        /// </summary>
        private void PlayOneGame()
        {
            Board board = new Board();
            Player currentPlayer = _player1;

            while (true)
            {
                Console.Clear();
                _view.ShowBoard(board);
                _view.ShowScores(_player1, _player2);

                // Polymorphic call — works for both HumanPlayer and ComputerPlayer
                int col = currentPlayer.ChooseColumn(board);

                if (!board.IsColumnAvailable(col))
                {
                    _view.ShowColumnFull();
                    System.Threading.Thread.Sleep(1000);
                    continue;
                }

                int row = board.DropDisc(col, currentPlayer.Symbol);

                // Check win
                if (board.CheckWinAt(row, col, currentPlayer.Symbol))
                {
                    Console.Clear();
                    _view.ShowBoard(board);
                    _view.ShowWinner(currentPlayer);
                    currentPlayer.IncrementScore();
                    System.Threading.Thread.Sleep(1500);
                    return;
                }

                // Check draw
                if (board.IsFull())
                {
                    Console.Clear();
                    _view.ShowBoard(board);
                    _view.ShowDraw();
                    System.Threading.Thread.Sleep(1500);
                    return;
                }

                // Switch turns
                currentPlayer = (currentPlayer == _player1) ? _player2 : _player1;
            }
        }
    }
}
