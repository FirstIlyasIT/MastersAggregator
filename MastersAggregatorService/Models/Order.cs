namespace MastersAggregatorService.Models;

public class Order : BaseModel
{
    public Order(User orderSender, IEnumerable<Image> images)
    {
        OrderSender = orderSender;
        Images = images;
    }

    public User OrderSender { get; }
    
    public IEnumerable<Image> Images { get; }
}