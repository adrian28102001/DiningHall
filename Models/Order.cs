namespace DiningHall.Models;

public class Order : Entity
{
    public IList<Food> FoodList { get; set; }
    public int Priority { get; set; }
    public int MaxWait { get; set; }
    public bool OrderIsComplete { get; set; }    
    public Waiter WaiterId { get; set; }
}