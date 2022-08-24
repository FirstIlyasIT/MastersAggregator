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

    public override User? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public override User Save(User model)
    {
        throw new NotImplementedException();
    }

    public override void Delete(User model)
    {
        throw new NotImplementedException();
    }

    public UserRepository(IConfiguration configuration) : base(configuration)
    {
    }
}