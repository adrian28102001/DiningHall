using System.Collections.Concurrent;
using DiningHall.Helpers;
using DiningHall.Models;
using DiningHall.Models.Status;
using DiningHall.Repositories.TableRepository;
using DiningHall.Repositories.WaiterRepository;
using DiningHall.Services.OrderService;

namespace DiningHall.Services.WaiterService;

public class WaiterService : IWaiterService
{
    private readonly IWaiterRepository _waiterRepository;
    private readonly ITableRepository _tableRepository;
    private readonly IOrderService _orderService;

    public WaiterService(IOrderService orderService, IWaiterRepository waiterRepository,
        ITableRepository tableRepository)
    {
        _orderService = orderService;
        _waiterRepository = waiterRepository;
        _tableRepository = tableRepository;
    }

    public Task GenerateWaiters()
    {
        return _waiterRepository.GenerateWaiters();
    }

    public async Task SleepWaiter()
    {
        await Task.Run(async () =>
            {
                var randomSleepTime = RandomGenerator.NumberGenerator(15, 30);
                ConsoleHelper.Print($"Waiter will sleep for: {randomSleepTime}", ConsoleColor.Yellow);
                await SleepGenerator.Delay(randomSleepTime);
            }
        );
    }

    public Task<ConcurrentBag<Waiter>> GetAll()
    {
        return _waiterRepository.GetAll();
    }

    public Task<Waiter?> GetById(int id)
    {
        return _waiterRepository.GetById(id);
    }

    private Task<Waiter?> GetFreeWaiter()
    {
        return _waiterRepository.GetFreeWaiter();
    }

    public async Task ServeTable()
    {
        var waiter = await GetFreeWaiter();

        if (waiter != null)
        {
            var table = await _tableRepository.GetTableByStatus(TableStatus.WaitingForWaiter);

            if (table != null)
            {
                var order = await _orderService.GetOrderByTableId(table.Id);
                if (order != null)
                {
                    order.WaiterId = waiter.Id;

                    waiter.Order = order;
                    waiter.IsFree = false;
                    waiter.ActiveOrders.Add(order);

                    Task.Run(() => _orderService.SendOrder(order));

                    ConsoleHelper.Print(
                        $"I am {waiter.Name} and I drive order {order.Id} in the kitchen from table {table.Id}",
                        ConsoleColor.Blue);
                    table.TableStatus = TableStatus.WaitingForOrderToBeServed;

                    var sleepTime = RandomGenerator.NumberGenerator(10);
                    ConsoleHelper.Print($"I am waiter {waiter.Name}. I will rest for {sleepTime} seconds",
                        ConsoleColor.Yellow);
                    ConsoleHelper.Print($"Waiter {waiter.Name} is ready for a new order", ConsoleColor.Green);
                    waiter.IsFree = true;
                }
            }
            else
            {
                ConsoleHelper.Print("There are no tables that need an waiter now", ConsoleColor.Red);
            }
        }
        else
        {
            ConsoleHelper.Print("There are no free waiters now", ConsoleColor.Red);
        }
    }
}