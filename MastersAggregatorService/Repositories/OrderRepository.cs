using Dapper;
using MastersAggregatorService.Models;
using Npgsql;

namespace MastersAggregatorService.Repositories;
 

public class OrderRepository : BaseRepository<Order>
{  
    /// <summary>
    /// Получить список всех Order (Async)
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        const string sqlQuery = "SELECT orders.id AS Id, users.id AS Id, users.name AS Name, " +
            "users.first_name AS FirstName, users.pfone AS Pfone, images.id AS Id, " +
            "images.url AS ImageUrl, images.description AS ImageDescription " +
            "FROM master_shema.orders " +
            "INNER JOIN master_shema.users ON users.id = orders.user_id " +
            "INNER JOIN master_shema.image_of_orders ON image_of_orders.order_id = orders.id " +
            "INNER JOIN master_shema.images ON images.id = image_of_orders.image_id";

        var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        var orders = await connection.QueryAsync<Order>(sqlQuery);
        await connection.CloseAsync();
        return orders;
    }

    public override IEnumerable<Order> GetAll()
    {
        return GetAllAsync().Result;
    }

    //  sql запрос для GetAllAsync рабочий возращает таблицу с данными проверено на pgAdmin
    /*SELECT orders.id AS Id, users.id AS Id, users.name AS Name, 
           users.first_name AS FirstName, users.pfone AS Pfone, images.id AS Id, 
           images.url AS ImageUrl, images.description AS ImageDescription  
                FROM master_shema.orders 
                INNER JOIN master_shema.users ON users.id = orders.user_id
                INNER JOIN master_shema.image_of_orders ON image_of_orders.order_id = orders.id 
                INNER JOIN master_shema.images ON images.id = image_of_orders.image_id	*/




    public override Order GetById(int id)
    {
        throw new NotImplementedException();
    }

    public override Order Save(Order model)
    {
        throw new NotImplementedException();
    }

    public override void Delete(Order model)
    {
        throw new NotImplementedException();
    }

    public OrderRepository(IConfiguration configuration) : base(configuration)
    {
    }
}
