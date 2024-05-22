using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FadeFactory_Accounts.Models
{
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

        [Required]
        [JsonProperty("passwordHash")]
        public byte[] PasswordHash { get; set; }

        [Required]
        [JsonProperty("passwordSalt")]
        public byte[] PasswordSalt { get; set; }

        [Required]
        [JsonProperty("isPromotional")]
        public bool IsPromotional { get; set; }

        public override string ToString()
        {
            return $"AccountId: {AccountId}, FirstName: {FirstName}, Email: {Email}, IsPromotional: {IsPromotional}";
        }
    }
}
