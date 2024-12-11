using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Model;
using Interfaces;
using Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

public class UserControllerTests
{
    private readonly Mock<IUserService> _mockUserService;
    private readonly UserController _controller;

    public UserControllerTests()
    {
        _mockUserService = new Mock<IUserService>();
        _controller = new UserController(_mockUserService.Object);
    }

    [Fact]
    public async Task GetAllUser_ReturnsOkResult_WithUsers()
    {
        // Arrange
        var expectedUsers = new List<User>
        {
            new User { Id = 1, Name = "John", Surname = "Doe" },
            new User { Id = 2, Name = "Jane", Surname = "Smith" }
        };
        _mockUserService.Setup(service => service.GetAllUsers())
            .ReturnsAsync(expectedUsers);

        // Act
        var result = await _controller.GetAllUser();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedUsers = Assert.IsAssignableFrom<IEnumerable<User>>(okResult.Value);
        Assert.Equal(2, returnedUsers.Count());
    }

    [Fact]
    public async Task GetUserById_ReturnsUser_WhenUserExists()
    {
        // Arrange
        var expectedUser = new User { Id = 1, Name = "John", Surname = "Doe" };
        _mockUserService.Setup(service => service.GetUserById(1))
            .ReturnsAsync(expectedUser);

        // Act
        var result = await _controller.GetUserById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedUser.Id, result.Id);
    }

    [Fact]
    public async Task CreateUser_ReturnsCreatedAtAction()
    {
        // Arrange
        var userToCreate = new User { Name = "John", Surname = "Doe" };
        var createdUser = new User { Id = 1, Name = "John", Surname = "Doe" };
        _mockUserService.Setup(service => service.CreateUser(userToCreate))
            .ReturnsAsync(createdUser);

        // Act
        var result = await _controller.CreateUser(userToCreate);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(nameof(UserController.GetUserById), createdAtActionResult.ActionName);
        var returnedUser = Assert.IsType<User>(createdAtActionResult.Value);
        Assert.Equal(1, returnedUser.Id);
    }

    [Fact]
    public async Task UpdateUser_ReturnsOkResult()
    {
        // Arrange
        var userToUpdate = new User { Id = 1, Name = "John Updated", Surname = "Doe" };
        _mockUserService.Setup(service => service.UpdateUser(userToUpdate))
            .ReturnsAsync(userToUpdate);

        // Act
        var result = await _controller.UpdateUser(userToUpdate);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedUser = Assert.IsType<User>(okResult.Value);
        Assert.Equal(userToUpdate.Name, returnedUser.Name);
    }

    [Fact]
    public async Task RemoveUser_ReturnsUser_WhenUserExists()
    {
        // Arrange
        var userToDelete = new User { Id = 1, Name = "John", Surname = "Doe" };
        _mockUserService.Setup(service => service.RemoveUser(1))
            .ReturnsAsync(userToDelete);

        // Act
        var result = await _controller.RemoveUser(1);

        // Assert
        Assert.Equal(userToDelete, result.Value);
    }
}