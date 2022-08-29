using MastersAggregatorService.Data;
using MastersAggregatorService.Models;
 
namespace MastersAggregatorService.Repositories;

public class OrderRepository : BaseRepository<Order>
{
    public IEnumerable<Order> GetAll()
    {
        throw new NotImplementedException();
    }

    public Order GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Order Save(Order model)
    {
        throw new NotImplementedException();
    }

    public void Delete(Order model)
    {
        throw new NotImplementedException();
    }

    public OrderRepository(IConfiguration configuration) : base(configuration)
    {
    }
}
