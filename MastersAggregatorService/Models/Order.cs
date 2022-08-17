namespace MastersAggregatorService.Models;

public class Order : BaseModel
{ 
    public Order(int id, User orderSender, IEnumerable<Image> images)
    {
        Id = id;
        Sender = orderSender;
        Images = images;
    }

    public User Sender { get; }
    
    public IEnumerable<Image> Images { get; }
}