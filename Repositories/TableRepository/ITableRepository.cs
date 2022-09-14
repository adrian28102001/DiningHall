using DiningHall.Models;

namespace DiningHall.Repositories.TableRepository;

public interface ITableRepository
{
    void InsertTable(Table table);
    void GenerateTables();
    IList<Table> GetAll();
    Table? GetById(int id);
    Table? GetTableByStatus(Status status);
}