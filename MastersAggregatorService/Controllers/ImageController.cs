using MastersAggregatorService.Models;
using MastersAggregatorService.Repositories;

namespace MastersAggregatorService.Controllers;

public class ImageController : BaseController<Image>
{
    public ImageController(BaseRepository<Image> repository) : base(repository)
    {
    }
}