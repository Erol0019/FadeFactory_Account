using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace FadeFactory_Accounts.Models;

public class Account
{
    [JsonProperty("accountId")]
    public string? AccountId { get; set; }

    [JsonProperty("firstName")]
    public string? FirstName { get; set; }

    [StringLength(450)]
    // [Index(IsUnique = true)]
    [JsonProperty("email")]
    public string? Email { get; set; }

    [JsonProperty("password")]
    public string? Password { get; set; }

    [JsonProperty("isPromotional")]
    public bool IsPromotional { get; set; }

    public override string ToString()
    {
        return $"AccountId: {AccountId}, FirstName: {FirstName}, Email: {Email}, Password: {Password}, IsPromotional: {IsPromotional}";
    }
}



