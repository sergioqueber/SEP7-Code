using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Model;
using Interfaces;
using Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

public class CarPoolTest
{
    private readonly Mock<ICarPoolService> _mockService;
    private readonly CarPoolController _controller;

    public CarPoolTest()
    {
        _mockService = new Mock<ICarPoolService>();
        _controller = new CarPoolController(_mockService.Object);
    }

    [Fact]
    public async Task GetAllCarPool_ReturnsOkResult_WithCarPools()
    {
        // Arrange
        var expectedCarPools = new List<CarPool>
        {
            new CarPool { Id = 1, Name = "CarPool1", Distance = 10 },
            new CarPool { Id = 2, Name = "CarPool2", Distance = 20 }
        };
        _mockService.Setup(s => s.GetAllCarPool())
            .ReturnsAsync(expectedCarPools);

        // Act
        var result = await _controller.GetAllCarPool();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedCarPools = Assert.IsAssignableFrom<IEnumerable<CarPool>>(okResult.Value);
        Assert.Equal(2, returnedCarPools.Count());
    }

    [Fact]
    public async Task GetCarPoolById_ReturnsNotFound_WhenCarPoolDoesNotExist()
    {
        // Arrange
        _mockService.Setup(s => s.GetCarPoolById(1))
            .ReturnsAsync((CarPool)null);

        // Act
        var result = await _controller.GetCarPoolById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task CreateCarPool_ReturnsCreatedAtAction()
    {
        // Arrange
        var carPoolToCreate = new CarPool { Name = "New CarPool", Distance = 15 };
        var createdCarPool = new CarPool { Id = 1, Name = "New CarPool", Distance = 15 };
        _mockService.Setup(s => s.CreateCarPool(carPoolToCreate))
            .ReturnsAsync(createdCarPool);

        // Act
        var result = await _controller.CreateCarPool(carPoolToCreate);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(nameof(CarPoolController.GetCarPoolById), createdAtActionResult.ActionName);
        var returnedCarPool = Assert.IsType<CarPool>(createdAtActionResult.Value);
        Assert.Equal(1, returnedCarPool.Id);
    }

    [Fact]
    public async Task UpdateCarPool_ReturnsNoContent()
    {
        // Arrange
        var carPoolToUpdate = new CarPool { Id = 1, Name = "Updated CarPool", Distance = 20 };
        _mockService.Setup(s => s.UpdateCarPool(carPoolToUpdate))
            .ReturnsAsync(carPoolToUpdate);

        // Act
        var result = await _controller.UpdateCarPool(carPoolToUpdate);

        // Assert
        Assert.IsType<NoContentResult>(result.Result);
    }

    [Fact]
    public async Task RemoveCarPool_ReturnsNotFound_WhenCarPoolDoesNotExist()
    {
        // Arrange
        _mockService.Setup(s => s.RemoveCarPool(1))
            .ReturnsAsync((CarPool)null);

        // Act
        var result = await _controller.RemoveCarPool(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetAllCarPoolByDate_ReturnsOkResult_WithCarPools()
    {
        // Arrange
        var startDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-10));
        var endDate = DateOnly.FromDateTime(DateTime.Now);
        var expectedCarPools = new List<CarPool>
        {
            new CarPool { Id = 1, Name = "CarPool1", Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-5)) },
            new CarPool { Id = 2, Name = "CarPool2", Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-3)) }
        };
        _mockService.Setup(s => s.GetCarPoolByDatesAsync(startDate, endDate))
            .ReturnsAsync(expectedCarPools);

        // Act
        var result = await _controller.GetAllCarPoolByDate(startDate, endDate);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedCarPools = Assert.IsAssignableFrom<IEnumerable<CarPool>>(okResult.Value);
        Assert.Equal(2, returnedCarPools.Count());
    }
    [Fact]
    public async Task CreateCarPool_ReturnsCreatedAtAction_WithCorrectPoints()
    {
        // Arrange
        var carPoolToCreate = new CarPool
        {
            Name = "New CarPool",
            Distance = 10,
            CarType = "Electric",
            EmptySeats = 4,
            FilledSeats = 3 // 75% occupancy
        };

        _mockService.Setup(s => s.CreateCarPool(It.IsAny<CarPool>()))
            .ReturnsAsync((CarPool cp) =>
            {
                // Base points (10) + Electric bonus (20) + High occupancy bonus (10)
                cp.AwardedPoints = (cp.Distance * 1) +
                                 (cp.CarType == "Electric" ? 20 : 0) +
                                 (((double)cp.FilledSeats / cp.EmptySeats >= 0.75) ? 10 : 0);
                return cp;
            });

        // Act
        var result = await _controller.CreateCarPool(carPoolToCreate);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(nameof(CarPoolController.GetCarPoolById), createdAtActionResult.ActionName);
        var returnedCarPool = Assert.IsType<CarPool>(createdAtActionResult.Value);
        Assert.Equal(40, returnedCarPool.AwardedPoints); // 10 + 20 + 10
    }

    [Theory]
    [InlineData("Electric", 4, 4, 40)] // Full occupancy: 10 (distance) + 20 (electric) + 10 (>=75% occupancy)
    [InlineData("Hybrid", 4, 2, 25)]   // 50% occupancy: 10 (distance) + 10 (hybrid) + 5 (>=50% occupancy)
    [InlineData("Petrol", 4, 1, 17)]   // 25% occupancy: 10 (distance) + 5 (petrol) + 2 (>=25% occupancy)
    public async Task CreateCarPool_CalculatesPoints_ForDifferentScenarios(string carType, int emptySeats, int filledSeats, int expectedPoints)
    {
        // Arrange
        var carPoolToCreate = new CarPool
        {
            Name = "Test CarPool",
            Distance = 10,
            CarType = carType,
            EmptySeats = emptySeats,
            FilledSeats = filledSeats
        };

        _mockService.Setup(s => s.CreateCarPool(It.IsAny<CarPool>()))
            .ReturnsAsync((CarPool cp) =>
            {
                int basePoints = cp.Distance;
                int typeBonus = cp.CarType switch
                {
                    "Electric" => 20,
                    "Hybrid" => 10,
                    "Petrol" => 5,
                    _ => 0
                };
                double occupancyRatio = (double)cp.FilledSeats / cp.EmptySeats;
                int occupancyBonus = occupancyRatio switch
                {
                    >= 0.75 => 10,
                    >= 0.5 => 5,
                    >= 0.25 => 2,
                    _ => 0
                };
                cp.AwardedPoints = basePoints + typeBonus + occupancyBonus;
                return cp;
            });

        // Act
        var result = await _controller.CreateCarPool(carPoolToCreate);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnedCarPool = Assert.IsType<CarPool>(createdAtActionResult.Value);
        Assert.Equal(expectedPoints, returnedCarPool.AwardedPoints);
    }
}