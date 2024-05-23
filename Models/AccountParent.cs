using FadeFactory_Accounts.Helpers;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FadeFactory_Accounts.Models;

public class AccountParent
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

    [Required]
    [JsonProperty("isPromotional")]
    public bool IsPromotional { get; set; }
}

