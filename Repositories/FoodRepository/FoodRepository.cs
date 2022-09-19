using DiningHall.Models;

namespace DiningHall.Repositories.FoodRepository;

public class FoodRepository : IFoodRepository
{
    private readonly IList<Food> _foods;

    public FoodRepository()
    {
        _foods = new List<Food>();
    }

    public Task GenerateMenu()
    {
        _foods.Add(new Food
        {
            Id = Task.FromResult(1),
            Name = "Pizza",
            PreparationTime = 20
        });
        _foods.Add(new Food
        {
            Id = Task.FromResult(2),
            Name = "Salad",
            PreparationTime = 10
        });
        _foods.Add(new Food
        {
            Id = Task.FromResult(3),
            Name = " Zeama",
            PreparationTime = 7
        });
        _foods.Add(new Food
        {
            Id = Task.FromResult(4),
            Name = "Scallop Sashimi with Meyer Lemon Confit",
            PreparationTime = 32
        });
        _foods.Add(new Food
        {
            Id = Task.FromResult(5),
            Name = "Island Duck with Mulberry Mustard",
            PreparationTime = 35
        });
        _foods.Add(new Food
        {
            Id = Task.FromResult(6),
            Name = "Waffles",
            PreparationTime = 10
        });
        _foods.Add(new Food
        {
            Id = Task.FromResult(7),
            Name = "Aubergine",
            PreparationTime = 20
        });

        _foods.Add(new Food
        {
            Id = Task.FromResult(8),
            Name = "Lasagna",
            PreparationTime = 30
        });
        _foods.Add(new Food
        {
            Id = Task.FromResult(9),
            Name = "Burger",
            PreparationTime = 15
        });
        _foods.Add(new Food
        {
            Id = Task.FromResult(10),
            Name = "Gyros",
            PreparationTime = 15
        });
        _foods.Add(new Food
        {
            Id = Task.FromResult(11),
            Name = "Kebab",
            PreparationTime = 15
        });
        _foods.Add(new Food
        {
            Id = Task.FromResult(12),
            Name = "UnagiMaki",
            PreparationTime = 20
        });
        _foods.Add(new Food
        {
            Id = Task.FromResult(13),
            Name = "TobaccoChicken",
            PreparationTime = 30
        });
        return Task.CompletedTask;
    }


    public Task<IList<Food>> GetAll()
    {
        return Task.FromResult(_foods);
    }

    public Task<Food?> GetById(int id)
    {
        return Task.FromResult(_foods.FirstOrDefault(food => food.Id.Result.Equals(id)));
    }
}