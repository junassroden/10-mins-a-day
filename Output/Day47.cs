using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Day47
{
    public interface ITransaction
    {
        void Deposit(double amount);
        bool Withdraw(double amount);
    }

    public abstract class BankAccount : ITransaction
    {
        public int Id { get; }
        public string Owner { get; }
        public double Balance { get; protected set; }

        protected BankAccount(int id, string owner, double balance)
        {
            Id = id;
            Owner = owner;
            Balance = balance;
        }

        public abstract string GetAccountType();

        public abstract double CalculateInterest();

        public virtual void Deposit(double amount)
        {
            if (amount > 0)
                Balance += amount;
        }

        public virtual bool Withdraw(double amount)
        {
            if (amount <= 0 || amount > Balance)
                return false;

            Balance -= amount;
            return true;
        }
    }

    public class SavingsAccount : BankAccount
    {
        public SavingsAccount(
            int id,
            string owner,
            double balance)
            : base(id, owner, balance)
        {
        }

        public override string GetAccountType()
        {
            return "Savings";
        }

        public override double CalculateInterest()
        {
            return Balance * 0.05;
        }
    }

    public class CheckingAccount : BankAccount
    {
        public CheckingAccount(
            int id,
            string owner,
            double balance)
            : base(id, owner, balance)
        {
        }

        public override string GetAccountType()
        {
            return "Checking";
        }

        public override double CalculateInterest()
        {
            return Balance * 0.01;
        }

        public override bool Withdraw(double amount)
        {
            if (amount <= 0)
                return false;

            if (amount > Balance + 500)
                return false;

            Balance -= amount;
            return true;
        }
    }

    public class ApiUser
    {
        public int id { get; set; }
        public string firstName { get; set; } = "";
        public string lastName { get; set; } = "";
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

                UserResponse? response =
                    JsonSerializer.Deserialize<UserResponse>(
                        json,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                return response?.users ?? new List<ApiUser>();
            }
            catch (Exception error)
            {
                Console.WriteLine("API Error: " + error.Message);
                return new List<ApiUser>();
            }
        }
    }

    public class Bank
    {
        private readonly List<BankAccount> accounts = new();

        public void AddAccount(BankAccount account)
        {
            accounts.Add(account);
        }

        public BankAccount? FindAccount(int id)
        {
            return accounts.FirstOrDefault(a => a.Id == id);
        }

        public void ShowAccounts()
        {
            foreach (BankAccount account in accounts)
            {
                Console.WriteLine(
                    $"{account.Id} | " +
                    $"{account.Owner} | " +
                    $"{account.GetAccountType()} | " +
                    $"${account.Balance:F2}"
                );
            }
        }

        public void ShowDetails(int id)
        {
            BankAccount? account = FindAccount(id);

            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }

            Console.WriteLine("\n========== ACCOUNT ==========");
            Console.WriteLine($"ID: {account.Id}");
            Console.WriteLine($"Owner: {account.Owner}");
            Console.WriteLine($"Type: {account.GetAccountType()}");
            Console.WriteLine($"Balance: ${account.Balance:F2}");
            Console.WriteLine(
                $"Interest: ${account.CalculateInterest():F2}"
            );
        }

        public void Deposit(int id, double amount)
        {
            BankAccount? account = FindAccount(id);

            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }

            account.Deposit(amount);

            Console.WriteLine(
                $"New Balance: ${account.Balance:F2}"
            );
        }

        public void Withdraw(int id, double amount)
        {
            BankAccount? account = FindAccount(id);

            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }

            if (!account.Withdraw(amount))
            {
                Console.WriteLine("Transaction failed.");
                return;
            }

            Console.WriteLine(
                $"New Balance: ${account.Balance:F2}"
            );
        }

        public void ShowInterest()
        {
            foreach (BankAccount account in accounts)
            {
                Console.WriteLine(
                    $"{account.Owner} | " +
                    $"{account.GetAccountType()} | " +
                    $"Interest: ${account.CalculateInterest():F2}"
                );
            }
        }
    }

    class Program
    {
        static async Task Main()
        {
            ApiService api = new();
            Bank bank = new();

            Console.WriteLine("Loading customers...");

            List<ApiUser> users = await api.GetUsersAsync();

            if (users.Count == 0)
                return;

            for (int i = 0; i < users.Count; i++)
            {
                ApiUser user = users[i];

                string name =
                    $"{user.firstName} {user.lastName}";

                double balance = 10000 + (i * 2500);

                BankAccount account;

                if (i % 2 == 0)
                {
                    account = new SavingsAccount(
                        user.id,
                        name,
                        balance
                    );
                }
                else
                {
                    account = new CheckingAccount(
                        user.id,
                        name,
                        balance
                    );
                }

                bank.AddAccount(account);
            }

            while (true)
            {
                Console.WriteLine("\n================================");
                Console.WriteLine("         BANKING SYSTEM");
                Console.WriteLine("================================");
                Console.WriteLine("1. View Accounts");
                Console.WriteLine("2. Account Details");
                Console.WriteLine("3. Deposit");
                Console.WriteLine("4. Withdraw");
                Console.WriteLine("5. Calculate Interest");
                Console.WriteLine("6. Exit");

                Console.Write("\nChoose: ");
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        bank.ShowAccounts();
                        break;

                    case "2":
                        Console.Write("Account ID: ");

                        if (int.TryParse(
                            Console.ReadLine(),
                            out int detailsId))
                        {
                            bank.ShowDetails(detailsId);
                        }

                        break;

                    case "3":
                        Console.Write("Account ID: ");

                        if (!int.TryParse(
                            Console.ReadLine(),
                            out int depositId))
                            break;

                        Console.Write("Amount: ");

                        if (double.TryParse(
                            Console.ReadLine(),
                            out double deposit))
                        {
                            bank.Deposit(depositId, deposit);
                        }

                        break;

                    case "4":
                        Console.Write("Account ID: ");

                        if (!int.TryParse(
                            Console.ReadLine(),
                            out int withdrawId))
                            break;

                        Console.Write("Amount: ");

                        if (double.TryParse(
                            Console.ReadLine(),
                            out double withdraw))
                        {
                            bank.Withdraw(
                                withdrawId,
                                withdraw
                            );
                        }

                        break;

                    case "5":
                        bank.ShowInterest();
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
