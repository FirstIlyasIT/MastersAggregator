using Dapper;
using MastersAggregatorService.Models;
using Npgsql;

namespace MastersAggregatorService.Repositories;

public class UserRepository : BaseRepository<User>
{
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        const string sqlQuery = "SELECT id AS Id, name AS Name, first_name AS FirstName, pfone AS Pfone " +
                                "FROM master_shema.users";
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        var users = await connection.QueryAsync<User>(sqlQuery);
        
        return users;
    }

    public override IEnumerable<User> GetAll()
    {
        return GetAllAsync().Result;
    }

    public async Task<User> GetByIdAsync(int userId)
    {
        const string sqlQuery = 
        $@"SELECT id AS {nameof(User.Id)},
                  name AS {nameof(User.Name)},
                  first_name AS {nameof(User.FirstName)},
                  pfone AS {nameof(User.Pfone)}
        FROM master_shema.users 
        WHERE id = @Id";

        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        var user = await connection.QueryFirstAsync<User>(sqlQuery, new { Id = userId });

        return user;
    }

    public override User? GetById(int id)
    {
        return GetByIdAsync(id).Result;
    }

    public async Task<User> SaveAsync(User model)
    {
        const string sqlQuery =
            $@"INSERT INTO master_shema.users (name, first_name, pfone)
             VALUES (@{nameof(User.Name)}, @{nameof(User.FirstName)}, @{nameof(User.Pfone)})";

        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        await connection.ExecuteAsync(sqlQuery, model);

        return model;
    }

    public override User Save(User model)
    {
        return SaveAsync(model).Result;
    }

    public async Task DeleteAsync(User model)
    {
        const string sqlQuery =
            "DELETE FROM master_shema.users WHERE id = @Id";
        
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        await connection.ExecuteAsync(sqlQuery, new { Id = model.Id });
    }


    public override void Delete(User model)
    {
        DeleteAsync(model);
    }

    public async Task UpdateAsync(User model)
    {
        const string sqlQuery =
            $@"UPDATE master_shema.users 
               SET name = @{nameof(User.Name)},
                   firstname = @{nameof(User.FirstName)},
                   pfone = @{nameof(User.Pfone)}
               WHERE id = @{nameof(User.Id)}";

        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        await connection.ExecuteAsync(sqlQuery, model);
    }

    public UserRepository(IConfiguration configuration) : base(configuration)
    {
    }
}