using System;

namespace TenMinsADay
{
    public class Day06
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter first number: ");
            int num1 = int.Parse(Console.ReadLine());

            Console.Write("Enter second number: ");
            int num2 = int.Parse(Console.ReadLine());

            Console.Write("Enter third number: ");
            int num3 = int.Parse(Console.ReadLine());

            int min = Math.Min(num1, Math.Min(num2, num3));
            Console.WriteLine("Min number: " + min);
        }
    }
}
