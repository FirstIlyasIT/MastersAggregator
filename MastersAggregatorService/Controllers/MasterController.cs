using MastersAggregatorService.Models;
using MastersAggregatorService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MastersAggregatorService.Controllers;

[Route("[controller]")]
public class MasterController : BaseController<Master>
{
    private MasterRepository _repository;

    public MasterController(MasterRepository repository) : base(repository)
    {
        _repository = repository;
    }
    
    /// <summary>
    /// GET all masters
    /// </summary> 
    /// <returns>List of all masters</returns> 
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(new JsonResult(_repository.GetAll()));
    }
    
    /// <summary>
    /// GET master by Id
    /// </summary>
    /// <param name="Master's Id"></param>
    /// <returns>Master by id</returns> 
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        if (id == null)
        {
            return NotFound();
        }
        
        return Ok(new JsonResult(_repository.GetById(id)));
    }
    
    /// <summary>
    /// GET masters by condition
    /// </summary>
    /// <param name=""></param>
    /// <returns>List of masters by condition</returns>
    [HttpGet("{condition:bool}")]
    public IActionResult GetByCondition(bool condition)
    {
        return Ok(new JsonResult(_repository.FindByCondition(condition)));
    }

    /// <summary>
    /// POST to change master's condition
    /// </summary>
    /// <param name="ObjectMaster"></param>
    /// <returns>Master with changed condition</returns> 
    [HttpPost]
    public IActionResult ChangeCondition(Master master)
    {
        return Ok(new JsonResult(_repository.ChangeCondition(master)));
    }
}