using System;

namespace TenMinsADay
{
    public class Day08
    {
        public static void Main(string[] args)
        {
            Console.Write("Celsius: ");
            Console.WriteLine("Fahrenheit: " + (int.Parse(Console.ReadLine()) * 9 / 5 + 32));
        }
    }
}
