using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Model;
using Interfaces;
using Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

public class VolunteeringTest
{
    private readonly Mock<IVolunteeringService> _mockService;
    private readonly VolunteeringController _controller;
    private const int BasePointsPerHour = 40;

    public VolunteeringTest()
    {
        _mockService = new Mock<IVolunteeringService>();
        _controller = new VolunteeringController(_mockService.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithVolunteering()
    {
        // Arrange
        var expectedVolunteering = new List<Volunteering>
        {
            new Volunteering { Id = 1, Name = "Volunteering1", Hours = 2 },
            new Volunteering { Id = 2, Name = "Volunteering2", Hours = 4 }
        };
        _mockService.Setup(s => s.GetAllVolunteeringAsync())
            .ReturnsAsync(expectedVolunteering);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedVolunteering = Assert.IsAssignableFrom<IEnumerable<Volunteering>>(okResult.Value);
        Assert.Equal(2, returnedVolunteering.Count());
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtAction_WithCorrectPoints()
    {
        // Arrange
        var volunteeringToCreate = new Volunteering { Name = "New Volunteering", Hours = 3, Location = "Location1" };
        var createdVolunteering = new Volunteering 
        { 
            Id = 1, 
            Name = "New Volunteering", 
            Hours = 3, 
            Location = "Location1",
            AwardedPoints = 3 * BasePointsPerHour // 120 points
        };

        _mockService.Setup(s => s.CreateVolunteeringAsync(It.IsAny<Volunteering>()))
            .ReturnsAsync((Volunteering v) => 
            {
                v.AwardedPoints = v.Hours * BasePointsPerHour;
                return v;
            });

        // Act
        var result = await _controller.Create(volunteeringToCreate);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(nameof(VolunteeringController.GetById), createdAtActionResult.ActionName);
        var returnedVolunteering = Assert.IsType<Volunteering>(createdAtActionResult.Value);
        Assert.Equal(120, returnedVolunteering.AwardedPoints);
    }

    [Fact]
    public async Task GetById_ReturnsVolunteering_WhenVolunteeringExists()
    {
        // Arrange
        var expectedVolunteering = new Volunteering { Id = 1, Name = "Volunteering1", Hours = 2 };
        _mockService.Setup(s => s.GetVolunteeringByIdAsync(1))
            .ReturnsAsync(expectedVolunteering);

        // Act
        var result = await _controller.GetById(1);

        // Assert
        Assert.Equal(expectedVolunteering.Id, result.Id);
    }

    [Fact]
    public async Task Update_ReturnsNoContent_WhenUpdateIsSuccessful()
    {
        // Arrange
        var volunteeringToUpdate = new Volunteering { Id = 1, Name = "Updated Volunteering", Hours = 4 };
        _mockService.Setup(s => s.UpdateVolunteeringAsync(volunteeringToUpdate))
            .ReturnsAsync(volunteeringToUpdate);

        // Act
        var result = await _controller.Update(volunteeringToUpdate);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsVolunteering_WhenVolunteeringExists()
    {
        // Arrange
        var volunteeringToDelete = new Volunteering { Id = 1, Name = "Volunteering1", Hours = 2 };
        _mockService.Setup(s => s.RemoveVolunteeringAsync(1))
            .ReturnsAsync(volunteeringToDelete);

        // Act
        var result = await _controller.Delete(1);

        // Assert
        Assert.Equal(volunteeringToDelete.Id, result.Id);
    }

    [Fact]
    public async Task GetAllVolunteeringByDate_ReturnsOkResult_WithVolunteering()
    {
        // Arrange
        var startDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-10));
        var endDate = DateOnly.FromDateTime(DateTime.Now);
        var expectedVolunteering = new List<Volunteering>
        {
            new Volunteering { Id = 1, Name = "Volunteering1", Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-5)) },
            new Volunteering { Id = 2, Name = "Volunteering2", Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-3)) }
        };
        _mockService.Setup(s => s.GetVolunteeringByDatesAsync(startDate, endDate))
            .ReturnsAsync(expectedVolunteering);

        // Act
        var result = await _controller.GetAllVolunteeringByDate(startDate, endDate);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedVolunteering = Assert.IsAssignableFrom<IEnumerable<Volunteering>>(okResult.Value);
        Assert.Equal(2, returnedVolunteering.Count());
    }
}