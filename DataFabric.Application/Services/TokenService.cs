using DataFabric.Domain.Entities;
using DataFabric.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class TokenService
{
    private readonly IConfiguration _config;
    private readonly IPersistenceRepository<User> _userRepository;

    public TokenService(IConfiguration config, IPersistenceRepository<User> userRepository)
    {
        _config = config;
        _userRepository = userRepository;
    }

    public string GenerateAccessToken(string email)
    {
        var user = _userRepository.FirstOrDefaultAsync(u => u.Email == email).Result;

        if (user == null)
            throw new ArgumentException($"User with email {email} not found");

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var bytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }

    public string GenerateResetToken()
    {
        return GenerateRefreshToken();
    }
}
