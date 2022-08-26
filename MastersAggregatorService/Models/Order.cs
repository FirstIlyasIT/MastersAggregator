namespace MastersAggregatorService.Models;

public class Order : BaseModel
{ 
    public User Sender { get; set; }  //init

    public IEnumerable<Image> Images { get; set; }
}