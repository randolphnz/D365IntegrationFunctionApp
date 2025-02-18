using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace D365IntegrationFunctionApp.Models
{
    [Serializable]
    public class Container
    {
        [JsonInclude]
        [JsonPropertyName("loadId")]
        public string LoadId { get; set; }
        [JsonInclude]
        [JsonPropertyName("containerType")]
        public string ContainerType { get; set; }
        [JsonInclude]
        [JsonPropertyName("items")]
        public List<Item> Items { get; set; }
    }
}
