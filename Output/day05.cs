using System;

namespace TenMinsADay
{
    public class Day05
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter first number: ");
            int num1 = int.Parse(Console.ReadLine());

            Console.Write("Enter second number: ");
            int num2 = int.Parse(Console.ReadLine());

            int max = Math.Max(num1, num2);
            Console.WriteLine("Max number: " + max);
        }
    }
}
