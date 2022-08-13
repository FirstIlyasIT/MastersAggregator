using MastersAggregatorService.Models;
using Microsoft.AspNetCore.Mvc;

namespace MastersAggregatorService.Repositories;

public class MasterRepository : BaseRepository<Master>
{
    private static IEnumerable<Master> MastersList { get; set; }

    static MasterRepository()
    {
        MastersList = new List<Master>
        {
            new Master(0, "Sergey", true),
            new Master(1, "Roman", true),
            new Master(2, "Ilyas", false)
        };
    }

    public new Master GetById(int id)
    {
        if (id == null) return null;
        
        return MastersList.FirstOrDefault(x => x.Id == id);
    }

    public new IEnumerable<Master> GetAll()
    {
        return MastersList;
    }

    public IEnumerable<Master> FindByCondition(bool condition)
    {
        return MastersList.Where(x => x.IsActive == condition);
    }

    public Master ChangeCondition(Master master)
    {
        throw new NotImplementedException();
    }
}