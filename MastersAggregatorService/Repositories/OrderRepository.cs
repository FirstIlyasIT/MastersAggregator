using MastersAggregatorService.Data;
using MastersAggregatorService.Models;
 
namespace MastersAggregatorService.Repositories;

public class OrderRepository : BaseRepository<Order>
{ 
    static List<Order> Orders { get; set; }
    //Все тестовые данные в классе TestData для получения тестовых данных прописал их в конструкторе и получай через DI
    public OrderRepository()
    {
        Orders = TestData.Orders;
    }

    //get all Order 
    public override IEnumerable<Order> GetAll()
    {
        return Orders;
    }
     
    //Get Order in ID 
    public override Order? GetById(int id) 
    {
        var index = Orders.FindIndex(p => p.Id == id);
        if (index == -1)
            return null;

        return Orders[index];
    }
 
    //create a new order
    public override Order Save(Order model)
    {
        Orders.Add(model);
        return model;
    }
 
    //Delete by index Order 
    public override void Delete(Order model)
    {
        var index = Orders.FindIndex(p => p.Id == model.Id);
        if (index == -1)
            return;

        Orders.RemoveAt(index);
    }

    //Get Count Orders
    public int GetCount() => Orders.Count;

}
