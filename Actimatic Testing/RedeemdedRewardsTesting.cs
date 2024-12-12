using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Model;
using Interfaces;
using Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

public class RedeemedRewardsTestting
{
    private readonly Mock<IRedeemedRewardService> _mockService;
    private readonly RedeemedRewardController _controller;

    public RedeemedRewardsTestting()
    {
        _mockService = new Mock<IRedeemedRewardService>();
        _controller = new RedeemedRewardController(_mockService.Object);
    }

    [Fact]
    public async Task GetAllRedeemedRewards_ReturnsOkResult_WithRewards()
    {
        // Arrange
        var expectedRewards = new List<RedeemedReward>
        {
            new RedeemedReward { Id = 1, UserId = 1, RewardId = 1 },
            new RedeemedReward { Id = 2, UserId = 2, RewardId = 2 }
        };
        _mockService.Setup(s => s.GetAllRedeemedRewards())
            .ReturnsAsync(expectedRewards);

        // Act
        var result = await _controller.GetAllRedeemedRewards();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedRewards = Assert.IsAssignableFrom<IEnumerable<RedeemedReward>>(okResult.Value);
        Assert.Equal(2, returnedRewards.Count());
    }

    [Fact]
    public async Task GetRedeemedRewardById_ReturnsNotFound_WhenRewardDoesNotExist()
    {
        // Arrange
        _mockService.Setup(s => s.GetRedeemedRewardById(1))
            .ReturnsAsync((RedeemedReward)null);

        // Act
        var result = await _controller.GetRedeemedRewardById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetRedeemedRewardsByUserId_ReturnsOkResult_WithUserRewards()
    {
        // Arrange
        var userId = 1;
        var expectedRewards = new List<RedeemedReward>
        {
            new RedeemedReward { Id = 1, UserId = userId, RewardId = 1 },
            new RedeemedReward { Id = 2, UserId = userId, RewardId = 2 }
        };
        _mockService.Setup(s => s.GetRedeemedRewardsByUserId(userId))
            .ReturnsAsync(expectedRewards);

        // Act
        var result = await _controller.GetRedeemedRewardsByUserId(userId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedRewards = Assert.IsAssignableFrom<IEnumerable<RedeemedReward>>(okResult.Value);
        Assert.All(returnedRewards, reward => Assert.Equal(userId, reward.UserId));
    }

    [Fact]
    public async Task CreateRedeemedReward_ReturnsCreatedAtAction()
    {
        // Arrange
        var rewardToCreate = new RedeemedReward { UserId = 1, RewardId = 1 };
        var createdReward = new RedeemedReward { Id = 1, UserId = 1, RewardId = 1 };
        _mockService.Setup(s => s.CreateRedeemedReward(rewardToCreate))
            .ReturnsAsync(createdReward);

        // Act
        var result = await _controller.CreateRedeemedReward(rewardToCreate);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(nameof(RedeemedRewardController.GetRedeemedRewardById), createdAtActionResult.ActionName);
        var returnedReward = Assert.IsType<RedeemedReward>(createdAtActionResult.Value);
        Assert.Equal(1, returnedReward.Id);
    }

    [Fact]
    public async Task UpdateRedeemedReward_ReturnsBadRequest_WhenIdMismatch()
    {
        // Arrange
        var reward = new RedeemedReward { Id = 2, UserId = 1, RewardId = 1 };

        // Act
        var result = await _controller.UpdateRedeemedReward(1, reward);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task RemoveRedeemedReward_ReturnsNotFound_WhenRewardDoesNotExist()
    {
        // Arrange
        _mockService.Setup(s => s.RemoveRedeemedReward(1))
            .ReturnsAsync((RedeemedReward)null);

        // Act
        var result = await _controller.RemoveRedeemedReward(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    
}