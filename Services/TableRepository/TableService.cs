using DiningHall.Helpers;
using DiningHall.Models;
using DiningHall.Repositories.OrderRepository;
using DiningHall.Repositories.TableRepository;
using DiningHall.Repositories.WaiterRepository;

namespace DiningHall.Services.TableRepository;

public class TableService : ITableService
{
    private readonly ITableRepository _tableRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IWaiterRepository _waiterRepository;

    public TableService(ITableRepository tableRepository, IOrderRepository orderRepository,
        IWaiterRepository waiterRepository)
    {
        _tableRepository = tableRepository;
        _orderRepository = orderRepository;
        _waiterRepository = waiterRepository;
    }


    public IList<Table> GetAll()
    {
        return _tableRepository.GetAll();
    }

    public Table? GetById(int id)
    {
        return _tableRepository.GetById(id);
    }

    public Table? GetTableByStatus(Status status)
    {
        return _tableRepository.GetTableByStatus(status);
    }

    public Table? GetTableWithSmallestWaitingTime()
    {
        var orders = _orderRepository.GetAll();
        var orderWithMinWaitingTime = orders.MinBy(order => order.MaxWait);
        return GetById(orderWithMinWaitingTime!.TableId);
    }

    public void GenerateTables()
    {
        _tableRepository.GenerateTables();
    }

    public void AssignTableWaiter()
    {
        var waiter = _waiterRepository.GetFreeWaiter();
        var table = _tableRepository.GetTableByStatus(Status.ReadyToOrder);
        if (waiter != null && table != null)
        {
            var order = _orderRepository.GetOrderByTableId(table.Id);

            waiter.Order = order!;
            waiter.IsFree = false;
            waiter.ActiveOrders.Add(order!);

            var coolDown = RandomGenerator.NumberGenerator(10);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine($"I will rest for {coolDown} seconds");
            Thread.Sleep(coolDown * 1000);
            waiter.IsFree = true;
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine($"I am ready for a new order");
        }
        else if (waiter == null)
        {
            Console.WriteLine($"There are no free waiters now");
        }
        else if (table == null)
        {
            Console.WriteLine($"There are no desks to be served");
        }
    }
}