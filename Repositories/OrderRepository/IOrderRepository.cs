using DiningHall.Models;
using DiningHall.Models.Status;

namespace DiningHall.Repositories.OrderRepository;

public interface IOrderRepository
{
    void InsertOrder(Order order);
    Task<IList<Order>> GetAll();
    Task<Order?> GetById(Task<int> id);
    Task<Order?> GetOrderByStatus(OrderStatus status);

    Task<Order?> GetOrderByTableId(Task<int> id);
}