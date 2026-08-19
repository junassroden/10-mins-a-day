using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TenMinsADay
{
    public class Day18
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter a number: ");
            string number = Console.ReadLine().Trim();
            string reversed = new string(number.Reverse().ToArray());
            if(reversed == number)
            {
                Console.WriteLine("Palindrome");
            }
            else
            {
                Console.WriteLine("Not a palindrome");
            }
        }
    }
}