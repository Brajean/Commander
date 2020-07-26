using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Commander.DTOs
{
    public class CommandCreateDTO
    {
        [JsonPropertyName("how_to")]
        [Required]
        [MaxLength(250)]
        public string HowTo { get; set; }

        [Required]
        public string Line { get; set; }

        [Required]
        public string Platform { get; set; }
    }
}