using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AzSelfService.Models
{
    public class SubRequestViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [Required]
        [JsonProperty(PropertyName = "subscriptionName")]
        public string SubscriptionName { get; set; }


        [Required]
        [JsonProperty(PropertyName = "subscriptionType")]
        public string SubscriptionType { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*$")]
        [JsonProperty(PropertyName = "costId")]
        public string CostId { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*$")]
        [JsonProperty(PropertyName = "departmentId")]
        public string DepartmentId { get; set; }

    }
}
