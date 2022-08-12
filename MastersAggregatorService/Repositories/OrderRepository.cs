using MastersAggregatorService.Models;

namespace MastersAggregatorService.Repositories;

public class OrderRepository : BaseRepository<Order>
{
    static List<Order> Orders { get; }
     
    static OrderRepository()
    {
        UserRepository userRepositors = new UserRepository();
        ImageRepository imgRepos = new ImageRepository();

        Orders = new List<Order>
        {
            new Order(userRepositors.GetById(0), new List<Image> { imgRepos.GetById(0) }),
            new Order(userRepositors.GetById(1), new List<Image> { imgRepos.GetById(1), imgRepos.GetById(2) })
        };
    }

    //добавить в список ордеров новый Order 
    public new void Add(Order order) => Orders.Add(order);

    //получить List Order
    public new List<Order> GetAll() => Orders;


    //получить Order по ID 
    public new Order? GetById(int id)
    {
        if (Orders.Count <= id)
            return null;

        return Orders[id];
    }
  
    //Удалить по индексу Order
    public void Delete(int id)
    {
        if (Orders.Count <= id)
            return;

        Orders.Remove(GetById(id));
    }

    //изменить по индексу Order
    public void Update(Order order)
    {
        var index = Orders.FindIndex(p => p.Id == order.Id);
        if (index == -1)
            return;

        Orders[index] = order;
    }
}
