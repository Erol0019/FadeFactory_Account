using Newtonsoft.Json;

namespace FadeFactory_Accounts.Models;

public class Account
{
    [JsonProperty("id")]
    public string? Id { get; set; }
    [JsonProperty("firstName")]
    public string? FirstName { get; set; }
    [JsonProperty("email")]
    public string? Email { get; set; }
    [JsonProperty("password")]
    public string? Password { get; set; }
    [JsonProperty("isPromotional")]
    public bool IsPromotional { get; set; }

    public override string ToString()
    {
        return $"Id: {Id}, FirstName: {FirstName}, Email: {Email}, Password: {Password}, IsPromotional: {IsPromotional}";
    }
}



