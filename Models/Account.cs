using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace FadeFactory_Accounts.Models;

public class Account
{
    [Key, Required]
    [JsonProperty("accountId")]
    public int AccountId { get; set; }

    [JsonProperty("firstName")]
    [StringLength(255), Required]
    public string FirstName { get; set; }

    [StringLength(255), Required]
    [JsonProperty("email")]
    public string Email { get; set; }

    [StringLength(255), Required]
    [JsonProperty("password")]
    public string Password { get; set; }

    [Required]
    [JsonProperty("isPromotional")]
    public bool IsPromotional { get; set; }

    public override string ToString()
    {
        return $"AccountId: {AccountId}, FirstName: {FirstName}, Email: {Email}, Password: {Password}, IsPromotional: {IsPromotional}";
    }
}



