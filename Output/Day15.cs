using System;
using System.IO.Pipelines;
using System.IO.Pipes;

namespace TenMinsADay
{
    public class Day15
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter a year: ");
            int year = int.Parse(Console.ReadLine());

            string result = ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0)) ? "Leap year" : "Not Leap Year";
            Console.WriteLine(result);
        }
    }
}