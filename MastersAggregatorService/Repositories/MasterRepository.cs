using Dapper;
using MastersAggregatorService.Interfaces;
using MastersAggregatorService.Models;
using Npgsql;

namespace MastersAggregatorService.Repositories;

public class MasterRepository : BaseRepository<Master>, IMasterRepository
{
    public MasterRepository(IConfiguration configuration) : base(configuration)
    {
    }

    /// <summary>
    /// Get a list of all masters (Async)
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Master>> GetAllAsync()
    {
        string sqlQuery = $"SELECT id AS Id, name AS MastersName, is_active AS IsActive FROM master_shema.masters  ORDER BY id";

        await using var connection = new NpgsqlConnection(ConnectionString); 
        connection.Open();
        var masters = await connection.QueryAsync<Master>(sqlQuery); 
        return masters;
    }

    /// <summary>
    /// Get a list of all masters  
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Master> GetAll()
    {
        return GetAllAsync().Result;
    }


    /// <summary>
    /// Get the Master object by its id (Async)
    /// </summary>
    /// <param name="idMaster"></param>
    /// <returns></returns> 
    public async Task<Master> GetByIdAsync(int idMaster)
    {
        string sqlQuery = "SELECT id AS Id, name AS MastersName, is_active AS IsActive  FROM master_shema.masters WHERE Id = @idMaster";

        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        return await connection.QueryFirstOrDefaultAsync<Master>(sqlQuery, new { idMaster }); 
    }

    /// <summary>
    /// Get the Master object by its id  
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns> 
    public Master? GetById(int id)
    {
        return GetByIdAsync(id).Result;
    }


    /// <summary>
    /// Returns a list of all masters in free(true) busy(false) status (async method)
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>    
    public async Task<IEnumerable<Master>> GetByConditionAsync(bool condition) 
    {
        string sqlQuery = $"SELECT id AS Id, name AS MastersName, is_active AS IsActive FROM master_shema.masters WHERE is_active = '{condition}' ";

        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        var masters = await connection.QueryAsync<Master>(sqlQuery, new { condition }); 
        return masters;
    }


    /// <summary>
    /// Changes Master condition and returns it back (async)
    /// </summary>
    /// <param name="model">Object to save</param>
    /// <returns>New object with database Id</returns>
    public async Task<Master> UpdateAsync(Master model)
    { 
        string sqlQuery = "UPDATE master_shema.masters SET is_active = @IsActive, name = @MastersName  WHERE id = @Id";  
 
        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        await connection.ExecuteAsync(sqlQuery, new { model.Id, model.MastersName, model.IsActive });
        return model;
    }


    /// <summary>
    /// Saves a new Master or updates if exist (async)
    /// </summary>
    /// <param name="model"></param>
    /// <returns>New object</returns>
    public async Task<Master> SaveAsync(Master model)
    {
        string sqlQuery = $"INSERT INTO master_shema.masters (name, is_active) VALUES (@MastersName, @IsActive)"; 

        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
         
        await connection.ExecuteAsync(sqlQuery, new { model.MastersName, model.IsActive });
        return model; 
    }

    /// <summary>
    /// Saves a new Master or updates if exist
    /// </summary>
    /// <param name="model">Object to save</param>
    /// <returns>New object</returns>
    public Master Save(Master model)
    {
        return SaveAsync(model).Result; 
    }


    /// <summary>
    /// Delete (async)
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task DeleteAsync(Master model)
    {
        string sqlQuery = "DELETE FROM master_shema.masters WHERE id = @Id";

        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
         
        await connection.ExecuteAsync(sqlQuery, new { model.Id });
    }

    /// <summary>
    /// Delete 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public void Delete(Master model)
    {
        DeleteAsync(model);
    } 
}