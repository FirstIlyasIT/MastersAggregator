using MastersAggregatorService.Models;

namespace MastersAggregatorService.Interfaces;

public interface IMasterRepository
{
    IEnumerable<Master> GetAll();
    Task<IEnumerable<Master>> GetAllAsync();
    public Master? GetById(int id);
    Task<Master> GetByIdAsync(int Id);

    /// <summary>
    /// Saves a new object or updates if exist
    /// </summary>
    /// <param name="model">Object to save</param>
    /// <returns>New object with database Id</returns>
    public Master? Save(Master model);
    Task<Master> SaveAsync(Master model);
    public void Delete(Master model);
    Task DeleteAsync(Master model);
    Task<Master> UpdateAsync(Master model);
    Task<IEnumerable<Master>> GetByConditionAsync(bool condition);

}
