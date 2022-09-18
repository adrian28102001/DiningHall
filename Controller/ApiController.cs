using DiningHall.Helpers;
using DiningHall.Models;
using DiningHall.Models.Status;
using DiningHall.Repositories.FoodRepository;
using DiningHall.Repositories.OrderRepository;
using DiningHall.Repositories.TableRepository;
using DiningHall.Repositories.WaiterRepository;
using Microsoft.AspNetCore.Mvc;

namespace DiningHall.Controller;

[ApiController]
[Route("/dininghall")]
public class ApiController : ControllerBase
{
    private readonly IFoodRepository _foodRepository;
    private readonly ITableRepository _tableRepository;
    private readonly IWaiterRepository _waiterRepository;
    private readonly IOrderRepository _orderRepository;

    public ApiController(IFoodRepository foodRepository, ITableRepository tableRepository,
        IWaiterRepository waiterRepository, IOrderRepository orderRepository)
    {
        _foodRepository = foodRepository;
        _tableRepository = tableRepository;
        _waiterRepository = waiterRepository;
        _orderRepository = orderRepository;
    }

    [HttpGet]
    public IList<Order> GetOrders()
    {
        return _orderRepository.GetAll();
    }

    [HttpPost]
    public void SendOrder([FromBody] Order order)
    {
        order.OrderStatus = OrderStatus.OrderCooked;
        var table = _tableRepository.GetById(order.TableId);
        if (table != null)
        {
            table.TableStatus = TableStatus.IsAvailable;
            ConsoleHelper.Print($"I received from the kitchen an order with id {order.Id.Result} for table {order.TableId}");
        }

        var waiter = _waiterRepository.GetById(order.WaiterId);
        ConsoleHelper.Print($"Dear {waiter.Name}, please come and take the order {order.Id.Result} for table {order.TableId}");

        //serve order
        //get rating
    }
}