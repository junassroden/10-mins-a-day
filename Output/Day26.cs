using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TenMinsADay
{
    public class Day26
    {
        public static void Main(string[] args)
        {
            double sum = 0;
            Console.Write("Enter first number: ");
            double num1 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter second number: ");
            double num2 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter a operator(+ - * / %): ");
            char op = Convert.ToChar(Console.ReadLine());
            switch (op)
            {
                case '+': sum = num1 + num2;
                          Console.WriteLine("Sum: " + sum);
                          break;
                case '-': sum = num1 - num2;
                          Console.WriteLine("Sum: " + sum);
                          break;
                case '*': sum = num1 * num2;
                          Console.WriteLine("Sum: " + sum);
                          break;
                case '/': if(num2 == 0)
                            {
                                Console.WriteLine("Error: Division by zero!");
                            }
                            else
                            {
                                sum = num1 / num2;
                                Console.WriteLine("Sum: " + sum);
                            }

                          break;
                case '%': sum = num1 % num2;
                          Console.WriteLine("Sum: " + sum);
                          break;
                default: Console.WriteLine("Invalid Operator!");
                          break;
            }
        }
    }
}