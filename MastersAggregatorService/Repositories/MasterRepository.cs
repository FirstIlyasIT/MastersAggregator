using Dapper;
using MastersAggregatorService.Models;
using Npgsql;

namespace MastersAggregatorService.Repositories;

public class MasterRepository : BaseRepository<Master>
{
    const string SQLTABL = "master_shema.masters";

    public MasterRepository(IConfiguration configuration) : base(configuration)
    {
    }

    /// <summary>
    /// Get a list of all masters (Async)
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Master>> GetAllAsync()
    {
        string sqlQuery = $"SELECT id AS Id, name AS MastersName, is_active AS IsActive FROM {SQLTABL}";
        var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        var masters = await connection.QueryAsync<Master>(sqlQuery);
        await connection.CloseAsync();
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
        string sqlQuery = $"SELECT id AS Id, name AS MastersName, is_active AS IsActive  FROM {SQLTABL} WHERE Id = '{idMaster}'";
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        try
        {
            Master master = await connection.QueryFirstAsync<Master>(sqlQuery);
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
        string sqlQuery = $"SELECT id AS Id, name AS MastersName, is_active AS IsActive FROM {SQLTABL} WHERE is_active = '{condition}' ";
        var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        var masters = await connection.QueryAsync<Master>(sqlQuery);
        await connection.CloseAsync();
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
    public async Task<Master> ChangeCondition(Master master)
    {
        if (master.IsActive)
        {
            master.IsActive = false;
            return await SaveAsync(master);
        }

        master.IsActive = true;
        return await SaveAsync(master);
    }



    /// <summary>
    /// Saves a new object or updates if exist (async)
    /// </summary>
    /// <param name="model"></param>
    /// <returns>New object</returns>
    public async Task<Master> SaveAsync(Master model)
    {
        string sqlQuery = $"INSERT INTO {SQLTABL} (name, is_active) VALUES ('{model.MastersName}', '{model.IsActive}')";
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        try
        {
            await using (var cmd = new NpgsqlCommand(sqlQuery, connection))
            {
                await cmd.ExecuteNonQueryAsync();
            }
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
        string sqlQuery = $"DELETE FROM {SQLTABL} WHERE id = '{model.Id}'";
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        await connection.ExecuteAsync(sqlQuery);
    }

    public override void Delete(Master model)
    {
        DeleteAsync(model);
    }

}