using DiningHall.Helpers;
using DiningHall.Models;
using DiningHall.Models.Status;
using DiningHall.Repositories.TableRepository;
using DiningHall.Repositories.WaiterRepository;
using DiningHall.Services.OrderService;
using DiningHall.Services.TableRepository;

namespace DiningHall.Services.WaiterService;

public class WaiterService : IWaiterService
{
    private readonly IWaiterRepository _waiterRepository;
    private readonly ITableRepository _tableRepository;
    private readonly IOrderService _orderService;
    private readonly ITableService _tableService;

    public WaiterService(IOrderService orderService, ITableService tableService, IWaiterRepository waiterRepository,
        ITableRepository tableRepository)
    {
        _orderService = orderService;
        _tableService = tableService;
        _waiterRepository = waiterRepository;
        _tableRepository = tableRepository;
    }

    public Task GenerateWaiters()
    {
        return _waiterRepository.GenerateWaiters();
    }

    public void ChangeWaiterStatus(Waiter waiter, bool isFree)
    {
        waiter.IsFree = isFree;
    }

    public IList<Waiter> GetAll()
    {
        return _waiterRepository.GetAll();
    }

    public Waiter GetById(Task<int> id)
    {
        return _waiterRepository.GetById(id);
    }

    private Waiter? GetFreeWaiter()
    {
        return _waiterRepository.GetFreeWaiter();
    }

    private static void RestWaiter(Waiter waiter)
    {
        var sleepTime = RandomGenerator.NumberGenerator(100);
        ConsoleHelper.Print($"I sent the order in the kitchen and I will rest for {sleepTime} seconds",
            ConsoleColor.DarkYellow);
        SleepGenerator.Sleep(sleepTime);
        ConsoleHelper.Print($"Waiter {waiter.Name} is ready for a new order", ConsoleColor.DarkGreen);
    }

    public void ChangeWaiterDetails(Waiter waiter, Order order, bool isFree)
    {
        waiter.Order = order;
        waiter.IsFree = isFree;
        waiter.ActiveOrders.Add(order);
    }

    public async Task ServeTable()
    {
        await Task.Run(() =>
        {
            while (true)
            {
                var waiter = GetFreeWaiter();
                var table = _tableRepository.GetTableByStatus(TableStatus.WaitingForWaiter);

                if (waiter != null && table != null)
                {
                    var order = _orderService.GetOrderByTableId(table.Id);

                    if (order != null)
                    {
                        _orderService.AssignOrderWaiter(order, waiter.Id);
                        ChangeWaiterDetails(waiter, order, false);
                        ConsoleHelper.Print(
                            $"I am {waiter.Name} and I drive order {order.Id.Result} in the kitchen");
                        _orderService.SendOrder(order);
                        _tableService.ChangeTableStatus(table, TableStatus.WaitingForOrderToBeServed);
                        RestWaiter(waiter);
                        ChangeWaiterStatus(waiter, true);
                    }
                }
                else if (waiter == null)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    ConsoleHelper.Print("There are no free waiters now", ConsoleColor.Red);
                    SleepGenerator.Sleep(RandomGenerator.NumberGenerator(20, 40));
                    continue;
                }
                else if (table == null)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    ConsoleHelper.Print("There are no tables that need an waiter now", ConsoleColor.Red);
                    SleepGenerator.Sleep(RandomGenerator.NumberGenerator(20, 40));
                    continue;
                }

                break;
            }
        });
    }
}