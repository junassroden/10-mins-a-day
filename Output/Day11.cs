using System;

namespace TenMinsADay
{
    public class Day11
    {
        public static void Main(string[] args)
        {
            int [] numbers = new int[10];
            int i = 0;
            do
            {
                Console.Write("Enter number: ");
                numbers[i] = int.Parse(Console.ReadLine());
                i++;
            }while(i < 10);
            Console.Write("Numbers: ");
            foreach(int n in numbers)
            {
                Console.Write($"{n} ");
            }
        }
    }
}
