using Bunit;
using Moq;
using Xunit;
using Interfaces;
using Model;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using ActimaticWebApp.Components.Pages; // Replace 'YourNamespace' with the actual namespace of the Leaderboard component

public class LeaderboardTests : TestContext
{
    private Mock<IUserService> _mockUserService;
    private Mock<AuthenticationStateProvider> _mockAuthProvider;

    public LeaderboardTests()
    {
        _mockUserService = new Mock<IUserService>();
        _mockAuthProvider = new Mock<AuthenticationStateProvider>();

        // Register services
        Services.AddSingleton<IUserService>(_mockUserService.Object);
        Services.AddSingleton<AuthenticationStateProvider>(_mockAuthProvider.Object);
    }

    [Fact]
    public async Task Leaderboard_DisplaysTopUsers_Correctly()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Id = 1, Name = "Alice", Surname = "Smith", Points = 100 },
            new User { Id = 2, Name = "Bob", Surname = "Brown", Points = 90 },
            new User { Id = 3, Name = "Charlie", Surname = "Davis", Points = 80 },
            new User { Id = 4, Name = "David", Surname = "Evans", Points = 70 },
            new User { Id = 5, Name = "Eve", Surname = "Williams", Points = 60 }
        };

        var currentUser = new User { Id = 2, Name = "Bob", Surname = "Brown", Points = 90 };

        _mockUserService.Setup(s => s.GetAllUsers()).ReturnsAsync(users);
        var authState = new AuthenticationState(new ClaimsPrincipal(
            new ClaimsIdentity(new[] { new Claim("Id", currentUser.Id.ToString()) }, "TestAuth")));
        _mockAuthProvider.Setup(a => a.GetAuthenticationStateAsync()).ReturnsAsync(authState);

        // Act
        var component = RenderComponent<Leaderboard>();

        // Assert
        var rows = component.FindAll("tbody tr");
        Assert.Equal(5, rows.Count); // Total 5 users displayed

        // Verify ranking symbols for top three
        Assert.Contains("🥇", rows[0].InnerHtml);
        Assert.Contains("🥈", rows[1].InnerHtml);
        Assert.Contains("🥉", rows[2].InnerHtml);

        // Verify current user row is highlighted
        var highlightedRow = rows[1]; // Bob is rank 2
        Assert.Contains("table-primary", highlightedRow.ClassName);

        // Verify user data is correctly displayed
        Assert.Contains("Alice Smith", rows[0].InnerHtml);
        Assert.Contains("Bob Brown", rows[1].InnerHtml);
        Assert.Contains("100", rows[0].InnerHtml); // Points for Alice
        Assert.Contains("90", rows[1].InnerHtml);  // Points for Bob
    }

    [Fact]
    public async Task Leaderboard_DisplaysSeparatorAndSurroundingUsers_WhenCurrentUserIsNotTop()
    {
        // Arrange
        var users = Enumerable.Range(1, 23)
            .Select(i => new User { Id = i, Name = $"User{i}", Surname = $"Test{i}", Points = 100 - i })
            .ToList();

        var currentUser = new User { Id = 22, Name = "User19", Surname = "Test19", Points = 2 };

        _mockUserService.Setup(s => s.GetAllUsers()).ReturnsAsync(users);
        var authState = new AuthenticationState(new ClaimsPrincipal(
            new ClaimsIdentity(new[] { new Claim("Id", currentUser.Id.ToString()) }, "TestAuth")));
        _mockAuthProvider.Setup(a => a.GetAuthenticationStateAsync()).ReturnsAsync(authState);

        // Act
        var component = RenderComponent<Leaderboard>();

        // Assert
        var rows = component.FindAll("tbody tr");
        Assert.Equal(21, rows.Count); // Top 17 + separator + 3 surrounding current user

        // Verify separator is displayed
        //Assert.Contains("table-secondary", rows[18].ClassName);
        Assert.Contains(". . .", rows[17].InnerHtml);

        // Verify current user and surrounding ranks are displayed
        Assert.Contains("21", rows[18].InnerHtml); // Previous user
        Assert.Contains("22", rows[19].InnerHtml); // Current user
        Assert.Contains("23", rows[20].InnerHtml); // Next user
    }
}
