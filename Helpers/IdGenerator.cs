namespace DiningHall.Helpers;

public static class IdGenerator
{
    private static int Id { get; set; } = 1;
    public static int GenerateId()
    {
        return Id++;
    }
}