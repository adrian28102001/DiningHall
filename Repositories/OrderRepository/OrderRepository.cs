using DiningHall.Models;

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

    public IList<Order> GetAll()
    {
        return _orders;
    }

    public Order? GetById(int id)
    {
        return _orders.FirstOrDefault(order => order.Id.Equals(id));
    }

    public Order? GetOrderByStatus(Status status)
    {
        return _orders.FirstOrDefault(order => order.Status == status);
    }

    public Order? GetOrderByTableId(int id)
    {
        return _orders.FirstOrDefault(order => order.TableId.Equals(id));
    }
}