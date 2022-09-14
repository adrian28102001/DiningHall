using DiningHall.Services.FoodService;
using DiningHall.Services.OrderService;
using DiningHall.Services.TableRepository;
using DiningHall.Services.WaiterService;

namespace DiningHall.DiningHall;

public class DiningHall : IDiningHall
{
    private readonly IOrderService _orderService;
    private readonly ITableService _tableService;
    private readonly IFoodService _foodService;
    private readonly IWaiterService _waiterService;

    public DiningHall(IOrderService orderService, ITableService tableService, IFoodService foodService,
        IWaiterService waiterService)
    {
        _orderService = orderService;
        _tableService = tableService;
        _foodService = foodService;
        _waiterService = waiterService;
    }

    public void InitializeDiningHall()
    {
        _foodService.GenerateMenu();
        _tableService.GenerateTables();
        _waiterService.GenerateWaiters();
    }

    public void MaintainRestaurant(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _orderService.GenerateOrder();
            _waiterService.AssignTableWaiter();
        }
    }
}