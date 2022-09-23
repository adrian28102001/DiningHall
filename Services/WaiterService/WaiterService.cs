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
        while (true)
        {
            var waiter = await GetFreeWaiter();
            var table = await _tableRepository.GetTableByStatus(TableStatus.WaitingForWaiter);

            if (waiter != null && table != null)
            {
                var order = await _orderService.GetOrderByTableId(table.Id);

                if (order != null)
                {
                    order.WaiterId = waiter.Id;

                    waiter.Order = order;
                    waiter.IsFree = false;
                    waiter.ActiveOrders.Add(order);
                    
                    await _orderService.SendOrder(order);
                    ConsoleHelper.Print($"I am {waiter.Name} and I drive order {order.Id} in the kitchen", ConsoleColor.Blue);
                    table.TableStatus = TableStatus.WaitingForOrderToBeServed;

                    var sleepTime = RandomGenerator.NumberGenerator(60);
                    ConsoleHelper.Print($"I am waiter {waiter.Name}. I will rest for {sleepTime} seconds", ConsoleColor.Yellow);
                    
                    await SleepGenerator.Delay(sleepTime);
                    ConsoleHelper.Print($"Waiter {waiter.Name} is ready for a new order", ConsoleColor.Green);
                    waiter.IsFree = true;
                }
            }
            else if (waiter == null)
            {
                ConsoleHelper.Print("There are no free waiters now", ConsoleColor.Red);
                await SleepGenerator.Delay(RandomGenerator.NumberGenerator(20, 40));
                continue;
            }
            else if (table == null)
            {
                ConsoleHelper.Print("There are no tables that need an waiter now" , ConsoleColor.Red);
                await SleepGenerator.Delay(RandomGenerator.NumberGenerator(20, 40));
                continue;
            }

            break;
        }
    }
}