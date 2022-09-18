using DiningHall.Models;

namespace DiningHall.Repositories.WaiterRepository;

public interface IWaiterRepository
{
    void InsertWaiter(Waiter waiter);
    IList<Waiter> GetAll();
    Waiter GetById(Task<int> id);
    Waiter? GetFreeWaiter();
    Task GenerateWaiters();
}