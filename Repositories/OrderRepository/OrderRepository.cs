using DiningHall.Helpers;
using DiningHall.Models;
using DiningHall.Repositories.FoodRepository;

namespace DiningHall.Repositories.OrderRepository;

public class OrderRepository : IOrderRepository
{
    private readonly IFoodRepository _foodRepository;
    private IList<Order> Orders { get; set; }
    private IList<Waiter> Waiters { get; set; }


    public OrderRepository(IList<Order> orders, IList<Waiter> waiters, IFoodRepository foodRepository)
    {
        Orders = orders;
        Waiters = waiters;
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