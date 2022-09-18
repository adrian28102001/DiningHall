using DiningHall.Models;

namespace DiningHall.Repositories.WaiterRepository;

public class WaiterRepository : IWaiterRepository
{
    private readonly IList<Waiter> _waiters;

    public WaiterRepository()
    {
        _waiters = new List<Waiter>();
    }

    public Task GenerateWaiters()
    {
        const int nrOfWaiters = Settings.NrOfWaiters;
        for (var id = 1; id < nrOfWaiters + 1; id++)
        {
            _waiters.Add(new Waiter
            {
                Id = Task.FromResult(id),
                Name = $"Waiter {id}",
                IsFree = true,
            });
        }

        return Task.CompletedTask;
    }

    public void InsertWaiter(Waiter waiter)
    {
        _waiters.Add(waiter);
    }

    public IList<Waiter> GetAll()
    {
        return _waiters;
    }

    public Waiter GetById(Task<int> id)
    {
        return _waiters.FirstOrDefault(waiter => waiter.Id.Equals(id))!;
    }

    public Waiter? GetFreeWaiter()
    {
        return _waiters.FirstOrDefault(waiter => waiter.IsFree);
    }
}