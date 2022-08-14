using MastersAggregatorService.Models;

namespace MastersAggregatorService.Repositories;
 
public abstract class BaseRepository<T> where T : BaseModel
{
    public IEnumerable<T> GetAll()
    {
        throw new NotImplementedException();
    }
     
    public T GetById(int id)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Saves a new object or updates if exist
    /// </summary>
    /// <param name="model">Object to save</param>
    /// <returns>New object with database Id</returns>
    public T Save(T model)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Delete object 
    /// </summary>
    /// <param name="id">id object to delete</param>
    /// <returns></returns>
    public T DeleteId(int id)
    {
        throw new NotImplementedException();
    }
}

 