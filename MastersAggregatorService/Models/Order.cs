namespace MastersAggregatorService.Models;

public class Order : BaseModel
{ 
    public User Sender { get; init; }  //init

    public IEnumerable<Image> Images { get; init; }
}