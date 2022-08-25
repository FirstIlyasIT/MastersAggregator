using MastersAggregatorService.Models;
using MastersAggregatorService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MastersAggregatorService.Controllers;

[ApiController]
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
    public async Task<IActionResult> GetAll()
    {
        var masters = await _repository.GetAllAsync();
        if (masters.Any())
            return Ok(masters);
        else
            return NotFound();
    }


    /// <summary>
    /// GET master by Id
    /// </summary>
    /// <param name="Master's Id"></param>
    /// <returns>Master by id</returns>
    /// <response code="200"> Returns Master by id in Json format.</response>
    /// <response code="404"> Master does not exist.</response>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var master = await _repository.GetByIdAsync(id);

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
    /// <response code="404"> Masters with this condition does not exist</response>
    [HttpGet("{condition:bool}")]
    public async Task<IActionResult> GetByCondition(bool condition)
    {
        var masters = await _repository.GetByCondition(condition);
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
    /// Create new master's
    /// </summary>
    /// <param name="master"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("CreateMaster")]
    public async Task<IActionResult> CreateMaster([FromBody] Master master)
    {
        await _repository.SaveAsync(master);
        return NoContent();
    }


    /// <summary>
    /// POST to change master's condition
    /// </summary>
    /// <param name="ObjectMaster"></param>
    /// <returns>Master with changed condition in Json format</returns>
    /// <response code="200"> Changes master's condition.</response>
    /// <response code="400"> Invalid master's model</response>
    [HttpPut]
    [Route("ChangeCondition")]
    public async Task<IActionResult> ChangeCondition(Master master)
    {
        if (ModelState.IsValid)
        {
            await _repository.ChangeCondition(master);
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }

    /// <summary>
    /// Delete Master
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>    
    /// <response code="200"> Delete master </response>
    /// <response code="400"> Invalid master's model</response>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteMaster(int id)
    {
        var master = await _repository.GetByIdAsync(id);
        if (master is null)
            return BadRequest();
        else
        {
            await _repository.DeleteAsync(master);
            return NoContent();
        }
    }
}