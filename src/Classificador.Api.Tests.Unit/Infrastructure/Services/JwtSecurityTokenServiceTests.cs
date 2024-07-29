namespace Classificador.Api.Tests.Unit.Infrastructure.Services;

public sealed class JwtSecurityTokenServiceTests
{
    private readonly Mock<IOptions<JwtOptions>> _mockOptions;
    private readonly JwtSecurityTokenService _jwtSecurityTokenService;

    public JwtSecurityTokenServiceTests()
    {
        _mockOptions = new Mock<IOptions<JwtOptions>>();
        _mockOptions.Setup(o => o.Value).Returns(new JwtOptions
        {
            TokenSecurityKey = "2RHWbxgZqS5fuU5ylEZFGUOBYqT/gktGkZINGfFmAA0=",
            TokenExpirationInMinutes = 30
        });

        _jwtSecurityTokenService = new JwtSecurityTokenService(_mockOptions.Object);
    }

    [Fact]
    public void GenerateToken_ShouldReturnJwtToken_WithExpectedClaims()
    {
        // Arrange
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, "1"),
            new(ClaimTypes.Email, "test@example.com")
        };

        // Act
        var token = _jwtSecurityTokenService.GenerateToken(claims);

        // Assert
        Assert.NotNull(token);
        Assert.False(string.IsNullOrEmpty(token.Token));
        Assert.True(token.ExpiredAtOnUtc > DateTime.UtcNow);
    }

    [Fact]
    public void GenerateToken_SingleClaim_ShouldReturnJwtToken_WithExpectedClaims()
    {
        // Arrange
        var claim = new Claim(ClaimTypes.NameIdentifier, "1");

        // Act
        var token = _jwtSecurityTokenService.GenerateToken(claim);

        // Assert
        Assert.NotNull(token);
        Assert.False(string.IsNullOrEmpty(token.Token));
        Assert.True(token.ExpiredAtOnUtc > DateTime.UtcNow);
    }

    [Fact]
    public void GenerateClaims_ShouldReturnClaims_ForGivenUser()
    {
        // Arrange
        User user = new("test@example.com", "Password1@", "Test User", UserRole.Padrao, Guid.NewGuid(), string.Empty);

        // Act
        var claims = _jwtSecurityTokenService.GenerateClaims(user);

        // Assert
        Assert.NotNull(claims);
        Assert.Contains(claims, c => c.Type == ClaimTypes.Name && c.Value == user.Name);
        Assert.Contains(claims, c => c.Type == ClaimTypes.Email && c.Value == user.Email);
        Assert.Contains(claims, c => c.Type == ClaimTypes.Role && c.Value == user.Role.ToString());
    }
}
