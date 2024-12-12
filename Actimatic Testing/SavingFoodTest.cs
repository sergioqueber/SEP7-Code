using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Model;
using Interfaces;
using Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SavingFoodTest
{
    private readonly Mock<ISavingFoodService> _mockService;
    private readonly SavingFoodController _controller;

    public SavingFoodTest()
    {
        _mockService = new Mock<ISavingFoodService>();
        _controller = new SavingFoodController(_mockService.Object);
    }

    [Fact]
    public async Task GetAllSavingFood_ReturnsOkResult_WithSavingFoods()
    {
        // Arrange
        var expectedSavingFoods = new List<SavingFood>
        {
            new SavingFood { Id = 1, Name = "SavingFood1", PackagesSaved = 10 },
            new SavingFood { Id = 2, Name = "SavingFood2", PackagesSaved = 20 }
        };
        _mockService.Setup(s => s.GetAllSavingFoodAsync())
            .ReturnsAsync(expectedSavingFoods);

        // Act
        var result = await _controller.GetAllSavingFood();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedSavingFoods = Assert.IsAssignableFrom<IEnumerable<SavingFood>>(okResult.Value);
        Assert.Equal(2, returnedSavingFoods.Count());
    }

    [Fact]
    public async Task GetSavingFoodById_ReturnsNotFound_WhenSavingFoodDoesNotExist()
    {
        // Arrange
        _mockService.Setup(s => s.GetSavingFoodByIdAsync(1))
            .ReturnsAsync((SavingFood)null);

        // Act
        var result = await _controller.GetSavingFoodById(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateSavingFood_ReturnsCreatedAtAction()
    {
        // Arrange
        var savingFoodToCreate = new SavingFood { Name = "New SavingFood", PackagesSaved = 15 };
        var createdSavingFood = new SavingFood { Id = 1, Name = "New SavingFood", PackagesSaved = 15 };
        _mockService.Setup(s => s.CreateSavingFoodAsync(savingFoodToCreate))
            .ReturnsAsync(createdSavingFood);

        // Act
        var result = await _controller.CreateSavingFood(savingFoodToCreate);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(nameof(SavingFoodController.GetSavingFoodById), createdAtActionResult.ActionName);
        var returnedSavingFood = Assert.IsType<SavingFood>(createdAtActionResult.Value);
        Assert.Equal(1, returnedSavingFood.Id);
    }

    [Fact]
    public async Task UpdateSavingFood_ReturnsNoContent_WhenUpdateIsSuccessful()
    {
        // Arrange
        var savingFoodToUpdate = new SavingFood { Id = 1, Name = "Updated SavingFood", PackagesSaved = 20 };
        _mockService.Setup(s => s.UpdateSavingFoodAsync(savingFoodToUpdate))
            .ReturnsAsync(savingFoodToUpdate);

        // Act
        var result = await _controller.UpdateSavingFood(savingFoodToUpdate);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task RemoveSavingFood_ReturnsNotFound_WhenSavingFoodDoesNotExist()
    {
        // Arrange
        _mockService.Setup(s => s.RemoveSavingFoodAsync(1))
            .ReturnsAsync((SavingFood)null);

        // Act
        var result = await _controller.RemoveSavingFood(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task RemoveSavingFood_ReturnsOkResult_WhenSavingFoodExists()
    {
        // Arrange
        var savingFoodToRemove = new SavingFood { Id = 1, Name = "SavingFood1", PackagesSaved = 10 };
        _mockService.Setup(s => s.RemoveSavingFoodAsync(1))
            .ReturnsAsync(savingFoodToRemove);

        // Act
        var result = await _controller.RemoveSavingFood(1);

        // Assert
        Assert.Equal(savingFoodToRemove, result);
    }

    [Fact]
    public async Task GetAllSavingFoodByDate_ReturnsOkResult_WithSavingFoods()
    {
        // Arrange
        var startDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-10));
        var endDate = DateOnly.FromDateTime(DateTime.Now);
        var expectedSavingFoods = new List<SavingFood>
        {
            new SavingFood { Id = 1, Name = "SavingFood1", Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-5)) },
            new SavingFood { Id = 2, Name = "SavingFood2", Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-3)) }
        };
        _mockService.Setup(s => s.GetSavingFoodByDatesAsync(startDate, endDate))
            .ReturnsAsync(expectedSavingFoods);

        // Act
        var result = await _controller.GetAllSavingFoodByDate(startDate, endDate);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedSavingFoods = Assert.IsAssignableFrom<IEnumerable<SavingFood>>(okResult.Value);
        Assert.Equal(2, returnedSavingFoods.Count());
    }
}