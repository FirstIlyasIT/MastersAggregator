using MastersAggregatorService.Controllers;
using MastersAggregatorService.Models;
using MastersAggregatorService.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using Moq;
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
        var objectResultFromGetAll = controller.GetAll() as ObjectResult;
        
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
        var objectResultFromGetById = controller.GetById(modelId) as ObjectResult;

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
        var objectResultFromGetByCondition = controller.GetByCondition(modelCondition) as ObjectResult;

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
        var result = controller.ChangeCondition(StaticData.testMaster);
        
        //Assert
        Assert.IsNotNull(result);
        
        Assert.That(result, Is.InstanceOf<OkResult>());
    }


}

public static class StaticData
{
    public static Master testMaster = new (0, "Name0", true);
    
    public static IEnumerable<Master> Masters = new[]
    {
        new Master(0, "Name0", true),
        new Master(1, "Name1", false),
        new Master(2, "Name2", true)
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