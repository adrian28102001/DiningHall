using DiningHall.Models;

namespace DiningHall.Services.TableRepository;

public interface ITableService
{
    IList<Table> GetAll();
    Table? GetById(int id);
    Table? GetTableByStatus(Status status);
    Table? GetTableWithSmallestWaitingTime();
    void AssignTableWaiter();
    void GenerateTables();
}