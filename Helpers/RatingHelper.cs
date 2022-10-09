using DiningHall.Models;

namespace DiningHall.Helpers;

public static class RatingHelper
{
    private static readonly List<int> Rating = new();
    
    public static void GetRating(Order order)
    {
        var servedTime = Math.Abs(order.CreatedOnUtc.Subtract(order.UpdatedOnUtc).TotalSeconds);

        if (servedTime < order.MaxWait)
        {
            Rating.Add(5);
        }
        else if (servedTime < order.MaxWait * 1.1)
        {
            Rating.Add(4);
        }
        else if (servedTime < order.MaxWait * 1.2)
        {
            Rating.Add(3);
        }
        else if (servedTime < order.MaxWait * 1.3)
        {
            Rating.Add(2);
        }
        else if (servedTime < order.MaxWait * 1.4)
        {
            Rating.Add(1);
        }
        
        ConsoleHelper.Print($"Rating for {Rating.Count} is :{Rating.Sum() / Rating.Count}", ConsoleColor.Magenta);
    }
    
}