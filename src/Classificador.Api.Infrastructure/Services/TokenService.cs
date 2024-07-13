using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Classificador.Api.Application.Models.Options;
using Classificador.Api.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Classificador.Api.Infrastructure.Services;

public sealed class TokenService : ITokenService
{
    private readonly JwtOptions _options;

    public TokenService(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public JwtToken GenerateToken(IEnumerable<Claim> claims)
    {
        byte[] key = Encoding.ASCII.GetBytes(_options.TokenSecurityKey!);
        DateTime expiredDate = DateTime.UtcNow.AddMinutes(_options.TokenExpirationInMinutes);

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expiredDate,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        JwtSecurityTokenHandler tokenHandler = new();
        SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
        string jwt = tokenHandler.WriteToken(securityToken);

        JwtToken tokenModel = new()
        {
            Token = jwt, 
            ExpiredAtOnUtc = expiredDate
        };

        return tokenModel;
    }

    public JwtToken GenerateToken(Claim claim)
    {
        return GenerateToken([claim]);
    }
    public IEnumerable<Claim> GenerateClaims(User user)
    {
        Claim[] claims = 
        [
            new (ClaimTypes.NameIdentifier, user.Id.ToString()),
            new (ClaimTypes.Email, user.Email),
            new (ClaimTypes.Role, user.Role.ToString())
        ];

        return claims;
    }

}
