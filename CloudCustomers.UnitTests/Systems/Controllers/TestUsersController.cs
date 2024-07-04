

using CloudCustomers.API.Controllers;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CloudCustomers.UnitTests.Systems.Controllers;

public class TestUsersController
{
    [Fact]
    public async Task GetUsers_OnSuccess_ReturnsStatusCode200()
    {
        // Arrange
        var userServiceMock = new Mock<IUsersService>();
        var systemUnderTest = new UsersController(Mock.Of<ILogger<UsersController>>(), userServiceMock.Object);
        // Act
        var result = await systemUnderTest.GetUsers() as OkObjectResult;
        // Assert
        result?.StatusCode.Should().Be(StatusCodes.Status200OK);
    }

    [Fact]
    public async Task GetUsers_OnSuccess_InvokesUsersServiceExactlyOnce()
    {
        // Arrange
        var userServiceMock = new Mock<IUsersService>();
        userServiceMock.Setup(service => service.GetUsers()).ReturnsAsync(new List<User>());
        var systemUnderTest = new UsersController(Mock.Of<ILogger<UsersController>>(), userServiceMock.Object);
        // Act
        await systemUnderTest.GetUsers();
        // Assert
        userServiceMock.Verify(service => service.GetUsers(), Times.Once);
    }
    
    [Theory]
    [InlineData(1, 2)]
    [InlineData(2, 3)]
    public void Test2(int a, int b)
    {
        Assert.Equal(a + 1, b);
    }
}