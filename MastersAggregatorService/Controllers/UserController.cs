using MastersAggregatorService.Models;
using MastersAggregatorService.Repositories;

namespace MastersAggregatorService.Controllers;

public class UserController : BaseController<User>
{
    public UserController(BaseRepository<User> repository) : base(repository)
    {
    }
}