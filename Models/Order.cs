using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using DiningHall.Models.Status;

namespace DiningHall.Models;

public class Order : Entity
{
    public int TableId { get; set; }
    public int WaiterId { get; set; }
    [Range(1, 3)] public int Priority { get; set; }
    public int MaxWait { get; set; }
    public bool OrderIsComplete { get; set; }
    public ConcurrentBag<int> FoodList { get; set; }
    public OrderStatus OrderStatus { get; set; } 
}