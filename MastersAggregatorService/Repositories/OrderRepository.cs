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
                new Order { Id = 0, Sender = userRepositors.GetById(0), Images = new List<Image> { imgRepos.GetById(0) } },
                new Order { Id = 1, Sender = userRepositors.GetById(1), Images = new List<Image> { imgRepos.GetById(1), imgRepos.GetById(2) } }
            };
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

        Orders.Remove(GetById(index)); 
    }

    //Get Count Orders
    public int GetCount() => Orders.Count;

}
