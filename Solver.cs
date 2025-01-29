using System;
using System.Collections;
using System.Collections.Generic;

namespace SudokuFinal
{
    internal class Solver : Sudoku
    {
        public Solver(int[,] board) : base(board) { }

        // DFS με ArrayList (λειτουργεί ως Stack)
        public bool SolveDFS_ArrayList()
        {
            var stack = new ArrayList { (int[,])Board.Clone() };

            while (stack.Count > 0)
            {
                var currentBoard = (int[,])stack[stack.Count - 1]!; // Παίρνουμε το τελευταίο στοιχείο (Stack)
                stack.RemoveAt(stack.Count - 1);

                if (!FindEmptyCell(currentBoard, out var row, out var col))
                {
                    Board = currentBoard;
                    return true;
                }

                for (var num = 1; num <= 9; num++)
                {
                    if (!IsSafe(currentBoard, row, col, num)) continue;
                    var newBoard = (int[,])currentBoard.Clone();
                    newBoard[row, col] = num;
                    stack.Add(newBoard); // Push στο ArrayList
                }
            }
            return false;
        }

        // BFS με ArrayList (λειτουργεί ως Queue)
        public bool SolveBFS_ArrayList()
        {
            var queue = new ArrayList { (int[,])Board.Clone() };

            while (queue.Count > 0)
            {
                var currentBoard = (int[,])queue[0]!; // Παίρνουμε το πρώτο στοιχείο (Queue)
                queue.RemoveAt(0);

                if (!FindEmptyCell(currentBoard, out var row, out var col))
                {
                    Board = currentBoard;
                    return true;
                }

                for (var num = 1; num <= 9; num++)
                {
                    if (!IsSafe(currentBoard, row, col, num)) continue;
                    var newBoard = (int[,])currentBoard.Clone();
                    newBoard[row, col] = num;
                    queue.Add(newBoard); // Enqueue στο ArrayList
                }
            }
            return false;
        }

        // DFS με Stack
        public bool SolveDFS_Stack()
        {
            var stack = new Stack<int[,]>();
            stack.Push((int[,])Board.Clone());

            while (stack.Count > 0)
            {
                var currentBoard = stack.Pop();

                if (!FindEmptyCell(currentBoard, out var row, out var col))
                {
                    Board = currentBoard;
                    return true;
                }

                for (var num = 1; num <= 9; num++)
                {
                    if (!IsSafe(currentBoard, row, col, num)) continue;
                    var newBoard = (int[,])currentBoard.Clone();
                    newBoard[row, col] = num;
                    stack.Push(newBoard);
                }
            }
            return false;
        }

        // BFS με LinkedList (Queue)
        public bool SolveBFS_LinkedList()
        {
            var queue = new LinkedList<int[,]>();
            queue.AddLast((int[,])Board.Clone());

            while (queue.Count > 0)
            {
                var currentBoard = queue.First?.Value;
                queue.RemoveFirst();

                if (!FindEmptyCell(currentBoard, out var row, out var col))
                {
                    Board = currentBoard;
                    return true;
                }

                for (var num = 1; num <= 9; num++)
                {
                    if (!IsSafe(currentBoard, row, col, num)) continue;
                    var newBoard = (int[,])currentBoard.Clone();
                    newBoard[row, col] = num;
                    queue.AddLast(newBoard);
                }
            }
            return false;
        }
    }
}
