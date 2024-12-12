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

public class ActivitiesServiceTests
{
    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private readonly HttpClient _httpClient;
    private readonly ActivitiesService _service;

    public ActivitiesServiceTests()
    {
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_mockHttpMessageHandler.Object)
        {
            BaseAddress = new Uri("http://localhost:5205/")
        };
        _service = new ActivitiesService(_httpClient);
    }

    [Fact]
    public async Task GetAllTransport_ShouldReturnAllTransport()
    {
        // Arrange
        var expectedData = new List<Transport>
        {
            new Transport { Id = 1, Name = "Bus" },
            new Transport { Id = 2, Name = "Train" }
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
        var result = await _service.GetAllTransport();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Collection(result,
            item => Assert.Equal("Bus", item.Name),
            item => Assert.Equal("Train", item.Name));
    }

    [Fact]
    public async Task GetTransportById_ShouldReturnSpecificTransport()
    {
        // Arrange
        var expectedTransport = new Transport { Id = 1, Name = "Bus" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = JsonContent.Create(expectedTransport)
            });

        // Act
        var result = await _service.GetTransportById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Bus", result.Name);
    }

    [Fact]
    public async Task CreateTransport_ShouldCreateAndReturnTransport()
    {
        // Arrange
        var newTransport = new Transport { Id = 3, Name = "Bike" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                Content = JsonContent.Create(newTransport)
            });

        // Act
        var result = await _service.CreateTransport(newTransport);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Bike", result.Name);
    }

    [Fact]
    public async Task RemoveTransport_ShouldRemoveAndReturnTransport()
    {
        // Arrange
        var expectedTransport = new Transport { Id = 1, Name = "Bus" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = JsonContent.Create(expectedTransport)
            });

        // Act
        var result = await _service.RemoveTransport(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Bus", result.Name);
    }

    [Fact]
    public async Task UpdateTransport_ShouldUpdateAndReturnTransport()
    {
        // Arrange
        var updatedTransport = new Transport { Id = 1, Name = "Updated Bus" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = JsonContent.Create(updatedTransport)
            });

        // Act
        var result = await _service.UpdateTransport(updatedTransport);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated Bus", result.Name);
    }

    [Fact]
    public async Task GetTransportByDatesAsync_ShouldReturnFilteredTransport()
    {
        // Arrange
        var startDate = new DateOnly(2024, 3, 1);
        var endDate = new DateOnly(2024, 3, 31);
        var expectedData = new List<Transport>
    {
        new Transport { Id = 1, Name = "March Bus", Date = new DateOnly(2024, 3, 21) },
        new Transport { Id = 2, Name = "March Train", Date = new DateOnly(2024, 3, 25) }
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
        var result = await _service.GetTransportByDatesAsync(startDate, endDate);

        // Assert
        Assert.NotNull(result);
        Assert.All(result, item =>
        {
            Assert.True(item.Date >= startDate && item.Date <= endDate, "Item is outside the date range.");
        });
    }
    [Fact]
    public async Task GetAllCarPool_ShouldReturnAllCarPool()
    {
        // Arrange
        var expectedData = new List<CarPool>
        {
            new CarPool { Id = 1, Name = "Car Pool A" },
            new CarPool { Id = 2, Name = "Car Pool B" }
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
        var result = await _service.GetAllCarPool();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Collection(result,
            item => Assert.Equal("Car Pool A", item.Name),
            item => Assert.Equal("Car Pool B", item.Name));
    }

    [Fact]
    public async Task GetCarPoolById_ShouldReturnSpecificCarPool()
    {
        // Arrange
        var expectedCarPool = new CarPool { Id = 1, Name = "Car Pool A" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = JsonContent.Create(expectedCarPool)
            });

        // Act
        var result = await _service.GetCarPoolById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Car Pool A", result.Name);
    }

    [Fact]
    public async Task CreateCarPool_ShouldCreateAndReturnCarPool()
    {
        // Arrange
        var newCarPool = new CarPool { Id = 3, Name = "Car Pool C" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                Content = JsonContent.Create(newCarPool)
            });

        // Act
        var result = await _service.CreateCarPool(newCarPool);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Car Pool C", result.Name);
    }

    [Fact]
    public async Task RemoveCarPool_ShouldRemoveAndReturnCarPool()
    {
        // Arrange
        var expectedCarPool = new CarPool { Id = 1, Name = "Car Pool A" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = JsonContent.Create(expectedCarPool)
            });

        // Act
        var result = await _service.RemoveCarPool(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Car Pool A", result.Name);
    }

    [Fact]
    public async Task UpdateCarPool_ShouldUpdateAndReturnCarPool()
    {
        // Arrange
        var updatedCarPool = new CarPool { Id = 1, Name = "Updated Car Pool" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = JsonContent.Create(updatedCarPool)
            });

        // Act
        var result = await _service.UpdateCarPool(updatedCarPool);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated Car Pool", result.Name);
    }

    [Fact]
    public async Task GetCarPoolByDatesAsync_ShouldReturnFilteredCarPools()
    {
        // Arrange
        var startDate = new DateOnly(2024, 4, 1);
        var endDate = new DateOnly(2024, 4, 30);
        var expectedData = new List<CarPool>
        {
            new CarPool { Id = 1, Name = "April Car Pool A", Date = new DateOnly(2024, 4, 15) },
            new CarPool { Id = 2, Name = "April Car Pool B", Date = new DateOnly(2024, 4, 20) }
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
        var result = await _service.GetCarPoolByDatesAsync(startDate, endDate);

        // Assert
        Assert.NotNull(result);
        Assert.All(result, item =>
        {
            Assert.True(item.Date >= startDate && item.Date <= endDate, "Item is outside the date range.");
        });
    }
    [Fact]
    public async Task GetAllVolunteering_ShouldReturnAllVolunteering()
    {
        // Arrange
        var expectedData = new List<Volunteering>
        {
            new Volunteering { Id = 1, Name = "Volunteering A" },
            new Volunteering { Id = 2, Name = "Volunteering B" }
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
        var result = await _service.GetAllVolunteeringAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Collection(result,
            item => Assert.Equal("Volunteering A", item.Name),
            item => Assert.Equal("Volunteering B", item.Name));
    }

    [Fact]
    public async Task GetVolunteeringById_ShouldReturnSpecificVolunteering()
    {
        // Arrange
        var expectedVolunteering = new Volunteering { Id = 1, Name = "Volunteering A" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = JsonContent.Create(expectedVolunteering)
            });

        // Act
        var result = await _service.GetVolunteeringByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Volunteering A", result.Name);
    }

    [Fact]
    public async Task CreateVolunteering_ShouldCreateAndReturnVolunteering()
    {
        // Arrange
        var newVolunteering = new Volunteering { Id = 3, Name = "Volunteering C" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                Content = JsonContent.Create(newVolunteering)
            });

        // Act
        var result = await _service.CreateVolunteeringAsync(newVolunteering);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Volunteering C", result.Name);
    }

    [Fact]
    public async Task RemoveVolunteering_ShouldRemoveAndReturnVolunteering()
    {
        // Arrange
        var expectedVolunteering = new Volunteering { Id = 1, Name = "Volunteering A" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = JsonContent.Create(expectedVolunteering)
            });

        // Act
        var result = await _service.RemoveVolunteeringAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Volunteering A", result.Name);
    }

    [Fact]
    public async Task UpdateVolunteering_ShouldUpdateAndReturnVolunteering()
    {
        // Arrange
        var updatedVolunteering = new Volunteering { Id = 1, Name = "Updated Volunteering" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = JsonContent.Create(updatedVolunteering)
            });

        // Act
        var result = await _service.UpdateVolunteeringAsync(updatedVolunteering);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated Volunteering", result.Name);
    }

    [Fact]
    public async Task GetVolunteeringByDatesAsync_ShouldReturnFilteredVolunteering()
    {
        // Arrange
        var startDate = new DateOnly(2024, 5, 1);
        var endDate = new DateOnly(2024, 5, 31);
        var expectedData = new List<Volunteering>
        {
            new Volunteering { Id = 1, Name = "May Volunteering A", Date = new DateOnly(2024, 5, 15) },
            new Volunteering { Id = 2, Name = "May Volunteering B", Date = new DateOnly(2024, 5, 20) }
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
        var result = await _service.GetVolunteeringByDatesAsync(startDate, endDate);

        // Assert
        Assert.NotNull(result);
        Assert.All(result, item =>
        {
            Assert.True(item.Date >= startDate && item.Date <= endDate, "Item is outside the date range.");
        });
    }
    [Fact]
    public async Task GetAllSavingFood_ShouldReturnAllSavingFood()
    {
        // Arrange
        var expectedData = new List<SavingFood>
        {
            new SavingFood { Id = 1, Name = "Saving Food A" },
            new SavingFood { Id = 2, Name = "Saving Food B" }
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
        var result = await _service.GetAllSavingFoodAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Collection(result,
            item => Assert.Equal("Saving Food A", item.Name),
            item => Assert.Equal("Saving Food B", item.Name));
    }

    [Fact]
    public async Task GetSavingFoodById_ShouldReturnSpecificSavingFood()
    {
        // Arrange
        var expectedSavingFood = new SavingFood { Id = 1, Name = "Saving Food A" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = JsonContent.Create(expectedSavingFood)
            });

        // Act
        var result = await _service.GetSavingFoodByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Saving Food A", result.Name);
    }

    [Fact]
    public async Task CreateSavingFood_ShouldCreateAndReturnSavingFood()
    {
        // Arrange
        var newSavingFood = new SavingFood { Id = 3, Name = "Saving Food C" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                Content = JsonContent.Create(newSavingFood)
            });

        // Act
        var result = await _service.CreateSavingFoodAsync(newSavingFood);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Saving Food C", result.Name);
    }

    [Fact]
    public async Task RemoveSavingFood_ShouldRemoveAndReturnSavingFood()
    {
        // Arrange
        var expectedSavingFood = new SavingFood { Id = 1, Name = "Saving Food A" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = JsonContent.Create(expectedSavingFood)
            });

        // Act
        var result = await _service.RemoveSavingFoodAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Saving Food A", result.Name);
    }

    [Fact]
    public async Task UpdateSavingFood_ShouldUpdateAndReturnSavingFood()
    {
        // Arrange
        var updatedSavingFood = new SavingFood { Id = 1, Name = "Updated Saving Food" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = JsonContent.Create(updatedSavingFood)
            });

        // Act
        var result = await _service.UpdateSavingFoodAsync(updatedSavingFood);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated Saving Food", result.Name);
    }

    [Fact]
    public async Task GetSavingFoodByDatesAsync_ShouldReturnFilteredSavingFood()
    {
        // Arrange
        var startDate = new DateOnly(2024, 6, 1);
        var endDate = new DateOnly(2024, 6, 30);
        var expectedData = new List<SavingFood>
        {
            new SavingFood { Id = 1, Name = "June Saving Food A", Date = new DateOnly(2024, 6, 15) },
            new SavingFood { Id = 2, Name = "June Saving Food B", Date = new DateOnly(2024, 6, 20) }
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
        var result = await _service.GetSavingFoodByDatesAsync(startDate, endDate);

        // Assert
        Assert.NotNull(result);
        Assert.All(result, item =>
        {
            Assert.True(item.Date >= startDate && item.Date <= endDate, "Item is outside the date range.");
        });
    }
    [Fact]
    public async Task GetAllStairs_ShouldReturnAllStairs()
    {
        // Arrange
        var expectedData = new List<Stairs>
        {
            new Stairs { Id = 1, Name = "Stairs A" },
            new Stairs { Id = 2, Name = "Stairs B" }
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
        var result = await _service.GetAllStairsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Collection(result,
            item => Assert.Equal("Stairs A", item.Name),
            item => Assert.Equal("Stairs B", item.Name));
    }

    [Fact]
    public async Task GetStairsById_ShouldReturnSpecificStairs()
    {
        // Arrange
        var expectedStairs = new Stairs { Id = 1, Name = "Stairs A" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = JsonContent.Create(expectedStairs)
            });

        // Act
        var result = await _service.GetStairsByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Stairs A", result.Name);
    }

    [Fact]
    public async Task CreateStairs_ShouldCreateAndReturnStairs()
    {
        // Arrange
        var newStairs = new Stairs { Id = 3, Name = "Stairs C" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                Content = JsonContent.Create(newStairs)
            });

        // Act
        var result = await _service.CreateStairsAsync(newStairs);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Stairs C", result.Name);
    }

    [Fact]
    public async Task RemoveStairs_ShouldRemoveAndReturnStairs()
    {
        // Arrange
        var expectedStairs = new Stairs { Id = 1, Name = "Stairs A" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = JsonContent.Create(expectedStairs)
            });

        // Act
        var result = await _service.RemoveStairsAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Stairs A", result.Name);
    }

    [Fact]
    public async Task UpdateStairs_ShouldUpdateAndReturnStairs()
    {
        // Arrange
        var updatedStairs = new Stairs { Id = 1, Name = "Updated Stairs" };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = JsonContent.Create(updatedStairs)
            });

        // Act
        var result = await _service.UpdateStairsAsync(updatedStairs);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated Stairs", result.Name);
    }

    [Fact]
    public async Task GetStairsByDatesAsync_ShouldReturnFilteredStairs()
    {
        // Arrange
        var startDate = new DateOnly(2024, 7, 1);
        var endDate = new DateOnly(2024, 7, 31);
        var expectedData = new List<Stairs>
        {
            new Stairs { Id = 1, Name = "July Stairs A", Date = new DateOnly(2024, 7, 15) },
            new Stairs { Id = 2, Name = "July Stairs B", Date = new DateOnly(2024, 7, 20) }
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
        var result = await _service.GetStairsByDatesAsync(startDate, endDate);

        // Assert
        Assert.NotNull(result);
        Assert.All(result, item =>
        {
            Assert.True(item.Date >= startDate && item.Date <= endDate, "Item is outside the date range.");
        });
    }
}