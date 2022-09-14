using DiningHall.Models;

namespace DiningHall.Repositories.WaiterRepository;

public class WaiterRepository : IWaiterRepository
{
    private readonly IList<Waiter> _waiters;

    public WaiterRepository()
    {
        _waiters = new List<Waiter>();
    }


    public void InsertWaiter(Waiter waiter)
    {
        _waiters.Add(waiter);
    }

    public IList<Waiter> GetAll()
    {
        return _waiters;
    }

    public Waiter GetById(int id)
    {
        return _waiters.FirstOrDefault(waiter => waiter.Id.Equals(id))!;
    }

    public Waiter? GetFreeWaiter()
    {
        return _waiters.FirstOrDefault(waiter => waiter.IsFree);
    }
}