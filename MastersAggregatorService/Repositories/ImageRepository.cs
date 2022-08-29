using Dapper;
using MastersAggregatorService.Models;
using Npgsql;

namespace MastersAggregatorService.Repositories;

public class ImageRepository : BaseRepository<Image>, IImageRepository
{
    public async Task<IEnumerable<Image>> GetAllAsync()
    {
        const string sqlQuery =
            $@"SELECT id AS {nameof(Image.Id)},
                      url AS {nameof(Image.ImageUrl)},
                      description AS {nameof(Image.ImageDescription)}
                FROM master_shema.images";

        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        var images = await connection.QueryAsync<Image>(sqlQuery);

        return images;
    }

    public IEnumerable<Image> GetAll()
    {
        return GetAllAsync().Result;
    }

    public async Task<Image> GetByIdAsync(int imageId)
    {
        const string sqlQuery =
            $@"SELECT id AS {nameof(Image.Id)},
                      url AS {nameof(Image.ImageUrl)},
                      description AS {nameof(Image.ImageDescription)}
                FROM master_shema.images
                WHERE id = @Id";
        
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        var image = await connection.QueryFirstAsync(sqlQuery, new { Id = imageId });

        return image;
    }

    public Image GetById(int id)
    {
        return GetByIdAsync(id).Result;
    }

    public async Task<Image> SaveAsync(Image model)
    {
        const string sqlQuery =
            $@"INSERT INTO master_shema.images(url, description)
               VALUES (@{nameof(Image.ImageUrl)}, @{nameof(Image.ImageDescription)})";
        
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        await connection.ExecuteAsync(sqlQuery, model);

        return model;
    }

    public Image Save(Image model)
    {
        return SaveAsync(model).Result;
    }

    public async Task DeleteAsync(Image model)
    {
        const string sqlQuery =
            "DELETE FROM master_shema.images WHERE id = @Id";

        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        await connection.ExecuteAsync(sqlQuery, new {Id = model.Id});
    }

    public void Delete(Image model)
    {
         DeleteAsync(model);
    }

    public async Task UpdateAsync(Image model)
    {
        const string sqlQuery =
            $@"UPDATE master_shema.images
               SET url = @{nameof(model.ImageUrl)},
                   description = @{nameof(model.ImageDescription)}
               WHERE id = @{nameof(model.Id)}";

        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        await connection.ExecuteAsync(sqlQuery, model);
    }

    public void Update(Image model)
    {
        UpdateAsync(model);
    }

    public ImageRepository(IConfiguration configuration) : base(configuration)
    {
    }
}