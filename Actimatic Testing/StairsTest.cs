using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Model;
using Interfaces;
using Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

public class StairsTest
{
    private readonly Mock<IStairsService> _mockService;
    private readonly StairsController _controller;

    public StairsTest()
    {
        _mockService = new Mock<IStairsService>();
        _controller = new StairsController(_mockService.Object);
    }

    [Fact]
    public async Task GetAllStairs_ReturnsOkResult_WithStairs()
    {
        // Arrange
        var expectedStairs = new List<Stairs>
        {
            new Stairs { Id = 1, Name = "Stairs1", Floors = 10 },
            new Stairs { Id = 2, Name = "Stairs2", Floors = 20 }
        };
        _mockService.Setup(s => s.GetAllStairsAsync())
            .ReturnsAsync(expectedStairs);

        // Act
        var result = await _controller.GetAllStairs();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedStairs = Assert.IsAssignableFrom<IEnumerable<Stairs>>(okResult.Value);
        Assert.Equal(2, returnedStairs.Count());
    }

    [Fact]
    public async Task GetStairsById_ReturnsNotFound_WhenStairsDoesNotExist()
    {
        // Arrange
        _mockService.Setup(s => s.GetStairsByIdAsync(1))
            .ReturnsAsync((Stairs)null);

        // Act
        var result = await _controller.GetStairsById(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateStairs_ReturnsCreatedAtAction()
    {
        // Arrange
        var stairsToCreate = new Stairs { Name = "New Stairs", Floors = 15 };
        var createdStairs = new Stairs { Id = 1, Name = "New Stairs", Floors = 15 };
        _mockService.Setup(s => s.CreateStairsAsync(stairsToCreate))
            .ReturnsAsync(createdStairs);

        // Act
        var result = await _controller.CreateStairs(stairsToCreate);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(nameof(StairsController.GetStairsById), createdAtActionResult.ActionName);
        var returnedStairs = Assert.IsType<Stairs>(createdAtActionResult.Value);
        Assert.Equal(1, returnedStairs.Id);
    }

    [Fact]
    public async Task UpdateStairs_ReturnsNoContent_WhenUpdateIsSuccessful()
    {
        // Arrange
        var stairsToUpdate = new Stairs { Id = 1, Name = "Updated Stairs", Floors = 20 };
        _mockService.Setup(s => s.UpdateStairsAsync(stairsToUpdate))
            .ReturnsAsync(stairsToUpdate);

        // Act
        var result = await _controller.UpdateStairs(stairsToUpdate);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task RemoveStairs_ReturnsNotFound_WhenStairsDoesNotExist()
    {
        // Arrange
        _mockService.Setup(s => s.RemoveStairsAsync(1))
            .ReturnsAsync((Stairs)null);

        // Act
        var result = await _controller.RemoveStairs(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task RemoveStairs_ReturnsOkResult_WhenStairsExists()
    {
        // Arrange
        var stairsToRemove = new Stairs { Id = 1, Name = "Stairs1", Floors = 10 };
        _mockService.Setup(s => s.RemoveStairsAsync(1))
            .ReturnsAsync(stairsToRemove);

        // Act
        var result = await _controller.RemoveStairs(1);

        // Assert
        Assert.Equal(stairsToRemove, result);
    }

    [Fact]
    public async Task GetAllStairsByDate_ReturnsOkResult_WithStairs()
    {
        // Arrange
        var startDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-10));
        var endDate = DateOnly.FromDateTime(DateTime.Now);
        var expectedStairs = new List<Stairs>
        {
            new Stairs { Id = 1, Name = "Stairs1", Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-5)) },
            new Stairs { Id = 2, Name = "Stairs2", Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-3)) }
        };
        _mockService.Setup(s => s.GetStairsByDatesAsync(startDate, endDate))
            .ReturnsAsync(expectedStairs);

        // Act
        var result = await _controller.GetAllStairsByDate(startDate, endDate);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedStairs = Assert.IsAssignableFrom<IEnumerable<Stairs>>(okResult.Value);
        Assert.Equal(2, returnedStairs.Count());
    }

    [Fact]
    public async Task CreateStairs_ReturnsCreatedAtAction_WithCorrectPoints()
    {
        // Arrange
        var stairsToCreate = new Stairs
        {
            Name = "New Stairs",
            Floors = 10
        };

        _mockService.Setup(s => s.CreateStairsAsync(It.IsAny<Stairs>()))
            .ReturnsAsync((Stairs s) =>
            {
                s.AwardedPoints = s.Floors * 5; // 5 points per floor
                return s;
            });

        // Act
        var result = await _controller.CreateStairs(stairsToCreate);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(nameof(StairsController.GetStairsById), createdAtActionResult.ActionName);
        var returnedStairs = Assert.IsType<Stairs>(createdAtActionResult.Value);
        Assert.Equal(50, returnedStairs.AwardedPoints); // 10 floors * 5 points
    }

    [Theory]
    [InlineData(5, 25)]   // 5 floors = 25 points
    [InlineData(10, 50)]  // 10 floors = 50 points
    [InlineData(20, 100)] // 20 floors = 100 points
    public async Task CreateStairs_CalculatesPoints_ForDifferentFloors(int floors, int expectedPoints)
    {
        // Arrange
        var stairsToCreate = new Stairs
        {
            Name = "Test Stairs",
            Floors = floors
        };

        _mockService.Setup(s => s.CreateStairsAsync(It.IsAny<Stairs>()))
            .ReturnsAsync((Stairs s) =>
            {
                s.AwardedPoints = s.Floors * 5;
                return s;
            });

        // Act
        var result = await _controller.CreateStairs(stairsToCreate);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnedStairs = Assert.IsType<Stairs>(createdAtActionResult.Value);
        Assert.Equal(expectedPoints, returnedStairs.AwardedPoints);
    }
}