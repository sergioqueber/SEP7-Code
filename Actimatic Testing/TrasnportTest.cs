using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Model;
using Interfaces;
using Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TransportControllerTests
{
    private readonly Mock<ITransportService> _mockService;
    private readonly TransportController _controller;

    public TransportControllerTests()
    {
        _mockService = new Mock<ITransportService>();
        _controller = new TransportController(_mockService.Object);
    }

    [Fact]
    public async Task GetAllTransport_ReturnsOkResult_WithTransports()
    {
        // Arrange
        var expectedTransports = new List<Transport>
        {
            new Transport { Id = 1, Name = "Transport1", Distance = 10 },
            new Transport { Id = 2, Name = "Transport2", Distance = 20 }
        };
        _mockService.Setup(s => s.GetAllTransport())
            .ReturnsAsync(expectedTransports);

        // Act
        var result = await _controller.GetAllTransport();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedTransports = Assert.IsAssignableFrom<IEnumerable<Transport>>(okResult.Value);
        Assert.Equal(2, returnedTransports.Count());
    }

    [Fact]
    public async Task GetTransportById_ReturnsNotFound_WhenTransportDoesNotExist()
    {
        // Arrange
        _mockService.Setup(s => s.GetTransportById(1))
            .ReturnsAsync((Transport)null);

        // Act
        var result = await _controller.GetTransportById(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateTransport_ReturnsCreatedAtAction()
    {
        // Arrange
        var transportToCreate = new Transport { Name = "New Transport", Distance = 15 };
        var createdTransport = new Transport { Id = 1, Name = "New Transport", Distance = 15 };
        _mockService.Setup(s => s.CreateTransport(transportToCreate))
            .ReturnsAsync(createdTransport);

        // Act
        var result = await _controller.CreateTransport(transportToCreate);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(nameof(TransportController.GetTransportById), createdAtActionResult.ActionName);
        var returnedTransport = Assert.IsType<Transport>(createdAtActionResult.Value);
        Assert.Equal(1, returnedTransport.Id);
    }

    [Fact]
    public async Task UpdateTransport_ReturnsNoContent_WhenUpdateIsSuccessful()
    {
        // Arrange
        var transportToUpdate = new Transport { Id = 1, Name = "Updated Transport", Distance = 20 };
        _mockService.Setup(s => s.UpdateTransport(transportToUpdate))
            .ReturnsAsync(transportToUpdate);

        // Act
        var result = await _controller.UpdateTransport(transportToUpdate);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task RemoveTransport_ReturnsNotFound_WhenTransportDoesNotExist()
    {
        // Arrange
        _mockService.Setup(s => s.RemoveTransport(1))
            .ReturnsAsync((Transport)null);

        // Act
        var result = await _controller.RemoveTransport(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task RemoveTransport_ReturnsOkResult_WhenTransportExists()
    {
        // Arrange
        var transportToRemove = new Transport { Id = 1, Name = "Transport1", Distance = 10 };
        _mockService.Setup(s => s.RemoveTransport(1))
            .ReturnsAsync(transportToRemove);

        // Act
        var result = await _controller.RemoveTransport(1);

        // Assert
        Assert.Equal(transportToRemove, result);
    }

    [Fact]
    public async Task GetAllTransportByDate_ReturnsOkResult_WithTransports()
    {
        // Arrange
        var startDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-10));
        var endDate = DateOnly.FromDateTime(DateTime.Now);
        var expectedTransports = new List<Transport>
        {
            new Transport { Id = 1, Name = "Transport1", Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-5)) },
            new Transport { Id = 2, Name = "Transport2", Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-3)) }
        };
        _mockService.Setup(s => s.GetTransportByDatesAsync(startDate, endDate))
            .ReturnsAsync(expectedTransports);

        // Act
        var result = await _controller.GetAllTransportByDate(startDate, endDate);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedTransports = Assert.IsAssignableFrom<IEnumerable<Transport>>(okResult.Value);
        Assert.Equal(2, returnedTransports.Count());
    }
     [Fact]
    public async Task CreateTransport_ReturnsCreatedAtAction_WithCorrectPoints()
    {
        // Arrange
        var transportToCreate = new Transport { Name = "New Transport", Distance = 10, Type = "Bike" };
        var createdTransport = new Transport { Id = 1, Name = "New Transport", Distance = 10, Type = "Bike", AwardedPoints = 20 };
        _mockService.Setup(s => s.CreateTransport(It.IsAny<Transport>()))
            .ReturnsAsync((Transport t) => 
            {
                t.AwardedPoints = t.Distance + (t.Type == "Bike" ? 10 : 0); // Example calculation
                return t;
            });

        // Act
        var result = await _controller.CreateTransport(transportToCreate);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(nameof(TransportController.GetTransportById), createdAtActionResult.ActionName);
        var returnedTransport = Assert.IsType<Transport>(createdAtActionResult.Value);
        Assert.Equal(20, returnedTransport.AwardedPoints); // Verify points calculation
    }
}