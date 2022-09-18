using DiningHall.Services.FoodService;

namespace DiningHall.Helpers;

public static class MaxWaitingTimeHelper
{
    public static int CalculateMaximWaitingTime(this IEnumerable<int> foodList, IFoodService repository)
    {
        var maxWaitingTime = foodList.Select(repository.GetById).Where(food => food != null).Sum(food => food!.PreparationTime);

        return (int) (maxWaitingTime * 1.3);
    }
}