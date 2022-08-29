using Dapper;
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
        var connection = new NpgsqlConnection(ConnectionString);
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
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        try
        {
            User user = await connection.QueryFirstAsync<User>(sqlQuery, new { idUser });
            return user;
        }
        catch (Exception)
        { 
            return null;
        } 
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
        string sqlQuery = $"INSERT INTO master_shema.users (name, first_name, pfone) VALUES (@userName, @userFirstName, @userPfone)";
        string userName = model.Name;
        string userFirstName = model.FirstName;
        string userPfone = model.Pfone;

        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
         
        try
        {
            await connection.ExecuteAsync(sqlQuery, new { userName, userFirstName, userPfone });
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
        int userId = model.Id;
        string userName = model.Name;
        string userFirstName = model.FirstName;
        string userPfone = model.Pfone;
        var sqlQuery = $@"UPDATE master_shema.users SET name = @userName, first_name = @userFirstName, pfone = @userPfone  WHERE id = @userId";
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        await connection.ExecuteAsync(sqlQuery, new { userName, userFirstName, userPfone, userId });
    }


    /// <summary>
    /// Удалить из БД User (Async)
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task DeleteAsync(User model)
    {
        int userId = model.Id;
        string sqlQuery = $"DELETE FROM master_shema.users WHERE id = @userId";
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        await connection.ExecuteAsync(sqlQuery, new { userId }); 
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