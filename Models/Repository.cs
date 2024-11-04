using System.Text.Json.Serialization;

namespace APIAggregator.Models
{
    public class Repository
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonPropertyName("full_name")]
        public string FullName { get; set; }

        [JsonPropertyName("html_url")]
        public string HtmlUrl { get; set; }

        [JsonPropertyName("private")]
        public bool IsPrivate { get; set; }

        public string Description { get; set; }
        public string Language { get; set; }
    }
}
