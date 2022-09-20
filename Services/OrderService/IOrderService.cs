using System.Collections.Concurrent;
using DiningHall.Models;
using DiningHall.Models.Status;

namespace DiningHall.Services.OrderService;

public interface IOrderService
{
    Task GenerateOrder();
    Task SendOrder(Order order);
    Task<ConcurrentBag<Order>> GetAll();
    Task<Order?> GetById(Task<int> id);
    Task<Order?> GetOrderByStatus(OrderStatus status);
    Task<Order?> GetOrderByTableId(int id);
    Task ChangeOrderDetails(Order order, int waiterId, OrderStatus status);
}