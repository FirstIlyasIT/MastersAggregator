using MastersAggregatorService.Models;

namespace MastersAggregatorService.Repositories;

public class OrderRepository : BaseRepository<Order>
{
    public static List<Order> Orders { get; }


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

    //добавление данных в таблицу Order
    //public static void Add(Order order) => Orders.Add(order);
    public new void Save(Order order) => Orders.Add(order);

    //получить всю таблицу Order
    public new List<Order> GetAll() => Orders;


    //получить Order по ID
    //public static Order? Get(int id) => Orders.FirstOrDefault(p => p.Id == id);
    public new Order? GetById(int id)
    {
        if (Orders[id] is null)
            return null;

        return Orders[id];
    }
    //”далить по индексу Order
    public void Delete(int id)
    {
        var order = GetById(id);
        if (order is null)
            return;

        Orders.Remove(order);
    }

    //»зменить по индексу Order
    public static void Update(Order order)
    {
        var index = Orders.FindIndex(p => p.Id == order.Id);
        if (index == -1)
            return;

        Orders[index] = order;
    }
}
