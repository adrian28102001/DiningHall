using System.Collections.Concurrent;
using DiningHall.Models;

namespace DiningHall.Services.FoodService;

public interface IFoodService
{
     Task<ConcurrentBag<int>> GenerateOrderFood();
     Task<ConcurrentBag<Food>> GetAll();
     Task<Food?> GetById(int id);

     Task GenerateMenu();
}