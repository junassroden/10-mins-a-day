using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TenMinsADay
{
    public class Day17
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter a number: ");
            int input = int.Parse(Console.ReadLine());
            string convert = input.ToString();
            int length = convert.Length;
            Console.Write(length);
        }
    }
}