using MastersAggregatorService.Models;

namespace MastersAggregatorService.Repositories;

public interface IImageRepository
{
    IEnumerable<Image> GetAll();
    Task<IEnumerable<Image>> GetAllAsync();
    public Image? GetById(int id);
    Task<Image> GetByIdAsync(int imageId);

    /// <summary>
    /// Saves a new object or updates if exist
    /// </summary>
    /// <param name="model">Object to save</param>
    /// <returns>New object with database Id</returns>
    public Image? Save(Image model);
    Task<Image> SaveAsync(Image model);
    public void Delete(Image model);
    Task DeleteAsync(Image model);
    Task UpdateAsync(Image model);
}