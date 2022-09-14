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

    public IList<int> GenerateOrderFood()
    {
        var size = RandomGenerator.NumberGenerator(10);
        var listOfFood = new List<int>();

        for (var id = 0; id < size; id++)
        {
            listOfFood.Add(RandomGenerator.NumberGenerator(13));
        }

        return listOfFood;
    }

    public IList<Food> GetAll()
    {
        return _foodRepository.GetAll();
    }

    public Food? GetById(int id)
    {
        return _foodRepository.GetById(id);
    }

    public void GenerateMenu()
    {
        _foodRepository.GenerateFood();
    }
}