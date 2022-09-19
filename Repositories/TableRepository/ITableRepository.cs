using DiningHall.Models;
using DiningHall.Models.Status;

namespace DiningHall.Repositories.TableRepository;

public interface ITableRepository
{
    void InsertTable(Table table);
    Task GenerateTables();
    Task<IList<Table>> GetAll();
    Task<Table?> GetById(Task<int> id);
    Task<Table?> GetTableByStatus(TableStatus status);
}