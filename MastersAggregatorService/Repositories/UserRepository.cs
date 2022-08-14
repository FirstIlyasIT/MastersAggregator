using MastersAggregatorService.Models;

namespace MastersAggregatorService.Repositories;

public class UserRepository : BaseRepository<User>
{
    static List<User> Users { get; set; }

    static UserRepository() //В конструкторе создаем 3-х пользователей 
    {
        Users = new List<User>
        {
            new User { Id = 0, UserName = "Sergey", UserFirstName = "Sidorov", UserPfone = "+745-34-34-153" },
            new User { Id = 1, UserName = "Антон", UserFirstName = "Быстрый", UserPfone = "+745-77-88-111" },
            new User { Id = 2, UserName = "Kolia", UserFirstName = "Smelov", UserPfone = "+745-88-11-222" }
        };
    }

    //получить List Users
    public new List<User> GetAll() => Users;

    //получить User по Id   
    public new User? GetById(int id)
    {
        var index = Users.FindIndex(p => p.Id == id);
        if (index == -1)
            return null;

        return Users[id];
    }
}