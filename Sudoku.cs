using System;

namespace SudokuFinal
{
    public class Sudoku
    {
        protected int[,] Board;

        // Κατασκευαστής που αντιγράφει τον αρχικό πίνακα
        protected Sudoku(int[,] initialBoard)
        {
            Board = (int[,])initialBoard.Clone();
        }

        // Μέθοδος για να βρούμε ένα κενό κελί
        protected virtual bool FindEmptyCell(int[,] board, out int row, out int col)
        {
            for (row = 0; row < 9; row++)
            {
                for (col = 0; col < 9; col++)
                {
                    if (board[row, col] == 0)
                        return true;
                }
            }

            row = col = -1;
            return false; // Δεν υπάρχουν κενά κελιά
        }

        // Έλεγχος αν είναι ασφαλές να τοποθετήσουμε έναν αριθμό στο κελί
        protected virtual bool IsSafe(int[,] currentBoard, int row, int col, int num)
        {
            return !UsedInRow(currentBoard, row, num) &&
                   !UsedInCol(currentBoard, col, num) &&
                   !UsedInBox(currentBoard, row - row % 3, col - col % 3, num);
        }

        // Έλεγχος αν ο αριθμός υπάρχει ήδη στη σειρά
        protected virtual bool UsedInRow(int[,] currentBoard, int row, int num)
        {
            for (var col = 0; col < 9; col++)
                if (currentBoard[row, col] == num)
                    return true;
            return false;
        }

        // Έλεγχος αν ο αριθμός υπάρχει ήδη στη στήλη
        protected virtual bool UsedInCol(int[,] currentBoard, int col, int num)
        {
            for (var row = 0; row < 9; row++)
                if (currentBoard[row, col] == num)
                    return true;
            return false;
        }

        // Έλεγχος αν ο αριθμός υπάρχει ήδη στο 3x3 τετράγωνο
        protected virtual bool UsedInBox(int[,] currentBoard, int boxStartRow, int boxStartCol, int num)
        {
            for (var row = 0; row < 3; row++)
                for (var col = 0; col < 3; col++)
                    if (currentBoard[row + boxStartRow, col + boxStartCol] == num)
                        return true;
            return false;
        }

        // Εκτύπωση της λύσης του Sudoku 
        public void PrintSolution()
        {
            for (var row = 0; row < 9; row++)
            {
                if (row % 3 == 0 && row != 0)
                    Console.WriteLine("------+-------+------");

                for (var col = 0; col < 9; col++)
                {
                    if (col % 3 == 0 && col != 0)
                        Console.Write("| ");

                    Console.Write(Board[row, col] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
