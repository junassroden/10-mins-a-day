int num = 153;
int original = num;
int sum = 0;

while (num > 0)
{
    int digit = num % 10;
    sum += digit * digit * digit;
    num /= 10;
}

if (sum == original)
{
    Console.WriteLine("Armstrong Number");
}
else
{
    Console.WriteLine("Not an Armstrong Number");
}
