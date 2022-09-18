using DiningHall.Models;
using DiningHall.Models.Status;
using DiningHall.Repositories.OrderRepository;
using DiningHall.Repositories.TableRepository;

namespace DiningHall.Services.TableRepository;

public class TableService : ITableService
{
    private readonly ITableRepository _tableRepository;
    private readonly IOrderRepository _orderRepository;

    public TableService(ITableRepository tableRepository, IOrderRepository orderRepository)
    {
        _tableRepository = tableRepository;
        _orderRepository = orderRepository;
    }

    public IList<Table> GetAll()
    {
        return _tableRepository.GetAll();
    }

    public Table? GetById(Task<int> id)
    {
        return _tableRepository.GetById(id);
    }

    public Table? GetTableByStatus(TableStatus status)
    {
        return _tableRepository.GetTableByStatus(status);
    }

    public Table? GetTableWithSmallestWaitingTime()
    {
        var orders = _orderRepository.GetAll();
        var orderWithMinWaitingTime = orders.MinBy(order => order.MaxWait);
        return GetById(orderWithMinWaitingTime!.TableId);
    }

    public Task GenerateTables()
    {
        return _tableRepository.GenerateTables();
    }

    public void ChangeTableStatus(Table table, Task<int> orderId, TableStatus status)
    {
        table.OrderId = orderId;
        table.TableStatus = status;
    }

    public Task ChangeTableStatus(Table table, TableStatus status)
    {
        table.TableStatus = status;
        return Task.CompletedTask;
    }
}