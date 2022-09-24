using DiningHall.Helpers;
using DiningHall.Models;
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
        var taskList = new List<Task>
        {
            Task.Run(() => GenerateOrders(stoppingToken), stoppingToken),
            Task.Run(() => ServeTable(stoppingToken), stoppingToken),
            Task.Run(() => ServeTable(stoppingToken), stoppingToken),
            Task.Run(() => ServeTable(stoppingToken), stoppingToken),
            Task.Run(() => ServeTable(stoppingToken), stoppingToken)
        };

        await Task.WhenAll(taskList);
    }

    private static async Task ServeTable(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _waiterService.ServeTable();
            await _waiterService.SleepWaiter();
        } 
    }
    private static async Task GenerateOrders(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _orderService.GenerateOrder();
            var randomSleepTime = RandomGenerator.NumberGenerator(10, 20);
            ConsoleHelper.Print($"Next order will be generated in: {randomSleepTime}", ConsoleColor.Yellow);
            await SleepGenerator.Delay(randomSleepTime);
        }
    }
}