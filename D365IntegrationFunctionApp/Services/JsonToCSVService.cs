using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace D365IntegrationFunctionApp.Services
{
    internal class JsonToCSVService
    {
        public static string JsonToCsv(string jsonData)
        {
            var jsonArray = JsonNode.Parse(jsonData).AsArray();
            var flattenedJsonList = new List<Dictionary<string, string>>();

            foreach (var item in jsonArray)
            {
                var flattenedJson = FlattenJson(item);
                flattenedJsonList.Add(flattenedJson);
            }

            var csvHeaders = new HashSet<string>();
            foreach (var dict in flattenedJsonList)
            {
                foreach (var key in dict.Keys)
                {
                    csvHeaders.Add(key);
                }
            }

            var csvHeaderLine = string.Join(",", csvHeaders);
            var csvLines = new List<string> { csvHeaderLine };

            foreach (var dict in flattenedJsonList)
            {
                var csvLine = string.Join(",", csvHeaders.Select(header => dict.ContainsKey(header) ? dict[header] : string.Empty));
                csvLines.Add(csvLine);
            }

            return string.Join(Environment.NewLine, csvLines);
        }

        public static Dictionary<string, string> FlattenJson(JsonNode node, string prefix = "")
        {
            var flatJson = new Dictionary<string, string>();

            if (node is JsonObject jsonObject)
            {
                foreach (var prop in jsonObject)
                {
                    foreach (var pair in FlattenJson(prop.Value, prefix + prop.Key + "_"))
                    {
                        flatJson[pair.Key] = pair.Value;
                    }
                }
            }
            else if (node is JsonArray jsonArray)
            {
                int index = 0;
                foreach (var item in jsonArray)
                {
                    foreach (var pair in FlattenJson(item, prefix + index + "_"))
                    {
                        flatJson[pair.Key] = pair.Value;
                    }
                    index++;
                }
            }
            else if (node is JsonValue jsonValue)
            {
                flatJson[prefix.TrimEnd('_')] = jsonValue.ToString();
            }

            return flatJson;
        }

    }

    
}
