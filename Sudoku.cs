namespace SudokuFinal
{
    public class Sudoku
    {
        protected int[,] Board;

        // Κατασκευαστής που αντιγράφει τον αρχικό πίνακα
        protected Sudoku(int[,] initialBoard)
        {
            Board = new int[9, 9];
            Array.Copy(initialBoard, Board, 9 * 9);
        }

        // Μέθοδος για να βρούμε ένα κενό κελί
        protected static bool FindEmptyCell(int[,] board, out int row, out int col)
        {
            for (row = 0; row < 9; row++)
            {
                for (col = 0; col < 9; col++)
                {
                    if (board[row, col] == 0)
                        return true;
                }
            }

            row = -1;
            col = -1;
            return false; // Δεν υπάρχουν κενά κελιά
        }

        // Έλεγχος αν είναι ασφαλές να τοποθετήσουμε έναν αριθμό στο κελί
        protected static bool IsSafe(int[,] currentBoard, int row, int col, int num)
        {
            return !UsedInRow(currentBoard, row, num) &&
                   !UsedInCol(currentBoard, col, num) &&
                   !UsedInBox(currentBoard, row - row % 3, col - col % 3, num);
        }

        // Έλεγχος αν ο αριθμός υπάρχει ήδη στη σειρά
        private static bool UsedInRow(int[,] currentBoard, int row, int num)
        {
            for (var col = 0; col < 9; col++)
            {
                if (currentBoard[row, col] == num)
                    return true;
            }
            return false;
        }

        // Έλεγχος αν ο αριθμός υπάρχει ήδη στη στήλη
        private static bool UsedInCol(int[,] currentBoard, int col, int num)
        {
            for (var row = 0; row < 9; row++)
            {
                if (currentBoard[row, col] == num)
                    return true;
            }
            return false;
        }

        // Έλεγχος αν ο αριθμός υπάρχει ήδη στο 3x3 τετράγωνο
        private static bool UsedInBox(int[,] currentBoard, int boxStartRow, int boxStartCol, int num)
        {
            for (var row = 0; row < 3; row++)
            {
                for (var col = 0; col < 3; col++)
                {
                    if (currentBoard[row + boxStartRow, col + boxStartCol] == num)
                        return true;
                }
            }
            return false;
        }

        // Εκτύπωση της λύσης του Sudoku
        public void PrintSolution()
        {
            for (var row = 0; row < 9; row++)
            {
                for (var col = 0; col < 9; col++)
                {
                    Console.Write(Board[row, col] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}