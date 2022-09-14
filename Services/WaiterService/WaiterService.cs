using DiningHall.Helpers;
using DiningHall.Models;
using DiningHall.Repositories.OrderRepository;
using DiningHall.Repositories.TableRepository;
using DiningHall.Repositories.WaiterRepository;
using DiningHall.Services.OrderService;

namespace DiningHall.Services.WaiterService;

public class WaiterService : IWaiterService
{
    private readonly IWaiterRepository _waiterRepository;
    private readonly ITableRepository _tableRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderService _orderService;

    public WaiterService(IWaiterRepository waiterRepository, IOrderRepository orderRepository,
        ITableRepository tableRepository, IOrderService orderService)
    {
        _waiterRepository = waiterRepository;
        _orderRepository = orderRepository;
        _tableRepository = tableRepository;
        _orderService = orderService;
    }

    public void GenerateWaiters()
    {
        var nrOfWaiters = Settings.NrOfWaiters;
        for (var id = 1; id < nrOfWaiters + 1; id++)
        {
            _waiterRepository.InsertWaiter(new Waiter
            {
                Id = id,
                Name = $"Waiter {id}",
                IsFree = true,
            });
        }
    }

    public IList<Waiter> GetAll()
    {
        return _waiterRepository.GetAll();
    }

    public Waiter GetById(int id)
    {
        return _waiterRepository.GetById(id);
    }

    private Waiter? GetFreeWaiter()
    {
        return _waiterRepository.GetFreeWaiter();
    }

    public void AssignTableWaiter()
    {
        var waiter = GetFreeWaiter();
        var table = _tableRepository.GetTableByStatus(Status.ReadyToOrder);
        if (waiter != null && table != null)
        {
            var order = _orderRepository.GetOrderByTableId(table.Id);

            waiter.Order = order!;
            waiter.IsFree = false;
            waiter.ActiveOrders.Add(order!);

            order!.Status = Status.OrderTaken;
            order.WaiterId = waiter.Id;
            
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine($"I am waiter {waiter.Name}the table {table.Id} and took the order with id {table.Id}");
            Console.WriteLine($"I go to drive order with id {table.OrderId} from the table {table.Id} to the kitchen");
            _orderService.SendOrder(order);
            table.Status = Status.OrderTaken;

            var coolDown = RandomGenerator.NumberGenerator(10);
            Console.WriteLine($"I sent the order in the kitchen and I will rest for {coolDown} seconds");
            Thread.Sleep(coolDown * 1000);
            waiter.IsFree = true;
            Console.WriteLine($"I am ready for a new order");
        }
        else if (waiter == null)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine($"There are no free waiters now");
        }
        else if (table == null)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine($"There are no desks to be served");
        }
    }
}