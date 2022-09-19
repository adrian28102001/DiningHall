using DiningHall.Models;

namespace DiningHall.Services.FoodService;

public interface IFoodService
{
     Task<IList<int>> GenerateOrderFood();
     Task<IList<Food>> GetAll();
     Task<Food?> GetById(int id);

     Task GenerateMenu();
}