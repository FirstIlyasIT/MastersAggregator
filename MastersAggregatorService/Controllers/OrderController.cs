using MastersAggregatorService.Models;
using MastersAggregatorService.Repositories;

namespace MastersAggregatorService.Controllers;

public class OrderController : BaseController<Order>
{
    public OrderController(OrderRepository repository)
    {
        
    }
}