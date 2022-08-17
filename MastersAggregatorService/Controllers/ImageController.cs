using MastersAggregatorService.Models;
using MastersAggregatorService.Repositories;

namespace MastersAggregatorService.Controllers;

public class ImageController : BaseController<Image>
{
    public ImageController(ImageRepository repository)
    {
    }
}