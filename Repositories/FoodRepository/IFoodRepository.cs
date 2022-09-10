using DiningHall.Models;

namespace DiningHall.Repositories.FoodRepository;

public interface IFoodRepository
{
    public IList<Food> GenerateFood();
    public Food GetFoodById(int id);
    public IList<int> GenerateOrderFood();
}