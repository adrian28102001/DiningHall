namespace DiningHall.Helpers;

public static class SleepGenerator
{
    public static void Sleep(int sleep)
    {
        Thread.Sleep(TimeSpan.FromSeconds(sleep));
    }
    public static void Delay(int sleep)
    {
        Task.Delay(TimeSpan.FromSeconds(sleep));
    }
}