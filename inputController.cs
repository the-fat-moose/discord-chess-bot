namespace ChessBot
{
    /*class DisplayBoard
    {
        private static string[,] chessBoard = new string[8,8];

        public static void Main(string[] args)
        {

            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 == 1)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                    }

                    if (j != 7)
                    {
                        Console.Write("  ");
                    }
                    else
                    {
                        Console.Write("  ");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine();
                    }
                }
            }
        }
    }*/

    class ChessMove
    {
        enum Piece
        {
            KING,
            QUEEN,
            ROOK,
            BISHOP,
            KNIGHT,
            PAWN
        }
        
        // A dictionary used to assign each file on a chess board to a column index
        private static Dictionary<char, int> fileToColumn = new Dictionary<char, int>
        {
            { 'a', 0 }, { 'b', 1 }, { 'c', 2 }, { 'd', 3 }, { 'e', 4 }, { 'f', 5 }, { 'g', 6 }, { 'h', 7 }
        };
        // A dictionary used to assign each piece of a user input to a enum piece
        private static Dictionary<char, Piece> stringPieceToPieceEnum = new Dictionary<char, Piece>
        {
            { 'K', Piece.KING }, { 'Q', Piece.QUEEN }, { 'R', Piece.ROOK }, { 'B', Piece.BISHOP }, { 'N', Piece.KNIGHT }, { '\x00', Piece.PAWN }
        };

        ////////////////////// TEST METHOD //////////////////////
        public static void DetectInput()
        {
            try
            {
                Console.Write("Enter a move in algebraic chess notation (e.g. Nf3): ");
                string userInput = Console.ReadLine();

                Tuple<string, string> separationResult = SeparateChessPieceAndMove(userInput);

                string piece = separationResult.Item1;
                string move = separationResult.Item2;

                Tuple<int, int> fileAndRank = ParseSquare(move);

                int file = fileAndRank.Item1;
                int rank = fileAndRank.Item2;

                Console.WriteLine("Piece: " + piece);
                Console.WriteLine("Move: " + move);
                Console.WriteLine("File: " + file);
                Console.WriteLine("Rank: " + rank);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        //////////////////// END TEST METHOD ////////////////////

        // A method that separates a piece from a move and returns a Tuple<string, string>
        public static Tuple<string, string> SeparateChessPieceAndMove(string chessMove)
        {
            string piece = "";
            string move = "";

            // Check if the user input is atleast 2 characters long
            if (chessMove.Length >= 2)
            {
                char firstChar = chessMove[0];

                // Check if the first character in the string is a letter
                if (Char.IsLetter(firstChar))
                {
                    if (stringPieceToPieceEnum.ContainsKey(firstChar))
                    {
                        // Updates the piece string and ensures the string is capitalized
                        piece = firstChar.ToString().ToUpper();
                        // Updates the move string to be everything in the string past the piece
                        move = chessMove.Substring(1);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid input. Please ensure the letter corresponds to a piece in Algebraic Chess Notation.");
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid input. Please ensure that the piece input is a letter");
                }
            }
            else
            {
                throw new ArgumentException("Invalid move. Please ensure the move contains atleast a piece, a file, and a rank.");
            }

            return new Tuple<string, string>(piece, move);
        }
        
        // A method that inserts a user input, validates the input, and returns a Tuple<int, int>
        public static Tuple<int, int> ParseSquare(string move)
        {
            // Checks to see if the move contains two characters (because there would be 1 character for each the rank and file of a piece). If not, throw an argument exception
            if (move.Length != 2)
            {
                throw new ArgumentException("Invalid move. Input should consist of two characters.");
            }

            // variables to store the file and rank of the input
            char fileChar = move[0];
            char rankChar = move[1];

            // Checks the dictionary "fileToColumn" to see if it contains the character in index 0 of the inputted string. If not, throw an argument exception
            if (!fileToColumn.ContainsKey(fileChar))
            {
                throw new ArgumentException("Invalid file. Please insert a file between a-h.");
            }
            // Checks to see if the rank of the input in index 1 is between 1 and 8 (inclusive). If not, throw an argument exception
            if (!Char.IsDigit(rankChar) || rankChar < '1' || rankChar > '8')
            {
                throw new ArgumentException("Invalid rank. Please insert a rank between 1-8.");
            }

            int file = fileToColumn[fileChar];
            int rank = int.Parse(rankChar.ToString()) - 1;

            return new Tuple<int, int>(rank, file);
        }
    }
}