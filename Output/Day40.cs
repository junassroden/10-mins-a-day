using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter customer name: ");
        string customerName = Console.ReadLine();

        int transactionCount;

        while (true)
        {
            Console.Write("Enter number of transactions: ");

            if (int.TryParse(Console.ReadLine(), out transactionCount) &&
                transactionCount > 0)
            {
                break;
            }

            Console.WriteLine("Invalid number of transactions.");
        }

        char[] transactionTypes = new char[transactionCount];
        decimal[] transactionAmounts = new decimal[transactionCount];

        decimal balance = 0;
        decimal totalDeposits = 0;
        decimal totalWithdrawals = 0;

        int depositCount = 0;
        int withdrawalCount = 0;

        decimal largestDeposit = 0;
        decimal largestWithdrawal = 0;

        for (int i = 0; i < transactionCount; i++)
        {
            char type;

            while (true)
            {
                Console.Write($"\nTransaction #{i + 1} (D = Deposit, W = Withdrawal): ");
                string input = Console.ReadLine().ToUpper();

                if (input == "D" || input == "W")
                {
                    type = input[0];
                    break;
                }

                Console.WriteLine("Invalid transaction type.");
            }

            decimal amount;

            while (true)
            {
                Console.Write("Enter amount: ");

                if (decimal.TryParse(Console.ReadLine(), out amount) &&
                    amount > 0)
                {
                    break;
                }

                Console.WriteLine("Invalid amount. Enter a value greater than 0.");
            }

            // Deposit
            if (type == 'D')
            {
                balance += amount;
                totalDeposits += amount;
                depositCount++;

                if (amount > largestDeposit)
                {
                    largestDeposit = amount;
                }

                transactionTypes[i] = type;
                transactionAmounts[i] = amount;

                Console.WriteLine("Deposit successful.");
            }

            // Withdrawal
            else
            {
                if (amount > balance)
                {
                    Console.WriteLine(
                        "Transaction rejected: Insufficient balance.");

                    // Don't count rejected transaction
                    i--;
                    continue;
                }

                balance -= amount;
                totalWithdrawals += amount;
                withdrawalCount++;

                if (amount > largestWithdrawal)
                {
                    largestWithdrawal = amount;
                }

                transactionTypes[i] = type;
                transactionAmounts[i] = amount;

                Console.WriteLine("Withdrawal successful.");
            }
        }

        // Determine customer status
        string status;

        if (balance >= 50000)
        {
            status = "Premium";
        }
        else if (balance >= 20000)
        {
            status = "Gold";
        }
        else if (balance >= 5000)
        {
            status = "Silver";
        }
        else
        {
            status = "Basic";
        }

        // Find highest transaction
        decimal highestTransaction = 0;
        char highestTransactionType = ' ';

        for (int i = 0; i < transactionCount; i++)
        {
            if (transactionAmounts[i] > highestTransaction)
            {
                highestTransaction = transactionAmounts[i];
                highestTransactionType = transactionTypes[i];
            }
        }

        // Display report
        Console.WriteLine("\n=================================");
        Console.WriteLine("          BANK REPORT");
        Console.WriteLine("=================================");

        Console.WriteLine($"Customer: {customerName}");

        Console.WriteLine($"\nTotal Deposits     : ₱{totalDeposits:N2}");
        Console.WriteLine($"Total Withdrawals  : ₱{totalWithdrawals:N2}");
        Console.WriteLine($"Final Balance      : ₱{balance:N2}");

        Console.WriteLine($"\nNumber of Deposits    : {depositCount}");
        Console.WriteLine($"Number of Withdrawals : {withdrawalCount}");

        Console.WriteLine($"\nLargest Deposit       : ₱{largestDeposit:N2}");
        Console.WriteLine($"Largest Withdrawal    : ₱{largestWithdrawal:N2}");

        Console.WriteLine($"\nCustomer Status       : {status}");

        Console.WriteLine(
            $"\nHighest Transaction   : {highestTransactionType} - ₱{highestTransaction:N2}");

        Console.WriteLine("=================================");
    }
}
