using DiningHall.Models;
using DiningHall.Models.Status;

namespace DiningHall.Services.OrderService;

public interface IOrderService
{
    Task GenerateOrder();
    Task SendOrder(Order order);
    Task<IList<Order>> GetAll();
    Task<Order?> GetById(Task<int> id);
    Task<Order?> GetOrderByStatus(OrderStatus status);
    Task<Order?> GetOrderByTableId(Task<int> id);
    Task ChangeOrderDetails(Order order, Task<int> waiterId, OrderStatus status);
}