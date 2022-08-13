namespace MastersAggregatorService.Models;

public class Order : BaseModel
{
    public Order(User orderSender, IEnumerable<Image> images)
    {
        Sender = orderSender;
        Images = images;
    }

    public User Sender { get; }
    
    public IEnumerable<Image> Images { get; }
}