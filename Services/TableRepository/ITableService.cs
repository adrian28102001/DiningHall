using DiningHall.Models;
using DiningHall.Models.Status;

namespace DiningHall.Services.TableRepository;

public interface ITableService
{
    Task<IList<Table>> GetAll();
    Task<Table?> GetById(Task<int> id);
    Task<Table?> GetTableByStatus(TableStatus status);
    Task<Table?> GetTableWithSmallestWaitingTime();
    Task GenerateTables();
    Task ChangeTableStatus(Table table, TableStatus status);
}