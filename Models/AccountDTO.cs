using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using FadeFactory_Accounts.Helpers;

namespace FadeFactory_Accounts.Models;

public class AccountDTO : AccountParent
{
    private readonly AuthHelper _authHelper = new AuthHelper();

    [Required]
    [JsonProperty("password")]
    public string Password { get; set; }

    public Account Adapt()
    {
        _authHelper.CreatePasswordHash(Password, out byte[] passwordHash, out byte[] passwordSalt);

        return new Account
        {
            AccountId = AccountId,
            FirstName = FirstName,
            Email = Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            IsPromotional = IsPromotional
        };
    }
}

