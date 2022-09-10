using DiningHall.Models;

namespace DiningHall.Repositories.OrderRepository;

public interface IOrderRepository
{
    public Order GenerateOrder(int table, int waiter );
}