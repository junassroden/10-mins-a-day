int[] numbers = { 12, 45, 8, 90, 67, 90 };

int largest = int.MinValue;
int secondLargest = int.MinValue;

foreach (int num in numbers)
{
    if (num > largest)
    {
        secondLargest = largest;
        largest = num;
    }
    else if (num > secondLargest && num != largest)
    {
        secondLargest = num;
    }
}

Console.WriteLine("Second Largest: " + secondLargest);
