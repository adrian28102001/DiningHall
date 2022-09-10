using DiningHall.Models;

namespace DiningHall.Repositories.WaiterRepository;

public class WaiterRepository : IWaiterRepository
{
    private readonly IList<Waiter> _waiters = new List<Waiter>();

    public IList<Waiter> GenerateWaiters()
    {
        var nrOfWaiters = Settings.NrOfWaiters;
        for (var id = 0; id < nrOfWaiters; id++)
        {
            _waiters.Add(new Waiter
            {
                Id = id,
                Name = $"Waiter {id}",
                IsFree = true,
            });
        }

        return _waiters;
    }
}