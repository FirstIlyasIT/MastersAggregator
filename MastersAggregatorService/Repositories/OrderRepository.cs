using Dapper;
using MastersAggregatorService.Models;
using Npgsql;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace MastersAggregatorService.Repositories;
 

public class OrderRepository : BaseRepository<Order>
{
    /// <summary>
    /// Получить список всех Order (Async)
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        var Orders = new List<Order>();//список всех Order

        const string sqlQuerySender = @"SELECT DISTINCT orders.id AS ordersid, users.id AS usersid, users.name AS name, users.first_name, users.pfone 
                      FROM master_shema.orders 
                      INNER JOIN master_shema.users ON users.id = orders.user_id
                      INNER JOIN master_shema.image_of_orders ON image_of_orders.order_id = orders.id
					  ORDER BY ordersid";

        const string sqlQueryImage = @"SELECT orders.id AS ordersid, images.url, images.description, images.id AS imagesid
                      FROM master_shema.orders
                      INNER JOIN master_shema.image_of_orders ON order_id = orders.id
                      INNER JOIN master_shema.images ON images.id = image_of_orders.image_id 
                      ORDER BY orders.id";
         
        var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
         
        IEnumerable<dynamic> resultDynamicQuerySenders = connection.Query(sqlQuerySender);
        IEnumerable<dynamic> resultDynamicQueryImages = connection.Query(sqlQueryImage);

        foreach (var ObjSender in resultDynamicQuerySenders)
        {
            //собираем обьект Images с resultDynamicQueryImages
            var Images = new List<Image>();
            foreach (var ObjImages in resultDynamicQueryImages)
            {
                if (ObjSender.ordersid == ObjImages.ordersid)
                    Images.Add(new Image { Id = ObjImages.imagesid, ImageDescription = ObjImages.description, ImageUrl = ObjImages.url });
            }

            //Собираем обьект Order с resultDynamicQuerySenders
            Order order = new Order { Id = ObjSender.ordersid, 
                 Sender = new User { Id = ObjSender.usersid, Name = ObjSender.name, FirstName = ObjSender.first_name, Pfone = ObjSender.pfone }, 
                 Images = Images };

            Orders.Add(order);
        }
          
        await connection.CloseAsync(); 
      
      return Orders;
    }


    public override IEnumerable<Order> GetAll()
    {
        return GetAllAsync().Result;
    }
  

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
