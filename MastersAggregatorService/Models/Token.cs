namespace MastersAggregatorService.Models;

public class Token : BaseModel
{
    public string ApiToken { get; set; }
    public int User_Id { get; set; }
}