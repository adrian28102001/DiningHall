using DiningHall.Models;

namespace DiningHall.Repositories.TableRepository;

public class TableRepository : ITableRepository
{
    private readonly IList<Table> _tables = new List<Table>();

    public IList<Table> GenerateTables()
    {
        var maxTables = Settings.NrOfTables;
        for (var id = 0; id < maxTables; id++)
        {
         _tables.Add(new Table
         {
             Id = id,
             TableStatus = TableStatus.IsAvailable
         });   
        }
        return _tables;
    }
}