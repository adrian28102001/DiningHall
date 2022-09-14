using DiningHall.Models;

namespace DiningHall.Repositories.TableRepository;

public class TableRepository : ITableRepository
{
    private readonly IList<Table> _tables;
    public TableRepository()
    {
        _tables = new List<Table>(10);
    }
    public void GenerateTables()
    {
        var maxTables = Settings.NrOfTables;
        for (var id = 1; id < maxTables + 1; id++)
        {
            _tables.Add(
                new Table
                {
                    Id = id,
                    Status = Status.IsAvailable
                });
        }
    }
    public void InsertTable(Table table)
    {
        _tables.Add(table);
    }

    public IList<Table> GetAll()
    {
        return _tables;
    }

    public Table? GetById(int id)
    {
        return _tables.FirstOrDefault(table => table.Id.Equals(id));
    }
    
    public Table? GetTableByStatus(Status status)
    {
        return _tables.FirstOrDefault(table => table.Status == status);
    }
   
}