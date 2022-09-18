using DiningHall.Models;
using DiningHall.Models.Status;

namespace DiningHall.Services.OrderService;

public interface IOrderService
{
    Task GenerateOrder();
    void SendOrder(Order order);
    IList<Order> GetAll();
    Order? GetById(Task<int> id);
    Order? GetOrderByStatus(OrderStatus status);
    Order? GetOrderByTableId(Task<int> id);
    void AssignOrderWaiter(Order order, Task<int> waiterId);
    Task ChangeOrderDetails(Order order, Task<int> waiterId, OrderStatus status);
}