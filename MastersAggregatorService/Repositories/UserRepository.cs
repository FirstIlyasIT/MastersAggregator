using MastersAggregatorService.Data;
using MastersAggregatorService.Models;

namespace MastersAggregatorService.Repositories;

public class UserRepository : BaseRepository<User>
{
    public override IEnumerable<User> GetAll()
    {
        throw new NotImplementedException();
    }

    public override User? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public override User Save(User model)
    {
        throw new NotImplementedException();
    }

    public override void Delete(User model)
    {
        throw new NotImplementedException();
    }
}