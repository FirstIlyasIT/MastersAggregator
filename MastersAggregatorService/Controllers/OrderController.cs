using MastersAggregatorService.Models;
using MastersAggregatorService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MastersAggregatorService.Controllers;

/*� ������ OrderController ������� ������, ��� : ������ ������( POST), ��������� ���� ������� ��� �������� ������������(GET), 
 * ��������� ������ �� ��� id(GET) � �������� ������ (DELETE) ; ��� �������� ����� 
� ���� ������� ��� ������, �� ��������, �������� � ��������� ��������� � ������� json, � ��� �� ���� �������.
������� �� ����� ��� ������� ������ � ���������������� �������.*/

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
    /// <response code="200"> Returns List of all Order.</response>
    [HttpGet]
    public ActionResult GetAll()
    {
        return Ok(_orderRepository.GetAll());
    }
 
    /// <summary>
    /// GET by Id order
    /// </summary>
    /// <param name="id"> Id Order</param>
    /// <returns>Order by id</returns>  
    /// <response code="200"> Returns Order.</response>
    /// <response code="400"> Order id does not exist.</response>
    [HttpGet("{id}")]
    public ActionResult GetById(int id)
    {
        if (_orderRepository.GetById(id) == null)
            return NotFound($"id Order {id} does not exist.");

        return Ok(_orderRepository.GetById(id));
    }

    /// <summary>
    /// Add new Order. 
    /// </summary>
    /// <param name="idUser">Id Model User.</param>
    /// <param name="arrIdImage">Array Id Model Image.</param>
    /// <returns>New object with database Id.</returns>
    /// <response code="200"> Returns int id create Order.</response>
    /// <response code="400"> idUser id does not exist and can't create order.</response>
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
    /// <param name = "idOrder"> Id Order </param>
    /// <returns>Delete Id Order</returns>  
    /// <response code="200"> successfully deleted Order.</response>
    /// <response code="400"> failed to delete order, such id order does not exist.</response>
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