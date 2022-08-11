using MastersAggregatorService.Models;
using MastersAggregatorService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MastersAggregatorService.Controllers;

/*В классе OrderController создать методы, для : записи заказа( POST), получении всех заказов для текущего пользователя(GET), 
 * получение заказа по его id(GET) и удаление заказа (DELETE) ; все действия через 
В вики описать эти методы, их маршруты, входящие и исходящие параметры в формате json, а так же коды ответов.
Создать по тесту для каждого метода с нормализованными данными.*/

//[ApiController] //Так как контроллер помечен атрибутом [ApiController], подразумевается, что параметр Pizza будет находиться в тексте запроса.
public class OrderController : BaseController<Order>
{
    OrderRepository orderReposit = new OrderRepository();
    public OrderController(BaseRepository<Order> repository) : base(repository)
    {

    }

    // GET all action
    [HttpGet]
    public ActionResult<List<Order>> GetAll() =>
    orderReposit.GetAll();


    // GET by Id action
    [HttpGet("{id}")]
    public ActionResult<Order> GetById(int id)
    {
        var order = orderReposit.GetById(id);

        if (order == null)
            return NotFound();

        return order;
    }





    /*

        // POST action
        [HttpPost] 
        public IActionResult Create(Order pizza)
        {
            PizzaService.Add(pizza);
            return CreatedAtAction(nameof(Create), new { id = pizza.Id }, pizza);
        }


        // PUT action Изменение или обновление 
        [HttpPut("{id}")]
        public IActionResult Update(int id, Pizza pizza)
        {
            if (id != pizza.Id)
                return BadRequest();

            var existingPizza = PizzaService.Get(id);
            if (existingPizza is null)
                return NotFound();

            PizzaService.Update(pizza);

            return NoContent();
        }


        // DELETE action
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pizza = PizzaService.Get(id);

            if (pizza is null)
                return NotFound();

            PizzaService.Delete(id);

            return NoContent();
        }*/
}
