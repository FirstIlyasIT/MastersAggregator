using MastersAggregatorService.Models;
using MastersAggregatorService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MastersAggregatorService.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
[Consumes("application/json")]
public class UserController : BaseController<User>
{
    private readonly UserRepository _repository;
    public UserController(UserRepository repository)
    {
        _repository = repository;
    }


    [HttpGet] 
    public async Task<IActionResult> GetUsers()
    {
        var users = await _repository.GetAllAsync();
        if (users.Any())
            return Ok(users);
        else
            return NotFound();
    }


    [HttpGet("id")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user is null)
            return NotFound();
        else
            return Ok(new JsonResult(user));
    }
     

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody]User user)
    {
        var users = await _repository.GetAllAsync();
        if (users.Any(u => u.Id == user.Id))
            return BadRequest();
        else
        {
            await _repository.SaveAsync(user);
            return NoContent();
        }
    }


    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] User user)
    {
        var users = await _repository.GetAllAsync();

        if (users.Any(u => u.Id == user.Id))
        {
            await _repository.UpdateAsync(user);
            return NoContent();
        }
        else
        {
            return BadRequest(); 
        }
    }
     

    [HttpDelete("id")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user is null)
            return BadRequest();
        else
        {
            await _repository.DeleteAsync(user);
            return NoContent();
        }
    }
}
 