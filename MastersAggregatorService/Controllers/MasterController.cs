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
    /// <returns>List of all masters in Json format</returns>
    /// <response code="200"> Returns List of all Masters in Json format.</response>
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
    /// <response code="200"> Returns Master by id in Json format.</response>
    /// <response code="404"> Master does not exist.</response>
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
    /// <param name="Boolean condition"></param>
    /// <returns>List of masters by condition in Json format</returns>
    /// <response code="200"> Returns Masters by condition in Json format.</response>
    [HttpGet("{condition:bool}")]
    public IActionResult GetByCondition(bool condition)
    {
        return Ok(new JsonResult(_repository.GetByCondition(condition)));
    }

    /// <summary>
    /// POST to change master's condition
    /// </summary>
    /// <param name="ObjectMaster"></param>
    /// <returns>Master with changed condition in Json format</returns>
    /// <response code="200"> Returns Master with changed condition in Json format.</response>
    /// <response code="400"> Invalid master's model</response>
    [HttpPost]
    public IActionResult ChangeCondition(Master master)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        return Ok(new JsonResult(_repository.ChangeCondition(master)));
    }
}