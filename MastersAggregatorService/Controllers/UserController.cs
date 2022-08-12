using MastersAggregatorService.Models;
using MastersAggregatorService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MastersAggregatorService.Controllers;
[Route("[controller]")]
public class UserController : BaseController<User>
{ 
    private UserRepository _userRepository { get; set; }
     
 //   public UserController(BaseRepository<User> repository) : base(repository) 
    public UserController(UserRepository repository) : base(repository)
    {
        _userRepository = repository;
    }

    // GET all user
    [HttpGet] 
    public JsonResult GetAll()
    { 
        return new JsonResult(_userRepository.GetAll());
    }
    // GET all id user
    [HttpGet("{id}")]
    public JsonResult GetById(int id)
    {  
        if (_userRepository.GetById(id) == null)
            return new JsonResult("user does not exist");

        return new JsonResult(_userRepository.GetById(id));  
    }

}