using DiningHall.Models;

namespace DiningHall.Services.WaiterService;

public interface IWaiterService
{
    Task<IList<Waiter>> GetAll();
    Task<Waiter?> GetById(Task<int> id);
    Task ServeTable();
    Task GenerateWaiters();
}