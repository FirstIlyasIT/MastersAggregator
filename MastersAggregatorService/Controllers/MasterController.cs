using MastersAggregatorService.Models;
using MastersAggregatorService.Repositories;

namespace MastersAggregatorService.Controllers;

public class MasterController : BaseController<Master>
{
    public MasterController(BaseRepository<Master> repository) : base(repository)
    {
    }
}