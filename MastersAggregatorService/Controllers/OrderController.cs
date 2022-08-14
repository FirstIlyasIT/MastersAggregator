using MastersAggregatorService.Models;
using MastersAggregatorService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MastersAggregatorService.Controllers;

/*В классе OrderController создать методы, для : записи заказа( POST), получении всех заказов для текущего пользователя(GET), 
 * получение заказа по его id(GET) и удаление заказа (DELETE) ; все действия через 
В вики описать эти методы, их маршруты, входящие и исходящие параметры в формате json, а так же коды ответов.
Создать по тесту для каждого метода с нормализованными данными.*/

[Route("[controller]")]
[Produces("application/json")]
public class OrderController : BaseController<Order>
{
    private OrderRepository _orderRepository { get; set; }
    public OrderController(OrderRepository repository) : base(repository)
    {
        _orderRepository = repository;
    }

    /// <summary>
    /// GET all order
    /// </summary> 
    /// <returns>List of all Order</returns>  
    [HttpGet]
    public ActionResult GetAll()
    {
        return Ok(_orderRepository.GetAll());
    }

    /// <summary>
    /// GET by Id order
    /// </summary>
    /// <param Id Order ="id"></param>
    /// <returns>Order by id</returns>  
    [HttpGet("{id}")]
    public ActionResult GetById(int id)
    {
        if (_orderRepository.GetById(id) == null)
            return new JsonResult("order does not exist");

        return Ok(_orderRepository.GetById(id));
    }

    /// <summary>
    /// Saves a new object 
    /// </summary>
    /// <param name="idUser">Id Model User</param>
    /// <param name="idImage">Id Model Image</param>
    /// <returns>New object with database Id</returns>
    [HttpPost]
    public ActionResult Save(int idUser, int[] arrIdImage)
    {
        int? idOrder = _orderRepository.SaveOrder(idUser, arrIdImage);

        if (idOrder == null)
            return NotFound($"id User {idUser} does not exist, can't create order");

        return Ok(idOrder);
    }

    /// <summary>
    /// delete id order
    /// </summary> 
    /// <param Id Order ="idOrder"></param>
    /// <returns>Delete Id Order</returns>  
    [HttpDelete]
    public ActionResult Delete(int idOrder)
    {
        try
        {
            return Ok(_orderRepository.DeleteId(idOrder));
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }
}