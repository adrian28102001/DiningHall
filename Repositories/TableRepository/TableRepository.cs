using DiningHall.Models;
using DiningHall.Models.Status;

namespace DiningHall.Repositories.TableRepository;

public class TableRepository : ITableRepository
{
    private readonly IList<Table> _tables;

    public TableRepository()
    {
        _tables = new List<Table>(10);
    }

    public Task GenerateTables()
    {
        const int maxTables = Settings.NrOfTables;
        for (var id = 1; id < maxTables + 1; id++)
        {
            _tables.Add(
                new Table
                {
                    Id = Task.FromResult(id),
                    TableStatus = TableStatus.IsAvailable
                });
        }

        return Task.CompletedTask;
    }

    public void InsertTable(Table table)
    {
        _tables.Add(table);
    }

    public IList<Table> GetAll()
    {
        return _tables;
    }

    public Table? GetById(Task<int> id)
    {
        return _tables.FirstOrDefault(table => table.Id.Equals(id));
    }

    public Table? GetTableByStatus(TableStatus status)
    {
        return _tables.FirstOrDefault(table => table.TableStatus == status);
    }
}