namespace DiningHall.Helpers;

public static class SleepGenerator
{
    public static void Sleep(int sleep)
    {
        Thread.Sleep(TimeSpan.FromSeconds(sleep));
    }

    public static Task Delay(int sleep)
    {
        return Task.Delay(TimeSpan.FromSeconds(sleep));
    }
}