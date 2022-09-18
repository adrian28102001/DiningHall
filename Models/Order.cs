using System.ComponentModel.DataAnnotations;
using DiningHall.Models.Status;

namespace DiningHall.Models;

public class Order : Entity
{
    public Task<int> TableId { get; set; }
    public Task<int> WaiterId { get; set; }
    [Range(1, 3)] public int Priority { get; set; }
    public int MaxWait { get; set; }
    public bool OrderIsComplete { get; set; }
    public IList<int> FoodList { get; set; }
    public OrderStatus OrderStatus { get; set; } 
}