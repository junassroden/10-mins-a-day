using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagementSystem
{

    abstract class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }

        public Employee(int id, string name, double salary)
        {
            Id = id;
            Name = name;
            Salary = salary;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"ID      : {Id}");
            Console.WriteLine($"Name    : {Name}");
            Console.WriteLine($"Salary  : {Salary:C}");
        }

        public abstract double CalculateBonus();
    }

    class Manager : Employee
    {
        public string Department { get; set; }

        public Manager(int id, string name, double salary, string department)
            : base(id, name, salary)
        {
            Department = department;
        }

        public override double CalculateBonus()
        {
            return Salary * 0.20;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine("\n===== MANAGER =====");
            base.DisplayInfo();
            Console.WriteLine($"Department : {Department}");
            Console.WriteLine($"Bonus      : {CalculateBonus():C}");
        }
    }
    
    class Developer : Employee
    {
        public string ProgrammingLanguage { get; set; }

        public Developer(int id, string name, double salary, string language)
            : base(id, name, salary)
        {
            ProgrammingLanguage = language;
        }

        public override double CalculateBonus()
        {
            return Salary * 0.15;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine("\n===== DEVELOPER =====");
            base.DisplayInfo();
            Console.WriteLine($"Language   : {ProgrammingLanguage}");
            Console.WriteLine($"Bonus      : {CalculateBonus():C}");
        }
    }

    class Program
    {
        static List<Employee> employees = new List<Employee>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("==================================");
                Console.WriteLine(" EMPLOYEE MANAGEMENT SYSTEM");
                Console.WriteLine("==================================");
                Console.WriteLine("1. Add Manager");
                Console.WriteLine("2. Add Developer");
                Console.WriteLine("3. Display Employees");
                Console.WriteLine("4. Search Employee");
                Console.WriteLine("5. Remove Employee");
                Console.WriteLine("6. Total Payroll");
                Console.WriteLine("7. Exit");
                Console.Write("\nChoose: ");

                int choice;

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid Input!");
                    Console.ReadKey();
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddManager();
                        break;

                    case 2:
                        AddDeveloper();
                        break;

                    case 3:
                        DisplayEmployees();
                        break;

                    case 4:
                        SearchEmployee();
                        break;

                    case 5:
                        RemoveEmployee();
                        break;

                    case 6:
                        TotalPayroll();
                        break;

                    case 7:
                        return;

                    default:
                        Console.WriteLine("Invalid Choice!");
                        Console.ReadKey();
                        break;
                }
            }
        }
      
        static void AddManager()
        {
            Console.Clear();

            Console.Write("ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Salary: ");
            double salary = Convert.ToDouble(Console.ReadLine());

            Console.Write("Department: ");
            string department = Console.ReadLine();

            employees.Add(new Manager(id, name, salary, department));

            Console.WriteLine("\nManager Added Successfully!");
            Console.ReadKey();
        }

        static void AddDeveloper()
        {
            Console.Clear();

            Console.Write("ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Salary: ");
            double salary = Convert.ToDouble(Console.ReadLine());

            Console.Write("Programming Language: ");
            string language = Console.ReadLine();

            employees.Add(new Developer(id, name, salary, language));

            Console.WriteLine("\nDeveloper Added Successfully!");
            Console.ReadKey();
        }
      
        static void DisplayEmployees()
        {
            Console.Clear();

            if (employees.Count == 0)
            {
                Console.WriteLine("No Employees Found.");
            }
            else
            {
                foreach (Employee emp in employees)
                {
                    emp.DisplayInfo();
                    Console.WriteLine("------------------------------");
                }
            }

            Console.ReadKey();
        }

        static void SearchEmployee()
        {
            Console.Clear();

            Console.Write("Enter Employee ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Employee emp = employees.FirstOrDefault(e => e.Id == id);

            if (emp != null)
            {
                emp.DisplayInfo();
            }
            else
            {
                Console.WriteLine("Employee Not Found.");
            }

            Console.ReadKey();
        }

        static void RemoveEmployee()
        {
            Console.Clear();

            Console.Write("Enter Employee ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Employee emp = employees.FirstOrDefault(e => e.Id == id);

            if (emp != null)
            {
                employees.Remove(emp);
                Console.WriteLine("Employee Removed.");
            }
            else
            {
                Console.WriteLine("Employee Not Found.");
            }

            Console.ReadKey();
        }

        static void TotalPayroll()
        {
            Console.Clear();

            double totalSalary = employees.Sum(e => e.Salary);
            double totalBonus = employees.Sum(e => e.CalculateBonus());

            Console.WriteLine($"Total Salary : {totalSalary:C}");
            Console.WriteLine($"Total Bonus  : {totalBonus:C}");
            Console.WriteLine($"Grand Total  : {(totalSalary + totalBonus):C}");

            Console.ReadKey();
        }
    }
}
