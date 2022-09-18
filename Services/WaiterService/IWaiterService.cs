using DiningHall.Models;

namespace DiningHall.Services.WaiterService;

public interface IWaiterService
{
    IList<Waiter> GetAll();
    Waiter GetById(Task<int> id);
    Task ServeTable();
    Task GenerateWaiters();
    void ChangeWaiterStatus(Waiter waiter, bool isFree);
    void ChangeWaiterDetails(Waiter waiter, Order order, bool isFree);
}