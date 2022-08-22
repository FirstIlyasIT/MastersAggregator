using MastersAggregatorService.Data;
using MastersAggregatorService.Models;

namespace MastersAggregatorService.Repositories;

public class UserRepository : BaseRepository<User>
{ 
    static List<User> Users { get; set; }
    //Все тестовые данные в классе TestData для получения тестовых данных прописал их в конструкторе и получай через DI
    public UserRepository()
    {
        Users = TestData.Users; 
    }

    public override IEnumerable<User> GetAll()
    { 
        return Users;
    }

    public override User? GetById(int id)
    {
        var index = Users.FindIndex(p => p.Id == id);
        if (index == -1)
            return null;

        return Users[index];
    }

    public override User Save(User model)
    {
        Users.Add(model);
        return model;
    }

    public override void Delete(User model)
    {
        int index = Users.FindIndex(p => p.Id == model.Id);
        if (index == -1)
            return;

        Users.RemoveAt(index);
    }
     
}