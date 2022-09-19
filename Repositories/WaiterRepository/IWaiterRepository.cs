using DiningHall.Models;

namespace DiningHall.Repositories.WaiterRepository;

public interface IWaiterRepository
{
    void InsertWaiter(Waiter waiter);
    Task<IList<Waiter>> GetAll();
    Task<Waiter?> GetById(Task<int> id);
    Task<Waiter?> GetFreeWaiter();
    Task GenerateWaiters();
    }