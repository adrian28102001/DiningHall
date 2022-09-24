using System.Collections.Concurrent;
using DiningHall.Models;
using DiningHall.Repositories.GenericRepository;

namespace DiningHall.Repositories.FoodRepository;

public class FoodRepository : IFoodRepository
{
    private readonly ConcurrentBag<Food> _foods;
    private readonly IGenericRepository<Food> _genericRepository;
    
    public FoodRepository()
    {
        _foods = new ConcurrentBag<Food>();
        _genericRepository = new GenericRepository<Food>(_foods);
    }

    public Task GenerateMenu()
    {
        _foods.Add(new Food
        {
            Id = 1,
            Name = "Pizza",
            PreparationTime = 20
        });
        _foods.Add(new Food
        {
            Id = 2,
            Name = "Salad",
            PreparationTime = 10
        });
        _foods.Add(new Food
        {
            Id = 3,
            Name = " Zeama",
            PreparationTime = 7
        });
        _foods.Add(new Food
        {
            Id = 4,
            Name = "Scallop Sashimi with Meyer Lemon Confit",
            PreparationTime = 32
        });
        _foods.Add(new Food
        {
            Id = 5,
            Name = "Island Duck with Mulberry Mustard",
            PreparationTime = 35
        });
        _foods.Add(new Food
        {
            Id = 6,
            Name = "Waffles",
            PreparationTime = 10
        });
        _foods.Add(new Food
        {
            Id = 7,
            Name = "Aubergine",
            PreparationTime = 20
        });

        _foods.Add(new Food
        {
            Id = 8,
            Name = "Lasagna",
            PreparationTime = 30
        });
        _foods.Add(new Food
        {
            Id = 9,
            Name = "Burger",
            PreparationTime = 15
        });
        _foods.Add(new Food
        {
            Id = 10,
            Name = "Gyros",
            PreparationTime = 15
        });
        _foods.Add(new Food
        {
            Id = 11,
            Name = "Kebab",
            PreparationTime = 15
        });
        _foods.Add(new Food
        {
            Id = 12,
            Name = "UnagiMaki",
            PreparationTime = 20
        });
        _foods.Add(new Food
        {
            Id = 13,
            Name = "TobaccoChicken",
            PreparationTime = 30
        });
        return Task.CompletedTask;
    }

    public Task<ConcurrentBag<Food>> GetAll()
    {
        return _genericRepository.GetAll();
    }
    
    public Task<Food?> GetById(int id)
    {
        return _genericRepository.GetById(id);
    }
}