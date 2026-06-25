namespace ConnectFour
{
    /// <summary>
    /// Abstract base class for all player types.
    /// Demonstrates ABSTRACTION and INHERITANCE.
    /// </summary>
    public abstract class Player
    {
        // Encapsulated fields with public read-only properties
        public string Name { get; private set; }
        public char Symbol { get; private set; }
        public int Score { get; private set; }

        protected Player(string name, char symbol)
        {
            Name = name;
            Symbol = symbol;
            Score = 0;
        }

        /// <summary>
        /// Abstract method — each subclass must implement how they pick a column.
        /// Demonstrates POLYMORPHISM via method overriding.
        /// </summary>
        public abstract int ChooseColumn(Board board);

        public void IncrementScore() => Score++;

        public override string ToString() => $"{Name} ({Symbol})";
    }
}
