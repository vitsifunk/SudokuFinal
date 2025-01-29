using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace SudokuFinal
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: Solver <inputFile>");
                return;
            }

            var inputFile = args[0];

            // Επιλογή μεθόδου επίλυσης από τον χρήστη
            var solvingMethod = GetUserChoice();

            Console.WriteLine($"\nYou selected option {solvingMethod}. Processing file '{inputFile}'...");

            int[,] initialBoard;
            try
            {
                initialBoard = ReadSudokuFromFile(inputFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading the input file: " + ex.Message);
                return;
            }

            var sudoku = new Solver(initialBoard);
            var solved = SolveSudoku(sudoku, solvingMethod);

            if (solved)
            {
                Console.WriteLine("\nSolved Sudoku:\n");
                sudoku.PrintSolution();
            }
            else
            {
                Console.WriteLine("No solution found for the Sudoku.");
            }
        }

        private static int GetUserChoice()
        {
            while (true)
            {
                Console.WriteLine("Select solving method:");
                Console.WriteLine("1: DFS with ArrayList");
                Console.WriteLine("2: BFS with ArrayList");
                Console.WriteLine("3: DFS with Stack");
                Console.WriteLine("4: BFS with LinkedList");
                Console.Write("\nEnter your choice (1-4): ");

                if (int.TryParse(Console.ReadLine(), out var choice) && choice is >= 1 and <= 4)
                {
                    return choice;
                }

                Console.WriteLine("\nInvalid choice. Please choose a number between 1 and 4.\n");
            }
        }

        private static int[,] ReadSudokuFromFile(string filePath)
        {
            var board = new int[9, 9];

            using var reader = new StreamReader(filePath);
            for (var i = 0; i < 9; i++)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    throw new Exception("Invalid file format: missing line");

                var numbers = line.Split(' ');
                if (numbers.Length != 9)
                    throw new Exception("Invalid file format: each line must contain exactly 9 numbers");

                for (var j = 0; j < 9; j++)
                {
                    if (!int.TryParse(numbers[j], out board[i, j]))
                        throw new Exception($"Invalid number at row {i + 1}, column {j + 1}");
                }
            }

            return board;
        }

        private static bool SolveSudoku(Solver sudoku, int method)
        {
            return method switch
            {
                1 => sudoku.SolveDFS_ArrayList(),
                2 => sudoku.SolveBFS_ArrayList(),
                3 => sudoku.SolveDFS_Stack(),
                4 => sudoku.SolveBFS_LinkedList(),
                _ => false
            };
        }
    }
}
