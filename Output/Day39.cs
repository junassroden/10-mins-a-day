using System;

class Program
{
    static void Main()
    {
        int[,] grid = new int[3, 3];
        bool[] seen = new bool[10];
        bool isValid = true;

        Console.WriteLine("Enter the 3x3 Sudoku block:");
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                Console.Write($"Enter value [{row},{col}]: ");
                grid[row, col] = Convert.ToInt32(Console.ReadLine());

                int num = grid[row, col];

                if (num < 1 || num > 9)
                {
                    isValid = false;
                }
                else
                {
                    if (seen[num])
                    {
                        isValid = false;
                    }
                    else
                    {
                        seen[num] = true;
                    }
                }
            }
        }

        Console.WriteLine();

        Console.WriteLine("Sudoku Block:");
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                Console.Write(grid[row, col] + " ");
            }
            Console.WriteLine();
        }

        Console.WriteLine();

        if (isValid)
        {
            Console.WriteLine("Valid Sudoku Block");
        }
        else
        {
            Console.WriteLine("Invalid Sudoku Block");
        }
    }
}
