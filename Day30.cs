using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace TenMinsADay
{
    public class Day30
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter a number: ");
            string number = Console.ReadLine();

            int largest = number.Max(c => c - '0');
            Console.WriteLine("Largest digit: " + largest);
        }
    }
}