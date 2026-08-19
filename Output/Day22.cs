using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TenMinsADay
{
    public class Day22
    {
        public static void Main(string[] args)
        {
            for(int i = 2; i < 100; i++)
            {
                bool isPrime = true;  
                for (int d = 2; d < i; d++)
                {
                    if (i % d == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                    if (isPrime)
                    Console.WriteLine($"{i}");
            }
        }
    }
}