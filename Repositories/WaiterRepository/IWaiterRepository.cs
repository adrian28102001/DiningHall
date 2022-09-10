using DiningHall.Models;

namespace DiningHall.Repositories.WaiterRepository;

public interface IWaiterRepository
{
    public IList<Waiter> GenerateWaiters();
}