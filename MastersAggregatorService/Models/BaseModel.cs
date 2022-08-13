namespace MastersAggregatorService.Models;

public abstract class BaseModel
{
    public BaseModel(int id)
    {
        Id = id;
    }

    public int Id { get; }
}