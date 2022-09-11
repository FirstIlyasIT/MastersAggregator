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
        string sqlQuery = @"SELECT api_token AS ApiToken, user_name AS ApiUserName  " +
                                 @"FROM master_shema.access";

        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        var tokens = await connection.QueryAsync<Token>(sqlQuery);
        //проверяем есть ли такой токен в БД
        foreach (Token token in tokens)
        {
            if (token.ApiToken == strTokenApi)
            { 
                context.Items.Add("ApiUserName", token.ApiUserName);
                return true;
            } 
        }
        
        return false; 
    }
}
