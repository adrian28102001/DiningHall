using DiningHall.Models;
using DiningHall.Repositories.FoodRepository;
using DiningHall.Repositories.TableRepository;
using DiningHall.Repositories.WaiterRepository;

namespace DiningHall.DiningHall;

public class DiningHall : IDiningHall
{
    private readonly ITableRepository _tableRepository;
    private readonly IWaiterRepository _waiterRepository;
    private readonly IFoodRepository _foodRepository;

    private IList<Table> _tables = new List<Table>();
    private IList<Waiter> _waiters = new List<Waiter>();
    private IList<Food> _menu = new List<Food>();


    public DiningHall(IWaiterRepository waiterRepository, IFoodRepository foodRepository,
        ITableRepository tableRepository)
    {
        _waiterRepository = waiterRepository;
        _foodRepository = foodRepository;
        _tableRepository = tableRepository;
    }

    private void InitializeDiningHall()
    {
        _waiters = _waiterRepository.GenerateWaiters();
        _tables = _tableRepository.GenerateTables();
        _menu = _foodRepository.GenerateFood();
    }

    public void RunRestaurant()
    {
        InitializeDiningHall();
    }
}