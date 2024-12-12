using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Model;
using Interfaces;
using Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

public class DepartmentTest
{
    private readonly Mock<IDepartmentService> _mockService;
    private readonly DepartmentController _controller;

    public DepartmentTest()
    {
        _mockService = new Mock<IDepartmentService>();
        _controller = new DepartmentController(_mockService.Object);
    }

    [Fact]
    public async Task GetAllDepartment_ReturnsOkResult_WithDepartments()
    {
        // Arrange
        var expectedDepartments = new List<Department>
        {
            new Department { Id = 1, Name = "Department1" },
            new Department { Id = 2, Name = "Department2" }
        };
        _mockService.Setup(s => s.GetAllDepartments())
            .ReturnsAsync(expectedDepartments);

        // Act
        var result = await _controller.GetAllDepartment();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedDepartments = Assert.IsAssignableFrom<IEnumerable<Department>>(okResult.Value);
        Assert.Equal(2, returnedDepartments.Count());
    }

    [Fact]
    public async Task GetDepartmentById_ReturnsNotFound_WhenDepartmentDoesNotExist()
    {
        // Arrange
        _mockService.Setup(s => s.GetDepartmentById(1))
            .ReturnsAsync((Department)null);

        // Act
        var result = await _controller.GetDepartmentById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task CreateDepartment_ReturnsCreatedAtAction()
    {
        // Arrange
        var departmentToCreate = new Department { Name = "New Department" };
        var createdDepartment = new Department { Id = 1, Name = "New Department" };
        _mockService.Setup(s => s.Create(departmentToCreate))
            .ReturnsAsync(createdDepartment);

        // Act
        var result = await _controller.CreateDepartment(departmentToCreate);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(nameof(DepartmentController.GetDepartmentById), createdAtActionResult.ActionName);
        var returnedDepartment = Assert.IsType<Department>(createdAtActionResult.Value);
        Assert.Equal(1, returnedDepartment.Id);
    }

    [Fact]
    public async Task UpdateDepartment_ReturnsNoContent_WhenUpdateIsSuccessful()
    {
        // Arrange
        var departmentToUpdate = new Department { Id = 1, Name = "Updated Department" };
        _mockService.Setup(s => s.Update(departmentToUpdate))
            .ReturnsAsync(departmentToUpdate);

        // Act
        var result = await _controller.Update(departmentToUpdate.Id, departmentToUpdate);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task RemoveDepartment_ReturnsNotFound_WhenDepartmentDoesNotExist()
    {
        // Arrange
        _mockService.Setup(s => s.Remove(1))
            .ReturnsAsync((Department)null);

        // Act
        var result = await _controller.RemoveDepartment(1);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task RemoveDepartment_ReturnsOkResult_WhenDepartmentExists()
    {
        // Arrange
        var departmentToRemove = new Department { Id = 1, Name = "Department1" };
        _mockService.Setup(s => s.Remove(1))
            .ReturnsAsync(departmentToRemove);

        // Act
        var result = await _controller.RemoveDepartment(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedDepartment = Assert.IsType<Department>(okResult.Value);
        Assert.Equal(departmentToRemove.Id, returnedDepartment.Id);
    }
}