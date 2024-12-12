using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Xunit;
using AppServices;
using Model;

public class DepartmentServiceTests
{
    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private readonly HttpClient _httpClient;
    private readonly DepartmentService _service;

    public DepartmentServiceTests()
    {
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_mockHttpMessageHandler.Object)
        {
            BaseAddress = new Uri("https://testapi.local/")
        };
        _service = new DepartmentService(_httpClient);
    }

    [Fact]
    public async Task GetAllDepartments_ShouldReturnAllDepartments()
    {
        // Arrange
        var expectedData = new List<Department>
        {
            new Department { Id = 1, Name = "HR" },
            new Department { Id = 2, Name = "Finance" }
        };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = JsonContent.Create(expectedData)
            });

        // Act
        var result = await _service.GetAllDepartments();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Collection(result,
            item => Assert.Equal("HR", item.Name),
            item => Assert.Equal("Finance", item.Name));
    }

    [Fact]
    public async Task GetDepartmentById_ShouldReturnSpecificDepartment()
    {
        // Arrange
        var expectedDepartment = new Department { Id = 1, Name = "HR" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = JsonContent.Create(expectedDepartment)
            });

        // Act
        var result = await _service.GetDepartmentById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("HR", result.Name);
    }

    [Fact]
    public async Task Create_ShouldCreateAndReturnDepartment()
    {
        // Arrange
        var newDepartment = new Department { Id = 3, Name = "IT" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                Content = JsonContent.Create(newDepartment)
            });

        // Act
        var result = await _service.Create(newDepartment);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("IT", result.Name);
    }

    [Fact]
    public async Task Update_ShouldUpdateAndReturnDepartment()
    {
        // Arrange
        var updatedDepartment = new Department { Id = 1, Name = "Updated HR" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = JsonContent.Create(updatedDepartment)
            });

        // Act
        var result = await _service.Update(updatedDepartment);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated HR", result.Name);
    }

    [Fact]
    public async Task Remove_ShouldRemoveAndReturnDepartment()
    {
        // Arrange
        var expectedDepartment = new Department { Id = 1, Name = "HR" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = JsonContent.Create(expectedDepartment)
            });

        // Act
        var result = await _service.Remove(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("HR", result.Name);
    }
}
