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

    public void Save(T model)
    {
        throw new NotImplementedException();
    }
}

 