using Dapper;
using MastersAggregatorService.Errors;
using MastersAggregatorService.Interfaces;
using Npgsql;

namespace MastersAggregatorService.Repositories;

public class ExceptionRepository : BaseRepository<ApiException>, IExceptionRepository
{
    public async Task<IEnumerable<ApiException>> GetAllAsync()
    {
        const string sqlQuery =
            $@"SELECT status_code AS {nameof(ApiException.StatusCode)}," +
            $@"user_name AS {nameof(ApiException.UserName)}, " +
            $@"message AS {nameof(ApiException.Message)}, " +
            $@"details AS {nameof(ApiException.Details)}" +
            @" FROM master_shema.errors";

        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        var exceptions = await connection.QueryAsync<ApiException>(sqlQuery);

        return exceptions;
    }
    
    public async Task SaveAsync(ApiException ex)
    {
        const string sqlQuery = 
            @"INSERT INTO master_shema.errors (status_code, user_name, message, details)" +
            $@"VALUES (@{nameof(ex.StatusCode)}), (@{nameof(ex.UserName)}), (@{nameof(ex.Message)}), (@{nameof(ex.Details)})";

        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        await connection.ExecuteAsync(sqlQuery);
    }

    public ExceptionRepository(IConfiguration configuration) : base(configuration)
    {
        
    }
}