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
        var taskList = new List<Task>()
        {
            Task.Run(() => _foodService.GenerateMenu()),
            Task.Run(() => _waiterService.GenerateWaiters()),
            Task.Run(() => _tableService.GenerateTables())
        };

        await Task.WhenAll(taskList);
    }

    public Task MaintainRestaurant(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _orderService.GenerateOrder();
            _waiterService.ServeTable();
        }

        return Task.CompletedTask;
    }
}