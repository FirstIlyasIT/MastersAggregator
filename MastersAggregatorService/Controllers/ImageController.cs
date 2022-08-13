using MastersAggregatorService.Models;
using MastersAggregatorService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MastersAggregatorService.Controllers;
[Route("[controller]")] 
public class ImageController : BaseController<Image>
{ 
    private ImageRepository _imageRepository { get; set; } 
    public ImageController(ImageRepository repository) : base(repository)
    {
        _imageRepository = repository;
    }
     
    // GET all image
    [HttpGet] 
    public ActionResult GetAll()
    { 
        return Ok(new JsonResult(_imageRepository.GetAll()));
    }
    // GET id image
    [HttpGet("{id}")]
    public ActionResult GetById(int id)
    {  
        if (_imageRepository.GetById(id) == null)
            return new JsonResult("Image does not exist");

        return Ok(new JsonResult(_imageRepository.GetById(id)));  
    }


}