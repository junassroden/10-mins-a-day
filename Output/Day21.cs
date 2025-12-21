using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TenMinsADay
{
    public class Day21
    {
        public static void Main(string[] args)
        {
            bool isPrime = true;  
            Console.Write("Enter a number: ");
            int n = int.Parse(Console.ReadLine());
            for (int d = 2; d < n; d++)
            {
                if (n % d == 0)    
                {
                    isPrime = false;
                    break;
                }
            }
            if (isPrime)
                Console.WriteLine($"{n} is a prime number.");
            else
                Console.WriteLine($"{n} is not a prime number.");
        }
    }
}