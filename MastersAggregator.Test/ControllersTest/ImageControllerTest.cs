using MastersAggregatorService.Controllers;
using MastersAggregatorService.Models;
using MastersAggregatorService.Repositories;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;

namespace MastersAggregator.Test.ControllersTest;

public class ImageControllerTest
{ 
    [Test]
    public void Get_Image_Ok_Result_Test()
    {
        // Arrange
        var repository = Substitute.For<ImageRepository>();
        repository.GetById(15).Returns(StaticDataImage.TestImage1);
        var controller = new ImageController(repository);
        // Act
        var result = controller.GetImage(15);
        // Assert
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public void Get_Image_Bad_Result_Test()
    {
        // Arrange
        var repository = Substitute.For<ImageRepository>();
        repository.GetById(15).Returns(StaticDataImage.TestImage1);
        var controller = new ImageController(repository);
        // Act
        var result = controller.GetImage(100);
        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }

 
    [Test]
    public void Get_Images_Ok_Result_Test()
    {
        // Arrange
        var repository = Substitute.For<ImageRepository>();
        repository.GetAll().Returns(StaticDataImage.Images);
        var controller = new ImageController(repository);
        // Act
        var result = controller.GetImages();
        var expectedGetUsers = StaticData.Users;
        // Assert
        Assert.That((result as ObjectResult).StatusCode, Is.EqualTo(200)); 
    }

    [Test]
    public void Delete_Image_Ok_Result_Test()
    {
        // Arrange
        var repository = Substitute.For<ImageRepository>();
        repository.GetById(15).Returns(StaticDataImage.TestImage1);
        var controller = new ImageController(repository);
        // Act
        var result = controller.DeleteImage(15);
        // Assert
        Assert.That((result as StatusCodeResult).StatusCode, Is.EqualTo(204)); 
    }

    [Test]
    public void Delete_Image_Bad_Result_Test()
    {
        // Arrange
        var repository = Substitute.For<ImageRepository>();
        repository.GetById(15).Returns(StaticDataImage.TestImage1);
        var controller = new ImageController(repository);
        // Act
        var result = controller.DeleteImage(150);
        // Assert
        Assert.That((result as StatusCodeResult).StatusCode, Is.EqualTo(400));
    }

    [Test]
    public void Create_Image_Ok_Result_Test()
    {
        // Arrange
        var repository = Substitute.For<ImageRepository>();
        repository.GetAll().Returns(StaticDataImage.Images);
        var controller = new ImageController(repository);
        // Act
        var result = controller.CreateImage(StaticDataImage.TestImage1);
        // Assert
        Assert.That((result as StatusCodeResult).StatusCode, Is.EqualTo(204));
    }

    [Test]
    public void Create_User_Image_Result_Test()
    {
        // Arrange
        var repository = Substitute.For<ImageRepository>();
        repository.GetAll().Returns(StaticDataImage.Images);
        var controller = new ImageController(repository);
        // Act
        var result = controller.CreateImage(StaticDataImage.TestImage2);
        // Assert
        Assert.That((result as StatusCodeResult).StatusCode, Is.EqualTo(400));
    }

    [Test]
    public void Update_Image_Ok_Result_Test()
    {
        // Arrange
        var repository = Substitute.For<ImageRepository>();
        repository.GetAll().Returns(StaticDataImage.Images);
        var controller = new ImageController(repository);
        // Act
        var result = controller.UpdateImage(StaticDataImage.TestImage2);
        // Assert
        Assert.That((result as StatusCodeResult).StatusCode, Is.EqualTo(204));
    }

    [Test]
    public void Update_Image_Bad_Result_Test()
    {
        // Arrange
        var repository = Substitute.For<ImageRepository>();
        repository.GetAll().Returns(StaticDataImage.Images);
        var controller = new ImageController(repository);
        // Act
        var result = controller.UpdateImage(StaticDataImage.TestImage1);
        // Assert
        Assert.That((result as StatusCodeResult).StatusCode, Is.EqualTo(400));
    }

}

 
public static class StaticDataImage
{
    //TestUser1 - уникальный юзер нет в Users
    public static Image TestImage1 = new Image { Id = 15, ImageUrl = "https://my-domen.com/conten/images/21515.ipg", ImageDescription = "описание работы: перекос крыши вид с перекосившегося окна" };
    //TestUser2 - есть в списке Users
    public static Image TestImage2 = new Image { Id = 2, ImageUrl = "https://my-domen.com/conten/images/21326.ipg", ImageDescription = "описание работы: перекос окна вид с другой стороны" };

    public static List<Image> Images = new List<Image>
        {
            new Image { Id = 0, ImageUrl = "https://my-domen.com/conten/images/21324.ipg", ImageDescription = "описание работы: необходимо починить дверной замок на фото показана поломка - сломался ключ" },
            new Image { Id = 1, ImageUrl = "https://my-domen.com/conten/images/21325.ipg", ImageDescription = "описание работы: у меня не закрываеться окно на фото видно проблему" },
            new Image { Id = 2, ImageUrl = "https://my-domen.com/conten/images/21326.ipg", ImageDescription = "описание работы: перекос окна вид с другой стороны" }
        };

}
