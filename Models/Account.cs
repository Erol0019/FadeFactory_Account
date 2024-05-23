using FadeFactory_Accounts.Helpers;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FadeFactory_Accounts.Models;

public class Account : AccountParent
{
    [Required]
    [JsonProperty("passwordHash")]
    public byte[] PasswordHash { get; set; }

    [Required]
    [JsonProperty("passwordSalt")]
    public byte[] PasswordSalt { get; set; }
}

