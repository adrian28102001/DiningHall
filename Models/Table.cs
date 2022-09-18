using DiningHall.Models.Status;

namespace DiningHall.Models;

public class Table : BaseEntity
{
    public Task<int> OrderId { get; set; }
    public TableStatus TableStatus { get; set; }
}