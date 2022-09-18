namespace DiningHall.Helpers;

public static class ConsoleHelper
{
    public static Task Print(string message)
    {
        Console.WriteLine(message);
        return Task.CompletedTask;
    }
    public static void Print (string message, ConsoleColor color)
    {
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(message);
    }
}