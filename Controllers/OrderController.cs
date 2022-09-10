using DiningHall.Models;
using DiningHall.Repositories.FoodRepository;
using Microsoft.AspNetCore.Mvc;

namespace DiningHall.Controllers;

[ApiController]
[Route("api/controller")]
public class OrderController : ControllerBase
{
    private readonly IFoodRepository _foodRepository;

    public OrderController(IFoodRepository foodRepository)
    {
        _foodRepository = foodRepository;
    }

    [HttpGet]
    public Food GetFood()
    {
        return _foodRepository.GetFoodById(1);
    }
    
    
}
