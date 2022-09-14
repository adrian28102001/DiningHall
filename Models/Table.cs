namespace DiningHall.Models;

public class Table : BaseEntity
{
    public int OrderId { get; set; }
    public Status Status { get; set; }
}