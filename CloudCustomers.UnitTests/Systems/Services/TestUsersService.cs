using System.Net;
using CloudCustomers.API.Config;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
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
        const string endpoint = "https://jsonplaceholder.typicode.com/users";
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        var systemUnderTest = new UsersService(httpClientMock.Object, config);
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
        const string endpoint = "https://jsonplaceholder.typicode.com/users";
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        var systemUnderTest = new UsersService(httpClientMock.Object, config);
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
        const string endpoint = "https://jsonplaceholder.typicode.com/users";
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        var systemUnderTest = new UsersService(httpClientMock.Object, config);
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
            result.First().Address.Should().BeEquivalentTo(firstUser.Address);
            result.First().Company.Should().BeEquivalentTo(firstUser.Company);
        }
    }
    
    [Fact]
    public async Task GetUsers_WhenCalled_ReturnsNotFound_WhenResourceNotFound()
    {
        // Arrange
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicErrorResponse(HttpStatusCode.NotFound);
        var httpClientMock = new Mock<HttpClient>(handlerMock.Object);
        const string endpoint = "https://jsonplaceholder.typicode.com/users";
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        var systemUnderTest = new UsersService(httpClientMock.Object, config);
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
        const string endpoint = "https://jsonplaceholder.typicode.com/users";
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        var systemUnderTest = new UsersService(httpClientMock.Object, config);
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
        const string endpoint = "https://jsonplaceholder.typicode.com/users";
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        var systemUnderTest = new UsersService(httpClientMock.Object, config);
        // Act
        var result = await systemUnderTest.GetUsers();
        // Assert
        result.Should().HaveCount(expectedResponse.Count);
    }
    
    [Fact]
    public async Task GetUsers_WhenCalled_InvokesConfiguredUrl()
    {
        // Arrange
        var expectedResponse = UsersFixture.GetUsers();
        const string endpoint = "https://jsonplaceholder.typicode.com/users";
        var handlerMock = MockHttpMessageHandler<User>
            .SetupBasicGetResourceListResponse(expectedResponse, endpoint);
        var httpClient = new HttpClient(handlerMock.Object);
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        var systemUnderTest = new UsersService(httpClient, config);
        // Act
        await systemUnderTest.GetUsers();
        // Assert
        handlerMock
            .Protected()
            .Verify(
                "SendAsync", 
                Times.Exactly(1), 
                ItExpr.Is<HttpRequestMessage>(
                    req => req.RequestUri == new Uri(config.Value.Endpoint) && req.Method == HttpMethod.Get
                ), 
                ItExpr.IsAny<CancellationToken>());
    }

}