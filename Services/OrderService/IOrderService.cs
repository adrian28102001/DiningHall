using DiningHall.Models;

namespace DiningHall.Services.OrderService;

public interface IOrderService
{
    void GenerateOrder();
    void SendOrder(Order order);
    IList<Order> GetAll();
    Order? GetById(int id);
    Order? GetOrderByStatus(Status status);
    Order? GetOrderByTableId(int id);
}