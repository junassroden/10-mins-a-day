using System;

namespace TenMinsADay
{
    public class Day12
    {
        public static void Main(string[] args)
        {
            int sum = 0;
            Console.Write("Enter number: ");
            int n = int.Parse(Console.ReadLine());
            for(int i = 1; i <= n; i++)
            {
                sum += i;
            }
            Console.WriteLine("Sum of first 10 numbers is" + sum);
        }
    }
}
