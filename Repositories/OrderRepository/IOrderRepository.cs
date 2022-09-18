using DiningHall.Models;
using DiningHall.Models.Status;

namespace DiningHall.Repositories.OrderRepository;

public interface IOrderRepository
{
    void InsertOrder(Order order);
    IList<Order> GetAll();
    Order? GetById(Task<int> id);
    Order? GetOrderByStatus(OrderStatus status);

    Order? GetOrderByTableId(Task<int> id);
}