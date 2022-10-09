using System.Collections.Concurrent;
using DiningHall.Helpers;
using DiningHall.Models;
using DiningHall.Models.Status;
using DiningHall.Repositories.TableRepository;
using Microsoft.AspNetCore.Mvc;

namespace DiningHall.Controller;

[ApiController]
[Route("/dininghall")]
public class ApiController : ControllerBase
{
    private readonly ITableRepository _tableRepository;

    public ApiController(ITableRepository tableRepository)
    {
        _tableRepository = tableRepository;
    }

    [HttpPost]
    public async Task GetOrderFromKitchen([FromBody] Order order)
    {
        order.OrderStatus = OrderStatus.OrderCooked;
        var table = await _tableRepository.GetById(order.TableId);
        if (table != null)
        {
            table.TableStatus = TableStatus.IsAvailable;
            ConsoleHelper.Print($"I received from the kitchen an order with id {order.Id} for table {order.TableId}");
        }

        RatingHelper.GetRating(order);
    }
}