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


    public Task MaintainRestaurant(CancellationToken stoppingToken)
    {
        for (var id = 0; id < Settings.NrOfWaiters; id++)
        {
            CreateThread(stoppingToken).Start();
        }
        
        // var generateOrderThread1 = CreateThread(stoppingToken);
        // var generateOrderThread2 = CreateThread(stoppingToken);
        // var generateOrderThread3 = CreateThread(stoppingToken);
        // var generateOrderThread4 = CreateThread(stoppingToken);
        // generateOrderThread1.Start();
        // generateOrderThread2.Start();
        // generateOrderThread3.Start();
        // generateOrderThread4.Start();

        return Task.CompletedTask;
    }

    private static async Task CreateThread(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _orderService.GenerateOrder();
            await _waiterService.ServeTable();
        }
    }
}