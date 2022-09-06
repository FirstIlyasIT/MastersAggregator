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
        const string sqlQuery =
            $@" SELECT id AS {nameof(User.Id)}," +
            $@" name AS {nameof(User.Name)}," +
            $@" first_name AS {nameof(User.FirstName)}," +
            $@" pfone AS {nameof(User.Pfone)}" +
            @" FROM master_shema.users";
        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        var users = await connection.QueryAsync<User>(sqlQuery);
        
        return users;
    }

    /// <summary>
    /// Получить список всех User  
    /// </summary>
    /// <returns></returns>
    public IEnumerable<User> GetAll()
    {
        return GetAllAsync().GetAwaiter().GetResult();
    }

    public async Task<User> GetByIdAsync(int userId)
    {
        const string sqlQuery = 
        $@" SELECT id AS {nameof(User.Id)}," +
        $@" name AS {nameof(User.Name)}," +
        $@" first_name AS {nameof(User.FirstName)}," +
        $@" pfone AS {nameof(User.Pfone)}" +
         @" FROM master_shema.users WHERE id = @Id";

        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        var user = await connection.QueryFirstAsync<User>(sqlQuery, new { Id = userId });

        return user;
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

    public async Task<User> SaveAsync(User model)
    {
        const string sqlQuery =
            @"INSERT INTO master_shema.users (name, first_name, pfone)" +
            $@"VALUES (@{nameof(User.Name)}, @{nameof(User.FirstName)}, @{nameof(User.Pfone)})" +
            @"RETURNING id";

        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        var id = connection.Query<int>(sqlQuery, model);

        var result =
            new User { Id = id.FirstOrDefault(), Name = model.Name, Pfone = model.Pfone, FirstName = model.FirstName};

        return result;
    }
 

    public User Save(User model)
    {
        return SaveAsync(model).Result;
    }

    public async Task DeleteAsync(User model)
    {
        const string sqlQuery =
            "DELETE FROM master_shema.users WHERE id = @Id";

        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        await connection.ExecuteAsync(sqlQuery, new { Id = model.Id });
    }


    public void Delete(User model)
    {
        DeleteAsync(model).GetAwaiter().GetResult();
    }

    public async Task UpdateAsync(User model)
    {
        const string sqlQuery =
            @" UPDATE master_shema.users" +
            $@" SET name = @{nameof(User.Name)}," +
            $@"firstname = @{nameof(User.FirstName)}," +
            $@"pfone = @{nameof(User.Pfone)}" +
            $@" WHERE id = @{nameof(User.Id)}";

        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        await connection.ExecuteAsync(sqlQuery, model);
    }
}