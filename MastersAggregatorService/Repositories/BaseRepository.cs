using MastersAggregatorService.Models;

namespace MastersAggregatorService.Repositories;

public abstract class BaseRepository<T> where T: BaseModel
{
    public virtual IEnumerable<T> GetAll()
    {
        throw new NotImplementedException();
    }

    public virtual T GetById(int id)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Saves a new object or updates if exist
    /// </summary>
    /// <param name="model">Object to save</param>
    /// <returns>New object with database Id</returns>
    public virtual T Save(T model)
    {
        throw new NotImplementedException();
    }

    public virtual IEnumerable<T> GetByCondition(bool condition)
    {
        throw new NotImplementedException();
    }
}