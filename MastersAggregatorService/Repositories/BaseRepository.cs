using MastersAggregatorService.Models;

namespace MastersAggregatorService.Repositories;

public abstract class BaseRepository<T> where T : BaseModel
{
    private protected readonly string _connectionString;
    protected BaseRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetValue<string>("ConnectionString");
    }
    public abstract IEnumerable<T> GetAll();
    public abstract T? GetById(int id);

    /// <summary>
    /// Saves a new object or updates if exist
    /// </summary>
    /// <param name="model">Object to save</param>
    /// <returns>New object with database Id</returns>
    public abstract T? Save(T model);
    public abstract void Delete(T model);
}