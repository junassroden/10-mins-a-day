using System;

namespace TenMinsADay
{
    public class Day07
    {
        public static void Main(string[] args)
        {
        Console.Write("Enter two numbers separated by space: ");
        var nums = Console.ReadLine().Split();
        Console.WriteLine($"Swapped Numbers:\n{nums[1]}\n{nums[0]}");
        }
    }
}
