using DiningHall.Models;

namespace DiningHall.Repositories.TableRepository;

public interface ITableRepository
{
    public IList<Table> GenerateTables();
}