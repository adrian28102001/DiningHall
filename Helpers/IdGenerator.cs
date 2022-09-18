namespace DiningHall.Helpers;

public static class IdGenerator
{
    private static int Id { get; set; }

    public static async Task<int> GenerateId()
    {
        return await Task.FromResult(Id += 1);
    }
}