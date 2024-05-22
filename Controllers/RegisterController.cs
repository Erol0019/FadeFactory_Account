using Microsoft.AspNetCore.Mvc;
using FadeFactory_Accounts.Models;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace FadeFactory_Accounts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly string _tokenKey;
        public static AccountRegister accountRegister = new AccountRegister();

        public RegisterController()
        {
            _tokenKey = Environment.GetEnvironmentVariable("TOKEN");
            if (string.IsNullOrEmpty(_tokenKey))
            {
                throw new InvalidOperationException("Token key is not configured.");
            }
        }



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
                return BadRequest("Invalid account");
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
                new Claim(ClaimTypes.Name, accountRegister.FirstName),
                new Claim(ClaimTypes.Role, "NormalAccount")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(10),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        [HttpGet("{account}")]
        public ActionResult<AccountRegister> GetUser(string account)
        {
            if (accountRegister.FirstName == account)
            {
                return Ok(accountRegister);
            }
            return NotFound("User not found");
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
