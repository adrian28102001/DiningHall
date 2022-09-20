using System.Collections.Concurrent;
using DiningHall.Models;

namespace DiningHall.Repositories.FoodRepository;

public interface IFoodRepository
{
    Task GenerateMenu();
    public Task<ConcurrentBag<Food>> GetAll();
    public Task<Food?> GetById(int id);
}