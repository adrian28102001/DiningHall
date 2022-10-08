using System.Collections.Concurrent;
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
    public Task<ConcurrentBag<Order>> GetOrders()
    {
        return _orderRepository.GetAll();
    }

    [HttpPost]
    public async Task GetOrderFromKitchen([FromBody] Order order)
    {
        var servedTime = order.UpdatedOnUtc - order.CreatedOnUtc;
        order.OrderStatus = OrderStatus.OrderCooked;
        var table = await _tableRepository.GetById(order.TableId);
        if (table != null)
        {
            table.TableStatus = TableStatus.IsAvailable;
            ConsoleHelper.Print($"I received from the kitchen an order with id {order.Id} for table {order.TableId}");
        }
        
        ConsoleHelper.Print($"This order with maxim waiting time {order.MaxWait} was served in {servedTime}");

        if (servedTime.Duration().Minutes < order.MaxWait)
        {
            Console.WriteLine("Rating 5");
        }
        else if (servedTime.Duration().Minutes < order.MaxWait * 1.1)
        {
            Console.WriteLine("Rating 4");
        }
        else if (servedTime.Duration().Minutes < order.MaxWait * 1.2)
        {
            Console.WriteLine("Rating 3");
        }
        else if (servedTime.Duration().Minutes < order.MaxWait * 1.3)
        {
            Console.WriteLine("Rating 2");
        }
        else if (servedTime.Duration().Minutes < order.MaxWait * 1.4)
        {
            Console.WriteLine("Rating 1");
        }

        //serve order
        //get rating
    }
}