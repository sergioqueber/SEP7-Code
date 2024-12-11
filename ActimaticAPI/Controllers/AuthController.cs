using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Model;
using Dto;

[ApiController]
[Route("[controller]")]
public class AuthController(IConfiguration config, IAuthService authService) : ControllerBase
{

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] UserLogInDTO user)
    {
        try
        {
            User userLog = await authService.ValidateUser(user.Id, user.Password);
            string token = GenerateJwt(userLog);

            return Ok(token);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    private string GenerateJwt(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(config["Jwt:Key"] ?? "");

        Console.WriteLine(user is User ? "User" : "Moderator");
        List<Claim> claims = GenerateClaims(user);
    Console.WriteLine("Claims Generated");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = config["Jwt:Issuer"],
            Audience = config["Jwt:Audience"]
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    private List<Claim> GenerateClaims(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, config["Jwt:Subject"] ?? ""),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("Id", user.Id.ToString()),
            new Claim("Name", user.Name),                                
            new Claim("Surname", user.Surname),                         
            new Claim("Email", user.Email),       
            new Claim("Role", user.Role),
            new Claim("IsApproved", user.IsApproved.ToString())
        };
        return [.. claims];
    }


}