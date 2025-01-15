using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class JwtService(IOptions<AuthSettings> options)
{
    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim("id", user.Id.ToString()),
            new Claim("username", user.Username),
            new Claim("email", user.Email),
            new Claim("role", user.Role!),
        };
        var jwtToken = new JwtSecurityToken(
            expires: DateTime.UtcNow.Add(options.Value.Expires),
            claims: claims,
            signingCredentials: 
            new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SecurityKey!)),
                SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }

    public Dictionary<string, string> Decode(string jwtToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(options.Value.SecurityKey!);
        var token = tokenHandler.ReadJwtToken(jwtToken);
        var claims = token.Claims.ToDictionary(c => c.Type, c => c.Value);
        return claims;
    }
}