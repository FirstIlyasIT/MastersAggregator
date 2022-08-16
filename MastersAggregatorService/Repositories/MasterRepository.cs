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

    public override Master GetById(int id)
    {
        return MastersList.FirstOrDefault(x => x.Id == id);
    }

    public override IEnumerable<Master> GetAll()
    {
        return MastersList;
    }

    public override IEnumerable<Master> GetByCondition(bool condition)
    {
        return MastersList.Where(x => x.IsActive == condition).ToList();
    }

    /// <summary>
    /// Changes model condition and returns it back
    /// </summary>
    /// <param name="model">Object to save</param>
    /// <returns>New object with database Id</returns>
    public Master ChangeCondition(Master master)
    {
        if (master.IsActive == false)
        {
            master.IsActive = true;
            return Save(master);
        }

        master.IsActive = false;
        return Save(master);
    }

    /// <summary>
    /// Saves a new object or updates if exist
    /// </summary>
    /// <param name="model">Object to save</param>
    /// <returns>New object with database Id</returns>
    public override Master Save(Master model)
    {
        return model;
    }
}