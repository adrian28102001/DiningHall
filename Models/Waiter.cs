namespace DiningHall.Models;

public class Waiter : BaseEntity
{
    public Waiter()
    {
        Name = "";
        Order = new Order();
        ActiveOrders = new List<Order>();
        NrOfOrdersCompleted = new List<int>();
    }

    public string Name { get; set; }
    public bool IsFree { get; set; }
    public Order Order { get; set; }
    public IList<Order> ActiveOrders { get; set; }
    public IList<int> NrOfOrdersCompleted { get; set; }
}