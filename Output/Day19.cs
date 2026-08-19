using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TenMinsADay
{
    public class Day19
    {
        public static void Main(string[] args)
        {
            int sum = 0;
            Console.Write("Enter a number: ");
            string numbers = Console.ReadLine().Trim();
            foreach(char c in numbers)
            {
                sum += int.Parse(c.ToString());
            }
            Console.Write(sum);
        }
    }
}