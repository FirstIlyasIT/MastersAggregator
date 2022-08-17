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

        [HttpGet("id")]
        public IActionResult GetUser(int id)
        {
            var user = _repository.GetById(id);
            if (user is null)
                return NotFound();
            else
                return Ok(new JsonResult(user));
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetUsers()
        {
            var users = _repository.GetAll();
            if (users.Any())
                return Ok(users);
            else
                return NotFound();
        }

        [HttpDelete("id")]
        public IActionResult DeleteUser(int id)
        {
            var user = _repository.GetById(id);
            if (user is null)
                return BadRequest();
            else
            {
                _repository.Delete(user);
                return NoContent();
            }
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            var users = _repository.GetAll();
            if (users.Contains(user))
                return BadRequest();
            else
            {
                _repository.Save(user);
                return NoContent();
            }
        }

    }
 