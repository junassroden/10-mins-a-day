using System;

class Program
{
    static void Main()
    {
        const int SIZE = 10;
        int[] numbers = new int[SIZE];

        Console.WriteLine("Enter 10 unique integers:");

        for (int i = 0; i < SIZE; i++)
        {
            while (true)
            {
                Console.Write($"Number {i + 1}: ");

                if (!int.TryParse(Console.ReadLine(), out numbers[i]))
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }

                bool duplicate = false;

                for (int j = 0; j < i; j++)
                {
                    if (numbers[j] == numbers[i])
                    {
                        duplicate = true;
                        break;
                    }
                }

                if (duplicate)
                {
                    Console.WriteLine("Duplicate numbers are not allowed.");
                    continue;
                }

                break;
            }
        }

        Console.WriteLine();

        Console.WriteLine("Original Array:");
        Display(numbers);

        Console.WriteLine();

        BubbleSort(numbers);

        Console.WriteLine("Sorted Array:");
        Display(numbers);

        Console.WriteLine();

        Console.WriteLine($"Highest Number : {numbers[SIZE - 1]}");
        Console.WriteLine($"Lowest Number  : {numbers[0]}");
        Console.WriteLine($"Median         : {GetMedian(numbers):F2}");
        Console.WriteLine($"Average        : {GetAverage(numbers):F2}");

        Console.WriteLine();

        Console.Write("Search a number: ");
        int target = int.Parse(Console.ReadLine());

        int index = BinarySearch(numbers, target);

        if (index != -1)
            Console.WriteLine($"{target} found at index {index}.");
        else
            Console.WriteLine($"{target} was not found.");
    }

    static void Display(int[] arr)
    {
        foreach (int num in arr)
            Console.Write(num + " ");

        Console.WriteLine();
    }

    static void BubbleSort(int[] arr)
    {
        for (int i = 0; i < arr.Length - 1; i++)
        {
            for (int j = 0; j < arr.Length - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
    }

    static double GetAverage(int[] arr)
    {
        int sum = 0;

        foreach (int num in arr)
            sum += num;

        return (double)sum / arr.Length;
    }

    static double GetMedian(int[] arr)
    {
        int mid = arr.Length / 2;

        return (arr[mid - 1] + arr[mid]) / 2.0;
    }

    static int BinarySearch(int[] arr, int target)
    {
        int left = 0;
        int right = arr.Length - 1;

        while (left <= right)
        {
            int mid = (left + right) / 2;

            if (arr[mid] == target)
                return mid;

            if (arr[mid] < target)
                left = mid + 1;
            else
                right = mid - 1;
        }

        return -1;
    }
}
