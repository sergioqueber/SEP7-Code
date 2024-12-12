using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Model;
using Interfaces;
using Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TeamTest
{
    private readonly Mock<ITeamService> _mockService;
    private readonly TeamController _controller;

    public TeamTest()
    {
        _mockService = new Mock<ITeamService>();
        _controller = new TeamController(_mockService.Object);
    }

    [Fact]
    public async Task GetAllTeams_ReturnsOkResult_WithTeams()
    {
        // Arrange
        var expectedTeams = new List<Team>
        {
            new Team { Id = 1, Name = "Team1" },
            new Team { Id = 2, Name = "Team2" }
        };
        _mockService.Setup(s => s.GetAllTeams())
            .ReturnsAsync(expectedTeams);

        // Act
        var result = await _controller.GetAllTeams();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedTeams = Assert.IsAssignableFrom<IEnumerable<Team>>(okResult.Value);
        Assert.Equal(2, returnedTeams.Count());
    }

    [Fact]
    public async Task GetTeamById_ReturnsNotFound_WhenTeamDoesNotExist()
    {
        // Arrange
        _mockService.Setup(s => s.GetTeamById(1))
            .ReturnsAsync((Team)null);

        // Act
        var result = await _controller.GetTeamById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task CreateTeam_ReturnsCreatedAtAction()
    {
        // Arrange
        var teamToCreate = new Team { Name = "New Team" };
        var createdTeam = new Team { Id = 1, Name = "New Team" };
        _mockService.Setup(s => s.CreateTeam(teamToCreate))
            .ReturnsAsync(createdTeam);

        // Act
        var result = await _controller.CreateTeam(teamToCreate);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(nameof(TeamController.GetTeamById), createdAtActionResult.ActionName);
        var returnedTeam = Assert.IsType<Team>(createdAtActionResult.Value);
        Assert.Equal(1, returnedTeam.Id);
    }

    [Fact]
    public async Task UpdateTeam_ReturnsBadRequest_WhenIdMismatch()
    {
        // Arrange
        var team = new Team { Id = 2, Name = "Updated Team" };

        // Act
        var result = await _controller.UpdateTeam(1, team);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task UpdateTeam_ReturnsNotFound_WhenTeamDoesNotExist()
    {
        // Arrange
        var team = new Team { Id = 1, Name = "Updated Team" };
        _mockService.Setup(s => s.UpdateTeam(team))
            .ReturnsAsync((Team)null);

        // Act
        var result = await _controller.UpdateTeam(1, team);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task UpdateTeam_ReturnsOkResult_WhenUpdateIsSuccessful()
    {
        // Arrange
        var team = new Team { Id = 1, Name = "Updated Team" };
        _mockService.Setup(s => s.UpdateTeam(team))
            .ReturnsAsync(team);

        // Act
        var result = await _controller.UpdateTeam(1, team);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedTeam = Assert.IsType<Team>(okResult.Value);
        Assert.Equal(team.Id, returnedTeam.Id);
    }

    [Fact]
    public async Task RemoveTeam_ReturnsNotFound_WhenTeamDoesNotExist()
    {
        // Arrange
        _mockService.Setup(s => s.RemoveTeam(1))
            .ReturnsAsync((Team)null);

        // Act
        var result = await _controller.RemoveTeam(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task RemoveTeam_ReturnsOkResult_WhenTeamExists()
    {
        // Arrange
        var teamToRemove = new Team { Id = 1, Name = "Team1" };
        _mockService.Setup(s => s.RemoveTeam(1))
            .ReturnsAsync(teamToRemove);

        // Act
        var result = await _controller.RemoveTeam(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedTeam = Assert.IsType<Team>(okResult.Value);
        Assert.Equal(teamToRemove.Id, returnedTeam.Id);
    }
}