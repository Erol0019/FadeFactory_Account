using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FadeFactory_Accounts.Models;

public class AccountParent
{
    [Key]
    [JsonProperty("accountId")]
    public int AccountId { get; set; }

    [JsonProperty("firstName")]
    [StringLength(255)]
    public string? FirstName { get; set; }

    [StringLength(255)]
    [JsonProperty("email")]
    public string? Email { get; set; }

    [JsonProperty("isPromotional")]
    [DefaultValue(false)]
    public bool IsPromotional { get; set; }

    [JsonProperty("IsAdmin")]
    [DefaultValue(false)]
    public bool IsAdmin { get; set; }
}

