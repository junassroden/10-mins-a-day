using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TenMinsADay
{
    public class Day2
    {
        public static void Main(string[] args)
        {
                        Console.Write("Enter first number: ");
            int num1 = int.Parse(Console.ReadLine());
            
            Console.Write("Enter second number: ");
            int num2 = int.Parse(Console.ReadLine());

            int originalNum1 = num1;
            int originalNum2 = num2;

            while (num1 != num2)
            {
                if (num1 > num2)
                    num1 = num1 - num2;
                else
                    num2 = num2 - num1;
            }

            int gcd = num1; 
            int lcm = (originalNum1 * originalNum2) / gcd;

            Console.WriteLine($"LCM = {lcm}");
        }
    }
}