using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Xunit;
using AppServices;
using Model;

public class TeamServiceTests
{
    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private readonly HttpClient _httpClient;
    private readonly TeamService _service;

    public TeamServiceTests()
    {
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_mockHttpMessageHandler.Object)
        {
            BaseAddress = new Uri("https://testapi.local/")
        };
        _service = new TeamService(_httpClient);
    }

    [Fact]
    public async Task GetAllTeams_ShouldReturnAllTeams()
    {
        // Arrange
        var expectedData = new List<Team>
        {
            new Team { Id = 1, Name = "Team A" },
            new Team { Id = 2, Name = "Team B" }
        };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = JsonContent.Create(expectedData)
            });

        // Act
        var result = await _service.GetAllTeams();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Collection(result,
            item => Assert.Equal("Team A", item.Name),
            item => Assert.Equal("Team B", item.Name));
    }

    [Fact]
    public async Task GetTeamById_ShouldReturnSpecificTeam()
    {
        // Arrange
        var expectedTeam = new Team { Id = 1, Name = "Team A" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = JsonContent.Create(expectedTeam)
            });

        // Act
        var result = await _service.GetTeamById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Team A", result.Name);
    }

    [Fact]
    public async Task CreateTeam_ShouldCreateAndReturnTeam()
    {
        // Arrange
        var newTeam = new Team { Id = 3, Name = "Team C" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                Content = JsonContent.Create(newTeam)
            });

        // Act
        var result = await _service.CreateTeam(newTeam);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Team C", result.Name);
    }

    [Fact]
    public async Task UpdateTeam_ShouldUpdateAndReturnTeam()
    {
        // Arrange
        var updatedTeam = new Team { Id = 1, Name = "Updated Team A" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = JsonContent.Create(updatedTeam)
            });

        // Act
        var result = await _service.UpdateTeam(updatedTeam);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated Team A", result.Name);
    }

    [Fact]
    public async Task RemoveTeam_ShouldRemoveAndReturnTeam()
    {
        // Arrange
        var expectedTeam = new Team { Id = 1, Name = "Team A" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = JsonContent.Create(expectedTeam)
            });

        // Act
        var result = await _service.RemoveTeam(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Team A", result.Name);
    }
}
