using MastersAggregatorService.Models;
using MastersAggregatorService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MastersAggregatorService.Controllers;

[Route("{controller}")]
[Produces("application/json")]
[Consumes("application/json")]
public class MasterController : BaseController<Master>
{
    private readonly MasterRepository _repository;

    public MasterController(MasterRepository repository)
    {
        _repository = repository;
    }
    
    /// <summary>
    /// GET all masters
    /// </summary> 
    /// <returns>List of all masters in Json format</returns>
    /// <response code="200"> Returns List of all Masters in Json format.</response>
    [HttpGet]
    [Route("all")]
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
        var master = _repository.GetById(id);
        
        if (master is null)
        {
            return NotFound();
        }
        
        return Ok(new JsonResult(master));
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
        var masters = _repository.GetByCondition(condition);
        if (masters.Any())
        {
            return Ok(new JsonResult(masters));
        }
        else
        {
            return NotFound();
        }
        
    }

    /// <summary>
    /// POST to change master's condition
    /// </summary>
    /// <param name="ObjectMaster"></param>
    /// <returns>Master with changed condition in Json format</returns>
    /// <response code="200"> Changes master's condition.</response>
    /// <response code="400"> Invalid master's model</response>
    [HttpPost]
    public IActionResult ChangeCondition([FromBody]Master master)
    {
        if (ModelState.IsValid)
        {
            _repository.ChangeCondition(master);
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }
}