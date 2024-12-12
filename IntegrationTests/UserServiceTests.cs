using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppServices;
using Model;
using Moq;
using Moq.Protected;
using Xunit;
using FluentAssertions;

public class UserServiceTests
{
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly HttpClient _httpClient;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri("http://localhost:5205/")
        };
        _userService = new UserService(_httpClient);
    }

    [Fact]
    public async Task GetAllUsers_ReturnsListOfUsers()
    {
        // Arrange
        var expectedUsers = new List<User>
        {
            new User { Id = 1, Name = "John Doe", TeamId = 101 },
            new User { Id = 2, Name = "Jane Smith", TeamId = 102 }
        };

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(expectedUsers)
            });

        // Act
        var users = await _userService.GetAllUsers();

        // Assert
        users.Should().NotBeNull();
        users.Should().HaveCount(2);
        users.Should().BeEquivalentTo(expectedUsers);
    }

    [Fact]
    public async Task GetUserById_ReturnsCorrectUser()
    {
        // Arrange
        var expectedUser = new User { Id = 1, Name = "John Doe", TeamId = 101 };

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.RequestUri.ToString().EndsWith("api/user/1")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(expectedUser)
            });

        // Act
        var user = await _userService.GetUserById(1);

        // Assert
        user.Should().NotBeNull();
        user.Should().BeEquivalentTo(expectedUser);
    }

    [Fact]
    public async Task CreateUser_ReturnsCreatedUser()
    {
        // Arrange
        var newUser = new User { Name = "New User", TeamId = 103 };
        var createdUser = new User { Id = 3, Name = "New User", TeamId = 103 };

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Created,
                Content = JsonContent.Create(createdUser)
            });

        // Act
        var user = await _userService.CreateUser(newUser);

        // Assert
        user.Should().NotBeNull();
        user.Should().BeEquivalentTo(createdUser);
    }

    [Fact]
    public async Task RemoveUser_ReturnsRemovedUser()
    {
        // Arrange
        var removedUser = new User { Id = 1, Name = "John Doe", TeamId = 101 };

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Delete),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(removedUser)
            });

        // Act
        var user = await _userService.RemoveUser(1);

        // Assert
        user.Should().NotBeNull();
        user.Should().BeEquivalentTo(removedUser);
    }

    [Fact]
    public async Task UpdateUser_ReturnsUpdatedUser()
    {
        // Arrange
        var updatedUser = new User { Id = 1, Name = "Updated User", TeamId = 101 };

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Put),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(updatedUser)
            });

        // Act
        var user = await _userService.UpdateUser(updatedUser);

        // Assert
        user.Should().NotBeNull();
        user.Should().BeEquivalentTo(updatedUser);
    }
}