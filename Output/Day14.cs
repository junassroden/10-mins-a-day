using System;
using System.IO.Pipelines;
using System.IO.Pipes;

namespace TenMinsADay
{
    public class Day14
    {
        public static void Main(string[] args)
        {
            int sum = 1;
            Console.Write("Enter a number: ");
            int number = int.Parse(Console.ReadLine());
            for(int i = 1; i <= number; i++)
            {
                sum *= i;
            }
            Console.WriteLine($"Factorial of {number} is: {sum}");
        }
    }
}