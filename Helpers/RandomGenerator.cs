namespace DiningHall.Helpers;

public class RandomGenerator
{
    private static Random _randomGenerator;

    public RandomGenerator(Random randomGenerator)
    {
        _randomGenerator = randomGenerator;
    }

    public static int NumberGenerator(int max)
    {
        return _randomGenerator.Next(1, max);
    }
}