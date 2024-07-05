using CloudCustomers.API.Controllers;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
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
        var users = UsersFixture.GetUsers();
        userServiceMock.Setup(service => service.GetUsers()).ReturnsAsync(users);
        var systemUnderTest = new UsersController(Mock.Of<ILogger<UsersController>>(), userServiceMock.Object);
        // Act
        var result = await systemUnderTest.GetUsers() as OkObjectResult;
        // Assert
        result.Should().NotBeNull();
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
    
    [Fact]
    public async Task GetUsers_OnSuccess_ReturnsListOfUsersWithExpectedData()
    {
        // Arrange
        var userServiceMock = new Mock<IUsersService>();
        var users = UsersFixture.GetUsers();
        var firstUser = users.First();
        var userCount = users.Count;
        userServiceMock.Setup(service => service.GetUsers()).ReturnsAsync(users);
        var systemUnderTest = new UsersController(Mock.Of<ILogger<UsersController>>(), userServiceMock.Object);
        // Act
        var result = await systemUnderTest.GetUsers();
        // Assert
        result?.Should().BeOfType<OkObjectResult>();
        
        var objectResult = result as OkObjectResult;
        objectResult?.Value.Should().BeOfType<List<User>>();
        
        var usersResult = objectResult?.Value as List<User>;
        usersResult?.Should().HaveCount(userCount);
        
        usersResult?.First().Name.Should().Be(firstUser.Name);
        usersResult?.First().Email.Should().Be(firstUser.Email);
        usersResult?.First().Phone.Should().Be(firstUser.Phone);
        usersResult?.First().Address.Should().Be(firstUser.Address);
        usersResult?.First().City.Should().Be(firstUser.City);
        usersResult?.First().State.Should().Be(firstUser.State);
        usersResult?.First().Zip.Should().Be(firstUser.Zip);
        usersResult?.First().Country.Should().Be(firstUser.Country);
        usersResult?.First().Password.Should().Be(firstUser.Password);
        usersResult?.First().ConfirmPassword.Should().Be(firstUser.ConfirmPassword);
    }

    [Fact]
    public async Task GetUsers_OnNoUsersFound_ReturnsStatusCode404()
    {
        // Arrange
        var userServiceMock = new Mock<IUsersService>();
        userServiceMock.Setup(service => service.GetUsers()).ReturnsAsync(new List<User>());
        var systemUnderTest = new UsersController(Mock.Of<ILogger<UsersController>>(), userServiceMock.Object);
        // Act
        var result = await systemUnderTest.GetUsers() as NotFoundResult;
        // Assert
        result?.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
    
    
    
    // [Theory]
    // [InlineData(1, 2)]
    // [InlineData(2, 3)]
    // public void Test2(int a, int b)
    // {
    //     Assert.Equal(a + 1, b);
    // }
}