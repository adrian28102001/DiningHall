namespace DiningHall.Helpers;

public class RandomGenerator
{
    public static int NumberGenerator(int max)
    {
        var random = new Random();
        return random.Next(1, max);
    }
}