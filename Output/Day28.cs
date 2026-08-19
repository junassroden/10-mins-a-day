using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TenMinsADay
{
    public class Day28
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter a character: ");
            char let = Console.ReadKey().KeyChar;
            int ascii = (int)let;
            Console.WriteLine("ASCII value of 5 is: " + ascii);
        }
    }
}