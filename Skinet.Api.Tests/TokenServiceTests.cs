using System.IdentityModel.Tokens.Jwt;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Skinet.Api.Models;
using Skinet.Api.Services;
using Xunit;

namespace Skinet.Api.Tests;

public class TokenServiceTests
{
    private readonly TokenService _tokenService;

    public TokenServiceTests()
    {
        var configData = new Dictionary<string, string?>
        {
            ["JwtSettings:SecretKey"] = "your-super-secret-key-at-least-32-chars!!",
            ["JwtSettings:Issuer"] = "SkinetApi",
            ["JwtSettings:Audience"] = "SkinetClient",
            ["JwtSettings:ExpiresInMinutes"] = "60"
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(configData)
            .Build();

        _tokenService = new TokenService(configuration);
    }

    [Fact]
    public void CreateToken_Should_Return_A_Valid_Jwt()
    {
        var user = new User
        {
            Id = 1,
            Email = "test@example.com",
            Role = "Admin",
            PasswordHash = "hashed"
        };

        var token = _tokenService.CreateToken(user);

        token.Should().NotBeNullOrWhiteSpace();

        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);

        jwt.Claims.FirstOrDefault(c => c.Type.Contains("email"))?.Value.Should().Be("test@example.com");
        jwt.Claims.FirstOrDefault(c => c.Type.Contains("role"))?.Value.Should().Be("Admin");
    }

    [Fact]
    public void GenerateRefreshToken_Should_Return_NonEmpty_Random_String()
    {
        var token1 = _tokenService.GenerateRefreshToken();
        var token2 = _tokenService.GenerateRefreshToken();

        token1.Should().NotBeNullOrWhiteSpace();
        token2.Should().NotBeNullOrWhiteSpace();
        token1.Should().NotBe(token2);
    }
}
