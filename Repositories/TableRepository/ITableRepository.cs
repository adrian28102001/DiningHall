using DiningHall.Models;
using DiningHall.Models.Status;

namespace DiningHall.Repositories.TableRepository;

public interface ITableRepository
{
    void InsertTable(Table table);
    Task GenerateTables();
    IList<Table> GetAll();
    Table? GetById(Task<int> id);
    Table? GetTableByStatus(TableStatus status);
}