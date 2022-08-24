using MastersAggregatorService.Models;
using Microsoft.AspNetCore.Mvc;

namespace MastersAggregatorService.Repositories;

public class MasterRepository : BaseRepository<Master>
{
    private static IEnumerable<Master> MastersList { get; set; } // TODO: Временное решение для тестов, потом нужно заменить на DBConnection
    
    public override Master GetById(int id)
    {
        return MastersList.FirstOrDefault(x => x.Id == id);
    }

    public override IEnumerable<Master> GetAll()
    {
        return MastersList;
    }

    public virtual IEnumerable<Master> GetByCondition(bool condition) // TODO: временно виртуальный для прохождения тестов
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

    public override void Delete(Master model)
    {
        throw new NotImplementedException();
    }

    public MasterRepository(IConfiguration configuration) : base(configuration)
    {
    }
}