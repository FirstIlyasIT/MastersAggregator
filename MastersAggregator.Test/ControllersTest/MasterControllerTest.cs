using MastersAggregatorService.Controllers;
using MastersAggregatorService.Models;
using MastersAggregatorService.Repositories;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace MastersAggregator.Test;

[TestFixture(TestOf = typeof(MasterController))]
public class MasterControllerTest
{
    [Test]
    public async Task GetAllTest()
    {
        // Arrange
        var repository = Substitute.For<MasterRepository>();
        
        repository.GetAll().Returns(StaticData.Masters);
        
        var controller = new MasterController(repository);
        
        // Act
        var objectResultFromGetAll = await controller.GetAll() as ObjectResult;
        
        var actualJson = objectResultFromGetAll.Value as JsonResult;

        var expectedJson = StaticData.ReturnExpectedActionResult() as JsonResult;
        
        // Assert
        Assert.That(actualJson, Is.Not.Null);
        
        Assert.That(objectResultFromGetAll.StatusCode, Is.EqualTo(200));

        Assert.That(actualJson.Value, Is.EqualTo(expectedJson.Value));
    }

    [Test]
    public async Task GetByIdTest()
    {
        // Arrange
        var repository = Substitute.For<MasterRepository>();

        var random = Random.Shared;

        var modelId = random.Next(0, StaticData.Masters.Count() - 1);
        
        repository.GetById(modelId).Returns(StaticData.Masters.FirstOrDefault(x => x.Id == modelId));
        
        var controller = new MasterController(repository);
        
        // Act
        var objectResultFromGetById = await controller.GetById(modelId) as ObjectResult;

        var actualJson = objectResultFromGetById.Value as JsonResult;

        var expectedJson = StaticData.ReturnExpectedActionResultById(modelId) as JsonResult;

        // Assert
        Assert.That(actualJson, Is.Not.Null);
        
        Assert.That(objectResultFromGetById.StatusCode, Is.EqualTo(200));

        Assert.That(actualJson.Value, Is.EqualTo(expectedJson.Value));
    }

    [Test]
    public async Task GetByConditionTest()
    {
        // Arrange
        var repository = Substitute.For<MasterRepository>();

        var modelCondition = false;

        repository.GetByCondition(modelCondition).Returns(StaticData.Masters.Where(x => x.IsActive == modelCondition));
        
        var controller = new MasterController(repository);
        
        //Act
        var objectResultFromGetByCondition = await controller.GetByCondition(modelCondition) as ObjectResult;

        var actualJson = objectResultFromGetByCondition.Value as JsonResult;

        var expectedJson = StaticData.ReturnExpectedActionResultByCondition(modelCondition) as JsonResult;
        
        
        //Assert
        Assert.That(actualJson, Is.Not.Null);
        
        Assert.That(objectResultFromGetByCondition.StatusCode, Is.EqualTo(200));

        Assert.That(actualJson.Value, Is.EqualTo(expectedJson.Value));
    }

    [Test]
    public async Task ChangeConditionOkResultTest()
    {
        // Arrange
        var repository = Substitute.For<MasterRepository>();
        
        repository.ChangeCondition(StaticData.testMaster)
            .Returns(StaticData.testMaster);
        
        var controller = new MasterController(repository);
        
        //Act
        var result = await controller.ChangeCondition(StaticData.testMaster);
        
        //Assert
        Assert.IsNotNull(result);
        
        Assert.That(result, Is.InstanceOf<OkResult>());
    }


}

public static class StaticData
{
    public static Master testMaster = new Master() {Id = 0, MastersName = "Name0", IsActive = true };
    
    public static IEnumerable<Master> Masters = new[]
    {
        new Master(){Id = 0, MastersName = "Name0", IsActive = true },
        new Master(){Id = 1, MastersName = "Name1", IsActive = false },
        new Master(){Id = 2, MastersName = "Name2", IsActive = true }
    };

    public static IActionResult ReturnExpectedActionResult()
    {
        return new JsonResult(Masters);
    }

    public static IActionResult ReturnExpectedActionResultById(int id)
    {
        return new JsonResult(Masters.FirstOrDefault(x => x.Id == id));
    }

    public static IActionResult ReturnExpectedActionResultByCondition(bool condition)
    {
        return new JsonResult(Masters.Where(x => x.IsActive == condition).ToList());
    }
}