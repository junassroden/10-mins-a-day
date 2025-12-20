using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TenMinsADay
{
    public class Day20
    {
        public static void Main(string[] args)
        {
            int num1 = 0; int num2 = 1;
            Console.Write("Enter number of terms: ");
            int terms = int.Parse(Console.ReadLine());
            Console.Write($"Fibonacci Series up to {terms} elements: {num1} {num2} ");
            for(int i = 2; i < terms; i++)
            {
                int nextNum = num1 + num2;
                Console.Write($"{nextNum} ");
                num1 = num2;
                num2 = nextNum;
            }
        }
    }
}