using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Model;
using Interfaces;
using Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

public class RewardControllerTests
{
    private readonly Mock<IRewardService> _mockRewardService;
    private readonly RewardController _controller;

    public RewardControllerTests()
    {
        _mockRewardService = new Mock<IRewardService>();
        _controller = new RewardController(_mockRewardService.Object);
    }

    [Fact]
    public async Task GetAllRewards_ReturnsOkResult_WithRewards()
    {
        // Arrange
        var expectedRewards = new List<Reward>
        {
            new Reward { Id = 1, Name = "Reward1", Description = "Test1", PointsRequired = 100 },
            new Reward { Id = 2, Name = "Reward2", Description = "Test2", PointsRequired = 200 }
        };
        _mockRewardService.Setup(service => service.GetAllRewards())
            .ReturnsAsync(expectedRewards);

        // Act
        var result = await _controller.GetAllRewards();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedRewards = Assert.IsAssignableFrom<IEnumerable<Reward>>(okResult.Value);
        Assert.Equal(2, returnedRewards.Count());
    }

    [Fact]
    public async Task GetRewardById_ReturnsNotFound_WhenRewardDoesNotExist()
    {
        // Arrange
        _mockRewardService.Setup(service => service.GetRewardById(1))
            .ReturnsAsync((Reward)null);

        // Act
        var result = await _controller.GetRewardById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task CreateReward_ReturnsCreatedAtAction()
    {
        // Arrange
        var rewardToCreate = new Reward { Name = "New Reward", PointsRequired = 100 };
        var createdReward = new Reward { Id = 1, Name = "New Reward", PointsRequired = 100 };
        _mockRewardService.Setup(service => service.CreateReward(rewardToCreate))
            .ReturnsAsync(createdReward);

        // Act
        var result = await _controller.CreateReward(rewardToCreate);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(nameof(RewardController.GetRewardById), createdAtActionResult.ActionName);
        var returnedReward = Assert.IsType<Reward>(createdAtActionResult.Value);
        Assert.Equal(1, returnedReward.Id);
    }

    [Fact]
    public async Task UpdateReward_ReturnsBadRequest_WhenIdMismatch()
    {
        // Arrange
        var reward = new Reward { Id = 2, Name = "Updated Reward" };

        // Act
        var result = await _controller.UpdateReward(1, reward);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task RemoveReward_ReturnsNotFound_WhenRewardDoesNotExist()
    {
        // Arrange
        _mockRewardService.Setup(service => service.RemoveReward(1))
            .ReturnsAsync((Reward)null);

        // Act
        var result = await _controller.RemoveReward(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}