using System.Text.Json.Serialization;

namespace APIAggregator.Models
{
    public class CatFactsResponse
    {
        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }
        public List<CatFact> Data { get; set; } // List of cat facts

        [JsonPropertyName("last_page")]
        public int LastPage { get; set; }

        [JsonPropertyName("last_page_url")]
        public string LastPageUrl { get; set; }

        public string Path { get; set; }

        [JsonPropertyName("per_page")]
        public int PerPage { get; set; }

        public int Total { get; set; }
    }

    public class CatFact
    {
        public string Fact { get; set; } // The text of the cat fact
        public int Length { get; set; }   // The length of the fact string
    }
}
