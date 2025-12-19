using System;
using System.Diagnostics;
using System.Security.Principal;

namespace TenMinsADay
{
    public class Day09
    {
        public static void Main(string[] args)
        {
            Console.Write("Length: ");
            int length = int.Parse(Console.ReadLine());
            
            Console.Write("Width: ");
            int width = int.Parse(Console.ReadLine());
            
            int area = length * width;
            Console.WriteLine("Area: " + area);
        }
    }
}
