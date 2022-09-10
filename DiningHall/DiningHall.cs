using DiningHall.Repositories.FoodRepository;
using DiningHall.Repositories.TableRepository;
using DiningHall.Repositories.WaiterRepository;

namespace DiningHall.DiningHall;

public class DiningHall : IDiningHall
{
    private readonly ITableRepository _tableRepository;
    private readonly IWaiterRepository _waiterRepository;
    private readonly IFoodRepository _foodRepository;
    
    public DiningHall(ITableRepository tableRepository, IWaiterRepository waiterRepository,
        IFoodRepository foodRepository)
    {
        _tableRepository = tableRepository;
        _waiterRepository = waiterRepository;
        _foodRepository = foodRepository;
    }

    public void InitializeDiningHall()
    {
        _tableRepository.GenerateTables();
        _waiterRepository.GenerateWaiters();
        _foodRepository.GenerateFood();
    }

    public void RunRestaurant()
    {
        InitializeDiningHall();
    }
}