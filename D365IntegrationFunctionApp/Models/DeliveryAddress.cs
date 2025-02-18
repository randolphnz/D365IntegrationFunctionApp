using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace D365IntegrationFunctionApp.Models
{
    [Serializable]
    public class DeliveryAddress
    {
        [JsonInclude]
        [JsonPropertyName("street")]
        public string Street { get; set; }
        [JsonInclude]
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonInclude]
        [JsonPropertyName("state")]
        public string State { get; set; }
        [JsonInclude]
        [JsonPropertyName("postalCode")]
        public string PostalCode { get; set; }
        [JsonInclude]
        [JsonPropertyName("country")]
        public string Country { get; set; }
    }
}
