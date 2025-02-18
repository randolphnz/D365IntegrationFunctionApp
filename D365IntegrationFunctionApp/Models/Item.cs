using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace D365IntegrationFunctionApp.Models
{
    [Serializable]
    public class Item
    {
        [JsonInclude]
        [JsonPropertyName("itemCode")]
        public string ItemCode { get; set; }
        [JsonInclude]
        [JsonPropertyName("quantity")]
        public int ItemQuantity { get; set; }
        [JsonInclude]
        [JsonPropertyName("cartonWeight")]
        public decimal ItemWeight { get; set; }
    }
}
