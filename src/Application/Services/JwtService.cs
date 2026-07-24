using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public record CreateJwtTokenRequest(
    string Subject,
    Guid AppId,
    string Audience,
    int ExpiresInMinutes,
    IReadOnlyList<string> Scopes,
    IReadOnlyDictionary<string, string>? Claims = null);

public interface IJwtService
{
    string CreateToken(CreateJwtTokenRequest request);
}

public class JwtService(IOptions<JwtOptions> jwtOptions) : IJwtService
{
    private readonly JwtOptions _options = jwtOptions.Value;

    public string CreateToken(CreateJwtTokenRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Subject))
        {
            throw new ArgumentException("Subject is required.", nameof(request));
        }

        if (string.IsNullOrWhiteSpace(request.Audience))
        {
            throw new ArgumentException("Audience is required.", nameof(request));
        }

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, request.Subject),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new("app_id", request.AppId.ToString())
        };

        if (request.Scopes.Count > 0)
        {
            claims.Add(new Claim("scope", string.Join(' ', request.Scopes)));
        }

        if (request.Claims is not null)
        {
            claims.AddRange(request.Claims.Select(c => new Claim(c.Key, c.Value)));
        }

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: request.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(request.ExpiresInMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
