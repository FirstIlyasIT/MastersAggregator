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
        return Ok(new JsonResult(_orderRepository.GetAll()));
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

        return Ok(new JsonResult(_orderRepository.GetById(id)));
    }

    /// <summary>
    /// Save new order or idit order
    /// </summary>
    /// <param Id User ="idUser"></param>
    /// <param Id Image ="idImage"></param>
    /// <returns>Save new order or idit order</returns>  
    [HttpPost]
    public ActionResult Save(int idUser, int idImage)
    {
        UserRepository userRepositors = new UserRepository();
        ImageRepository imgRepos = new ImageRepository();
        Order newOrder = new Order(_orderRepository.GetCount(), userRepositors.GetById(idUser), new List<Image> { imgRepos.GetById(idImage) });

        _orderRepository.Save(newOrder);
        return Ok();
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
            _orderRepository.Delete(idOrder);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }

        return NoContent();
    }
}