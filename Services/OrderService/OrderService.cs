using System.Collections.Concurrent;
using System.Net;
using System.Text;
using DiningHall.Helpers;
using DiningHall.Models;
using DiningHall.Models.Status;
using DiningHall.Repositories.OrderRepository;
using DiningHall.Services.FoodService;
using DiningHall.Services.TableRepository;
using Newtonsoft.Json;

namespace DiningHall.Services.OrderService;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ITableService _tableService;
    private readonly IFoodService _foodService;

    public OrderService(IOrderRepository orderRepository, IFoodService foodService, ITableService tableService)
    {
        _orderRepository = orderRepository;
        _foodService = foodService;
        _tableService = tableService;
    }

    public async Task GenerateOrder()
    {
        var table = await _tableService.GetTableByStatus(TableStatus.IsAvailable);

        if (table != null)
        {
            var foodList = await _foodService.GenerateOrderFood();
            var order = new Order
            {
                Id = await IdGenerator.GenerateId(),
                TableId = table.Id,
                Priority = RandomGenerator.NumberGenerator(5),
                CreatedOnUtc = DateTime.Now,
                OrderIsComplete = false,
                FoodList = foodList,
                MaxWait = foodList.CalculateMaximWaitingTime(_foodService),
            };

            table.OrderId = order.Id;
            table.TableStatus = TableStatus.WaitingForWaiter;

            _orderRepository.InsertOrder(order);
            ConsoleHelper.Print($"A order with id {order.Id} was generated", ConsoleColor.Green);
            var sleepingTime = RandomGenerator.NumberGenerator(5, 10);
            ConsoleHelper.Print($"The next order in: {sleepingTime} seconds", ConsoleColor.Yellow);
            await SleepGenerator.Delay(sleepingTime);
        }
        else
        {
            var tableWithSmallestWaitingTime = await _tableService.GetTableWithSmallestWaitingTime();
            if (tableWithSmallestWaitingTime != null)
            {
                var order = await GetById(tableWithSmallestWaitingTime.OrderId);
                if (order != null)
                {
                    ConsoleHelper.Print($"There are no free tables now, you need to wait {order.MaxWait}",
                        ConsoleColor.DarkRed);
                    await SleepGenerator.Delay(order.MaxWait);
                }
            }
        }
    }

    public async Task SendOrder(Order order)
    {
        await Task.Run(async () => { 
            try
            {
                var serializeObject = JsonConvert.SerializeObject(order);
                var data = new StringContent(serializeObject, Encoding.UTF8, "application/json");

                const string url = Settings.KitchenUrl;
                using var client = new HttpClient();

                var response = await client.PostAsync(url, data);

                if (response.StatusCode == HttpStatusCode.Accepted)
                {
                    ConsoleHelper.Print($"The order with id {order.Id} was driven in the kitchen");
                    order.OrderStatus = OrderStatus.OrderInTheKitchen;
                }
            }
            catch (Exception e)
            {
                ConsoleHelper.Print($"Failed to send order {order.Id}", ConsoleColor.Red);
            }
        });
    }

    public Task<ConcurrentBag<Order>> GetAll()
    {
        return _orderRepository.GetAll();
    }

    private Task<Order?> GetById(int id)
    {
        return _orderRepository.GetById(id);
    }

    public Task<Order?> GetOrderByStatus(OrderStatus status)
    {
        return _orderRepository.GetOrderByStatus(status);
    }

    public Task<Order?> GetOrderByTableId(int id)
    {
        return _orderRepository.GetOrderByTableId(id);
    }
}