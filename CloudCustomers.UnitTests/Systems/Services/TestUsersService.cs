using System.Net;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Moq.Protected;

namespace CloudCustomers.UnitTests.Systems.Services;

public class TestUsersService
{
    [Fact]
    public async Task GetUsers_WhenCalled_InvokesHttpGetRequest(){
        // Arrange
        var expectedResponse = UsersFixture.GetUsers();
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceListResponse(expectedResponse);
        var httpClientMock = new Mock<HttpClient>(handlerMock.Object);
        var systemUnderTest = new UsersService(httpClientMock.Object);
        // Act
        await systemUnderTest.GetUsers();
        // Assert
        // Verify that HTTP GET request was invoked and is a GET request
        handlerMock
            .Protected()
            .Verify(
                "SendAsync", 
                Times.Exactly(1), 
                ItExpr.Is<HttpRequestMessage>(
                    req => req.Method == HttpMethod.Get
                    ), 
                ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task GetUsers_WhenCalled_ReturnsListOfUsers()
    {
        // Arrange
        var expectedResponse = UsersFixture.GetUsers();
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceListResponse(expectedResponse);
        var httpClientMock = new Mock<HttpClient>(handlerMock.Object);
        var systemUnderTest = new UsersService(httpClientMock.Object);
        var users = UsersFixture.GetUsers();
        var usersCount = users.Count;
        // Act
        var result = await systemUnderTest.GetUsers();
        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(usersCount);
        result.Should().BeOfType<List<User>>();
    }

    [Fact]
    public async Task GetUsers_WhenCalled_ReturnsListOfUsersWithExpectedData()
    {
        // Arrange
        var expectedResponse = UsersFixture.GetUsers();
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceListResponse(expectedResponse);
        var httpClientMock = new Mock<HttpClient>(handlerMock.Object);
        var systemUnderTest = new UsersService(httpClientMock.Object);
        var users = UsersFixture.GetUsers();
        var firstUser = users.First();
        var userCount = users.Count;
        // Act
        var result = await systemUnderTest.GetUsers();
        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(userCount);
        if (result != null)
        {
            result.First().Should().BeOfType<User>();

            result.First().Name.Should().Be(firstUser.Name);
            result.First().Email.Should().Be(firstUser.Email);
            result.First().Phone.Should().Be(firstUser.Phone);
            result.First().Address.Should().Be(firstUser.Address);
            result.First().City.Should().Be(firstUser.City);
            result.First().State.Should().Be(firstUser.State);
            result.First().Zip.Should().Be(firstUser.Zip);
            result.First().Country.Should().Be(firstUser.Country);
            result.First().Password.Should().Be(firstUser.Password);
            result.First().ConfirmPassword.Should().Be(firstUser.ConfirmPassword);
        }
    }
    
    [Fact]
    public async Task GetUsers_WhenCalled_ReturnsNotFound_WhenResourceNotFound()
    {
        // Arrange
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicErrorResponse(HttpStatusCode.NotFound);
        var httpClientMock = new Mock<HttpClient>(handlerMock.Object);
        var systemUnderTest = new UsersService(httpClientMock.Object);
        // Act
        var result = await systemUnderTest.GetUsers();
        // Assert
        result.Should().BeNull();
    }
    
    [Fact]
    public async Task GetUsers_WhenCalled_ReturnsNull_WhenResourceNotFound()
    {
        // Arrange
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicErrorResponse(HttpStatusCode.NotFound);
        var httpClientMock = new Mock<HttpClient>(handlerMock.Object);
        var systemUnderTest = new UsersService(httpClientMock.Object);
        // Act
        var result = await systemUnderTest.GetUsers();
        // Assert
        result.Should().BeNull();
    }
    
    [Fact]
    public async Task GetUsers_WhenCalled_ReturnsListOfUsersOfExpectedSize()
    {
        // Arrange
        var expectedResponse = UsersFixture.GetUsers();
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceListResponse(expectedResponse);        var httpClientMock = new Mock<HttpClient>(handlerMock.Object);
        var systemUnderTest = new UsersService(httpClientMock.Object);
        // Act
        var result = await systemUnderTest.GetUsers();
        // Assert
        result.Should().HaveCount(expectedResponse.Count);
    }
}