using DiningHall.Helpers;
using DiningHall.Models;
using DiningHall.Repositories.FoodRepository;

namespace DiningHall.Repositories.OrderRepository;

public class OrderRepository : IOrderRepository
{
    private readonly IFoodRepository _foodRepository;
    
    public OrderRepository(IFoodRepository foodRepository)
    {
        _foodRepository = foodRepository;
    }

    public Order GenerateOrder(int table, int waiter)
    {
        var foodList = _foodRepository.GenerateOrderFood();
        return new Order
        {
            Id = IdGenerator.GenerateId(),
            Priority = RandomGenerator.NumberGenerator(3),
            CreatedOnUtc = DateTime.UtcNow,
            OrderIsComplete = false,
            FoodList = foodList,
            TableId = table,
            WaiterId = waiter,
            MaxWait = foodList.CalculateMaximWaitingTime(_foodRepository)
        };
    }
}