using MastersAggregatorService.Models;
using MastersAggregatorService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MastersAggregatorService.Controllers;

/*В классе OrderController создать методы, для : записи заказа( POST), получении всех заказов для текущего пользователя(GET), 
 * получение заказа по его id(GET) и удаление заказа (DELETE) ; все действия через 
В вики описать эти методы, их маршруты, входящие и исходящие параметры в формате json, а так же коды ответов.
Создать по тесту для каждого метода с нормализованными данными.*/

[Route("[controller]")]
public class OrderController : BaseController<Order>
{
    private OrderRepository _orderRepository { get; set; }

    //public OrderController(BaseRepository<User> repository) : base(repository) 
    public OrderController(OrderRepository repository) : base(repository)
    {
        _orderRepository = repository;
    }

    /// <summary>
    /// GET all order
    /// </summary> 
    /// <returns></returns>  
    [HttpGet]
    public JsonResult GetAll()
    {
        return new JsonResult(_orderRepository.GetAll());
    }

    /// <summary>
    /// GET by Id order
    /// </summary>
    /// <param Id Order ="id"></param>
    /// <returns></returns>  
    [HttpGet("{id}")]
    public JsonResult GetById(int id)
    {
        if (_orderRepository.GetById(id) == null)
            return new JsonResult("order does not exist");

        return new JsonResult(_orderRepository.GetById(id));
    }

    /// <summary>
    /// Save new order
    /// </summary>
    /// <param Id User ="idUser"></param>
    /// <param Id Image ="idImage"></param>
    /// <returns></returns>  
    [HttpPost]
    public JsonResult Save(int idUser, int idImage)
    {
        UserRepository userRepositors = new UserRepository();
        ImageRepository imgRepos = new ImageRepository();
        Order newOrder = new Order(userRepositors.GetById(idUser), new List<Image> { imgRepos.GetById(idImage) });

        _orderRepository.Add(newOrder);
        return GetAll();
    }

    /// <summary>
    /// delete id order
    /// </summary> 
    /// <param Id Order ="idOrder"></param>
    /// <returns></returns>  
    [HttpDelete]
    public void Delete(int idOrder)
    {
        _orderRepository.Delete(idOrder);
    }
}
 

     
