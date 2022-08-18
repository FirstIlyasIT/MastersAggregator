using MastersAggregatorService.Models;

namespace MastersAggregatorService.Repositories;

public class OrderRepository : BaseRepository<Order>
{
    public override IEnumerable<Order> GetAll()
    {
        throw new NotImplementedException();
    }

    public override Order GetById(int id)
    {
        throw new NotImplementedException();
    }

    public override Order Save(Order model)
    {
        throw new NotImplementedException();
    }

    public override void Delete(Order model)
    {
        throw new NotImplementedException();
    }
}