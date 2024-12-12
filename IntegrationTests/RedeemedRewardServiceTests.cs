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

public class RedeemedRewardsServiceTests
{
    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private readonly HttpClient _httpClient;
    private readonly RedeemedRewardsService _service;

    public RedeemedRewardsServiceTests()
    {
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_mockHttpMessageHandler.Object)
        {
            BaseAddress = new Uri("https://testapi.local/")
        };
        _service = new RedeemedRewardsService(_httpClient);
    }

    [Fact]
    public async Task GetAllRedeemedRewards_ShouldReturnAllRedeemedRewards()
    {
        // Arrange
        var date = new DateOnly(2024, 3, 1);
        var expectedData = new List<RedeemedReward>
        {
            new RedeemedReward { Id = 1, Date = date },
            new RedeemedReward { Id = 2, Date = date }
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
        var result = await _service.GetAllRedeemedRewards();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Collection(result,
            item => Assert.Equal(date, item.Date),
            item => Assert.Equal(date, item.Date));
    }

    [Fact]
    public async Task GetRedeemedRewardById_ShouldReturnSpecificRedeemedReward()
    {
        // Arrange
        var date = new DateOnly(2024, 3, 1);
        var expectedRedeemedReward = new RedeemedReward { Id = 1, Date = date };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = JsonContent.Create(expectedRedeemedReward)
            });

        // Act
        var result = await _service.GetRedeemedRewardById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(date, result.Date);
    }

    [Fact]
    public async Task CreateRedeemedReward_ShouldCreateAndReturnRedeemedReward()
    {
        // Arrange
        var date = new DateOnly(2024, 3, 1);
        var newRedeemedReward = new RedeemedReward { Id = 3, Date = date };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                Content = JsonContent.Create(newRedeemedReward)
            });

        // Act
        var result = await _service.CreateRedeemedReward(newRedeemedReward);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(date, result.Date);
    }

    [Fact]
    public async Task UpdateRedeemedReward_ShouldUpdateAndReturnRedeemedReward()
    {
        // Arrange
        var date = new DateOnly(2024, 3, 1);
        var updatedRedeemedReward = new RedeemedReward { Id = 1, Date = date };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = JsonContent.Create(updatedRedeemedReward)
            });

        // Act
        var result = await _service.UpdateRedeemedReward(updatedRedeemedReward);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(date, result.Date);
    }

    [Fact]
    public async Task RemoveRedeemedReward_ShouldRemoveAndReturnRedeemedReward()
    {
        // Arrange
        var date = new DateOnly(2024, 3, 1);
        var expectedRedeemedReward = new RedeemedReward { Id = 1, Date = date };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = JsonContent.Create(expectedRedeemedReward)
            });

        // Act
        var result = await _service.RemoveRedeemedReward(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(date, result.Date);
    }

    [Fact]
    public async Task GetRedeemedRewardsByUserId_ShouldReturnRedeemedRewardsForUser()
    {
        // Arrange
        var date = new DateOnly(2024, 3, 1);
        var expectedData = new List<RedeemedReward>
        {
            new RedeemedReward { Id = 1, Date = date, UserId = 1 },
            new RedeemedReward { Id = 2, Date = date, UserId = 1 }
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
        var result = await _service.GetRedeemedRewardsByUserId(1);

        // Assert
        Assert.NotNull(result);
        Assert.All(result, item =>
        {
            Assert.True(item.UserId == 1, "Item is outside the date range.");
        });
    }
}