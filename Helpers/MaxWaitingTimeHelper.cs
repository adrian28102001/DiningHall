using DiningHall.Repositories.FoodRepository;

namespace DiningHall.Helpers;

public static class MaxWaitingTimeHelper
{
    public static int CalculateMaximWaitingTime(this IEnumerable<int> foodList, IFoodRepository repository)
    {
        var maxWaitingTime = 0;

        foreach (var foodId in foodList)
        {
            var food = repository.GetById(foodId);
            if (food != null)
            {
                maxWaitingTime += food.PreparationTime;
            }
        }

        return (int) (maxWaitingTime * 1.3);
    }
}