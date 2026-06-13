using System;
using System.Collections.Generic;

namespace Day33
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n===== NUMBER MANAGER =====");
                Console.WriteLine("1. Add Number");
                Console.WriteLine("2. Display Numbers");
                Console.WriteLine("3. Find Largest Number");
                Console.WriteLine("4. Find Smallest Number");
                Console.WriteLine("5. Calculate Average");
                Console.WriteLine("6. Search Number");
                Console.WriteLine("7. Remove Number");
                Console.WriteLine("8. Exit");

                Console.Write("Choose an option: ");
                int choice;

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddNumber(numbers);
                        break;

                    case 2:
                        DisplayNumbers(numbers);
                        break;

                    case 3:
                        FindLargest(numbers);
                        break;

                    case 4:
                        FindSmallest(numbers);
                        break;

                    case 5:
                        CalculateAverage(numbers);
                        break;

                    case 6:
                        SearchNumber(numbers);
                        break;

                    case 7:
                        RemoveNumber(numbers);
                        break;

                    case 8:
                        running = false;
                        Console.WriteLine("Program terminated.");
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        static void AddNumber(List<int> numbers)
        {
            Console.Write("Enter a number: ");
            int num;

            if (int.TryParse(Console.ReadLine(), out num))
            {
                numbers.Add(num);
                Console.WriteLine("Number added successfully.");
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }

        static void DisplayNumbers(List<int> numbers)
        {
            if (numbers.Count == 0)
            {
                Console.WriteLine("List is empty.");
                return;
            }

            Console.WriteLine("Numbers:");

            foreach (int num in numbers)
            {
                Console.Write(num + " ");
            }

            Console.WriteLine();
        }

        static void FindLargest(List<int> numbers)
        {
            if (numbers.Count == 0)
            {
                Console.WriteLine("List is empty.");
                return;
            }

            int largest = numbers[0];

            foreach (int num in numbers)
            {
                if (num > largest)
                {
                    largest = num;
                }
            }

            Console.WriteLine("Largest number: " + largest);
        }

        static void FindSmallest(List<int> numbers)
        {
            if (numbers.Count == 0)
            {
                Console.WriteLine("List is empty.");
                return;
            }

            int smallest = numbers[0];

            foreach (int num in numbers)
            {
                if (num < smallest)
                {
                    smallest = num;
                }
            }

            Console.WriteLine("Smallest number: " + smallest);
        }

        static void CalculateAverage(List<int> numbers)
        {
            if (numbers.Count == 0)
            {
                Console.WriteLine("List is empty.");
                return;
            }

            int sum = 0;

            foreach (int num in numbers)
            {
                sum += num;
            }

            double average = (double)sum / numbers.Count;

            Console.WriteLine("Average: " + average);
        }

        static void SearchNumber(List<int> numbers)
        {
            if (numbers.Count == 0)
            {
                Console.WriteLine("List is empty.");
                return;
            }

            Console.Write("Enter number to search: ");
            int target;

            if (int.TryParse(Console.ReadLine(), out target))
            {
                if (numbers.Contains(target))
                {
                    Console.WriteLine(target + " was found.");
                }
                else
                {
                    Console.WriteLine(target + " was not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }

        static void RemoveNumber(List<int> numbers)
        {
            if (numbers.Count == 0)
            {
                Console.WriteLine("List is empty.");
                return;
            }

            Console.Write("Enter number to remove: ");
            int target;

            if (int.TryParse(Console.ReadLine(), out target))
            {
                if (numbers.Remove(target))
                {
                    Console.WriteLine("Number removed successfully.");
                }
                else
                {
                    Console.WriteLine("Number not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }
    }
}
