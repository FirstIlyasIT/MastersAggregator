namespace MastersAggregatorService.Models;

public class Master : BaseModel
{
    public string MastersName { get; init; }
    public bool IsActive { get; init; } // TODO: Заменить на enum

/*    public Master(int id, string name, bool condition)
    {
        Id = id;
        MastersName = name;
        IsActive = condition;
    }*/
    
}