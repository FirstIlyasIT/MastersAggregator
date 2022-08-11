using MastersAggregatorService.Models;

namespace MastersAggregatorService.Repositories;

public class UserRepository : BaseRepository<User>
{
    public static List<User> Users { get; }

    static UserRepository() //В конструкторе создаем 3-х пользователей 
    {
        Users = new List<User>
        {
            new User { UserName = "Sergey", UserFirstName = "Sidorov", UserPfone = "+745-34-34-153" },
            new User { UserName = "Антон", UserFirstName = "Быстрый", UserPfone = "+745-77-88-111" },
            new User { UserName = "Kolia", UserFirstName = "Smelov", UserPfone = "+745-88-11-222" }
        };
    }

    public new User? GetById(int id)
    {
        if (Users[id] is null)
            return null;

        return Users[id];
    }
}