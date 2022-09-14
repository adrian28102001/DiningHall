using System.Text;
using DiningHall.Helpers;
using DiningHall.Models;
using DiningHall.Repositories.FoodRepository;
using DiningHall.Repositories.OrderRepository;
using DiningHall.Repositories.TableRepository;
using DiningHall.Services.FoodService;
using DiningHall.Services.TableRepository;
using Newtonsoft.Json;

namespace DiningHall.Services.OrderService;

public class OrderService : IOrderService
{
    private readonly ITableRepository _tableRepository;
    private readonly IFoodRepository _foodRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly ITableService _tableService;
    private readonly IFoodService _foodService;

    public OrderService(ITableRepository tableRepository, IFoodRepository foodRepository,
        IOrderRepository orderRepository, ITableService tableService, IFoodService foodService)
    {
        _tableRepository = tableRepository;
        _foodRepository = foodRepository;
        _orderRepository = orderRepository;
        _tableService = tableService;
        _foodService = foodService;
    }

    public void GenerateOrder()
    {
        Console.BackgroundColor = ConsoleColor.Blue;
        while (true)
        {
            var table = _tableRepository.GetTableByStatus(Status.IsAvailable);
            if (table != null && table.Status == Status.IsAvailable)
            {
                var foodList = _foodService.GenerateOrderFood();
                var order = new Order
                {
                    Id = IdGenerator.GenerateId(),
                    TableId = table.Id,
                    Priority = RandomGenerator.NumberGenerator(3),
                    CreatedOnUtc = DateTime.UtcNow,
                    OrderIsComplete = false,
                    FoodList = foodList,
                    MaxWait = foodList.CalculateMaximWaitingTime(_foodRepository)
                };

                table.Status = Status.ReadyToOrder;
                table.OrderId = order.Id;

                _orderRepository.InsertOrder(order);
                Console.WriteLine($"Table {table.Id} has order id {order.Id} and status {order.Status}");
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Red;
                var tableWithSmallestWaitingTime = _tableService.GetTableWithSmallestWaitingTime();
                if (tableWithSmallestWaitingTime != null)
                {
                    var order = GetById(tableWithSmallestWaitingTime.OrderId);
                    Console.WriteLine($"I am sorry, we have no free tables, we need to wait for next table that is ready in: {order!.MaxWait}");
                    Thread.Sleep(order.MaxWait * 100);
                    continue;
                }
            }

            break;
        }
    }

    public async void SendOrder(Order order)
    {
        var json = JsonConvert.SerializeObject(order);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        const string url = Settings.KitchenUrl;
        using var client = new HttpClient();

        var response = await client.PostAsync(url, data);
    }

    public IList<Order> GetAll()
    {
        return _orderRepository.GetAll();
    }

    public Order? GetById(int id)
    {
        return _orderRepository.GetById(id);
    }

    public Order? GetOrderByStatus(Status status)
    {
        return _orderRepository.GetOrderByStatus(status);
    }

    public Order? GetOrderByTableId(int id)
    {
        return _orderRepository.GetOrderByTableId(id);
    }
}