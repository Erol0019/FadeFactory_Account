using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FadeFactory_Accounts.Models
{
    public class Promotion
    {
        [Key]
        [JsonProperty("promotionId")]
        public int PromotionId { get; set; }

        [Required]
        [StringLength(255)]
        [JsonProperty("promotionSubject")]
        public string PromotionSubject { get; set; }

        [Required]
        [StringLength(1000)]
        [JsonProperty("promotionDescription")]
        public string PromotionDescription { get; set; }

        [Required]
        [JsonProperty("promotionReceivers")]
        public List<Receiver> PromotionReceivers { get; set; }
    }

    public class Receiver
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
