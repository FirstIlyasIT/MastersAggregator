using MastersAggregatorService.Models;
using MastersAggregatorService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MastersAggregatorService.Controllers;

public abstract class BaseController<T> : Controller where T: BaseModel
{
    protected BaseRepository<T> Repository { get; }
    
    public BaseController(BaseRepository<T> repository)
    {
        Repository = repository;
    }
}