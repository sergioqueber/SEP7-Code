using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model;
using Interfaces;
using Dto;
using System.Security.Claims;

public class AuthControllerTests
{
    private readonly Mock<IAuthService> _mockAuthService;
    private readonly Mock<IConfiguration> _mockConfig;
    private readonly AuthController _controller;

    public AuthControllerTests()
    {
        _mockAuthService = new Mock<IAuthService>();
        _mockConfig = new Mock<IConfiguration>();

        // Setup configuration mock
        _mockConfig.Setup(c => c["Jwt:Key"]).Returns("your-test-secret-key-with-minimum-length");
        _mockConfig.Setup(c => c["Jwt:Subject"]).Returns("test-subject");
        _mockConfig.Setup(c => c["Jwt:Issuer"]).Returns("test-issuer");
        _mockConfig.Setup(c => c["Jwt:Audience"]).Returns("test-audience");

        _controller = new AuthController(_mockConfig.Object, _mockAuthService.Object);
    }

    [Fact]
    public async Task Login_ReturnsOkResult_WhenCredentialsAreValid()
    {
        // Arrange
        var loginDto = new UserLogInDTO { Id = 1, Password = "password123" };
        var user = new User 
        { 
            Id = 1, 
            Name = "Test",
            Surname = "User",
            Email = "test@test.com",
            Password = "password123",
            Role = "Employee",
            IsApproved = true
        };

        _mockAuthService.Setup(s => s.ValidateUser(loginDto.Id, loginDto.Password))
            .ReturnsAsync(user);

        // Act
        var result = await _controller.Login(loginDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var token = Assert.IsType<string>(okResult.Value);
        Assert.NotEmpty(token);
    }

    [Fact]
    public async Task Login_ReturnsBadRequest_WhenUserNotFound()
    {
        // Arrange
        var loginDto = new UserLogInDTO { Id = 1, Password = "password123" };
        
        _mockAuthService.Setup(s => s.ValidateUser(loginDto.Id, loginDto.Password))
            .ThrowsAsync(new Exception("User not found"));

        // Act
        var result = await _controller.Login(loginDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("User not found", badRequestResult.Value);
    }

    [Fact]
    public async Task Login_ReturnsBadRequest_WhenPasswordIsWrong()
    {
        // Arrange
        var loginDto = new UserLogInDTO { Id = 1, Password = "wrongpassword" };
        
        _mockAuthService.Setup(s => s.ValidateUser(loginDto.Id, loginDto.Password))
            .ThrowsAsync(new Exception("Password mismatch"));

        // Act
        var result = await _controller.Login(loginDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Password mismatch", badRequestResult.Value);
    }

    [Fact]
    public async Task Login_GeneratesToken_WithCorrectClaims()
    {
        // Arrange
        var loginDto = new UserLogInDTO { Id = 1, Password = "password123" };
        var user = new User 
        { 
            Id = 1, 
            Name = "Test",
            Surname = "User",
            Email = "test@test.com",
            Password = "password123",
            Role = "Employee",
            IsApproved = true
        };

        _mockAuthService.Setup(s => s.ValidateUser(loginDto.Id, loginDto.Password))
            .ReturnsAsync(user);

        // Act
        var result = await _controller.Login(loginDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var token = Assert.IsType<string>(okResult.Value);

        // Verify token contains expected claims
        var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as System.IdentityModel.Tokens.Jwt.JwtSecurityToken;

        Assert.NotNull(jsonToken);
        Assert.Contains(jsonToken.Claims, c => c.Type == "Id" && c.Value == user.Id.ToString());
        Assert.Contains(jsonToken.Claims, c => c.Type == "Name" && c.Value == user.Name);
        Assert.Contains(jsonToken.Claims, c => c.Type == "Role" && c.Value == user.Role);
        Assert.Contains(jsonToken.Claims, c => c.Type == "IsApproved" && c.Value == user.IsApproved.ToString());
    }
}