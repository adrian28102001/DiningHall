namespace DiningHall.Models;

public class Table : BaseEntity
{
    public int OrderId { get; set; }
    public TableStatus TableStatus { get; set; }
}