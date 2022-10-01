using DiningHall.Services.FoodService;
using DiningHall.Services.OrderService;
using DiningHall.Services.TableRepository;
using DiningHall.Services.WaiterService;

namespace DiningHall.DiningHall;

public class DiningHall : IDiningHall
{
    private static IOrderService _orderService;
    private readonly ITableService _tableService;
    private readonly IFoodService _foodService;
    private static IWaiterService _waiterService;

    public DiningHall(IOrderService orderService, ITableService tableService, IFoodService foodService,
        IWaiterService waiterService)
    {
        _orderService = orderService;
        _tableService = tableService;
        _foodService = foodService;
        _waiterService = waiterService;
    }

    public async Task InitializeDiningHallParallelAsync()
    {
        // var watch = System.Diagnostics.Stopwatch.StartNew();
        var taskList = new List<Task>
        {
            Task.Run(() => _foodService.GenerateMenu()),
            Task.Run(() => _waiterService.GenerateWaiters()),
            Task.Run(() => _tableService.GenerateTables())
        };

        await Task.WhenAll(taskList);
    }


    public async Task MaintainRestaurant(CancellationToken stoppingToken)
    {
        var orderThread = Task.Run(() => GenerateOrders(stoppingToken), stoppingToken);
        var waiter1 = Task.Run(() => ServeTable(stoppingToken), stoppingToken);
        var waiter2 = Task.Run(() => ServeTable(stoppingToken), stoppingToken);
        var waiter3 = Task.Run(() => ServeTable(stoppingToken), stoppingToken);
        var waiter4 = Task.Run(() => ServeTable(stoppingToken), stoppingToken);

        var taskList = new List<Task>
        {
            orderThread, waiter1, waiter2, waiter3, waiter4
        };
        
        await Task.WhenAll(taskList);
    }

    private static async Task GenerateOrders(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _orderService.GenerateOrder();
        }
    }

    private static async Task ServeTable(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _waiterService.ServeTable();
        }
    }
}