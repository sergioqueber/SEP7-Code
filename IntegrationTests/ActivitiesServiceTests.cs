using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Model;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;

public class ActivitiesServiceIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;

    public ActivitiesServiceIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _httpClient = factory.CreateClient(); // Creates an in-memory test server
    }

    [Fact]
    public async Task GetAllTransport_ShouldReturnAllTransports()
    {
        // Act
        var response = await _httpClient.GetAsync("api/transport");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var transports = await response.Content.ReadFromJsonAsync<List<Transport>>();
        transports.Should().NotBeNull();
        transports.Should().HaveCountGreaterThan(0); // Ensure at least one record exists
    }

    [Fact]
    public async Task CreateTransport_ShouldAddNewTransport()
    {
        // Arrange
        var newTransport = new Transport { Distance = 15, Type = "Bike" };

        // Act
        var postResponse = await _httpClient.PostAsJsonAsync("api/transport", newTransport);

        // Assert
        postResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var createdTransport = await postResponse.Content.ReadFromJsonAsync<Transport>();
        createdTransport.Should().NotBeNull();
        createdTransport.Mode.Should().Be("Bike");
    }

    [Fact]
    public async Task GetTransportById_ShouldReturnCorrectTransport()
    {
        // Arrange
        var transportId = 1;

        // Act
        var response = await _httpClient.GetAsync($"api/transport/{transportId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var transport = await response.Content.ReadFromJsonAsync<Transport>();
        transport.Should().NotBeNull();
        transport.Id.Should().Be(transportId);
    }

    [Fact]
    public async Task DeleteTransport_ShouldRemoveTransport()
    {
        // Arrange
        var transportId = 1;

        // Act
        var deleteResponse = await _httpClient.DeleteAsync($"api/transport/{transportId}");

        // Assert
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        // Verify deletion
        var getResponse = await _httpClient.GetAsync($"api/transport/{transportId}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}