using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WarehouseManager.BusinessLogic.Auth.Interfaces;
using WarehouseManager.BusinessLogic.Models;

namespace WarehouseManager.BusinessLogic.Auth;

public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
    private readonly JwtOptions _options = options.Value;

    public string GenerateToken(Employee employee)
    {
        Claim[] claims = [new Claim("employeeId", employee.Id.ToString()),
            new Claim("employeeEmail", employee.Email), new Claim("position", "Employee")
        ];
        
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)), SecurityAlgorithms.Sha256);

        var token = new JwtSecurityToken(signingCredentials: signingCredentials,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(_options.ExpiresHours));

        var value = new JwtSecurityTokenHandler().WriteToken(token);
        return value;
    }
    // write overload for GenerateToken(Boss boss)
    
}