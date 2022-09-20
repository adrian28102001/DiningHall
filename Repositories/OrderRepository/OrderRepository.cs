using System.Collections.Concurrent;
using DiningHall.Models;
using DiningHall.Models.Status;

namespace DiningHall.Repositories.OrderRepository;

public class OrderRepository : IOrderRepository
{
    private readonly ConcurrentBag<Order> _orders;

    public OrderRepository()
    {
        _orders = new ConcurrentBag<Order>();
    }

    public void InsertOrder(Order order)
    {
        _orders.Add(order);
    }

    public Task<ConcurrentBag<Order>> GetAll()
    {
        return Task.FromResult(_orders);
    }

    public Task<Order?> GetById(int id)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    public Task<Order?> GetOrderByTableId(int id)
    {
        return Task.FromResult(_orders.FirstOrDefault(order => order.TableId.Equals(id)));
    }
}