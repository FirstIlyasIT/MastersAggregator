using MastersAggregatorService.Controllers;
using MastersAggregatorService.Models;
using MastersAggregatorService.Repositories;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;

namespace MastersAggregator.Test.ControllersTest;

[TestFixture(TestOf = typeof(UserController))]
public class UserControllerTest
{
    [Test]
    public void GetByIdOkResultTest()
    {
        var repository = Substitute.For<UserRepository>();
        repository.GetById(15).Returns(StaticData.TestUser1);
        var controller = new UserController(repository);
        var result = controller.GetUser(15);
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }
    
    [Test]
    public void GetByIdBadResultTest()
    {
        var repository = Substitute.For<UserRepository>();
        repository.GetById(0).ReturnsNullForAnyArgs();
        var controller = new UserController(repository);
        var result = controller.GetUser(76);
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}

public static class StaticData
{
    public static User TestUser1 = new User {Id = 15};
}