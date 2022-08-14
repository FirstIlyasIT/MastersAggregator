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

    //создаем новый ордер, т.к ордер состоит из обьекта User и Image то в метод передаем id этих обьектов
    public int? SaveOrder(int idUser, int[] arrIdImage)
    {
        UserRepository userRepositors = new UserRepository();
        ImageRepository imgRepos = new ImageRepository();
        List<Image> ListTempAddImage = new List<Image>();//список обьектов Image для этого ордера

        foreach (var idImage in arrIdImage)
        {
            if (imgRepos.GetById(idImage) == null)//если не существует обьекта Image с заданным Id
                continue;

            ListTempAddImage.Add(imgRepos.GetById(idImage));
        }

        if (userRepositors.GetById(idUser) == null)//если не существует такого юзера то возращаем null
            return null;

        Orders.Add(new Order(GetCount(), userRepositors.GetById(idUser), ListTempAddImage));
        return Orders.Count - 1;
    }

    //получить все Order
    public new List<Order> GetAll() => Orders;

    //получить Order по ID 
    public new Order? GetById(int id)
    {
        var index = Orders.FindIndex(p => p.Id == id);
        if (index == -1)
            return null;

        return Orders[id];
    }

    //Удалить по индексу Order
    public new string DeleteId(int id)
    {
        var index = Orders.FindIndex(p => p.Id == id);
        if (index == -1)
            return $"Order {id} does not exist";

        Orders.Remove(GetById(id));

        return $"Order {id} Delete";
    }

    //получить Count Orders
    public int GetCount() => Orders.Count;

}
