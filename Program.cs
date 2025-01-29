namespace SudokuFinal
{
    internal abstract class Program
    {
        private static void Main(string[] args)
        {
            // Έλεγχος για την ύπαρξη του ορίσματος
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: Solver <inputFile>");
                return;
            }

            var inputFile = args[0];

            int dataStructureNumber; // To store the user's choice
            
            // Επανάληψη μέχρι ο χρήστης να εισάγει έγκυρη επιλογή
            while (true)
            {
                Console.WriteLine("Select solving method:");
                Console.WriteLine("1: DFS with ArrayList");
                Console.WriteLine("2: BFS with ArrayList");
                Console.WriteLine("3: DFS with Stack");
                Console.WriteLine("4: BFS with LinkedList");
                Console.Write("\nEnter your choice (1-4): ");

                // Ανάγνωση και έλεγχος εγκυρότητας επιλογής
                if (int.TryParse(Console.ReadLine(), out dataStructureNumber) &&
                    dataStructureNumber is >= 1 and <= 4)
                {
                    break; // Valid input; exit the loop
                }

                Console.WriteLine("\nInvalid choice. Please choose a number between 1 and 4.\n");
            }

            // Μετά την επιτυχή επιλογή, προχωρήστε με το πρόγραμμα
            Console.WriteLine($"\nYou selected option {dataStructureNumber}. Processing file '{inputFile}'...");
            // Continue with your solving logic here...


            var initialBoard = new int[9, 9];
            try
            {
                using var reader = new StreamReader(inputFile);
                // Ανάγνωση του πίνακα από το αρχείο
                for (var i = 0; i < 9; i++)
                {
                    var line = reader.ReadLine();
                    var numbers = line?.Split(' ');
                    if (numbers == null) continue;
                    for (var j = 0; j < numbers.Length; j++)
                    {
                        initialBoard[i, j] = int.Parse(numbers[j]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading the input file: " + ex.Message);
                return;
            }

            var sudoku = new Solver(initialBoard);

            // Επίλυση του Sudoku ανάλογα με την επιλογή του χρήστη
            var solved = dataStructureNumber switch
            {
                1 => sudoku.SolveDFS_ArrayList(),
                2 => sudoku.SolveBFS_ArrayList(),
                3 => sudoku.SolveDFS_Stack(),
                4 => sudoku.SolveBFS_LinkedList(),
                _ => false
            };

            // Εκτύπωση της λύσης ή του μηνύματος λάθους
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
    }
}