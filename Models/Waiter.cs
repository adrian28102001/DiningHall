namespace DiningHall.Models;

public class Waiter : BaseEntity
{
    public string Name { get; set; }
    public bool IsFree { get; set; }
    public Order Order { get; set; }
    public List<Order> ActiveOrders { get; set; }
    public List<int> NrOfOrdersCompleted { get; set; }
}