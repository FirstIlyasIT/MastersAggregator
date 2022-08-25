using MastersAggregatorService.Models;
using MastersAggregatorService.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
 

namespace MastersAggregatorService.Controllers;
   
[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
[Consumes("application/json")]
public class OrderController : BaseController<Order>
{
    private OrderRepository _repository { get; set; }
    private readonly UserRepository _userRepository;
    public OrderController(OrderRepository repository, UserRepository userRepository) 
    {
        _repository = repository;
        _userRepository = userRepository;
    }

 
     
    /// <summary>
    /// GET all order
    /// </summary> 
    /// <returns>List of all Order</returns>  
    /// <response code="200"> Returns List of all Order.</response>
    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _repository.GetAllAsync();
        if (orders.Any())
            return Ok(orders);
        else
            return NotFound();
    }


    /// <summary>
    /// GET by Id order
    /// </summary>
    /// <param name="id"> Id Order</param>
    /// <returns>Order by id</returns>  
    /// <response code="200"> Returns Order.</response>
    /// <response code="400"> Order id does not exist.</response>
    [HttpGet("id")]
    public IActionResult GetOrder(int id)
    {
        var order = _repository.GetById(id);
        if (order is null)
            return NotFound();
        else
            return Ok(new JsonResult(order));
    }


    /// <summary>
    /// delete id order
    /// </summary> 
    /// <param name = "id"> Id Order </param>
    /// <returns>Delete Id Order</returns>  
    /// <response code="200"> successfully deleted Order.</response>
    /// <response code="400"> failed to delete order, such id order does not exist.</response>
    [HttpDelete("id")]
    public IActionResult DeleteOrder(int id)
    {
        var order = _repository.GetById(id);
        if (order is null)
            return BadRequest();
        else
        {
            _repository.Delete(order);
            return NoContent();
        }
    }

    /// <summary>
    /// Add new Order. 
    /// </summary>
    /// <param name="order">order Model Order.</param> 
    /// <returns></returns>
    /// <response code="200"> create Order.</response>
    /// <response code="400"> I can't create an Order, such an Order already exists.</response> 
    [HttpPost]
    public IActionResult CreateOrder([FromBody] Order order)
    { 
        var orders = _repository.GetAll(); 
        //если order с таким id  существует то не создаем его возращаем BadRequest()
        if (orders.Any(s => s.Id == order.Id))
            return BadRequest();

        //проверяем существует юзер с таким id если нет юзера то и order создать для него не можем 
        if (_userRepository.GetById(order.Sender.Id)== null)
            return BadRequest();


        _repository.Save(order);
        return NoContent(); 
    }
}