using System.Text.Json.Serialization;

namespace Commander.DTOs
{
    public class CommandReadDTO
    {
        public int Id { get; set; }

        [JsonPropertyName("how_to")]
        public string HowTo { get; set; }
        public string Line { get; set; }
        public string Platform { get; set; }
    }
}