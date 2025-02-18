using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace D365IntegrationFunctionApp.Models
{
    [Serializable]
    public class SalesOrder
    {
        [JsonInclude]
        [JsonPropertyName("controlNumber")]
        public int ControlNumber { get; set; }
        [JsonInclude]
        [JsonPropertyName("salesOrder")]
        public string CustomerReference { get; set; }
        [JsonInclude]
        [JsonPropertyName("containers")]
        public List<Container> Containers { get; set; }
        [JsonInclude]
        [JsonPropertyName("deliveryAddress")]
        public DeliveryAddress DeliveryAddress { get; set; }         
    }
}
