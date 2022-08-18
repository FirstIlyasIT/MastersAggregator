using MastersAggregatorService.Models;

namespace MastersAggregatorService.Repositories;

public class UserRepository : BaseRepository<User>
{
    static List<User> Users = new List<User>
    {
        new User { Id = 0, UserName = "Sergey", UserFirstName = "Sidorov", UserPfone = "+745-34-34-153" },
        new User { Id = 1, UserName = "Антон", UserFirstName = "Быстрый", UserPfone = "+745-77-88-111" },
        new User { Id = 5, UserName = "Kolia", UserFirstName = "Smelov", UserPfone = "+745-88-11-222" }
    };

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
        var index = Users.FindIndex(p => p.Id == model.Id);
        if (index == -1)
            return;

        Users.Remove(GetById(index));
    }
}