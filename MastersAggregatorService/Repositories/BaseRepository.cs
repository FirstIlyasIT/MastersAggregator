using MastersAggregatorService.Models;

namespace MastersAggregatorService.Repositories;
 
public abstract class BaseRepository<T> where T : BaseModel
{
    public abstract IEnumerable<T> GetAll();
    public abstract T? GetById(int id);

    /// <summary>
    /// Saves a new object or updates if exist
    /// </summary>
    /// <param name="model">Object to save</param>
    /// <returns>New object with database Id</returns>
    public T Save(T model)
    {
        throw new NotImplementedException();
    }
}