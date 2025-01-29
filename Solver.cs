
namespace SudokuFinal
{
    internal class Solver(int[,] board) : Sudoku(board)
    {
        // Επίλυση με BFS και χρήση ArrayList
        public bool SolveBFS_ArrayList()
        {
            var queue = new List<int[,]> { (int[,])Board.Clone() };

            while (queue.Count > 0)
            {
                var currentBoard = queue[0];
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
                    queue.Add(newBoard);
                }
            }
            return false;
        }

        // Επίλυση με DFS και χρήση ArrayList
        public bool SolveDFS_ArrayList()
        {
            var stack = new List<int[,]> { (int[,])Board.Clone() };

            while (stack.Count > 0)
            {
                var currentBoard = stack[^1]; // Χρήση του ^1 για το τελευταίο στοιχείο
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
                    stack.Add(newBoard);
                }
            }
            return false;
        }

        // Επίλυση με DFS και χρήση Stack
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

        // Επίλυση με BFS και χρήση LinkedList
        public bool SolveBFS_LinkedList()
        {
            var queue = new LinkedList<int[,]>();
            queue.AddLast((int[,])Board.Clone());

            while (queue.Count > 0)
            {
                if (queue.First == null) continue;
                var currentBoard = queue.First.Value;
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