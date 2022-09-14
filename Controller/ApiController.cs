using DiningHall.Models;
using DiningHall.Repositories.FoodRepository;
using DiningHall.Repositories.OrderRepository;
using DiningHall.Repositories.TableRepository;
using DiningHall.Repositories.WaiterRepository;
using Microsoft.AspNetCore.Mvc;

namespace DiningHall.Controller;

[ApiController]
[Route("[controller]")]
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

    [HttpGet("menu")]
    public IList<Food> GetMenu()
    {
        return _foodRepository.GetAll();
    }

    [HttpGet("menu/{id:int}")]
    public Food? GetItemFromMenu(int id)
    {
        return _foodRepository.GetById(id);
    }

    [HttpGet("waiters")]
    public IList<Waiter> GetWaiters()
    {
        return _waiterRepository.GetAll();
    }

    [HttpGet("waiters/{id:int}")]
    public Waiter? GetWaiter(int id)
    {
        return _waiterRepository.GetById(id);
    }

    [HttpGet("tables")]
    public IList<Table> GetTables()
    {
        return _tableRepository.GetAll();
    }

    [HttpGet("tables/{id:int}")]
    public Table? GetTablesById(int id)
    {
        return _tableRepository.GetById(id);
    }

    [HttpGet("orders")]
    public IList<Order> GetOrders()
    {
        return _orderRepository.GetAll();
    }
    
    [HttpGet("orders/{id:int}")]
    public Order? GetOrderById(int id)
    {
        return _orderRepository.GetById(id);
    }
    
    [HttpPost("sendorder")]
    public void SendOrder([FromBody] Order order)
    {
        var finsihedOrder = order;
        Console.BackgroundColor = ConsoleColor.Red;
        Console.WriteLine($"I recieved from the kitchen an order with id {order.Id} for table {order.TableId}");
        var waiter = _waiterRepository.GetById(order.WaiterId);
        Console.BackgroundColor = ConsoleColor.Red;
        Console.WriteLine($"Dear waiter {waiter.Name}, please come and take the order");
        
        //serve order
        //get rating
    }
}