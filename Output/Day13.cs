using System;
using System.IO.Pipelines;

namespace TenMinsADay
{
    public class Day12
    {
        public static void Main(string[] args)
        {
            int sum;
            Console.WriteLine("Multiplication Table");
            for(int i = 1; i <= 10; i++)
            {
                for(int y = 1; y <= 10; y++)
                {
                    sum = i * y;
                    Console.Write($"{sum}\t");
                }
                Console.WriteLine();
            }
        }
    }
}
