using Dapper;
using MastersAggregatorService.Models;
using Npgsql;

namespace MastersAggregatorService.Repositories;

public class MasterRepository : BaseRepository<Master>
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
        
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        
        var masters = await connection.QueryAsync<Master>(sqlQuery); 
        return masters;
    }

    public override IEnumerable<Master> GetAll()
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
        
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        try
        {
            Master master = await connection.QueryFirstAsync<Master>(sqlQuery, new{ idMaster });
            return master;
        }
        catch (Exception)
        {
            return null;
        }
    }


    /// <summary>
    /// Returns a list of all masters in free(true) busy(false) status (async method)
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>    
    public async Task<IEnumerable<Master>> GetByCondition(bool condition) // TODO: временно виртуальный для прохождения тестов
    {
        string sqlQuery = $"SELECT id AS Id, name AS MastersName, is_active AS IsActive FROM master_shema.masters WHERE is_active = '{condition}' ";
        
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        
        var masters = await connection.QueryAsync<Master>(sqlQuery, new { condition }); 
        return masters;
    }

    public override Master? GetById(int id)
    {
        return GetByIdAsync(id).Result;
    }



    /// <summary>
    /// Changes model condition and returns it back (async)
    /// </summary>
    /// <param name="model">Object to save</param>
    /// <returns>New object with database Id</returns>
    public async Task<Master> ChangeCondition(Master model)
    { 
        string sqlQuery = "UPDATE master_shema.masters SET is_active = @isActive, name = @masterName   WHERE id = @masterId";  
        int masterId = model.Id;
        string masterName = model.MastersName;
        bool isActive = model.IsActive;

        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
         
        try
        {
            await connection.ExecuteAsync(sqlQuery, new { masterId, masterName, isActive }); 
            return model;
        }
        catch (Exception)
        {
            return null;
        }
    }



    /// <summary>
    /// Saves a new object or updates if exist (async)
    /// </summary>
    /// <param name="model"></param>
    /// <returns>New object</returns>
    public async Task<Master> SaveAsync(Master model)
    {
        string sqlQuery = $"INSERT INTO master_shema.masters (name, is_active) VALUES (@mastersName, @isActive)";
        string mastersName = model.MastersName;
        bool isActive = model.IsActive;

        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        try
        { 
            await connection.ExecuteAsync(sqlQuery, new { mastersName, isActive });
            return model;
        }
        catch (Exception)
        {
            return null;
        }
    }
    /// <summary>
    /// Saves a new object or updates if exist
    /// </summary>
    /// <param name="model">Object to save</param>
    /// <returns>New object</returns>
    public override Master Save(Master model)
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
        string sqlQuery = "DELETE FROM master_shema.masters WHERE id = @intIdMaster";

        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        int intIdMaster = model.Id; 
        await connection.ExecuteAsync(sqlQuery, new { intIdMaster });
    }

    public override void Delete(Master model)
    {
        DeleteAsync(model);
    }

}