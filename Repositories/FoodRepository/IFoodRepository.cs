using DiningHall.Models;

namespace DiningHall.Repositories.FoodRepository;

public interface IFoodRepository
{
    void GenerateFood();
    public IList<Food> GetAll();
    public Food? GetById(int id);
}