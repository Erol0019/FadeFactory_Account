using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using FadeFactory_Accounts.Helpers;

namespace FadeFactory_Accounts.Models;

public class AccountDTO
{
    private readonly AuthHelper _authHelper = new AuthHelper();

    [Key, Required]
    [JsonProperty("accountId")]
    public int AccountId { get; set; }

    [JsonProperty("firstName")]
    [StringLength(255), Required]
    public string FirstName { get; set; }

    [StringLength(255), Required]
    [JsonProperty("email")]
    public string Email { get; set; }

    [Required]
    [JsonProperty("password")]
    public string Password { get; set; }

    [Required]
    [JsonProperty("isPromotional")]
    public bool IsPromotional { get; set; }

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

    public override string ToString()
    {
        return $"AccountId: {AccountId}, FirstName: {FirstName}, Email: {Email}, IsPromotional: {IsPromotional}";
    }
}

