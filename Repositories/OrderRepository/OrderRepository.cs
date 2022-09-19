using DiningHall.Models;
using DiningHall.Models.Status;

namespace DiningHall.Repositories.OrderRepository;

public class OrderRepository : IOrderRepository
{
    private readonly IList<Order> _orders;

    public OrderRepository()
    {
        _orders = new List<Order>();
    }

    public void InsertOrder(Order order)
    {
        _orders.Add(order);
    }

    public Task<IList<Order>> GetAll()
    {
        return Task.FromResult(_orders);
    }

    public Task<Order?> GetById(Task<int> id)
    {
        return Task.FromResult(_orders.FirstOrDefault(order => order.Id.Equals(id)));
    }

    public Task<Order?> GetOrderByStatus(OrderStatus status)
    {
        return Task.FromResult(_orders.FirstOrDefault(order => order.OrderStatus == status));
    }

    public Task<Order?> GetOrderByTableId(Task<int> id)
    {
        return Task.FromResult(_orders.FirstOrDefault(order => order.TableId.Equals(id)));
    }
}