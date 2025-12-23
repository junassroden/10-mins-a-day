using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace TenMinsADay
{
    public class Day29
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter a letter: ");
            string let = Console.ReadLine().ToLower();

            bool contains = Regex.IsMatch(let, "^[aeiou]$");
            if (contains)
            {
                Console.Write("Vowel");
            }
            else
            {
                Console.Write("Consonant");
            }
        }
    }
}