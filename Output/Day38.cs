using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentGradeSystem
{
    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> Grades { get; set; }

        public Student(int id, string name)
        {
            Id = id;
            Name = name;
            Grades = new List<int>();
        }

        public void AddGrade(int grade)
        {
            Grades.Add(grade);
        }

        public double GetAverage()
        {
            if (Grades.Count == 0)
                return 0;

            return Grades.Average();
        }

        public char GetLetterGrade()
        {
            double avg = GetAverage();

            if (avg >= 90)
                return 'A';
            else if (avg >= 80)
                return 'B';
            else if (avg >= 70)
                return 'C';
            else if (avg >= 60)
                return 'D';
            else
                return 'F';
        }
    }

    class Program
    {
        static List<Student> students = new List<Student>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===== STUDENT GRADE SYSTEM =====");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Add Grade");
                Console.WriteLine("3. View All Students");
                Console.WriteLine("4. Search Student");
                Console.WriteLine("5. Show Top Student");
                Console.WriteLine("6. Remove Student");
                Console.WriteLine("7. Exit");
                Console.Write("\nChoose: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;

                    case "2":
                        AddGrade();
                        break;

                    case "3":
                        ViewStudents();
                        break;

                    case "4":
                        SearchStudent();
                        break;

                    case "5":
                        ShowTopStudent();
                        break;

                    case "6":
                        RemoveStudent();
                        break;

                    case "7":
                        Console.WriteLine("\nThank you for using Student Grade System!");
                        return;

                    default:
                        Console.WriteLine("\nInvalid choice.");
                        Pause();
                        break;
                }
            }
        }

        static void AddStudent()
        {
            Console.Clear();

            Console.Write("Student ID: ");

            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Invalid ID. Enter again: ");
            }

            if (students.Any(s => s.Id == id))
            {
                Console.WriteLine("Student ID already exists.");
                Pause();
                return;
            }

            Console.Write("Student Name: ");
            string name = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(name))
            {
                Console.Write("Name cannot be empty. Enter again: ");
                name = Console.ReadLine();
            }

            students.Add(new Student(id, name));

            Console.WriteLine("\nStudent added successfully.");
            Pause();
        }

        static void AddGrade()
        {
            Console.Clear();

            Console.Write("Enter Student ID: ");

            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            Student student = students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                Console.WriteLine("Student not found.");
                Pause();
                return;
            }

            Console.Write("Enter Grade (0-100): ");

            int grade;
            while (!int.TryParse(Console.ReadLine(), out grade) ||
                   grade < 0 ||
                   grade > 100)
            {
                Console.Write("Invalid grade. Enter again: ");
            }

            student.AddGrade(grade);

            Console.WriteLine("Grade added successfully.");
            Pause();
        }

        static void ViewStudents()
        {
            Console.Clear();

            if (students.Count == 0)
            {
                Console.WriteLine("No students available.");
                Pause();
                return;
            }

            foreach (Student student in students)
            {
                Console.WriteLine("----------------------------------");
                Console.WriteLine($"ID: {student.Id}");
                Console.WriteLine($"Name: {student.Name}");

                if (student.Grades.Count == 0)
                {
                    Console.WriteLine("Grades: No grades yet.");
                    Console.WriteLine("Average: N/A");
                }
                else
                {
                    Console.WriteLine("Grades:");

                    foreach (int grade in student.Grades)
                    {
                        Console.WriteLine(grade);
                    }

                    Console.WriteLine($"Average: {student.GetAverage():F2}");
                    Console.WriteLine($"Letter Grade: {student.GetLetterGrade()}");
                }

                Console.WriteLine("----------------------------------");
            }

            Pause();
        }

        static void SearchStudent()
        {
            Console.Clear();

            Console.Write("Enter Student ID: ");

            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            Student student = students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                Console.WriteLine("Student not found.");
            }
            else
            {
                Console.WriteLine("\nStudent Information");
                Console.WriteLine("----------------------");
                Console.WriteLine($"ID: {student.Id}");
                Console.WriteLine($"Name: {student.Name}");

                if (student.Grades.Count == 0)
                {
                    Console.WriteLine("No grades yet.");
                }
                else
                {
                    Console.WriteLine("Grades: " + string.Join(", ", student.Grades));
                    Console.WriteLine($"Average: {student.GetAverage():F2}");
                    Console.WriteLine($"Letter Grade: {student.GetLetterGrade()}");
                }
            }

            Pause();
        }

        static void ShowTopStudent()
        {
            Console.Clear();

            var gradedStudents = students.Where(s => s.Grades.Count > 0).ToList();

            if (gradedStudents.Count == 0)
            {
                Console.WriteLine("No grades available.");
                Pause();
                return;
            }

            Student top = gradedStudents
                .OrderByDescending(s => s.GetAverage())
                .First();

            Console.WriteLine("Top Student");
            Console.WriteLine("----------------");
            Console.WriteLine($"Name: {top.Name}");
            Console.WriteLine($"Average: {top.GetAverage():F2}");
            Console.WriteLine($"Letter Grade: {top.GetLetterGrade()}");

            Pause();
        }

        static void RemoveStudent()
        {
            Console.Clear();

            Console.Write("Enter Student ID: ");

            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid ID.");
                Pause();
                return;
            }

            Student student = students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                Console.WriteLine("Student not found.");
                Pause();
                return;
            }

            Console.Write($"Delete {student.Name}? (Y/N): ");

            string answer = Console.ReadLine().ToUpper();

            if (answer == "Y")
            {
                students.Remove(student);
                Console.WriteLine("Student removed successfully.");
            }
            else
            {
                Console.WriteLine("Deletion cancelled.");
            }

            Pause();
        }

        static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
