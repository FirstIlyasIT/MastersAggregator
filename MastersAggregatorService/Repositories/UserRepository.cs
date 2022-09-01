using Dapper;
using MastersAggregatorService.Interfaces;
using MastersAggregatorService.Models;
using Npgsql;

namespace MastersAggregatorService.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(IConfiguration configuration) : base(configuration)
    {
    }

    /// <summary>
    /// Получить список всех User (Async)
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        const string sqlQuery = $"SELECT id AS Id, name AS Name, first_name AS FirstName, pfone AS Pfone FROM master_shema.users";
        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        var users = await connection.QueryAsync<User>(sqlQuery);
        await connection.CloseAsync();
        return users;
    }

    /// <summary>
    /// Получить список всех User  
    /// </summary>
    /// <returns></returns>
    public IEnumerable<User> GetAll()
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
        string sqlQuery = $"SELECT id AS Id, name AS Name, first_name AS FirstName, pfone AS Pfone  FROM master_shema.users WHERE Id = @idUser";
        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        return await connection.QueryFirstOrDefaultAsync<User>(sqlQuery, new { idUser });
    }

    /// <summary>
    /// Получить обьект User по его id  
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns> 
    public User? GetById(int id)
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
        string sqlQuery = $"INSERT INTO master_shema.users (name, first_name, pfone) VALUES (@Name, @FirstName, @Pfone)";   

        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
         
        try
        {
            await connection.ExecuteAsync(sqlQuery, new { model.Name, model.FirstName, model.Pfone });
            return model;
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    /// Добавить нового юзера 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public User Save(User model)
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
        var sqlQuery = $@"UPDATE master_shema.users SET name = @Name, first_name = @FirstName, pfone = @Pfone  WHERE id = @Id";
        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        await connection.ExecuteAsync(sqlQuery, new { model.Name, model.FirstName, model.Pfone, model.Id });
    }


    /// <summary>
    /// Удалить из БД User (Async)
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task DeleteAsync(User model)
    { 
        string sqlQuery = $"DELETE FROM master_shema.users WHERE id = @Id";
        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        await connection.ExecuteAsync(sqlQuery, new { model.Id }); 
    }

    /// <summary>
    /// Удалить из БД User
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public void Delete(User model)
    {
        DeleteAsync(model);
    } 
}