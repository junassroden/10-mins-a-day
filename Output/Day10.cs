using System;
using System.Diagnostics;
using System.Security.Principal;

namespace TenMinsADay
{
    public class Day10
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter a number: ");
            int number = int.Parse(Console.ReadLine());
            string result = (number > 0) ? "Positive" : (number < 0) ? "Negative" : "Zero";
            Console.WriteLine(result);
        }
    }
}
