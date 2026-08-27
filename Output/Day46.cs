using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Day46
{
    public abstract class Person
    {
        public int Id { get; }
        public string Name { get; private set; }
        public string Email { get; private set; }

        protected Person(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public abstract string GetRole();
    }

    public interface IEmployeeActions
    {
        void Promote();
        double CalculateSalary();
    }

    public class Employee : Person, IEmployeeActions
    {
        public string Department { get; private set; }
        public double Salary { get; protected set; }

        public Employee(
            int id,
            string name,
            string email,
            string department,
            double salary)
            : base(id, name, email)
        {
            Department = department;
            Salary = salary;
        }

        public override string GetRole()
        {
            return "Employee";
        }

        public virtual void Promote()
        {
            Salary += 5000;
        }

        public virtual double CalculateSalary()
        {
            return Salary;
        }
    }

    public class Developer : Employee
    {
        public string ProgrammingLanguage { get; private set; }

        public Developer(
            int id,
            string name,
            string email,
            string department,
            double salary,
            string programmingLanguage)
            : base(id, name, email, department, salary)
        {
            ProgrammingLanguage = programmingLanguage;
        }

        public override string GetRole()
        {
            return "Developer";
        }

        public override double CalculateSalary()
        {
            return Salary + 3000;
        }

        public override void Promote()
        {
            Salary += 8000;
        }
    }

    public class Manager : Employee
    {
        public int TeamSize { get; private set; }

        public Manager(
            int id,
            string name,
            string email,
            string department,
            double salary,
            int teamSize)
            : base(id, name, email, department, salary)
        {
            TeamSize = teamSize;
        }

        public override string GetRole()
        {
            return "Manager";
        }

        public override double CalculateSalary()
        {
            return Salary + (TeamSize * 500);
        }

        public override void Promote()
        {
            Salary += 10000;
        }
    }

    public class ApiUser
    {
        public int id { get; set; }
        public string firstName { get; set; } = "";
        public string lastName { get; set; } = "";
        public string email { get; set; } = "";
    }

    public class UserResponse
    {
        public List<ApiUser> users { get; set; } = new();
    }

    public class ApiService
    {
        public async Task<List<ApiUser>> GetUsersAsync()
        {
            try
            {
                using HttpClient client = new();

                string json = await client.GetStringAsync(
                    "https://dummyjson.com/users"
                );

                UserResponse? result =
                    JsonSerializer.Deserialize<UserResponse>(
                        json,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                return result?.users ?? new List<ApiUser>();
            }
            catch (Exception error)
            {
                Console.WriteLine("API Error: " + error.Message);
                return new List<ApiUser>();
            }
        }
    }

    public class EmployeeManager
    {
        private readonly List<Employee> employees = new();

        public void AddEmployee(Employee employee)
        {
            employees.Add(employee);
        }

        public void ShowEmployees()
        {
            if (employees.Count == 0)
            {
                Console.WriteLine("No employees available.");
                return;
            }

            foreach (Employee employee in employees)
            {
                Console.WriteLine(
                    $"{employee.Id} | " +
                    $"{employee.Name} | " +
                    $"{employee.GetRole()} | " +
                    $"${employee.CalculateSalary():F2}"
                );
            }
        }

        public Employee? FindEmployee(int id)
        {
            return employees.FirstOrDefault(e => e.Id == id);
        }

        public List<Employee> Search(string keyword)
        {
            return employees
                .Where(e =>
                    e.Name.Contains(
                        keyword,
                        StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public double GetTotalPayroll()
        {
            return employees.Sum(e => e.CalculateSalary());
        }

        public void PromoteEmployee(int id)
        {
            Employee? employee = FindEmployee(id);

            if (employee == null)
            {
                Console.WriteLine("Employee not found.");
                return;
            }

            employee.Promote();

            Console.WriteLine(
                $"{employee.Name} has been promoted."
            );
        }

        public void ShowDetails(Employee employee)
        {
            Console.WriteLine("\n========== EMPLOYEE ==========");
            Console.WriteLine($"ID: {employee.Id}");
            Console.WriteLine($"Name: {employee.Name}");
            Console.WriteLine($"Email: {employee.Email}");
            Console.WriteLine($"Role: {employee.GetRole()}");
            Console.WriteLine($"Salary: ${employee.CalculateSalary():F2}");

            if (employee is Developer developer)
            {
                Console.WriteLine(
                    $"Language: {developer.ProgrammingLanguage}");
            }

            if (employee is Manager manager)
            {
                Console.WriteLine(
                    $"Team Size: {manager.TeamSize}");
            }
        }
    }

    class Program
    {
        static async Task Main()
        {
            ApiService api = new();
            EmployeeManager manager = new();

            Console.WriteLine("Loading employees...");

            List<ApiUser> users = await api.GetUsersAsync();

            if (users.Count == 0)
                return;

            for (int i = 0; i < users.Count; i++)
            {
                ApiUser user = users[i];

                Employee employee;

                if (i % 3 == 0)
                {
                    employee = new Manager(
                        user.id,
                        $"{user.firstName} {user.lastName}",
                        user.email,
                        "Management",
                        50000,
                        i + 3
                    );
                }
                else if (i % 2 == 0)
                {
                    employee = new Developer(
                        user.id,
                        $"{user.firstName} {user.lastName}",
                        user.email,
                        "IT",
                        40000,
                        "C#"
                    );
                }
                else
                {
                    employee = new Employee(
                        user.id,
                        $"{user.firstName} {user.lastName}",
                        user.email,
                        "Operations",
                        30000
                    );
                }

                manager.AddEmployee(employee);
            }

            while (true)
            {
                Console.WriteLine("\n================================");
                Console.WriteLine("     EMPLOYEE MANAGEMENT");
                Console.WriteLine("================================");
                Console.WriteLine("1. View Employees");
                Console.WriteLine("2. Search Employee");
                Console.WriteLine("3. Employee Details");
                Console.WriteLine("4. Promote Employee");
                Console.WriteLine("5. Calculate Payroll");
                Console.WriteLine("6. Exit");

                Console.Write("\nChoose: ");
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        manager.ShowEmployees();
                        break;

                    case "2":
                        Console.Write("Search: ");
                        string search = Console.ReadLine() ?? "";

                        List<Employee> results =
                            manager.Search(search);

                        foreach (Employee employee in results)
                        {
                            Console.WriteLine(
                                $"{employee.Id} | {employee.Name} | {employee.GetRole()}"
                            );
                        }

                        if (results.Count == 0)
                            Console.WriteLine("No employees found.");

                        break;

                    case "3":
                        Console.Write("Employee ID: ");

                        if (int.TryParse(
                            Console.ReadLine(),
                            out int detailId))
                        {
                            Employee? employee =
                                manager.FindEmployee(detailId);

                            if (employee == null)
                                Console.WriteLine("Employee not found.");
                            else
                                manager.ShowDetails(employee);
                        }

                        break;

                    case "4":
                        Console.Write("Employee ID: ");

                        if (int.TryParse(
                            Console.ReadLine(),
                            out int promoteId))
                        {
                            manager.PromoteEmployee(promoteId);
                        }

                        break;

                    case "5":
                        Console.WriteLine(
                            $"Total Payroll: ${manager.GetTotalPayroll():F2}"
                        );
                        break;

                    case "6":
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    }
}
