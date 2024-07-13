namespace Classificador.Api.Domain.Interfaces.Services;

public interface ITokenService
{
    public JwtToken GenerateToken(IEnumerable<Claim> claims);
    public JwtToken GenerateToken(Claim claim);
    public IEnumerable<Claim> GenerateClaims(User user);
}
