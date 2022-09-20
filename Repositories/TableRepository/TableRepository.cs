using System.Collections.Concurrent;
using DiningHall.Models;
using DiningHall.Models.Status;

namespace DiningHall.Repositories.TableRepository;

public class TableRepository : ITableRepository
{
    private readonly ConcurrentBag<Table> _tables;

    public TableRepository()
    {
        _tables = new ConcurrentBag<Table>(); // set tables to max 10
    }

    public Task GenerateTables()
    {
        const int maxTables = Settings.NrOfTables;
        for (var id = 1; id < maxTables + 1; id++)
        {
            _tables.Add(
                new Table
                {
                    Id = id,
                    TableStatus = TableStatus.IsAvailable
                });
        }

        return Task.CompletedTask;
    }

    public void InsertTable(Table table)
    {
        _tables.Add(table);
    }

    public Task<ConcurrentBag<Table>> GetAll()
    {
        return Task.FromResult(_tables);
    }

    public Task<Table?> GetById(int id)
    {
        return Task.FromResult(_tables.FirstOrDefault(table => table.Id.Equals(id)));
    }

    public Task<Table?> GetTableByStatus(TableStatus status)
    {
        return Task.FromResult(_tables.FirstOrDefault(table => table.TableStatus == status));
    }
}