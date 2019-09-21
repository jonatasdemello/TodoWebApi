using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using TodoWebApi.Repository;
using TodoWebApi.Controllers;
using TodoWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace XUnitTest
{
    public class ItemControllerTest
    {
        [Fact]
        public async Task Test1()
        {
            // Arrange
            var mockRepo = new Mock<ITodoRepository>();

            mockRepo.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<TodoItemModel>()
                { 
                    new TodoItemModel() { Id = 1, Title = "Test Task", Complete = false, Priority = 0 }
                });

            var controller = new ItemController(mockRepo.Object);

            // Act
            var result = await controller.Get();

            // Assert: that the method was called once
            mockRepo.Verify(x => x.GetAllAsync(), Times.Once);

            var viewResult = Assert.IsType<OkObjectResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<TodoItemModel>>(
                viewResult.Value);
        }

        // more
        // https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-2.2
        // https://github.com/aspnet/AspNetCore.Docs/tree/master/aspnetcore/mvc/controllers/testing/samples/2.x/TestingControllersSample/tests/TestingControllersSample.Tests/UnitTests
    }
}
