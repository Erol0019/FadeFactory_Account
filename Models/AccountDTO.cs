using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using FadeFactory_Accounts.Helpers;

namespace FadeFactory_Accounts.Models;

public class AccountDTO : AccountParent
{
    private readonly AuthHelper _authHelper = new AuthHelper();

    [JsonProperty("password")]
    public string? Password { get; set; }

    public Account Adapt()
    {
        byte[]? passwordHash, passwordSalt;
        if (Password != null)
        {
            _authHelper.CreatePasswordHash(Password, out passwordHash, out passwordSalt);
        }
        else
        {
            passwordHash = null;
            passwordSalt = null;
        }

        return new Account
        {
            AccountId = AccountId,
            FirstName = FirstName,
            Email = Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            IsPromotional = IsPromotional,
            IsAdmin = IsAdmin
        };
    }
}

