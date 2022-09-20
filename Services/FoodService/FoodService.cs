using System.Collections.Concurrent;
using DiningHall.Helpers;
using DiningHall.Models;
using DiningHall.Repositories.FoodRepository;

namespace DiningHall.Services.FoodService;

public class FoodService : IFoodService
{
    private readonly IFoodRepository _foodRepository;

    public FoodService(IFoodRepository foodRepository)
    {
        _foodRepository = foodRepository;
    }

    public Task<ConcurrentBag<int>> GenerateOrderFood()
    {
        var size = RandomGenerator.NumberGenerator(10);
        var listOfFood = new ConcurrentBag<int>();

        for (var id = 0; id < size; id++)
        {
            listOfFood.Add(RandomGenerator.NumberGenerator(13));
        }

        return Task.FromResult(listOfFood);
    }

    public Task<ConcurrentBag<Food>> GetAll()
    {
        return _foodRepository.GetAll();
    }

    public Task<Food?> GetById(int id)
    {
        return _foodRepository.GetById(id);
    }

    public Task GenerateMenu()
    {
        return _foodRepository.GenerateMenu();
    }
}