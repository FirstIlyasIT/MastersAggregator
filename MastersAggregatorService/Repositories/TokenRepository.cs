using Dapper;
using MastersAggregatorService.Interfaces;
using MastersAggregatorService.Models;
using Npgsql;

namespace MastersAggregatorService.Repositories;

public class TokenRepository : BaseRepository<Token>
{
    public TokenRepository(IConfiguration configuration) : base(configuration)
    {
    }

    /// <summary>
    /// Проверяем существует strTokenApi в БД  
    /// </summary>
    /// <returns></returns> 
    public async Task<bool> ValidateTokenAsync(string strTokenApi, HttpContext context)
    {
        //получаем список Token из master_shema.access
        string sqlQuery = @"SELECT api_token AS ApiToken, user_id AS User_Id  " +
                                 @"FROM master_shema.access";

        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        var tokens = await connection.QueryAsync<Token>(sqlQuery);

        foreach (Token token in tokens)
        {
            if (token.ApiToken == strTokenApi)
            {
                context.Items.Add("TokenApiUserId", token);
                return true;
            } 
        }

        return false; 
    }
}
