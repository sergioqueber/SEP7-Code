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

public class RewardServiceTests
{
    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private readonly HttpClient _httpClient;
    private readonly RewardService _service;

    public RewardServiceTests()
    {
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_mockHttpMessageHandler.Object)
        {
            BaseAddress = new Uri("https://testapi.local/")
        };
        _service = new RewardService(_httpClient);
    }

    [Fact]
    public async Task GetAllRewards_ShouldReturnAllRewards()
    {
        // Arrange
        var expectedData = new List<Reward>
        {
            new Reward { Id = 1, Name = "Reward A" },
            new Reward { Id = 2, Name = "Reward B" }
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
        var result = await _service.GetAllRewards();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Collection(result,
            item => Assert.Equal("Reward A", item.Name),
            item => Assert.Equal("Reward B", item.Name));
    }

    [Fact]
    public async Task GetRewardById_ShouldReturnSpecificReward()
    {
        // Arrange
        var expectedReward = new Reward { Id = 1, Name = "Reward A" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = JsonContent.Create(expectedReward)
            });

        // Act
        var result = await _service.GetRewardById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Reward A", result.Name);
    }

    [Fact]
    public async Task CreateReward_ShouldCreateAndReturnReward()
    {
        // Arrange
        var newReward = new Reward { Id = 3, Name = "Reward C" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                Content = JsonContent.Create(newReward)
            });

        // Act
        var result = await _service.CreateReward(newReward);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Reward C", result.Name);
    }

    [Fact]
    public async Task UpdateReward_ShouldUpdateAndReturnReward()
    {
        // Arrange
        var updatedReward = new Reward { Id = 1, Name = "Updated Reward" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = JsonContent.Create(updatedReward)
            });

        // Act
        var result = await _service.UpdateReward(updatedReward);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated Reward", result.Name);
    }

    [Fact]
    public async Task RemoveReward_ShouldRemoveAndReturnReward()
    {
        // Arrange
        var expectedReward = new Reward { Id = 1, Name = "Reward A" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = JsonContent.Create(expectedReward)
            });

        // Act
        var result = await _service.RemoveReward(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Reward A", result.Name);
    }
}