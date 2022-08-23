using MastersAggregatorService.Models;
using MastersAggregatorService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MastersAggregatorService.Controllers;


[ApiController]
//[Route("{controller}")]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
[Consumes("application/json")]
public class ImageController : BaseController<Image> 
{ 
    private ImageRepository _repository { get; set; }
    public ImageController(ImageRepository repository)
    {
        _repository = repository;
    }


    [HttpGet("id")]
    public IActionResult GetImage(int id)
    {
        Image? image = _repository.GetById(id);
        if (image is null)
            return NotFound();
        else
            return Ok(new JsonResult(image));
    }
    
    [HttpGet]
    [Route("all")]
    public IActionResult GetImages()
    {
        var images = _repository.GetAll();
        if (images.Any())
            return Ok(images);
        else
            return NotFound();
    }

    [HttpDelete("id")]
    public IActionResult DeleteImage(int id)
    {
        var image = _repository.GetById(id);
        if (image is null)
            return BadRequest();
        else
        {
            _repository.Delete(image);
            return NoContent();
        }
    }

    [HttpPost]
    public IActionResult CreateImage([FromBody] Image image)
    {
        var images = _repository.GetAll(); 
        //���� image � ����� id ���������� �� �� ������� ��� ��������� BadRequest()
        if (images.Any(s => s.Id == image.Id))
            return BadRequest();

        _repository.Save(image);
        return NoContent();
    }
    [HttpPut]
    public IActionResult UpdateImage([FromBody] Image image)
    {
        var Images = _repository.GetAll();

        foreach (var ImageTemp in Images)
        {
            if (ImageTemp.Id == image.Id)
            {
                DeleteImage(image.Id);
                _repository.Save(image);
                return NoContent();
            }
        }
        return BadRequest();
    }
}