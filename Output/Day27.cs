﻿using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TenMinsADay
{
    public class Day27
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter a student grade: ");
            double grade = int.Parse(Console.ReadLine());
            if(grade >= 90 && grade <= 100)
            {
                Console.Write("Grade: A");
            }
            else if(grade >= 80 && grade <= 89)
            {
                Console.Write("Grade: B");
            }
            else if(grade >= 70 && grade <= 79)
            {
                Console.Write("Grade: C");
            }
            else if(grade >= 60 && grade <= 69)
            {
                Console.Write("Grade: D");
            }
            else
            {
                Console.Write("GRade: F");
            }
        }
    }
}