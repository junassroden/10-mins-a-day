using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TenMinsADay
{
    public class Day23
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter first number: ");
            int num1 = int.Parse(Console.ReadLine());
            
            Console.Write("Enter second number: ");
            int num2 = int.Parse(Console.ReadLine());

            while(num1 != num2)
            {
                if(num1 > num2)
                {
                    num1 = num1 - num2;
                }
                else
                {
                    num2 = num2 - num1;
                }
            }
            Console.WriteLine($"GCD = {num1}");
        }
    }
}