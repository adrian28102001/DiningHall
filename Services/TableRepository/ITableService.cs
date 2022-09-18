using DiningHall.Models;
using DiningHall.Models.Status;

namespace DiningHall.Services.TableRepository;

public interface ITableService
{
    IList<Table> GetAll();
    Table? GetById(Task<int> id);
    Table? GetTableByStatus(TableStatus status);
    Table? GetTableWithSmallestWaitingTime();
    Task GenerateTables();
    void ChangeTableStatus(Table table, Task<int> orderId, TableStatus status);
    Task ChangeTableStatus(Table table, TableStatus status);
}