using Dapper;
using MastersAggregatorService.Interfaces;
using MastersAggregatorService.Models;
using Npgsql;

namespace MastersAggregatorService.Repositories;


public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(IConfiguration configuration) : base(configuration)
    {
    }

    /// <summary>
    /// Получить список всех Order (Async)
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        var orders = new List<Order>();//список всех Order

        const string sqlQuerySender = @"SELECT DISTINCT orders.id AS ordersid, users.id AS usersid, users.name AS name, users.first_name, users.pfone " +
                                            @"FROM master_shema.orders " +
                                            @"INNER JOIN master_shema.users ON users.id = orders.user_id " +
                                            @"INNER JOIN master_shema.image_of_orders ON image_of_orders.order_id = orders.id " +
                                            @"ORDER BY ordersid";

        const string sqlQueryImage = @"SELECT orders.id AS ordersid, images.url, images.description, images.id AS imagesid " +
                                            @"FROM master_shema.orders " +
                                            @"INNER JOIN master_shema.image_of_orders ON order_id = orders.id " +
                                            @"INNER JOIN master_shema.images ON images.id = image_of_orders.image_id " +
                                            @"ORDER BY orders.id";

        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
         
        IEnumerable<dynamic> resultDynamicQuerySenders = connection.Query(sqlQuerySender);
        IEnumerable<dynamic> resultDynamicQueryImages = connection.Query(sqlQueryImage);

        foreach (var ObjSender in resultDynamicQuerySenders)
        {
            //собираем обьект Images с resultDynamicQueryImages
            var Images = new List<Image>();
            foreach (var ObjImages in resultDynamicQueryImages)  //TODO Заменить сборку Images на метод из ImageRepository
            {
                if (ObjSender.ordersid == ObjImages.ordersid)
                    Images.Add(new Image { Id = ObjImages.imagesid, ImageDescription = ObjImages.description, ImageUrl = ObjImages.url });
            }

            //Собираем обьект Order с resultDynamicQuerySenders
            Order order = new Order { Id = ObjSender.ordersid, 
                 Sender = new User { Id = ObjSender.usersid, Name = ObjSender.name, FirstName = ObjSender.first_name, Pfone = ObjSender.pfone }, 
                 Images = Images };

            orders.Add(order);
        }
            
      return orders;
    }

    /// <summary>
    /// Получить список всех Order  
    /// </summary>
    /// <returns></returns>   
    public IEnumerable<Order> GetAll()
    {
        return GetAllAsync().GetAwaiter().GetResult();
    }

     
    /// <summary>
    /// Получить обьект Order по его id (Async)
    /// </summary>
    /// <param name="idOrder"></param>
    /// <returns></returns> 
    public async Task<Order> GetByIdAsync(int idOrder)
    {
        const string sqlQueryUserSender = @"SELECT DISTINCT orders.id AS ordersid, users.id AS usersid, users.name AS name, users.first_name, users.pfone " +
                                                @"FROM master_shema.orders " +
                                                @"INNER JOIN master_shema.users ON users.id = orders.user_id " +
                                                @"INNER JOIN master_shema.image_of_orders ON image_of_orders.order_id = orders.id " +
                                                @"WHERE orders.id = @idOrder";

        const string sqlQueryImage = @"SELECT orders.id AS ordersid, images.url, images.description, images.id AS imagesid " +
                                            @"FROM master_shema.orders " +
                                            @"INNER JOIN master_shema.image_of_orders ON order_id = orders.id " +
                                            @"INNER JOIN master_shema.images ON images.id = image_of_orders.image_id " +
                                            @"WHERE orders.id = @idOrder";


        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
           
        IEnumerable<dynamic> resultDynamicQuerySenders = connection.Query(sqlQueryUserSender, new { idOrder });
        var ObjSender = resultDynamicQuerySenders.First();

        IEnumerable<dynamic> resultDynamicQueryImages = connection.Query(sqlQueryImage, new { idOrder });

        //собираем обьект Images с resultDynamicQueryImages
        var Images = new List<Image>();
        foreach (var ObjImages in resultDynamicQueryImages)
        { 
            Images.Add(new Image { Id = ObjImages.imagesid, ImageDescription = ObjImages.description, ImageUrl = ObjImages.url });
        }

        //Собираем обьект Order с resultDynamicQuerySenders
        Order order = new Order
        {
            Id = ObjSender.ordersid,
            Sender = new User { Id = ObjSender.usersid, Name = ObjSender.name, FirstName = ObjSender.first_name, Pfone = ObjSender.pfone },
            Images = Images
        };
             
        return order;
       
    }

    /// <summary>
    /// Получить обьект Order по его id 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns> 
    public Order GetById(int id)
    {
        return GetByIdAsync(id).GetAwaiter().GetResult();
    }
     

    /// <summary>
    /// Добавить новый Order (Async)
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<Order> SaveAsync(Order model)
    {
        //SQL запрос - добавляем в таблицу orders новый ордер если user_id существует
        string sqlQueryAddOrders = @"INSERT INTO master_shema.orders (user_id) " +
                                          @"SELECT users.id FROM master_shema.users " +
                                          @"WHERE master_shema.users.id = @userId " +
                                          @"RETURNING id";
        int userId = model.Sender.Id;

        //SQL запрос - добавляем в таблицу image_of_orders новый image если они существуют в таблице 
        string sqlQueryAddImages = @"INSERT INTO master_shema.image_of_orders (order_id, image_id) " +
                                          @"VALUES (@resIdNewOrder, (SELECT master_shema.images.id  FROM master_shema.images WHERE master_shema.images.id = @Id)) ";
         
        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
         
        int? resIdNewOrder = await connection.QuerySingleAsync<int>(sqlQueryAddOrders, new { userId });
        if (resIdNewOrder == null)
            return null;

        //перебераем список Images и добавляем в БД master_shema.image_of_orders Id-картинки (добавляем если они существуют в master_shema.images)
        foreach (var image in model.Images)
        { 
            try
            {
                await connection.ExecuteAsync(sqlQueryAddImages, new { resIdNewOrder, image.Id });
            }
            catch (Exception)
            { 
            } 
        } 

        return model; 
    }

    /// <summary>
    /// Добавить новый Order  
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public Order Save(Order model)
    {
        return SaveAsync(model).GetAwaiter().GetResult();
    }


    /// <summary>
    /// Удалить из БД Order (Async)
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task DeleteAsync(Order model)
    { 
        string sqlQuery = @"DELETE FROM master_shema.image_of_orders WHERE image_of_orders.order_id = @Id; " +
                          @"DELETE FROM master_shema.orders WHERE orders.id = @Id";
        await using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        await connection.ExecuteAsync(sqlQuery, new { model.Id });
    }

    /// <summary>
    /// Удалить из БД Order
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public void Delete(Order model)
    {
        DeleteAsync(model).GetAwaiter().GetResult();
    } 
}
