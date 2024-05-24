using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FadeFactory_Accounts.Models;

public class Account : AccountParent
{
    [JsonProperty("passwordHash")]
    public byte[]? PasswordHash { get; set; }

    [JsonProperty("passwordSalt")]
    public byte[]? PasswordSalt { get; set; }
}

