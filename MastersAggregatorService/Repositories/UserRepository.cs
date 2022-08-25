using Dapper;
using MastersAggregatorService.Models;
using Npgsql;

namespace MastersAggregatorService.Repositories;

public class UserRepository : BaseRepository<User>
{
    const string SQLTABL = "master_shema.users"; 
    /// <summary>
    /// Получить список всех User (Async)
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        const string sqlQuery = $"SELECT id AS Id, name AS Name, first_name AS FirstName, pfone AS Pfone FROM {SQLTABL}";
        var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        var users = await connection.QueryAsync<User>(sqlQuery);
        await connection.CloseAsync();
        return users;
    }

    public override IEnumerable<User> GetAll()
    {
        return GetAllAsync().Result;
    }

    /// <summary>
    /// Получить обьект User по его id (Async)
    /// </summary>
    /// <param name="idUser"></param>
    /// <returns></returns> 
    public async Task<User> GetByIdAsync(int idUser)
    {
        string sqlQuery = $"SELECT id AS Id, name AS Name, first_name AS FirstName, pfone AS Pfone  FROM {SQLTABL} WHERE Id = '{idUser}'";
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        try
        {
            User user = await connection.QueryFirstAsync<User>(sqlQuery);
            return user;
        }
        catch (Exception)
        { 
            return null;
        } 
    } 
 
    public override User? GetById(int id)
    {
        return GetByIdAsync(id).Result;
    }


    /// <summary>
    /// Добавить нового юзера (Async)
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<User> SaveAsync(User model)
    {    
        string sqlQuery = $"INSERT INTO {SQLTABL} (name, first_name, pfone) VALUES ('{model.Name}', '{model.FirstName}', '{model.Pfone}')";
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
 
    public override User Save(User model)
    {
        return SaveAsync(model).Result;
    }



    /// <summary>
    /// Обновить User 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task UpdateAsync(User model)
    {
        var sqlQuery = $@"UPDATE {SQLTABL} SET name = '{model.Name}', first_name = '{model.FirstName}', pfone = '{model.Pfone}' WHERE id = '{model.Id}'";
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
         
        await connection.ExecuteAsync(sqlQuery);
    }

  
 

    /// <summary>
    /// Удалить из БД User
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task DeleteAsync(User model)
    {
        string sqlQuery = $"DELETE FROM {SQLTABL} WHERE id = '{model.Id}'";
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        await connection.ExecuteAsync(sqlQuery); 
    }
  
    public override void Delete(User model)
    {
        DeleteAsync(model);
    }



    public UserRepository(IConfiguration configuration) : base(configuration)
    {
    }
}