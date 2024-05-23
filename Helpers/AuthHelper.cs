

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FadeFactory_Accounts.Models;
using Microsoft.IdentityModel.Tokens;

namespace FadeFactory_Accounts.Helpers;

public class AuthHelper
{
    private readonly string jwtTokenSecret = Environment.GetEnvironmentVariable("TOKEN") ?? throw new ArgumentNullException();

    public string CreateToken(Account account)
    {
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.FirstName),
                new Claim(ClaimTypes.Email, account.Email)
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenSecret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.Now.AddDays(10),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }

    public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}