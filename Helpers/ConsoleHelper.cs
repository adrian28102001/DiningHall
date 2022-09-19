namespace DiningHall.Helpers;

public static class ConsoleHelper
{
    public static void Print (string message)
    {
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(message);
    }
}