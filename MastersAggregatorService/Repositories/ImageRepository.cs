using MastersAggregatorService.Data;
using MastersAggregatorService.Interfaces;
using MastersAggregatorService.Models;

namespace MastersAggregatorService.Repositories;

public class ImageRepository : BaseRepository<Image>, IImageRepository
{
    public ImageRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public void Delete(Image model)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Image model)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Image> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Image>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Image? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Image> GetByIdAsync(int imageId)
    {
        throw new NotImplementedException();
    }

    public Image? Save(Image model)
    {
        throw new NotImplementedException();
    }

    public Task<Image> SaveAsync(Image model)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Image model)
    {
        throw new NotImplementedException();
    }
}