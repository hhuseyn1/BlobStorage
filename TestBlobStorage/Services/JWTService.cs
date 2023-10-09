using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestBlobStorage.Models;

namespace TestBlobStorage.Services;

public static class JWTService
{
    public static string GenerateSecurityToken(User user)
    {
        List<Claim> Claims = new()
    {
        new Claim(ClaimTypes.Name, user.Name)
    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("hrfujeidwkopjifgrhuefojpihugfwjdphiegfwjdkpgbfeuohfioehfioheihfioehfiw"));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: Claims,
            signingCredentials: creds,
            expires: DateTime.Now.AddSeconds(40)
            );

        var JWT = new JwtSecurityTokenHandler().WriteToken(token);
        return JWT;
    }

}
