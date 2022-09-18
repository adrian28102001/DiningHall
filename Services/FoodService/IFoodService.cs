using DiningHall.Models;

namespace DiningHall.Services.FoodService;

public interface IFoodService
{
     IList<int> GenerateOrderFood();
     IList<Food> GetAll();
     Food? GetById(int id);

     Task GenerateMenu();
}