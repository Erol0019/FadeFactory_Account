using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FadeFactory_Accounts.Models;
using FadeFactory_Accounts.Data;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace FadeFactory_Accounts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        public static AccountRegister accountRegister = new AccountRegister();

        [HttpPost("register")]
        public async Task<ActionResult<AccountRegister>> Register(Account request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            accountRegister.FirstName = request.FirstName;
            accountRegister.PasswordHash = passwordHash;
            accountRegister.PasswordSalt = passwordSalt;

            return Ok(accountRegister);

        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(Account request)
        {
            if (accountRegister.FirstName != request.FirstName)
            {
                return BadRequest("Invalid Username");
            }

            if (!VerifyPasswordHash(request.Password, accountRegister.PasswordHash, accountRegister.PasswordSalt))
            {
                return BadRequest("Invalid Password");
            }

            string token = CreateToken(accountRegister);
            return Ok(token);
        }

        private string CreateToken(AccountRegister accountRegister)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, accountRegister.FirstName)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes());

            return "token";
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}