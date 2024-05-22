using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace FadeFactory_Accounts.Models;

public class Account
{
    [Required]
    [Key]
    [JsonProperty("accountId")]
    public int AccountId { get; set; }

    [Required]
    [JsonProperty("firstName")]
    [StringLength(255)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(255)]
    //[Index(IsUnique = true)]
    [JsonProperty("email")]
    public string Email { get; set; }

    [Required]
    [StringLength(255)]
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



