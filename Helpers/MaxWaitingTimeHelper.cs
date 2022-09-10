using DiningHall.Repositories.FoodRepository;

namespace DiningHall.Helpers;

public static class MaxWaitingTimeHelper
{
    public static int CalculateMaximWaitingTime(this IList<int> foodList, IFoodRepository repository)
    {
        var maxWaitingTime = 0;
        foreach (var foodId in foodList)
        {
            var food = repository.GetFoodById(foodId);
            maxWaitingTime += food.PreparationTime;
        }

        return (int) (maxWaitingTime * 1.3);
    }
}