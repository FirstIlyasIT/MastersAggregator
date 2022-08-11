using MastersAggregatorService.Models;
using MastersAggregatorService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MastersAggregatorService.Controllers;

/*� ������ OrderController ������� ������, ��� : ������ ������( POST), ��������� ���� ������� ��� �������� ������������(GET), 
 * ��������� ������ �� ��� id(GET) � �������� ������ (DELETE) ; ��� �������� ����� 
� ���� ������� ��� ������, �� ��������, �������� � ��������� ��������� � ������� json, � ��� �� ���� �������.
������� �� ����� ��� ������� ������ � ���������������� �������.*/

//[ApiController] //��� ��� ���������� ������� ��������� [ApiController], ���������������, ��� �������� Pizza ����� ���������� � ������ �������.
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


        // PUT action ��������� ��� ���������� 
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
