using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TenMinsADay
{
    public class Day16
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter a number: ");
            string numbers = Console.ReadLine().Trim();
            string resversed = new string(numbers.Reverse().ToArray());
            int reservedNumber = int.Parse(resversed);
            Console.WriteLine(reservedNumber);
        }
    }
}