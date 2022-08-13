using MastersAggregatorService.Models;

namespace MastersAggregatorService.Repositories;

public class OrderRepository : BaseRepository<Order>
{
    static List<Order> Orders { get; set; }

    static OrderRepository()
    {
        UserRepository userRepositors = new UserRepository();
        ImageRepository imgRepos = new ImageRepository();

        Orders = new List<Order>
        {
            new Order( 0, userRepositors.GetById(0), new List<Image> { imgRepos.GetById(0) }),
            new Order( 1, userRepositors.GetById(1), new List<Image> { imgRepos.GetById(1), imgRepos.GetById(2) })
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

    //изменить по индексу Order или добавить новый если такого ордера нет - проверяем по Id
    public new void Save(Order order)
    {
        //получаем индекс order  
        var index = Orders.FindIndex(p => p.Id == order.Id);

        //если индекс не существует - значит метод добавления добвляем новый Order
        if (index == -1)
        {
            Orders.Add(order);
            return;
        }
        else //изменение Order  
            Orders[index] = order;
    }

    //получить Count Orders
    public int GetCount() => Orders.Count;
}
