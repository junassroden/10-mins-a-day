using System;
using System.Collections.Generic;

class Program
{
    static decimal balance = 10000m;
    static List<string> history = new List<string>();

    static void Main()
    {
        int choice;

        while (true)
        {
            Console.WriteLine("\n===== ATM MENU =====");
            Console.WriteLine("1. Check Balance");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. Transaction History");
            Console.WriteLine("5. Exit");

            Console.Write("Enter your choice: ");
            bool validChoice = int.TryParse(Console.ReadLine(), out choice);

            if (!validChoice)
            {
                Console.WriteLine("Invalid input.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    CheckBalance();
                    break;

                case 2:
                    Deposit();
                    break;

                case 3:
                    Withdraw();
                    break;

                case 4:
                    ShowHistory();
                    break;

                case 5:
                    Console.WriteLine("Thank you for using the ATM.");
                    return;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    static void CheckBalance()
    {
        Console.WriteLine($"Current Balance: ₱{balance}");
    }

    static void Deposit()
    {
        Console.Write("Enter amount to deposit: ");

        if (decimal.TryParse(Console.ReadLine(), out decimal amount))
        {
            if (amount > 0)
            {
                balance += amount;
                history.Add($"Deposit: ₱{amount}");

                Console.WriteLine("Deposit successful.");
                Console.WriteLine($"New Balance: ₱{balance}");
            }
            else
            {
                Console.WriteLine("Amount must be greater than zero.");
            }
        }
        else
        {
            Console.WriteLine("Invalid amount.");
        }
    }

    static void Withdraw()
    {
        Console.Write("Enter amount to withdraw: ");

        if (decimal.TryParse(Console.ReadLine(), out decimal amount))
        {
            if (amount <= 0)
            {
                Console.WriteLine("Amount must be greater than zero.");
            }
            else if (amount > balance)
            {
                Console.WriteLine("Insufficient balance.");
            }
            else
            {
                balance -= amount;
                history.Add($"Withdraw: ₱{amount}");

                Console.WriteLine("Withdrawal successful.");
                Console.WriteLine($"Remaining Balance: ₱{balance}");
            }
        }
        else
        {
            Console.WriteLine("Invalid amount.");
        }
    }

    static void ShowHistory()
    {
        Console.WriteLine("\n===== TRANSACTION HISTORY =====");

        if (history.Count == 0)
        {
            Console.WriteLine("No transactions found.");
        }
        else
        {
            foreach (string transaction in history)
            {
                Console.WriteLine(transaction);
            }
        }
    }
}
