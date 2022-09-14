using DiningHall.Models;

namespace DiningHall.Services.WaiterService;

public interface IWaiterService
{
    IList<Waiter> GetAll();
    Waiter GetById(int id);
    void AssignTableWaiter();
    void GenerateWaiters();
}