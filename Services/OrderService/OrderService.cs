﻿using System.Net;
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
        while (true)
        {
            var table = await _tableService.GetTableByStatus(TableStatus.IsAvailable);

            if (table != null)
            {
                var foodList = await _foodService.GenerateOrderFood();
                var order = new Order
                {
                    Id = IdGenerator.GenerateId(),
                    TableId = table.Id,
                    Priority = RandomGenerator.NumberGenerator(3),
                    CreatedOnUtc = DateTime.UtcNow,
                    OrderIsComplete = false,
                    FoodList = foodList,
                    MaxWait = foodList.CalculateMaximWaitingTime(_foodService),
                };

                table.OrderId = order.Id;   
                table.TableStatus = TableStatus.WaitingForWaiter;

                _orderRepository.InsertOrder(order);
                ConsoleHelper.Print($"A order with id {order.Id.Result} was generated");
                var randomSleepTime = RandomGenerator.NumberGenerator(20, 40);
                ConsoleHelper.Print($"Next order will be generated in: {randomSleepTime}");
                await SleepGenerator.Delay(randomSleepTime);
            }
            else
            {
                var tableWithSmallestWaitingTime = await _tableService.GetTableWithSmallestWaitingTime();
                if (tableWithSmallestWaitingTime != null)
                {
                    var order = await GetById(tableWithSmallestWaitingTime.OrderId);
                    if (order != null)
                    {
                        ConsoleHelper.Print($"There are no free tables now, you need to wait {order.MaxWait}");
                        await SleepGenerator.Delay(order.MaxWait); // sleep
                        continue;
                    }
                }
            }
            break;
        }
    }

    public async Task SendOrder(Order order)
    {
        try
        {
            var serializeObject = JsonConvert.SerializeObject(order);
            var data = new StringContent(serializeObject, Encoding.UTF8, "application/json");

            const string url = Settings.KitchenUrl;
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            if (response.StatusCode != HttpStatusCode.Accepted) return;
            ConsoleHelper.Print($"The order with id {order.Id} was driven in the kitchen");
            await ChangeOrderStatus(order, OrderStatus.OrderInTheKitchen);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to send order {order.Id}");
        }
    }

    public Task<IList<Order>> GetAll()
    {
        return _orderRepository.GetAll();
    }

    public Task<Order?> GetById(Task<int> id)
    {
        return _orderRepository.GetById(id);
    }

    public Task<Order?> GetOrderByStatus(OrderStatus status)
    {
        return _orderRepository.GetOrderByStatus(status);
    }

    public Task<Order?> GetOrderByTableId(Task<int> id)
    {
        return _orderRepository.GetOrderByTableId(id);
    }

    public Task ChangeOrderDetails(Order order, Task<int> waiterId, OrderStatus status)
    {
        order.WaiterId = waiterId;
        order.OrderStatus = status;
        return Task.CompletedTask;
    }

    private static Task ChangeOrderStatus(Order order, OrderStatus status)
    {
        order.OrderStatus = status;
        return Task.CompletedTask;
    }
    
}